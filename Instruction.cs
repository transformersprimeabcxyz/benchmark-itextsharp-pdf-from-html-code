﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace benchmark_itextsharp_pdf_from_html_code
{
    public class Instruction
    {
        public Instruction()
        {
            this.Childreen = new List<Instruction>();
        }

        public InstructionType Type { get; set; }
        public string Text { get; set; }
        public IList<Instruction> Childreen { get; private set; }
    }
}
