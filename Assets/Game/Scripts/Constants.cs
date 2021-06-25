namespace Game
{
    public static class Constants
    {
        public const float DistanceThreshold = .01f;

        public static class Scenes
        {
            public const int MainMenuIndex = 1;
            public const int GameplayIndex = 2;
        }

        public static class PhysicLayers
        {
            public const int Default = 0;
            public const int Player = 3;
            public const int Enemies = 6;
        }

        public static class PlayerPrefsIds
        {
            public const string SelectedSaveFileIndexId = "SelectedSaveFileIndex";
        }
    }
}
