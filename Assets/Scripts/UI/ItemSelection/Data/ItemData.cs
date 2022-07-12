using System;
using UnityEngine;

public abstract class ItemData<T> : MonoBehaviour
{
    [SerializeField] private T item;

    public T Item => item;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateDraw();
    }
    #endif

    public abstract void UpdateDraw();
}