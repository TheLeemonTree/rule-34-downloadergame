using Godot;
using System;

namespace R34.BooruDownloader
{
    public partial class Donate : Control
    {
        public void OnTextureButtonButtonDown()
        {
            OS.ShellOpen("https://github.com/TheLeemonTree/rule-34-downloadergame");
        }
    }
}
