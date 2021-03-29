using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsoleApp
{
    internal class ZipFileEntry
    {
        public ZipFileEntry(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
