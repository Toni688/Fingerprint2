using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;

using iText.IO.Font;
using iText.Kernel.Font;

//using iTextSharp.text;
//using iTextSharp.text.pdf;

using iText.Layout.Properties;
using iText.License;

namespace FingerPrint2
{

    public partial class Form1 : Form
    {
        public String src = "C:/Users/Toni/Desktop/Fingerprint_Test/hello_Fingerprint_src.pdf";
        public String dest = "C:/Users/Toni/Desktop/Fingerprint_Test/hello_Fingerprint.pdf";
        // Note slash directions
        public String info_src = @"C:\Users\Toni\Desktop\Fingerprint_Test\Information.txt";
        public String LeftIndex = "C:/Users/Toni/Desktop/Fingerprint_Test/LeftIndex.bmp";
        public String RightIndex = "C:/Users/Toni/Desktop/Fingerprint_Test/RightIndex.bmp";
        public String FONT = @"C:\Users\Toni\Downloads\itext7-dotnet-7.1.1\itext7-dotnet-7.1.1\itext.tests\itext.kernel.tests\resources\itext\kernel\pdf\fonts/NotoNaskhArabic-Regular.ttf";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LicenseKey.LoadLicenseFile ("C:/Users/Toni/Desktop/Fingerprint_Test/itextkey1524180183686_0.xml");

            iText.Layout.Element.Image leftIndex = new iText.Layout.Element.Image(ImageDataFactory.Create(LeftIndex));
            iText.Layout.Element.Image rightIndex = new iText.Layout.Element.Image(ImageDataFactory.Create(RightIndex));
            
            /*
            // Modify PDF located at "source" and save to "target"
            PdfDocument pdfDocument = new PdfDocument(new PdfReader(src), new PdfWriter(dest));
            // Document to add layout elements: paragraphs, images etc
            Document document = new Document(pdfDocument);

            // Load image from disk
            ImageData imageData = ImageDataFactory.Create(LeftIndex);
            // Create layout image object and provide parameters. Page number = 1
            iText.Layout.Element.Image image = new iText.Layout.Element.Image(imageData).ScaleAbsolute(100, 200).SetFixedPosition(1, 25, 25);
            // This adds the image to the page
            document.Add(image);

            // Don't forget to close the document.
            // When you use Document, you should close it rather than PdfDocument instance
            document.Close();
            */

            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines(info_src, Encoding.UTF8);

            //string line1 = System.Text.Encoding.UTF8.GetString(lines[1]);

            // Display the file contents by using a foreach loop.
            //Console.OutputEncoding = Encoding.UTF8;
            //System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            //System.Console.WriteLine("Contents of WriteLines2.txt = ");
            textBox1.Text = lines[1];
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                //Console.WriteLine("\t" + line/*, Console.OutputEncoding.UTF8Encoding*/);
            }


