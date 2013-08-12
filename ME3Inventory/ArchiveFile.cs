using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ME3Inventory
{
    public class ArchiveFile
    {
        public String FilePath { get; private set; }

        public ArchiveFile(String path)
        {
            FilePath = path;
        }

        public override string ToString()
        {
            return Regex.Replace(Path.GetFileNameWithoutExtension(FilePath), ".*?([0-9]{4})-([0-9]{2})-([0-9]{2})-([0-9]{2})-([0-9]{2})-([0-9]{2})", "$2/$3/$1 $4:$5:$6");
        }
    }
}
