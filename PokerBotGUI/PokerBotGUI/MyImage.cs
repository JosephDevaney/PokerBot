using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace PokerBotGUI
{
    public class MyImage
    {
        private ImageSource image;

        public ImageSource Image
        {
            get { return image; }
        }

        private string name;

        public string Name
        {
            get { return name; }
        }

        public MyImage(ImageSource image, string name)
        {
            this.image = image;
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
