using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsoleApp
{
    internal class Metadata
    {
        public Metadata(List<MetadataFile> metaData)
        {
            MetaData = metaData;
        }

        public List<MetadataFile> MetaData { get; set; }
    }
}
