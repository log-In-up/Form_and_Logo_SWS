using Data.Application;
using Data.Player;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class LogoScreen : ITeamSettingsState
    {
        #region Fields
        private int _logosIndex;
        private readonly Button _left = null, _next = null, _right = null;

        private PlayerTeamData _playerTeamData = null;
        private TeamSettingsScreen _teamSettingsScreen = null;
        private ColorViewer _colorViewer = null;
        private FormAndLogoUIData _uiFormAndLogoData;
        private List<FormLogoObject> _availableLogos = null;
        private List<ItemData> _logos;
        private UIAnimatedObjectsData _uiAnimatedObjectsData;
        #endregion

        public LogoScreen(TeamSettingsScreen teamSettingsScreen, PlayerTeamData playerTeamData, Button left, Button right, Button next, FormAndLogoUIData formAndLogoUIData, ColorViewer colorViewer, List<ItemData> logos, UIAnimatedObjectsData uiAnimatedObjectsData)
        {
            _teamSettingsScreen = teamSettingsScreen;
            _playerTeamData = playerTeamData;

            _left = left;
            _next = next;
            _right = right;

            _uiFormAndLogoData = formAndLogoUIData;
            _colorViewer = colorViewer;

            _availableLogos = new List<FormLogoObject>();
            _logos = logos;

            _uiAnimatedObjectsData = uiAnimatedObjectsData;
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

            foreach (ItemData logos in _logos)
            {
                List<Color> colors = _colorViewer.GetRandomColors(3);

                FormLogoObject formLogoObject = new FormLogoObject
                {
                    FirstLayer = logos.FirstLayer,
                    FirstLayerColor = colors[0],
                    SecondLayer = logos.SecondLayer,
                    SecondLayerColor = colors[1],
                    ThirdLayer = logos.ThirdLayer,
                    ThirdLayerColor = colors[2],
                    FourthLayer = logos.FourthLayer
                };

                _availableLogos.Add(formLogoObject);

            }

            _logosIndex = 0;
            SetLogo();

            _teamSettingsScreen.StartCoroutine(LaunchAnimation());
        }
        #endregion

        #region Button handlers
        private void OnClickLeft()
        {
            if (_logosIndex <= 0) return;

            _logosIndex--;

            SetLogo();
        }

        private void OnClickNext()
        {
            _playerTeamData.SetTeamLogo(_availableLogos[_logosIndex]);

            _teamSettingsScreen.State = TeamSettingsState.TeamName;
        }

        private void OnClickRight()
        {
            if (_logosIndex + 1 >= _availableLogos.Count) return;

            _logosIndex++;

            SetLogo();
        }

        private void OnSetColorForActiveToggle(Color color, int index)
        {
            FormLogoObject itemData = _availableLogos[_logosIndex];

            switch (index)
            {
                case 0:
                    _uiFormAndLogoData.FirstLogoForegroundLayer.color = color;
                    itemData.FirstLayerColor = color;
                    break;
                case 1:
                    _uiFormAndLogoData.FirstLogoForegroundLayer.color = color;
                    itemData.SecondLayerColor = color;
                    break;
                case 2:
                    _uiFormAndLogoData.FirstLogoForegroundLayer.color = color;
                    itemData.ThirdLayerColor = color;
                    break;
                default:
                    break;
            }

            _availableLogos[_logosIndex] = itemData;
        }
        #endregion

        #region Methods
        private void SetLogo()
        {
            FormLogoObject logo = _availableLogos[_logosIndex];

            _uiFormAndLogoData.SetLogo(logo);

            List<Color> colors = new List<Color>
            {
                logo.FirstLayerColor,
                logo.SecondLayerColor,
                logo.ThirdLayerColor
            };

            _colorViewer.SetVieverColors(colors);
        }
        #endregion

        #region Corotines
        private IEnumerator LaunchAnimation()
        {
            _next.interactable = false;

            float time = _uiAnimatedObjectsData.TimeToWait;
            string name = "MovementIsOn";

            List<GameObject> logoAndBackgroundObjects = new List<GameObject>
            {
                _uiAnimatedObjectsData.Logo,
                _uiAnimatedObjectsData.FormOnBackground,
                _uiAnimatedObjectsData.TopLogoText
            };

            foreach (GameObject backgroundObject in logoAndBackgroundObjects)
            {
                backgroundObject.SetActive(true);
                if (backgroundObject.TryGetComponent(out Animator backgroundAnimator))
                {
                    backgroundAnimator.SetBool(name, true);
                }
            }

            List<GameObject> foregroundObjects = new List<GameObject>
            {
                _uiAnimatedObjectsData.FormOnForeground,
                _uiAnimatedObjectsData.TopUniformText
            };

            foreach (GameObject foregroundObject in foregroundObjects)
            {
                if (foregroundObject.TryGetComponent(out Animator foregroundAnimator))
                {
                    foregroundAnimator.SetBool(name, true);
                }
            }

            while (time > 0.0f)
            {
                time -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            foreach (GameObject item in foregroundObjects)
            {
                item.SetActive(false);
            }

            _next.interactable = true;
        }
        #endregion
    }
}