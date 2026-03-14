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
        private Button _mainPageButton;
        [Export]
        private Button _settingsPageButton;

        public override void _Ready()
        {
            _mainPageButton.Disabled = true;
            _settingsPageButton.Disabled = false;
        }
        public void OnMainButtonButtonDown()
        {
            _mainPageButton.Disabled = true;
            _settingsPageButton.Disabled = false;
            _mainPage.Visible = true;
            _settingsPage.Visible = false;
        }
        public void OnSettingsButtonButtonDown()
        {
            _mainPageButton.Disabled = false;
            _settingsPageButton.Disabled = true;
            _mainPage.Visible = false;
            _settingsPage.Visible = true;
        }
    }
}
