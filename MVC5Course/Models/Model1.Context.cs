﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC5Course.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FabricsEntities : DbContext
    {
        public FabricsEntities()
            : base("name=FabricsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Occupation> Occupations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<vwClientOrder> vwClientOrders { get; set; }
    
        public virtual ObjectResult<usp_Fabrics_Result> usp_Fabrics(Nullable<int> createClients, Nullable<int> createOrders)
        {
            var createClientsParameter = createClients.HasValue ?
                new ObjectParameter("CreateClients", createClients) :
                new ObjectParameter("CreateClients", typeof(int));
    
            var createOrdersParameter = createOrders.HasValue ?
                new ObjectParameter("CreateOrders", createOrders) :
                new ObjectParameter("CreateOrders", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_Fabrics_Result>("usp_Fabrics", createClientsParameter, createOrdersParameter);
        }
    }
}
