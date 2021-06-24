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
        public bool IsShooting { get; private set; }

        public int TankSize => settings.InfiniteTankSize ? int.MaxValue : settings.TankSize;
        public int ClipSize => settings.InfiniteAmmo ? int.MaxValue : settings.ClipSize;
        public int AmmoTypesCount => clipsPerAmmo.Length;

        public int EquippedAmmoLeft
        {
            get => clipsPerAmmo [EquippedAmmoIndex];
            set => clipsPerAmmo[EquippedAmmoIndex] = value;
        }

        public WeaponSettings.AmmoSettings EquippedAmmoSettings => GetAmmoSettings(EquippedAmmoIndex);

        private readonly Transform projectilePosReference;
        private readonly WeaponSettings settings;
        private readonly int[] clipsPerAmmo;
        private float timeSinceLastProjectile;

        public WeaponSettings.AmmoSettings GetAmmoSettings(int ammoIndex) => settings.Ammo[ammoIndex];
        public int GetAmmoLeft(int ammoIndex) => clipsPerAmmo[ammoIndex];

        public Weapon(Entity entity, Transform projectilePosReference, WeaponSettings settings) : base(entity)
        {
            this.projectilePosReference = projectilePosReference;
            this.settings = settings;

            clipsPerAmmo = new int[settings.Ammo.Length];
            for (int i = 0; i < clipsPerAmmo.Length; ++i)
                clipsPerAmmo[i] = ClipSize;

            CurrentGasInTank = TankSize;

            timeSinceLastProjectile = settings.ProjectilePeriod - settings.TimeForFirstProjectile;
            SetEquippedAmmo(0);
        }

        public void StartShooting()
        {
            IsShooting = true;
        }

        public void StopShooting()
        {
            IsShooting = false;
            timeSinceLastProjectile = settings.ProjectilePeriod - settings.TimeForFirstProjectile;
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
            if (IsShooting)
            {
                timeSinceLastProjectile += deltaTime;
                if (timeSinceLastProjectile >= settings.ProjectilePeriod)
                {
                    timeSinceLastProjectile -= settings.ProjectilePeriod;
                    TryShootProjectile();
                }
            }
        }

        private void TryShootProjectile()
        {
            if (CurrentGasInTank > 0 && EquippedAmmoLeft > 0)
            {
                --EquippedAmmoLeft;
                --CurrentGasInTank;
                var projectile = PoolManager.Instance.GetInstanceFor(EquippedAmmoSettings.ProjectilePrefab);
                var projectileSettings = settings.GetCurrentProjectileSettings(CurrentGasInTank, TankSize);
                projectile.Init(projectilePosReference, EquippedAmmoSettings, settings.HitAudioEffects, projectileSettings);
                OnGasChanged?.Invoke();
                OnAmmoChanged?.Invoke();
                Core.Audio.AudioManager.Instance.PlayAudioEffect(settings.ShootAudioEffects, entity.transform.position);
            }
        }
    }
}
