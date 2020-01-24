using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePLeagueDomain.Converters.Schedule;
using ThePLeagueDomain.Models.Schedule;
using ThePLeagueDomain.ViewModels.Schedule;

namespace ThePLeagueDomain.Supervisor
{
    public partial class ThePLeagueSupervisor : IThePLeagueSupervisor
    {
        #region Constants

        protected const string DRAW = "Draw";
            
        #endregion

        #region Methods

        public async Task<bool> PublishSessionsSchedulesAsync(List<LeagueSessionScheduleViewModel> newLeagueSessionsSchedules, CancellationToken ct = default(CancellationToken))
        {
            List<LeagueSessionSchedule> leagueSessionOperations = new List<LeagueSessionSchedule>();

            foreach (LeagueSessionScheduleViewModel newSchedule in newLeagueSessionsSchedules)
            {
                LeagueSessionSchedule leagueSessionSchedule = new LeagueSessionSchedule()
                {
                    Active = newSchedule.Active,
                    LeagueID = newSchedule.LeagueID,
                    ByeWeeks = newSchedule.ByeWeeks,
                    NumberOfWeeks = newSchedule.NumberOfWeeks,
                    SessionStart = newSchedule.SessionStart,
                    SessionEnd = newSchedule.SessionEnd                    
                };                

                // create game day entry for all configured game days
                foreach (GameDayViewModel gameDay in newSchedule.GamesDays)
                {
                    GameDay newGameDay = new GameDay()
                    {
                        GamesDay = gameDay.GamesDay
                    };
                    
                    // create game time entry for every game day
                    foreach (GameTimeViewModel gameTime in gameDay.GamesTimes)
                    {
                        GameTime newGameTime = new GameTime()
                        {
                            GamesTime = DateTimeOffset.FromUnixTimeSeconds(gameTime.GamesTime).DateTime.ToLocalTime(),                            
                        };
                                 
                        newGameDay.GamesTimes.Add(newGameTime);
                    }

                    leagueSessionSchedule.GamesDays.Add(newGameDay);
                }

                // update teams sessions
                foreach (TeamSessionViewModel teamSession in newSchedule.TeamsSessions)
                {
                    // retrieve the team with the corresponding id
                    Team team = await this._teamRepository.GetByIdAsync(teamSession.TeamId, ct);

                    if(team != null)
                    {
                        TeamSession newTeamSession = new TeamSession()
                        {
                            Team = team,                            
                            LeagueSessionSchedule = leagueSessionSchedule
                        };

                        leagueSessionSchedule.TeamsSessions.Add(newTeamSession);
                    }
                }

                // update matches for this session
                foreach (MatchViewModel match in newSchedule.Matches)
                {
                    // if match DateTime is not set do not add it to the database
                    if(match.DateTime != 0)
                    {
                        Match newMatch = new Match()
                        {
                            DateTime = match.DateTime,
                            HomeTeamId = match.HomeTeamId,
                            AwayTeamId = match.AwayTeamId,
                            LeagueID = match.LeagueID
                        };

                        MatchResult newMatchResult = new MatchResult()
                        {
                            LeagueId = match.LeagueID,
                            HomeTeamId = match.HomeTeamId,
                            AwayTeamId = match.AwayTeamId
                        };

                        newMatch.MatchResult = newMatchResult;

                        leagueSessionSchedule.Matches.Add(newMatch);
                    }                    
                }

                leagueSessionOperations.Add(await this._sessionScheduleRepository.AddScheduleAsync(leagueSessionSchedule, ct));
            }

            // ensure all leagueSessionOperations did not return any null values
            return leagueSessionOperations.All(op => op != null);
        }
        
        public async Task<List<LeagueSessionScheduleViewModel>> GetAllSessionsByLeagueIdAsync(string leagueId, CancellationToken ct = default)
        {
            List<LeagueSessionScheduleViewModel> leagueSessions = LeagueSessionScheduleConverter.ConvertList(await this._sessionScheduleRepository.GetAllSessionsByLeagueIdAsync(leagueId, ct));

            return leagueSessions;
        }

        public async Task<List<ActiveSessionInfoViewModel>> GetActiveSessionsInfoAsync(List<string> leagueIDs, CancellationToken ct = default(CancellationToken))
        {
            //await this.UpdateActiveSessionsAsync(leagueIDs);

            List<LeagueSessionScheduleViewModel> activeSessions = LeagueSessionScheduleConverter.ConvertList(await this._sessionScheduleRepository.GetAllActiveSessionsAsync(ct)); // use GetAllSessions and filter the list
            List<ActiveSessionInfoViewModel> filteredActiveSessionsInfo = activeSessions.Where(s => s.Active == true).Select(session => new ActiveSessionInfoViewModel
            {
                SessionId = session.Id,
                LeagueId = session.LeagueID,
                StartDate = session.SessionStart,
                EndDate = session.SessionEnd
            }).ToList();

            foreach (string leagueID in leagueIDs)
            {
                filteredActiveSessionsInfo.Add(filteredActiveSessionsInfo.Where(s => s.LeagueId == leagueID).FirstOrDefault());
            }

            return filteredActiveSessionsInfo;
            
        }

