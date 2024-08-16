using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Save
{
    public class PlayerPrefsAppStateSave : IAppStateSave
    {
        private const string SaveKey = "PlayerPrefsSave";
        private Dictionary<string, string> _saveMap = new();

        public void SaveValue(string key, object save)
        {
            _saveMap[key] = JsonConvert.SerializeObject(save);
            var stringSave = JsonConvert.SerializeObject(_saveMap);
            PlayerPrefs.SetString(SaveKey, stringSave);
            PlayerPrefs.Save();
        }

        public TType GetValue<TType>(string key, TType defaultValue = default)
        {
            return _saveMap.TryGetValue(key, out var serialized)
                ? JsonConvert.DeserializeObject<TType>(serialized)
                : defaultValue;
        }

        public void Load()
        {
            _saveMap = PlayerPrefs.HasKey(SaveKey)
                ? JsonConvert.DeserializeObject<Dictionary<string, string>>(PlayerPrefs.GetString(SaveKey))
                : new Dictionary<string, string>();
        }
    }
}