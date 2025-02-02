using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace Managers
{
    public class LanguageManager
    {
        private static LanguageData translations;
        private static RoleData roles;
        private static AchievementData achievements;
        public static string currentLang;
        public static string currentTheme;

        private static readonly string LANGUAGE_FILE_PATH = Path.Combine(FileManager.GetUserDataDirectory(), "language.json");

        public static void ChangeLanguage(string languageCode, bool isStart)
        {
            if (currentLang.Equals(languageCode, StringComparison.OrdinalIgnoreCase) && !isStart)
            {
                return;
            }

            try
            {
                string langFilePath = $"/com/rolegame/game/lang/{languageCode}.json";
                using (Stream inputStream = typeof(LanguageManager).Assembly.GetManifestResourceStream(langFilePath)) // Using typeof(LanguageManager)
                {
                    if (inputStream == null)
                    {
                        throw new FileNotFoundException(langFilePath);
                    }

                    using (var reader = new StreamReader(inputStream))
                    {
                        var json = reader.ReadToEnd();
                        translations = JsonUtility.FromJson<LanguageData>(json);
                    }

                    currentLang = languageCode;
                    ChangeTheme(currentTheme);
                    SaveLanguage(currentLang, currentTheme);
                    LoadAchievements(currentLang);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static void ChangeTheme(string theme)
        {
            try
            {
                string filePos = $"/com/rolegame/game/lang/roles/{currentLang}_{theme}.json";
                using (Stream inputStream = typeof(LanguageManager).Assembly.GetManifestResourceStream(filePos)) // Using typeof(LanguageManager)
                {
                    if (inputStream == null)
                    {
                        throw new FileNotFoundException("File could not be found: " + filePos);
                    }

                    using (var reader = new StreamReader(inputStream))
                    {
                        var json = reader.ReadToEnd();
                        roles = JsonUtility.FromJson<RoleData>(json);
                    }

                    currentTheme = theme;
                    SaveLanguage(currentLang, currentTheme);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static void LoadLanguageAndTheme()
        {
            try
            {
                if (File.Exists(LANGUAGE_FILE_PATH))
                {
                    var json = File.ReadAllText(LANGUAGE_FILE_PATH);
                    var data = JsonUtility.FromJson<LanguageThemeData>(json);
                    currentLang = data.Language;
                    currentTheme = data.Theme;
                }
                else
                {
                    currentLang = "en_us";
                    currentTheme = "normal";
                    SaveLanguage(currentLang, currentTheme);
                }
            }
            catch (IOException e)
            {
                Debug.LogError(e);
                currentLang = "en_us";
                currentTheme = "normal";
            }

            ChangeLanguage(currentLang, true);
            ChangeTheme(currentTheme);
            LoadAchievements(currentLang);
        }

        public static void LoadAchievements(string languageCode)
        {
            try
            {
                string filePos = $"/com/rolegame/game/lang/achievements/{languageCode}.json";
                using (Stream inputStream = typeof(LanguageManager).Assembly.GetManifestResourceStream(filePos)) // Using typeof(LanguageManager)
                {
                    if (inputStream == null)
                    {
                        throw new FileNotFoundException("Achievement file not found: " + filePos);
                    }

                    using (var reader = new StreamReader(inputStream))
                    {
                        var json = reader.ReadToEnd();
                        achievements = JsonUtility.FromJson<AchievementData>(json);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                achievements = null;
            }
        }

        public static string GetText(string category, string key)
        {
            if (translations != null && translations.categories.ContainsKey(category))
            {
                return translations.categories[category].GetValueOrDefault(key, key);
            }
            return key;
        }

        public static string GetRoleText(string category, string key)
        {
            if (roles != null && roles.categories.ContainsKey(category))
            {
                return roles.categories[category].GetValueOrDefault(key, key);
            }
            return key;
        }

        public static string GetAchievementText(string category, string key)
        {
            if (achievements != null && achievements.categories.ContainsKey(category))
            {
                return achievements.categories[category].GetValueOrDefault(key, key);
            }
            return key;
        }

        private static void SaveLanguage(string language, string theme)
        {
            try
            {
                var data = new LanguageThemeData(language, theme);
                var json = JsonUtility.ToJson(data, true);
                File.WriteAllText(LANGUAGE_FILE_PATH, json);
            }
            catch (IOException e)
            {
                Debug.LogError(e);
            }
        }

        [System.Serializable]
        private class LanguageData
        {
            public Dictionary<string, Dictionary<string, string>> categories;
        }

        [System.Serializable]
        private class RoleData
        {
            public Dictionary<string, Dictionary<string, string>> categories;
        }

        [System.Serializable]
        private class AchievementData
        {
            public Dictionary<string, Dictionary<string, string>> categories;
        }

        [System.Serializable]
        private class LanguageThemeData
        {
            public string Language;
            public string Theme;

            public LanguageThemeData(string language, string theme)
            {
                Language = language;
                Theme = theme;
            }

            public LanguageThemeData() { }
        }
    }
}
