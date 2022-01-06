using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;


using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class TimerEmulador
    {

        private static HttpClient httpClient = new HttpClient();

        [FunctionName("index")]
        public static IActionResult Index([HttpTrigger(AuthorizationLevel.Anonymous)]HttpRequest req, ExecutionContext context)
        {
            var path = Path.Combine(context.FunctionAppDirectory, "content", "index.html");
            return new ContentResult
            {
                Content = File.ReadAllText(path),
                ContentType = "text/html",
            };
        }

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate( 
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "serverlessSample")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }


        [FunctionName("broadcast")]
        public static async Task Run([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer,
        [SignalR(HubName = "serverlessSample")] IAsyncCollector<SignalRMessage> signalRMessages)

        {
             // */5 * * * * * cada 5 segundos
            // 0 */5 * * * * cada 5 min
            
            int precipitacion1 = new Random().Next(50, 100);
            int temperatura1 = new Random().Next(0, 50);
            int humendad1 = new Random().Next(0, 50);

            int precipitacion2 = new Random().Next(50, 100);
            int temperatura2 = new Random().Next(0, 50);
            int humendad2 = new Random().Next(0, 50);

            int precipitacion3 = new Random().Next(50, 100);
            int temperatura3 = new Random().Next(0, 50);
            int humendad3 = new Random().Next(0, 50);

            int vcturbiedadentrada = new Random().Next(0, 50);
            int vccaudalentrada = new Random().Next(0, 50);
            int vcconductividad = new Random().Next(0, 50);
            int vcph = new Random().Next(0, 50);
            int vcpresion = new Random().Next(0, 50);
            int vccolor = new Random().Next(0, 50);

            int coagulantenivelacutal = new Random().Next(0, 50);
            int coagulantenivelrecomendado = new Random().Next(0, 50);

            int apturbiedadsalida = new Random().Next(0, 50);
            int apcolor = new Random().Next(0, 50);
            int apcaudalsalida = new Random().Next(0, 50);
            int apniveltanques = new Random().Next(0, 50);

            int ncactual = new Random().Next(0, 100);
            int ncrecomendado = new Random().Next(0, 100);
            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "newMessage",
                    Arguments = new[] { 
                        "[{'Siata':["+
                        "{'Sensor1':{'precipitacion': "+precipitacion1+",'precipitaciontime': '"+DateTime.Now.AddHours(-precipitacion1).ToString("MM/dd/yy H:mm:ss")+"','temperatura':"+temperatura1+",'temperaturatime':'"+DateTime.Now.AddHours(-temperatura1).ToString("MM/dd/yy H:mm:ss")+"','humedad':"+humendad1+",'humedadtime':'"+DateTime.Now.AddHours(-humendad1).ToString("MM/dd/yy H:mm:ss")+"'}},"+
                        "{'Sensor2':{'precipitacion': "+precipitacion2+",'precipitaciontime': '"+DateTime.Now.AddHours(-precipitacion2).ToString("MM/dd/yy H:mm:ss")+"','temperatura':"+temperatura2+",'temperaturatime':'"+DateTime.Now.AddHours(-temperatura2).ToString("MM/dd/yy H:mm:ss")+"','humedad':"+humendad2+",'humedadtime':'"+DateTime.Now.AddHours(-humendad2).ToString("MM/dd/yy H:mm:ss")+"'}},"+
                        "{'Sensor3':{'precipitacion': "+precipitacion3+",'precipitaciontime': '"+DateTime.Now.AddHours(-precipitacion3).ToString("MM/dd/yy H:mm:ss")+"','temperatura':"+temperatura3+",'temperaturatime':'"+DateTime.Now.AddHours(-temperatura3).ToString("MM/dd/yy H:mm:ss")+"','humedad':"+humendad3+",'humedadtime':'"+DateTime.Now.AddHours(-humendad3).ToString("MM/dd/yy H:mm:ss")+"'}}]},"+
                        "{'AguaNatural':{'turbiedadentrada': "+vcturbiedadentrada+",'turbiedadentradatime': '"+DateTime.Now.AddHours(-vcturbiedadentrada).ToString("MM/dd/yy H:mm:ss")+"',"+
                                "'caudalentrada':"+vccaudalentrada+","+"'caudalentradatime':'"+DateTime.Now.AddHours(-vccaudalentrada).ToString("MM/dd/yy H:mm:ss")+"',"+
                                "'conductividad':"+vcconductividad+","+ "'conductividadtime':'"+DateTime.Now.AddHours(-vcconductividad).ToString("MM/dd/yy H:mm:ss")+"',"+
                                "'ph':"+vcph+","+"'phtime':'"+DateTime.Now.AddHours(-vcph).ToString("MM/dd/yy H:mm:ss")+"',"+
                                "'presion':"+vcpresion+","+"'presiontime':'"+DateTime.Now.AddHours(-vcpresion).ToString("MM/dd/yy H:mm:ss")+"',"+
                                "'color':"+vccolor+","+"'colortime':'"+DateTime.Now.AddHours(-vccolor).ToString("MM/dd/yy H:mm:ss")+"'}},"+
                        "{'AguaPotable':{'turbiedadsalida': "+apturbiedadsalida+",'turbiedadsalidatime': '"+DateTime.Now.AddHours(-apturbiedadsalida).ToString("MM/dd/yy H:mm:ss")+"',"+
                                "'caudalsalida':"+apcaudalsalida+","+"'caudalsalidatime':'"+DateTime.Now.AddHours(-apcaudalsalida).ToString("MM/dd/yy H:mm:ss")+"',"+
                                "'niveltanques':"+apniveltanques+","+"'niveltanquestime':'"+DateTime.Now.AddHours(-apniveltanques).ToString("MM/dd/yy H:mm:ss")+"',"+
                                "'color':"+apcolor+",'colortime':'"+DateTime.Now.AddHours(-apcolor).ToString("MM/dd/yy H:mm:ss")+"'}},"+
                        "{'NivelesCoagulacion':{'actual': "+ncactual+","+"'actualtime': '"+DateTime.Now.AddHours(-ncactual).ToString("MM/dd/yy H:mm:ss")+"',"+
                                "'recomendado':"+ncrecomendado+",'recomendadotime':'"+DateTime.Now.AddHours(-ncrecomendado).ToString("MM/dd/yy H:mm:ss")+"'}}]"
                    }
                });
            
        }
         private class GitResult
        {
            [JsonRequired]
            [JsonProperty("stargazers_count")]
            public string StarCount { get; set; }
        }
    }
}
