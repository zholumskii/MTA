using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.BLL.Interfaces;
using MTA.BLL.Services;

namespace MTA.BLL.BusinessModels
{
    public class MovingAverageMethod : IForecast
    {
        private double[] samples;
        private int depth;

        public MovingAverageMethod(double[] samples, int depth)
        {
            this.samples = samples;
            this.depth = depth;
        }

        public Forecast GetForecast()
        {
            const int interval = 3;
            double[] smoothedSamples = new double[samples.Length];

            int index = (interval - 1) / 2;

            for (int i = index; i < samples.Length - index; i++)
            {
                smoothedSamples[i] = (samples[i - 1] + samples[i] + samples[i + 1]) / interval;
            }

            double forecast = smoothedSamples[samples.Length - 2];
            forecast += (samples[samples.Length - 1] - samples[samples.Length - 2]) / interval;

            double error = 0;

            for (int i = index; i < samples.Length - index; i++)
            {
                error += Math.Abs(samples[i] - smoothedSamples[i]) * 100 / samples[i];
            }
            error /= samples.Length - index * 2;

            Forecast result = new Forecast
            {
                forecast = forecast,
                error = error,
            };

            return result;
        }
    }
}

