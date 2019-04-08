using System.Linq;
using System.Windows.Forms;
using Core;
using KeePass.Plugins;

namespace Extensions
{
    public static class PluginHostEx
    {
        public static ToolStripMenuItem AddToolsMenu(this IPluginHost host, string name)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem("Генерация логина на основе названия"){ Name = name };

            ToolStripMenuItem gost = new ToolStripMenuItem("ГОСТ 16876-71")
            {
                Name = Constants.CommandGostName,
                Checked = host.CustomConfig.GetBool(Constants.CommandGostName, false),
            };
            ToolStripMenuItem iso = new ToolStripMenuItem("ISO 9-95")
            {
                Name = Constants.CommandIsoName,
                Checked = host.CustomConfig.GetBool(Constants.CommandIsoName, false),
            };
            ToolStripMenuItem disable = new ToolStripMenuItem("Выкл.")
            {
                Name = Constants.CommandDisableName,
                Checked = host.CustomConfig.GetBool(Constants.CommandDisableName, true)
            };

            menuItem.DropDownItems.Add(gost);
            menuItem.DropDownItems.Add(iso);
            menuItem.DropDownItems.Add(disable);

            ToolStripItemCollection menu = host.MainWindow.ToolsMenu.DropDownItems;

            menu.Add(menuItem);

            return menuItem;
        }

        public static void RemoveToolsMenu(this IPluginHost host, string name)
        {
            ToolStripItemCollection menu = host.MainWindow.ToolsMenu.DropDownItems;

            ToolStripItem item = menu.Cast<ToolStripItem>().FirstOrDefault(i => i.Name == name);

            if (item != null)
            {
                var items = (item as ToolStripMenuItem).DropDownItems;

                foreach (ToolStripMenuItem i in items) host.CustomConfig.SetBool(i.Name, i.Checked);
                
                menu.Remove(item);
            }
        }

    }
}