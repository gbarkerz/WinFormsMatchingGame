using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinFormsMatchingGame.Controls;
using WinFormsMatchingGame.Properties;

// In this update:
// Add buttons making it clearer how to play the game with the keyboard.
// Add a message when attempt to turn up a card when 2 unmatched cards are already up.
//  This is more helpful than nothing happening in that situation.
// Move the Restart action to a menu with F5 shortcut.
//
// Notes:
// - When fixing up the UIA order, do remember to remove old TabIndex settings, 
//    otherwise tab order might still be broken.
//
// Todo:
// - Make DPI aware.
// - Add app icon.
// - Update store icons.

// For details on this app, please read the ReadMe file included in the app's VS solution.
namespace WinFormsMatchingGame
{
    public partial class FormMatchingGame : Form
    {
        private CardMatchingGrid cardMatchingGrid;

        public FormMatchingGame()
        {
            InitializeComponent();

            // Check that the current settings for the game are still valid.
            // For example, verify that all player-supplied pictures previously
            // selected are still available to use in the game.
            if (Settings.Default.UseYourPictures)
            {
                if (!IsPicturePathValid(Settings.Default.YourPicturesPath))
                {
                    // Ths settings are not valid, so show the Settings window now.
                    var gameSettings = new GameSettings(this);
                    if (gameSettings.ShowDialog(this) == DialogResult.Cancel)
                    {
                        // The Settings window was cancelled, so the settings are
                        // still invalid. As such, use the default pictures until
                        // the setting are fixed by the player.
                        Settings.Default.UseYourPictures = false;
                    }
                }
            }

            CreateCardMatchingGrid();
        }

