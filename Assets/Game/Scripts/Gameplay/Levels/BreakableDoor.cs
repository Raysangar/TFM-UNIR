using UnityEngine;
using Core.EntitySystem;
using Game.Gameplay.Units;

namespace Game.Gameplay
{
    public class BreakableDoor : Entity
    {
        [SerializeField] GameObject door;
        [SerializeField] WeaponsSystem.AmmoType ammoTypeRequiredToBreak;

        private Life life;

        protected override void Awake()
        {
            base.Awake();
            life = new Life(this, 1, ammoTypeRequiredToBreak);
            life.OnDeath += OnDoorBroken;
        }

        private void OnDoorBroken()
        {
            door.SetActive(false);
            RemoveFromScene();
        }
    }
}
