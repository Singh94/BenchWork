using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentAdmissions.Resources.Forms.TableView {
    public partial class StudentAccountsTV : Form {
        public StudentAccountsTV(string restrictedUser) {
            InitializeComponent();
            lblRUsername.Visible = false;
            lblRUsername.Text = restrictedUser;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Selector selector = new Selector(lblRUsername.Text);
            selector.Show();
            this.Close();
        }
    }
}
