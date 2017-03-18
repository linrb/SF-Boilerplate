using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace SF.Web.Control.Editor
{
    public static class Config
    {
        public static string WebRootPath { get; set; }
        public static string ConfigFile { set; get; } = "config.json";
        public static bool noCache { set; get; } = true;

        #region Qiniu
        public static string AccessKey = "BoWcHqG7aHO6w2fVronGFSv4jzdWGYrSM9TTSGxM";     // ��ţ�ṩ�� AccessKey
        public static string SecretKey = "wnP3iGQsZJ8fZkXYlS-6S2HTn0X_p6Uozlcd8Oox";     // ��ţ�ṩ�� SecretKey
        public static string Bucket = "mayi";           // ����ţ���õĿռ���
        public static string Suffix = "";                 // ����ţ���õ�����ͼ��׺
        public static string Domain = "http://o7plv0g55.bkt.clouddn.com";   // ����ţ�󶨵�����

        #endregion

        private static JObject BuildItems()
        {
            var json = File.ReadAllText(ConfigFile);
            return JObject.Parse(json);
        }

        public static JObject Items
        {
            get
            {
                if (noCache || _Items == null)
                {
                    _Items = BuildItems();
                }
                return _Items;
            }
        }
        private static JObject _Items;


        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }

        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}