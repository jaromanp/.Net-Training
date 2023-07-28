using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where( c => c.Orders.Sum(o => o.Total) > limit);
                    
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var result = from c in customers
                         join s in suppliers on new { c.City, c.Country } equals new { s.City, s.Country } into cs
                         select (c, cs.Select(s => s));

            return result;
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var result = customers.GroupJoin(suppliers, c => new { c.City, c.Country }, s => new { s.City, s.Country}, (c, s) => (customer: c, suppliers: s));

            return result;
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            var result = customers.Where(c => c.Orders.Where(o => o.Total > 0).Sum(o => o.Total) > limit);
            return result;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            return customers.Select(c => (customer: c, dateOfEntry: c.Orders.OrderBy(o => o.OrderDate).FirstOrDefault()?.OrderDate ?? DateTime.MinValue))
                            .Where(c => c.customer.Orders.Length != 0);
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            return customers.Select(c => (customer: c, dateOfEntry: c.Orders.OrderBy(o => o.OrderDate).FirstOrDefault()?.OrderDate ?? DateTime.MinValue))
                            .Where(c => c.customer.Orders.Length != 0)
                            .OrderBy(c => c.dateOfEntry.Year)
                            .ThenBy(c => c.dateOfEntry.Month)
                            .ThenByDescending(c => c.customer.Orders.Sum(o => o.Total))
                            .ThenBy(c => c.customer.CompanyName);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            return customers.Where(c => int.TryParse(c.PostalCode, out int num) == false || string.IsNullOrEmpty(c.Region) || !c.Phone.Contains('(') || !c.Phone.Contains(')'));                            
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            var query = from product in products
                        group product by product.Category into categoryGroup
                        select new Linq7CategoryGroup
                        {
                            Category = categoryGroup.Key,
                            UnitsInStockGroup =
                                from product in categoryGroup
                                group product by product.UnitsInStock > 0 into availabilityGroup
                                orderby availabilityGroup.Key descending
                                select new Linq7UnitsInStockGroup
                                {
                                    UnitsInStock = availabilityGroup.Key ? 1 : 0,
                                    Prices = from product in availabilityGroup
                                             orderby product.UnitPrice ascending
                                             select product.UnitPrice
                                }
                        };

            return query.OrderBy(c => c.Category);
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            var cheapProducts = products.Where(p => p.UnitPrice <= cheap).ToList();
            var averageProducts = products.Where(p => p.UnitPrice > cheap && p.UnitPrice <= middle).ToList();
            var expensiveProducts = products.Where(p => p.UnitPrice > middle && p.UnitPrice <= expensive).ToList();

            return new List<(decimal category, IEnumerable<Product> products)>
            {
                (cheap, cheapProducts),
                (middle, averageProducts),
                (expensive, expensiveProducts)
            };
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            var result = customers.GroupBy(c => c.City)
                                  .Select(g => new
                                  {
                                      City = g.Key,
                                      AvgProfitability = g.Average(c => c.Orders.Sum(o => o.Total)),
                                      AvgRate = g.Average(c => c.Orders.Length / g.Count())
                                  })
                                  .Select(g => (g.City, Convert.ToInt32(Math.Round(g.AvgProfitability)), (int)g.AvgRate));
            return result;
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var countries = suppliers.Select(s => s.Country).Distinct().OrderBy(c => c.Length).ThenBy(c => c);

            return string.Join("", countries);          
        }
    }
}