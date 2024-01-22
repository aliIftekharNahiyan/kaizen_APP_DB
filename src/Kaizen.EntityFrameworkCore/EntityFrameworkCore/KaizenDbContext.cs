using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Kaizen.Authorization.Roles;
using Kaizen.Authorization.Users;
using Kaizen.MultiTenancy;
using Kaizen.Entities.Addresses;
using Kaizen.Entities.Invoicing;
using Kaizen.Entities.PhoneNumbers;
using Kaizen.Entities.LinkTables;
using Kaizen.Entities.Notes;
using Kaizen.Entities.PaymentTerms;
using Kaizen.Entities.SupportTypes;
using Kaizen.Entities.BulkProcess;
using Kaizen.Entities.Universitys;
using Kaizen.Entities.Disabilitys;
using Kaizen.Entities.StorageFiles;
using Kaizen.Entities.ContactMethods;
using Kaizen.Entities.Regions;
using Kaizen.Entities.RegionLocations;
using Kaizen.Entities.CustomerTypes;
using Kaizen.Entities.SessionGroups;
using Kaizen.Entities.FundingBodys;
using Kaizen.Entities.Checklists;
using System.Reflection.Emit;
using Kaizen.Entities.SessionTypes;
using Kaizen.Entities.Companies;
using Kaizen.Entities.AcademicYears;
using Kaizen.Entities.AcademicTerms;
using Kaizen.Entities.Feedbacks;
using Kaizen.Entities.Lookups;
using Kaizen.Entities.Skills;
using Kaizen.Entities.UserKins;
using Kaizen.Entities.UserLookups;
using Kaizen.Entities.Courses;
using Kaizen.Entities.CourseTerms;
using Kaizen.Entities.UserClientSupports;
using Kaizen.Entities.SupportSessions;
using Kaizen.Entities.Communications;
using Kaizen.Entities.CommunicationGroups;
using Kaizen.Entities.CommunicationTemplates;
using Kaizen.Entities.InfoChecklists;

namespace Kaizen.EntityFrameworkCore
{
    public class KaizenDbContext : AbpZeroDbContext<Tenant, Role, User, KaizenDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public KaizenDbContext(DbContextOptions<KaizenDbContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionGroup>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<SupportType>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<Disability>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<University>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<Checklist>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<ChecklistItem>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<ChecklistItemOption>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<Company>().HasQueryFilter(p => !p.Deleted);
            modelBuilder.Entity<SessionType>().HasQueryFilter(p => !p.Deleted);

            modelBuilder.Entity<UserClientSupport>()
                .HasOne(c => c.Users)
                .WithOne()
                .HasForeignKey<UserClientSupport>(c => c.UserId)
                .HasForeignKey<UserClientSupport>(c => c.ClientId);



            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Address> Address { get; set; }
        public DbSet<PhoneNumber> PhoneNumber { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<SessionGroup> SessionGroup { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<PaymentTerm> PaymentTerm { get; set; }
        public DbSet<SupportType> SupportType { get; set; }
        public DbSet<BulkProcessJob> BulkProcessJob { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<Disability> Disability { get; set; }
        public DbSet<StorageFile> StorageFile { get; set; }
        public DbSet<ContactMethod> ContactMethod { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<RegionLocation> RegionLocation { get; set; }
        public DbSet<CustomerType> CustomerType { get; set; }
        public DbSet<FundingBody> FundingBody { get; set; }
        public DbSet<AcademicYear> AcademicYear { get; set; }
        public DbSet<AcademicTerm> AcademicTerm { get; set; }

        public DbSet<Checklist> Checklist { get; set; }
        public DbSet<ChecklistItem> ChecklistItem { get; set; }
        public DbSet<ChecklistItemOption> ChecklistItemOption { get; set; }

        public DbSet<Course> Course { get; set; }
        public DbSet<CourseTerm> CourseTerm { get; set; }
        public DbSet<SessionType> SessionType { get; set; }
        public DbSet<SupportSession> SupportSession { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Communication> Communication { get; set; }
        public DbSet<CommunicationGroup> CommunicationGroup { get; set; }
        public DbSet<CommunicationTemplate> CommunicationTemplate { get; set; }

        // Linked tables
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<UserSkill> UserSkill { get; set; }
        public DbSet<UserLivingRegionLocation> UserLivingRegionLocation { get; set; }
        public DbSet<UserWorkRegionLocation> UserWorkRegionLocation { get; set; }
        public DbSet<UserDisability> UserDisability { get; set; }
        public DbSet<ChecklistCheckListItem> ChecklistChecklistItem { get; set; }
        public DbSet<CommunicationGroupUser> CommunicationGroupUser { get; set; }
        public DbSet<InfoCheckList> InfoCheckList { get; set; }

        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Lookup> Lookup { get; set; }
        public DbSet<UserKin> UserKin { get; set; }
        public DbSet<UserLookup> UserLookup { get; set; }
        public DbSet<Skill> Skill { get; set; }

        public DbSet<UserClientSupport> UserClientSupport { get; set; }
    }
}
