using Godot;
using System;

namespace Core
{
    public partial class Settings : Node
    {
        public static Settings Instance { get; private set; }

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
