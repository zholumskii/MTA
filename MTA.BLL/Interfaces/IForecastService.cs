using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.BLL.Interfaces;
using MTA.BLL.DTO;
using MTA.BLL.Services;

namespace MTA.BLL.Interfaces
{
    public interface IForecastService
    {
        IEnumerable<OrderProductDTO> GetOrderProducts();
        IEnumerable<ProductDTO> GetProducts();
        IEnumerable<OrderDTO> GetOrders();
        double[,] GetProductSamples(DateTime begin, DateTime end);
        double[,] GetEmployeeSamples(DateTime begin, DateTime end);
        Forecast[] Forecasting(DateTime begin, DateTime end, int depth, double[,] samples);
    }
}
