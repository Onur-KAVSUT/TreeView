using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace WpfTreeView
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
       




        public MainWindow()
        {
            InitializeComponent();
         
        }
        #endregion
        #region On Loaded
        /// <summary>
        /// when the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {   
            // get eveery logical drive on the machiine
            foreach (var drive in Directory.GetLogicalDrives())
            {   
                // create a new item for it
                var item = new TreeViewItem();
                //set the header and path
                item.Header = drive;
                item.Tag = drive;
                //add a dummy item
                item.Items.Add(null);
                // listen out for item being expended
                item.Expanded += Folder_Expanded;
                // add it to the main tree-view
                FolderView.Items.Add(item);
            }
        }
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            // If the ıtem onlly contaıns the dumy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;
            // Clear dummy data 
            item.Items.Clear();
            // get full path
            var fullPath = (string)item.Tag;
            // Create a blank list for directories
            var directories = new List<string>();
            // Try and get directories from the folder
            // ignorig any issues dosing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }
            // for each directory....
            directories.ForEach(directoryPath =>
            {
                // create directory ıtem
                var subItem = new TreeViewItem()
                {
                    Header = Path.GetDirectoryName(directoryPath),
                    Tag = directoryPath
                };
                subItem.Items.Add(null);
                subItem.Expanded += Folder_Expanded;
                item.Items.Add(subItem);

            });
        }
        #endregion
    }
}
