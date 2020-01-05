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
            List<ActiveSessionInfoViewModel> activeSessionsInfo = ActiveSessionInfoConverter.ConvertList(await this._sessionScheduleRepository.GetAllActiveSessionsInfoAsync(ct));

            return activeSessionsInfo;
            
        }

        public async Task<List<LeagueSessionScheduleViewModel>> GetAllActiveSessions(CancellationToken ct = default(CancellationToken))
        {
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

        public async Task<bool> ReportMatch(MatchResultViewModel matchResult, CancellationToken ct = default(CancellationToken))
        {
            MatchResult reportMatch = new MatchResult()
            {
                MatchResultId = matchResult.Id,
                MatchId = matchResult.MatchId,
                Status = matchResult.Status,
                AwayTeamScore = matchResult.AwayTeamScore,
                AwayTeamId = matchResult.AwayTeamId,
                HomeTeamScore = matchResult.HomeTeamScore,
                HomeTeamId = matchResult.HomeTeamId,
                Score = matchResult.Score,
                WonTeamName = matchResult.WonTeamName,
                LostTeamName = matchResult.LostTeamName
            };

            return await this._sessionScheduleRepository.ReportMatch(reportMatch, ct);
        }

        public async Task<bool> ReportMatches(List<MatchResultViewModel> matchesResults, CancellationToken ct = default(CancellationToken))
        {
            List<bool> matchResultsReportingOperations = new List<bool>();

            foreach (MatchResultViewModel matchResult in matchesResults)
            {
                matchResultsReportingOperations.Add(await this.ReportMatch(matchResult, ct));
            }

            return matchResultsReportingOperations.All(op => op == true);
        }

        #endregion
    }
}
