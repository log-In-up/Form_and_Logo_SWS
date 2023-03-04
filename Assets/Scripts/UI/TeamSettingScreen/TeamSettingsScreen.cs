using Data.Application;
using Data.Player;
using Items;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace UserInterface
{
    [DisallowMultipleComponent]
    public sealed class TeamSettingsScreen : MonoBehaviour, IDataPersistence
    {
        #region Editor fields
        [SerializeField] private Button _left = null;
        [SerializeField] private Button _next = null;
        [SerializeField] private Button _right = null;
        [SerializeField] private UniformAndLogoData _formAndLogoData = null;
        [SerializeField] private ColorViewer _colorViewer = null;
        [SerializeField] private ColorPalette _colorPalette = null;
        [SerializeField] private FormAndLogoUIData _uiFormAndLogoData;
        [SerializeField] private UIAnimatedObjectsData _uIAnimatedObjectsData;
        [SerializeField] private BackgroundData _backgroundData;
        [SerializeField] private UITeamNameData uiTeamNameData;
        #endregion

        #region Fields
        private TeamSettingsState _teamSettingsState;
        private ITeamSettingsState _screenState = null;
        private Dictionary<TeamSettingsState, ITeamSettingsState> _states;
        private PlayerTeamData _playerTeamData = null;

        private List<FormLogoObject> _availableUniforms = null, _availableLogos = null;
        #endregion

        #region Properties
        public TeamSettingsState State
        {
            get => _teamSettingsState;
            set
            {
                _teamSettingsState = value;

                _screenState.Close();
                _screenState = _states[_teamSettingsState];
                _screenState.Initialize();
            }
        }

        public List<FormLogoObject> AvailableUniforms { get => _availableUniforms; set => _availableUniforms = value; }
        public List<FormLogoObject> AvailableLogos { get => _availableLogos; set => _availableLogos = value; }
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _states = new Dictionary<TeamSettingsState, ITeamSettingsState>
            {
                [TeamSettingsState.Form] = new FormScreen(this, _playerTeamData, _left, _right, _next, _uiFormAndLogoData, _colorViewer),
                [TeamSettingsState.Logo] = new LogoScreen(this, _playerTeamData, _left, _right, _next, _uiFormAndLogoData,
                _colorViewer, _uIAnimatedObjectsData),
                [TeamSettingsState.TeamName] = new TeamNameScreen(_playerTeamData, _left, _right, _next, _backgroundData,
                _colorPalette, _colorViewer, _uIAnimatedObjectsData, uiTeamNameData)
            };

            _screenState = _states[TeamSettingsState.Form];
        }

        private void Start()
        {
            _screenState.Initialize();
        }
        #endregion

        #region Interface Implementation
        public void LoadData(GameData data)
        {
            _availableUniforms = new List<FormLogoObject>();
            _availableLogos ??= new List<FormLogoObject>();
            _playerTeamData = new PlayerTeamData();

            _playerTeamData.SetTeamName(data.playerTeamName);

            List<ItemData> uniforms = new List<ItemData>(_formAndLogoData.GetUniforms);
            for (int index = 0; index < uniforms.Count; index++)
            {
                List<Color> colors = new List<Color>();

                string[] colorHexes = data.uniforms[index].Split(',');

                foreach (string colorInHex in colorHexes)
                {
                    Color color = Utils.HexToColor(colorInHex);
                    colors.Add(color);
                }

                FormLogoObject formLogoObject = new FormLogoObject
                {
                    FirstLayer = uniforms[index].FirstLayer,
                    FirstLayerColor = colors[0],
                    SecondLayer = uniforms[index].SecondLayer,
                    SecondLayerColor = colors[1],
                    ThirdLayer = uniforms[index].ThirdLayer,
                    ThirdLayerColor = colors[2],
                    FourthLayer = uniforms[index].FourthLayer
                };
                _availableUniforms.Add(formLogoObject);
            }

            List<ItemData> logos = new List<ItemData>(_formAndLogoData.GetLogos);
            for (int index = 0; index < logos.Count; index++)
            {
                List<Color> colors = new List<Color>();

                string[] colorHexes = data.logos[index].Split(',');

                foreach (string colorInHex in colorHexes)
                {
                    Color color = Utils.HexToColor(colorInHex);
                    colors.Add(color);
                }

                FormLogoObject formLogoObject = new FormLogoObject
                {
                    FirstLayer = logos[index].FirstLayer,
                    FirstLayerColor = colors[0],
                    SecondLayer = logos[index].SecondLayer,
                    SecondLayerColor = colors[1],
                    ThirdLayer = logos[index].ThirdLayer,
                    ThirdLayerColor = colors[2],
                    FourthLayer = logos[index].FourthLayer
                };
                _availableLogos.Add(formLogoObject);
            }
        }

        public void NewGame(GameData data)
        {
            _availableUniforms = new List<FormLogoObject>();
            _availableLogos = new List<FormLogoObject>();

            //Create uniforms
            for (int index = 0; index < _formAndLogoData.GetUniforms.Count; index++)
            {
                ItemData uniform = _formAndLogoData.GetUniforms[index];
                List<Color> colors = _colorViewer.GetRandomColors(3);

                FormLogoObject uniformObject = new FormLogoObject
                {
                    FirstLayer = uniform.FirstLayer,
                    FirstLayerColor = colors[0],
                    SecondLayer = uniform.SecondLayer,
                    SecondLayerColor = colors[1],
                    ThirdLayer = uniform.ThirdLayer,
                    ThirdLayerColor = colors[2],
                    FourthLayer = uniform.FourthLayer
                };
                _availableUniforms.Add(uniformObject);

                List<string> colorsInHex = new List<string>
                {
                    uniformObject.FirstLayerColor.ToHexString(),
                    uniformObject.SecondLayerColor.ToHexString(),
                    uniformObject.ThirdLayerColor.ToHexString()
                };

                string value = string.Join(",", colorsInHex);

                data.uniforms.Add(index, value);
            }

            //Create logotypes
            for (int index = 0; index < _formAndLogoData.GetLogos.Count; index++)
            {
                ItemData logo = _formAndLogoData.GetLogos[index];
                List<Color> colors = _colorViewer.GetRandomColors(3);

                FormLogoObject logoObject = new FormLogoObject
                {
                    FirstLayer = logo.FirstLayer,
                    FirstLayerColor = colors[0],
                    SecondLayer = logo.SecondLayer,
                    SecondLayerColor = colors[1],
                    ThirdLayer = logo.ThirdLayer,
                    ThirdLayerColor = colors[2],
                    FourthLayer = logo.FourthLayer
                };
                _availableLogos.Add(logoObject);

                List<string> colorsInHex = new List<string>
                {
                    logoObject.FirstLayerColor.ToHexString(),
                    logoObject.SecondLayerColor.ToHexString(),
                    logoObject.ThirdLayerColor.ToHexString()
                };

                string value = string.Join(",", colorsInHex);

                data.logos.Add(index, value);
            }
        }

        public void SaveData(GameData data)
        {
            data.playerTeamName = _playerTeamData.TeamName;

            //Save uniforms
            for (int index = 0; index < _availableUniforms.Count; index++)
            {
                List<string> colorsInHex = new List<string>
                {
                    _availableUniforms[index].FirstLayerColor.ToHexString(),
                    _availableUniforms[index].SecondLayerColor.ToHexString(),
                    _availableUniforms[index].ThirdLayerColor.ToHexString(),
                };

                string value = string.Join(",", colorsInHex);

                data.uniforms[index] = value;
            }

            //Save logotypes
            for (int index = 0; index < _availableLogos.Count; index++)
            {
                List<string> colorsInHex = new List<string>
                {
                    _availableLogos[index].FirstLayerColor.ToHexString(),
                    _availableLogos[index].SecondLayerColor.ToHexString(),
                    _availableLogos[index].ThirdLayerColor.ToHexString(),
                };

                string value = string.Join(",", colorsInHex);

                data.logos[index] = value;
            }
        }
        #endregion
    }
}