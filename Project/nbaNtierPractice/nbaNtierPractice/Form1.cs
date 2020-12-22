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
        bool active;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dgvPlyers.RowHeadersVisible = false;
                LoadTeams();
                cboDivision.DataSource = Enum.GetValues(typeof(Division));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                int team = Convert.ToInt32(cboTeams.SelectedValue);
                TeamService service = new TeamService();
                dgvPlyers.DataSource = service.GetPlayersByTeam(team);
                dgvPlyers.Columns[0].Visible = false;
                dgvPlyers.Columns[3].Visible = false;
                dgvPlyers.Columns[7].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            try
            {
                int playerId = Convert.ToInt32(txtId.Text);

                TeamService service = new TeamService();

                Player player = new Player();
                player = service.GetPlayer(playerId);

                PopulateFormFields(player);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                TeamService service = new TeamService();

                if (!service.AddPlayer(PopulatePlayerObject()))
                {
                    MessageBox.Show("Insert failed");
                }
                else
                {
                    MessageBox.Show("Player inserted successfully");
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text == string.Empty)
                {
                    MessageBox.Show("You  must retrieve a record first.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TeamService service = new TeamService();

                if (!service.ModifyPlayer(PopulatePlayerObjectUpdate()))
                {
                    MessageBox.Show("Update failed", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Player updated successfully");
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void PopulateFormFields(Player p)
        {
            if (p.Active == true)
            {
                cbxActive.Checked = true;
                active = true;
            }
            else
            {
                cbxActive.Checked = false;
                active = false;
            }

            txtId.Text = p.PlayerId.ToString();
            txtName.Text = p.FirstName.ToString();
            txtLastName.Text = p.LastName.ToString();
            active = p.Active;
            dtpBirthDate.Value = p.BirthDate;
            txtSalary.Text = p.Salary.ToString();
            if (p.TeamId == null)
            {
                cboAllTeams.SelectedIndex = -1;
            }
            else
            {
                cboAllTeams.SelectedValue = p.TeamId;
            }
            
        }

        public Player PopulatePlayerObject()
        {
            Player p = new Player();

            if (cbxActive.Checked == true)
            {
                active = true;
            }
            else
            {
                active = false;
            }

            p.FirstName = txtName.Text.Trim();
            p.LastName = txtLastName.Text.Trim();
            p.Active = active;
            p.BirthDate = dtpBirthDate.Value;
            p.Salary = Convert.ToDecimal(txtSalary.Text.Trim());
            p.TeamId = Convert.ToInt32(cboAllTeams.SelectedValue);

            return p;
        }

        public Player PopulatePlayerObjectUpdate()
        {
            Player p = new Player();

            if (cbxActive.Checked == true)
            {
                active = true;
            }
            else
            {
                active = false;
            }

            p.PlayerId = Convert.ToInt32(txtId.Text.Trim());
            p.FirstName = txtName.Text.Trim();
            p.LastName = txtLastName.Text.Trim();
            p.Active = active;
            p.BirthDate = dtpBirthDate.Value;
            p.Salary = Convert.ToDecimal(txtSalary.Text.Trim());
            p.TeamId = Convert.ToInt32(cboAllTeams.SelectedValue);

            return p;
        }

        public void ResetForm()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtLastName.Text = "";
            cbxActive.Checked = false;
            dtpBirthDate.Value = DateTime.Today;
            txtSalary.Text = "";
            cboAllTeams.SelectedIndex = -1;
        }
    }
}
