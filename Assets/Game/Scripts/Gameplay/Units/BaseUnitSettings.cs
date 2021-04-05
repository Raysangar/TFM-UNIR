using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Units
{
    public abstract class BaseUnitSettings : ScriptableObject
    {
        public float Speed;
        public int MaxLife;
        public WeaponsSystem.WeaponSettings WeaponSettings;
    }
}
