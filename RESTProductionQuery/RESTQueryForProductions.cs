 public static async Task<QueryResult> QueryExample()
        {
            try
            {
                QueryResult result = null;
                using (HttpClient client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("YOURUSERNAME:YOURPASSWORD")));
                    var url = "YOURURL/Relativity.REST/api/Relativity.Objects/workspace/WORKSPACEARTIFACTID/object/query";
                    string inputJSON = "{\"Request\":{\"ObjectType\":{\"Guid\":\"24190650-2E73-4373-B0C4-F31142CBF300\"},\"fields\":[{\"ArtifactID\":FIELDARTIFACTIDHERE}]},\"start\":0,\"length\":100}";

                    var response = await client.PostAsync(url, new StringContent(inputJSON, Encoding.UTF8, "application/json"));
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<QueryResult>(content);
                                    
                    foreach(var item in result.Objects.SelectMany(x => x.FieldValues).ToList())
                    {
                        Console.WriteLine(item.Value + " " + item.Field.ArtifactID.ToString());

                    }                    
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                QueryResult result = new QueryResult();
                return result;
            }
        }