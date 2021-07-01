using UnityEngine;
using UnityEngine.UI;
using Game.Gameplay.Units;
using Core.Utils;

namespace Game.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] RectTransform lifeBar;
        [SerializeField] RectTransform gasBar;
        [SerializeField] Image ammoBar;

        private PlayerController player;
        private RectTransform cachedAmmoBarTransform;

        public void Init(PlayerController player)
        {
            this.player = player;
            cachedAmmoBarTransform = ammoBar.rectTransform;
            ammoBar.color = player.Weapon.EquippedAmmoSettings.Color;
            lifeBar.SetLocalScaleX(1);
            gasBar.SetLocalScaleX(1);
            cachedAmmoBarTransform.SetLocalScaleX(1);
            player.Life.OnChanged += OnLifeChanged;
            player.Life.OnDeath += OnPlayerDeath;
            player.Weapon.OnGasChanged += OnGasChanged;
            player.Weapon.OnAmmoChanged += OnAmmoChanged;
        }

        private void OnLifeChanged()
        {
            lifeBar.SetLocalScaleX((float)player.Life.Current / player.Life.Max);
        }

        private void OnPlayerDeath()
        {
            lifeBar.SetLocalScaleX(0);
        }

        private void OnGasChanged()
        {
            gasBar.SetLocalScaleX((float)player.Weapon.CurrentGasInTank / player.Weapon.TankSize);
        }

        private void OnAmmoChanged()
        {
            cachedAmmoBarTransform.SetLocalScaleX((float)player.Weapon.EquippedAmmoLeft / player.Weapon.ClipSize);
            ammoBar.color = player.Weapon.EquippedAmmoSettings.Color;
        }
    }
}
