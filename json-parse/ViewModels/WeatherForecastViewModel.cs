﻿using System;

namespace json_parse.ViewModels
{
    public class WeatherForecastViewModel
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string Summary { get; set; }
    }
}