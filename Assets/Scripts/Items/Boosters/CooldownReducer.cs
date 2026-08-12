using Assets.Scripts.Configs.Items;

namespace Assets.Scripts.Items.Boosters
{
    public class CooldownReducer : Booster
    {
        private readonly CooldownReducerConfig _config;

        public CooldownReducer(CooldownReducerConfig config) : base(config)
        {
            _config = config;
        }

        public override void Upgrade()
        {
            GameParameters.DecreaseCooldownMultiplier(_config.СooldownReduceMultiplier);
        }
    }
}
