namespace AdyenDemoPos
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
            this.button1 = new System.Windows.Forms.Button();
            this.Query = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Receipt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 67);
            this.button1.TabIndex = 0;
            this.button1.Text = "Tender";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Tender_ClickAsync);
            // 
            // Query
            // 
            this.Query.Location = new System.Drawing.Point(138, 257);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(120, 67);
            this.Query.TabIndex = 1;
            this.Query.Text = "Query";
            this.Query.UseVisualStyleBackColor = true;
            this.Query.Click += new System.EventHandler(this.Query_ClickAsync);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(264, 257);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(120, 67);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_ClickAsync);
            // 
            // Receipt
            // 
            this.Receipt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Receipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Receipt.Location = new System.Drawing.Point(13, 13);
            this.Receipt.Name = "Receipt";
            this.Receipt.Size = new System.Drawing.Size(371, 231);
            this.Receipt.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 336);
            this.Controls.Add(this.Receipt);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Query);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Query;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label Receipt;
    }
}

