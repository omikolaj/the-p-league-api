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

                     // leagueSessionSchedule.GamesDays.Add(newGameDay);

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
                    Match newMatch = new Match()
                    {
                        DateTime = match.DateTime,
                        HomeTeamId = match.HomeTeam.Id,
                        AwayTeamId = match.AwayTeam.Id,
                        LeagueID = match.LeagueID                        
                    };

                    leagueSessionSchedule.Matches.Add(newMatch);
                }

                leagueSessionOperations.Add(await this._sessionScheduleRepository.AddScheduleAsync(leagueSessionSchedule, ct));
            }

            // ensure all leagueSessionOperations did not return any null values
            return leagueSessionOperations.All(op => op != null);
        }

        public async Task<List<ActiveSessionInfoViewModel>> GetActiveSessionsInfoAsync(List<string> leagueIDs, CancellationToken ct = default(CancellationToken))
        {
            List<ActiveSessionInfoViewModel> activeSessions = ActiveSessionInfoConverter.ConvertList(await this._sessionScheduleRepository.GetAllActiveSessionsAsync(ct));

            return activeSessions;
            
        }

        #endregion
    }
}
