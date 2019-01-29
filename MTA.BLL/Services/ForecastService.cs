using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTA.BLL.DTO;
using MTA.BLL.Interfaces;
using MTA.DAL.Interfaces;
using AutoMapper;
using MTA.DAL.Entities;
using MTA.DAL.Repositories;
using MTA.BLL.BusinessModels;

namespace MTA.BLL.Services
{
    public abstract class ForecastService : IForecastService
    {
        protected IUnitOfWork Database { get; set; }

        public ForecastService(IUnitOfWork database)
        {
            Database = database;
        }

        protected int GetSampleLength(DateTime begin, DateTime end)
        {
            int length = 0;
            for (int i = begin.Year; i <= end.Year; i++)
                length++;

            return length;
        }

        public IEnumerable<OrderProductDTO> GetOrderProducts()
        {
            var mapper = new MapperConfiguration(config => config.CreateMap<OrderProduct, OrderProductDTO>()).CreateMapper();

            IEnumerable<OrderProductDTO> products = mapper.Map<IEnumerable<OrderProduct>, 
                List<OrderProductDTO>>(Database.OrderProducts.GetAll());
 
            return products;
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var mapper = new MapperConfiguration(config => config.CreateMap<Product, ProductDTO>()).CreateMapper();

            IEnumerable<ProductDTO> products = mapper.Map<IEnumerable<Product>, 
                List<ProductDTO>>(Database.Products.GetAll());

            return products;
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var mapper = new MapperConfiguration(config => config.CreateMap<Order, OrderDTO>()).CreateMapper();

            IEnumerable<OrderDTO> orders = mapper.Map<IEnumerable<Order>,
                List<OrderDTO>>(Database.Orders.GetAll());

            return orders;
        }

        public double[,] GetProductSamples(DateTime begin, DateTime end)
        {
            int length = GetSampleLength(begin, end);
            IEnumerable<ProductDTO> products = GetProducts();

            double[,] samples = new double[products.Count(), length];
            Random random = new Random();

            for (int i = 0; i < products.Count(); i++)
            {
                for (int j = 0; j < length; j++)
                    samples[i, j] = random.Next(60, 120);
            }

            return samples;
        }
    
        public double[,] GetEmployeeSamples(DateTime begin, DateTime end)
        {
            int length = GetSampleLength(begin, end);
            IEnumerable<ProductDTO> products = GetProducts();

            double[,] samples = new double[products.Count(), length];
            Random random = new Random();

            for (int i = 0; i < products.Count(); i++)
            {
                for (int j = 0; j < length; j++)
                    samples[i, j] = random.Next(60, 120);
            }

            return samples;
        }

        public abstract Forecast[] Forecasting(DateTime begin, DateTime end, int depth, double[,] samples);

    }

    public struct Forecast
    {
        public double forecast;
        public double error;
    }
}
