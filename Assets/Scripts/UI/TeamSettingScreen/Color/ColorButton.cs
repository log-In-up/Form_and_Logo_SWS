using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button), typeof(Image))]
    public sealed class ColorButton : MonoBehaviour
    {
        #region Fields
        private Button _button = null;
        private Image _image = null;
        #endregion

        #region Properties

        #endregion

        #region Events
        public delegate void ButtonEventHandler(Color buttonColor);
        public event ButtonEventHandler OnClick;
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClickButton);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickButton);
        }
        #endregion

        #region Public Methods
        public void SetColor(Color color)
        {
            _image.color = color;
        }
        #endregion

        #region EventHandlers
        private void OnClickButton()
        {
            OnClick?.Invoke(_image.color);
        }
        #endregion
    }
}