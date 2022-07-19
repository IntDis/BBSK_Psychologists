using BBSK_Psycho.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace BBSK_Psycho.DataLayer;

public class BBSK_PsychoContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Psychologist> Psychologists { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Problem> Problems { get; set; }
    public DbSet<TherapyMethod> TherapyMethods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Education> Educations { get; set; }


    public BBSK_PsychoContext(DbContextOptions<BBSK_PsychoContext> option)
        : base(option)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable(nameof(Order));
            entity.HasKey(o => o.Id);

            entity
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders);
            entity
                .HasOne(o => o.Psychologist)
                .WithMany(c => c.Orders);

            entity
                .Property(p => p.Cost)
                .HasPrecision(7, 2);

            entity.Property(c => c.Message).HasMaxLength(255);

        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable(nameof(Comment));
            entity.HasKey(c => c.Id);

            entity
                .HasOne(c => c.Client)
                .WithMany(c => c.Comments);

            entity
                .HasOne(c => c.Psychologist)
                .WithMany(p => p.Comments);

            entity.Property(c => c.Text).HasMaxLength(255);

        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable(nameof(Client));
            entity.HasKey(c => c.Id);


            entity.Property(c => c.Name).HasMaxLength(50);
            entity.Property(c => c.LastName).HasMaxLength(50);
            entity.Property(c => c.Email).HasMaxLength(140);
            entity.Property(c => c.Password).HasMaxLength(140);
            entity.Property(c => c.PhoneNumber).HasMaxLength(12);
            entity.Property(c => c.RegistrationDate)
            .HasDefaultValueSql("getdate()");

        });

        modelBuilder.Entity<Psychologist>(entity =>
        {
            entity.ToTable(nameof(Psychologist));
            entity.HasKey(p => p.Id);

            
            entity.Property(p => p.Price)
                    .HasPrecision(7, 2);

            entity.Property(p => p.Name).HasMaxLength(50);
            entity.Property(p => p.LastName).HasMaxLength(50);
            entity.Property(p => p.Patronymic).HasMaxLength(50);
            entity.Property(p => p.Phone).HasMaxLength(11);
            entity.Property(p => p.Email).HasMaxLength(140);
            entity.Property(p => p.Password).HasMaxLength(140);
            entity.Property(p => p.PasportData).HasMaxLength(255);

        });

       

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable(nameof(Schedule));
            entity.HasKey(s => s.Id);

            entity
                .HasOne(s => s.Psychologist)
                .WithMany(p => p.Schedules);
        });

        modelBuilder.Entity<Problem>(entity =>
        {
            entity.ToTable(nameof(Problem));
            entity.HasKey(p => p.Id);

            entity
                .HasMany(p => p.Psychologists)
                .WithMany(p => p.Problems);

            entity.Property(p => p.ProblemName).HasMaxLength(50);
        });

        modelBuilder.Entity<TherapyMethod>(entity =>
        {
            entity.ToTable(nameof(TherapyMethod));
            entity.HasKey(t => t.Id);

            entity
                .HasMany(t => t.Psychologists)
                .WithMany(p => p.TherapyMethods);

            entity.Property(t => t.Method).HasMaxLength(50);
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.ToTable(nameof(Education));
            entity.HasKey(e => e.Id);

            entity
                .HasOne(e => e.Psychologist)
                .WithMany(p => p.Educations);


            entity.Property(e =>e.EducationData ).HasMaxLength(255);
        });

        modelBuilder.Entity<ApplicationForPsychologistSearch>(entity =>
        {
            entity.ToTable(nameof(ApplicationForPsychologistSearch));
            entity.HasKey(r => r.Id);

            entity
            .HasOne(r => r.Client)
            .WithMany(r => r.ApplicationForPsychologistSearch);

            entity.Property(r => r.Name).HasMaxLength(50);
            entity.Property(r => r.PhoneNumber).HasMaxLength(11);
            entity.Property(r => r.Description).HasMaxLength(255);
            entity.Property(r => r.Description).HasMaxLength(255);
            
            entity.Property(r => r.CostMin)
                .HasPrecision(7, 2);

            entity.Property(r => r.CostMax)
                .HasPrecision(7, 2);
        });
    }
} 