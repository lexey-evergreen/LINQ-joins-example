using System.Collections.Generic;

namespace LinqJoinExamples
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
