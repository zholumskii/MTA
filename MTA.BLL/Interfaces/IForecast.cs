using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.BLL.Services;

namespace MTA.BLL.Interfaces
{
    public interface IForecast
    {
        Forecast GetForecast();
    }
}