        private void CreateCardMatchingGrid()
        {
            // Create the grid to host the cards shown in the game.
            cardMatchingGrid = new CardMatchingGrid();

            // Set up the grid in a way that most closely matches the needs of the game.
            cardMatchingGrid.RowHeadersVisible = false;
            cardMatchingGrid.ColumnHeadersVisible = false;
            cardMatchingGrid.AllowUserToResizeRows = false;
            cardMatchingGrid.AllowUserToResizeColumns = false;
            cardMatchingGrid.AllowUserToAddRows = false;
            cardMatchingGrid.Dock = DockStyle.Fill;
            cardMatchingGrid.ShowCellToolTips = false;

            // Make sure the grid itself has an accessible name.
            cardMatchingGrid.AccessibilityObject.Name = Resources.ResourceManager.GetString("CardsForMatching");

            // Don't have Tab presses move keyboard focus between cells in the grid.
            cardMatchingGrid.StandardTab = true;

            // The game currently only shows a 4x4 grid of cards.

            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnMatchingGame());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnMatchingGame());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnMatchingGame());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnMatchingGame());

            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();

            // Set up the cards to be shown.
            SetupCardList();

            this.panelCardGrid.Controls.Add(cardMatchingGrid);
        }

        private void SetupCardList()
        {
            bool cardsAreSetUp = false;

            if (Settings.Default.UseYourPictures)
            {
                cardsAreSetUp = SetupYourPicturesCardList();
            }
            
            if (!cardsAreSetUp)
            {
                SetupDefaultCardList();
            }

            cardMatchingGrid.Shuffle();
        }

        private bool SetupYourPicturesCardList()
        {
            bool cardsAreSetUp = true;

            cardMatchingGrid.CardList = new List<Card>();

            // For now, the game only handles 8 pairs of pictures.
            for (int i = 0; i < 8; i++)
            {
                var settingNamePath = "Card" + (i + 1) + "Path";

                if (String.IsNullOrWhiteSpace(Settings.Default[settingNamePath].ToString()))
                {
                    cardsAreSetUp = false;

                    break;
                }

                var settingNameName = "Card" + (i + 1) + "Name";
                var settingNameDescription = "Card" + (i + 1) + "Description";

                var name = Settings.Default[settingNameName].ToString();
                var desc = Settings.Default[settingNameDescription] == null ?
                            "" : Settings.Default[settingNameDescription].ToString();

                Bitmap image;

                try
                {
                    image = new Bitmap(Settings.Default[settingNamePath].ToString());
                }
                catch
                {
                    cardsAreSetUp = false;

                    break;
                }

                cardMatchingGrid.CardList.Add(
                    new Card
                    {
                        Name = name,
                        Description = desc,
                        Image = image
                    });

                cardMatchingGrid.CardList.Add(
                    new Card
                    {
                        Name = name,
                        Description = desc,
                        Image = image
                    });
            }

            return cardsAreSetUp;
        }

        private void SetupDefaultCardList()
        {
            var resManager = Resources.ResourceManager;

            // Note: This app assumes the total count of cards is 16.
            cardMatchingGrid.CardList = new List<Card>()
            {
                new Card {
                    Name = resManager.GetString("DefaultCard1Name"),
                    Description = resManager.GetString("DefaultCard1Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card {
                    Name = resManager.GetString("DefaultCard1Name"),
                    Description = resManager.GetString("DefaultCard1Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card {
                    Name = resManager.GetString("DefaultCard2Name"),
                    Description = resManager.GetString("DefaultCard2Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card {
                    Name = resManager.GetString("DefaultCard2Name"),
                    Description = resManager.GetString("DefaultCard2Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card {
                    Name = resManager.GetString("DefaultCard3Name"),
                    Description = resManager.GetString("DefaultCard3Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card {
                    Name = resManager.GetString("DefaultCard3Name"),
                    Description = resManager.GetString("DefaultCard3Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card {
                    Name = resManager.GetString("DefaultCard4Name"),
                    Description = resManager.GetString("DefaultCard4Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card {
                    Name = resManager.GetString("DefaultCard4Name"),
                    Description = resManager.GetString("DefaultCard4Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card {
                    Name = resManager.GetString("DefaultCard5Name"),
                    Description = resManager.GetString("DefaultCard5Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card {
                    Name = resManager.GetString("DefaultCard5Name"),
                    Description = resManager.GetString("DefaultCard5Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card {
                    Name = resManager.GetString("DefaultCard6Name"),
                    Description = resManager.GetString("DefaultCard6Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card {
                    Name = resManager.GetString("DefaultCard6Name"),
                    Description = resManager.GetString("DefaultCard6Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card {
                    Name = resManager.GetString("DefaultCard7Name"),
                    Description = resManager.GetString("DefaultCard7Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card {
                    Name = resManager.GetString("DefaultCard7Name"),
                    Description = resManager.GetString("DefaultCard7Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card {
                    Name = resManager.GetString("DefaultCard8Name"),
                    Description = resManager.GetString("DefaultCard8Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
                new Card {
                    Name = resManager.GetString("DefaultCard8Name"),
                    Description = resManager.GetString("DefaultCard8Description"),
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
            };
        }

        private void buttonTurnCardUp_Click(object sender, EventArgs e)
        {
            cardMatchingGrid.TurnCardUp();
        }

        private void buttonTryAgain_Click(object sender, EventArgs e)
        {
            TryAgain();
        }

        private void TryAgain()
        {
            cardMatchingGrid.TryAgain();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsDialog();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            // We might be showing a different set of cards now.
            SetupCardList();

            cardMatchingGrid.ResetGrid();
            cardMatchingGrid.Focus();
        }

        private void ShowSettingsDialog()
        {
            var gameSettings = new GameSettings(this);
            if (gameSettings.ShowDialog(this) != DialogResult.Cancel)
            {
                RestartGame();
            }
        }

        // For now, a folder is considered valid if it contains exactly 8 files
        // with extensions suggesting that the game can handle them.
        public bool IsPicturePathValid(string picturePath)
        {
            bool picturePathValid = true;

            System.IO.DirectoryInfo di = new DirectoryInfo(picturePath);

            try
            {
                string[] extensions = { ".jpg", ".png", ".bmp" };

                var files = di.EnumerateFiles("*", SearchOption.TopDirectoryOnly)
                                .Where(f => extensions.Contains(f.Extension.ToLower()))
                                .ToArray();

                if (files.Length != 8)
                {
                    picturePathValid = false;
                }
            }
            catch
            {
                picturePathValid = false;
            }

            if (!picturePathValid)
            {
                MessageBox.Show(
                    this,
                    "Please choose a folder that contains exactly 8 pictures.",
                    "Matching Game Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return picturePathValid;
        }
    }
}
