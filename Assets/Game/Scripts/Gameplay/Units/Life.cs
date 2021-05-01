using UnityEngine;
using Core.EntitySystem;

namespace Game.Gameplay.Units
{
    public class Life : EntityComponent
    {
        public System.Action OnDeath;
        public System.Action OnChanged;

        public int Max { get; private set; }
        public int Current { get; private set; }

        private WeaponsSystem.AmmoType ammoWeakness;

        public Life(int maxLife, WeaponsSystem.AmmoType ammoWeakness)
        {
            this.ammoWeakness = ammoWeakness;
            Max = Current = maxLife;
        }

        public void AddDamage(int damage, WeaponsSystem.AmmoType ammoType)
        {
            if (ammoWeakness == ammoType)
                AddDamage(damage);
        }

        public void AddDamage(int damage)
        {
            Current -= damage;
            if (Current <= 0)
            {
                Current = 0;
                OnDeath();
            }
            else
            {
                OnChanged?.Invoke();
            }
        }

        public void AddLife(int amount)
        {
            Current = Mathf.Min(Current + amount, Max);
            OnChanged?.Invoke();
        }
    }
}
