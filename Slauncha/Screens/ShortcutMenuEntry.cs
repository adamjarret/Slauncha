#region File Description
//-----------------------------------------------------------------------------
// ShortcutMenuEntry.cs
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
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Slauncha
{
    /// <summary>
    /// Helper class represents a single entry in a MenuScreen. By default this
    /// just draws the entry text string, but it can be customized to display menu
    /// entries in different ways. This also provides an event that will be raised
    /// when the menu entry is selected.
    /// </summary>
    class ShortcutMenuEntry : MenuEntry
    {
        #region Fields

        /// <summary>
        /// The shortcut location to be opened
        /// </summary>
        string path;

        #endregion

        #region Properties


        /// <summary>
        /// Gets or sets the path of this menu entry.
        /// </summary>
        public string ShortcutPath
        {
            get { return path; }
            set { path = value; }
        }


        #endregion

        #region Events
        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new menu entry with the specified text.
        /// </summary>
        public ShortcutMenuEntry(string path) : base(System.IO.Path.GetFileNameWithoutExtension(path))
        {            
            this.path = path;
        }


        #endregion

        #region Update and Draw
        #endregion
    }
}
