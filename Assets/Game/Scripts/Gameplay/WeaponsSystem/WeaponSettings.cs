using UnityEngine;

namespace Game.Gameplay.WeaponsSystem
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "Game/Gameplay/Weapon Settings")]
    public class WeaponSettings : ScriptableObject
    {
        public bool InfiniteAmmo;
        public int ClipSize;
        public bool InfiniteTankSize;
        public int TankSize;
        public float ProjectilePeriod;
        public float TimeForFirstProjectile;
        public AmmoSettings[] Ammo;
        public ProjectileSettingsForTankState[] ProjectilesSettingsForTankState;

        [System.Serializable]
        public class AmmoSettings
        {
            public AmmoType Type;
            public Projectile ProjectilePrefab;
            public Color Color;
        }

        [System.Serializable]
        public class ProjectileSettings
        {
            public float Speed;
            public float StartFallingAfterDistance;
            public float FallSpeed;
            public int Damage;
        }

        [System.Serializable]
        public class ProjectileSettingsForTankState
        {
            [SerializeField, Range(0, 100)] float applyFromTankLeftPercentage;
            [SerializeField, Range(0, 100)] float applyUntilTankLeftPercentage;

            public ProjectileSettings Settings;

            public bool MeetsRequirements(float tankLeftPercentage)
            {
                return applyFromTankLeftPercentage <= tankLeftPercentage
                    && tankLeftPercentage <= applyUntilTankLeftPercentage;
            }
        }

        public ProjectileSettings GetCurrentProjectileSettings(int currentGas, int tankSize)
        {
            float tankLeftPercetange = ((float)currentGas / tankSize) * 100;
            int i = 0;
            while (!ProjectilesSettingsForTankState[i].MeetsRequirements(tankLeftPercetange))
                ++i;
            return ProjectilesSettingsForTankState[i].Settings;
        }
    }
}
