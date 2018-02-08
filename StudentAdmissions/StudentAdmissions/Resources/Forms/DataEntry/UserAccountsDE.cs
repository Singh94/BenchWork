using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;


namespace StudentAdmissions.Resources.Forms.DataEntry {
    public partial class UserAccountsDE : Form {

        public UserAccountsDE(string restrictedUser) {
            InitializeComponent();
            txtUsername.Enabled = false;
            txtUsername.Text = generateID();
            lblRUsername.Visible = false;
            lblRUsername.Text = restrictedUser;

            lblPassword.ForeColor = Color.Red;
            lblFirstName.ForeColor = Color.Red;
            lblSurname.ForeColor = Color.Red;
        }

        private void clearTextBoxes() {
            Action<Control.ControlCollection> func = null;
            func = (controls) => {
                foreach (Control control in controls)
                    if (control is TextBox) (control as TextBox).Clear();
                    else if (control is RichTextBox) (control as RichTextBox).Clear();
                    else if (control is MaskedTextBox) (control as MaskedTextBox).Clear();
                    else func(control.Controls);
            };
            func(Controls);
        }

        private string generateID() {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            string id = builder.ToString();
            return id;
        }

        private static string encryptPassword(string value) {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(value));
            return Convert.ToBase64String(hashedDataBytes);
        }

        private void lblUsername_Click(object sender, EventArgs e) {
            txtUsername.Enabled = true;
            txtUsername.Clear();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Selector selector = new Selector(lblRUsername.Text);
            selector.Show();
            this.Close();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            About about = new About();
            about.Show();
        }

        private void btnFind_Click(object sender, EventArgs e) {
            try {
                SqlDataReader sdr = null;
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\Bench\\StudentAdmissions\\StudentAdmissions\\Resources\\Databases\\UserAccounts.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM LoginDetails WHERE Username = '" + txtUsername.Text + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read()) {
                    string check = sdr["isAdmin"].ToString();

                    if (check == "Y") chbxAdmin.Checked = true;
                    if (check == "N") chbxAdmin.Checked = false;

                    txtFirstName.Text = (txtFirstName.Text = (sdr["FirstName"].ToString()));
                    txtMName.Text = (txtMName.Text = (sdr["Middlename"].ToString()));
                    txtSurname.Text = (txtSurname.Text = (sdr["Surname"].ToString()));

                    MessageBox.Show("Password for this user MUST be changed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch {
                MessageBox.Show("Record Not Found. An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            try {
                string userPassword = encryptPassword(txtPassword.Text + "MySignature" + txtUsername.Text);

                if (lblPassword.ForeColor == Color.Green && lblFirstName.ForeColor == Color.Green && lblSurname.ForeColor == Color.Green) {
                    if (!chbxAdmin.Checked) {
                        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\Bench\\StudentAdmissions\\StudentAdmissions\\Resources\\Databases\\UserAccounts.mdf;Integrated Security=True");
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO LoginDetails(Username, Password, Firstname, Middlename, Surname, isAdmin) VALUES('"
                            + txtUsername.Text + "', '"
                            + userPassword + "', '"
                            + txtFirstName.Text + "', '"
                            + txtMName.Text + "', '"
                            + txtSurname.Text + "', '"
                            + 'N' + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        clearTextBoxes();
                        MessageBox.Show("User successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else {
                        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\Bench\\StudentAdmissions\\StudentAdmissions\\Resources\\Databases\\UserAccounts.mdf;Integrated Security=True");
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO LoginDetails(Username, Password, Firstname, Middlename, Surname, isAdmin) VALUES('"
                            + txtUsername.Text + "', '"
                            + userPassword + "', '"
                            + txtFirstName.Text + "', '"
                            + txtMName.Text + "', '"
                            + txtSurname.Text + "', '"
                            + 'Y' + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        clearTextBoxes();
                        MessageBox.Show("User successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else {
                    string outputMessage = "";
                    if (lblPassword.ForeColor == Color.Red) outputMessage += "Please ensure password contains Uppercase and Lowercase letters, a Digit and a Special character. Passwords need to be at least 8 characters. \n";
                    if (lblFirstName.ForeColor == Color.Red) outputMessage += "Please ensure you have entered a Firstname. \n";
                    if (lblSurname.ForeColor == Color.Red) outputMessage += "Please ensure you have enered a Surname. \n";
                    MessageBox.Show(outputMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch {
                MessageBox.Show("Record Not Added. An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e) {

            string check = "";

            if (chbxAdmin.Checked) check = "Y";
            if (!chbxAdmin.Checked) check = "N";

            try {
                if (lblPassword.ForeColor == Color.Green && lblFirstName.ForeColor == Color.Green && lblSurname.ForeColor == Color.Green) {

                    string userPassword = encryptPassword(txtPassword.Text + "MySignature" + txtUsername.Text);
                    SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\Bench\\StudentAdmissions\\StudentAdmissions\\Resources\\Databases\\UserAccounts.mdf;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE LoginDetails SET Firstname = '" + txtFirstName.Text + "'," + "Middlename = '" + txtMName.Text + "', " + "Surname = '" + txtSurname.Text + "', " + "Password = '" + userPassword + "'," + "isAdmin = '" + check + "' WHERE Username = '" + txtUsername.Text + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Successfully Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearTextBoxes();
                    chbxAdmin.Checked = false;
                }
                else {
                    string outputMessage = "";
                    if (lblPassword.ForeColor == Color.Red) outputMessage += "Please ensure password contains Uppercase and Lowercase letters, a Digit and a Special character. Passwords need to be at least 8 characters. \n";
                    if (lblFirstName.ForeColor == Color.Red) outputMessage += "Please ensure you have entered a Firstname. \n";
                    if (lblSurname.ForeColor == Color.Red) outputMessage += "Please ensure you have enered a Surname. \n";
                    MessageBox.Show(outputMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch {
                MessageBox.Show("Record Not Updated. An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            try {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\Bench\\StudentAdmissions\\StudentAdmissions\\Resources\\Databases\\UserAccounts.mdf;Integrated Security=True");
                if (lblRUsername.Text == txtUsername.Text) {
                    MessageBox.Show("WARNING: Unable to delete your own account!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM LoginDetails WHERE Username = '" + txtUsername.Text + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    clearTextBoxes();
                    MessageBox.Show("Record Successfully Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch {
                MessageBox.Show("Record Not Deleted. An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e) {
            clearTextBoxes();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e) {
            if (Regex.IsMatch(txtPassword.Text.Trim(), "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9_@.-]).{8,}$")) lblPassword.ForeColor = Color.Green;
            else lblPassword.ForeColor = Color.Red;
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e) {
            if (txtFirstName.Text != "") lblFirstName.ForeColor = Color.Green;
            else lblFirstName.ForeColor = Color.Red;
        }

        private void txtSurname_TextChanged(object sender, EventArgs e) {
            if (txtSurname.Text != "") lblSurname.ForeColor = Color.Green;
            else lblSurname.ForeColor = Color.Red;
        }

        private void lblPassword_Click(object sender, EventArgs e) {
            txtPassword.Clear();
        }
    }
}
