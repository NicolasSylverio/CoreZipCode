using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreZipCode.Interfaces
{
    public abstract class PostCodeBaseService : ApiHandler
    {
        protected PostCodeBaseService(HttpClient request) : base(request) { }

        public virtual string Execute(string postcode) => CallApi(SetPostCodeUrl(postcode));

        public virtual async Task<string> ExecuteAsync(string postcode) => await CallApiAsync(SetPostCodeUrl(postcode));

        public virtual T GetPostcode<T>(string postcode) => JsonConvert.DeserializeObject<T>(CallApi(SetPostCodeUrl(postcode)));

        public virtual async Task<T> GetPostcodeAsync<T>(string postcode) => JsonConvert.DeserializeObject<T>(await CallApiAsync(SetPostCodeUrl(postcode)));

        public abstract string SetPostCodeUrl(string postcode);
    }
}