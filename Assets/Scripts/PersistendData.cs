using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    private const string PREF_KEY_DATA = "PREF_LOADDATA";
    
    private List<CarConfigurationModel> _carConfigurations;

    private void Awake()
    {
        Load();
    }

    public void AddCarConfiguration(CarConfigurationModel carConfiguration)
    {
        if(carConfiguration == null) return;
        
        _carConfigurations.Add(carConfiguration);
    }

    public void UpdateCarConfiguration(CarConfigurationModel carConfiguration, int index)
    {
        if(carConfiguration == null) return;
        if (index > _carConfigurations.Count - 1 || index < 0) return;

        _carConfigurations[index] = carConfiguration;
    }

    public CarConfigurationModel GetCarConfiguration(int index)
    {
        if (index > _carConfigurations.Count - 1 || index < 0) return null;
        return _carConfigurations[index];
    }

    public void Save()
    {
        var json = JsonConvert.SerializeObject(_carConfigurations);
        PlayerPrefs.SetString(PREF_KEY_DATA, json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey(PREF_KEY_DATA))
        {
            _carConfigurations = new List<CarConfigurationModel>();
            return;
        }

        var loadedConfigs = JsonConvert.DeserializeObject<List<CarConfigurationModel>>(PlayerPrefs.GetString(PREF_KEY_DATA));

        if (loadedConfigs == null)
        {
            Debug.LogError("Error loading config data");
            return;
        }

        _carConfigurations = loadedConfigs;
    }
}