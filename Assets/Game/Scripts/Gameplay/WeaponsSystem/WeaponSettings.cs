using UnityEngine;

namespace Game.Gameplay.WeaponsSystem
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "Game/Gameplay/Weapon Settings")]
    public class WeaponSettings : ScriptableObject
    {
        [System.Serializable]
        public class AmmoSettings
        {
            public AmmoType Type;
            public Projectile ProjectilePrefab;
            public Color Color;
        }

        public bool InfiniteAmmo;
        public int ClipSize;
        public bool InfiniteTankSize;
        public int TankSize;
        public float ProjectileSpeed;
        public float ProjectilePeriod;
        public int Damage;
        public AmmoSettings[] Ammo;
    }
}
