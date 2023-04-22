using AstronBase.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AstronBase.DAL
{
    public sealed class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Client> Client { get; set; } = default!;

        public DbSet<Company> Company { get; set; } = default!;

        public DbSet<Engineer> Engineer { get; set; } = default!;

        public DbSet<Fiscal> Fiscal { get; set; } = default!;

        public DbSet<Model> Model { get; set; } = default!;

        public DbSet<RegisterState> RegisterState { get; set; } = default!;

        public DbSet<Request> Request { get; set; } = default!;

        public DbSet<StatusBlank> StatusBlank { get; set; } = default!;

        public DbSet<StatusFiscal> StatusFiscal { get; set; } = default!;

        public DbSet<StatusRequest> StatusRequest { get; set; } = default!;

        public DbSet<Store> Store { get; set; } = default!;

        public DbSet<TypeRequest> TypeRequest { get; set; } = default!;

    }
}