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
        private readonly TeamSettingsScreen _teamSettingsScreen = null;
        private readonly PlayerTeamData _playerTeamData = null;
        private readonly ColorViewer _colorViewer = null;
        private FormAndLogoUIData _uiFormAndLogoData;
        #endregion

        public FormScreen(TeamSettingsScreen teamSettingsScreen, PlayerTeamData playerTeamData, Button left, Button right, Button next, FormAndLogoUIData uiFormAndLogoData, ColorViewer colorViewer)
        {
            _teamSettingsScreen = teamSettingsScreen;
            _playerTeamData = playerTeamData;

            _left = left;
            _next = next;
            _right = right;

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
            _colorViewer.OnGetRandomColors -= OnGetRandomColors;
        }

        public void Initialize()
        {
            _left.onClick.AddListener(OnClickLeft);
            _next.onClick.AddListener(OnClickNext);
            _right.onClick.AddListener(OnClickRight);
            _colorViewer.OnSetColorForActiveToggle += OnSetColorForActiveToggle;
            _colorViewer.OnGetRandomColors += OnGetRandomColors;

            _formsIndex = 0;
            SetUniform(_teamSettingsScreen.AvailableUniforms[_formsIndex]);
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

            SetUniform(_teamSettingsScreen.AvailableUniforms[_formsIndex]);
        }

        private void OnClickNext()
        {
            _uiFormAndLogoData.SetBackgroundUniform(_teamSettingsScreen.AvailableUniforms[_formsIndex]);

            _playerTeamData.SetTeamUniform(_teamSettingsScreen.AvailableUniforms[_formsIndex]);

            _teamSettingsScreen.State = TeamSettingsState.Logo;
        }

        private void OnClickRight()
        {
            if (_formsIndex + 1 >= _teamSettingsScreen.AvailableUniforms.Count) return;

            _formsIndex++;

            SetUniform(_teamSettingsScreen.AvailableUniforms[_formsIndex]);
        }

        private void OnSetColorForActiveToggle(Color color, int index)
        {
            FormLogoObject itemData = _teamSettingsScreen.AvailableUniforms[_formsIndex];

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
            _teamSettingsScreen.AvailableUniforms[_formsIndex] = itemData;
        }

        private void OnGetRandomColors(List<Color> colors)
        {
            FormLogoObject itemData = _teamSettingsScreen.AvailableLogos[_formsIndex];

            _uiFormAndLogoData.FirstForegroundLayer.color = colors[0];
            _uiFormAndLogoData.SecondForegroundLayer.color = colors[1];
            _uiFormAndLogoData.ThirdForegroundLayer.color = colors[2];
            itemData.FirstLayerColor = colors[0];
            itemData.SecondLayerColor = colors[1];
            itemData.ThirdLayerColor = colors[2];

            _teamSettingsScreen.AvailableLogos[_formsIndex] = itemData;
        }
        #endregion
    }
}