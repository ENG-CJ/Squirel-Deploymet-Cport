using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Squirrel;
namespace Library
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("You cant convert empty string to number","Title",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int num;
            if(!int.TryParse(textBox1.Text, out num))
            {
                MessageBox.Show("we cant convert this data", "Title", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            MessageBox.Show($"The num you eneted is "+num.ToString(), "Number displayer",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        private bool IsMajorUpdate(Version current, Version available)
        {
            // Check if there is a major version difference
            return available.Major > current.Major;
        }

        private bool IsPatchUpdate(Version current, Version available)
        {
            // Check if there is a patch-level update (same major, same minor, different patch)
            return available.Major == current.Major && available.Minor == current.Minor && available.Build > current.Build;
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            string updateUrl = "https://github.com/ENG-CJ/Squirel-Deploymet-Cport/releases/latest/download";
            Version currentVersion = new Version(Application.ProductVersion);

            try
            {
                using (var manager = new UpdateManager(updateUrl))
                {
                    var updateInfo = await manager.CheckForUpdate();
                    if (updateInfo.ReleasesToApply.Count > 0)
                    {
                        // Get the latest available version
                        Version availableVersion = updateInfo.FutureReleaseEntry.Version.Version;

                        // Check if it's a major update
                        if (IsMajorUpdate(currentVersion, availableVersion))
                        {
                            await manager.UpdateApp();
                            MessageBox.Show("Major update applied successfully! Restart the application to complete the update.", "Update Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No major updates available.", "Up to Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No updates available.", "Up to Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update application: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string updateUrl = "https://github.com/ENG-CJ/Squirel-Deploymet-Cport/releases/latest/download";
            Version currentVersion = new Version(Application.ProductVersion);

            try
            {
                using (var manager = new UpdateManager(updateUrl))
                {
                    var updateInfo = await manager.CheckForUpdate();
                    if (updateInfo.ReleasesToApply.Count > 0)
                    {
                        // Get the latest available version
                        Version availableVersion = updateInfo.FutureReleaseEntry.Version.Version;

                        // Check if it's a patch update
                        if (IsPatchUpdate(currentVersion, availableVersion))
                        {
                            await manager.UpdateApp();
                            MessageBox.Show("Bug fix applied successfully! Restart the application to complete the fix.", "Bug Fix Pulled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No new bug fixes available.", "Up to Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No bug fixes available.", "Up to Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to pull bug fix: {ex.Message}", "Bug Fix Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}
