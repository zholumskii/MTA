using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTA.BLL.Interfaces
{
    public interface IForecastType
    {
        IForecast ExponentialSmoothingForecast { get; }
        IForecast LeastSqueraForecast { get; }
        IForecast MovingAverageForecast { get; }
    }
}
