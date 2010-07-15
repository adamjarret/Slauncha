#region File Description
//-----------------------------------------------------------------------------
// FullscreenCheck.cs
//
// Slauncha
// Adam Jarret (adamjarret.com)
//
// Huge thanks to Richard Banks for his article on how to do this
// http://www.richard-banks.org/2007/09/how-to-detect-if-another-application-is.html
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Slauncha
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    static class FullscreenCheck
    {
        #region DllImport

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowRect(IntPtr hwnd, out RECT rc);

        #endregion

        public static bool IsAnotherApplicationRunningFullScreen()
        {
            //Detect if the current app is running in full screen

            //Get the handles for the desktop and shell now.
            IntPtr desktopHandle = GetDesktopWindow();
            IntPtr shellHandle = GetShellWindow();

            bool runningFullScreen = false;
            RECT appBounds;
            int appHeight;
            int appWidth;
            System.Drawing.Rectangle screenBounds;
            IntPtr hWnd;

            //get the dimensions of the active window
            hWnd = GetForegroundWindow();
            if (hWnd != null && !hWnd.Equals(IntPtr.Zero))
            {
                //Check we haven't picked up the desktop or the shell
                if (!(hWnd.Equals(desktopHandle) || hWnd.Equals(shellHandle)))
                {
                    GetWindowRect(hWnd, out appBounds);
                    //determine if window is fullscreen
                    screenBounds = System.Windows.Forms.Screen.FromHandle(hWnd).Bounds;
                    appWidth = (appBounds.Right - appBounds.Left);
                    appHeight = (appBounds.Bottom - appBounds.Top);
                    if (appWidth == (int)screenBounds.Width && appHeight == (int)screenBounds.Height)
                    {
                        runningFullScreen = true;
                    }
                    else
                    {
                        //Logger.Log("Not running full screen b/c app: {0}x{1} != screen: {2}x{3}", appWidth, appHeight, screenBounds.Width, screenBounds.Height);
                    }
                }
                else
                {
                    //Logger.Log("Not running full screen b/c of either desktop or shell");
                }
            }

            return runningFullScreen;
        }
    }
}