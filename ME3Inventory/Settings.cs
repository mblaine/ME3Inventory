using System;
using System.IO;

namespace ME3Inventory
{
    public class Settings
    {
        public String LastPlatform = "PC";
        public String PCUser = "";
        public String XboxUser = "";
        public String PS3User = "";
        public String WiiUUser = "";

        public Settings()
        {
            String path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ME3Inventory", "settings.txt");
            if (File.Exists(path))
            {
                foreach (String line in File.ReadAllLines(path))
                {
                    if (line.Trim().Length <= 0 || line[line.Length - 1] == '=')
                        continue;
                    String[] pair = line.Split('=');
                    switch (pair[0])
                    {
                        case "platform":
                            LastPlatform = pair[1];
                            break;
                        case "pc":
                            PCUser = pair[1];
                            break;
                        case "xbox":
                            XboxUser = pair[1];
                            break;
                        case "ps3":
                            PS3User = pair[1];
                            break;
                        case "wiiu":
                            WiiUUser = pair[1];
                            break;
                        default:
                            throw new Exception("Unrecognized entry in settings file");
                    }
                }
            }
        }

        public void Write()
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ME3Inventory", "settings.txt"), false))
            {
                sw.WriteLine("platform=" + LastPlatform);
                sw.WriteLine("pc=" + PCUser);
                sw.WriteLine("xbox=" + XboxUser);
                sw.WriteLine("ps3=" + PS3User);
                sw.WriteLine("wiiu=" + WiiUUser);
            }
        }
    }
}
