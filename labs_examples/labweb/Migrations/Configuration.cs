namespace labweb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<labweb.DbcontextSt>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "labweb.DbcontextSt";
        }

        protected override void Seed(labweb.DbcontextSt context)
        {
            var g1 = new Group("MS");
            var g2 = new Group("Apple");
            var g3 = new Group("Lego");
            Group[] stG = new[] { g1, g2, g3 };
            var s1 = new Students(g1, "Bill", "Gates", 1, 5.0, Budget.yes);
            var s2 = new Students(g2, "Alicia", "Nogates", 2, 3.4, Budget.yes);
            var s3 = new Students(g3, "Andy", "Wozniak", 42, 5.0, Budget.yes);
            var s4 = new Students(g3, "Anton", "Nebeda", 334, 4.4, Budget.yes);
            var s5 = new Students(g3, "Ashot", "Obeda", 12, 4.3, Budget.no);
            var s6 = new Students(g1, "Nemo", "Beda", 984, 3.4, Budget.no);
            Students[] stA = new[] { s1, s2, s3, s4, s5, s6 };

            context.Students.AddOrUpdate(stA);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
