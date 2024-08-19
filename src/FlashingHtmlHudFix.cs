using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;

namespace FlashingHtmlHudFix
{
    [MinimumApiVersion(247)]
    public partial class FlashingHtmlHudFix : BasePlugin
    {
        public override string ModuleName => "FlashingHtmlHudFix";
        public override string ModuleVersion => "1.0";
        public override string ModuleAuthor => "Deana https://x.com/dea_bb/";
        public override string ModuleDescription => "A Plugin that fixes Html Hud.";

        private CCSGameRules? _gameRules;

        public override void Load(bool hotReload)
        {
            RegisterListener<Listeners.OnTick>(OnTick);
            RegisterListener<Listeners.OnMapStart>(OnMapStartHandler);
        }

        private void OnMapStartHandler(string mapName)
        {
            _gameRules = null;
        }

        private void InitializeGameRules()
        {
            var gameRulesProxy = Utilities.FindAllEntitiesByDesignerName<CCSGameRulesProxy>("cs_gamerules").FirstOrDefault();
            _gameRules = gameRulesProxy?.GameRules;
        }

        private void OnTick()
        {
            if (_gameRules == null)
            {
                InitializeGameRules();
            }
            else
            {
                _gameRules.GameRestart = _gameRules.RestartRoundTime < Server.CurrentTime;
            }
        }
        //thx for watching make sure to like and subscribe!
    }
}