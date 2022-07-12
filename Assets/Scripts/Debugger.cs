using System.Collections.Generic;
using Data;
using Newtonsoft.Json;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private VersionInfoSO version;
    [SerializeField] private EngineInfoSO engine;
    [SerializeField] private ColorInfoSO color;
    [SerializeField] private UpholsteryInfoSO upholstery;
    [SerializeField] private AdditionalPackageInfoSO[] additionalPackages;

    [ContextMenu("Debug Item")]
    public void DebugItem()
    {
        var car1 = new CarConfigurationModel();
        
        car1.SetVersion(version);
        car1.SetEngine(engine);
        car1.SetColor(color);
        car1.SetUpholstery(upholstery);
        foreach (var package in additionalPackages)
        {
            car1.AddAdditionalPackageInfo(package);
        }

        var car = new List<CarConfigurationModel>()
        {
            car1, car1
        };

        var json = JsonConvert.SerializeObject(car);
        Debug.Log(json);
        
        Debug.Log(JsonConvert.DeserializeObject<List<CarConfigurationModel>>(json)[0]);
    }
}