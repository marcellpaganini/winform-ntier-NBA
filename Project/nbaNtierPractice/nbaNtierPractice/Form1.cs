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
        private Player _plr;
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
                DgvSetup();
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
                if(dgvPlyers.SelectedCells.Count == 0)
                {
                    MessageBox.Show("You must select a player first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
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

                    int team = Convert.ToInt32(cboTeams.SelectedValue);
                    dgvPlyers.DataSource = service.GetPlayersByTeam(team);
                }
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
                if(txtId.Text == "")
                {
                    MessageBox.Show("You must insert an Id number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int playerId = Convert.ToInt32(txtId.Text);

                    TeamService service = new TeamService();

                    _plr = service.GetPlayer(playerId);

                    PopulateFormFields(_plr);
                }
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

                _plr = new Player();
                PopulatePlayerObject();


                if (service.AddPlayer(_plr))
                {
                    MessageBox.Show("Player inserted successfully." + Environment.NewLine +
                                    $"Player Id: " + _plr.PlayerIdOut.ToString());
                    ResetForm();
                }
                else
                {
                    string msg = "";
                    foreach (ValidationError error in _plr.Errors)
                    {
                        msg += error.Description + Environment.NewLine;
                    }
                    MessageBox.Show(msg, "Insert failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                PopulatePlayerObjectUpdate();

                if (service.ModifyPlayer(_plr))
                {
                    MessageBox.Show("Player updated successfully");
                    ResetForm();
                }
                else
                {
                    string msg = "";
                    foreach (ValidationError error in _plr.Errors)
                    {
                        msg += error.Description + Environment.NewLine;
                    }
                    MessageBox.Show(msg, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void PopulatePlayerObject()
        {

            if (cbxActive.Checked == true)
            {
                active = true;
            }
            else
            {
                active = false;
            }

            _plr.FirstName = txtName.Text.Trim();
            _plr.LastName = txtLastName.Text.Trim();
            _plr.Active = active;
            _plr.BirthDate = dtpBirthDate.Value;
            _plr.Salary = Convert.ToDecimal(txtSalary.Text.Trim());
            _plr.TeamId = Convert.ToInt32(cboAllTeams.SelectedValue);

        }

        public void PopulatePlayerObjectUpdate()
        {

            if (cbxActive.Checked == true)
            {
                active = true;
            }
            else
            {
                active = false;
            }

            _plr.PlayerId = Convert.ToInt32(txtId.Text.Trim());
            _plr.FirstName = txtName.Text.Trim();
            _plr.LastName = txtLastName.Text.Trim();
            _plr.Active = active;
            _plr.BirthDate = dtpBirthDate.Value;
            _plr.Salary = Convert.ToDecimal(txtSalary.Text.Trim());
            _plr.TeamId = Convert.ToInt32(cboAllTeams.SelectedValue);
        }

        public void ResetForm()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtLastName.Text = "";
            cbxActive.Checked = false;
            dtpBirthDate.Value = DateTime.Today;
            txtSalary.Text = "0";
            cboAllTeams.SelectedIndex = -1;
        }

        public void DgvSetup()
        {
            dgvPlyers.Columns[0].Width = 60;
            dgvPlyers.Columns[2].Width = 90;
            dgvPlyers.Columns[3].Width = 90;
            dgvPlyers.Columns[6].Width = 80;
            dgvPlyers.Columns[10].Width = 80;

            dgvPlyers.Columns[6].DefaultCellStyle.Format = "c";

            dgvPlyers.Columns[1].Visible = false;
            dgvPlyers.Columns[4].Visible = false;
            dgvPlyers.Columns[8].Visible = false;
            dgvPlyers.Columns[11].Visible = false;

            dgvPlyers.Columns[0].HeaderText = "Id";
            dgvPlyers.Columns[2].HeaderText = "First Name";
            dgvPlyers.Columns[3].HeaderText = "Last Name";
            dgvPlyers.Columns[5].HeaderText = "Birth Date";
            dgvPlyers.Columns[7].HeaderText = "Team";

            foreach (DataGridViewColumn col in dgvPlyers.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureChange()
        {
            int value = Convert.ToInt32(cboTeams.SelectedValue);

            switch (value)
            {
                case 1:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\01celtics60.jpg");
                    break;
                case 2:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\02nets.jpg");
                    break;
                case 3:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\03knicks.jpg");
                    break;
                case 4:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\0476ers.png");
                    break;
                case 5:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\05raptors.jpg");
                    break;
                case 6:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\06bulls.jpg");
                    break;
                case 7:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\07cavaliers.jpg");
                    break;
                case 8:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\08pistons.png");
                    break;
                case 9:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\09pacers.jpeg");
                    break;
                case 10:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\10bucks.jpg");
                    break;
                case 11:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\11hawks.jpg");
                    break;
                case 12:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\12hornets.png");
                    break;
                case 13:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\13heat.png");
                    break;
                case 14:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\14magic.jpg");
                    break;
                case 15:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\15wizards.png");
                    break;
                case 16:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\16nuggets.jpg");
                    break;
                case 17:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\17timberwolves.jpg");
                    break;
                case 18:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\18thunder.jpg");
                    break;
                case 19:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\19blazers.jpg");
                    break;
                case 20:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\20jazz.jpg");
                    break;
                case 21:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\21warriors.jpg");
                    break;
                case 22:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\22clippers.png");
                    break;
                case 23:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\23lakers.jpg");
                    break;
                case 24:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\24suns.jpg");
                    break;
                case 25:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\25kings.jpg");
                    break;
                case 26:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\26mavericks.jpg");
                    break;
                case 27:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\27rockets.png");
                    break;
                case 28:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\28grizzlies.jpg");
                    break;
                case 29:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\29pelicans.jpg");
                    break;
                case 30:
                    pbxEmblem.Image = Image.FromFile("C:\\Users\\pagan\\Documents\\NBCC\\Senior Year\\Fall Term\\N-tier Dev\\nba\\nTierNBA\\Project\\nbaNtierPractice\\nbaNtierPractice\\30spurs.jpg");
                    break;
            }
        }

        private void cboTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureChange();
        }

        private void dgvPlyers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRowIndex = dgvPlyers.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvPlyers.Rows[selectedRowIndex];
            int playerId = Convert.ToInt32(selectedRow.Cells[0].Value);

            TeamService service = new TeamService();

            _plr = service.GetPlayer(playerId);

            PopulateFormFields(_plr);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
