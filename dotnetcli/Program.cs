using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace ConsoleApplication
{
    public class Program
    {
        [DataContract(Name = "repo")]
        public class Repo_SQ
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name ="description")]
            public string Des { get; set; }

        }


        private static async Task<List<Repo_SQ>> ProcessRepos()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            client.DefaultRequestHeaders.Add("User-Agent", ".net from sq");
            var serializer = new DataContractJsonSerializer(typeof(List<Repo_SQ>));
            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            Console.WriteLine(streamTask);
            var repos = serializer.ReadObject(await streamTask) as List<Repo_SQ>;

            return repos;
        }

        public static  void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("你好00");

            var repos = ProcessRepos().Result;
            foreach (var item in repos)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Des);
            }







        }
    }
}
