using UnityEngine;

public class ConfigurationEditorUI : MonoBehaviour
{
    private CarConfigurationModel _currentModel;
    private int _currentEditingIndex = -1;

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void SetConfigurationModel(CarConfigurationModel carConfigurationModel)
    {
        _currentModel = carConfigurationModel;
    }

    public void SetConfigurationEditingIndex(int index)
    {
        _currentEditingIndex = index;
    }
}