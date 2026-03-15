using Godot;
using System;

namespace R34.BooruDownloader.UI
{
    [Tool]
    public partial class SettingsText : TextEdit
    {
        [Export]
        private Label _label;
        [Export]
        private string _placeholderText = "Placeholder";
        [Export]
        private string _tittle = "Title";

        public override void _Ready()
        {
            _label.Text = _tittle;
            PlaceholderText = _placeholderText;
            Text = "";
        }
    }
}
