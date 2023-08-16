using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Sequence.Provider;
using UnityEngine;
using UnityEngine.Networking;

namespace Sequence.WaaS
{
    public class HttpClient
    {
        private readonly string _url = "https://next-api.sequence.app/rpc/Wallet";
        private Dictionary<string, string> _defaultHeaders;

        public HttpClient()
        {
            this._defaultHeaders = new Dictionary<string, string>();
        }

        public void AddDefaultHeader(string key, string value)
        {
            this._defaultHeaders[key] = value;
        }

        public async Task<T2> SendRequest<T, T2>(T args, [CanBeNull] Dictionary<string, string> headers = null)
        {
            string requestJson = JsonConvert.SerializeObject(args);
            UnityWebRequest request = UnityWebRequest.Post(_url, requestJson);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");
            request.method = UnityWebRequest.kHttpVerbPOST;

            if (headers == null)
            {
                headers = _defaultHeaders;
            }
            
            foreach (string key in headers.Keys)
            {
                request.SetRequestHeader(key, headers[key]);
            }

            await request.SendWebRequest();
            
            if (request.error != null || request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Error sending request to {_url} with args {args}: {request.error}");
                request.Dispose();
                return default;
            }
            else
            {
                byte[] results = request.downloadHandler.data;
                var responseJson = Encoding.UTF8.GetString(results);
                T2 result = JsonConvert.DeserializeObject<T2>(responseJson);
                request.Dispose();
                return result;
            }
        }
    }
}