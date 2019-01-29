using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using MTA.BLL.Interfaces;
using MTA.BLL.Services;

namespace MTA.WEB.Util
{
    public class ForecastModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IForecastService>().To<LeastSquareForecastService>();
        }
    }
}