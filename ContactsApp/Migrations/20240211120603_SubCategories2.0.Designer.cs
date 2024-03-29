﻿// <auto-generated />
using System;
using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContactsApp.Migrations
{
    [DbContext(typeof(ContactContext))]
    [Migration("20240211120603_SubCategories2.0")]
    partial class SubCategories20
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContactsApp.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Business"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Private"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("ContactsApp.Models.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactId"));

                    b.Property<int?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("ContactId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            ContactId = 1,
                            CategoryId = 1,
                            DateOfBirth = new DateOnly(2000, 12, 25),
                            Email = "sgudmestad@gmail.com",
                            FirstName = "Scott",
                            LastName = "Gudmestad",
                            PhoneNumber = "123-456-7890",
                            SubCategoryId = 2
                        },
                        new
                        {
                            ContactId = 2,
                            CategoryId = 1,
                            DateOfBirth = new DateOnly(1980, 1, 23),
                            Email = "ggudmestad@gmail.com",
                            FirstName = "Gerry",
                            LastName = "Gudmestad",
                            PhoneNumber = "123-456-7890",
                            SubCategoryId = 2
                        },
                        new
                        {
                            ContactId = 3,
                            CategoryId = 2,
                            DateOfBirth = new DateOnly(1983, 2, 3),
                            Email = "bboyer@gmail.com",
                            FirstName = "Blake",
                            LastName = "Boyer",
                            PhoneNumber = "123-456-7890"
                        });
                });

            modelBuilder.Entity("ContactsApp.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoryId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubCategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories");

                    b.HasData(
                        new
                        {
                            SubCategoryId = 1,
                            CategoryId = 1,
                            Name = "Boss"
                        },
                        new
                        {
                            SubCategoryId = 2,
                            CategoryId = 1,
                            Name = "Co-worker"
                        },
                        new
                        {
                            SubCategoryId = 3,
                            CategoryId = 1,
                            Name = "Client"
                        });
                });

            modelBuilder.Entity("ContactsApp.Models.Contact", b =>
                {
                    b.HasOne("ContactsApp.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContactsApp.Models.SubCategory", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId");

                    b.Navigation("Category");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("ContactsApp.Models.SubCategory", b =>
                {
                    b.HasOne("ContactsApp.Models.Category", null)
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContactsApp.Models.Category", b =>
                {
                    b.Navigation("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
