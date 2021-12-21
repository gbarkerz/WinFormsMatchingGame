using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Automation;
using WinFormsSquaresGame.Properties;

namespace WinFormsSquaresGame.Controls
{
    public class SquaresGrid : DataGridView
    {
        public List<Square> SquareList { get; set; }
        private int moveCount = 0;

        public SquaresGrid()
        {
            CellClick += DataGridView_CellClick;
            SizeChanged += Grid_SizeChanged;
            CellPainting += Grid_CellPainting;

            // The grid's accessible name is also set when the background picture is set.
            this.AccessibilityObject.Name = Resources.ResourceManager.GetString("SquaresGrid");

            LoadSettings();
        }

        private void LoadSettings()
        {
            this.ShowNumbers = Settings1.Default.ShowNumbers;
            this.NumberSizeIndex = Settings1.Default.NumberSizeIndex;
            this.ShowPicture = Settings1.Default.ShowPicture;
            this.BackgroundPictureFullName = Settings1.Default.BackgroundPicture;
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Square adjacentSquare = null;
            int emptySquareIndex = -1;
            string direction = "";

            // Is the empty square adjacent to this square?
            if (e.ColumnIndex > 0)
            {
                adjacentSquare = GetSquareFromRowColumn(e.RowIndex, e.ColumnIndex - 1);
                if (adjacentSquare.TargetIndex == EmptySquareTargetIndex)
                {
                    emptySquareIndex = GetSquareIndexFromRowColumn(e.RowIndex, e.ColumnIndex - 1);
                    
                    direction = Resources.ResourceManager.GetString("Left");
                }
            }

            if ((emptySquareIndex == -1) && (e.RowIndex > 0))
            {
                adjacentSquare = GetSquareFromRowColumn(e.RowIndex - 1, e.ColumnIndex);
                if (adjacentSquare.TargetIndex == EmptySquareTargetIndex)
                {
                    emptySquareIndex = GetSquareIndexFromRowColumn(e.RowIndex - 1, e.ColumnIndex);

                    direction = Resources.ResourceManager.GetString("Up");
                }
            }

            if ((emptySquareIndex == -1) && (e.ColumnIndex < GridDimensions - 1))
            {
                adjacentSquare = GetSquareFromRowColumn(e.RowIndex, e.ColumnIndex + 1);
                if (adjacentSquare.TargetIndex == EmptySquareTargetIndex)
                {
                    emptySquareIndex = GetSquareIndexFromRowColumn(e.RowIndex, e.ColumnIndex + 1);

                    direction = Resources.ResourceManager.GetString("Right");
                }
            }

            if ((emptySquareIndex == -1) && (e.RowIndex < GridDimensions - 1))
            {
                adjacentSquare = GetSquareFromRowColumn(e.RowIndex + 1, e.ColumnIndex);
                if (adjacentSquare.TargetIndex == EmptySquareTargetIndex)
                {
                    emptySquareIndex = GetSquareIndexFromRowColumn(e.RowIndex + 1, e.ColumnIndex);

                    direction = Resources.ResourceManager.GetString("Down");
                }
            }

            // If we found an adjacent empty square, swap the clicked square with the empty square.
            if (emptySquareIndex != -1)
            {
                ++moveCount;

                var emptySquare = this.SquareList[emptySquareIndex];

                var clickedSquareIndex = GetSquareIndexFromRowColumn(e.RowIndex, e.ColumnIndex);
                var clickedSquare = this.SquareList[clickedSquareIndex];

                this.SquareList[emptySquareIndex] = clickedSquare;
                this.SquareList[clickedSquareIndex] = emptySquare;

                // Todo: Only refresh the two affected squares, not the whole grid.
                this.Refresh();

                // Has the game been won?
                if (GameIsWon())
                {
                    // Todo: Localize this.
                    var answer = MessageBox.Show(
                        this,
                        "Congratulations! You won the game in " + moveCount +
                        " goes.\r\n\r\nWould you like another game?",
                        "Accessible Squares Game",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        ResetGrid();
                    }
                }
                else
                {
                    string announcement = Resources.ResourceManager.GetString("Moved") + 
                        " " + clickedSquare.Name + " " + direction + ".";
                    AnnounceAction(announcement);
                }
            }
            else
            {
                string announcement = Resources.ResourceManager.GetString("NoMovePossible");
                AnnounceAction(announcement);
            }
        }

