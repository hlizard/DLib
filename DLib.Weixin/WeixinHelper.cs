using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace DLib.Weixin
{
    public static class WeixinHelper
    {
        public static void SendTextMessage(string touser, string content)
        {
            //获取access token
            string access_token = GetAccessToken();

            if (!string.IsNullOrEmpty(access_token))
            {
                string sendMessageUrlTemplate = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
                string sendMessageUrl = string.Format(sendMessageUrlTemplate, access_token);
                string message = content;
                //message = Encoding.UTF8.GetString(Encoding.Default.GetBytes(message));    //message原本是utf16按utf8读取后自然是乱码
                //message = HttpUtility.UrlEncodeUnicode(message);  //这没问题，问题是结果还是utf16的string
                string textMessageArgumentTemplate = "{{\"touser\": \"{0}\", \"msgtype\": \"text\", \"text\":{{\"content\": \"{1}\"}}}}";
                string textMessageArgument = string.Format(textMessageArgumentTemplate, touser, message);
                var result = "";
                try
                {
                    using (var webClient = new WebClient())
                    {
                        //result = webClient.UploadString(sendMessageUrl, textMessageArgument); //参数为utf16的string，也没有其他参数指示编码，所以微信服务器将始终按其默认的utf8读取，所以出现乱码是必然的
                        result = Encoding.UTF8.GetString(webClient.UploadData(sendMessageUrl, Encoding.UTF8.GetBytes(textMessageArgument)));    //所以字节数组参数更强大
                        Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                        var errcode = values["errcode"];
                        if (errcode != "0")
                        {
                            throw new Exception(string.Format("服务器返回“{0}”", result));
                        }
                    }
                }
                catch (Exception ex)
                {
                    var wtf = ex.Message;
                    throw new Exception(string.Format("发送微信消息失败，原因：{0} textMessageArgument：{1}", wtf, textMessageArgument));
                }
            }
        }

        public static string GetAccessToken()
        {
            //若已获取的access_token有效则直接将其返回
            if (!string.IsNullOrWhiteSpace(ACCESS_TOKEN))
            {
                if (ACCESS_TOKEN_CREATEDATE.HasValue &&
                    (DateTime.Now - ACCESS_TOKEN_CREATEDATE.Value).TotalSeconds < EXPIRES_IN)
                {
                    return ACCESS_TOKEN;
                }
            }
            string getAccess_tokenUrlTemplate = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
            string getAccess_tokenUrl = string.Format(getAccess_tokenUrlTemplate, _appid, _appsecret);
            {
                var result = "";
                try
                {
                    using (var webClient = new WebClient())
                    {
                        using (var stream = webClient.OpenRead(getAccess_tokenUrl))
                        {
                            using (var streamReader = new StreamReader(stream))
                            {
                                result = streamReader.ReadToEnd();
                                Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                                ACCESS_TOKEN = values["access_token"];
                                EXPIRES_IN = Convert.ToDouble(values["expires_in"]);
                                ACCESS_TOKEN_CREATEDATE = DateTime.Now;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var wtf = ex.Message;
                    throw new Exception(string.Format("获取ACCESS_TOKEN失败，原因：{0}", wtf));
                }
            }
            return ACCESS_TOKEN;
        }

        public static string _appid;
        public static string _appsecret;

        private static string ACCESS_TOKEN = null;
        private static DateTime? ACCESS_TOKEN_CREATEDATE = null;
        private static double EXPIRES_IN = 7200.0;

        public static string APPID
        {
            get
            {
                if (_appid == null)
                {
                    _appid = DConfig.GetConfig("APPID");
                    if (_appid == null)
                    {
                        throw new Exception("未配置APPID");
                    }
                }
                return _appid;
            }
        }

        public static string APPSECRET
        {
            get
            {
                if (_appsecret == null)
                {
                    _appsecret = DConfig.GetConfig("APPSECRET");
                    if (_appsecret == null)
                    {
                        throw new Exception("未配置APPSECRET");
                    }
                }
                return _appsecret;
            }
        }
    }
}