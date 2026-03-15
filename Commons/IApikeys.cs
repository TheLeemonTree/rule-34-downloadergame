using Commons.Servers;
using R34.BooruDownloader;
using Rule34downloadergame.Commons.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rule34downloadergame.Commons
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Rule34), "Rule34")]
    [JsonDerivedType(typeof(Gelbooru), "Gelbooru")]
    public interface IApikeys
    {
        string ApiKey { get; set; }
        string ServerName { get; set; }
    }
}
