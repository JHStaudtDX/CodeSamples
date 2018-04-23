using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Relativity.Services.Objects.DataContracts;


namespace RelConsoleHub
{
    class AuditAPIReviewerStatisticsReport
    {
        public class Report
        {
            public string FullName { get; set; }
            public int UserId { get; set; }
            public string TotalUsageTime { get; set; }
            public int Views { get; set; }
            public int DistinctViews { get; set; }
            public int Edits { get; set; }
            public int DistinctEdits { get; set; }
            public float EditsPerHour { get; set; }
            public float EditsPerDay { get; set; }
            public float DistinctEditsPerHour { get; set; }
            public float DistinctEditsPerDay { get; set; }
            public int MassEdits { get; set; }
            public int DistinctMassEdits { get; set; }
            public float DistinctMassEditsPerHour { get; set; }
            public float DistinctMassEditsPerDay { get; set; }
            public float MassEditsPerHour { get; set; }
            public float MassEditsPerDay { get; set; }
            public int Propagations { get; set; }
            public int DistinctPropagations { get; set; }
            public float DistinctMassPropagationsPerHour { get; set; }
            public float DistinctMassPropagationsPerDay { get; set; }
            public float PropagationsPerHour { get; set; }
            public float PropagationsPerDay { get; set; }
        }
        
        public static async Task<QueryResult> RevStatsEx()
        {
            QueryResult result = null;
            using (HttpClient client = new HttpClient())
            {
                //Set Variables, add -services to R1 URL.
                var workspaceID = "WORKSPACEID";
                var _user = "USERNAME";
                var _pass = "PASSWORD";
                var url = $"https://< YOURURL >-services.relativity.one/Relativity.REST/api/Relativity.Objects.Audits/workspaces/{workspaceID}/reviewerstatistics/query";

                //Set Headers, create JSON request
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($"{_user}:{_pass}")));
                client.DefaultRequestHeaders.Add("X-CSRF-Header", string.Empty);                
                string inputJSON = "{\"reviewerStatsDataRequest\": {\"startDate\": \"2010-11-01T00:00:00Z\"," +
                    "\"endDate\": \"2018-11-30T00:00:00Z\",\"timeZone\": \"-06.0\",\"downTimeThreshold\": \"900\"" +
                    ",\"nonAdmin\": false,\"additionalActions\": \"None\"}}";        
                
                //Execute Query
                try
                {
                    var response = await client.PostAsync(url, new StringContent(inputJSON, Encoding.UTF8, "application/json"));
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var results = JsonConvert.DeserializeObject<Report[]>(content);
                    
                    //Iterate through results & print                
                    foreach (var item in results)
                    {
                        Console.WriteLine("WorkspaceID: " + workspaceID);
                        foreach(var prop in item.GetType().GetProperties())
                        {
                           
                            Console.WriteLine(prop.Name + ": " + prop.GetValue(item));
                        }                       
                        Console.WriteLine("\r\n");
                    }                    
                }
                catch (Exception ex)
                { Console.WriteLine(ex); }                
                return result;
            }
        }
    }
}



