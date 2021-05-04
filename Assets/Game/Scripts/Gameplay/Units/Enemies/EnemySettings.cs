using UnityEngine;

namespace Game.Gameplay.Units
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Game/Gameplay/Enemy Settings")]
    public class EnemySettings : BaseUnitSettings
    {
        public float DistanceToPlayerToStartFollowing;
        public float DistanceToPlayerToStartShooting;
    }
}
