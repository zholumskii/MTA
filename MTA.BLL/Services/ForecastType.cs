using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.BLL.Interfaces;
using MTA.BLL.BusinessModels;

namespace MTA.BLL.Services
{
    public class ForecastType : IForecastType
    {
        private double[] samples;
        private int depth;
        private ExponentialSmoothingMethod exponentialSmoothingMethod;
        private LeastSquareMethod leastSquareMethod;
        private MovingAverageMethod movingAverageMethod;

        public ForecastType(double[] samples, int depth)
        {
            this.samples = samples;
            this.depth = depth;
        }
        public ForecastType(double[] samples)
        {
            this.samples = samples;
            depth = 0;
        }

        public IForecast ExponentialSmoothingForecast
        {
            get
            {
                exponentialSmoothingMethod =  new ExponentialSmoothingMethod(samples);
                return exponentialSmoothingMethod;
            }
        }

        public IForecast LeastSqueraForecast
        {
            get
            {
                leastSquareMethod = new LeastSquareMethod(samples, depth);
                return leastSquareMethod;
            }
        }

        public IForecast MovingAverageForecast
        {
            get
            {
                movingAverageMethod = new MovingAverageMethod(samples, depth);
                return movingAverageMethod;
            }
        }
    }
}
