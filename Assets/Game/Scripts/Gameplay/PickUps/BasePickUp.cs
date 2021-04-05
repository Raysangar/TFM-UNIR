using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public abstract class BasePickUp : MonoBehaviour
    {
        protected abstract void Activate(Player.PlayerController player);

        private void OnTriggerEnter(Collider other)
        {
            Activate(other.GetComponent<Player.PlayerController>());
        }
    }
}
