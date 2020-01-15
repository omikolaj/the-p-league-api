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
                            HomeTeamId = match.HomeTeam.Id,
                            AwayTeamId = match.AwayTeam.Id,
                            LeagueID = match.LeagueID
                        };

                        MatchResult newMatchResult = new MatchResult()
                        {
                            LeagueId = match.LeagueID,
                            HomeTeamId = match.HomeTeam.Id,
                            AwayTeamId = match.AwayTeam.Id
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

        public async Task<List<ActiveSessionInfoViewModel>> GetActiveSessionsInfoAsync(List<string> leagueIDs, CancellationToken ct = default(CancellationToken))
        {
            await this.UpdateActiveSessionsAsync(leagueIDs);

            List<ActiveSessionInfoViewModel> activeSessionsInfo = ActiveSessionInfoConverter.ConvertList(await this._sessionScheduleRepository.GetAllActiveSessionsInfoAsync(ct));
            List<ActiveSessionInfoViewModel> filteredActiveSessionsInfo = new List<ActiveSessionInfoViewModel>();

            foreach (string leagueID in leagueIDs)
            {
                filteredActiveSessionsInfo.Add(activeSessionsInfo.Where(s => s.LeagueId == leagueID).FirstOrDefault());
            }

            return filteredActiveSessionsInfo;
            
        }

        public async Task<List<LeagueSessionScheduleViewModel>> GetAllActiveSessionsAsync(CancellationToken ct = default(CancellationToken))
        {
            // this will ensure that whenever we retrieve sessions we always check to see they are active or not
            await this.UpdateActiveSessionsAsync();

            List<LeagueSessionScheduleViewModel> activeSessions = LeagueSessionScheduleConverter.ConvertList(await this._sessionScheduleRepository.GetAllActiveSessionsAsync(ct));

            // for each session loop through all matches and include the team. EF is not returning teams for some reason. HomeTeam or AwayTeam
            foreach (LeagueSessionScheduleViewModel session in activeSessions)
            {
                foreach (MatchViewModel match in session.Matches)
                {
                    match.AwayTeam = await this.GetTeamByIdAsync(match.AwayTeamId, ct);
                    match.HomeTeam = await this.GetTeamByIdAsync(match.HomeTeamId, ct);                    
                }
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

        public async Task<bool> ReportMatchesAsync(List<MatchResultViewModel> matchesResults, CancellationToken ct = default(CancellationToken))
        {
            List<bool> matchResultsReportingOperations = new List<bool>();

            foreach (MatchResultViewModel matchResult in matchesResults)
            {
                matchResultsReportingOperations.Add(await this.ReportMatchAsync(matchResult, ct) != null);
            }

            return matchResultsReportingOperations.All(op => op == true);
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
            sessionToUpdate.LeagueID = updatedSession.LeagueID;
            sessionToUpdate.NumberOfWeeks = updatedSession.NumberOfWeeks;
            sessionToUpdate.SessionEnd = updatedSession.SessionEnd;
            sessionToUpdate.SessionStart = updatedSession.SessionStart;            

            return await this._sessionScheduleRepository.UpdateLeagueSessionScheduleAsync(sessionToUpdate, ct);
        }

        public async Task<bool> UpdateSessionActiveStatusAsync(LeagueSessionScheduleViewModel session, CancellationToken ct = default(CancellationToken))
        {
            LeagueSessionSchedule sessionToUpdate = await this._sessionScheduleRepository.GetLeagueSessionScheduleByIdAsync(session.Id, ct);

            if(sessionToUpdate == null)
            {
                return false;
            }

            sessionToUpdate.Active = session.Active;

            return await this._sessionScheduleRepository.UpdateActiveStatusAsync(sessionToUpdate);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the passed in sessions based on their session end date. If the session end date is in the past from now then we will mark it as not active
        /// </summary>
        /// <param name="sessions"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        private async Task<bool> UpdateActiveSessionsAsync(List<string> leagueIDs = null, CancellationToken ct = default(CancellationToken))
        {
            List<string> allSessions = new List<string>();

            List<LeagueSessionScheduleViewModel> allActiveSessions = LeagueSessionScheduleConverter.ConvertList(await this._sessionScheduleRepository.GetAllActiveSessionsAsync(ct));
            List<bool> operations = new List<bool>();

            // if the passed in list of league ids is null set it equal to the list of leagueIDs that match all active sessions
            if (leagueIDs == null)
            {   
                leagueIDs = allActiveSessions.Select(session => session.LeagueID).ToList();
            }

            // iterate over each league ID
            foreach (string leagueID in leagueIDs)
            {
                // retrieve all of the corresponding sessions matching that league id
                List<LeagueSessionScheduleViewModel> sessions = allActiveSessions.Where(s => s.LeagueID == leagueID).ToList();                
                foreach (LeagueSessionScheduleViewModel session in sessions)
                {
                    // this means session is over. Update the 'Active' property
                    if (session.SessionEnd < DateTime.Now)
                    {
                        session.Active = false;
                        operations.Add(await this.UpdateSessionActiveStatusAsync(session, ct));
                    }
                }
                
            };

            return operations.All(op => op == true);
        }

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
