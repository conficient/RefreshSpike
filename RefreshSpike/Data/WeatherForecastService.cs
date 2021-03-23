using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace RefreshSpike.Data
{
    /// <summary>
    /// Amended weather forecast service
    /// </summary>
    public class WeatherForecastService : IDisposable
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly Random rng;
        private readonly WeatherForecast[] forecasts;
        private readonly Timer timer;

        const int INTERVAL_MS = 5000; // every 5 seconds

        /// <summary>
        /// Generate random data in the ctor
        /// </summary>
        public WeatherForecastService()
        {
            var startDate = DateTime.Today;
            rng = new Random();
            forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();
            timer = new Timer(INTERVAL_MS);
            timer.Elapsed += TimerTick;
            timer.Enabled = true;

        }


        /// <summary>
        /// Get forecasts for inital load
        /// </summary>
        /// <returns></returns>
        public Task<WeatherForecast[]> GetForecastAsync()
        {
            return Task.FromResult(forecasts);
        }

        /// <summary>
        /// Event raised when a weather forecast changes
        /// </summary>
        public event Action<WeatherForecast> OnForecastChanged;

        /// <summary>
        /// Simulate a data change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, ElapsedEventArgs e)
        {
            // get random weather forecast entry
            var index = rng.Next(0, forecasts.Length - 1);
            var forecast = forecasts[index];
            // change the entry randomly
            forecast.TemperatureC = rng.Next(-20, 55);
            forecast.Summary = Summaries[rng.Next(Summaries.Length)];

            // broadcast the event
            OnForecastChanged?.Invoke(forecast);
        }


        public void Dispose()
        {
            timer.Stop();
            timer.Dispose();
        }


    }
}
