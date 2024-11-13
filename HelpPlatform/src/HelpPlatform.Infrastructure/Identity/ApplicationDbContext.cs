using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HelpPlatform.Infrastructure.Identity;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {   
    }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //        base.OnModelCreating(builder);
//
    //        builder.Entity<ApplicationUser>().Property(uint =>)
    //}
}