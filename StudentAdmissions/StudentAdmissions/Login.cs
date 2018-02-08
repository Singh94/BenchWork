using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using StudentAdmissions.Resources.Forms;

namespace StudentAdmissions {
    public partial class Login : Form {
        public Login() {
            InitializeComponent();
        }

        private static string encryptPassword(string value) {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(value));
            return Convert.ToBase64String(hashedDataBytes);
        }

        private void btnLogin_Click(object sender, EventArgs e) {
            string userPassword = encryptPassword(txtPassword.Text + "MySignature" + txtUsername.Text);

            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\Bench\\StudentAdmissions\\StudentAdmissions\\Resources\\Databases\\UserAccounts.mdf;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM LoginDetails WHERE Username = '" + txtUsername.Text + "' AND Password = '" + userPassword + "'", con);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read()) {
                Selector selector = new Selector(txtUsername.Text);
                selector.Show();
                this.Hide();
            }
            else {
                MessageBox.Show("Username and/or Password is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }
            con.Close();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }
    }
}
