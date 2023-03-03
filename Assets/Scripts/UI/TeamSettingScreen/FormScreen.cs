using Data.Player;
using Data.Application;
using Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
        private UIAnimatedObjectsData _uiAnimatedObjectsData;
        #endregion

        public FormScreen(TeamSettingsScreen teamSettingsScreen, PlayerTeamData playerTeamData, Button left, Button right, Button next, FormAndLogoUIData uiFormAndLogoData, ColorViewer colorViewer, List<ItemData> uniforms, UIAnimatedObjectsData uiAnimatedObjectsData)
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
            SetUniform();
        }
        #endregion

        #region Methods
        private void SetUniform()
        {
            FormLogoObject itemData = _availableUniforms[_formsIndex];

            _uiFormAndLogoData.FirstForegroundLayer.sprite = itemData.FirstLayer;
            _uiFormAndLogoData.FirstForegroundLayer.color = itemData.FirstLayerColor;

            _uiFormAndLogoData.SecondForegroundLayer.sprite = itemData.SecondLayer;
            _uiFormAndLogoData.SecondForegroundLayer.color = itemData.SecondLayerColor;

            _uiFormAndLogoData.ThirdForegroundLayer.sprite = itemData.ThirdLayer;
            _uiFormAndLogoData.ThirdForegroundLayer.color = itemData.ThirdLayerColor;

            _uiFormAndLogoData.FourthForegroundLayer.sprite = itemData.FourthLayer;

            List<Color> colors = new List<Color>
            {
                itemData.FirstLayerColor,
                itemData.SecondLayerColor,
                itemData.ThirdLayerColor
            };

            _colorViewer.SetVieverColors(colors);
        }

        private void SetBackgroundUniform()
        {
            FormLogoObject itemData = _availableUniforms[_formsIndex];

            _uiFormAndLogoData.FirstBackgroundLayer.sprite = itemData.FirstLayer;
            _uiFormAndLogoData.FirstBackgroundLayer.color = itemData.FirstLayerColor;

            _uiFormAndLogoData.SecondBackgroundLayer.sprite = itemData.SecondLayer;
            _uiFormAndLogoData.SecondBackgroundLayer.color = itemData.SecondLayerColor;

            _uiFormAndLogoData.ThirdBackgroundLayer.sprite = itemData.ThirdLayer;
            _uiFormAndLogoData.ThirdBackgroundLayer.color = itemData.ThirdLayerColor;

            _uiFormAndLogoData.FourthBackgroundLayer.sprite = itemData.FourthLayer;
        }
        #endregion

        #region Button handlers
        private void OnClickLeft()
        {
            if (_formsIndex <= 0) return;

            _formsIndex--;

            SetUniform();
        }

        private void OnClickNext()
        {
            SetBackgroundUniform();

            _playerTeamData.SetTeamUniform(_availableUniforms[_formsIndex]);

            _teamSettingsScreen.StartCoroutine(LaunchAnimation());
        }

        private void OnClickRight()
        {
            if (_formsIndex + 1 >= _availableUniforms.Count) return;

            _formsIndex++;

            SetUniform();
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

        #region Corotines
        private IEnumerator LaunchAnimation()
        {
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

            foreach(GameObject foregroundObject in foregroundObjects)
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

            _teamSettingsScreen.State = TeamSettingsState.Logo;
        }
        #endregion
    }
}