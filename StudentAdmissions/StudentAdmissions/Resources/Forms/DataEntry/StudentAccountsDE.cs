using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentAdmissions.Resources.Forms.DataEntry {
    public partial class StudentAccountsDE : Form {
        public StudentAccountsDE(string restrictedUser) {
            InitializeComponent();
            lblRUsername.Text = restrictedUser;
            txtUsername.Enabled = false;
            txtUsername.Text = generateID();
            cmboGrade.DropDownStyle = ComboBoxStyle.DropDownList;
            lblRUsername.Visible = false;
            chlbModules.Enabled = false;
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
            string id = "P" + dateEnumerate.ToString() + new string(Enumerable.Repeat("0123456789", 6).Select(s => s[random.Next(s.Length)]).ToArray()) + fl + sl;
            return id;
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

        private void txtFirstName_TextChanged(object sender, EventArgs e) {
            fl = txtFirstName.Text.Substring(0, 1);
        }

        private void txtSurname_TextChanged(object sender, EventArgs e) {
            sl = txtSurname.Text.Substring(0, 1);
        }
    }
}