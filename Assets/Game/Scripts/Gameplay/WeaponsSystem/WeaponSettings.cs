using UnityEngine;

namespace Game.Gameplay.WeaponsSystem
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "Game/Gameplay/Weapon")]
    public class WeaponSettings : ScriptableObject
    {
        public class AmmoSettings
        {
            public GameObject ProjectilePrefab;
        }

        public int ClipSize;
        public AmmoSettings[] AmmoSettins;
        public float ProjectileFrequency;
    }
}
