using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsMatchingGame.Controls;
using WinFormsMatchingGame.Properties;

// DataGridViewButtonCell doesn't seem to support an image on the button.
// (Painting the image in CellPainting doesn't seem a very clean way to go.)
// So use DataGridViewButtonCell, and explicitly check for a Space key press.
// Then again, DataGridViewImageCell doesn't support a click, and didn't seem 
// to support being clicked via Windows Speech Recognition, so stick with a
// DataGridViewButtonCell.

// https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_Name
// https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_HelpText
// https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_ValueValue
// https://www.linkedin.com/pulse/common-approaches-enhancing-programmatic-your-win32-winforms-barker/
// https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.queryaccessibilityhelp?redirectedfrom=MSDN&view=windowsdesktop-5.0

// Don't bother setting the DataGridViewButtonCellWithCustomName's Description property.
// That doesn't get exposed as UIA clients would expect. (Rather it gets exposed in a
// way to support clients of a legacy Windows accessibility API.)

// Leave the default grid behavior of an Enter press moving to the cell below the focused cell.

namespace WinFormsMatchingGame
{
    public partial class FormMatchingGame : Form
    {
        private CardMatchingGrid cardMatchingGrid;
        private DataGridViewButtonCellWithCustomName firstCardInPairAttempt;
        private DataGridViewButtonCellWithCustomName secondCardInPairAttempt;

        private int tryAgainCount = 0;

        private Shuffler shuffler;

        public FormMatchingGame()
        {
            InitializeComponent();

            CreateCardMatchingGrid();
        }

        private void CreateCardMatchingGrid()
        {
            cardMatchingGrid = new CardMatchingGrid();

            cardMatchingGrid.AccessibilityObject.Name = Resources.ResourceManager.GetString("CardsForMatching");

            cardMatchingGrid.RowHeadersVisible = false;
            cardMatchingGrid.ColumnHeadersVisible = false;
            cardMatchingGrid.Dock = DockStyle.Fill;
            cardMatchingGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            cardMatchingGrid.AllowUserToAddRows = false;
            cardMatchingGrid.ShowCellToolTips = false;
            cardMatchingGrid.StandardTab = true;

            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());

            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();

            cardMatchingGrid.CellClick += DataGridView_CellClick;
            cardMatchingGrid.CellPainting += CardMatchingGrid_CellPainting;
            cardMatchingGrid.SizeChanged += Grid_SizeChanged;

            SetupCardList();

            this.panelCardGrid.Controls.Add(cardMatchingGrid);

            ResizeGridContent();
        }

