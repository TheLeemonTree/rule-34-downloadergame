using Godot;
using Rule34downloadergame.Commons;
using System;
using System.Collections.Generic;

namespace Commons
{
    public class SaveData
    {
        public List<IApikeys> ApiKeys { get; set; } = new List<IApikeys>();
        public string SavePath { get; set; } = "";
        public List<string> BlackList { get; set; } = new List<string>();
        public bool IgnoreVideos { get; set; } = false;
        public bool CheckDuplicates { get; set; } = true;
        public int SelectedOptionsIndex { get; set; } = 0;
        public override string ToString()
        {
            return $"My api {ApiKeys} \n {SavePath}";
        }
    }
}
