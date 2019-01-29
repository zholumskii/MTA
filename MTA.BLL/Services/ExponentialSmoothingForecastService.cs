using MTA.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.BLL.DTO;
using MTA.BLL.Interfaces;

namespace MTA.BLL.Services
{
    public class ExponentialSmoothingForecastService : ForecastService
    {
        public ExponentialSmoothingForecastService(IUnitOfWork database): base(database) { }

        public override Forecast[] Forecasting(DateTime begin, DateTime end, int depth, double[,] samples)
        {
            IEnumerable<ProductDTO> products = GetProducts();
            int length = GetSampleLength(begin, end);

            Forecast[] result = new Forecast[products.Count()];
            double[] temp = new double[length];

            for (int i = 0; i < products.Count(); i++)
            {
                for (int j = 0; j < length; j++)
                    temp[j] = samples[i, j];

                IForecastType forecast = new ForecastType(temp);
                result[i] = forecast.ExponentialSmoothingForecast.GetForecast();
            }

            return result;
        }
    }
}
