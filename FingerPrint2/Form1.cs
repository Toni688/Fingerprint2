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

using iTextSharp.text;
using iTextSharp.text.pdf;
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
            document.Add(new Paragraph(lines[1]));
            Paragraph p = new Paragraph("Right Index").Add(rightIndex).Add("Left Index").Add(leftIndex);
            // Add Paragraph to document
            document.Add(p);

            //********************************************************************************************************
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

            PdfFont f = PdfFontFactory.createFont(FONT, PdfEncodings.IDENTITY_H);

            //*********************************************************************
            /*
            //Launch the document if you have a file association set for PDF's 
            Process AcrobatReader = new Process();
            AcrobatReader.StartInfo.FileName = @"D:\Test.Pdf";
            AcrobatReader.Start();
            */

            document.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
