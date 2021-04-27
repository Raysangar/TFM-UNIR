using UnityEngine;
using Game.Gameplay.WeaponsSystem;

namespace Game.Gameplay.Units
{
    public abstract class BaseUnitSettings : ScriptableObject
    {
        public float Speed;
        public int MaxLife;
        public WeaponSettings WeaponSettings;
        public AmmoType AmmoWeakness;
    }
}
