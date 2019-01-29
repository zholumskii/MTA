using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.BLL.Interfaces;
using MTA.BLL.Services;

namespace MTA.BLL.BusinessModels
{
    public class ExponentialSmoothingMethod : IForecast
    {
        private double[] samples;

        public ExponentialSmoothingMethod(double[] samples)
        {
            this.samples = samples;
        }

        public Forecast GetForecast()
        {
            double parameter = 2 / ((double) samples.Length + 1);

            double[] weightedValues = new double[samples.Length];
            weightedValues[0] = 0;

            for (int i = 0; i < samples.Length; i++)
            {
                weightedValues[0] += samples[i];
            }
            weightedValues[0] /= samples.Length;

            for (int i = 1; i < samples.Length; i++)
            {
                weightedValues[i] = parameter * samples[i - 1] + (1 - parameter) * weightedValues[i - 1];
            }

            double forecast =  parameter * samples[samples.Length - 1] + (1 - parameter) * weightedValues[samples.Length - 1];

            double forecastValue = weightedValues[0];
            double error = Math.Abs(samples[0] - forecastValue) * 100 / samples[0]; 
      
            for (int i = 1; i < samples.Length; i++)
            {
                forecastValue = parameter * samples[i - 1] + (1 - parameter) * weightedValues[i - 1];
                error += Math.Abs(samples[i] - forecastValue) * 100 / samples[i];
            }
            error /= samples.Length;

            Forecast result = new Forecast
            {
                forecast = forecast,
                error = error
            };

            return result;
        }
    }
}
