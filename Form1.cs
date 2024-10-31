﻿using System;
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
            var  num = Convert.ToInt32(textBox1.Text);
            MessageBox.Show(num.ToString());
        }

        private async void button1_Click(object sender, EventArgs e)
        {
       
            string updateUrl = "https://github.com/ENG-CJ/Job-Finder-App/releases";

            try
            {
                using (var manager = new UpdateManager(updateUrl))
                {
                    // Check for updates
                    var release = await manager.UpdateApp();

                    if (release != null)
                    {
                        MessageBox.Show("Update applied successfully! Restart the application to complete the update.", "Update Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
    
}
