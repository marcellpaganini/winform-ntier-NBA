using Model;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Types;

namespace nbaNtierPractice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvPlyers.RowHeadersVisible = false;
            LoadTeams();
            cboDivision.DataSource = Enum.GetValues(typeof(Division));
        }

        private void LoadTeams()
        {
            TeamService service = new TeamService();
            List<TeamLookup> teams = service.GetTeams();
            teams.Insert(0, new TeamLookup { TeamId = 0, TeamName = "" });
            cboAllTeams.ValueMember = "TeamId";
            cboAllTeams.DisplayMember = "TeamName";
            cboAllTeams.DataSource = teams;
            cboAllTeams.SelectedIndex = -1;
        }

        private void cboDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int division = cboDivision.SelectedIndex;
            TeamService service = new TeamService();
            List<TeamLookup> teams = new List<TeamLookup>();
            teams = service.GetTeamsByDivision(division);

            teams.Insert(0, new TeamLookup { TeamId = 0, TeamName = "" });
            cboTeams.ValueMember = "TeamId";
            cboTeams.DisplayMember = "TeamName";
            cboTeams.DataSource = teams;
            cboTeams.SelectedIndex = -1;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            int team = Convert.ToInt32(cboTeams.SelectedValue);
            TeamService service = new TeamService();
            dgvPlyers.DataSource = service.GetPlayersByTeam(team);
            dgvPlyers.Columns[0].Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dgvPlyers.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvPlyers.Rows[selectedRowIndex];
            int playerId = Convert.ToInt32(selectedRow.Cells[0].Value);

            if (MessageBox.Show("Are you sure you want to delete the current record?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            TeamService service = new TeamService();
            service.Delete(playerId);
        }
    }
}
