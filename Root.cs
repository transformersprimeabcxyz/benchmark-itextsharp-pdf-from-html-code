using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace benchmark_itextsharp_pdf_from_html_code
{
    public class Root
    {
        private static readonly string HTML_PATH = @"Data\zengarden_001.html";
        private static readonly string PDF_PATH = @"D:\Temp\benchmark-itextsharp-pdf-from-html-thumb\zengarden_{0}.pdf";
        private static readonly iTextSharp.text.Rectangle PDF_SIZE = iTextSharp.text.PageSize.A4;

        public static void Main(string[] args)
        {
            var html = ReadHtml();
            var instructions = ReadInstructions(html);
            var pdf = GeneratePdf(instructions);
            OutputFile(pdf);
        }

        private static void OutputFile(byte[] pdf)
        {
            var makePath = string.Format(PDF_PATH, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            var outputDir = Path.GetDirectoryName(makePath);
            if (!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);
            using (var stream = new FileStream(makePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                stream.Write(pdf, 0, pdf.Length);
        }

        private static byte[] GeneratePdf(IEnumerable<Instruction> instructions)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<Instruction> ReadInstructions(HtmlNode node)
        {
            var instructions = new List<Instruction>();
            if (node == null) { return instructions; }
            if (node.ChildNodes == null || !node.ChildNodes.Any()) { return instructions; }

            foreach (var child in node.ChildNodes) {
                instructions.Add(new InstructionBuilder()
                    .WithNode(child)
                    .Build());

                instructions.AddRange(
                    ReadInstructions(child));
            }
            return instructions;
        }

        private static IEnumerable<Instruction> ReadInstructions(string content)
        {
            var html = new HtmlDocument();
            html.LoadHtml(content);

            var instructions = new List<Instruction>();
            var document = html.DocumentNode;
            instructions.AddRange(ReadInstructions(document));
            return instructions;
        }

        private static string ReadHtml()
        {
            using (var f = new FileStream(HTML_PATH, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var r = new StreamReader(f)) return r.ReadToEnd();
        }
    }
}
