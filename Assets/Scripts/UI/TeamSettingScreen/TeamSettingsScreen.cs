using Data.Application;
using Data.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    [DisallowMultipleComponent]
    public sealed class TeamSettingsScreen : MonoBehaviour
    {
        #region Editor fields
        [SerializeField] private Button _left = null;
        [SerializeField] private Button _next = null;
        [SerializeField] private Button _right = null;
        [SerializeField] private Image _background = null;
        [SerializeField] private UniformAndLogoData _formAndLogoData = null;
        [SerializeField] private ColorViewer _colorViewer = null;
        [SerializeField] private FormAndLogoUIData _uiFormAndLogoData;
        [SerializeField] private UIAnimatedObjectsData _uIAnimatedObjectsData;
        #endregion

        #region Fields
        private TeamSettingsState _teamSettingsState;
        private ITeamSettingsState _screenState = null;
        private Dictionary<TeamSettingsState, ITeamSettingsState> _states;
        private PlayerTeamData _playerTeamData = null;
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
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _playerTeamData = new PlayerTeamData();

            _states = new Dictionary<TeamSettingsState, ITeamSettingsState>
            {
                [TeamSettingsState.Form] = new FormScreen(this, _playerTeamData, _left, _right, _next, _uiFormAndLogoData,
                _colorViewer, _formAndLogoData.GetUniforms),
                [TeamSettingsState.Logo] = new LogoScreen(this, _playerTeamData, _left, _right, _next, _uiFormAndLogoData,
                _colorViewer, _formAndLogoData.GetLogos, _uIAnimatedObjectsData),
                [TeamSettingsState.TeamName] = new TeamNameScreen(_playerTeamData)
            };
        }

        private void Start()
        {
            _screenState = _states[TeamSettingsState.Form];
            _screenState.Initialize();
        }
        #endregion
    }
}