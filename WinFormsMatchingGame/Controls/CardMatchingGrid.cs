using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Automation;
using WinFormsMatchingGame.Properties;

namespace WinFormsMatchingGame.Controls
{
    public class CardMatchingGrid : DataGridView
    {
        private DataGridViewButtonCellWithMatchingGame firstCardInMatchAttempt;
        private DataGridViewButtonCellWithMatchingGame secondCardInMatchAttempt;
        private int tryAgainCount = 0;

        public List<Card> CardList { get; set; }

        private SoundPlayer soundPlayer = null;

        // Pick two sounds that seems unlikely to be occurring in the background during the typical
        // playing of the game. Assume that there are no copyright issues with playing a .wav file
        // in \Windows\Media, given that the following page shows sample code doing just that. 
        // https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-play-a-sound-from-a-windows-form?view=netframeworkdesktop-4.8
        private string soundPathMatch = @"C:\Windows\Media\Windows Unlock.wav";
        private string soundPathNotMatch = @"C:\Windows\Media\Windows Default.wav";

        public CardMatchingGrid()
        {
            CellClick += DataGridView_CellClick;
            SizeChanged += Grid_SizeChanged;
            CellPainting += CardMatchingGrid_CellPainting;

            try
            {
                soundPlayer = new SoundPlayer();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CardMatchingGrid: Failed to load SoundPlayer, " + ex.Message);
            }
        }

        public void Shuffle()
        {
            var shuffler = new Shuffler();
            shuffler.Shuffle(this.CardList);
        }

        private void Grid_SizeChanged(object sender, EventArgs e)
        {
            ResizeGridContent();
        }

        private void ResizeGridContent()
        {
            // Resize the columns and rows such that the collection of cells always
            // exactly fills the grid.

            for (int i = 0; i < GridDimensions; ++i)
            {
                Columns[i].Width = (this.ClientSize.Width / GridDimensions) - 1;
                Rows[i].Height = (this.ClientSize.Height / GridDimensions) - 1;
            }
        }

        public int GridDimensions
        {
            get
            {
                // Currently the app assumes the card count is a square number.
                return (int)Math.Sqrt(CardList.Count);
            }
        }

        public Card GetCardFromRowColumn(int rowIndex, int columnIndex)
        {
            var index = (this.GridDimensions * rowIndex) + columnIndex;
            return this.CardList[index];
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClickCell(e.RowIndex, e.ColumnIndex);
        }

        public void AnnounceAction(string announcement)
        {
            MethodInfo raiseMethod = typeof(AccessibleObject).GetMethod("RaiseAutomationNotification");
            if (raiseMethod != null)
            {
                raiseMethod.Invoke(
                    this.AccessibilityObject,
                    new object[3] {
                        AutomationNotificationKind.ActionCompleted,
                        AutomationNotificationProcessing.All,
                        announcement });
            }
        }

        // Todo: Is overriding the OnKeyDown really the most appropriate 
        // way of changing the behavior of Enter when focus is on the grid?
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Enter KeyDown results in any unmatched cards being turned back.
            if (e.KeyValue != 13)
            {
                base.OnKeyDown(e);
            }
            else
            {
                TryAgain();
            }
        }

