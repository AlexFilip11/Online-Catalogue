using FinalProjectCatalogue.Data.DAL;
using FinalProjectCatalogue.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public static class DalUtils
    {
        public static void AddDataAccessLayer(this IServiceCollection services, string? connString)
        {
            services.AddDbContext<CatalogueDbContext>(options => options.UseSqlServer(connString));

            services.AddScoped<IDalStudents, DataAccessLayerStudents>();
        }
    }
}
