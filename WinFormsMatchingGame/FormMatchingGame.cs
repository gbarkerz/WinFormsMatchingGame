﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinFormsMatchingGame.Controls;
using WinFormsMatchingGame.Properties;

// For details on this app, please read the ReadMe file included in the app's VS solution.
namespace WinFormsMatchingGame
{
    public partial class FormMatchingGame : Form
    {
        private CardMatchingGrid cardMatchingGrid;

        public FormMatchingGame()
        {
            InitializeComponent();

            if (Settings.Default.UseYourPictures)
            {
                if (!IsPicturePathValid(Settings.Default.YourPicturesPath))
                {
                    var gameSettings = new GameSettings(this);
                    if (gameSettings.ShowDialog(this) == DialogResult.Cancel)
                    {
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
            if (Settings.Default.UseYourPictures)
            {
                SetupYourPicturesCardList();
            }
            else
            {
                SetupDefaultCardList();
            }

            cardMatchingGrid.Shuffle();
        }

        private void SetupYourPicturesCardList()
        {
            cardMatchingGrid.CardList = new List<Card>();

            for (int i = 0; i < 8; i++)
            {
                var settingNamePath = "Card" + (i + 1) + "Path";

                if (String.IsNullOrWhiteSpace(Settings.Default[settingNamePath].ToString()))
                {
                    break;
                }

                var settingNameName = "Card" + (i + 1) + "Name";
                var settingNameDescription = "Card" + (i + 1) + "Description";

                var name = Settings.Default[settingNameName].ToString();
                var desc = Settings.Default[settingNameDescription] == null ?
                            "" : Settings.Default[settingNameDescription].ToString();
                var image = new Bitmap(Settings.Default[settingNamePath].ToString());

                cardMatchingGrid.CardList.Add(
                    new Card
                    {
                        Name = name,
                        Description = desc,
                        Image = image
                    } );

                cardMatchingGrid.CardList.Add(
                    new Card
                    {
                        Name = name,
                        Description = desc,
                        Image = image
                    });
            }
        }

        private void SetupDefaultCardList()
        {
            // Note: This app assumes the count of cards is square number. (Currently the count is 16.)
            cardMatchingGrid.CardList = new List<Card>()
            {
                new Card {
                    Name = "Daleks in Blackpool",
                    Description = "A smiling man with a red coat and arms outstretched, standing in front of 3 large Daleks and a TARDIS. The Daleks seem to be raised above the grass beneath them, and a slightly cloudy sky is in the background.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card {
                    Name = "Daleks in Blackpool",
                    Description = "A smiling man with a red coat and arms outstretched, standing in front of 3 large Daleks and a TARDIS. The Daleks seem to be raised above the grass beneath them, and a slightly cloudy sky is in the background.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card {
                    Name = "Lower Lighthouse in Fleetwood",
                    Description = "A small brown stone lighthouse, with an upper small balcony and a lower bigger balcony. A covered sitting area is at the base of the lighthouse. In front of the lighthouse in the stone ground are anchor and compass symbols, and in the background is a grey cloudy sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card {
                    Name = "Lower Lighthouse in Fleetwood",
                    Description = "A small brown stone lighthouse, with an upper small balcony and a lower bigger balcony. A covered sitting area is at the base of the lighthouse. In front of the lighthouse in the stone ground are anchor and compass symbols, and in the background is a grey cloudy sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card {
                    Name = "Fish and Chips in Cleveleys",
                    Description = "A first-person view looking down on two portions of fish and chips in trays and paper. In the background is a concrete path, and two partially shown shoes.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card {
                    Name = "Fish and Chips in Cleveleys",
                    Description = "A first-person view looking down on two portions of fish and chips in trays and paper. In the background is a concrete path, and two partially shown shoes.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card {
                    Name = "Cockersand Abbey",
                    Description = "A small, very old hexagonal red stone building, with castellated battlements housing a cross. Arched windows on the sides of the building are bricked-up from the inside. The building sits on grass, with a bay and cloudy sky in the background.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card {
                    Name = "Cockersand Abbey",
                    Description = "A small, very old hexagonal red stone building, with castellated battlements housing a cross. Arched windows on the sides of the building are bricked-up from the inside. The building sits on grass, with a bay and cloudy sky in the background.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card {
                    Name = "Lytham St Annes Windmill",
                    Description = "A white windmill with a black roof and black sails. The windmill has stairs going up to a black door, and black windows. In the background is a street of houses and a clear, blue sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card {
                    Name = "Lytham St Annes Windmill",
                    Description = "A white windmill with a black roof and black sails. The windmill has stairs going up to a black door, and black windows. In the background is a street of houses and a clear, blue sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card {
                    Name = "Bolton Abbey",
                    Description = "The stone ruins of a large, very old abbey. The abbey has no roof or windows, and the sun shines through the arches from behind the abbey. In the foreground is a lawn housing multiple large stone graves, and in the background is a clear blue sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card {
                    Name = "Bolton Abbey",
                    Description = "The stone ruins of a large, very old abbey. The abbey has no roof or windows, and the sun shines through the arches from behind the abbey. In the foreground is a lawn housing multiple large stone graves, and in the background is a clear blue sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card {
                    Name = "Harrogate Obelisk",
                    Description = "A large stone obelisk in a town square, with buildings and trees nearby. The sun shines on the obelisk, with a slightly cloudy, blue sky in the background. At the base of the obelisk are six red wreaths.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card {
                    Name = "Harrogate Obelisk",
                    Description = "A large stone obelisk in a town square, with buildings and trees nearby. The sun shines on the obelisk, with a slightly cloudy, blue sky in the background. At the base of the obelisk are six red wreaths.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card {
                    Name = "Hampsfell Hospice",
                    Description = "A small square stone building with an opening on the near side. Above the opening is a Greek inscription. Railing surrounds the top of the building, and a sundial sits at the centre of the top. In the background is rocky grassland, with rolling hills in the far background along with a slightly cloudy, blue sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
                new Card {
                    Name = "Hampsfell Hospice",
                    Description = "A small square stone building with an opening on the near side. Above the opening is a Greek inscription. Railing surrounds the top of the building, and a sundial sits at the centre of the top. In the background is rocky grassland, with rolling hills in the far background along with a slightly cloudy, blue sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
            };
        }

        private void buttonTryAgain_Click(object sender, EventArgs e)
        {
            TryAgain();
        }

        private void TryAgain()
        {
            cardMatchingGrid.TryAgain();
        }

        private void buttonRestartGame_Click(object sender, EventArgs e)
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

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsDialog();
        }

        private void ShowSettingsDialog()
        {
            var gameSettings = new GameSettings(this);
            gameSettings.ShowDialog(this);

            // Todo: Don't restart the game unless appropriate after the dialog has closed.
            RestartGame();
        }

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
                // Todo: Localize.
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
