using Microsoft.EntityFrameworkCore;
using OnatrixInlamning.Models;

namespace OnatrixInlamning.Data
{
	public class ApplicationDBcontext : DbContext
	{

		public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options)
		   : base(options)
		{

		}

		DbSet<ContactFormModel> contactModels { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) =>
			modelBuilder.Entity<ContactFormModel>(entity =>
			{
				entity.ToTable("contactForm");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id).HasColumnName("id");
				entity.Property(e => e.Name).HasColumnName("Name");
				entity.Property(e => e.Email).HasColumnName("Email");
				entity.Property(e => e.Phone).HasColumnName("Phone");
				entity.Property(e => e.Option).HasColumnName("Option");
			});


	}
}
