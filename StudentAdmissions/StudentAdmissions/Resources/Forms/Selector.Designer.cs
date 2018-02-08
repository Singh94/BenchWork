namespace StudentAdmissions.Resources.Forms {
    partial class Selector {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lblRUsername = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnTableView = new System.Windows.Forms.Button();
            this.btnDataEntry = new System.Windows.Forms.Button();
            this.cmboSelection = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblRUsername
            // 
            this.lblRUsername.AutoSize = true;
            this.lblRUsername.Location = new System.Drawing.Point(13, 43);
            this.lblRUsername.Name = "lblRUsername";
            this.lblRUsername.Size = new System.Drawing.Size(68, 13);
            this.lblRUsername.TabIndex = 9;
            this.lblRUsername.Text = "USERNAME";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(13, 59);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(259, 103);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnTableView
            // 
            this.btnTableView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnTableView.Location = new System.Drawing.Point(151, 58);
            this.btnTableView.Name = "btnTableView";
            this.btnTableView.Size = new System.Drawing.Size(120, 103);
            this.btnTableView.TabIndex = 7;
            this.btnTableView.Text = "Table View";
            this.btnTableView.UseVisualStyleBackColor = true;
            this.btnTableView.Click += new System.EventHandler(this.btnTableView_Click);
            // 
            // btnDataEntry
            // 
            this.btnDataEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataEntry.Location = new System.Drawing.Point(12, 58);
            this.btnDataEntry.Name = "btnDataEntry";
            this.btnDataEntry.Size = new System.Drawing.Size(120, 103);
            this.btnDataEntry.TabIndex = 6;
            this.btnDataEntry.Text = "Data Entry";
            this.btnDataEntry.UseVisualStyleBackColor = true;
            this.btnDataEntry.Click += new System.EventHandler(this.btnDataEntry_Click);
            // 
            // cmboSelection
            // 
            this.cmboSelection.FormattingEnabled = true;
            this.cmboSelection.Items.AddRange(new object[] {
            "Manage User Accounts",
            "Manage Student Accounts",
            "Exit"});
            this.cmboSelection.Location = new System.Drawing.Point(13, 15);
            this.cmboSelection.Name = "cmboSelection";
            this.cmboSelection.Size = new System.Drawing.Size(259, 21);
            this.cmboSelection.TabIndex = 5;
            this.cmboSelection.SelectedIndexChanged += new System.EventHandler(this.cmboSelection_SelectedIndexChanged);
            // 
            // Selector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 176);
            this.Controls.Add(this.lblRUsername);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnTableView);
            this.Controls.Add(this.btnDataEntry);
            this.Controls.Add(this.cmboSelection);
            this.Name = "Selector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRUsername;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnTableView;
        private System.Windows.Forms.Button btnDataEntry;
        private System.Windows.Forms.ComboBox cmboSelection;
    }
}