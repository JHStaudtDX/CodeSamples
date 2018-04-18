using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kCura.Relativity.Client;
using Relativity.Services.ServiceProxy;
using UsernamePasswordCredentials = kCura.Relativity.Client.UsernamePasswordCredentials;

namespace FindGuid
{
    class ConnectionManager
    {
       
            // public static readonly string[] credInfo = File.ReadAllLines(@"‪C:\HubCreds\ConsoleCreds.txt");
            public static readonly string _userName = "relativity.admin@relativity.com"; //credInfo[1];"Relativity.Admin@kcura.com";//

            public static readonly string _password = "Test1234!";
        public static readonly string BaseRelativityURL = "https://kcura-ea-sandbox-services.relativity.one/Relativity"; //"http://192.168.137.96/Relativity";//"http://192.168.137.202/Relativity";//"https://yr7p8tdqx6mv.hopper.relativity.com/Relativity";//"http://192.168.137.202/Relativity";//"https://xadrqekxazj2.hopper.relativity.com/Relativity"; //credInfo[0];"https://pd-dev-02-services.r1.kcura.com/Relativity"; //
            public static int WorkspaceID = 1018783;

            public static Uri WebApiUri => new Uri(BaseRelativityURL + "webapi/");
            public static Uri ServicesUri => new Uri(BaseRelativityURL + ".Services");
            public static Uri RestUri => new Uri(BaseRelativityURL + ".REST/api");




            public IRSAPIClient GetRsapiClient()
            {
                try
                {
                    IRSAPIClient proxy = new RSAPIClient
                        (ServicesUri, new UsernamePasswordCredentials(_userName, _password));
                    proxy.APIOptions.WorkspaceID = WorkspaceID;
                    return proxy;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to connect to RSAPI. " + ex);
                    throw;
                }
            }



            public ServiceFactory GetServiceFactory()
            {
                try
                {
                    Relativity.Services.ServiceProxy.UsernamePasswordCredentials credsService =
                        new Relativity.Services.ServiceProxy.UsernamePasswordCredentials(_userName, _password);
                    ServiceFactory factory
                        = new ServiceFactory(new ServiceFactorySettings(ServicesUri, RestUri, credsService));
                    return factory;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }

            }

          
        }
    }



