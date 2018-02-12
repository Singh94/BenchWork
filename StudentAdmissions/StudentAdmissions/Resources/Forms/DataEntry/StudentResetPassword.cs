using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentAdmissions.Resources.Forms.DataEntry {
    public partial class StudentResetPassword : Form {
        public StudentResetPassword() {
            InitializeComponent();
        }

        private static string encryptPassword(string value) {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(value));
            return Convert.ToBase64String(hashedDataBytes);
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e) {
            try {
                string userPassword = encryptPassword(txtPassword.Text);

                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\" + Environment.UserName + "\\Desktop\\Bench\\StudentAdmissions\\StudentAdmissions\\Resources\\Databases\\UserAccounts.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE StudentDetails SET Password = '" + userPassword + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                txtPassword.Clear();

                if (MessageBox.Show("Password Successfully Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) this.Close();

            }
            catch {
                MessageBox.Show("Password Update Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
