using System.Reflection;
using Microsoft.WindowsAPICodePack.Taskbar;
using MS.WindowsAPICodePack.Internal;

namespace RocketTaskBar
{
    public partial class Rocket : Form
    {
        public Rocket()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private void Form1_Shown(Object sender, EventArgs e)
        {
            // get where the app is running from
            string cmdPath = Assembly.GetEntryAssembly().Location;

            // create a jump list
            JumpList jumpList = JumpList.CreateJumpList();

            foreach (var folder in Program.settings.RocketFolders)
            {
                if (folder.Path != null && Directory.Exists(folder.Path))
                {
                    string folderName = "";
                    if (folder.Name == null)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(folder.Path);
                        folderName = dirInfo.Name;
                    } else
                    {
                        folderName = folder.Name;
                    }
                    JumpListCustomCategory category = new JumpListCustomCategory(folderName);

                    foreach (string file in Directory.EnumerateFiles(folder.Path))
                    {
                        if (Path.GetExtension(file).ToLower() == ".rdp") {
                        string Filename = Path.GetFileNameWithoutExtension(file);
                        category.AddJumpListItems(new JumpListLink(file, Filename) { Arguments = "" });
                        }
                    }

                    jumpList.AddCustomCategories(category);
                }


            }
            
            jumpList.Refresh();
        }

    }
}
