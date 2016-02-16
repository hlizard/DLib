using System;
using System.Collections;
using System.Text;
using DLib.Weixin;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.Helpers
{
    public static class JSSDKHelperEx
    {

        public static Model4getsignature getsignature(this JSSDKHelper jssdkHelper, string url)
        {
            return getsignature(url);
        }

        public static Model4getsignature getsignature(string url)
        {
            var obj = new Model4getsignature();
            obj.Url = url;
            if (!string.IsNullOrWhiteSpace(url))
            {
                string ticket = string.Empty;
                obj.timestamp = Convert.ToInt32(JSSDKHelper.GetTimestamp());
                obj.nonceStr = JSSDKHelper.GetNoncestr();
                obj.appId = WeixinHelper.APPID;
                ticket = AccessTokenContainer.TryGetJsApiTicket(obj.appId, WeixinHelper.APPSECRET);
                obj.signature = JSSDKHelper.GetSignature(ticket, obj.nonceStr, obj.timestamp.ToString(), url);
            }
            return obj;
        }

    }

    public class Model4getsignature
    {
        int _timestamp;
        string _nonceStr;
        string _signature;
        private string _appId;
        private string url;

        public int timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public string nonceStr
        {
            get { return _nonceStr; }
            set { _nonceStr = value; }
        }

        public string signature
        {
            get { return _signature; }
            set { _signature = value; }
        }

        public string appId
        {
            get { return _appId; }
            set { _appId = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
    }
}
