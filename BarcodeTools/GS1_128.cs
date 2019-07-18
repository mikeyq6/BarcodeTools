using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neodynamic.SDK.Barcode;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BarcodeLibrary
{
    public class GS1_128
    {
        const string LICENSE_OWNER = "Michael Dann-Standard Edition-Developer License";
        const string LICENSE_KEY = "GRB8TAVNX3RSWUWJ4A5TB9L6Y6A5NU435RN8QXP6JTZENH2KSSWA";

        public Image Encode(string data, string readable, int barcodeHeight, int barcodeWidth)
        {
            Image image = null;
            BarcodeProfessional barcode = new BarcodeProfessional();
            BarcodeProfessional.LicenseOwner = LICENSE_OWNER;
            BarcodeProfessional.LicenseKey = LICENSE_KEY;
            barcode.Symbology = Symbology.GS1128;
            barcode.Code = data;
            barcode.BarcodeUnit = BarcodeUnit.Millimeter;
            //barcode.AutoSize = true;
            barcode.BarHeight = barcodeHeight;
            //barcode.OutputSettings.Add("Dpi", 500);
            barcode.HumanReadableText = readable;
            barcode.BarWidth = 0.5d;
            barcode.BottomMargin = 1.1;
            using (var ms = new MemoryStream
                (
                barcode.GetBarcodeImage(ImageFormat.Png, 300, new SizeF(barcodeWidth, barcodeHeight))
                )
            )
            {
                image = Image.FromStream(ms);
            }
            return image;
        }
    }
}