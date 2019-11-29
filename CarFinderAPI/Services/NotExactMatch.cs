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

            var states = new List<State>();
            states.Add(new State() { PrevState = null, NotNullProperties = notNullProperties });
            var state = states.First();
            while (state.NotNullProperties.Count > 0)
            {
                for (int i = 0; i < state.NotNullProperties.Count; i++)
                {
                    var newState = GenerateNewState(state, state.NotNullProperties.Where((property, index) => index != i).ToList());
                    if (IsNewState(newState, states))
                    {
                        states.Add(newState);
                    }
                }
                states.Remove(state);
                state = states.First();

                result = getResult(properties, carRequest, state.NotNullProperties);
                if (result.Any())
                {
                    return result;
                }
            }

            return defaultResult();
        }

        private bool IsNewState(State newState, List<State> states)
        {
            if (states.Any(s => s.NotNullProperties.OrderBy(i => i).SequenceEqual(newState.NotNullProperties.OrderBy(i => i))))
            {
                return false;
            }
            return true;
        }

        private State GenerateNewState(State state, List<string> list)
        {
            return new State() { PrevState = state, NotNullProperties = list };
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

        private class State
        {
            public State PrevState { get; set; }
            public List<string> NotNullProperties { get; set; }
        }
    }
}
