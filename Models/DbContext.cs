using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BitysTest.Models {
    public class UsersContext : DbContext {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\yohan\\source\\repos\\BitysTest\\database\\Bit_Test.mdf;Integrated Security=True");
        }
    }
}
