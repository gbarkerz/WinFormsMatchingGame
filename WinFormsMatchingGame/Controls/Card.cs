using System;
using System.Drawing;

namespace WinFormsMatchingGame.Controls
{
    public class Card
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Bitmap Image { get; set; }
        public bool FaceUp { get; set; }
        public bool Matched { get; set; }
    }

}
