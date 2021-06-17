using UnityEngine;

namespace Game.Gameplay
{
    public enum LevelId
    {
        Paintball, Outdoor
    }

    [System.Serializable]
    public class LevelSettings
    {
        public LevelId Id;
        public int SceneIndex;
    }

    [CreateAssetMenu(fileName = "LevelsSettings", menuName = "Game/Gameplay/Levels Settings")]
    public class LevelsSettings : ScriptableObject
    {
        public LevelSettings[] Levels;
    }
}
