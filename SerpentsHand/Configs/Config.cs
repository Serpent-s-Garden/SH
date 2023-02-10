using Exiled.API.Interfaces;
using SerpentsHand.Configs;
using System.ComponentModel;

namespace SerpentsHand
{
    internal class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        public SerpentsHandModifiers SerpentsHandModifiers { get; set; } = new();

        public SpawnManager SpawnManager { get; set; } = new();
    }
}
