﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend;

namespace backend.Migrations
{
    [DbContext(typeof(AppContext))]
    partial class AppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085");

            modelBuilder.Entity("backend.Models.Canteen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AcceptCards");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Canteens");
                });

            modelBuilder.Entity("backend.Models.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<uint>("Calorie");

                    b.Property<int>("MealCategoryId");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<uint>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("MealCategoryId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("backend.Models.MealCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CanteenId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CanteenId");

                    b.ToTable("MealCategories");
                });

            modelBuilder.Entity("backend.Models.Meal", b =>
                {
                    b.HasOne("backend.Models.MealCategory", "MealCategory")
                        .WithMany()
                        .HasForeignKey("MealCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backend.Models.MealCategory", b =>
                {
                    b.HasOne("backend.Models.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
