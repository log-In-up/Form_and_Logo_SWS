using Data.Application;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    [DisallowMultipleComponent]
    public sealed class ColorViewer : MonoBehaviour
    {
        #region Editor fields
        [SerializeField] private ColorPickerData _colorPickerData = null;
        [SerializeField] private List<ToggleColorButton> _toggleColors = null;
        [SerializeField] private Button _randomButton = null;
        #endregion

        #region Event
        public delegate void ToggleColorButtonDelegate(Color color, int index);
        public event ToggleColorButtonDelegate OnSetColorForActiveToggle;
        #endregion

        #region MonoBehaviour API
        private void OnEnable()
        {
            _randomButton.onClick.AddListener(OnClickRandom);
        }

        private void OnDisable()
        {
            _randomButton.onClick.RemoveListener(OnClickRandom);
        }
        #endregion

        #region Public method
        internal void SetColorForActiveToggle(Color color)
        {
            int index = _toggleColors.FindIndex(a => a.IsOn == true);
            _toggleColors[index].SetColor(color);

            OnSetColorForActiveToggle?.Invoke(color, index);
        }

        internal bool Contains(Color color)
        {
            foreach (ToggleColorButton colorButton in _toggleColors)
            {
                if (colorButton.Color == color)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Button Handlers
        private void OnClickRandom()
        {
            List<Color> colors = new List<Color>(_colorPickerData.GetColors);

            for (int index = 0; index < _toggleColors.Count; index++)
            {
                int randomIndex = Random.Range(0, colors.Count - 1);
                Color color = colors[randomIndex];

                colors.Remove(color);

                _toggleColors[index].SetColor(color);

                OnSetColorForActiveToggle?.Invoke(color, index);
            }
        }
        #endregion

        #region Public Methods
        public List<Color> GetRandomColors(int count)
        {
            List<Color> result = new List<Color>();
            List<Color> colors = new List<Color>(_colorPickerData.GetColors);

            for (int index = 0; index < count; index++)
            {
                int randomIndex = Random.Range(0, colors.Count - 1);
                Color color = colors[randomIndex];

                result.Add(color);
                colors.Remove(color);
            }

            return result;
        }

        internal void SetVieverColors(List<Color> colors)
        {
            for (int index = 0; index < colors.Count; index++)
            {
                _toggleColors[index].SetColor(colors[index]);
            }
        }
        #endregion
    }
}