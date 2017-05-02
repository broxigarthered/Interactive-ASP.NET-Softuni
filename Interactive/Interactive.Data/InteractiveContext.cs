using Interactive.Models;
using Interactive.Models.EntityModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interactive.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class InteractiveContext : IdentityDbContext<ApplicationUser>
    {
        // Your context has been configured to use a 'InteractiveContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Interactive.Data.InteractiveContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'InteractiveContext' 
        // connection string in the application configuration file.
        public InteractiveContext()
            : base("name=InteractiveContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

            public virtual DbSet<Post> Posts { get; set; }

        public static InteractiveContext Create()
        {
            return new InteractiveContext();
        }
    }





    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}