            var writer = new PdfWriter(dest);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);
            document.Add(new Paragraph("Hello World!"));
                
            //************************************************************************************
            //Russian Part:

            //Text Constants:
            String CZECH =
            "Podivn\u00fd p\u0159\u00edpad Dr. Jekylla a pana Hyda";
            String RUSSIAN =
            "\u0421\u0442\u0440\u0430\u043d\u043d\u0430\u044f "
            + "\u0438\u0441\u0442\u043e\u0440\u0438\u044f "
            + "\u0434\u043e\u043a\u0442\u043e\u0440\u0430 "
            + "\u0414\u0436\u0435\u043a\u0438\u043b\u0430 \u0438 "
            + "\u043c\u0438\u0441\u0442\u0435\u0440\u0430 "
            + "\u0425\u0430\u0439\u0434\u0430";
            String KOREAN =
            "\ud558\uc774\ub4dc, \uc9c0\ud0ac, \ub098";

            //Font Constants:
            String FONT = "C:/Users/Toni/Desktop/Fingerprint_Test/FreeSans.ttf"; //"src/main/resources/fonts/FreeSans.ttf";
            String HCRBATANG = "C:/Users/Toni/Desktop/Fingerprint_Test/HANBatang.ttf"; //"src/main/resources/fonts/HANBatang.ttf";
            //String NAZANIN = "C:/Users/Toni/Desktop/Fingerprint_Test/B Nazanin.ttf"; //"src/main/resources/fonts/HANBatang.ttf";

            //Add to Document:
            PdfFont font1250 = PdfFontFactory.CreateFont(FONT, PdfEncodings.CP1250, true);
            document.Add(new Paragraph().SetFont(font1250)
            .Add(CZECH).Add(" by Robert Louis Stevenson"));
            PdfFont font1251 = PdfFontFactory.CreateFont(FONT, "Cp1251", true);
            document.Add(new Paragraph().SetFont(font1251)
            .Add(RUSSIAN).Add(" by Robert Louis Stevenson"));
            PdfFont fontUnicode = PdfFontFactory.CreateFont(HCRBATANG, PdfEncodings.IDENTITY_H, true);
            document.Add(new Paragraph().SetFont(fontUnicode)
            .Add(KOREAN).Add(" by Robert Louis Stevenson"));

            //End Russian Part
            //************************************************************************************
            
            /*
            //Add Arabic-Inverted
            PdfFont fontUnicodeNazanin =
            PdfFontFactory.CreateFont(NAZANIN, PdfEncodings.IDENTITY_H, true);
            //document.Add(new Paragraph().SetFont(fontUnicodeNazanin)
            //.Add("الموافق").Add(" by Ostad"));

            //Add Arabic Correct (?) @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //itext 7 Equivalent
            //Text redText = new Text("Nos formules")
            Text redText = new Text("الموافق")
            .SetFont(fontUnicodeNazanin)
            .SetFontSize(20);
            Paragraph p2 = new Paragraph().Add(redText);
            //document.Add(p2);
            //document.Add(new Phrase("This is a ميسو ɸ", f));
            */
            
            /*
            //************************************************************************************
            //Style Part:
            Style arabic = new Style().SetTextAlignment(TextAlignment.RIGHT).SetBaseDirection(BaseDirection.RIGHT_TO_LEFT).
                SetFontSize(20).SetFont(fontUnicodeNazanin);

            //document.Add(new Paragraph("م الموافق").SetFontScript(Character.UnicodeScript.ARABIC).AddStyle(arabic));


            //End Style Part
            //************************************************************************************
            */
            
            /*
            var times = PdfFontFactory.CreateFont(NAZANIN, PdfEncodings.IDENTITY_H, true);
            Style arabic2 = new Style().SetBaseDirection(BaseDirection.RIGHT_TO_LEFT);
            Paragraph p3 = new Paragraph();
            p3.SetFont(NAZANIN);
            p3.AddStyle(arabic2);
            Text t = new Text("أسبوع");
            p3.Add(t);
            //document.Add(p3);
            */

            //************************************************************************************
            //Calligraph Part:

            Document arabicPdf = new Document(new PdfDocument(new PdfWriter("C:/Users/Toni/Desktop/Fingerprint_Test/arabic.pdf")));

            // Arabic text starts near the top right corner of the page
            arabicPdf.SetTextAlignment(TextAlignment.RIGHT);

            // create a font, and make it the default for the document
            //PdfFont f = PdfFontFactory.CreateFont("C:/Users/Toni/Desktop/Fingerprint_Test/DroidKufi-Regular.ttf", PdfEncodings.IDENTITY_H, true);
            //Arial -> OK
            //Tahoma -> OK
            //Koodak -> OK
            //A_Nefel_Botan -> OK
            //PdfFont f = PdfFontFactory.CreateFont("C:/Windows/Fonts/tahoma.ttf", PdfEncodings.IDENTITY_H, true);
            PdfFont f = PdfFontFactory.CreateFont("C:/Users/Toni/Desktop/Fingerprint_Test/A_Nefel_Botan.ttf", PdfEncodings.IDENTITY_H, true);
            //PdfFont f = PdfFontFactory.CreateFont("C:/Windows/Fonts/W_nazanin.ttf", PdfEncodings.IDENTITY_H, true);
            arabicPdf.SetFont(f);

            // add content: السلام عليكم (as-salaamu 'aleykum - peace be upon you)
            //arabicPdf.Add(new Paragraph("أسبوع"));
            //arabicPdf.Add(new Paragraph("هویج"));
            arabicPdf.Add(new Paragraph(lines[1]));

            arabicPdf.Close();

            //End Calligraph Part
            //************************************************************************************

            /*
            //************************************************************************************
            //Hebrew Part:

            //Use a table so that we can set the text direction 
            Table table = new Table(1);
            //Ensure that wrapping is on, otherwise Right to Left text will not display 
            //table.DefaultCell.NoWrap = false;
            table.AddStyle(arabic);

            table.AddCell(new Cell().SetHorizontalAlignment(HorizontalAlignment.CENTER).Add(p2));
 

            //Create a regex expression to detect hebrew or arabic code points 
            const string regex_match_arabic_hebrew = @"[\u0600-\u06FF,\u0590-\u05FF]+";
            //if (Regex.IsMatch("מה קורה", regex_match_arabic_hebrew, RegexOptions.IgnoreCase))
            {
                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            }

            //Create a cell and add text to it 
            PdfPCell text = new PdfPCell(new Phrase("מה קורה", font));
            //Ensure that wrapping is on, otherwise Right to Left text will not display 
            text.NoWrap = false;

            //Add the cell to the table 
            table.AddCell(text);

            //Add the table to the document 
            document.Add(table);
            //End Hebrew Part
            //************************************************************************************
            */

            document.Add(new Paragraph(lines[1]));
            Paragraph p = new Paragraph("Right Index").Add(rightIndex).Add("Left Index").Add(leftIndex);
            // Add Paragraph to document
            document.Add(p);

            //********************************************************************************************************
            //Arabic in pdf using iTextSharp in c#
            //********************************************************************************************************
            /*
            //Declare a itextSharp document 
            //Document document = new Document(PageSize.A4);
            //Create our file stream and bind the writer to the document and the stream 
            //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"D:\Test.Pdf", FileMode.Create));

            //Open the document for writing 
            //document.Open();

            //Add a new page 
            //document.NewPage();

            //Reference a Unicode font to be sure that the symbols are present. 
            BaseFont bfArialUniCode = BaseFont.CreateFont(@"D:\ARIALUNI.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            //Create a font from the base font 
            Font font = new Font(bfArialUniCode, 12);

            //Use a table so that we can set the text direction 
            PdfPTable table = new PdfPTable(1);
            //Ensure that wrapping is on, otherwise Right to Left text will not display 
            table.DefaultCell.NoWrap = false;

            //Create a regex expression to detect hebrew or arabic code points 
            const string regex_match_arabic_hebrew = @"[\u0600-\u06FF,\u0590-\u05FF]+";
            if (Regex.IsMatch("م الموافق", regex_match_arabic_hebrew, RegexOptions.IgnoreCase))
            {
                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            }

            //Create a cell and add text to it 
            PdfPCell text = new PdfPCell(new Phrase(" : " + "من قبل وبين" + " 2007 " + "م الموافق" + " dsdsdsdsds " + "تم إبرام هذا العقد في هذا اليوم ", font));
            //Ensure that wrapping is on, otherwise Right to Left text will not display 
            text.NoWrap = false;

            //Add the cell to the table 
            table.AddCell(text);

            //Add the table to the document 
            document.Add(table);

            //PdfFont f = PdfFontFactory.createFont(FONT, PdfEncodings.IDENTITY_H);

            //*********************************************************************
            
            //Launch the document if you have a file association set for PDF's 
            //Process AcrobatReader = new Process();
            //AcrobatReader.StartInfo.FileName = @"D:\Test.Pdf";
            //AcrobatReader.Start();
            */


            document.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
