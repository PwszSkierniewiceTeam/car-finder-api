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
        private List<string> notNullProperties = new List<string>();
        private List<string> newRequestProperties = new List<string>();
        private int propNumber = 1;
        private int tryCount = 0;

        public NotExactMatch(ApplicationDbContext context)
        {
            _context = context;

        }

        public IQueryable<Car> CheckForResult(CarRequest carRequest)
        {
            var type = typeof(CarRequest);
            var properties = type.GetTypeInfo().GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(carRequest, null) != null)
                {
                    notNullProperties.Add(property.Name);
                }
            }


            newRequestProperties = notNullProperties;
            generateList();


            var result = getResult(properties, carRequest);

            while (result.Count() == 0)
            {
                generateList();
                result = getResult(properties, carRequest);
            }

            return result;

        }

        private IQueryable<Car> getResult(PropertyInfo[] properties, CarRequest carRequest)
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

        private void generateList()
        {
            if (newRequestProperties.Count - 1 >= tryCount)
            {
                propNumber = newRequestProperties.Count - 1 - tryCount;
            }
            else
            {
            }

            if (newRequestProperties.Count() == notNullProperties.Count())
            {
                newRequestProperties.RemoveAt(newRequestProperties.Count - propNumber);
                tryCount += 1;
            }
            else if (newRequestProperties.Any())
            {
                newRequestProperties.RemoveAt(newRequestProperties.Count - 1);
            }
            else
            {
                newRequestProperties = notNullProperties;
            }
        }
    }
}
