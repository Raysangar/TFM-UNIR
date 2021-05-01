using UnityEngine;
using Core.Utils.Pool;
using Core.EntitySystem;

namespace Game.Gameplay.WeaponsSystem
{
    public class Weapon : EntityComponent
    {
        public System.Action OnGasChanged;
        public System.Action OnAmmoChanged;

        public int CurrentGasInTank { get; private set; }
        public int EquippedAmmoIndex { get; private set; }

        public int TankSize => settings.InfiniteTankSize ? int.MaxValue : settings.TankSize;
        public int ClipSize => settings.InfiniteAmmo ? int.MaxValue : settings.ClipSize;
        public int AmmoTypesCount => clipsPerAmmo.Length;

        public int EquippedAmmo
        {
            get => clipsPerAmmo [EquippedAmmoIndex];
            set => clipsPerAmmo[EquippedAmmoIndex] = value;
        }

        private WeaponSettings.AmmoSettings EquippedAmmoSettings => GetAmmoSettings(EquippedAmmoIndex);

        private Transform projectilePosReference;
        private WeaponSettings settings;
        private int[] clipsPerAmmo;
        private float timeSinceLastProjectile;
        private bool isShooting;

        public WeaponSettings.AmmoSettings GetAmmoSettings(int ammoIndex) => settings.Ammo[ammoIndex];
        public int GetAmmoLeft(int ammoIndex) => clipsPerAmmo[ammoIndex];

        public Weapon(Transform projectilePosReference, WeaponSettings settings)
        {
            this.projectilePosReference = projectilePosReference;
            this.settings = settings;

            clipsPerAmmo = new int[settings.Ammo.Length];
            for (int i = 0; i < clipsPerAmmo.Length; ++i)
                clipsPerAmmo[i] = ClipSize;

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
            EquippedAmmoIndex = ammoIndex;
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

        public override void UpdateBehaviour(float deltaTime)
        {
            if (timeSinceLastProjectile < settings.ProjectilePeriod)
                timeSinceLastProjectile += deltaTime;

            if (isShooting && timeSinceLastProjectile >= settings.ProjectilePeriod)
            {
                timeSinceLastProjectile -= settings.ProjectilePeriod;
                TryShootProjectile();
            }
        }

        private void TryShootProjectile()
        {
            if (CurrentGasInTank > 0 && EquippedAmmo > 0)
            {
                --EquippedAmmo;
                --CurrentGasInTank;
                var projectile = PoolManager.Instance.GetInstanceFor(EquippedAmmoSettings.ProjectilePrefab);
                projectile.Init(projectilePosReference.position, projectilePosReference.rotation,
                    settings.ProjectileSpeed, settings.Damage, EquippedAmmoSettings.Type);
                OnGasChanged?.Invoke();
                OnAmmoChanged?.Invoke();
            }
        }
    }
}
