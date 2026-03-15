using Commons;
using Commons.Servers;
using Godot;
using R34.BooruDownloader.UI;
using Rule34downloadergame.Commons;
using Rule34downloadergame.Commons.Servers;
using System;
using System.Collections.Generic;

namespace R34.BooruDownloader
{
    public partial class Settings : Control
    {
        [Export]
        private SettingsText _savePathText;
        [Export]
        private SettingsText _currentApi;
        [Export]
        private SettingsText _BlackList;
        [Export]
        public CheckBox IgnoreVideo { get; private set; }
        [Export]
        public CheckBox CheckDuplicates { get; private set; }
        [Export]
        private OptionButton _serverOptions;
        public event Action OnServerSelected;
        public int SelectedServerIndex => _serverOptions.Selected;
        public override void _Ready()
        {
            SaveSystem.Instance.Load();
            _savePathText.Text = SaveSystem.Instance.saveData.SavePath;
            IgnoreVideo.ButtonPressed = SaveSystem.Instance.saveData.IgnoreVideos;
            CheckDuplicates.ButtonPressed = SaveSystem.Instance.saveData.CheckDuplicates;
            _serverOptions.Selected = SaveSystem.Instance.saveData.SelectedOptionsIndex;
            _BlackList.Text = string.Join(' ', SaveSystem.Instance.saveData.BlackList);
            if (SaveSystem.Instance.saveData.ApiKeys.Count > 0)
            {
                _currentApi.Text = SaveSystem.Instance.saveData.ApiKeys.Find(x => x.ServerName == _serverOptions.GetItemText(_serverOptions.Selected)).ApiKey;
            }
            else
            {
                _currentApi.Text = "";
            }
            Shareware.Instance.SetSettings(this);
            Shareware.Instance.UpdateData();
        }
        public void OnServerOptionsItemSelected(int index)
        {
            _currentApi.Text = SaveSystem.Instance.saveData.ApiKeys.Find(x => x.ServerName == _serverOptions.GetItemText(index)).ApiKey ?? "";
            OnServerSelected?.Invoke();
        }
        public void OnSaveButtonButtonDown()
        {
            IApikeys apikey;
            SaveSystem.Instance.SetPath(_savePathText.Text);
            SaveSystem.Instance.saveData.IgnoreVideos = IgnoreVideo.ButtonPressed;
            SaveSystem.Instance.saveData.CheckDuplicates = CheckDuplicates.ButtonPressed;
            SaveSystem.Instance.saveData.SelectedOptionsIndex = _serverOptions.Selected;
            SaveSystem.Instance.saveData.BlackList = new List<string>(_BlackList.Text.Split(' '));
            if(_serverOptions.Selected == 0)
            {
                apikey = new Rule34();
                apikey.ApiKey = _currentApi.Text;
                SaveSystem.Instance.SetApi(apikey);
            }
            else if(_serverOptions.Selected == 1)
            {
                apikey = new Gelbooru();
                apikey.ApiKey = _currentApi.Text;
                SaveSystem.Instance.SetApi(apikey);
            }
            SaveSystem.Instance.Save();
        }
    }
}
