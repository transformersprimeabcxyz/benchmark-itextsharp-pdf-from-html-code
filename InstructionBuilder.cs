using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace benchmark_itextsharp_pdf_from_html_code
{
    public class InstructionBuilder
    {
        #region Fields
        private HtmlNode node;
        #endregion

        #region Fluent Loaders
        public InstructionBuilder WithNode(HtmlNode node) { this.node = node; return this; }
        #endregion

        public Instruction Build()
        {
            this.AssertRequirements();

            return this.MakeInstruction();
        }

        #region Auxiliary

        private Instruction MakeInstruction()
        {
            var i = new Instruction();
            return i;
        }

        private InstructionType ParseType()
        {
            var tag = (this.node.Name ?? string.Empty).ToUpperInvariant().Trim();
            switch (tag)
            {
                case "P":
                default:
                    throw new ArgumentOutOfRangeException(tag);
            }
        }

        private void AssertRequirements()
        {
            if (node == null) { throw new ArgumentNullException("node"); }
        }

        #endregion
    }
}
