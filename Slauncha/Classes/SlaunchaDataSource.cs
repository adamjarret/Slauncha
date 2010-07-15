#region File Description
//-----------------------------------------------------------------------------
// SlaunchaDataSource.cs
//
// Slauncha
// Adam Jarret (adamjarret.com)
//-----------------------------------------------------------------------------
#endregion

using System;
using System.IO;

namespace Slauncha
{
    /// <summary>
    /// SlaunchaDataSource is the model for the saved games list
    /// </summary>
    public delegate void EventHandler(object sender, EventArgs e);

    public static class SlaunchaDataSource
    {
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Slauncha\\";

        public static event System.EventHandler Changed;

        public static string[] shortcutFileList()
        {
            string[] fileNames = null;

            try
            {
                Directory.CreateDirectory(SlaunchaDataSource.path); //does nothing if dir exists
                fileNames = Directory.GetFiles(SlaunchaDataSource.path, "*.url");
            }
            catch (Exception e)
            {
                Logger.Log("Error getting file list: {0}", e.Message);
            }

            return fileNames;
        }

        public static void addShortcut(string shortcutPath)
        {
            try
            {
                Directory.CreateDirectory(SlaunchaDataSource.path); //does nothing if dir exists
                File.Copy(shortcutPath, SlaunchaDataSource.path + System.IO.Path.GetFileName(shortcutPath));
                SlaunchaDataSource.Changed(null, null);
            }
            catch(Exception e)
            {
                Logger.Log("Error adding shortcut: {0}", e.Message);
            }
        }

        public static void removeShortcut(string shortcutPath)
        {
            try
            {
                Directory.CreateDirectory(SlaunchaDataSource.path); //does nothing if dir exists
                File.Delete(shortcutPath);
                SlaunchaDataSource.Changed(null, null);
            }
            catch (Exception e)
            {
                Logger.Log("Error removing shortcut: {0}", e.Message);
            }
        }
    }
}
