using UnityEngine;
using UnityEngine.UI;
using Game.Gameplay.WeaponsSystem;
using Core.Utils;

namespace Game.UI
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public class AmmoSelectionWidget : MonoBehaviour
    {
        public void SetAmmoCounter(int currentAmmo, int clipSize)
        {
            transform.SetLocalScale((float)currentAmmo / clipSize);
        }

        public void Init(WeaponSettings.AmmoSettings ammo, int ammoTypeCount, int ammoIndex)
        {
            var image = GetComponent<Image>();
            image.color = ammo.Color;
            image.fillAmount = 1f / ammoTypeCount;
            transform.rotation = Quaternion.Euler(0, 0, (360f / ammoTypeCount) * ammoIndex);
        }
    }
}
