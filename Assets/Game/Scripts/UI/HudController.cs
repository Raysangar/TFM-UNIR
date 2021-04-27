using UnityEngine;
using Game.Gameplay.Units;
using Core.Utils;

namespace Game.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] PlayerController player;
        [SerializeField] RectTransform lifeBar;
        [SerializeField] RectTransform gasBar;
        [SerializeField] RectTransform ammoBar;

        private void Start()
        {
            player.Life.OnChanged += OnLifeChanged;
            player.Weapon.OnGasChanged += OnGasChanged;
            player.Weapon.OnAmmoChanged += OnAmmoChanged;
            lifeBar.SetLocalScaleX(1);
            gasBar.SetLocalScaleX(1);
            ammoBar.SetLocalScaleX(1);
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
            ammoBar.SetLocalScaleX((float)player.Weapon.CurrentAmmo / player.Weapon.CurrentClipSize);
        }
    }
}
