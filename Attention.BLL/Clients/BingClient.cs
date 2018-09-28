using Attention.BLL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Attention.BLL.Clients
{
    public class BingClient
    {
        private readonly HttpClient _client;

        public BingClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://cn.bing.com/HPImageArchive.aspx");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            httpClient.DefaultRequestHeaders.Add("Cookie", "SRCHD=AF=NOFORM; SRCHUID=V=2&GUID=13DD9DD96C29405A80C6CDC936783ED9&dmnchg=1; _EDGE_V=1; MUID=1AF657053B94627F322F5B393ABB63E9; MUIDB=1AF657053B94627F322F5B393ABB63E9; BFBN=gRBFUDVu9A4J7pPjXUeT-ezT31YMdh0GS7lTTE5suCZAZw; ANON=A=8AC4A7383D0B3CA8FF579218FFFFFFFF&E=15ac&W=1; NAP=V=1.9&E=1552&C=h6GnnSffsZ-Ql34Rx5NZx-mRP3JCnBKR1YaJ0yCAX9sqf4mxI9KyTQ&W=1; ENSEARCH=BENVER=1; SRCHUSR=DOB=20180730&T=1538107234000; _EDGE_S=SID=2FF3F9A75F98669E0B50F5DF5EB667A6; _UR=MC=1; ipv6=hit=1538116466060&t=4; undefined=undefined=undefined; ULC=P=16A92|139:@39&H=16A92|139:39&T=16A92|139:39:7; _SS=SID=2FF3F9A75F98669E0B50F5DF5EB667A6&bIm=311413&HV=1538112880; SRCHHPGUSR=CW=150&CH=931&DPR=1.25&UTC=480&WTS=63673709657");

            _client = httpClient;
        }

        /// <summary>
        /// https://www.lylares.com/api-bing-wallpaper.html
        /// https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&pid=hp
        /// 1、format，非必要。返回结果的格式，不存在或者等于xml时，输出为xml格式，等于js时，输出json格式。
        /// 2、idx，非必要。不存在或者等于0时，输出当天的图片，-1为已经预备用于明天显示的信息，1则为昨天的图片，idx最多获取到前16天的图片信息。
        /// 3、n，必要参数。这是输出信息的数量。比如n=1，即为1条，以此类推，至多输出8条。
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetBingJsonAsync()
        {
            string format = "js";
            int idx = 0;
            int n = 8;
            string pid = "hp";
            int quiz = 1;
            int og = 1;

            IEnumerable<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>(nameof(format),format),
                    new KeyValuePair<string, object>(nameof(idx),idx),
                    new KeyValuePair<string, object>(nameof(n),n),
                    new KeyValuePair<string, object>(nameof(pid),pid),
                    new KeyValuePair<string, object>(nameof(quiz),quiz),
                    new KeyValuePair<string, object>(nameof(og),og)
                };

            UriBuilder builder = new UriBuilder(_client.BaseAddress);
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            foreach (KeyValuePair<string, object> kv in parameters)
            {
                query[kv.Key] = kv.Value.ToString();
            }

            builder.Query = query.ToString();
            string url = builder.ToString();

            HttpResponseMessage message = await _client.GetAsync(url).ConfigureAwait(false);
            message.EnsureSuccessStatusCode();
            string json = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            return json;
        }

        public async Task<BingObject> GetBingModelsAsync()
        {
            string json = await GetBingJsonAsync();
            Trace.WriteLine(json);

            BingObject model = JsonConvert.DeserializeObject<BingObject>(json);
            model.Images.ForEach(img =>
            {
                string originalString = _client.BaseAddress.OriginalString;
                string localPath = _client.BaseAddress.LocalPath;
                img.Url = originalString.Replace(localPath, img.Url);
                img.Urlbase = originalString.Replace(localPath, img.Urlbase);
                img.Copyrightlink = originalString.Replace(localPath, img.Copyrightlink);
                img.Quiz = originalString.Replace(localPath, img.Quiz);
            });
            return model;
        }
    }
}
