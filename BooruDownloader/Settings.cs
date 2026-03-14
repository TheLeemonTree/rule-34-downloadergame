using Commons;
using Godot;
using System;
using System.Collections.Generic;

namespace R34.BooruDownloader
{
    public partial class Settings : Control
    {
        [Export]
        private TextEdit _apiKeyTextEdit;
        [Export]
        private TextEdit _blackListTextEdit;
        [Export]
        private TextEdit _savePathTextEdit;
        public override void _Ready()
        {
            SaveSystem.Instance.Load();
            _apiKeyTextEdit.Text = SaveSystem.Instance.saveData.ApiKey;
            _savePathTextEdit.Text = SaveSystem.Instance.saveData.SavePath;
        }
        public void OnSaveButtonButtonDown()
        {
            GD.Print(SaveSystem.Instance.saveData);
            SaveSystem.Instance.Save();
        }
        public void OnApiTextTextChanged()
        {
            SaveSystem.Instance.SetApi(_apiKeyTextEdit.Text);
        }
        public void OnBlackListTextChanged()
        {
            //todo
        }
        public void OnSavePathTextChanged()
        {
            SaveSystem.Instance.SetPath(_savePathTextEdit.Text);
        }
    }
}
