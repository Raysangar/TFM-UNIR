using UnityEngine;
using Core.EntitySystem;
using Game.Gameplay.Units;

namespace Game.Gameplay.Pickups
{
    public abstract class BasePickup : Entity
    {
        protected abstract void Activate(PlayerController player);

        private void OnTriggerEnter(Collider other)
        {
            Activate(other.GetComponent<PlayerController>());
            RemoveFromScene();
        }
    }
}
