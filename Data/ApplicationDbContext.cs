using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebAppMovie.Models;

namespace WebAppMovie.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Person> Persons { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasMany(p => p.Producers)
                .WithMany(m => m.Movies)
                .UsingEntity<ProducerMovies>(
                    j => j
                        .HasOne(pm => pm.Producer)
                        .WithMany()
                        .HasForeignKey(p => p.ProducerId),
                    j => j
                        .HasOne(pm => pm.Movie)
                        .WithMany()
                        .HasForeignKey(m => m.MovieId)
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
