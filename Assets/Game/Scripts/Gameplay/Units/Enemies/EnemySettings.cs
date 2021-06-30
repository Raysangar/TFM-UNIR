using UnityEngine;

namespace Game.Gameplay.Units
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Game/Gameplay/Enemy Settings")]
    public class EnemySettings : BaseUnitSettings
    {
        [System.Serializable]
        public class Skin
        {
            public WeaponsSystem.AmmoType AmmoWeakness;
            public Material Material;
        }

        public float DistanceToPlayerToStartFollowing;
        public float DistanceToPlayerToStartShooting;

        [SerializeField] Skin[] skins;

        public Material GetMaterialFor(WeaponsSystem.AmmoType ammoWaekness)
        {
            foreach (var skin in skins)
                if (skin.AmmoWeakness == ammoWaekness)
                    return skin.Material;
            return null;
        }
    }
}
