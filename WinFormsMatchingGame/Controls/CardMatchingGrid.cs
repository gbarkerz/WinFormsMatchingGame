using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinFormsMatchingGame.Properties;

namespace WinFormsMatchingGame.Controls
{
    public class CardMatchingGrid : DataGridView
    {
        public List<Card> CardList { get; set; }

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

            var button = (this.DataGridView.Rows[this.RowIndex].Cells[this.ColumnIndex] as DataGridViewButtonCellWithCustomName);
            this.DataGridView.InvalidateCell(button);
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

            // Attempting to override the UIA ControlType has apparently has no effect.
            // public override AccessibleRole Role
        }
    }
}
