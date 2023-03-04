namespace Data.Application
{
    [System.Serializable]
    public class GameData
    {
        public string playerTeamName;
        public SerializableDictionary<int, string> uniforms;
        public SerializableDictionary<int, string> logos;

        public GameData()
        {
            playerTeamName = string.Empty;
            uniforms = new SerializableDictionary<int, string>();
            logos = new SerializableDictionary<int, string>();
        }
    }
}