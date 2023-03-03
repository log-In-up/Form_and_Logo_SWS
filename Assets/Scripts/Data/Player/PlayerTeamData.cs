using Items;

namespace Data.Player
{
    public sealed class PlayerTeamData
    {
        #region Fields
        private string _teamName;
        private FormLogoObject _logo, _uniform;
        #endregion

        #region Properties
        public FormLogoObject Logo => _logo;
        public FormLogoObject Uniform => _uniform;
        #endregion

        public PlayerTeamData()
        {

        }

        #region Public Methods
        public void SetTeamName(string teamName) => _teamName = teamName;

        internal void SetTeamUniform(FormLogoObject uniform) => _uniform = uniform;

        internal void SetTeamLogo(FormLogoObject logo) => _logo = logo;
        #endregion
    }
}