
namespace WinformsPlayField
{
    partial class AsyncEx
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
            this.btnAsync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAsync
            // 
            this.btnAsync.Location = new System.Drawing.Point(246, 175);
            this.btnAsync.Name = "btnAsync";
            this.btnAsync.Size = new System.Drawing.Size(121, 36);
            this.btnAsync.TabIndex = 0;
            this.btnAsync.Text = "Click Here";
            this.btnAsync.UseVisualStyleBackColor = true;
            this.btnAsync.Click += new System.EventHandler(this.btnAsync_Click);
            // 
            // AsyncEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAsync);
            this.Name = "AsyncEx";
            this.Text = "Async Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAsync;
    }
}

