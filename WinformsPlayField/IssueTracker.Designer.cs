
namespace WinformsPlayField
{
    partial class IssueTracker
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
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.ibLog = new System.Windows.Forms.ListBox();
            this.dgvAllIssues = new System.Windows.Forms.DataGridView();
            this.IssueID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssueTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssueDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssuePriority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssueStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbAddEdit = new System.Windows.Forms.GroupBox();
            this.btnResolve = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbLogDetail = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblAllIssues = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllIssues)).BeginInit();
            this.gbAddEdit.SuspendLayout();
            this.gbLogDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(143, 33);
            this.txtID.Multiline = true;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(322, 22);
            this.txtID.TabIndex = 0;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(75, 36);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(62, 17);
            this.lblID.TabIndex = 1;
            this.lblID.Text = "Issue ID:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(247, 341);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ibLog
            // 
            this.ibLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ibLog.FormattingEnabled = true;
            this.ibLog.ItemHeight = 16;
            this.ibLog.Location = new System.Drawing.Point(6, 21);
            this.ibLog.Name = "ibLog";
            this.ibLog.Size = new System.Drawing.Size(383, 356);
            this.ibLog.TabIndex = 3;
            // 
            // dgvAllIssues
            // 
            this.dgvAllIssues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllIssues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IssueID,
            this.IssueTitle,
            this.IssueDesc,
            this.IssuePriority,
            this.IssueStatus});
            this.dgvAllIssues.Location = new System.Drawing.Point(12, 435);
            this.dgvAllIssues.Name = "dgvAllIssues";
            this.dgvAllIssues.RowHeadersWidth = 51;
            this.dgvAllIssues.RowTemplate.Height = 24;
            this.dgvAllIssues.Size = new System.Drawing.Size(903, 336);
            this.dgvAllIssues.TabIndex = 4;
            this.dgvAllIssues.SelectionChanged += new System.EventHandler(this.dgvAllIssues_SelectionChanged);
            // 
            // IssueID
            // 
            this.IssueID.DataPropertyName = "IssueID";
            this.IssueID.HeaderText = "Issue ID";
            this.IssueID.MinimumWidth = 6;
            this.IssueID.Name = "IssueID";
            this.IssueID.Width = 125;
            // 
            // IssueTitle
            // 
            this.IssueTitle.DataPropertyName = "IssueTitle";
            this.IssueTitle.HeaderText = "Title";
            this.IssueTitle.MinimumWidth = 6;
            this.IssueTitle.Name = "IssueTitle";
            this.IssueTitle.Width = 125;
            // 
            // IssueDesc
            // 
            this.IssueDesc.DataPropertyName = "IssueDesc";
            this.IssueDesc.HeaderText = "Description";
            this.IssueDesc.MinimumWidth = 6;
            this.IssueDesc.Name = "IssueDesc";
            this.IssueDesc.Width = 125;
            // 
            // IssuePriority
            // 
            this.IssuePriority.DataPropertyName = "IssuePriority";
            this.IssuePriority.HeaderText = "Priority";
            this.IssuePriority.MinimumWidth = 6;
            this.IssuePriority.Name = "IssuePriority";
            this.IssuePriority.Width = 125;
            // 
            // IssueStatus
            // 
            this.IssueStatus.DataPropertyName = "IssueStatus";
            this.IssueStatus.HeaderText = "Status";
            this.IssueStatus.MinimumWidth = 6;
            this.IssueStatus.Name = "IssueStatus";
            this.IssueStatus.Width = 125;
            // 
            // gbAddEdit
            // 
            this.gbAddEdit.Controls.Add(this.btnResolve);
            this.gbAddEdit.Controls.Add(this.btnNew);
            this.gbAddEdit.Controls.Add(this.cmbType);
            this.gbAddEdit.Controls.Add(this.cmbStatus);
            this.gbAddEdit.Controls.Add(this.cmbPriority);
            this.gbAddEdit.Controls.Add(this.lblType);
            this.gbAddEdit.Controls.Add(this.lblStatus);
            this.gbAddEdit.Controls.Add(this.lblPriority);
            this.gbAddEdit.Controls.Add(this.txtDesc);
            this.gbAddEdit.Controls.Add(this.lblDesc);
            this.gbAddEdit.Controls.Add(this.txtTitle);
            this.gbAddEdit.Controls.Add(this.lblTitle);
            this.gbAddEdit.Controls.Add(this.btnSave);
            this.gbAddEdit.Controls.Add(this.txtID);
            this.gbAddEdit.Controls.Add(this.lblID);
            this.gbAddEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAddEdit.Location = new System.Drawing.Point(12, 12);
            this.gbAddEdit.Name = "gbAddEdit";
            this.gbAddEdit.Size = new System.Drawing.Size(486, 388);
            this.gbAddEdit.TabIndex = 5;
            this.gbAddEdit.TabStop = false;
            this.gbAddEdit.Text = "Add/Edit Issue";
            // 
            // btnResolve
            // 
            this.btnResolve.Location = new System.Drawing.Point(341, 341);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(94, 23);
            this.btnResolve.TabIndex = 14;
            this.btnResolve.Text = "Resolve";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(153, 341);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(88, 23);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(143, 300);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(158, 24);
            this.cmbType.TabIndex = 12;
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.SystemColors.Window;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(143, 267);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(158, 24);
            this.cmbStatus.TabIndex = 11;
            // 
            // cmbPriority
            // 
            this.cmbPriority.BackColor = System.Drawing.SystemColors.Window;
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Location = new System.Drawing.Point(143, 234);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(158, 24);
            this.cmbPriority.TabIndex = 10;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(93, 303);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(44, 17);
            this.lblType.TabIndex = 9;
            this.lblType.Text = "Type:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(85, 270);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 17);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status:";
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriority.Location = new System.Drawing.Point(81, 237);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(56, 17);
            this.lblPriority.TabIndex = 7;
            this.lblPriority.Text = "Priority:";
            // 
            // txtDesc
            // 
            this.txtDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.Location = new System.Drawing.Point(143, 114);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(322, 103);
            this.txtDesc.TabIndex = 6;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.Location = new System.Drawing.Point(17, 117);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(120, 17);
            this.lblDesc.TabIndex = 5;
            this.lblDesc.Text = "Issue Description:";
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(143, 74);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(322, 22);
            this.txtTitle.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(61, 77);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(76, 17);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Issue Title:";
            // 
            // gbLogDetail
            // 
            this.gbLogDetail.Controls.Add(this.ibLog);
            this.gbLogDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLogDetail.Location = new System.Drawing.Point(520, 12);
            this.gbLogDetail.Name = "gbLogDetail";
            this.gbLogDetail.Size = new System.Drawing.Size(395, 388);
            this.gbLogDetail.TabIndex = 6;
            this.gbLogDetail.TabStop = false;
            this.gbLogDetail.Text = "Log Details";
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(840, 406);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblAllIssues
            // 
            this.lblAllIssues.AutoSize = true;
            this.lblAllIssues.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllIssues.Location = new System.Drawing.Point(12, 412);
            this.lblAllIssues.Name = "lblAllIssues";
            this.lblAllIssues.Size = new System.Drawing.Size(82, 17);
            this.lblAllIssues.TabIndex = 8;
            this.lblAllIssues.Text = "All Issues:";
            // 
            // IssueTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 783);
            this.Controls.Add(this.lblAllIssues);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.gbLogDetail);
            this.Controls.Add(this.gbAddEdit);
            this.Controls.Add(this.dgvAllIssues);
            this.Name = "IssueTracker";
            this.Text = "IssueTracker";
            this.Load += new System.EventHandler(this.IssueTracker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllIssues)).EndInit();
            this.gbAddEdit.ResumeLayout(false);
            this.gbAddEdit.PerformLayout();
            this.gbLogDetail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox ibLog;
        private System.Windows.Forms.DataGridView dgvAllIssues;
        private System.Windows.Forms.GroupBox gbAddEdit;
        private System.Windows.Forms.GroupBox gbLogDetail;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblAllIssues;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssueID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssueTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssueDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssuePriority;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssueStatus;
    }
}