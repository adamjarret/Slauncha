#region File Description
//-----------------------------------------------------------------------------
// GamesMenuScreen.cs
//
// Slauncha
// Adam Jarret (adamjarret.com)
//
// Based on code from:
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using Microsoft.Xna.Framework;

using System;
using System.IO;
using System.Windows.Forms;

#endregion

namespace Slauncha
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class GamesMenuScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public GamesMenuScreen()
            : base("Games")
        {
            this.refreshMenuEntries(null, null);
            SlaunchaDataSource.Changed += new System.EventHandler(this.refreshMenuEntries);
        }
        

        #endregion

        private void refreshMenuEntries(object Sender, EventArgs e)
        {
            MenuEntries.Clear();

            // Create our game menu entries.
            ShortcutMenuEntry entry;

            string[] fileNames = SlaunchaDataSource.shortcutFileList();
            if (fileNames != null)
            {
                foreach (string fileName in fileNames)
                {
                    entry = new ShortcutMenuEntry(fileName);
                    entry.Selected += OnLaunch;
                    MenuEntries.Add(entry);
                }
            }

            // Create and add spacer to MenuEntries
            MenuEntry spacerMenuEntry = new MenuEntry(" ");
            spacerMenuEntry.isSpacer = true;
            MenuEntries.Add(spacerMenuEntry);

            // Create and add action to 'Exit' menu item and add to MenuEntries
            MenuEntry closeMenuEntry = new MenuEntry("Hide");
            closeMenuEntry.Selected += OnMinimizeToTray;
            MenuEntries.Add(closeMenuEntry);

            // Create and add action to 'Exit' menu item and add to MenuEntries
            MenuEntry exitMenuEntry = new MenuEntry("Exit");
            exitMenuEntry.Selected += OnCancel;
            MenuEntries.Add(exitMenuEntry);
        }

        #region Handle Input

        /// <summary>
        /// Event handler for when a game menu item is selected.
        /// </summary>
        void OnLaunch(object sender, PlayerIndexEventArgs e)
        {
            // Launch Game via Steam URL
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = (((ShortcutMenuEntry)sender).ShortcutPath);
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.RedirectStandardOutput = false;

            try
            {
                process.Start();
            }
            catch (Exception exception)
            {
                MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(exception.Message + "\n(" + process.StartInfo.FileName + ")");
                confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;
                ScreenManager.AddScreen(confirmExitMessageBox, e.PlayerIndex);                
            }
        }

        /// <summary>
        /// When the user closes the main menu, hide the window.
        /// </summary>
        private void OnMinimizeToTray(object sender, PlayerIndexEventArgs e)
        {
            ((Game1)ScreenManager.Game).SendToBack();
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}