        private void SetupCardList()
        {
            // This app assumes the count of cards is square number. It's currently 16.
            cardMatchingGrid.CardList = new List<Card>()
            {
                new Card {
                    Name = "Daleks in Blackpool",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card {
                    Name = "Daleks in Blackpool",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card {
                    Name = "Lower Lighthouse in Fleetwood",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card {
                    Name = "Lower Lighthouse in Fleetwood",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card {
                    Name = "Fish and Chips in Cleveleys",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card {
                    Name = "Fish and Chips in Cleveleys",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card {
                    Name = "Cockersand Abbey",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card {
                    Name = "Cockersand Abbey",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card {
                    Name = "North Pier in Blackpool",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card {
                    Name = "North Pier in Blackpool",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card {
                    Name = "Bolton Abbey",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card {
                    Name = "Bolton Abbey",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card {
                    Name = "Harrogate Obelisk",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card {
                    Name = "Harrogate Obelisk",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card {
                    Name = "Hampsfell Hospice",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
                new Card {
                    Name = "Hampsfell Hospice",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
            };

            shuffler = new Shuffler();
            shuffler.Shuffle(cardMatchingGrid.CardList);
        }

        private void buttonTryAgain_Click(object sender, EventArgs e)
        {
            TryAgain();
        }

        private void buttonRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TryAgain()
        {
            ++tryAgainCount;

            buttonTryAgain.Enabled = false;

            firstCardInPairAttempt.TurnOver(false);
            secondCardInPairAttempt.TurnOver(false);

            firstCardInPairAttempt = null;
            secondCardInPairAttempt = null;

            // In the interests of game efficiency, move focus back into the grid now.
            cardMatchingGrid.Focus();
        }

        private void RestartGame()
        {
            tryAgainCount = 0;

            shuffler.Shuffle(cardMatchingGrid.CardList);

            for (int i = 0; i < this.cardMatchingGrid.CardList.Count; i++)
            {
                this.cardMatchingGrid.CardList[i].FaceUp = false;
                this.cardMatchingGrid.CardList[i].Matched = false;
            }

            firstCardInPairAttempt = null;
            secondCardInPairAttempt = null;

            var gridDimensions = cardMatchingGrid.GridDimensions;

            for (int r = 0; r < gridDimensions; r++)
            {
                for (int c = 0; c < gridDimensions; c++)
                {
                    var button = (this.cardMatchingGrid.Rows[r].Cells[c] as DataGridViewButtonCellWithCustomName);
                    button.TurnOver(false);
                }
            }

            buttonTryAgain.Enabled = false;

            cardMatchingGrid.Focus();
        }

        private void Grid_SizeChanged(object sender, EventArgs e)
        {
            ResizeGridContent();
        }

        private void ResizeGridContent()
        {
            var gridDimensions = cardMatchingGrid.GridDimensions;

            for (int i = 0; i < gridDimensions; ++i)
            {
                cardMatchingGrid.Columns[i].Width = (this.panelCardGrid.ClientSize.Width / gridDimensions) - 1;
                cardMatchingGrid.Rows[i].Height = (this.panelCardGrid.ClientSize.Height / gridDimensions) - 1;
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClickCell(e.RowIndex, e.ColumnIndex);
        }

        private void ClickCell(int rowIndex, int columnIndex)
        {
            // Take no action while the Try Again button is enabled.
            if (buttonTryAgain.Enabled)
            {
                return;
            }

            // Take no action if the click is on a cell that's already face-up.
            var card = cardMatchingGrid.GetCardFromRowColumn(rowIndex, columnIndex);
            if (card.FaceUp)
            {
                return;
            }

            var gridDimensions = cardMatchingGrid.GridDimensions;
            var index = (gridDimensions * rowIndex) + columnIndex;

            // Is this the first card turned over in an attempt to find a pair?
            if (firstCardInPairAttempt == null)
            {
                firstCardInPairAttempt = (this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName);
                firstCardInPairAttempt.TurnOver(true);
            }
            else
            {
                // This must be the second card turned over in an attempt to find a pair.
                // Has a pair been found?

                var firstIndex = firstCardInPairAttempt.GetCardIndex();
                var cardNameFirst = this.cardMatchingGrid.CardList[firstIndex].Name;

                var cardNameSecond = this.cardMatchingGrid.CardList[index].Name;

                secondCardInPairAttempt = (this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName);

                if (cardNameFirst == cardNameSecond)
                {
                    var button = (this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName);
                    button.TurnOver(true);

                    this.cardMatchingGrid.CardList[index].Matched = true;

                    this.cardMatchingGrid.CardList[index].Matched = true;
                    this.cardMatchingGrid.CardList[firstIndex].Matched = true;

                    firstCardInPairAttempt = null;
                    secondCardInPairAttempt = null;

                    // Has the game been won?
                    if (GameIsWon())
                    {
                        // Todo: Localized this.
                        var answer = MessageBox.Show(
                            this,
                            "Congratulations! You won the game in " +
                            (tryAgainCount +
                                ((cardMatchingGrid.RowCount * cardMatchingGrid.ColumnCount) / 2)) +
                            " goes.\r\n\r\nWould you like another game?",
                            "Accessible Matching Game",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (answer == DialogResult.Yes)
                        {
                            RestartGame();
                        }
                    }

                }
                else
                {
                    secondCardInPairAttempt = (this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName);
                    secondCardInPairAttempt.TurnOver(true);

                    buttonTryAgain.Enabled = true;
                }
            }
        }

        private bool GameIsWon()
        {
            for (int i = 0; i < this.cardMatchingGrid.CardList.Count; i++)
            {
                if (!this.cardMatchingGrid.CardList[i].Matched)
                {
                    return false;
                }
            }

            return true;
        }

        private void CardMatchingGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Set up clip regions below in order to prevent the image in the cell from
            // flashing when the cell is repainted.

            Bitmap bmp = null;
            Rectangle rectBitmap = new Rectangle();
            Rectangle rectCellImage = new Rectangle();

            // Is this cell showing an image?
            var card = cardMatchingGrid.GetCardFromRowColumn(e.RowIndex, e.ColumnIndex);
            if (card.FaceUp)
            {
                var columnCount = cardMatchingGrid.GridDimensions;
                var index = ((columnCount * e.RowIndex) + e.ColumnIndex);
                bmp = cardMatchingGrid.CardList[index].Image;

                rectBitmap = new Rectangle(0, 0, bmp.Width, bmp.Height);

                double ratio = Math.Max(
                    (double)rectBitmap.Width / (double)e.CellBounds.Width,
                    (double)rectBitmap.Height / (double)e.CellBounds.Height);

                var finalWidth = (int)(rectBitmap.Width / ratio) - 4;
                var finalHeight = (int)(rectBitmap.Height / ratio) - 4;

                rectCellImage = new Rectangle(
                    e.CellBounds.Left + ((e.CellBounds.Width - finalWidth) / 2),
                    e.CellBounds.Top + ((e.CellBounds.Height - finalHeight) / 2),
                    finalWidth,
                    finalHeight);

                e.Graphics.ExcludeClip(new Region(
                    rectCellImage));
            }

            // Now paint everything but the cell image if there is one.
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            if (card.FaceUp)
            {
                e.Graphics.Clip = new Region();

                // Now paint the image in the cell.
                e.Graphics.DrawImage(bmp,
                    rectCellImage,
                    rectBitmap,
                    GraphicsUnit.Pixel);
            }

            e.Handled = true;
        }
    }
}
