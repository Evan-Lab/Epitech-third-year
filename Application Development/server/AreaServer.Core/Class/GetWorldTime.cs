using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class WorldTimeAction
{
    public WorldTimeAction() {}
    public async Task <string> getWorldTime(int requestDay, int requestMonth, int requestYear, int requestHour, int requestMinute)
    {
        string url = "http://worldtimeapi.org/api/timezone/Europe/Paris";
        bool isError = true;
        string time = "";
        int hour = -1;
        int minute = -1;
        int day = -1;
        int month = -1;
        int year = -1;

        try {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            
            if (response.IsSuccessStatusCode) {
                string json = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(json);

                time = (string)jsonObject["datetime"];
                Console.WriteLine($"Time: {time}");
                if (!string.IsNullOrEmpty(time)) {
                    DateTime dateTime = DateTime.Parse(time);
                    hour = dateTime.Hour + 1;
                    minute = dateTime.Minute;
                    day = dateTime.Day;
                    month = dateTime.Month;
                    year = dateTime.Year;
                }
                Console.WriteLine($"Hour: {hour} Minute: {minute} Day: {day} Month: {month} Year: {year}");
                Console.WriteLine($"Hour: {requestHour} Minute: {requestMinute} Day: {requestDay} Month: {requestMonth} Year: {requestYear}");
                if (day == requestDay && month == requestMonth && year == requestYear && hour == requestHour && minute == requestMinute) {
                    Console.WriteLine("Action Execution...");
                    isError = false;
                }
            } else {
                Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                isError = true;
            }
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            isError = true;
        }
        var result = new { error = isError, value = new { hour = hour, minute = minute, day = day, month = month, year = year } };
        return JsonConvert.SerializeObject(result);
    }
}