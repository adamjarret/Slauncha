using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Windows.Forms;

namespace Slauncha
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;
        NotifyIcon notifyIcon;
        ContextMenu contextMenu1;
        MenuItem menuItem1;
        MenuItem optionsMenuItem;
        System.ComponentModel.IContainer components;
        Form gameForm;
        FormBorderStyle defaultBorderStyle;

        #region Properties


        /// <summary>
        /// Gets or sets the path of this menu entry.
        /// </summary>
        public Form GameForm
        {
            get { return gameForm; }
        }


        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.gameForm = (Form)Form.FromHandle(this.Window.Handle);
            this.defaultBorderStyle = this.gameForm.FormBorderStyle;

            this.SetBorderStyle(null, null);
            UserControl1.ToggleMenuWindowControlButtons += SetBorderStyle;

            this.CreateNotifyicon();

            graphics.PreferredBackBufferWidth = 400;
            graphics.PreferredBackBufferHeight = 500;

            // Create the screen manager component.
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            // Activate the first screens.
            screenManager.AddScreen(new GamesMenuScreen(), null);
        }

        private void SetBorderStyle(object sender, EventArgs args)
        {
            if (Properties.Settings.Default.HideMenuControlButtons)
                this.gameForm.FormBorderStyle = FormBorderStyle.None;
            else
                this.gameForm.FormBorderStyle = this.defaultBorderStyle;
        }

        private void CreateNotifyicon()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.optionsMenuItem = new System.Windows.Forms.MenuItem();

            // Initialize menuItem1
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "E&xit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);

            // Initialize optionsMenuItem
            this.optionsMenuItem.Index = 1;
            this.optionsMenuItem.Text = "C&onfigure";
            this.optionsMenuItem.Click += new System.EventHandler(this.optionsMenuItem_Click);

            // Initialize contextMenu1
            this.contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { this.optionsMenuItem, this.menuItem1 });

            // Create the NotifyIcon.
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            this.notifyIcon.Icon = new System.Drawing.Icon("Game.ico");

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            this.notifyIcon.ContextMenu = this.contextMenu1;

            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            this.notifyIcon.Text = "Slauncha";
            this.notifyIcon.Visible = true;

            // Handle the DoubleClick event to activate the form.
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
        }

        private void notifyIcon_Click(object Sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void menuItem1_Click(object Sender, EventArgs e)
        {
            this.Exit();
        }

        private void optionsMenuItem_Click(object Sender, EventArgs e)
        {
            Form1 configureForm = new Form1();
            configureForm.Show();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            this.notifyIcon.Visible = false;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!FullscreenCheck.IsAnotherApplicationRunningFullScreen())
            {
                Microsoft.Xna.Framework.Input.ButtonState buttonPressed = Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                GamePadButtons buttons = GamePad.GetState(PlayerIndex.One).Buttons;

                if (buttons.Back == buttonPressed)
                    this.SendToBack();
                if (buttons.Start == buttonPressed)
                    this.BringToFront();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        public void BringToFront()
        {
            this.gameForm.Show();
            this.gameForm.Activate();
        }

        public void SendToBack()
        {
            this.gameForm.Hide();
        }
    }
}
