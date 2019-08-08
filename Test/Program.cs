using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using PdfSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;



namespace Test
{

    class Program
    {

        static void Main(string[] args)
        {
            //Get the files and read them
            Console.WriteLine("Enter White Card File's Path");

            string FfilePath = Console.ReadLine();

            var lines = File.ReadAllLines(FfilePath, Encoding.UTF8);

            Console.WriteLine("Enter White Card Credit File's Path");

            string CfilePath = Console.ReadLine();

            var credits = File.ReadAllLines(CfilePath, Encoding.UTF8);
            if (lines.Length != credits.Length)
            {
                Console.WriteLine("Lines and Credits does not match lenght");
                Console.Read();
            }
            else
            {


                Console.Write("Processing...");

                //Create a font from the base font

                BaseFont bfTnrbUniCode = BaseFont.CreateFont(@"C:\Windows\Fonts\timesbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                //Main font
                iTextSharp.text.Font font1 = new iTextSharp.text.Font(bfTnrbUniCode, 16);

                //Font used for credits
                iTextSharp.text.Font font2 = new iTextSharp.text.Font(bfTnrbUniCode, 8);

                //Create a new document
                Document document = new Document(iTextSharp.text.PageSize.A4);

                //Creates a Writer, which is what generates the file and "opens" it
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"F:\Test.Pdf", FileMode.Create));

                //Open the document for writing 
                document.Open();

                //Add a new page 
                document.NewPage();
                //Add a new table
                PdfPTable table = new PdfPTable(5);
                //Set table's properties here
                table.WidthPercentage = 90;

                table.DefaultCell.NoWrap = false;

                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL; ;
                //Add the cell
                PdfPCell cell = new PdfPCell
                {
                    NoWrap = false
                };

                //The card's image and properties
                iTextSharp.text.Image card = iTextSharp.text.Image.GetInstance("card.png");

                card.Alignment = iTextSharp.text.Image.UNDERLYING;

                //For loop goes through each line/entry in the array and adds it to it's own cell,
                //including adding the credits from the credits array and the card image, then adds all of these into the table as a cell.
                for (var i = 0; i < lines.Length; i++)
                {
                    cell = new PdfPCell();
                    cell.AddElement(new Phrase(lines[i], font1));
                    cell.AddElement(new Phrase(credits[i], font2));
                    cell.AddElement(card);
                    table.AddCell(cell);
                }


                //Add the table to the document
                document.Add(table);

                //Close the document
                document.Close();

                //Finish
                Console.Write(" Saved!");

                Console.Read();
            }
        }
    }
}

