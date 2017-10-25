﻿using System.Drawing;
using System.Windows.Forms;

namespace kagv {

    public partial class MainForm {

        //function for importing an image as background
        private void ImportImage() {
            ofd_importmap.Filter = "png (*.png)|*.png | jpg (*.jpg)|*.jpg";
            ofd_importmap.FileName = "";

            if (ofd_importmap.ShowDialog() == DialogResult.OK) {
                _importedLayout = Image.FromFile(ofd_importmap.FileName);
                _importedImageFile = Image.FromFile(ofd_importmap.FileName);
                _overImage = true;
            }

        }
    }
}