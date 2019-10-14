namespace DatabaseFrontEnd
{
    partial class Form1
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
            this.tb_output = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_connection_state = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_output
            // 
            this.tb_output.Location = new System.Drawing.Point(17, 52);
            this.tb_output.Multiline = true;
            this.tb_output.Name = "tb_output";
            this.tb_output.Size = new System.Drawing.Size(587, 331);
            this.tb_output.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(514, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Get All Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_connection_state
            // 
            this.lbl_connection_state.BackColor = System.Drawing.Color.Red;
            this.lbl_connection_state.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_connection_state.Location = new System.Drawing.Point(13, 13);
            this.lbl_connection_state.Name = "lbl_connection_state";
            this.lbl_connection_state.Size = new System.Drawing.Size(154, 36);
            this.lbl_connection_state.TabIndex = 2;
            this.lbl_connection_state.Text = "Connection State";
            this.lbl_connection_state.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 434);
            this.Controls.Add(this.lbl_connection_state);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_output);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_output;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_connection_state;
    }
}

