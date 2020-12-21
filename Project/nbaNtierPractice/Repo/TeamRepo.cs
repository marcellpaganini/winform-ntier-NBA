using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types;

namespace Repo
{
    public class TeamRepo
    {
        private DataAccess db;

        public TeamRepo()
        {
            db = new DataAccess();
        }

        public List<TeamLookup> RetrieveTeamsList()
        {
            DataTable dt = db.GetData("TeamsLookup");

            List<TeamLookup> teams = new List<TeamLookup>();

            foreach (DataRow row in dt.Rows)
            {
                teams.Add(
                    new TeamLookup
                    {
                        TeamId = Convert.ToInt32(row["teamId"]),
                        TeamName = row["teamName"].ToString()
                    }
                );
            }

            return teams;
        }

        public List<TeamLookup> RetrieveTeamsByDivision(int division)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@Division", division, 0, SqlDbType.Int, ParameterDirection.Input));

            DataTable dt = db.GetData("RetrieveTeamByDivision", parms);

            List<TeamLookup> teams = new List<TeamLookup>();

            foreach (DataRow row in dt.Rows)
            {
                teams.Add(
                    new TeamLookup
                    {
                        TeamId = Convert.ToInt32(row["TeamId"]),
                        TeamName = row["TeamName"].ToString()
                    }
                );
            }
            return teams;
        }

        public List<Player> RetrieveSearchByTeamId(int teamId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@teamId", teamId, 0, SqlDbType.Int, ParameterDirection.Input));

            DataTable dt = db.GetData("RetrievePlayerByTeamId", parms);

            List<Player> players = new List<Player>();

            foreach (DataRow row in dt.Rows)
            {
                players.Add(
                    new Player
                    {
                        PlayerId = Convert.ToInt32(row["PlayerId"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        BirthDate = Convert.ToDateTime(row["BirthDate"]),
                        Salary = Convert.ToDecimal(row["Salary"]),
                        TeamName = row["TeamName"].ToString(),
                        Arena = row["Arena"].ToString(),
                        Conference = row["ConfName"].ToString()
                    }
                );
            }
            return players;
        }

        public bool Delete(int playerId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@PlayerId", playerId, 0, SqlDbType.Int, ParameterDirection.Input));

            int retVal = db.SendData("DeletePlayerById", parms);

            if (retVal > 0)
            {
                return true;
            }

            return false;
        }
    }
}
