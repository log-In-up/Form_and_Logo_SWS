using Items;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Application
{
    [CreateAssetMenu(fileName = "Uniform and Logo Data", menuName = "Application Data/Uniform and Logo Data", order = 1)]
    public sealed class UniformAndLogoData : ScriptableObject
    {
        #region Editor fields
        [SerializeField] private List<ItemData> _uniforms = null;
        [SerializeField] private List<ItemData> _logos = null;
        #endregion

        #region Properties
        public List<ItemData> GetUniforms => _uniforms;
        public List<ItemData> GetLogos => _logos;
        #endregion
    }
}