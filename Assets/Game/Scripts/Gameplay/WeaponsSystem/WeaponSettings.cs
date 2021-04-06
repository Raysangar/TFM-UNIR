using UnityEngine;

namespace Game.Gameplay.WeaponsSystem
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "Game/Gameplay/Weapon Settings")]
    public class WeaponSettings : ScriptableObject
    {
        [System.Serializable]
        public class AmmoSettings
        {
            public Projectile ProjectilePrefab;
        }

        public bool InfiniteAmmo;
        public int ClipSize;
        public float ProjectileSpeed;
        public float ProjectilePeriod;
        public int Damange;
        public AmmoSettings[] Ammo;
    }
}
