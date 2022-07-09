using System;
using UnityEngine;

[Serializable]
public struct VersionCompatibilityData
{
    [SerializeField] private VersionInfoSO versionInfo;
    [SerializeField] private bool isOption;

    public VersionInfoSO VersionInfo => versionInfo;
    public bool IsOption => isOption;
}