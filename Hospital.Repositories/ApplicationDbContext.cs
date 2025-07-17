using Hospital.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<HospitalInfo> HospitalInfos { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineReport> MedicineReports { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<PrescribedMedicine> PrescribedMedicines { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<TestPrice> TestPrices { get; set; }
        public DbSet<PatientReport> PatientReports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // TestPrice ↔ Lab (One-To-Many), Restrict delete
            builder.Entity<TestPrice>()
                .HasOne(tp => tp.Lab)
                .WithMany(l => l.TestPrices)
                .HasForeignKey(tp => tp.LabId)
                .OnDelete(DeleteBehavior.Restrict);

            // TestPrice ↔ Bill (One-To-Many), Restrict delete
            builder.Entity<TestPrice>()
                .HasOne(tp => tp.Bill)
                .WithMany(b => b.TestPrices)
                .HasForeignKey(tp => tp.BillId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment ↔ ApplicationUser relations:
            builder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(u => u.DoctorAppointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(u => u.PatientAppointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // PatientReport ↔ ApplicationUser relations:
            builder.Entity<PatientReport>()
                .HasOne(pr => pr.Doctor)
                .WithMany(u => u.PatientReportsAsDoctor)
                .HasForeignKey(pr => pr.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PatientReport>()
                .HasOne(pr => pr.Patient)
                .WithMany(u => u.PatientReportsAsPatient)
                .HasForeignKey(pr => pr.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
