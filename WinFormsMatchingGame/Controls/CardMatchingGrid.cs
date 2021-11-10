using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Automation;
using WinFormsMatchingGame.Properties;

namespace WinFormsMatchingGame.Controls
{
    public class CardMatchingGrid : DataGridView
    {
        public List<Card> CardList { get; set; }
        private DataGridViewButtonCellWithCustomName firstCardInMatchAttempt;
        private DataGridViewButtonCellWithCustomName secondCardInMatchAttempt;
        private int tryAgainCount = 0;

        public CardMatchingGrid()
        {
            CellClick += DataGridView_CellClick;
            SizeChanged += Grid_SizeChanged;
            CellPainting += CardMatchingGrid_CellPainting;
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
            var gridDimensions = GridDimensions;

            for (int i = 0; i < gridDimensions; ++i)
            {
                Columns[i].Width = (this.ClientSize.Width / gridDimensions) - 1;
                Rows[i].Height = (this.ClientSize.Height / gridDimensions) - 1;
            }
        }

        public int GridDimensions
        {
            get
            {
                return (int)Math.Sqrt(CardList.Count);
            }
        }

        public Card GetCardFromRowColumn(int rowIndex, int columnIndex)
        {
            var columnCount = this.GridDimensions;
            var index = (columnCount * rowIndex) + columnIndex;
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

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return base.ProcessDialogKey(keyData);
        }

