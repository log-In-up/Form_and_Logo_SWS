using System;
using UnityEngine;

namespace Items
{
    [Serializable]
    public struct ItemData
    {
        #region Properties
        [field: SerializeField] public Sprite FirstLayer { get; private set; }
        [field: SerializeField] public Sprite SecondLayer { get; private set; }
        [field: SerializeField] public Sprite ThirdLayer { get; private set; }
        [field: SerializeField] public Sprite FourthLayer { get; private set; }
        #endregion
    }
}