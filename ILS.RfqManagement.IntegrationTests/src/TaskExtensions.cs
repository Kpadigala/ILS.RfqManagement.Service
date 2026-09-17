using System.Threading.Tasks;

namespace ILS.RfqManagement.IntegrationTests
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Synchronously executes the Task and retrieves the result, preserving any exceptions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T SafeResult<T>(this Task<T> self)
        {
            return self.GetAwaiter().GetResult();
        }
    }
}