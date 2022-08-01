using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace _DunkBall.Scripts.Data
{
    public static class DataSaver
    {
        private static GlobalData _globalData;
        public static event UnityAction<GlobalData> OnGlobalDataChanged;

        public static GlobalData GlobalData
        {
            get
            {
                if (_globalData == null)
                {
                    var globalDataPath = Path.Combine(Application.persistentDataPath, "DS.gsd");
                    if (!File.Exists(globalDataPath))
                    {
                        _globalData = new GlobalData();
                        Debug.LogWarning("Create new data");
                    }
                    else
                    {
                        _globalData = JsonUtility.FromJson<GlobalData>(File.ReadAllText(globalDataPath));
                    }
                }

                return _globalData;
            }
            set
            {
                _globalData = value;
                Save();
                OnGlobalDataChanged?.Invoke(_globalData);
            }
        }

        public static void Save()
        {
            var saveDataPath = Path.Combine(Application.persistentDataPath, "DS.gsd");
            File.WriteAllText(saveDataPath, JsonUtility.ToJson(_globalData));

            Debug.LogWarning($"Save global data at path: {saveDataPath}");
        }
    }

    public class GlobalData
    {
        public int CurrentScore;
        public int MaxReachedScore;
        public int TotalStars;

        public bool SfxValue;
        public Sprite CurrentTheme;

        public GlobalData() => SfxValue = true;
    }
}