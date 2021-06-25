namespace Game
{
    public class SaveFile
    {
        public int LastLevelIndexCompleted;
        public int TimesGameFinished;

        public SaveFile()
        {
            LastLevelIndexCompleted = -1;
            TimesGameFinished = 0;
        }
    }
}
