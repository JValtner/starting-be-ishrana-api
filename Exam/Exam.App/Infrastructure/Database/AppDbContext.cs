using Exam.App.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Infrastructure.Database;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    
    public DbSet<AnimalType> AnimalTypes => Set<AnimalType>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<MedicalReport> MedicalReports => Set<MedicalReport>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Owner)
            .WithMany(u => u.OwnedPatients)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Veterinarian)
            .WithMany(u => u.AssignedPatients)
            .HasForeignKey(p => p.VeterinarianId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Report)
            .WithOne(r => r.Appointment)
            .HasForeignKey<MedicalReport>(r => r.AppointmentId);


        // Seed Roles
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "Veterinarian", NormalizedName = "VETERINARIAN" },
            new IdentityRole { Name = "Assistant", NormalizedName = "ASSISTANT" },
            new IdentityRole { Name = "Owner", NormalizedName = "OWNER"}
        );

        // Seed Entities
        // AnymalType (seed)
        modelBuilder.Entity<AnimalType>(e =>
        {
            e.Property(x => x.Name).IsRequired();

            e.HasData(
                new AnimalType { Id = 1, Name = "Pas" },
                new AnimalType { Id = 2, Name = "Mačka" },
                new AnimalType { Id = 3, Name = "Papagaj" },
                new AnimalType { Id = 4, Name = "Kornjača" },
                new AnimalType { Id = 5, Name = "Zec" },
                new AnimalType { Id = 6, Name = "Hrčak" }
            );
        });
    }
}
