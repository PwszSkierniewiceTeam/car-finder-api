using CarFinderAPI.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarFinderAPI.Utilities
{
    public static class CarPredicateBuilder
    {
        public static Expression<Func<Car, bool>> BuildPredicate(CarRequest carRequest)
        {
            var predicate = PredicateBuilder.New<Car>();
            
            if (carRequest.BodyType != null)
            {
                predicate.And(c => c.BodyType == carRequest.BodyType);
            }
            if (carRequest.Company != null)
            {
                predicate.And(c => c.Company == carRequest.Company);
            }
            if (carRequest.CapacityFrom != null)
            {
                predicate.And(c => c.Capacity >= carRequest.CapacityFrom);
            }
            if (carRequest.CapacityTo != null)
            {
                predicate.And(c => c.Capacity <= carRequest.CapacityTo);
            }
            if (carRequest.DriveType != null)
            {
                predicate.And(c => c.DriveType == carRequest.DriveType);
            }
            if (carRequest.Fuel != null)
            {
                predicate.And(c => c.Fuel == carRequest.Fuel);
            }
            if (carRequest.FuelConsumptionFrom != null)
            {
                predicate.And(c => c.FuelConsumption >= carRequest.FuelConsumptionFrom);
            }
            if (carRequest.FuelConsumptionTo != null)
            {
                predicate.And(c => c.FuelConsumption <= carRequest.FuelConsumptionTo);
            }
            if (carRequest.PowerFrom != null)
            {
                predicate.And(c => c.Power >= carRequest.PowerFrom);
            }
            if (carRequest.PowerTo != null)
            {
                predicate.And(c => c.Power <= carRequest.PowerTo);
            }
            if (carRequest.PriceFrom != null)
            {
                predicate.And(c => c.Price >= carRequest.PriceFrom);
            }
            if (carRequest.PriceTo != null)
            {
                predicate.And(c => c.Price <= carRequest.PriceTo);
            }
            if (carRequest.Transmission != null)
            {
                predicate.And(c => c.Transmission == carRequest.Transmission);
            }
            
            return predicate;
        }
    }
}
