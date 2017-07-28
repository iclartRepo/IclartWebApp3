namespace MigrationTool
{
    partial class Migration
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
            this.migrate = new System.Windows.Forms.Button();
            this.settings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // migrate
            // 
            this.migrate.Location = new System.Drawing.Point(135, 12);
            this.migrate.Name = "migrate";
            this.migrate.Size = new System.Drawing.Size(117, 39);
            this.migrate.TabIndex = 0;
            this.migrate.Text = "Start Migration";
            this.migrate.UseVisualStyleBackColor = true;
            this.migrate.Click += new System.EventHandler(this.migrate_Click);
            // 
            // settings
            // 
            this.settings.Location = new System.Drawing.Point(12, 12);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(117, 39);
            this.settings.TabIndex = 1;
            this.settings.Text = "DB Settings";
            this.settings.UseVisualStyleBackColor = true;
            this.settings.Click += new System.EventHandler(this.settings_Click);
            // 
            // Migration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 226);
            this.Controls.Add(this.settings);
            this.Controls.Add(this.migrate);
            this.Name = "Migration";
            this.Text = "Migration Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button migrate;
        private System.Windows.Forms.Button settings;
    }
}

