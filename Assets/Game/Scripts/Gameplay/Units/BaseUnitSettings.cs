using UnityEngine;
using Game.Gameplay.WeaponsSystem;

namespace Game.Gameplay.Units
{
    public abstract class BaseUnitSettings : ScriptableObject
    {
        public float Speed;
        public float SpeedWhileShooting;
        public int MaxLife;
        public WeaponSettings WeaponSettings;
    }
}
