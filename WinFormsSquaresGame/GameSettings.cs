using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsSquaresGame
{
    public partial class GameSettings : Form
    {
        private FormSquaresGame formSquaresGame;

        public GameSettings(FormSquaresGame formSquaresGame)
        {
            this.formSquaresGame = formSquaresGame;

            InitializeComponent();
        }

        private void checkBoxShowNumbers_CheckedChanged(object sender, EventArgs e)
        {
            this.formSquaresGame.ShowNumbers = (sender as CheckBox).Checked;
        }

        private void checkBoxShowPicture_CheckedChanged(object sender, EventArgs e)
        {
            buttonBrowse.Enabled = (sender as CheckBox).Checked;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select background picture";
            openFileDialog.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                labelPicture.Text = fileInfo.Name;

                this.formSquaresGame.SetBackgroundPicture(fileInfo);
            }
        }
    }
}
