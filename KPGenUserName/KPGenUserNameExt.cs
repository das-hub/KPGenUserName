using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Core;
using Extensions;
using KeePass.Forms;
using KeePass.Plugins;
using KeePass.UI;

namespace KPGenUserName
{
    public class KPGenUserNameExt : Plugin
    {
        private IPluginHost _host;

        public override bool Initialize(IPluginHost host)
        {
            _host = host;

            ToolStripMenuItem menu = _host.AddToolsMenu(GetType().Name);

            foreach (ToolStripMenuItem item in menu.DropDownItems)
            {
                item.Click += CheckedChanged;

                if (item.Checked) _host.CustomConfig.SetCurrentTransliterationName(item.Name);
            }

            GlobalWindowManager.WindowAdded += WindowAdded;

            return true;
        }

        public override void Terminate()
        {
            _host.RemoveToolsMenu(GetType().Name);

            GlobalWindowManager.WindowAdded -= WindowAdded;
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;

            _host.CustomConfig.SetCurrentTransliterationName(currentItem?.Name);

            var  items = ((ToolStripMenuItem)currentItem.OwnerItem).DropDownItems.OfType<ToolStripMenuItem>().ToList();
            items.ForEach(item =>
            {
                item.Checked = false;
            });

            currentItem.Checked = true;
        }

        private void WindowAdded(object sender, GwmWindowEventArgs e)
        {
            if (e.Form is PwEntryForm form && form.EditModeEx == PwEditMode.AddNewEntry)
            {
                Transliteration currentTransliteration = Transliterations.Get(_host.CustomConfig.GetCurrentTransliterationName());

                if (currentTransliteration == null) return;

                e.Form.Shown += delegate(object o, EventArgs args)
                {
                    TextBox tbTitle = e.Form.Controls.Find("m_tbTitle", true).FirstOrDefault() as TextBox;
                    TextBox tbUserName = e.Form.Controls.Find("m_tbUserName", true).FirstOrDefault() as TextBox;

                    tbTitle.LostFocus += delegate(object s, EventArgs eventArgs)
                    {
                        string title = currentTransliteration.Front(tbTitle.Text.ToShortTitle());

                        string userName = tbUserName.Text;

                        if (string.IsNullOrEmpty(userName))
                           tbUserName.Text = title;
                        else if (userName.Contains('\\'))
                        {
                            string[] parts = userName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                            tbUserName.Text = $"{parts[0]}\\{title}";
                        }
                    };
                };
            }
        }

    }
}