        private bool GameIsWon()
        {
            bool gameIsWon = true;

            for (int i = 0; i < this.SquareList.Count; i++)
            {
                if (SquareList[i].TargetIndex != i)
                {
                    gameIsWon = false;

                    break;
                }
            }

            return gameIsWon;
        }

        // Reset the grid to an initial game state.
        public void ResetGrid()
        {
            moveCount = 0;

            Shuffle();

            this.Refresh();
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

        public int GetSquareIndexFromRowColumn(int rowIndex, int columnIndex)
        {
            return (this.GridDimensions * rowIndex) + columnIndex;
        }

        public Square GetSquareFromRowColumn(int rowIndex, int columnIndex)
        {
            var index = (this.GridDimensions * rowIndex) + columnIndex;
            return this.SquareList[index];
        }

        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            int pictureOffset = 6;
            int showNumberScale = 4 - this.NumberSizeIndex;

            // If this is the empty square, we don;t need to do any custom painting here.
            var square = GetSquareFromRowColumn(e.RowIndex, e.ColumnIndex);
            if (square.TargetIndex == EmptySquareTargetIndex)
            {
                return;
            }

            // Set up a clip region to cover where we expect to draw our own content.
            if (ShowNumbers || (ShowPicture && (backgroundPicture != null)))
            {
                Rectangle rectClip = e.CellBounds;

                if (ShowPicture && (backgroundPicture != null))
                {
                    // We'll be filling most of the cell with a picture.
                    rectClip.Inflate(-pictureOffset, -pictureOffset);
                }
                else if (ShowNumbers)
                {
                    // We'll just be showing a number at the top left corner of the square.
                    rectClip = new Rectangle(
                        e.CellBounds.Location.X + pictureOffset, e.CellBounds.Location.Y + pictureOffset,
                        Math.Min(e.CellBounds.Width - (2 * pictureOffset), e.CellBounds.Size.Width / showNumberScale),
                        Math.Min(e.CellBounds.Height - (2 * pictureOffset), e.CellBounds.Size.Height / showNumberScale));
                }

                e.Graphics.ExcludeClip(new Region(rectClip));
            }

            // Now paint everything but our own content.
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            if (ShowNumbers || (ShowPicture && (backgroundPicture != null)))
            {
                e.Graphics.Clip = new Region();

                var rectNumber = new Rectangle();

                if (ShowNumbers)
                {
                    rectNumber = new Rectangle(
                        e.CellBounds.Location.X + pictureOffset, e.CellBounds.Location.Y + pictureOffset,
                        Math.Min(e.CellBounds.Width - (2 * pictureOffset), e.CellBounds.Size.Width / showNumberScale),
                        Math.Min(e.CellBounds.Height - (2 * pictureOffset), e.CellBounds.Size.Height / showNumberScale));
                }

                // Do we have a background picture to paint?
                if (ShowPicture && (backgroundPicture != null))
                {
                    // Set another clip region to exclude painting the image where
                    // a square number will lie over it.
                    e.Graphics.ExcludeClip(new Region(rectNumber));

                    int width = backgroundPicture.Width / GridDimensions;
                    int height = backgroundPicture.Height / GridDimensions;

                    int squareColumnIndex = square.TargetIndex % GridDimensions;
                    int squareRowIndex = square.TargetIndex / GridDimensions;

                    int left = squareColumnIndex * width;
                    int top = squareRowIndex * height;

                    Rectangle rectBitmap = new Rectangle(left, top, width, height);

                    Rectangle rectCellImage = e.CellBounds;
                    rectCellImage.Inflate(-pictureOffset, -pictureOffset);

                    // Now paint the image in the cell.
                    e.Graphics.DrawImage(backgroundPicture,
                        rectCellImage,
                        rectBitmap,
                        GraphicsUnit.Pixel);

                    e.Graphics.Clip = new Region();
                }

                if (ShowNumbers)
                {
                    e.Graphics.FillRectangle(SystemBrushes.Control, rectNumber);

                    int fontHeight = this.Font.FontFamily.GetEmHeight(FontStyle.Regular);
                    int lineSpacing = this.Font.FontFamily.GetLineSpacing(FontStyle.Regular);
                    var targetHeight = (e.CellBounds.Height * fontHeight) / (showNumberScale * lineSpacing);
                    var font = new Font(this.Font.FontFamily, targetHeight, GraphicsUnit.Pixel);

                    StringFormat format = new StringFormat();
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;

                    e.Graphics.DrawString(
                        (square.TargetIndex + 1).ToString(),
                        font,
                        SystemBrushes.ControlText,
                        rectNumber,
                        format);
                }
            }

            e.Handled = true;
        }

