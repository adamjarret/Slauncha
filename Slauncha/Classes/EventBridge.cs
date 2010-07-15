#region File Description
//-----------------------------------------------------------------------------
// EventBridge.cs
//
// Slauncha
// Adam Jarret (adamjarret.com)
//-----------------------------------------------------------------------------
#endregion

using System;

namespace Slauncha
{
    public static class EventBridge
    {
        public static event EventHandler FileBrowserOpened;
        public static event EventHandler FileBrowserClosed;
     
        public static void TriggerFileBrowserOpened()
        {
            FileBrowserOpened(null, null);
        }

        public static void TriggerFileBrowserClosed()
        {
            FileBrowserClosed(null, null);
        }
    }
}