        // Todo: Is overriding the OnKeyDown/OnKeyUp really the most appropriate 
        // way of changing the behavior of Enter when focus is on the grid?
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Take no action in response to Enter KeyDown.
            if (e.KeyValue != 13)
            {
                base.OnKeyDown(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            // Enter KeyDown results in any unmatched cards being turned back.
            if (e.KeyValue != 13)
            {
                base.OnKeyUp(e);
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
                AnnounceAction(Resources.TurnCardsBackToContinue);

                return;
            }

            // Take no action if the click is on a cell that's already face-up.
            var card = GetCardFromRowColumn(rowIndex, columnIndex);
            if (card.FaceUp)
            {
                return;
            }

            var gridDimensions = GridDimensions;
            var index = (gridDimensions * rowIndex) + columnIndex;

            // Is this the first card turned over in an attempt to find a match?
            if (firstCardInMatchAttempt == null)
            {
                firstCardInMatchAttempt = (this.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName);
                firstCardInMatchAttempt.TurnOver(true);
            }
            else
            {
                // This must be the second card turned over in an attempt to find a match.
                // Has a match been found?

                var firstIndex = firstCardInMatchAttempt.GetCardIndex();
                var cardNameFirst = this.CardList[firstIndex].Name;

                var cardNameSecond = this.CardList[index].Name;

                secondCardInMatchAttempt = (this.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName);

                if (cardNameFirst == cardNameSecond)
                {
                    // We have a match!
                    var button = (this.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName);
                    button.TurnOver(true);

                    this.CardList[index].Matched = true;

                    this.CardList[index].Matched = true;
                    this.CardList[firstIndex].Matched = true;

                    firstCardInMatchAttempt = null;
                    secondCardInMatchAttempt = null;

                    AnnounceAction(Resources.ThatsAMatch);

                    // Has the game been won?
                    if (GameIsWon())
                    {
                        // Todo: Localized this.
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
                    // This second card is not a match.
                    secondCardInMatchAttempt = (this.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName);
                    secondCardInMatchAttempt.TurnOver(true);
                }
            }
        }

        public void ResetGrid()
        {
            tryAgainCount = 0;

            for (int i = 0; i < this.CardList.Count; i++)
            {
                this.CardList[i].FaceUp = false;
                this.CardList[i].Matched = false;
            }

            firstCardInMatchAttempt = null;
            secondCardInMatchAttempt = null;

            var gridDimensions = GridDimensions;

            for (int r = 0; r < gridDimensions; r++)
            {
                for (int c = 0; c < gridDimensions; c++)
                {
                    var button = (this.Rows[r].Cells[c] as DataGridViewButtonCellWithCustomName);
                    button.TurnOver(false);
                }
            }

            Shuffle();
        }

        public void TryAgain()
        {
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
            // Set up clip regions below in order to prevent the image in the cell from
            // flashing when the cell is repainted.

            Bitmap bmp = null;
            Rectangle rectBitmap = new Rectangle();
            Rectangle rectCellImage = new Rectangle();

            // Is this cell showing an image?
            var card = GetCardFromRowColumn(e.RowIndex, e.ColumnIndex);
            if (card.FaceUp)
            {
                var columnCount = GridDimensions;
                var index = ((columnCount * e.RowIndex) + e.ColumnIndex);
                bmp = CardList[index].Image;

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

    public class DataGridViewButtonColumnWithCustomName : DataGridViewButtonColumn
    {
        public DataGridViewButtonColumnWithCustomName()
        {
            this.CellTemplate = new DataGridViewButtonCellWithCustomName();
        }
    }

    public class DataGridViewButtonCellWithCustomName : DataGridViewButtonCell
    {
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new DataGridViewButtonCellWithCustomNameAccessibleObject(this);
        }

        public void TurnOver(bool FaceUp)
        {
            var card = (this.DataGridView as CardMatchingGrid).GetCardFromRowColumn(this.RowIndex, this.ColumnIndex);
            card.FaceUp = FaceUp;

            if (FaceUp)
            {
                RaiseCardEvent(card);
            }

            var button = (this.DataGridView.Rows[this.RowIndex].Cells[this.ColumnIndex] as DataGridViewButtonCellWithCustomName);
            this.DataGridView.InvalidateCell(button);
        }

        private void RaiseCardEvent(Card card)
        {
            (this.DataGridView as CardMatchingGrid).AnnounceAction(Resources.TurnedOver + card.Name);
        }

        public int GetCardIndex()
        {
            var columnCount = (this.DataGridView as CardMatchingGrid).GridDimensions;
            return ((columnCount * this.RowIndex) + this.ColumnIndex);
        }

        protected class DataGridViewButtonCellWithCustomNameAccessibleObject :
            DataGridViewButtonCellAccessibleObject
        {
            public DataGridViewButtonCellWithCustomNameAccessibleObject(DataGridViewButtonCellWithCustomName owner) : base(owner)
            {
            }

            public override string Name
            {
                get
                {
                    var cardName = Resources.ResourceManager.GetString("Card") + " " +
                        ((this.Owner as DataGridViewButtonCellWithCustomName).GetCardIndex() + 1);

                    cardName += ", " + this.CurrentExposedName;

                    return cardName;
                }
            }

            // The currently exposed name is too important to not be announced by a
            // screen reader, so don't only have it be the Value here.
            //public override string Value 

            private string CurrentExposedName
            {
                get
                {
                    var button = (this.Owner as DataGridViewButtonCellWithCustomName);
                    var card = (this.Owner.DataGridView as CardMatchingGrid).GetCardFromRowColumn(
                                    button.RowIndex, button.ColumnIndex);

                    string value = card.FaceUp ?
                        card.Name :
                        Resources.ResourceManager.GetString("FaceDown");

                    return value;
                }
            }

            // Don't override the Description property here. Doing so impacts how data
            // gets exposed through a legacy Windows accessibility API, but not how 
            // Windows UI Automation clients expect it to be exposed. So override the 
            // Help property instead, as that maps to the UIA HelpText property.
            public override string Help
            {
                get
                {
                    var button = (this.Owner as DataGridViewButtonCellWithCustomName);
                    var index = button.GetCardIndex();

                    // A face down card needs no description.
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

            // Attempting to override the UIA ControlType has apparently has no effect.
            // public override AccessibleRole Role
        }
    }
}
