using KeePass.App.Configuration;

namespace KPGenUserName
{
    public static class CustomConfigEx
    {
        public static void SetCurrentTransliterationName(this AceCustomConfig customConfig, string name)
        {
            customConfig.SetString("CurrentTransliteration", name);
        }

        public static string GetCurrentTransliterationName(this AceCustomConfig customConfig, string defaultValue = "")
        {
            return customConfig.GetString("CurrentTransliteration", defaultValue);
        }
    }
}