﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using ADOP_Project_Part_B_Weather.Models;
using ADOP_Project_Part_B_Weather.Services;

namespace ADOP_Project_Part_B_Weather.Views
{
    public class GroupedForecast
    {
        public string City { get; set; }
        public IEnumerable<IGrouping<DateTime, ForecastItem>> Items { get; set; }
    }

    public partial class ForecastPage : ContentPage
    {
        OpenWeatherService service;
        GroupedForecast groupedforecast;
        CityPicture city;

        public ForecastPage(CityPicture city)
        {
            InitializeComponent();

            this.city = city;
            service = new OpenWeatherService();
            groupedforecast = new GroupedForecast();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Title = $"Forecast for {city.Name}";
            BackgroundImageSource = city.ImageSrc;
            MainThread.BeginInvokeOnMainThread(async () => {await LoadForecast();});
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await LoadForecast();
        }

        private async Task LoadForecast()
        {
            Forecast forecast = await service.GetForecastAsync(city.Name);
            var groupedList = forecast.Items.ToList().GroupBy(x => x.DateTime.Date.ToString("yyyy-MM-dd"));

            GroupedForecast.ItemsSource = groupedList;

            //Here Group your forecast and bind it to your
            //ListView ItemSource
        }
    }
}