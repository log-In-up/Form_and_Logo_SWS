using Data.Application;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{
    [DisallowMultipleComponent]
    public sealed class ColorPalette : MonoBehaviour
    {
        #region Editor Fields
        [SerializeField] private ColorPickerData _colorPickerData = null;
        [SerializeField] private GameObject _colorInstance = null;
        [SerializeField] private RectTransform _instanceHolder = null;
        [SerializeField] private ColorViewer _colorViewer = null;
        #endregion

        #region Fields
        private List<ColorButton> _buttons = null;
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _buttons = new List<ColorButton>();

            List<Color> colors = _colorPickerData.GetColors;

            for (int index = 0; index < colors.Count; index++)
            {
                ColorButton colorButton = Instantiate(_colorInstance, _instanceHolder).GetComponent<ColorButton>();
                colorButton.SetColor(colors[index]);

                _buttons.Add(colorButton);
            }
        }

        private void OnEnable()
        {
            foreach (ColorButton button in _buttons) 
            {
                button.OnClick += OnClickColorButton;
            }
        }

        private void OnDisable()
        {
            foreach (ColorButton button in _buttons)
            {
                button.OnClick -= OnClickColorButton;
            }
        }
        #endregion

        #region Button handlers
        private void OnClickColorButton(Color color)
        {
            if (_colorViewer.Contains(color)) return;

            _colorViewer.SetColorForActiveToggle(color);
        }
        #endregion
    }
}