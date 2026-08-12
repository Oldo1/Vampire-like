using Assets.Scripts.Configs.Items;

namespace Assets.Scripts.Items.Boosters
{
    public class DamageBooster : Booster
    {
        private readonly DamageBoosterConfig _config;

        public DamageBooster(DamageBoosterConfig config) : base(config)
        {
            _config = config;
        }

        public override void Upgrade()
        {
            GameParameters.IncreaseDamageMultiplier(_config.DamageMultiplierAmount);
        }
    }
}
