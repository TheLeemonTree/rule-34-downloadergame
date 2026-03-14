using Godot;
using System;

namespace Commons
{
    public partial class CommonData : Node
    {
        public static CommonData Instance { get; private set; }

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
