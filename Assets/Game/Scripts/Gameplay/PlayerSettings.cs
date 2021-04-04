using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Game/Gameplay/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        public float Speed;
        public WeaponsSystem.WeaponSettings WeaponSettings;
    }
}
