using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace StudentAdmissions.Resources.Forms.DataEntry {
    public partial class StudentAccountsDE : Form {
        public StudentAccountsDE(string restrictedUser) {
            InitializeComponent();
            lblRUsername.Text = restrictedUser;
            txtUsername.Enabled = false;
            cmboGrade.DropDownStyle = ComboBoxStyle.DropDownList;
            lblRUsername.Visible = false;
            chlbModules.Enabled = false;
            lblFYear.Visible = false;
        }

        private string fl = "";
        private string sl = "";

        private void clearTextBoxes() {
            Action<Control.ControlCollection> func = null;
            func = (controls) => {
                foreach (Control control in controls)
                    if (control is TextBox) (control as TextBox).Clear();
                    else if (control is RichTextBox) (control as RichTextBox).Clear();
                    else if (control is MaskedTextBox) (control as MaskedTextBox).Clear();
                    else if (control is DateTimePicker) (control as DateTimePicker).ResetText();
                    else func(control.Controls);
            };
            func(Controls);
        }

        private void checkGrades() {
            if (cmboGrade.Text == "CD" || cmboGrade.Text == "DD" || cmboGrade.Text == "DE" || cmboGrade.Text == "EE" || cmboGrade.Text == "PPP" || cmboGrade.Text == "MPP" || cmboGrade.Text == "MMP" || cmboGrade.Text == "MMM") {
                chlbModules.Enabled = false;
                lblFYear.ForeColor = Color.Red;
                lblFYear.Visible = true;
            }
            else {
                chlbModules.Enabled = true;
                lblFYear.Visible = false;
            }
        }

        private string generateID() {
            int dateEnumerate = DateTime.Now.Year % 100;
            var random = new Random();
            string id = "P" + dateEnumerate.ToString() + new string(Enumerable.Repeat("0123456789ABCDEF", 6).Select(s => s[random.Next(s.Length)]).ToArray()) + fl + sl;
            return id;
        }

        private static string encryptPassword(string value) {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(value));
            return Convert.ToBase64String(hashedDataBytes);
        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e) {

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

        private void cmboGrade_TextChanged(object sender, EventArgs e) {
            checkGrades();
        }

        private void cmboGrade_KeyUp(object sender, KeyEventArgs e) {
            checkGrades();
        }

        private void cmboGrade_KeyDown(object sender, KeyEventArgs e) {
            checkGrades();
        }

        private void btnGenerateID_Click(object sender, EventArgs e) {
            if (txtFirstName.Text == "" || txtSurname.Text == "") {
                string output = "";
                if (txtFirstName.Text == "") output += "Please Enter a First Name \n";
                if (txtSurname.Text == "") output += "Please Enter a Surname \n";
                MessageBox.Show(output, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                fl = txtFirstName.Text.Substring(0, 1).ToUpper();
                sl = txtSurname.Text.Substring(0, 1).ToUpper();
                txtUsername.Text = generateID();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            try {
                string password = "Password";
                password += txtFirstName.Text.Substring(0, 1).ToUpper();
                password += txtSurname.Text.Substring(0, 1).ToUpper();
                string userPassword = encryptPassword(password);

                float homeworkGrade = Int32.Parse(txtHomeworkGrade.Text);
                float examGrade = Int32.Parse(txtExamGrade.Text);
                double overallGrade = (homeworkGrade * 0.4) + (examGrade * 0.6);

                string checkedItems = "";

                if (chlbModules.CheckedItems.Count > 0) {
                    for (int i = 0; i < chlbModules.CheckedItems.Count; i++) {
                        checkedItems += "<" + chlbModules.CheckedItems[i].ToString() + ">";
                    }
                }

                if (txtUsername.Text != "" && txtFirstName.Text != "" && txtSurname.Text != "") {

                    if (lblFYear.Visible == false) {
                        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\Bench\\StudentAdmissions\\StudentAdmissions\\Resources\\Databases\\UserAccounts.mdf;Integrated Security=True");
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "INSERT INTO StudentDetails(Username, Password, Firstname, Middlename, Surname, DoB, PreviousGrade, Modules, HWGrade, ExamGrade, OverallGrade, ExtraCurricular, Concerns) VALUES('"
                            + txtUsername.Text + "', '"
                            + userPassword + "', '"
                            + txtFirstName.Text + "', '"
                            + txtMName.Text + "', '"
                            + txtSurname.Text + "', '"
                            + dtpDOB.Value.Date + "', '"
                            + cmboGrade.SelectedItem + "', '"
                            + checkedItems + "', '"
                            + homeworkGrade + "', '"
                            + examGrade + "', '"
                            + overallGrade + "', '"
                            + rtxtActivites.Text + "', '"
                            + rtxtConcerns.Text + "')";
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

                        cmd.CommandText = "INSERT INTO StudentDetails(Username, Password, Firstname, Middlename, Surname, DoB, PreviousGrade, Modules, HWGrade, ExamGrade, OverallGrade, ExtraCurricular, Concerns) VALUES('"
                            + txtUsername.Text + "', '"
                            + userPassword + "', '"
                            + txtFirstName.Text + "', '"
                            + txtMName.Text + "', '"
                            + txtSurname.Text + "', '"
                            + dtpDOB.Value.Date + "', '"
                            + cmboGrade.SelectedItem.ToString() + "', '"
                            + "Foundation Year" + "', '"
                            + homeworkGrade + "', '"
                            + examGrade + "', '"
                            + overallGrade + "', '"
                            + rtxtActivites.Text + "', '"
                            + rtxtConcerns.Text + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        clearTextBoxes();
                        MessageBox.Show("User successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else {
                    string output = "";

                    if (txtUsername.Text == "") output += "Please Press the Generate ID Button \n";
                    if (txtFirstName.Text == "") output += "Please Ensure A First Name Is Entered \n";
                    if (txtSurname.Text == "") output += "Please Ensure A Surname Is Entered \n";

                    MessageBox.Show(output, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch {
                MessageBox.Show("Record Not Added. An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblUsername_Click(object sender, EventArgs e) {
            if (txtUsername.Enabled != true) txtUsername.Enabled = true;
        }

        private void btnResetPassword_Click(object sender, EventArgs e) {
            if (txtUsername.Text != "") {

                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\Bench\\StudentAdmissions\\StudentAdmissions\\Resources\\Databases\\UserAccounts.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM StudentDetails WHERE Username = '" + txtUsername.Text + "'", con);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read()) {
                    StudentResetPassword studentResetPassword = new StudentResetPassword();
                    studentResetPassword.Text = txtUsername.Text + " : Password Reset";
                    studentResetPassword.Show(); ;
                }
                else {
                    MessageBox.Show("Username Not Found. Please Check Username and Re-Enter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtUsername.Focus();
                }
                con.Close();
                
            }
            else MessageBox.Show("Please Ensure Username Is Entered Into Text Box", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            try {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\Bench\\StudentAdmissions\\StudentAdmissions\\Resources\\Databases\\UserAccounts.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM StudentDetails WHERE Username = '" + txtUsername.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                clearTextBoxes();
                MessageBox.Show("Record Successfully Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch {
                MessageBox.Show("Record Not Deleted. An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}