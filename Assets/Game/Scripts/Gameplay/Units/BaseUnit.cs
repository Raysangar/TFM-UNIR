using UnityEngine;
using Core.EntitySystem;

namespace Game.Gameplay.Units
{
    public abstract class BaseUnit<T> : Entity where T : BaseUnitSettings
    {
        public WeaponsSystem.Weapon Weapon { get; private set; }
        public Life Life { get; private set; }
        public Movement Movement { get; private set; }

        [SerializeField] protected Transform projectilePosReference;
        [SerializeField] protected T settings;

        protected override void Awake()
        {
            base.Awake();

            Weapon = new WeaponsSystem.Weapon(projectilePosReference, settings.WeaponSettings);
            components.Add(Weapon);

            Life = new Life(settings.MaxLife, settings.AmmoWeakness);
            components.Add(Life);

            Movement = new Movement(settings.Speed);
            components.Add(Movement);
        }
    }
}