        public async Task<List<LeagueSessionScheduleViewModel>> GetAllActiveSessionsAsync(CancellationToken ct = default(CancellationToken))
        {
            // this will ensure that whenever we retrieve sessions we always check to see they are active or not
            // await this.UpdateActiveSessionsAsync();

            List<LeagueSessionScheduleViewModel> activeSessions = LeagueSessionScheduleConverter.ConvertList(await this._sessionScheduleRepository.GetAllActiveSessionsAsync(ct));
            HashSet<TeamViewModel> teams = new HashSet<TeamViewModel>();

            // for each session loop through all matches and include the team. EF is not returning teams for some reason. HomeTeam or AwayTeam
            foreach (LeagueSessionScheduleViewModel session in activeSessions)
            {
                foreach (MatchViewModel match in session.Matches)
                {
                    TeamViewModel awayTeam = await this.GetTeamByIdAsync(match.AwayTeamId, ct);
                    TeamViewModel homeTeam = await this.GetTeamByIdAsync(match.HomeTeamId, ct);                                        

                    match.AwayTeamName = awayTeam.Name;
                    match.HomeTeamName = homeTeam.Name;
                    match.MatchResult.SessionId = session.Id;

                    // stash retrieved teams so we don't have to hit the database again to retrieve team names
                    teams.Add(awayTeam);
                    teams.Add(homeTeam);
                }

                // teamSession.teamName is used by the front end scoreboard to avoid having dependency on sport types store. Before
                // /scoreboards route expected sport types store to have values, but if user navigates straight to scoreboards then
                // we cannot display a filter to allow user to filter by team name easily
                foreach (TeamSessionViewModel teamSession in session.TeamsSessions)
                {
                    teamSession.TeamName = teams.Where(t => t.Id == teamSession.TeamId).FirstOrDefault()?.Name;
                }

                LeagueViewModel league = await GetLeagueByIdAsync(session.LeagueID, ct);

                // these properties are not set by converters because they do not belong on the model
                session.LeagueName = league?.Name;

                SportTypeViewModel sportType = await GetSportTypeByIdAsync(league.SportTypeID, ct);
                // these properties are not set by converters because they do not belong on the model
                session.SportTypeID = sportType?.Id;
                session.SportTypeName = sportType?.Name;
                
            }

            return activeSessions;
        }

        public async Task<MatchResultViewModel> ReportMatchAsync(MatchResultViewModel matchResult, CancellationToken ct = default(CancellationToken))
        {
            MatchResult reportMatch = new MatchResult()
            {
                MatchResultId = matchResult.MatchResultId,
                MatchId = matchResult.MatchId,                
                AwayTeamScore = matchResult.AwayTeamScore,
                AwayTeamId = matchResult.AwayTeamId,
                HomeTeamScore = matchResult.HomeTeamScore,
                HomeTeamId = matchResult.HomeTeamId,
                LeagueId = matchResult.LeagueId,
                // mark this match as completed
                Status = MatchStatus.Completed
            };

            // home team's score will always be first
            reportMatch.Score = $"{matchResult.HomeTeamScore} : {matchResult.AwayTeamScore}";
            await this.SetWinnerLoserTeamNames(reportMatch);

            matchResult = MatchResultConverter.Convert(await this._sessionScheduleRepository.ReportMatchAsync(reportMatch, ct));

            // retrieve session id
            MatchViewModel matchViewModel = MatchConverter.Convert(await this._sessionScheduleRepository.GetMatchByIdAsync(matchResult.MatchId));
            if (matchViewModel != null)
            {
                matchResult.SessionId = matchViewModel.SessionId;
            }

            return matchResult;

        }

        public async Task<bool> UpdateSessionScheduleAsync(LeagueSessionScheduleViewModel updatedSession, CancellationToken ct = default(CancellationToken))
        {
            LeagueSessionSchedule sessionToUpdate = await this._sessionScheduleRepository.GetLeagueSessionScheduleByIdAsync(updatedSession.Id, ct);
            if(sessionToUpdate == null)
            {
                return false;
            }

            sessionToUpdate.Active = updatedSession.Active;
            sessionToUpdate.ByeWeeks = updatedSession.ByeWeeks;            
            sessionToUpdate.LeagueID = updatedSession.LeagueID ?? sessionToUpdate.LeagueID;
            sessionToUpdate.NumberOfWeeks = updatedSession.NumberOfWeeks;
            sessionToUpdate.SessionEnd = updatedSession.SessionEnd;
            sessionToUpdate.SessionStart = updatedSession.SessionStart;

            return await this._sessionScheduleRepository.UpdateSessionScheduleAsync(sessionToUpdate, ct);
        }
     
        #endregion

        #region Private Methods
        
        private async Task SetWinnerLoserTeamNames(MatchResult matchResult, CancellationToken ct = default)
        {
            if(matchResult.HomeTeamScore == matchResult.AwayTeamScore)
            {
                matchResult.WonTeamName = DRAW;
                matchResult.LostTeamName = DRAW;
            }
            else
            {
                TeamViewModel winner;
                TeamViewModel loser;
                if (matchResult.HomeTeamScore > matchResult.AwayTeamScore)
                {
                    winner = await GetTeamByIdAsync(matchResult.HomeTeamId, ct);
                    loser = await GetTeamByIdAsync(matchResult.AwayTeamId, ct);
                }
                else
                {
                    winner = await GetTeamByIdAsync(matchResult.AwayTeamId, ct);
                    loser = await GetTeamByIdAsync(matchResult.HomeTeamId, ct);
                }

                matchResult.WonTeamName = winner.Name;
                matchResult.LostTeamName = loser.Name;

            }

        }

        #endregion
    }
}
