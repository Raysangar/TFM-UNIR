using UnityEngine;

namespace Game.Gameplay.PickUps
{
    public abstract class BasePickUp : MonoBehaviour
    {
        protected abstract void Activate(Units.PlayerController player);

        private void OnTriggerEnter(Collider other)
        {
            Activate(other.GetComponent<Units.PlayerController>());
        }
    }
}
