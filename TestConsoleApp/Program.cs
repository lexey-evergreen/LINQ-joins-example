using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestConsoleApp
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

            var existInBoth = Enumerable.Join(entry, manifest.MetaData, entry => entry.Name, manifest => manifest.FileName, (entry, manifest) => manifest);

            foreach (var metadata in existInBoth)
            {
                Console.WriteLine($"{metadata.FileName} exist in both.");
            }

            // Left outer join 1
            var doesntExistInManifest = Enumerable
                .GroupJoin(
                entry,
                manifest.MetaData,
                entry => entry.Name,
                manifest => manifest.FileName,
                (entry, manifest) => { if (manifest.SingleOrDefault() == null) { return entry; } else { return null; } })
                .Where(_ => _ != null);

            foreach (var metadataAndManifest in doesntExistInManifest)
            {
                Console.WriteLine($"{metadataAndManifest.Name} is a ZipFileEntry that doesn't exist in manifest.");
            }

            // Left outer join 2
            var doesntExistInZipFile = Enumerable
                .GroupJoin(
                manifest.MetaData,
                entry,
                manifest => manifest.FileName,
                entry => entry.Name,
                (manifest, entry) => { if (entry.SingleOrDefault() == null) { return manifest; } else { return null; } })
                .Where(_ => _ != null);

            foreach (var metadataAndManifest in doesntExistInZipFile)
            {
                Console.WriteLine($"{metadataAndManifest.FileName} is a MetadataFile that doesn't exist in entry.");
            }
        }
    }
}
