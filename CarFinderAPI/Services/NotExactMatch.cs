using CarFinderAPI.Data;
using CarFinderAPI.Models;
using CarFinderAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CarFinderAPI.Services
{
    public class NotExactMatch
    {
        private readonly ApplicationDbContext _context;

        public NotExactMatch(ApplicationDbContext context)
        {
            _context = context;

        }

        public IQueryable<Car> CheckForResult(CarRequest carRequest)
        {
            List<string> notNullProperties = new List<string>();
            List<string> newRequestProperties;
            int tryCount = 0;
            IQueryable<Car> result;

            var type = typeof(CarRequest);
            var properties = type.GetTypeInfo().GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(carRequest, null) != null)
                {
                    notNullProperties.Add(property.Name);
                }
            }

            newRequestProperties = new List<string>(notNullProperties);

            for (int i = 0; i < newRequestProperties.Count; i++)
            {
                newRequestProperties.RemoveAt(i);
                result = getResult(properties, carRequest, newRequestProperties);

                if (result.Count() > 0)
                    return result;

                newRequestProperties = new List<string>(notNullProperties);
            }

            newRequestProperties = new List<string>(notNullProperties);

            for (int i = 0; i < newRequestProperties.Count; i++)
            {
                newRequestProperties.RemoveAt(i);
                result = getResult(properties, carRequest, newRequestProperties);

                if (result.Count() > 0)
                    return result;
            }

            //Func<IQueryable<Car>> q = () =>
            //{
            //    result = getResult(properties, carRequest, newRequestProperties);

            //    if (result.Count() > 0)
            //        return result;
            //};

            return defaultResult();
        }

        private IQueryable<Car> defaultResult()
        {
            var req = new CarRequest()
            {
                PriceFrom = 10
            };
            var predicate = CarPredicateBuilder.BuildPredicate(req);
            var newResult = _context.Cars.Where(predicate);

            return newResult;
        }

        private IQueryable<Car> getResult(PropertyInfo[] properties, CarRequest carRequest, List<string> newRequestProperties)
        {
            var newRequest = new CarRequest();
            foreach (var p in properties)
            {
                if (newRequestProperties.FirstOrDefault(n => n == p.Name) != null)
                {
                    p.SetValue(newRequest, p.GetValue(carRequest));
                }
            }

            var predicate = CarPredicateBuilder.BuildPredicate(newRequest);
            var newResult = _context.Cars.Where(predicate);


            return newResult;
        }

    }
}
