using Model;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
            if (Validate(p))
            {
                return repo.Insert(p);
            }
            else
            {
                return false;
            }
        }

        public bool ModifyPlayer(Player p)
        {
            if (Validate(p))
                return repo.Update(p);

            return false;
        }

        #region Private Methods
        private bool Validate(Player playerToValidate)
        {
            ValidationContext context = new ValidationContext(playerToValidate);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(playerToValidate, context, results, true);

            foreach(ValidationResult p in results)
            {
                playerToValidate.AddError(new ValidationError(p.ErrorMessage));
            }

            return playerToValidate.Errors.Count == 0;
        }
        #endregion
    }
}
