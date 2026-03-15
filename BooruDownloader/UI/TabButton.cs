using Godot;
using System;
using System.Collections.Generic;

namespace R34.BooruDownloader.UI
{
    public partial class TabButton : Button
    {
        [Export]
        private Control _myTab;
        [Export]
        private Control[] _otherTabs;
        [Export]
        private string _myDescription;
        private List<Button> _otherButtons = new List<Button>();

        public override void _Ready()
        {
            Text = _myDescription;
            foreach (var item in GetParent().GetChildren())
            {
                if(item is Button btn)
                {
                    if (btn != this)
                    {
                        _otherButtons.Add(btn);
                        btn.ButtonDown += DisableThis;
                    }
                }
            }
        }
        public void OnButtonDown()
        {
            _myTab.Visible = true;
            Disabled = true;
            HideOtherTabs();
        }
        private void DisableThis()
        {
            Disabled = false;
        }
        private void HideOtherTabs()
        {
            foreach (Control tab in _otherTabs)
            {
                tab.Visible = false;
            }
        }
    }
}
