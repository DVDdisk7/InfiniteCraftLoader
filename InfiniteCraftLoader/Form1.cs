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

        private string savedData = "";

        private bool isSaved()
        {
            // check if the local storage is the same as the savedData
            return savedData == webView.EvalScript("localStorage.getItem('infinite-craft-data');").ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize the web browser
            webView.Create(webControl.Handle);
            webView.Url = "https://neal.fun/infinite-craft/";
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            // check if the game is saved
            if (!isSaved())
            {
                DialogResult result = MessageBox.Show("Wil je het oude spel opslaan?", "Nieuw", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    saveToolStripButton_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            // Set savedData to an empty string
            savedData = "";

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

                // Set savedData to the json data
                savedData = json;

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

                // Set savedData to the json data
                savedData = json;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If the game is not saved, first ask for saving
            if (!isSaved())
            {
                DialogResult result = MessageBox.Show("Wil je het spel opslaan?", "Afsluiten", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    saveToolStripButton_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
