using Data.Application;
using Data.Player;
using Items;
using SaveSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class TeamNameScreen : ITeamSettingsState
    {
        #region Fields
        private readonly Button _left = null, _next = null, _right = null;
        private readonly BackgroundData _backgroundData;
        private readonly ColorPalette _colorPalette = null;
        private readonly ColorViewer _colorViewer = null;
        private readonly PlayerTeamData _playerTeamData = null;
        private readonly UIAnimatedObjectsData _uiAnimatedObjectsData;
        private readonly UITeamNameData _uiTeamNameData;
        #endregion

        public TeamNameScreen(PlayerTeamData playerTeamData, Button left, Button right, Button next, BackgroundData backgroundData, ColorPalette colorPalette, ColorViewer colorViewer, UIAnimatedObjectsData uIAnimatedObjectsData, UITeamNameData uiTeamNameData)
        {
            _playerTeamData = playerTeamData;

            _left = left;
            _right = right;
            _next = next;

            _backgroundData = backgroundData;
            _colorPalette = colorPalette;
            _colorViewer = colorViewer;
            _uiAnimatedObjectsData = uIAnimatedObjectsData;
            _uiTeamNameData = uiTeamNameData;
        }

        #region Interface Implementation
        public void Close()
        {
            _backgroundData.Background.sprite = _backgroundData.Room;

            List<GameObject> logoAndBackgroundObjects = new List<GameObject>
            {
                _uiAnimatedObjectsData.Logo,
                _uiAnimatedObjectsData.FormOnBackground,
                _uiAnimatedObjectsData.TopLogoText,
                _colorPalette.gameObject,
                _colorViewer.gameObject,
                _left.gameObject,
                _right.gameObject
            };

            foreach (GameObject item in logoAndBackgroundObjects)
            {
                item.SetActive(true);
            }

            _uiTeamNameData.InputField.gameObject.SetActive(false);
            _uiTeamNameData.TopTeamText.SetActive(false);
            _uiTeamNameData.InputTeamNameText.SetActive(false);
            _uiTeamNameData.LogoHolder.SetActive(false);
            _uiTeamNameData.UniformHolder.SetActive(false);

            _uiTeamNameData.InputField.onEndEdit.RemoveListener(OnEndEditTeamName);
            _uiTeamNameData.InputField.onValueChanged.RemoveListener(OnTeamValueChange);
        }

        public void Initialize()
        {
            _backgroundData.Background.sprite = _backgroundData.Stadium;

            List<GameObject> logoAndBackgroundObjects = new List<GameObject>
            {
                _uiAnimatedObjectsData.Logo,
                _uiAnimatedObjectsData.FormOnBackground,
                _uiAnimatedObjectsData.TopLogoText,
                _colorPalette.gameObject,
                _colorViewer.gameObject,
                _left.gameObject,
                _right.gameObject
            };

            foreach (GameObject item in logoAndBackgroundObjects)
            {
                item.SetActive(false);
            }

            _uiTeamNameData.InputField.gameObject.SetActive(true);
            _uiTeamNameData.TopTeamText.SetActive(true);
            _uiTeamNameData.InputTeamNameText.SetActive(true);
            _uiTeamNameData.LogoHolder.SetActive(true);
            _uiTeamNameData.UniformHolder.SetActive(true);

            _uiTeamNameData.InputField.onEndEdit.AddListener(OnEndEditTeamName);
            _uiTeamNameData.InputField.onValueChanged.AddListener(OnTeamValueChange);

            _uiTeamNameData.SetLogoData(_playerTeamData.Logo);
            _uiTeamNameData.SetUniformData(_playerTeamData.Uniform);

            _uiTeamNameData.InputField.text = _playerTeamData.TeamName != string.Empty ? _playerTeamData.TeamName : string.Empty;

        }
        #endregion

        #region Button handlers
        private void OnEndEditTeamName(string teamName)
        {
            if (teamName.Length > _uiTeamNameData.TextLengthLimit)
            {
                _uiTeamNameData.InputField.text = string.Empty;
                _uiTeamNameData.TeamNameText.text = string.Empty;

                _uiTeamNameData.TeamNameText.color = _uiTeamNameData.TextColorWhenLimitIsNormalized;

                _next.interactable = true;
                return;
            }
            _playerTeamData.SetTeamName(teamName);
            DataPersistenceManager.Instance.SaveGame();
        }

        private void OnTeamValueChange(string teamName)
        {
            if (teamName.Length > _uiTeamNameData.TextLengthLimit)
            {
                _uiTeamNameData.TeamNameText.color = _uiTeamNameData.TextColorWhenLimitIsExceeded;
                _next.interactable = false;
            }
            else
            {
                _uiTeamNameData.TeamNameText.color = _uiTeamNameData.TextColorWhenLimitIsNormalized;
                _next.interactable = true;
            }
        }
        #endregion
    }
}