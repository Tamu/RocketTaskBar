using System.Reflection;
using Microsoft.WindowsAPICodePack.Taskbar;

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

            JumpListCustomCategory category = new JumpListCustomCategory("Folder 1");
            category.AddJumpListItems(
                new JumpListLink(cmdPath, "Tomato") { Arguments = "Tomato" },
                new JumpListLink(cmdPath, "Prawns") { Arguments = "Prawns" },
                new JumpListLink(cmdPath, "Shrimps") { Arguments = "Shrimps" });

            jumpList.AddCustomCategories(category);

            jumpList.Refresh();
        }

    }
}
