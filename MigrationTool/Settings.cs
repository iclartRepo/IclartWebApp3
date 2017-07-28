using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MigrationTool
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                string value = ConfigurationManager.AppSettings[key];
                if (key == "Server")
                {
                    txtServer.Text = value;
                }
                else if (key == "Username")
                {
                    txtUsername.Text = value;
                }
                else if (key == "Password")
                {
                    txtPassword.Text = value;
                }
                else if (key == "DBName")
                {
                    txtName.Text = value;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateSetting("Server", txtServer.Text);
            UpdateSetting("Username", txtUsername.Text);
            UpdateSetting("Password", txtPassword.Text);
            UpdateSetting("DBName", txtName.Text);
            this.Close();
        }
        private static void UpdateSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
