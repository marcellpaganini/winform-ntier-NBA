using Model;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TeamService
    {
        private TeamRepo repo;

        public TeamService()
        {
            repo = new TeamRepo();
        }

        public List<TeamLookup> GetTeams()
        {
            return repo.RetrieveTeamsList();
        }

        public List<TeamLookup> GetTeamsByDivision(int division)
        {
            return repo.RetrieveTeamsByDivision(division);
        }

        public List<Player> GetPlayersByTeam(int teamId)
        {
            return repo.RetrieveSearchByTeamId(teamId);
        }

        public bool Delete(int playerId)
        {
            return repo.Delete(playerId);
        }

        public Player GetPlayer(int playerId)
        {
            return repo.RetrieveById(playerId);
        }

        public bool AddPlayer(Player p)
        {
            // perform any logic that needs to be done before an employee gets inserted
            
                return repo.Insert(p);
        }

        public bool ModifyPlayer(Player p)
        {
                return repo.Update(p);
        }
    }
}
