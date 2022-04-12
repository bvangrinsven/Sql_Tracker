using Microsoft.Extensions.Logging;
using NStack;
using Sql_Tracker.Engine.Interfaces;
using Sql_Tracker.Engine.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Sql_Tracker.Engine.Process
{
    public class ServerWizard : IServerWizard
    {
        private ILogger<ServerWizard> log;
        private ISettings Setting;
        private IDBExecute _db;

        private List<Server> serverList = new List<Server>();

        public ServerWizard(ILogger<ServerWizard> logger, ISettings settings, IDBExecute dB)
        {
            log = logger;
            Setting = settings;
            _db = dB;
        }

        public delegate MenuItem MenuItemDelegate(MenuItemDetails menuItem);

        private Action running;

        public void Execute()
        {



            Application.UseSystemConsole = true;

            Console.OutputEncoding = System.Text.Encoding.Default;

            running = MainApp;

            while (running != null)
            {
                running.Invoke();
            }
            Application.Shutdown();

        }

        private void PopulateServerList()
        {

        }


        private Label ml;
        private Label ml2;
        private MenuBar menu;
        private CheckBox menuKeysStyle;
        private CheckBox menuAutoMouseNav;
        private void MainApp()
        {
            if (Debugger.IsAttached)
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            Application.Init();
            Application.HeightAsBuffer = true;
            //ConsoleDriver.Diagnostics = ConsoleDriver.DiagnosticFlags.FramePadding | ConsoleDriver.DiagnosticFlags.FrameRuler;

            var top = Application.Top;
            var tframe = top.Frame;

            var win = new Window(new Rect(0, 1, tframe.Width, tframe.Height - 2), "Server List");

            menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_New", "Creates new Server to Connect to", NewServer, null, null, Key.AltMask | Key.CtrlMask| Key.N),
                    new MenuItem ("_Open", "", Open, null, null, Key.AltMask | Key.CtrlMask| Key.O),
                    new MenuItem ("_Close", "", Close, null, null, Key.AltMask | Key.Q),
                    null,
                    new MenuItem ("_Quit", "", () => { if (Quit ()) { running = null; top.Running = false; } }, null, null, Key.CtrlMask | Key.Q)
                }),
                new MenuBarItem ("_About...", "Demonstrates top-level menu item", () =>  MessageBox.ErrorQuery (50, 7, "About Demo", "This is a demo app for gui.cs", "Ok")),
            });

            menuKeysStyle = new CheckBox(3, 25, "UseKeysUpDownAsKeysLeftRight", true);
            menuKeysStyle.Toggled += MenuKeysStyle_Toggled;
            menuAutoMouseNav = new CheckBox(40, 25, "UseMenuAutoNavigation", true);
            menuAutoMouseNav.Toggled += MenuAutoMouseNav_Toggled;

            ShowServerList(win);

            ml = new Label(new Rect(0, 0, 47, 1), "Mouse: ");
            Application.RootMouseEvent += delegate (MouseEvent me)
            {
                ml.Text = $"Mouse: ({me.X},{me.Y}) - {me.Flags}";
            };

            win.Add(ml);

            var statusBar = new StatusBar(new StatusItem[] {
                new StatusItem(Key.F1, "~F1~ Help", () => Help()),
                new StatusItem(Key.CtrlMask | Key.Q, "~^Q~ Quit", () => { if (Quit ()) { running = null; top.Running = false; } })
            });

            win.KeyPress += Win_KeyPress;

            top.Add(win);
            top.Add(menu, statusBar);
            Application.Run(top);
        }


        void ShowServerList(View container)
        {
            var ok = new Button("Ok", is_default: true);
            ok.Clicked += () => { Application.RequestStop(); };

            container.Add(
                new ListView(new Rect(1, 1, 50, 10), serverList), 
                ok
             );
        }

        void NewServer()
        {
            Application.Init();

            var ntop = Application.Top;

            var text = new TextView() { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

            var win = new Window("Untitled")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            ntop.Add(win);

            win.Add(text);

            void Paste()
            {
                if (text != null)
                {
                    text.Paste();
                }
            }

            void Cut()
            {
                if (text != null)
                {
                    text.Cut();
                }
            }

            void Copy()
            {
                if (text != null)
                {
                    text.Copy();
                }
            }

            var menu = new MenuBar(new MenuBarItem[] {
                    new MenuBarItem ("_File", new MenuItem [] {
                        new MenuItem ("_Close", "", () => { if (Quit ()) { running = MainApp; Application.RequestStop (); } }, null, null, Key.AltMask | Key.Q),
                    }),
                    new MenuBarItem ("_Edit", new MenuItem [] {
                        new MenuItem ("_Copy", "", Copy, null, null, Key.C | Key.CtrlMask),
                        new MenuItem ("C_ut", "", Cut, null, null, Key.X | Key.CtrlMask),
                        new MenuItem ("_Paste", "", Paste, null, null, Key.Y | Key.CtrlMask)
                    }),
                });
            ntop.Add(menu);

            Application.Run(ntop);
        }

        bool Quit()
        {
            var n = MessageBox.Query(50, 7, "Quit Demo", "Are you sure you want to quit this demo?", "Yes", "No");
            return n == 0;
        }

        void Close()
        {
            MessageBox.ErrorQuery(50, 7, "Error", "There is nothing to close", "Ok");
        }

        void Help()
        {
            MessageBox.Query(50, 7, "Help", "This is a small help\nBe kind.", "Ok");
        }

        void Load()
        {
            MessageBox.Query(50, 7, "Load", "This is a small load\nBe kind.", "Ok");
        }

        void Save()
        {
            MessageBox.Query(50, 7, "Save", "This is a small save\nBe kind.", "Ok");
        }

        // Watch what happens when I try to introduce a newline after the first open brace
        // it introduces a new brace instead, and does not indent.  Then watch me fight
        // the editor as more oddities happen.

        private void Open()
        {
            var d = new OpenDialog("Open", "Open a file") { AllowsMultipleSelection = true };
            Application.Run(d);

            if (!d.Canceled)
                MessageBox.Query(50, 7, "Selected File", d.FilePaths.Count > 0 ? string.Join(", ", d.FilePaths) : d.FilePath, "Ok");
        }


        #region Helpers

        public class MenuItemDetails : MenuItem
        {
            ustring title;
            string help;
            Action action;

            public MenuItemDetails(ustring title, string help, Action action) : base(title, help, action)
            {
                this.title = title;
                this.help = help;
                this.action = action;
            }

            public static MenuItemDetails Instance(MenuItem mi)
            {
                return (MenuItemDetails)mi.GetMenuItem();
            }
        }

        public void ShowMenuItem(MenuItem mi)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Static;
            MethodInfo minfo = typeof(MenuItemDetails).GetMethod("Instance", flags);
            MenuItemDelegate mid = (MenuItemDelegate)Delegate.CreateDelegate(typeof(MenuItemDelegate), minfo);
            MessageBox.Query(70, 7, mi.Title.ToString(),
                $"{mi.Title.ToString()} selected. Is from submenu: {mi.GetMenuBarItem()}", "Ok");
        }

        private void Win_KeyPress(View.KeyEventEventArgs e)
        {
            switch (ShortcutHelper.GetModifiersKey(e.KeyEvent))
            {
                case Key.CtrlMask | Key.T:
                    if (menu.IsMenuOpen)
                        menu.CloseMenu();
                    else
                        menu.OpenMenu();
                    e.Handled = true;
                    break;
            }
        }

        void MenuKeysStyle_Toggled(bool e)
        {
            menu.UseKeysUpDownAsKeysLeftRight = menuKeysStyle.Checked;
        }

        void MenuAutoMouseNav_Toggled(bool e)
        {
            menu.WantMousePositionReports = menuAutoMouseNav.Checked;
        }

        #endregion

    }
}
