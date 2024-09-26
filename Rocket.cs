using System.Reflection;
using Dark.Net;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace RocketTaskBar
{
    public partial class Rocket : Form
    {


        public Rocket()
        {
            InitializeComponent();
            //this.AllowTransparency = true;
            //this.BackColor = Color.Lime;
            //this.TransparencyKey = Color.Lime;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            
            //this.ControlBox = false;
            //this.Text = String.Empty;
            this.Opacity = 0.9;
            this.StartPosition = FormStartPosition.Manual;
            Screen screen = Screen.FromControl(this);
            this.Location = new Point((screen.WorkingArea.Width - this.Width) / 2,
                          (screen.WorkingArea.Height - this.Height) - 16);

            DarkNet.Instance.SetWindowThemeForms(this, Theme.Auto);

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
                        if (folder.Filter != null && folder.Filter != "")
                        {
                            string[] filters = folder.Filter.Split("|");
                            if (filters.Length > 0)
                            {
                                foreach (string filter in filters)
                                {
                                    if (Path.GetExtension(file).ToLower() == filter) {
                                        string Filename = Path.GetFileNameWithoutExtension(file);
                                        category.AddJumpListItems(new JumpListLink(file, Filename) { Arguments = "" });
                                    }
                                }
                            }
                        }
                    }

                    jumpList.AddCustomCategories(category);
                }


            }
            
            jumpList.Refresh();
        }

    }
}
