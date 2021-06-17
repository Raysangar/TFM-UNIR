using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay.Units;

namespace Game.Gameplay
{
    public class LevelController : MonoBehaviour
    {
        public LevelEndArea EndArea => endArea;

        [SerializeField] Transform playerInitialPosition;
        [SerializeField] LevelEndArea endArea;

        public void Init(PlayerController player)
        {
            player.transform.position = playerInitialPosition.position;
            player.transform.rotation = playerInitialPosition.rotation;

            var enemies = FindObjectsOfType<EnemyController>();
            foreach (var enemy in enemies)
                enemy.InitBehaviour(player);
        }
    }
}
