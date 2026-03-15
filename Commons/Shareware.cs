using Godot;
using R34.BooruDownloader;
using System;

namespace Commons
{
    public partial class Shareware : Node
    {
        public static Shareware Instance { get; private set; }
        private Settings _settings;
        public string currentApiKey;
        public string currentServerURL;
        public string currentServerTagURL;
        public bool ignoreVideos;
        public bool checkDuplicates;
        public void SetSettings(Settings settings)
        {
            this._settings = settings;
            _settings.OnServerSelected += UpdateData;
            ignoreVideos = _settings.IgnoreVideo.ButtonPressed;
            checkDuplicates = _settings.IgnoreVideo.ButtonPressed;
        }
        public void UpdateData()
        {
            if(_settings.SelectedServerIndex == 0)
            {
                currentApiKey = SaveSystem.Instance.saveData.ApiKeys.Find(x => x.ServerName == "Rule34")?.ApiKey ?? "";
                currentServerURL = "https://api.rule34.xxx//index.php?page=dapi&s=post&q=index&json=1";
                currentServerTagURL = "https://api.rule34.xxx//index.php?page=dapi&s=tag&q=index&json=1";
            }
             else if(_settings.SelectedServerIndex == 1)
            {
                currentApiKey = SaveSystem.Instance.saveData.ApiKeys.Find(x => x.ServerName == "Gelbooru")?.ApiKey ?? "";
                currentServerURL = "https://gelbooru.com/index.php?page=dapi&s=post&q=index&json=1";
                currentServerTagURL = "https://gelbooru.com/index.php?page=dapi&s=tag&q=index&json=1";
            }
            GD.Print($"{currentServerURL} \n {currentApiKey}");
        }
        public override void _Ready()
        {
            if (Instance != null)
            {
                QueueFree();
                return;
            }
            Instance = this;
        }
    }
}
