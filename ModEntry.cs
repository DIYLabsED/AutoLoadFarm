using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutoLoadFarmMod
{
    public class ModEntry : Mod
    {
        private ModConfig Config;
        private bool loaded = false;

        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (loaded || !Context.IsMainPlayer)
                return;

            if (!Context.IsWorldReady)
            {
                Monitor.Log($"Loading save: {Config.SaveName}", LogLevel.Info);

                Game1.LoadGame(Config.SaveName);

                if (Config.HostMultiplayer)
                {
                    Game1.multiplayerMode = 2; // host mode
                }

                loaded = true;
            }
        }
    }
}