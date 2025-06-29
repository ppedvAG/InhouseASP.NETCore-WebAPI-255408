﻿using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using NorthwindODataApi.Models;

namespace NorthwindODataApi
{
    public static class ODataConfiguration
    {
        public static void AddOData(this IMvcBuilder builder)
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Category>(nameof(Category));
            modelBuilder.EntitySet<Customer>(nameof(Customer));
            modelBuilder.EntitySet<Employee>(nameof(Employee));
            modelBuilder.EntitySet<Order>(nameof(Order));

            modelBuilder.EntityType<Customer>().HasMany(c => c.Orders);
            modelBuilder.EntityType<Employee>().HasMany(c => c.Orders);
            modelBuilder.EntityType<Order>().HasRequired(o => o.Customer);
            modelBuilder.EntityType<Order>().HasRequired(o => o.Employee);

            builder.AddOData(options => options
                .Select()
                .Filter()
                .Expand()
                .Count()
                .OrderBy()
                .SetMaxTop(20) // wie viele Items pro Seite geladen werden sollen
                .AddRouteComponents("odata", modelBuilder.GetEdmModel())
            );
        }
    }
}