        public int GridDimensions
        {
            get
            {
                return (int)Math.Sqrt(SquareList.Count);
            }
        }

        public void Shuffle()
        {
            var shuffler = new Shuffler();

            // Keep shuffling until the arrangement of squares can be solved.
            bool gameCanBeSolved;

            do
            {
                shuffler.Shuffle(this.SquareList);

                gameCanBeSolved = CanGameBeSolved();
            }
            while (!gameCanBeSolved);
        }

        private bool CanGameBeSolved()
        {
            int parity = 0;
            int emptySquareRow = 0;

            for (int i = 0; i < this.SquareList.Count; i++)
            {
                if (this.SquareList[i].TargetIndex == EmptySquareTargetIndex)
                {
                    // The empty square row is one-based.
                    emptySquareRow = (i / GridDimensions) + 1;

                    continue;
                }

                for (int j = i + 1; j < this.SquareList.Count; j++)
                {
                    if ((this.SquareList[j].TargetIndex != EmptySquareTargetIndex) &&
                        (this.SquareList[i].TargetIndex > this.SquareList[j].TargetIndex))
                    {
                        parity++;
                    }
                }
            }

            // The following applies to an even grid.

            bool gridCanBeSolved = false;

            if (emptySquareRow % 2 == 0)
            {
                gridCanBeSolved = (parity % 2 == 0);
            }
            else
            {
                gridCanBeSolved = (parity % 2 != 0);
            }

            return gridCanBeSolved;
        }

        private bool showNumbers;
        public bool ShowNumbers 
        { 
            get
            {
                return showNumbers;
            }
            set
            {
                showNumbers = value;
                this.Refresh();
            }
        }

        private int numberSizeIndex;
        public int NumberSizeIndex
        {
            get
            {
                return numberSizeIndex;
            }
            set
            {
                numberSizeIndex = value;
                this.Refresh();
            }
        }

        private bool showPicture;
        public bool ShowPicture
        {
            get
            {
                return showPicture;
            }
            set
            {
                showPicture = value;
                this.Refresh();
            }
        }

        private Bitmap backgroundPicture;
        private string backgroundPictureFullName;
        public string BackgroundPictureFullName
        {
            get
            {
                return backgroundPictureFullName;
            }
            set
            {
                try
                {
                    this.backgroundPictureFullName = value;

                    string accessibleName = Resources.ResourceManager.GetString("SquaresGrid");

                    if (!string.IsNullOrWhiteSpace(this.backgroundPictureFullName))
                    {
                        this.backgroundPicture = new Bitmap(this.backgroundPictureFullName);

                        accessibleName += " showing " +
                            Path.GetFileNameWithoutExtension(this.backgroundPictureFullName);

                        this.Refresh();
                    }

                    this.AccessibilityObject.Name = accessibleName;
                }
                catch (Exception)
                {
                    this.backgroundPictureFullName = "";
                }
            }
        }

        private int EmptySquareTargetIndex
        {
            get
            {
                // The empty square should always end up in the bottom right corner of the grid.
                return this.SquareList.Count - 1;
            }
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
    }
}