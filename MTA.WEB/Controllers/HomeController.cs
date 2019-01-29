using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTA.BLL.DTO;
using MTA.BLL.Services;
using MTA.BLL.Interfaces;
using MTA.BLL.Infrastructure;
using MTA.WEB.Models;
using AutoMapper;
using MTA.DAL.Interfaces;
using MTA.DAL.Repositories;

namespace MTA.WEB.Controllers
{
    public class HomeController : Controller
    {
        private IForecastService forecastService;

        public HomeController(IForecastService forecastService)
        {
            this.forecastService = forecastService;
        }

        public ActionResult Index()
        {
            IEnumerable<ProductDTO> productsDTO = forecastService.GetProducts();
            IEnumerable<OrderDTO> ordersDTO = forecastService.GetOrders();

            var mapper = new MapperConfiguration(config => config.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productsDTO);

            mapper = new MapperConfiguration(config => config.CreateMap<OrderDTO, OrderWiewModel>()).CreateMapper();
            var orders = mapper.Map<IEnumerable<OrderDTO>, List<OrderWiewModel>>(ordersDTO);

            DateTime begin = orders[0].Date;
            DateTime end = orders[0].Date;

            foreach (OrderWiewModel order in orders)
            {
                if (order.Date < begin) begin = order.Date;
                if (order.Date > end) end = order.Date;
            }

            int length = end.Date.Year - begin.Date.Year + 1;

            double[,] productSamples = forecastService.GetProductSamples(begin, end);
            double[,] employeeSamples = forecastService.GetEmployeeSamples(begin, end);

            ViewBag.productSamples = productSamples;
            ViewBag.employeeSamples = employeeSamples;
            ViewBag.begin = begin;
            ViewBag.end = end;

            return View(products);
        }

        [HttpPost]
        public ActionResult Index(DateTime begin, DateTime end, int method, int depth, List<double> productSamples,
            List<double> employeeSamples)
        {
            
            IEnumerable<ProductDTO> productsDTO = forecastService.GetProducts();
            var mapper = new MapperConfiguration(config => config.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productsDTO);

            double[,] resultProductSamples = new double[products.Count(), end.Year - begin.Year + 1];
            double[,] resultEmployeeSamples = new double[products.Count(), end.Year - begin.Year + 1];
            double[,] resultPerfomanceSamples = new double[products.Count(), end.Year - begin.Year + 1];

            int counter = 0;
            for (int i = 0; i < products.Count(); i++)
            {
                for (int j = 0; j < end.Year - begin.Year + 1; j++)
                {
                    resultProductSamples[i, j] = productSamples[counter];
                    resultEmployeeSamples[i, j] = employeeSamples[counter];
                    resultPerfomanceSamples[i, j] = productSamples[counter] / employeeSamples[counter];
                    counter++;
                }
            }

            IUnitOfWork connection = new EFUnitOfWork("Data Source = (LocalDB)\\MSSQLLocalDB; " +
                "AttachDbFilename = '|DataDirectory|\\mtaInfo.mdf'; Integrated Security = True");

            if (method == 2)
            {
                forecastService = new MovingAverageForecasstService(connection);
            }
            
            if (method == 3)
            {
                forecastService = new ExponentialSmoothingForecastService(connection);
            }

            Forecast[] productForecast = forecastService.Forecasting(begin, end, depth, resultProductSamples);
            Forecast[] perfomanceForecast = forecastService.Forecasting(begin, end, depth, resultPerfomanceSamples);

            double[] employeeForecast = new double[productForecast.Length];

            for (int i = 0; i < employeeForecast.Length; i++)
            {
                if (perfomanceForecast[i].forecast == 0)
                {
                    double min = 1;
                    for (int j = 0; j < perfomanceForecast.Length; j++)
                    {
                        if (perfomanceForecast[j].forecast == perfomanceForecast[i].forecast) continue;
                        if (perfomanceForecast[j].forecast < min) min = perfomanceForecast[j].forecast;
                    }
                    perfomanceForecast[i].forecast = min;
                }
                else
                {
                    employeeForecast[i] = productForecast[i].forecast / perfomanceForecast[i].forecast;
                }
            }

            double[] deficit = new double[products.Count()];
            for (int i = 0; i < deficit.Length; i++)
            {
                deficit[i] = employeeForecast[i] - resultEmployeeSamples[i, end.Year - begin.Year];
            }

            ViewBag.productForecast = productForecast;
            ViewBag.employeeForecast = employeeForecast;
            ViewBag.perfomanceForecast = perfomanceForecast;

            ViewBag.resultProductSamples = resultProductSamples;
            ViewBag.resultPerfomanceSamples = resultPerfomanceSamples;
            ViewBag.resultEmployeeSamples = resultEmployeeSamples;
            ViewBag.deficit = deficit;
            ViewBag.begin = begin;
            ViewBag.end = end;

            return View("~/Views/Home/Forecast.cshtml", products); 
        }

        public ActionResult Forecast(IEnumerable<ProductViewModel> products)
        {
            return View(products);
        }

        [HttpPost]
        public ActionResult Forecast(IEnumerable<ProductViewModel> products, DateTime begin, DateTime end,
            int depth, int method, List<double> productSamples, List<double> perfomanceSamples, List<double> employeeSamples)
        {
            double[,] resultProductSamples = new double[products.Count(), end.Year - begin.Year + 1];
            double[,] resultPerfomanceSamples = new double[products.Count(), end.Year - begin.Year + 1];
            double[,] resultEmployeeSamples = new double[products.Count(), end.Year - begin.Year + 1];

            int counter = 0;
            for (int i = 0; i < products.Count(); i++)
            {
                for (int j = 0; j < end.Year - begin.Year + 1; j++)
                {
                    resultProductSamples[i, j] = productSamples[counter];
                    resultEmployeeSamples[i, j] = employeeSamples[counter];
                    resultPerfomanceSamples[i, j] = perfomanceSamples[counter];
                    counter++;
                }
            }

            IUnitOfWork connection = new EFUnitOfWork("Data Source = (LocalDB)\\MSSQLLocalDB; " +
                "AttachDbFilename = '|DataDirectory|\\mtaInfo.mdf'; Integrated Security = True");

            if (method == 2)
            {
                forecastService = new MovingAverageForecasstService(connection);
            }

            if (method == 3)
            {
                forecastService = new ExponentialSmoothingForecastService(connection);
            }

            Forecast[] productForecast = forecastService.Forecasting(begin, end, depth, resultProductSamples);
            Forecast[] perfomanceForecast = forecastService.Forecasting(begin, end, depth, resultPerfomanceSamples);

            double[] employeeForecast = new double[productForecast.Length];

            for (int i = 0; i < employeeForecast.Length; i++)
            {
                employeeForecast[i] = productForecast[i].forecast / perfomanceForecast[i].forecast;
            }

            double[] deficit = new double[products.Count()];
            for (int i = 0; i < deficit.Length; i++)
            {
                deficit[i] = employeeForecast[i] - resultEmployeeSamples[i, end.Year - begin.Year];
            }

            ViewBag.productForecast = productForecast;
            ViewBag.employeeForecast = employeeForecast;

            ViewBag.resultProductSamples = resultProductSamples;
            ViewBag.resultPerfomanceSamples = resultPerfomanceSamples;
            ViewBag.resultEmployeeSamples = resultEmployeeSamples;

            ViewBag.deficit = deficit;
            ViewBag.begin = begin;
            ViewBag.end = end;

            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}