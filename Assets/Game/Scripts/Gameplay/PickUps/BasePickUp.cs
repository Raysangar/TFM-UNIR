using UnityEngine;
using Core.Utils.Pool;

namespace Game.Gameplay.PickUps
{
    public abstract class BasePickup : PoolObject
    {
        protected abstract void Activate(Units.PlayerController player);

        private void OnTriggerEnter(Collider other)
        {
            Activate(other.GetComponent<Units.PlayerController>());
            PoolManager.Instance.Release(this);
        }
    }
}
