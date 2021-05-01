using UnityEngine;
using Core.EntitySystem;

namespace Game.Gameplay.WeaponsSystem
{
    public class GasStation : Entity
    {
        [SerializeField] int gasToAddPerSecond;

        private float addGasPeriodInSeconds;
        private float timeSinceLastGasAdding;
        private Weapon playerWeapon;

        protected override void Awake()
        {
            base.Awake();
            playerWeapon = null;
            addGasPeriodInSeconds = 1f / gasToAddPerSecond;
        }

        private void OnTriggerEnter(Collider other)
        {
            playerWeapon = other.GetComponent<Units.PlayerController>().Weapon;
            timeSinceLastGasAdding = 0;
        }

        public override void UpdateBehaviour(float deltaTime)
        {
            if (playerWeapon != null)
            {
                timeSinceLastGasAdding += deltaTime;
                if (timeSinceLastGasAdding >= addGasPeriodInSeconds)
                {
                    timeSinceLastGasAdding -= addGasPeriodInSeconds;
                    playerWeapon.AddGas(1);
                }
            }

            base.UpdateBehaviour(deltaTime);
        }

        private void OnTriggerExit(Collider other)
        {
            playerWeapon = null;
        }
    }
}
