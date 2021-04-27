using UnityEngine;

namespace Game.Gameplay.WeaponsSystem
{
    public class GasStation : MonoBehaviour
    {
        [SerializeField] int gasToAddPerSecond;

        private float addGasPeriodInSeconds;
        private float timeSinceLastGasAdding;
        private Weapon playerWeapon;

        private void Awake()
        {
            playerWeapon = null;
            addGasPeriodInSeconds = 1f / gasToAddPerSecond;
        }

        private void OnTriggerEnter(Collider other)
        {
            playerWeapon = other.GetComponent<Units.PlayerController>().Weapon;
            timeSinceLastGasAdding = 0;
        }

        private void Update()
        {
            if (playerWeapon != null)
            {
                timeSinceLastGasAdding += Time.deltaTime;
                if (timeSinceLastGasAdding >= addGasPeriodInSeconds)
                {
                    timeSinceLastGasAdding -= addGasPeriodInSeconds;
                    playerWeapon.AddGas(1);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            playerWeapon = null;
        }
    }
}
