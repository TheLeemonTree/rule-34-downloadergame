using Godot;
using Rule34downloadergame.Commons;
using System;
namespace Commons.Servers
{

    public class Rule34 : IApikeys
    {
        public string ApiKey { get; set; }
        public string ServerName { get; set; } = "Rule34";
    }
}

