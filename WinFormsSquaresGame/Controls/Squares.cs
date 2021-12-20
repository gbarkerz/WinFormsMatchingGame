using System;
using System.Drawing;

namespace WinFormsSquaresGame.Controls
{
    public class Square
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Bitmap Image { get; set; }
        public int TargetIndex { get; set; }
    }
}
