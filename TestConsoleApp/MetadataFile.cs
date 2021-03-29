using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsoleApp
{
    internal class MetadataFile
    {
        public MetadataFile(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; set; }
    }
}
