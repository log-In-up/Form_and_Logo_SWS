using System.Collections.Generic;
using UnityEngine;

namespace Data.Application
{
    [CreateAssetMenu(fileName = "Color Picker Data", menuName = "Application Data/Color Picker Data", order = 1)]
    public sealed class ColorPickerData : ScriptableObject
    {
        #region Editor fields
        [SerializeField] private List<Color> _colors = null;
        #endregion

        #region
        public List<Color> GetColors => _colors;
        #endregion
    }
}