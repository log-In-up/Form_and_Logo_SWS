using System;
using UnityEngine;

namespace Data.Application
{
    [Serializable]
    public struct UIAnimatedObjectsData
    {
        #region Editor fields
        [field: SerializeField] public GameObject TopUniformText { get; private set; }
        [field: SerializeField] public GameObject TopLogoText { get; private set; }
        [field: SerializeField] public GameObject FormOnBackground { get; private set; }
        [field: SerializeField] public GameObject FormOnForeground { get; private set; }
        [field: SerializeField] public GameObject Logo { get; private set; }
        [field: SerializeField, Min(0.0f)] public float TimeToWait { get; private set; }
        #endregion
    }
}