using UnityEngine;
using Core.EntitySystem;

namespace Game.Gameplay.Units
{
    public abstract class BaseUnit<T> : Entity where T : BaseUnitSettings
    {
        public WeaponsSystem.Weapon Weapon { get; private set; }
        public Life Life { get; private set; }
        public Movement Movement { get; private set; }

        [SerializeField] protected Animator animator;
        [SerializeField] protected Transform projectilePosReference;
        [SerializeField] protected T settings;

        protected readonly int WalkAnimationId = Animator.StringToHash("walking");
        protected readonly int ShootAnimationId = Animator.StringToHash("shooting");

        protected override void Awake()
        {
            base.Awake();
            Weapon = new WeaponsSystem.Weapon(this, projectilePosReference, settings.WeaponSettings);
            Life = new Life(this, settings.MaxLife, settings.AmmoWeakness);
            Movement = new Movement(this, settings.Speed, settings.SpeedWhileShooting, Weapon);
        }

    }
}