        private void ClickCell(int rowIndex, int columnIndex)
        {
            // Take no action if we've already turned over two not-matching cards.
            if (secondCardInMatchAttempt != null)
            {
                MessageBox.Show(
                    this,
                    "Please turn the unmatched cards back over before turning more cards up.\r\n\r\n" + 
                        "To turn the unmatched cards back over, either press the Enter key or click the \"Turn unmatched cards back\" button.",
                    "Matching Game Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            // Take no action if the click is on a cell that's already face-up.
            var card = GetCardFromRowColumn(rowIndex, columnIndex);
            if (card.FaceUp)
            {
                return;
            }

            // Is this the first card turned over in an attempt to find a match?
            if (firstCardInMatchAttempt == null)
            {
                firstCardInMatchAttempt = (this.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithMatchingGame);
                firstCardInMatchAttempt.TurnOver(true);
            }
            else
            {
                // This must be the second card turned over in an attempt to find a match.
                secondCardInMatchAttempt = (this.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithMatchingGame);
                secondCardInMatchAttempt.TurnOver(true);

                // Has a match been found?
                var firstIndex = firstCardInMatchAttempt.GetCardIndex();
                var cardNameFirst = this.CardList[firstIndex].Name;

                var secondIndex = secondCardInMatchAttempt.GetCardIndex();
                var cardNameSecond = this.CardList[secondIndex].Name;

                if (cardNameFirst == cardNameSecond)
                {
                    // We have a match!
                    this.CardList[secondIndex].Matched = true;
                    this.CardList[firstIndex].Matched = true;

                    AnnounceAction(Resources.ThatsAMatch);

                    firstCardInMatchAttempt = null;
                    secondCardInMatchAttempt = null;

                    if (Settings.Default.PlaySoundOnMatch)
                    {
                        PlayCardMatchSound(true);
                    }

                    // Has the game been won?
                    if (GameIsWon())
                    {
                        // Todo: Localize this.
                        var answer = MessageBox.Show(
                            this,
                            "Congratulations! You won the game in " +
                            (tryAgainCount +
                                ((RowCount * ColumnCount) / 2)) +
                            " goes.\r\n\r\nWould you like another game?",
                            "Accessible Matching Game",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (answer == DialogResult.Yes)
                        {
                            ResetGrid();
                        }
                    }
                }
                else
                {
                    if (Settings.Default.PlaySoundOnNotMatch)
                    {
                        PlayCardMatchSound(false);
                    }
                }
            }
        }

        private void PlayCardMatchSound(bool cardsMatch)
        {
            if (soundPlayer != null)
            {
                soundPlayer.SoundLocation = (cardsMatch ? soundPathMatch : soundPathNotMatch);
                soundPlayer.Play();
            }
        }

        // Reset the grid to an initial game state.
        public void ResetGrid()
        {
            tryAgainCount = 0;

            firstCardInMatchAttempt = null;
            secondCardInMatchAttempt = null;

            for (int i = 0; i < this.CardList.Count; i++)
            {
                this.CardList[i].FaceUp = false;
                this.CardList[i].Matched = false;
            }

            this.Refresh();

            Shuffle();
        }

        public void TurnCardUp()
        {
            DataGridViewButtonCellWithMatchingGame selectedCard;

            if (this.SelectedCells.Count > 0)
            {
                selectedCard = this.SelectedCells[0] as DataGridViewButtonCellWithMatchingGame;
                ClickCell(selectedCard.RowIndex, selectedCard.ColumnIndex);
            }
        }

        public void TryAgain()
        {
            // Turn any face-up unmatched cards back over.
            if (firstCardInMatchAttempt != null)
            {
                ++tryAgainCount;

                firstCardInMatchAttempt.TurnOver(false);
                firstCardInMatchAttempt = null;

                if (secondCardInMatchAttempt != null)
                {
                    secondCardInMatchAttempt.TurnOver(false);
                    secondCardInMatchAttempt = null;
                }

                AnnounceAction(Resources.CardsTurnedBack);
            }
        }

        private bool GameIsWon()
        {
            for (int i = 0; i < this.CardList.Count; i++)
            {
                if (!this.CardList[i].Matched)
                {
                    return false;
                }
            }

            return true;
        }

        private void CardMatchingGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Set up clip regions below in order to prevent the image in the cell 
            // from flashing when the cell is repainted.

            Bitmap bmp = null;
            Rectangle rectBitmap = new Rectangle();
            Rectangle rectCellImage = new Rectangle();

            // Is this cell showing an image?
            var card = GetCardFromRowColumn(e.RowIndex, e.ColumnIndex);
            if (card.FaceUp)
            {
                var index = ((GridDimensions * e.RowIndex) + e.ColumnIndex);
                bmp = CardList[index].Image;

                rectBitmap = new Rectangle(0, 0, bmp.Width, bmp.Height);

                double ratio = Math.Max(
                    (double)rectBitmap.Width / (double)e.CellBounds.Width,
                    (double)rectBitmap.Height / (double)e.CellBounds.Height);

                // Bring in the image a few pixels from the border.
                var finalWidth = (int)(rectBitmap.Width / ratio) - 4;
                var finalHeight = (int)(rectBitmap.Height / ratio) - 4;

                rectCellImage = new Rectangle(
                    e.CellBounds.Left + ((e.CellBounds.Width - finalWidth) / 2),
                    e.CellBounds.Top + ((e.CellBounds.Height - finalHeight) / 2),
                    finalWidth,
                    finalHeight);

                e.Graphics.ExcludeClip(new Region(rectCellImage));
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

    public class DataGridViewButtonColumnMatchingGame : DataGridViewButtonColumn
    {
        public DataGridViewButtonColumnMatchingGame()
        {
            this.CellTemplate = new DataGridViewButtonCellWithMatchingGame();
        }
    }

    public class DataGridViewButtonCellWithMatchingGame : DataGridViewButtonCell
    {
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new DataGridViewButtonCellWithMatchingGameAccessibleObject(this);
        }

        public int GetCardIndex()
        {
            var columnCount = (this.DataGridView as CardMatchingGrid).GridDimensions;
            return ((columnCount * this.RowIndex) + this.ColumnIndex);
        }

        public void TurnOver(bool FaceUp)
        {
            var card = (this.DataGridView as CardMatchingGrid).GetCardFromRowColumn(this.RowIndex, this.ColumnIndex);
            card.FaceUp = FaceUp;

            if (FaceUp)
            {
                // Raise an event for screen reader to react to.
                (this.DataGridView as CardMatchingGrid).AnnounceAction(Resources.TurnedOver + " " + card.Name);
            }

            // Repaint the cell to show the image.
            var button = (this.DataGridView.Rows[this.RowIndex].Cells[this.ColumnIndex] as DataGridViewButtonCellWithMatchingGame);
            this.DataGridView.InvalidateCell(button);
        }

        protected class DataGridViewButtonCellWithMatchingGameAccessibleObject :
            DataGridViewButtonCellAccessibleObject
        {
            public DataGridViewButtonCellWithMatchingGameAccessibleObject(DataGridViewButtonCellWithMatchingGame owner) : base(owner)
            {
            }

            public override string Name
            {
                get
                {
                    var cardNamePrefix = Resources.ResourceManager.GetString("Card") + " " +
                        ((this.Owner as DataGridViewButtonCellWithMatchingGame).GetCardIndex() + 1);

                    var button = (this.Owner as DataGridViewButtonCellWithMatchingGame);
                    var card = (this.Owner.DataGridView as CardMatchingGrid).GetCardFromRowColumn(
                                    button.RowIndex, button.ColumnIndex);

                    var CardCurrentlyShowing = card.FaceUp ?
                        card.Name :
                        Resources.ResourceManager.GetString("FaceDown");

                    var cardFullName = cardNamePrefix + ", " + CardCurrentlyShowing;

                    return cardFullName;
                }
            }

            public override string Help
            {
                get
                {
                    var button = (this.Owner as DataGridViewButtonCellWithMatchingGame);
                    var index = button.GetCardIndex();

                    // A face-down card needs no description.
                    var card = (this.Owner.DataGridView as CardMatchingGrid).GetCardFromRowColumn(
                                    button.RowIndex, button.ColumnIndex);

                    string description = card.FaceUp ?
                        (this.Owner.DataGridView as CardMatchingGrid).CardList[index].Description :
                        "";

                    return description;
                }
            }

            // Return an empty Value here to avoid having a screen reader announce "Null".
            public override string Value
            {
                get
                {
                    return "";
                }
            }
        }
    }
}
