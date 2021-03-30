using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MoreLinq;

namespace LinqJoinExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadOnlyCollection<ZipFileEntry> entry = new ReadOnlyCollection<ZipFileEntry>(new List<ZipFileEntry>
            {
                new ZipFileEntry("file1"),
                new ZipFileEntry("file2"),
                new ZipFileEntry("file3"),
                new ZipFileEntry("file4"),
                new ZipFileEntry("file5"),
                new ZipFileEntry("file6"),
            });
            Metadata manifest = new Metadata(new List<MetadataFile>
            {
                new MetadataFile("file0"),
                new MetadataFile("file1"),
                new MetadataFile("file2"),
                new MetadataFile("file3"),
                new MetadataFile("file4"),
                new MetadataFile("file5")
            });

            var existInBoth = entry.Join(manifest.MetaData, entry => entry.Name, manifest => manifest.FileName, (entry, manifest) => manifest);

            foreach (var metadata in existInBoth)
            {
                Console.WriteLine($"{metadata.FileName} exist in both.");
            }

            var metaDataFiles = manifest.MetaData.Select(_ => _.FileName);
            var zipFiles = entry.Select(_ => _.Name);

            var fullOuterJoin = metaDataFiles
                .FullGroupJoin(zipFiles, metaDataFile => metaDataFile, zipFile => zipFile)
                .Where(joined => !joined.First.Any() || !joined.Second.Any())
                .Select(joined => joined.Key);

            foreach (var filename in fullOuterJoin)
            {
                Console.WriteLine($"{filename} is a file that's doesn't exist in one of: manifest or entry.");
            }
        }
    }
}
