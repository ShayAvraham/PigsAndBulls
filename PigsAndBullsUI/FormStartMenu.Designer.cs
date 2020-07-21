namespace PigsAndBullsUI
{
    partial class FormStartMenu
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
            this.buttonNumOfChances = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonNumOfChances
            // 
            this.buttonNumOfChances.Location = new System.Drawing.Point(49, 27);
            this.buttonNumOfChances.Name = "buttonNumOfChances";
            this.buttonNumOfChances.Size = new System.Drawing.Size(261, 44);
            this.buttonNumOfChances.TabIndex = 0;
            this.buttonNumOfChances.Text = "Number of chances: 4";
            this.buttonNumOfChances.UseVisualStyleBackColor = true;
            this.buttonNumOfChances.Click += new System.EventHandler(this.buttonNumOfChances_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonStart.Location = new System.Drawing.Point(235, 93);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 27);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // FormStartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 154);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonNumOfChances);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormStartMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bool Pgia";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNumOfChances;
        private System.Windows.Forms.Button buttonStart;
    }
}