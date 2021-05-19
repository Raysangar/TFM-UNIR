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

            Weapon = new WeaponsSystem.Weapon(this, projectilePosReference, settings.WeaponSettings);
            enabledComponents.Add(Weapon);

            Life = new Life(this, settings.MaxLife, settings.AmmoWeakness);
            enabledComponents.Add(Life);

            Movement = new Movement(this, settings.Speed, settings.SpeedWhileShooting, Weapon);
            enabledComponents.Add(Movement);
        }

    }
}
