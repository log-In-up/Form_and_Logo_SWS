using System;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Application
{
    [Serializable]
    public struct BackgroundData
    {
        #region Editor fields
        [field: SerializeField] public Image Background { get; private set; }
        [field: SerializeField] public Sprite Room { get; private set; }
        [field: SerializeField] public Sprite Stadium { get; private set; }
        #endregion
    }
}