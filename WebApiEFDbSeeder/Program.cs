using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApiEFDbSeeder
{
    class Program
    {
        static async Task Main()
        {
            using var client = new HttpClient();
            IEnumerable<int> people = await AddPeopleAsync(client);
            IEnumerable<int> assignments = await AddAssignmentsAsync(client);
            await AddWorkloads(client, people, assignments);
        }

        private static async Task<IEnumerable<int>> AddPeopleAsync(HttpClient client)
        {
            string[] people = {
                "{\"firstName\": \"Adam\", \"lastName\": \"Adamsson\", \"postalCode\": \"11111\", \"Phone\": \"1234567890\", \"city\": \"Alvesta\"}" ,
                "{\"firstName\": \"Bertil\", \"lastName\": \"Bertilsson\", \"postalCode\": \"22222\", \"Phone\": \"1234567890\",\"city\": \"Boden\"}" ,
                "{\"firstName\": \"Caesar\", \"lastName\": \"Caesarsson\", \"postalCode\": \"33333\", \"Phone\": \"1234567890\",\"city\": \"Charlottenberg\"}" ,
                "{\"firstName\": \"David\", \"lastName\": \"Davidsson\", \"postalCode\": \"44444\", \"Phone\": \"1234567890\",\"city\": \"Djursholm\"}" ,
                "{\"firstName\": \"Erik\", \"lastName\": \"Eriksson\", \"postalCode\": \"55555\", \"Phone\": \"1234567890\",\"city\": \"Eksjö\"}" ,
                "{\"firstName\": \"Filip\", \"lastName\": \"Filipsson\", \"postalCode\": \"66666\", \"Phone\": \"1234567890\",\"city\": \"Falköping\"}" ,
                "{\"firstName\": \"Gustav\", \"lastName\": \"Gustavsson\", \"postalCode\": \"77777\", \"Phone\": \"1234567890\",\"city\": \"Göteborg\"}" ,
                "{\"firstName\": \"Henrik\", \"lastName\": \"Henriksson\", \"postalCode\": \"88888\", \"Phone\": \"1234567890\",\"city\": \"Halmstad\"}" ,
            };
            List<int> index = new List<int>();

            foreach (var person in people)
            {
                HttpContent httpContent = new StringContent(person, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("https://localhost:5001/workloads/people", httpContent);
                var i = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Added {i} status:\n\t{response.StatusCode}");
                index.Add(int.Parse(i));
            }
            return index;
        }

        private static async Task<IEnumerable<int>> AddAssignmentsAsync(HttpClient client)
        {
            string[] assignments = {
                "{\"customer\": \"Akelius\", \"description\": \"Kund ett\"}" ,
                "{\"customer\": \"Bofors\", \"description\": \"Kund två\"}" ,
                "{\"customer\": \"Capio\", \"description\": \"Kund tre\"}" ,
                "{\"customer\": \"DAFA AB\", \"description\": \"Kund fyra\"}" ,
                "{\"customer\": \"EON\", \"description\": \"Kund fem\"}" ,
                "{\"customer\": \"Facit\", \"description\": \"Kund sex\"}" ,
                "{\"customer\": \"Gambro\", \"description\": \"Kund sju\"}" ,
                "{\"customer\": \"HSB\", \"description\": \"Kund åtta\"}" ,
            };
            List<int> index = new List<int>();

            foreach (var assignment in assignments)
            {
                HttpContent httpContent = new StringContent(assignment, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("https://localhost:5001/workloads/assignments", httpContent);
                var i = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Added {i} status:\n\t{response.StatusCode}");
                index.Add(int.Parse(i));
            }
            return index;
        }
        private static async Task AddWorkloads(HttpClient client, IEnumerable<int> people, IEnumerable<int> assignments)
        {
            foreach (var personId in people)
            {
                foreach (var assignmentId in assignments)
                {
                    //Start a workload
                    var response = await client.PostAsync($"https://localhost:5001/workloads/{personId}&{assignmentId}&\"Comment {assignmentId}\"", null);
                    var idx = await response.Content.ReadAsStringAsync();
                    var i = int.Parse(idx);
                    Console.WriteLine($"Started work {i} status:\n\t{response.StatusCode}");

                    //End all workloads except the last for each assignment
                    //Just to have finished and unfinished workloads in the sample data
                    if (i % 8 != 0)
                    {
                        response = await client.PutAsync($"https://localhost:5001/workloads/{i}", null);
                        Console.WriteLine($"Stopped work {i} status:\n\t{response.StatusCode}");
                    }
                }
            }
        }
    }
}
