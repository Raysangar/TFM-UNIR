using UnityEngine;

namespace Game.Gameplay.Units
{
    public abstract class BaseUnit : MonoBehaviour
    {
        public WeaponsSystem.Weapon Weapon { get; private set; }
        public Life Life { get; private set; }
        public Movement Movement { get; private set; }

        [SerializeField] protected Transform projectilePosReference;
        [SerializeField] protected BaseUnitSettings settings;

        protected virtual void Awake()
        {
            Weapon = gameObject.AddComponent<WeaponsSystem.Weapon>();
            Weapon.Init(projectilePosReference, settings.WeaponSettings);

            Life = gameObject.AddComponent<Life>();
            Life.Init(settings.MaxLife);

            Movement = gameObject.AddComponent<Movement>();
            Movement.Init(settings.Speed);
        }
    }
}
