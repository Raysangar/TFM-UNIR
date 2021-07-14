using Game.Gameplay.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class UnlockLevelEndPickup : Pickups.BasePickup
    {
        public System.Action OnPlayerEarned;

        [SerializeField] EnemyController[] enemiesHidingThis;

        private int deadEnemiesHidingThis;

        public void SetVisual(int index)
        {
            var visualAssets = transform.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < visualAssets.Length; ++i)
                visualAssets[i].enabled = i == index;
        }

        protected override void Start()
        {
            base.Start();
            if (enemiesHidingThis.Length > 0)
            {
                deadEnemiesHidingThis = 0;
                gameObject.SetActive(false);
                foreach (var enemy in enemiesHidingThis)
                    enemy.Life.OnDeath += OnEnemyHidingThisDied;
            }
        }

        private void OnEnemyHidingThisDied()
        {
            if (++deadEnemiesHidingThis == enemiesHidingThis.Length)
                gameObject.SetActive(true);
        }

        protected override void Activate(PlayerController player)
        {
            OnPlayerEarned();
        }
    }
}
