using Godot;
using System;
using System.Collections.Generic;

namespace Commons
{
    public class SaveData
    {
        public string ApiKey { get; set; } = "";
        public string SavePath { get; set; } = "";
        public List<string> BlackList { get; set; } = new List<string>();

        public override string ToString()
        {
            return $"My api {ApiKey} \n {SavePath}";
        }
    }
}
