using Godot;
using System;
using System.Text.Json;
using System.Collections.Generic;
using Rule34downloadergame.Commons;

namespace Commons
{
    public partial class SaveSystem : Node
    {
        public static SaveSystem Instance { get; private set; }

        public SaveData saveData { get; private set; } = new SaveData();

        public void Save()
        {
            using var userData = FileAccess.Open("user://myData.save", FileAccess.ModeFlags.Write);
            string serializedData = JsonSerializer.Serialize(saveData);
            userData.StoreString(serializedData);
            userData.Close();
        }
        public void Load()
        {
            if (!FileAccess.FileExists("user://myData.save")) return;
            using var userData = FileAccess.Open("user://myData.save", FileAccess.ModeFlags.Read);
            var jsonStringfied = userData.GetAsText();
            var deserializedData = JsonSerializer.Deserialize<SaveData>(jsonStringfied);
            if (deserializedData != null)
            {
                saveData = deserializedData;
            }
            userData.Close();
            GD.Print(saveData.ToString());


        }
        public void SetApi(IApikeys api)
        {
            saveData.ApiKeys.Add(api);
        }
        public void SetPath(string path)
        {
            saveData.SavePath = path;
        }

        public int? GetValue(string key)
        {
            return null;
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