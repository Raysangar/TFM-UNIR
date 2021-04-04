using UnityEngine;

namespace Game.Gameplay.WeaponsSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] Transform projectileInitialPosition;
        [SerializeField] WeaponSettings settings;

        private WeaponSettings.AmmoSettings CurrentAmmo => settings.AmmoSettins[equippedAmmoIndex];

        private int[] clipsPerAmmo;
        private float timeSinceLastProjectile;
        private bool isShooting;
        private int equippedAmmoIndex;

        public void StartShooting()
        {
            isShooting = true;
        }

        public void StopShooting()
        {
            isShooting = false;
        }

        public void SetEquippedAmmo(int ammoIndex)
        {
            equippedAmmoIndex = ammoIndex;
        }

        private void Awake()
        {
            timeSinceLastProjectile = settings.ProjectileFrequency;
            equippedAmmoIndex = 0;
        }

        private void Update()
        {
            timeSinceLastProjectile += Time.deltaTime;
            if (isShooting && timeSinceLastProjectile > settings.ProjectileFrequency)
            {
                timeSinceLastProjectile -= settings.ProjectileFrequency;
                ShootProjectile();
            }
        }

        private void ShootProjectile()
        {
            if (clipsPerAmmo[equippedAmmoIndex] > 0)
            {
                --clipsPerAmmo[equippedAmmoIndex];

            }
        }
    }
}
