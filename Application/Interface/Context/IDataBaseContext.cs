using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Context
{
    public interface IDataBaseContext
    {


        DbSet<Education> Educations { get; set; }
       
        DbSet<Message> Messages { get; set; }
       
        DbSet<Portfolio> Portfolios { get; set; }
       
        DbSet<Pricing> Pricings { get; set; }
       
        DbSet<Service> Services { get; set; }
       
        DbSet<Skill> Skills { get; set; }
       
        DbSet<User> Users { get; set; }
        DbSet<Informations> Informations { get; set; }


        int SaveChanges(bool acceptAllChangeOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangeOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
