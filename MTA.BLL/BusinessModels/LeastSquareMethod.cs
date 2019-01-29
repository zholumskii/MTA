using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.BLL.Interfaces;
using MTA.BLL.Services;

namespace MTA.BLL.BusinessModels
{
    public class LeastSquareMethod : IForecast
    {
        private double[] samples;
        private int depth;

        public LeastSquareMethod(double[] samples, int depth)
        {
            this.samples = samples;
            this.depth = depth;
        }

        public Forecast GetForecast()
        {
            double sumX = 0, sumY = 0, sumXY = 0, sumXX = 0;

            for (int i = 0; i < samples.Length; i++)
            {
                sumX += i + 1;
                sumY += samples[i];
                sumXY += samples[i] * (i + 1);
                sumXX += Math.Pow(i + 1, 2);
            }

            double a = sumXY - (sumX * sumY) / samples.Length;
            a = a / (sumXX - Math.Pow(sumX, 2) / samples.Length);
            double b = sumY / samples.Length - a * sumX / samples.Length;

            double forecast =  a * (samples.Length + 1) + b;

            double forecastValue = 0;
            double error = 0;

            for (int i = 0; i < samples.Length; i++)
            {
                forecastValue = a * (i + 1) + b;
                error += Math.Abs(samples[i] - forecastValue) * 100 / samples[i];
            }
            error /= samples.Length;

            Forecast result = new Forecast
            {
                forecast = forecast,
                error = error,
            };

            return result;
        }
    }
}
