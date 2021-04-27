using UnityEngine;

namespace Game.Gameplay.Units
{
    public abstract class BaseUnit<T> : MonoBehaviour where T : BaseUnitSettings
    {
        public WeaponsSystem.Weapon Weapon { get; private set; }
        public Life Life { get; private set; }
        public Movement Movement { get; private set; }

        [SerializeField] protected Transform projectilePosReference;
        [SerializeField] protected T settings;

        protected virtual void Awake()
        {
            Weapon = gameObject.AddComponent<WeaponsSystem.Weapon>();
            Weapon.Init(projectilePosReference, settings.WeaponSettings);

            Life = gameObject.AddComponent<Life>();
            Life.Init(settings.MaxLife, settings.AmmoWeakness);

            Movement = gameObject.AddComponent<Movement>();
            Movement.Init(settings.Speed);
        }
    }
}
