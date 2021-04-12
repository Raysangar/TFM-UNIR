using UnityEngine;
using Game.Gameplay.Units;
using Core.Utils;

namespace Game.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] PlayerController player;
        [SerializeField] RectTransform lifeBar;

        private void Start()
        {
            player.Life.OnChanged += OnLifeChanged;
            lifeBar.SetLocalScaleX(1);
        }

        private void OnLifeChanged()
        {
            lifeBar.SetLocalScaleX((float)player.Life.Current / player.Life.Max);
        }
    }
}
