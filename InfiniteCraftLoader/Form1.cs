using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace InfiniteCraftLoader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize the web browser
            webView.Create(webControl.Handle);
            webView.Url = "https://neal.fun/infinite-craft/";
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            // remove "infinite-craft-data" in Local Storage
            webView.EvalScript("localStorage.removeItem('infinite-craft-data');");

            // refresh the page
            webView.Reload();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Read the json file
                string json = System.IO.File.ReadAllText(openFileDialog.FileName);

                // Set the json data to "infinite-craft-data" in Local Storage
                webView.EvalScript("localStorage.setItem('infinite-craft-data', '" + json + "');");

                // refresh the page
                webView.Reload();
            }   

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the json data from "infinite-craft-data" in Local Storage
                string json = webView.EvalScript("localStorage.getItem('infinite-craft-data');").ToString();

                // Write the json data to the file
                File.WriteAllText(saveFileDialog.FileName, json);
            }
        }
    }
}
