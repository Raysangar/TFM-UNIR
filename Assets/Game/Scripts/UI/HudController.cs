using UnityEngine;
using Game.Gameplay.Units;
using Core.Utils;

namespace Game.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] RectTransform lifeBar;
        [SerializeField] RectTransform gasBar;
        [SerializeField] RectTransform ammoBar;

        private PlayerController player;

        public void Init(PlayerController player)
        {
            this.player = player;
            lifeBar.SetLocalScaleX(1);
            gasBar.SetLocalScaleX(1);
            ammoBar.SetLocalScaleX(1);
            player.Life.OnChanged += OnLifeChanged;
            player.Weapon.OnGasChanged += OnGasChanged;
            player.Weapon.OnAmmoChanged += OnAmmoChanged;
        }

        private void OnLifeChanged()
        {
            lifeBar.SetLocalScaleX((float)player.Life.Current / player.Life.Max);
        }

        private void OnGasChanged()
        {
            gasBar.SetLocalScaleX((float)player.Weapon.CurrentGasInTank / player.Weapon.TankSize);
        }

        private void OnAmmoChanged()
        {
            ammoBar.SetLocalScaleX((float)player.Weapon.EquippedAmmo / player.Weapon.ClipSize);
        }
    }
}
