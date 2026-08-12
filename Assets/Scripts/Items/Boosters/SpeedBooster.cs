using Assets.Scripts.Configs.Items;
using System;

namespace Assets.Scripts.Items.Boosters
{
    public class SpeedBooster : Booster
    {
        private readonly SpeedBoosterConfig _config;

        public SpeedBooster(SpeedBoosterConfig config) : base(config)
        {
            _config = config;
        }

        public override void Upgrade()
        {
            GameParameters.IncreaseSpeedMultiplier(_config.SpeedMultipllierAmount);
        }
    }
}
