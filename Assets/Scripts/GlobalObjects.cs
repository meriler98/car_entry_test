using UnityEngine;

public static class GlobalObjects
{
    private static PersistentData _persistentData;

    public static PersistentData GetPersistentData()
    {
        if (_persistentData == null)
        {
            var persistentData = GameObject.FindObjectOfType<PersistentData>();

            if (persistentData == null)
            {
                Debug.LogWarning("No persistent data object was found on scene. Creating new one");
                var newGo = new GameObject("PersistentData");
                persistentData = newGo.AddComponent<PersistentData>();
            }

            _persistentData = persistentData;
        }

        return _persistentData;
    }       
}