using Data.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{
    public class TeamNameScreen : ITeamSettingsState
    {
        #region Fields
        private PlayerTeamData _playerTeamData = null;
        #endregion

        public TeamNameScreen(PlayerTeamData playerTeamData)
        {
            _playerTeamData = playerTeamData;
        }

        #region Interface Implementation
        public void Close()
        {

        }

        public void Initialize()
        {

        }
        #endregion
    }
}