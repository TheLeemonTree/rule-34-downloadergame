using Commons;
using Commons.Servers;
using Godot;
using R34.BooruDownloader.UI;
using Rule34downloadergame.Commons;
using Rule34downloadergame.Commons.Servers;
using System;

namespace R34.BooruDownloader
{
    public partial class ServerPage : Control
    {
        [Export]
        private OptionButton Options;
        [Export]
        private SettingsText _apiText;
        private IApikeys _currentServer;
        public override void _Ready()
        {
            if(Options.Selected == 0)
            {
                _currentServer = new Rule34();
                _currentServer.ApiKey = SaveSystem.Instance.saveData.ApiKeys.Find(x => x.ServerName == "Rule34").ServerName;
                _apiText.Text = _currentServer.ApiKey;
            }
        }
        public void OnOptionButtonItemFocused(int index)
        {
            if (Options.GetItemText(index) == "Rule34")
            {
                _currentServer = new Rule34();
                _currentServer.ApiKey = SaveSystem.Instance.saveData.ApiKeys.Find(x => x.ServerName == "Rule34").ServerName;
                _apiText.Text = _currentServer.ApiKey;
            }
            else if (Options.GetItemText(index) == "Gelbooru")
            {
                _currentServer = new Gelbooru();
                _currentServer.ApiKey = SaveSystem.Instance.saveData.ApiKeys.Find(x => x.ServerName == "Gelbooru").ServerName;
                _apiText.Text = _currentServer.ApiKey;
            }
        }
        public void OnSaveButtonButtonDown()
        {
            SaveSystem.Instance.SetApi(_currentServer);
            SaveSystem.Instance.Save();
        }
    }
}
