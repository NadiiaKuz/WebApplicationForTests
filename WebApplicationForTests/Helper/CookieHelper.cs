using System.Text.Json;
using WebApplicationForTests.Models.BindingModels;

namespace WebApplicationForTests.Helper
{
    public static class CookieHelper
    {
        public static void AddResult(HttpContext httpContext, int testId, decimal score)
        {
            var results = GetResults(httpContext);

            var testData = new TestResultBindingModel() { TestId = testId, Score = score };

            results.Add(testData);

            AddToCookie(httpContext, results);
        }

        private static void AddToCookie(HttpContext httpContext, List<TestResultBindingModel> results)
        {
            string serializedTestData = JsonSerializer.Serialize(results);

            httpContext.Response.Cookies.Append("TestResult", serializedTestData);
        }

        public static List<TestResultBindingModel> GetResults(HttpContext httpContext)
        {
            try
            {

                if (httpContext.Request.Cookies.TryGetValue("TestResult", out string jsonData))
                {
                    return JsonSerializer.Deserialize<List<TestResultBindingModel>>(jsonData);
                }
            }
            catch (Exception ex)
            { 
            }

            return new List<TestResultBindingModel>();
        }

        public static void ClearResults(HttpContext httpContext)
        {
            AddToCookie(httpContext, new());
        }
    }
}
