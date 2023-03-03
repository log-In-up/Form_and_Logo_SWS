using Data.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class LogoScreen : ITeamSettingsState
    {
        #region Fields
        private readonly Button _left = null, _next = null, _right = null;

        private PlayerTeamData _playerTeamData = null;
        private TeamSettingsScreen _teamSettingsScreen = null;
        private ColorViewer _colorViewer = null;
        private FormAndLogoUIData _uiFormAndLogoData;

        #endregion

        public LogoScreen(TeamSettingsScreen teamSettingsScreen, PlayerTeamData playerTeamData, Button left, Button right, Button next, FormAndLogoUIData formAndLogoUIData, ColorViewer colorViewer)
        {
            _teamSettingsScreen = teamSettingsScreen;
            _playerTeamData = playerTeamData;

            _left = left;
            _next = next;
            _right = right;

            _uiFormAndLogoData = formAndLogoUIData;
            _colorViewer = colorViewer;
        }

        #region Interface Implementation
        public void Close()
        {
            _left.onClick.RemoveListener(OnClickLeft);
            _next.onClick.RemoveListener(OnClickNext);
            _right.onClick.RemoveListener(OnClickRight);
        }

        public void Initialize()
        {
            _left.onClick.AddListener(OnClickLeft);
            _next.onClick.AddListener(OnClickNext);
            _right.onClick.AddListener(OnClickRight);
        }
        #endregion

        #region Button handlers
        private void OnClickLeft()
        {

        }

        private void OnClickNext()
        {

        }

        private void OnClickRight()
        {

        }
        #endregion
    }
}