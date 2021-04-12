using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Units.PlayerController player;

        private void Start()
        {
            player.OnDeath += OnPlayerDeath;

            var enemies = FindObjectsOfType<Units.EnemyController>();
            foreach (var enemy in enemies)
                enemy.InitBehaviour(player);
        }

        private void OnPlayerDeath()
        {
            SceneManager.LoadScene(2);
            Debug.Log("Game Over");
        }
    }
}
