using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image), typeof(Toggle))]
    public sealed class ToggleColorButton : MonoBehaviour
    {
        #region Editor fields
        [SerializeField] private Image _image = null;
        #endregion

        #region Fields
        private Toggle _toggle = null;
        #endregion

        #region Properties
        public bool IsOn => _toggle.isOn;
        public Color Color => _image.color;
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }
        #endregion


        #region Public methods
        public void SetColor(Color color)
        {
            _image.color = color;
        }
        #endregion
    }
}