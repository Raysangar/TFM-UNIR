using UnityEngine;
using Core.Utils.Pool;

namespace Game.Gameplay.WeaponsSystem
{
    public class Weapon : MonoBehaviour
    {
        public System.Action OnGasChanged;
        public System.Action OnAmmoChanged;

        public int CurrentGasInTank { get; private set; }

        public int TankSize => settings.InfiniteTankSize ? int.MaxValue : settings.TankSize;
        public int CurrentClipSize => settings.InfiniteAmmo ? int.MaxValue : settings.ClipSize;
        public int AmmoTypesCount => clipsPerAmmo.Length;

        public int CurrentAmmo
        {
            get => clipsPerAmmo [equippedAmmoIndex];
            set => clipsPerAmmo[equippedAmmoIndex] = value;
        }

        private WeaponSettings.AmmoSettings CurrentAmmoSettings => settings.Ammo[equippedAmmoIndex];

        private Transform projectilePosReference;
        private WeaponSettings settings;
        private int[] clipsPerAmmo;
        private float timeSinceLastProjectile;
        private bool isShooting;
        private int equippedAmmoIndex;

        public int GetAmmoLeftForType(int ammoIndex) => clipsPerAmmo[ammoIndex];

        public void Init(Transform projectilePosReference, WeaponSettings settings)
        {
            this.projectilePosReference = projectilePosReference;
            this.settings = settings;

            clipsPerAmmo = new int[settings.Ammo.Length];
            for (int i = 0; i < clipsPerAmmo.Length; ++i)
                clipsPerAmmo[i] = CurrentClipSize;

            CurrentGasInTank = TankSize;

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
            OnAmmoChanged?.Invoke();
        }

        public void AddAmmo(int ammoIndex, int amount)
        {
            clipsPerAmmo[ammoIndex] = Mathf.Min(clipsPerAmmo[ammoIndex] + amount, settings.ClipSize);
            OnAmmoChanged?.Invoke();
        }

        public void AddGas(int amount)
        {
            CurrentGasInTank = Mathf.Min(CurrentGasInTank + amount, TankSize);
            OnGasChanged?.Invoke();
        }

        private void Update()
        {
            if (timeSinceLastProjectile < settings.ProjectilePeriod)
                timeSinceLastProjectile += Time.deltaTime;

            if (isShooting && timeSinceLastProjectile >= settings.ProjectilePeriod)
            {
                timeSinceLastProjectile -= settings.ProjectilePeriod;
                TryShootProjectile();
            }
        }

        private void TryShootProjectile()
        {
            if (CurrentGasInTank > 0 && CurrentAmmo > 0)
            {
                --CurrentAmmo;
                --CurrentGasInTank;
                var projectile = PoolManager.Instance.GetInstanceFor(CurrentAmmoSettings.ProjectilePrefab);
                projectile.Init(projectilePosReference.position, projectilePosReference.rotation,
                    settings.ProjectileSpeed, settings.Damage, CurrentAmmoSettings.Type);
                OnGasChanged?.Invoke();
                OnAmmoChanged?.Invoke();
            }
        }
    }
}
