using UnityEngine;
using Core.Utils.Pool;

namespace Game.Gameplay.WeaponsSystem
{
    public class Weapon : MonoBehaviour
    {
        private WeaponSettings.AmmoSettings CurrentAmmo => settings.Ammo[equippedAmmoIndex];

        private Transform projectilePosReference;
        private WeaponSettings settings;
        private int[] clipsPerAmmo;
        private float timeSinceLastProjectile;
        private bool isShooting;
        private int equippedAmmoIndex;

        public void Init(Transform projectilePosReference, WeaponSettings settings)
        {
            this.projectilePosReference = projectilePosReference;
            this.settings = settings;

            clipsPerAmmo = new int[settings.Ammo.Length];
            int clipSize = settings.InfiniteAmmo ? int.MaxValue : settings.ClipSize;
            for (int i = 0; i < clipsPerAmmo.Length; ++i)
                clipsPerAmmo[i] = clipSize;
            
            timeSinceLastProjectile = settings.ProjectilePeriod;
            SetEquippedAmmo(0);
        }

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

        public void AddAmmo(int ammoIndex, int amount)
        {
            clipsPerAmmo[ammoIndex] = Mathf.Min(clipsPerAmmo[ammoIndex] + amount, settings.ClipSize);
        }

        private void Update()
        {
            if (timeSinceLastProjectile < settings.ProjectilePeriod)
                timeSinceLastProjectile += Time.deltaTime;

            if (isShooting && timeSinceLastProjectile >= settings.ProjectilePeriod)
            {
                timeSinceLastProjectile -= settings.ProjectilePeriod;
                ShootProjectile();
            }
        }

        private void ShootProjectile()
        {
            if (clipsPerAmmo[equippedAmmoIndex] > 0)
            {
                --clipsPerAmmo[equippedAmmoIndex];
                var projectile = PoolManager.Instance.GetInstanceFor(CurrentAmmo.ProjectilePrefab);
                projectile.Init(projectilePosReference.position, projectilePosReference.rotation, 
                    settings.ProjectileSpeed, settings.Damange);
            }
        }
    }
}
