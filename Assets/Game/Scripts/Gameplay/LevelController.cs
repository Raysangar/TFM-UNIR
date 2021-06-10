using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay.Units;

namespace Game.Gameplay
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] Transform playerInitialPosition;

        private PlayerController player;

        public void Init(PlayerController player)
        {
            this.player = player;
            player.transform.position = playerInitialPosition.position;
            player.transform.rotation = playerInitialPosition.rotation;

            var enemies = FindObjectsOfType<EnemyController>();
            foreach (var enemy in enemies)
                enemy.InitBehaviour(player);
        }
    }
}
