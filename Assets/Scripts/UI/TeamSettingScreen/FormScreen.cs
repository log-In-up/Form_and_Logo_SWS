using Data.Player;
using Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class FormScreen : ITeamSettingsState
    {
        #region Fields
        private int _formsIndex;

        private readonly Button _left = null, _next = null, _right = null;
        private readonly List<ItemData> _uniforms = null;

        private PlayerTeamData _playerTeamData = null;
        private ColorViewer _colorViewer = null;
        private TeamSettingsScreen _teamSettingsScreen = null;
        private List<FormLogoObject> _availableUniforms = null;
        private FormAndLogoUIData _uiFormAndLogoData;
        
        #endregion

        public FormScreen(TeamSettingsScreen teamSettingsScreen, PlayerTeamData playerTeamData, Button left, Button right, Button next, FormAndLogoUIData uiFormAndLogoData, ColorViewer colorViewer, List<ItemData> uniforms)
        {
            _teamSettingsScreen = teamSettingsScreen;
            _playerTeamData = playerTeamData;

            _left = left;
            _next = next;
            _right = right;

            _uniforms = uniforms;

            _availableUniforms = new List<FormLogoObject>();
            _uiFormAndLogoData = uiFormAndLogoData;
            _colorViewer = colorViewer;
        }

        #region Interface Implementation
        public void Close()
        {
            _left.onClick.RemoveListener(OnClickLeft);
            _next.onClick.RemoveListener(OnClickNext);
            _right.onClick.RemoveListener(OnClickRight);
            _colorViewer.OnSetColorForActiveToggle -= OnSetColorForActiveToggle;
        }

        public void Initialize()
        {
            _left.onClick.AddListener(OnClickLeft);
            _next.onClick.AddListener(OnClickNext);
            _right.onClick.AddListener(OnClickRight);
            _colorViewer.OnSetColorForActiveToggle += OnSetColorForActiveToggle;

            foreach (ItemData uniform in _uniforms)
            {
                List<Color> colors = _colorViewer.GetRandomColors(3);

                FormLogoObject formLogoObject = new FormLogoObject
                {
                    FirstLayer = uniform.FirstLayer,
                    FirstLayerColor = colors[0],
                    SecondLayer = uniform.SecondLayer,
                    SecondLayerColor = colors[1],
                    ThirdLayer = uniform.ThirdLayer,
                    ThirdLayerColor = colors[2],
                    FourthLayer = uniform.FourthLayer
                };

                _availableUniforms.Add(formLogoObject);

            }

            _formsIndex = 0;
            SetUniform(_availableUniforms[_formsIndex]);
        }
        #endregion

        #region Methods
        private void SetUniform(FormLogoObject itemData)
        {
            _uiFormAndLogoData.SetForegroundUniform(itemData);

            List<Color> colors = new List<Color>
            {
                itemData.FirstLayerColor,
                itemData.SecondLayerColor,
                itemData.ThirdLayerColor
            };

            _colorViewer.SetVieverColors(colors);
        }
        #endregion

        #region Button handlers
        private void OnClickLeft()
        {
            if (_formsIndex <= 0) return;

            _formsIndex--;

            SetUniform(_availableUniforms[_formsIndex]);
        }

        private void OnClickNext()
        {
            _uiFormAndLogoData.SetBackgroundUniform(_availableUniforms[_formsIndex]);

            _playerTeamData.SetTeamUniform(_availableUniforms[_formsIndex]);
            
            _teamSettingsScreen.State = TeamSettingsState.Logo;
        }

        private void OnClickRight()
        {
            if (_formsIndex + 1 >= _availableUniforms.Count) return;

            _formsIndex++;

            SetUniform(_availableUniforms[_formsIndex]);
        }

        private void OnSetColorForActiveToggle(Color color, int index)
        {
            FormLogoObject itemData = _availableUniforms[_formsIndex];

            switch (index)
            {
                case 0:
                    _uiFormAndLogoData.FirstForegroundLayer.color = color;
                    itemData.FirstLayerColor = color;
                    break;
                case 1:
                    _uiFormAndLogoData.SecondForegroundLayer.color = color;
                    itemData.SecondLayerColor = color;
                    break;
                case 2:
                    _uiFormAndLogoData.ThirdForegroundLayer.color = color;
                    itemData.ThirdLayerColor = color;
                    break;
                default:
                    break;
            }

            _availableUniforms[_formsIndex] = itemData;
        }
        #endregion
    }
}