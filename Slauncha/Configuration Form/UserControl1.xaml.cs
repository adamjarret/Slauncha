using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Slauncha
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public static event EventHandler ToggleMenuWindowControlButtons;

        public UserControl1()
        {
            InitializeComponent();

            this.refreshListbox(null, null);
            SlaunchaDataSource.Changed += new System.EventHandler(this.refreshListbox);

            checkBox1.IsChecked = Properties.Settings.Default.HideMenuControlButtons;
        }

        private void refreshListbox(object Sender, EventArgs e)
        {
            listBox1.Items.Clear();

            foreach (string path in SlaunchaDataSource.shortcutFileList())
            {
                listBox1.Items.Add(path);
            }
        }

        private void checkBox1_Toggle(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.HideMenuControlButtons = (bool)checkBox1.IsChecked;
            Properties.Settings.Default.Save();

            ToggleMenuWindowControlButtons(null, null);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Add Game Shortcut"; // Default file name
            dlg.DefaultExt = ".url"; // Default file extension
            dlg.Filter = "Steam Game Shortcuts (.url)|*.url"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                string filename = dlg.FileName;
                SlaunchaDataSource.addShortcut(filename);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SlaunchaDataSource.removeShortcut(listBox1.SelectedItem.ToString());
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
