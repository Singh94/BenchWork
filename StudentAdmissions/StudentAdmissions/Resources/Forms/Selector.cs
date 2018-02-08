using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentAdmissions.Resources.Forms.DataEntry;
using StudentAdmissions.Resources.Forms.TableView;

namespace StudentAdmissions.Resources.Forms {
    public partial class Selector : Form {
        public Selector(string restrictedUser) {
            InitializeComponent();
            lblRUsername.Visible = false;
            lblRUsername.Text = restrictedUser;
            cmboSelection.SelectedIndex = 0;
            cmboSelection.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void cmboSelection_SelectedIndexChanged(object sender, EventArgs e) {
            if (cmboSelection.SelectedIndex == 2) {
                btnExit.Visible = true;
                btnDataEntry.Visible = false;
                btnTableView.Visible = false;
            }
            else {
                btnDataEntry.Visible = true;
                btnTableView.Visible = true;
                btnExit.Visible = false;
            }
        }

        private void btnDataEntry_Click(object sender, EventArgs e) {
            if (cmboSelection.SelectedIndex == 0) {
                UserAccountsDE userAccountsDE = new UserAccountsDE(lblRUsername.Text);
                userAccountsDE.Show();
                this.Hide();
            }
            if (cmboSelection.SelectedIndex == 1) {
                StudentAccountsDE studentAccountsDE = new StudentAccountsDE(lblRUsername.Text);
                studentAccountsDE.Show();
                this.Hide();
            }
        }

        private void btnTableView_Click(object sender, EventArgs e) {
            if (cmboSelection.SelectedIndex == 0) {
                UserAccountsTV userAccountsTV = new UserAccountsTV(lblRUsername.Text);
                userAccountsTV.Show();
                this.Hide();
            }
            if (cmboSelection.SelectedIndex == 1) {
                StudentAccountsTV studentAccountsTV = new StudentAccountsTV(lblRUsername.Text);
                studentAccountsTV.Show();
                this.Hide();
            }
        }

        private void btnExit_Click(object sender, EventArgs e) {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
