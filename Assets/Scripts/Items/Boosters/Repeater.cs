using Assets.Scripts.Configs.Items;
using System;

namespace Assets.Scripts.Items.Boosters
{
    public class Repeater : Booster
    {
        private readonly RepeaterConfig _config;

        public Repeater(RepeaterConfig config) : base(config)
        {
            _config = config;
        }

        public override void Upgrade()
        {
            GameParameters.IncreaseProjectileCount(_config.ProjectileAmount);
        }
    }
}
