using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;

namespace RocketTaskBar
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 

        public static Settings? settings;

        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            LoadSettings();
            Application.Run(new Rocket());
        }


        static void LoadSettings()
        {
            string fileName = "RocketTaskBar.json";
            string PathName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), fileName);

            if (File.Exists(PathName)) {
                var json = File.ReadAllText(PathName);
                settings = JsonSerializer.Deserialize<Settings>(json);

            } else
            {
                RocketFolder folder = new()
                {
                    Name = "Exemple",
                    Path = "",
                    Filter = ".rdp"
                };
                settings = new()
                {
                    RocketFolders = [folder]
                };

                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(settings, options);
                File.WriteAllText(PathName, jsonString);
            }



        }


    }

    internal class Settings
    {
        public required RocketFolder[] RocketFolders { get; set; }
    }

    internal class RocketFolder
    {
        public string? Name { get; set; }
        public required string Path { get; set; }
        public string? Filter { get; set; }

    }

}
