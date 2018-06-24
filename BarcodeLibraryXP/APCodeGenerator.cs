using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BarcodeLibrary
{
    public class APCodeGenerator
    {
        private string humanReadable;
        const string prefix = "";
        char GS = (char)29;
        #region Properties

        public string HumanReadable
        {
            get
            {
                return humanReadable;
            }
        }
        public char SymbolCharacter { get; set; }
        public string ApplicationIdentifier1 { get; set; }
        public string Indicator { get; set; }
        public string CompanyPrefix { get; set; }
        public string ItemReference { get; set; }
        public string CheckDigit1 { get; set; }
        public string MLID { get; set; }
        public string ApplicationIdentifier2 { get; set; }
        public string CustomerId { get; set; }
        public string ConsignmentId { get; set; }
        public string ArticleCount { get; set; }
        public string ProductCode { get; set; }
        public string ServiceCode { get; set; }
        public string PostagePaidIndicator { get; set; }
        public string CheckDigit2 { get; set; }
        public string ApplicationIdentifier3 { get; set; }
        public string Postcode { get; set; }
        public string ApplicationIdentifier4 { get; set; }
        public string DeliveryPointIdentifier { get; set; }
        public string ApplicationIdentifier5 { get; set; }
        public string DateAndTimeOfProduction { get; set; }

        #endregion

        public string GenerateAPBarcodeCode()
        {
            StringBuilder readable = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();

            sb1.Append(prefix);
            sb1.Append(ApplicationIdentifier1);
            sb1.Append(Indicator);
            sb1.Append(CompanyPrefix);
            sb1.Append(ItemReference);
            sb1.Append(CheckDigit1);

            // Australia Post Article Reference
            StringBuilder sb2 = new StringBuilder();

            // MLID
            sb2.Append(MLID);
            // Article Number
            sb2.Append(ConsignmentId);
            // Article count
            sb2.Append(ArticleCount);
            // Sub Product
            sb2.Append(ProductCode);
            // Base Service Code
            sb2.Append(ServiceCode);
            // Pre paid indicator
            sb2.Append(PostagePaidIndicator);
            // Check Digit
            sb2.Append(BarcodeTools.getChecksumBitForDataMatrix(sb2.ToString()));

            // Application Identifier
            sb1.Append(ApplicationIdentifier2);
            readable.Append(sb2);
            humanReadable = readable.ToString();

            return sb1.Append(sb2).ToString();
        }

        public string GenerateDataMatrixCode()
        {
            // Generate Checksum
            string humanReadable = MLID + ConsignmentId + ArticleCount + ProductCode
                + ServiceCode + PostagePaidIndicator;
            CheckDigit2 = BarcodeTools.getChecksumBitForDataMatrix(humanReadable);

            StringBuilder sb = new StringBuilder();

            //sb.Append(FN);
            sb.Append(prefix);
            sb.Append(ApplicationIdentifier1);
            sb.Append(Indicator);
            sb.Append(CompanyPrefix);
            sb.Append(ItemReference);
            sb.Append(CheckDigit1);

            if (ApplicationIdentifier2 != null)
            {
                sb.Append(ApplicationIdentifier2);
                sb.Append(MLID);
                sb.Append(ConsignmentId);
                sb.Append(ArticleCount);
                sb.Append(ProductCode);
                sb.Append(ServiceCode);
                sb.Append(PostagePaidIndicator);
                sb.Append(CheckDigit2);
            }
            if (ApplicationIdentifier3 != null)
            {
                sb.Append(GS);
                sb.Append(ApplicationIdentifier3);
                sb.Append(Postcode);
            }
            if (ApplicationIdentifier4 != null)
            {
                sb.Append(GS);
                sb.Append(ApplicationIdentifier4);
                sb.Append(DeliveryPointIdentifier);
            }
            if (ApplicationIdentifier5 != null)
            {
                sb.Append(GS);
                sb.Append(ApplicationIdentifier5);
                sb.Append(DateAndTimeOfProduction);
            }

            return sb.ToString();
        }
    }
}