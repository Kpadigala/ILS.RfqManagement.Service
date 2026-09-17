using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ILS.RfqManagement.Tests
{
    public static class ObjectResultExtensions
    {
        /// <summary>
        /// Converts the Value property to an instance of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T ConvertValueTo<T>(this ObjectResult self)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(self.Value));
        }
    }
}