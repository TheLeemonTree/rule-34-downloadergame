using Godot;
using System;

namespace R34.BooruDownloader
{
    public partial class MainMenu : Control
    {
        [Export]
        private Control _mainPage;
        [Export]
        private Control _settingsPage;
        [Export]
        private Control _donatePage;
        public override void _Ready()
        {
            _mainPage.Visible = true;
            _settingsPage.Visible = false;
            _donatePage.Visible = false;
        }
    }
}
