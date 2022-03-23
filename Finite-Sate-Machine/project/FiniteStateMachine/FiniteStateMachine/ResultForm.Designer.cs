namespace FiniteStateMachine
{
    partial class ResultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.table1 = new System.Windows.Forms.DataGridView();
            this.table2 = new System.Windows.Forms.DataGridView();
            this.OkBtn = new System.Windows.Forms.Button();
            this.Q_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Q_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Q_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.q1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.q2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.q3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.table2)).BeginInit();
            this.SuspendLayout();
            // 
            // table1
            // 
            this.table1.AllowUserToAddRows = false;
            this.table1.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.table1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.S0,
            this.S1,
            this.S2,
            this.S3,
            this.S4});
            this.table1.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.table1.Location = new System.Drawing.Point(12, 12);
            this.table1.Name = "table1";
            this.table1.RowHeadersVisible = false;
            this.table1.RowTemplate.Height = 24;
            this.table1.Size = new System.Drawing.Size(231, 268);
            this.table1.TabIndex = 0;
            // 
            // table2
            // 
            this.table2.AllowUserToAddRows = false;
            this.table2.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.table2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Q_1,
            this.Q_2,
            this.Q_3,
            this.q1,
            this.q2,
            this.q3});
            this.table2.Location = new System.Drawing.Point(249, 12);
            this.table2.Name = "table2";
            this.table2.RowHeadersVisible = false;
            this.table2.RowTemplate.Height = 24;
            this.table2.Size = new System.Drawing.Size(490, 268);
            this.table2.TabIndex = 1;
            // 
            // OkBtn
            // 
            this.OkBtn.BackColor = System.Drawing.Color.GreenYellow;
            this.OkBtn.Location = new System.Drawing.Point(282, 284);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(178, 23);
            this.OkBtn.TabIndex = 2;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = false;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // Q_1
            // 
            this.Q_1.FillWeight = 35F;
            this.Q_1.HeaderText = "Q1";
            this.Q_1.Name = "Q_1";
            this.Q_1.Width = 35;
            // 
            // Q_2
            // 
            this.Q_2.FillWeight = 35F;
            this.Q_2.HeaderText = "Q2";
            this.Q_2.Name = "Q_2";
            this.Q_2.Width = 35;
            // 
            // Q_3
            // 
            this.Q_3.FillWeight = 35F;
            this.Q_3.HeaderText = "Q3";
            this.Q_3.Name = "Q_3";
            this.Q_3.Width = 35;
            // 
            // q1
            // 
            this.q1.FillWeight = 35F;
            this.q1.HeaderText = "q1";
            this.q1.Name = "q1";
            this.q1.Width = 35;
            // 
            // q2
            // 
            this.q2.FillWeight = 35F;
            this.q2.HeaderText = "q2";
            this.q2.Name = "q2";
            this.q2.Width = 35;
            // 
            // q3
            // 
            this.q3.FillWeight = 35F;
            this.q3.HeaderText = "q3";
            this.q3.Name = "q3";
            this.q3.Width = 35;
            // 
            // number
            // 
            this.number.HeaderText = "";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Width = 35;
            // 
            // S0
            // 
            this.S0.HeaderText = "S0";
            this.S0.Name = "S0";
            this.S0.ReadOnly = true;
            this.S0.Width = 38;
            // 
            // S1
            // 
            this.S1.HeaderText = "S1";
            this.S1.Name = "S1";
            this.S1.ReadOnly = true;
            this.S1.Width = 38;
            // 
            // S2
            // 
            this.S2.HeaderText = "S2";
            this.S2.Name = "S2";
            this.S2.ReadOnly = true;
            this.S2.Width = 38;
            // 
            // S3
            // 
            this.S3.HeaderText = "S3";
            this.S3.Name = "S3";
            this.S3.ReadOnly = true;
            this.S3.Width = 38;
            // 
            // S4
            // 
            this.S4.HeaderText = "S4";
            this.S4.Name = "S4";
            this.S4.ReadOnly = true;
            this.S4.Width = 38;
            // 
            // ResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(750, 318);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.table2);
            this.Controls.Add(this.table1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ResultForm";
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.table2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView table1;
        private System.Windows.Forms.DataGridView table2;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Q_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Q_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Q_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn q1;
        private System.Windows.Forms.DataGridViewTextBoxColumn q2;
        private System.Windows.Forms.DataGridViewTextBoxColumn q3;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn S0;
        private System.Windows.Forms.DataGridViewTextBoxColumn S1;
        private System.Windows.Forms.DataGridViewTextBoxColumn S2;
        private System.Windows.Forms.DataGridViewTextBoxColumn S3;
        private System.Windows.Forms.DataGridViewTextBoxColumn S4;
    }
}