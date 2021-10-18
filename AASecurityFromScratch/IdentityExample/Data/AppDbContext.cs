using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample.Data
{
    public class AppDbContext : IdentityDbContext
    {
        /// <summary>
        /// Contains all the user tables
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
    }
}
