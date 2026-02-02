using System.Globalization;

namespace HsUnpack
{
    /// <summary>
    /// 语言辅助类，用于管理应用程序的多语言支持
    /// Language helper class for managing application multi-language support
    /// </summary>
    public static class LanguageHelper
    {
        /// <summary>
        /// 获取当前系统语言的文化信息
        /// Gets the current system language culture info
        /// </summary>
        public static CultureInfo GetSystemCulture()
        {
            return CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// 获取当前语言的显示名称
        /// Gets the display name of the current language
        /// </summary>
        public static string GetCurrentLanguageName()
        {
            var culture = CultureInfo.CurrentUICulture;
            return culture.TwoLetterISOLanguageName switch
            {
                "zh" => "中文",
                "en" => "English",
                "ja" => "日本語",
                _ => culture.DisplayName
            };
        }

        /// <summary>
        /// 设置应用程序语言（需要重启窗体才能生效）
        /// Sets the application language (requires form restart to take effect)
        /// </summary>
        public static void SetLanguage(string cultureName)
        {
            var culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
        }

        /// <summary>
        /// 获取支持的语言列表
        /// Gets the list of supported languages
        /// </summary>
        public static Dictionary<string, string> GetSupportedLanguages()
        {
            return new Dictionary<string, string>
            {
                { "zh-CN", "中文 (Chinese)" },
                { "en", "English" },
                { "ja", "日本語 (Japanese)" }
            };
        }
    }
}
