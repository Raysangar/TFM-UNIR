using UnityEngine;
using Core.Utils.Pool;
using Game.Gameplay.Units;

namespace Game.Gameplay.Pickups
{
    public abstract class BasePickup : PoolObject
    {
        protected abstract void Activate(PlayerController player);

        private void OnTriggerEnter(Collider other)
        {
            Activate(other.GetComponent<Units.PlayerController>());
            PoolManager.Instance.Release(this);
        }
    }
}
