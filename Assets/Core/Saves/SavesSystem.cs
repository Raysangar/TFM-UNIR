using UnityEngine;
using System.IO;

namespace Core.Saves
{
    public static class SavesSystem
    {
        private const string SavesFolder = "Saves";
        private const string SaveFileFormat = "Save-";

        public static bool HasAtLeastOneSaveFile
        {
            get
            {
                string directoryPath = Path.Combine(Application.persistentDataPath, SavesFolder);
                return Directory.Exists(directoryPath) && Directory.GetFiles(directoryPath).Length > 0;
            }
        }

    public static void TrySave<T>(T saveFile, int saveIndex = 0) where T : class
        {
            CreateSavesDirectoryIfNecessary();
            string savePath = GetSavePath(saveIndex);
            File.WriteAllText(savePath, JsonUtility.ToJson(saveFile), System.Text.Encoding.UTF8);
        }

        public static T TryLoad<T>(int saveIndex = 0) where T : class
        {
            CreateSavesDirectoryIfNecessary();
            string savePath = GetSavePath(saveIndex);
            if (File.Exists(savePath))
                return JsonUtility.FromJson<T>(File.ReadAllText(savePath));
            else
                return null;
        }

        public static void TryDeleteSave(int saveIndex)
        {
            CreateSavesDirectoryIfNecessary();
            string savePath = GetSavePath(saveIndex);
            if (File.Exists(savePath))
                File.Delete(savePath);
        }

        private static string GetSavePath(int saveIndex) => Path.Combine(Application.persistentDataPath, SavesFolder, SaveFileFormat + saveIndex);

        private static void CreateSavesDirectoryIfNecessary()
        {
            string directoryPath = Path.Combine(Application.persistentDataPath, SavesFolder);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
    }
}
