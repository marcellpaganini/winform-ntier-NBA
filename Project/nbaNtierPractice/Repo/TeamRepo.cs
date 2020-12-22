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

        public Player RetrieveById(int playerId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@PlayerId", playerId, 0, SqlDbType.Int, ParameterDirection.Input));

            DataTable dt = db.GetData("RetrievePlayerByPlayerId", parms);

            return PopulatePlayerRecord(dt.Rows[0]);
        }

        private Player PopulatePlayerRecord(DataRow row)
        {
            Player p = new Player();

            p.PlayerId = Convert.ToInt32(row["PlayerId"]);
            p.FirstName = row["FirstName"].ToString();
            p.LastName = row["LastName"].ToString();
            p.Active = (bool)row["Active"];
            p.BirthDate = Convert.ToDateTime(row["BirthDate"]);
            p.Salary = Convert.ToDecimal(row["Salary"]);
            if (row["TeamId"] == DBNull.Value)
            {
                p.TeamId = null;
            }
            else
            {
                p.TeamId = Convert.ToInt32(row["TeamId"]);
            }

            return p;
        }

        public bool Insert(Player p)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@FirstName", p.FirstName, 50, SqlDbType.VarChar, ParameterDirection.Input));
            parms.Add(new ParmStruct("@LastName", p.LastName, 50, SqlDbType.VarChar, ParameterDirection.Input));
            parms.Add(new ParmStruct("@Active", p.Active, 0, SqlDbType.Bit, ParameterDirection.Input));
            parms.Add(new ParmStruct("@BirthDate", p.BirthDate, 0, SqlDbType.DateTime2, ParameterDirection.Input));
            parms.Add(new ParmStruct("@Salary", p.Salary, 19, SqlDbType.Decimal, ParameterDirection.Input));
            parms.Add(new ParmStruct("@TeamId", p.TeamId, 0, SqlDbType.Int, ParameterDirection.Input));

            if (db.SendData("InsertPlayer", parms) > 0)
            {
                return false;
            }

            return true;
        }

        public bool Update(Player p)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@PlayerId", p.PlayerId, 0, SqlDbType.Int, ParameterDirection.Input));
            parms.Add(new ParmStruct("@FirstName", p.FirstName, 50, SqlDbType.VarChar, ParameterDirection.Input));
            parms.Add(new ParmStruct("@LastName", p.LastName, 50, SqlDbType.VarChar, ParameterDirection.Input));
            parms.Add(new ParmStruct("@Active", p.Active, 0, SqlDbType.Bit, ParameterDirection.Input));
            parms.Add(new ParmStruct("@BirthDate", p.BirthDate, 0, SqlDbType.DateTime2, ParameterDirection.Input));
            parms.Add(new ParmStruct("@Salary", p.Salary, 19, SqlDbType.Decimal, ParameterDirection.Input));
            parms.Add(new ParmStruct("@TeamId", p.TeamId, 0, SqlDbType.Int, ParameterDirection.Input));

            if (db.SendData("UpdatePlayer", parms) > 0)
            {
                return false;
            }

            return true;
        }
    }
}
