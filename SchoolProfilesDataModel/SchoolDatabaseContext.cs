using Microsoft.EntityFrameworkCore;

namespace SchoolProfilesDataModel
{
    // Configures and sets up the database.
    public class SchoolDatabaseContext : DbContext
    {
        // Note that if you add new entities, you'll need to add them here too.
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<ParentEntity> Parents { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }
        public DbSet<AdministratorEntity> AdministratorEntities { get; set; }
        public DbSet<NoteEntity> Notes { get; set; }

        public static SchoolDatabaseContext TestContext() {
            var options = new DbContextOptionsBuilder<SchoolDatabaseContext>()
                .UseSqlite("Filename=test.db")
                .Options;

            var context = new SchoolDatabaseContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        public SchoolDatabaseContext(DbContextOptions<SchoolDatabaseContext> options) : base(options)
        {
        }
    }
}
