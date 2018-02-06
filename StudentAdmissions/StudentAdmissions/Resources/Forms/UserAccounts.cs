using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;


namespace StudentAdmissions.Resources.Froms {
    public partial class UserAccounts : Form {

        public UserAccounts() {
            InitializeComponent();
            txtUsername.Enabled = false;
            txtUsername.Text = generateID();
            //checkValues();
        }

        private void checkValues() {
            do {
                if (!Regex.IsMatch(txtPassword.Text.Trim(), "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9_@.-]).{8,}$")) lblPassword.ForeColor = Color.Red;
                if (txtFirstName.Text == "") lblFirstName.ForeColor = Color.Red;
                if (txtSurname.Text == "") lblSurname.ForeColor = Color.Red;

                if (Regex.IsMatch(txtPassword.Text.Trim(), "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9_@.-]).{8,}$")) lblPassword.ForeColor = Color.Green;
                if (txtFirstName.Text != "") lblFirstName.ForeColor = Color.Green;
                if (txtSurname.Text != "") lblSurname.ForeColor = Color.Green;
            } while (true);
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

        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void btnFind_Click(object sender, EventArgs e) {
            string userPassword = encryptPassword(txtPassword.Text + "MySignature" + txtUsername.Text);
            SqlDataReader sdr = null;
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\BenchWork\\StudentAdmissions\\StudentAdmissions\\Resources\\Database\\LoginDatabase.mdf;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM LoginDetails WHERE Username = '" + txtUsername.Text + "'", con);
            sdr = cmd.ExecuteReader();
            while (sdr.Read()) {
                txtFirstName.Text = (txtFirstName.Text = (sdr["FirstName"].ToString()));
                txtMName.Text = (txtMName.Text = (sdr["Middlename"].ToString()));
                txtSurname.Text = (txtSurname.Text = (sdr["Surname"].ToString()));
                txtPassword.Text = userPassword;
            }
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e) {

            string userPassword = encryptPassword(txtPassword.Text + "MySignature" + txtUsername.Text);

            if (chbxAdmin == null) {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\BenchWork\\StudentAdmissions\\StudentAdmissions\\Resources\\Database\\LoginDatabase.mdf;Integrated Security=True");
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
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\BenchWork\\StudentAdmissions\\StudentAdmissions\\Resources\\Database\\LoginDatabase.mdf;Integrated Security=True");
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

        private void btnUpdate_Click(object sender, EventArgs e) {
            string userPassword = encryptPassword(txtPassword.Text + "MySignature" + txtUsername.Text);
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\BenchWork\\StudentAdmissions\\StudentAdmissions\\Resources\\Database\\LoginDatabase.mdf;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE LoginDetails SET Firstname = '" + txtFirstName.Text + "'," + "Middlename = '" + txtMName.Text + "', " + "Surname = '" + txtSurname.Text + "', " + "Password = '" + userPassword + "'" + " WHERE Username = '" + txtUsername.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Successfully Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clearTextBoxes();
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\BenchWork\\StudentAdmissions\\StudentAdmissions\\Resources\\Database\\LoginDatabase.mdf;Integrated Security=True");
            if (true) {
                MessageBox.Show("WARNING: Unable to delete your own account!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                MessageBox.Show("Record Successfully Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e) {
            clearTextBoxes();
        }
    }
}
