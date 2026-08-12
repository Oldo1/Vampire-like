namespace Assets.Scripts
{
    public static class GameParameters
    {
        public static float DamageMultiplier { get; private set; } = 1f;
        public static float SpeedMultiplier { get; private set; } = 1f;
        public static float ProjectileCount { get; private set; } = 0f;
        public static float CooldownMultiplier { get; private set; } = 1f;


        public static void IncreaseDamageMultiplier(float amount)
        {
            if (amount < 0)
                throw new System.ArgumentException("Amount must be non-negative.", nameof(amount));

            DamageMultiplier += amount;
        } 

        public static void IncreaseSpeedMultiplier(float amount)
        {
            if (amount < 0)
                throw new System.ArgumentException("Amount must be non-negative.", nameof(amount));

            SpeedMultiplier += amount;
        }

        public static void IncreaseProjectileCount(float amount)
        {
            if (amount < 0)
                throw new System.ArgumentException("Amount must be non-negative.", nameof(amount));

            ProjectileCount += amount;
        }

        public static void DecreaseCooldownMultiplier(float amount)
        {
            if (amount < 0)
                throw new System.ArgumentException("Amount must be non-negative.", nameof(amount));

            if (amount > CooldownMultiplier)
                throw new System.ArgumentException("Amount must not be greater than the current CooldownMultiplier.", nameof(amount));

            CooldownMultiplier -= amount;
        }
    }
}
