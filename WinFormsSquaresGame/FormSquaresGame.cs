﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsSquaresGame.Controls;
using WinFormsSquaresGame.Properties;

namespace WinFormsSquaresGame
{
    public partial class FormSquaresGame : Form
    {
        private SquaresGrid squaresGrid;

        public FormSquaresGame()
        {
            InitializeComponent();

            CreateSquaresGrid();
        }

        private void CreateSquaresGrid()
        {
            // Create the grid to host the cards shown in the game.
            squaresGrid = new SquaresGrid();

            // Set up the grid in a way that most closely matches the needs of the game.
            squaresGrid.RowHeadersVisible = false;
            squaresGrid.ColumnHeadersVisible = false;
            squaresGrid.AllowUserToResizeRows = false;
            squaresGrid.AllowUserToResizeColumns = false;
            squaresGrid.AllowUserToAddRows = false;
            squaresGrid.Dock = DockStyle.Fill;
            squaresGrid.ShowCellToolTips = false;

            // Make sure the grid itself has an accessible name.
            squaresGrid.AccessibilityObject.Name = Resources.ResourceManager.GetString("SquaresGrid");

            // Don't have Tab presses move keyboard focus between cells in the grid.
            squaresGrid.StandardTab = true;

            // The game currently only shows a 4x4 grid of cards.

            squaresGrid.Columns.Add(new DataGridViewButtonColumnMatchingGame());
            squaresGrid.Columns.Add(new DataGridViewButtonColumnMatchingGame());
            squaresGrid.Columns.Add(new DataGridViewButtonColumnMatchingGame());
            squaresGrid.Columns.Add(new DataGridViewButtonColumnMatchingGame());

            squaresGrid.Rows.Add();
            squaresGrid.Rows.Add();
            squaresGrid.Rows.Add();
            squaresGrid.Rows.Add();

            // Set up the cards to be shown.
            SetupSquareList();

            this.panelSquaresGrid.Controls.Add(squaresGrid);
        }

        private void SetupSquareList()
        {
            SetupDefaultSquaresList();

            squaresGrid.Shuffle();
        }

        private void SetupDefaultSquaresList()
        {
            var resManager = Resources.ResourceManager;

            // Note: This app assumes the total count of cards is 16.
            squaresGrid.SquareList = new List<Square>()
                {
                    new Square {
                        TargetIndex = 0,
                        Name = resManager.GetString("DefaultSquare1Name"),
                        Description = resManager.GetString("DefaultSquare1Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square1) },
                    new Square {
                        TargetIndex = 1,
                        Name = resManager.GetString("DefaultSquare2Name"),
                        Description = resManager.GetString("DefaultSquare2Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square2) },
                    new Square {
                        TargetIndex = 2,
                        Name = resManager.GetString("DefaultSquare3Name"),
                        Description = resManager.GetString("DefaultSquare3Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square3) },
                    new Square {
                        TargetIndex = 3,
                        Name = resManager.GetString("DefaultSquare4Name"),
                        Description = resManager.GetString("DefaultSquare4Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square4) },
                    new Square {
                        TargetIndex = 4,
                        Name = resManager.GetString("DefaultSquare5Name"),
                        Description = resManager.GetString("DefaultSquare5Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square5) },
                    new Square {
                        TargetIndex = 5,
                        Name = resManager.GetString("DefaultSquare6Name"),
                        Description = resManager.GetString("DefaultSquare6Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square6) },
                    new Square {
                        TargetIndex = 6,
                        Name = resManager.GetString("DefaultSquare7Name"),
                        Description = resManager.GetString("DefaultSquare7Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square7) },
                    new Square {
                        TargetIndex = 7,
                        Name = resManager.GetString("DefaultSquare8Name"),
                        Description = resManager.GetString("DefaultSquare8Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square8) },
                    new Square {
                        TargetIndex = 8,
                        Name = resManager.GetString("DefaultSquare9Name"),
                        Description = resManager.GetString("DefaultSquare9Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square9) },
                    new Square {
                        TargetIndex = 9,
                        Name = resManager.GetString("DefaultSquare10Name"),
                        Description = resManager.GetString("DefaultSquare10Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square10) },
                    new Square {
                        TargetIndex = 10,
                        Name = resManager.GetString("DefaultSquare11Name"),
                        Description = resManager.GetString("DefaultSquare11Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square11) },
                    new Square {
                        TargetIndex = 11,
                        Name = resManager.GetString("DefaultSquare12Name"),
                        Description = resManager.GetString("DefaultSquare12Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square12) },
                    new Square {
                        TargetIndex = 12,
                        Name = resManager.GetString("DefaultSquare13Name"),
                        Description = resManager.GetString("DefaultSquare13Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square13) },
                    new Square {
                        TargetIndex = 13,
                        Name = resManager.GetString("DefaultSquare14Name"),
                        Description = resManager.GetString("DefaultSquare14Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square14) },
                    new Square {
                        TargetIndex = 14,
                        Name = resManager.GetString("DefaultSquare15Name"),
                        Description = resManager.GetString("DefaultSquare15Description"),
                        Image = new Bitmap(WinFormsSquaresGame.Properties.Resources.Square15) },
                    new Square {
                        TargetIndex = 15,
                        Name = resManager.GetString("DefaultSquareEmpty"),
                        Description = resManager.GetString("DefaultSquareEmptyDescription"),
                        Image = null },
                };
        }

        private void closeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void restartToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.squaresGrid.ResetGrid();
        }
    }

    public class DataGridViewButtonColumnMatchingGame : DataGridViewButtonColumn
    {
        public DataGridViewButtonColumnMatchingGame()
        {
            this.CellTemplate = new DataGridViewButtonCellSquaresGame();
        }
    }

    public class DataGridViewButtonCellSquaresGame : DataGridViewButtonCell
    {
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new DataGridViewButtonCellSquaresGameAccessibleObject(this);
        }

        public int GetCardIndex()
        {
            var columnCount = (this.DataGridView as SquaresGrid).GridDimensions;
            return ((columnCount * this.RowIndex) + this.ColumnIndex);
        }

        protected class DataGridViewButtonCellSquaresGameAccessibleObject :
            DataGridViewButtonCellAccessibleObject
        {
            public DataGridViewButtonCellSquaresGameAccessibleObject(DataGridViewButtonCellSquaresGame owner) : base(owner)
            {
            }

            public override string Name
            {
                get
                {
                    var button = (this.Owner as DataGridViewButtonCellSquaresGame);
                    var card = (this.Owner.DataGridView as SquaresGrid).GetSquareFromRowColumn(
                                    button.RowIndex, button.ColumnIndex);

                    return card.Name;
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
