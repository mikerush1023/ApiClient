using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConsoleTables;

namespace ApiClient
{
    class Joke
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("setup")]
        public string Setup { get; set; }
        [JsonPropertyName("punchline")]
        public string Punchline { get; set; }
    }
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var client = new HttpClient();

            var responseAsStream = await client.GetStreamAsync("https://official-joke-api.appspot.com/random_ten");

            List<Joke> jokes = await JsonSerializer.DeserializeAsync<List<Joke>>(responseAsStream);

            var table = new ConsoleTable("Id", "Type", "Setup", "Punchline");

            foreach (var joke in jokes)
            {
                table.AddRow(joke.Id, joke.Type, joke.Setup, joke.Punchline);
            }

            table.Write();

        }
    }
}
