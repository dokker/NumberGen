﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NumberGen.Data;

#nullable disable

namespace NumberGen.Data.Migrations
{
    [DbContext(typeof(NumberGenDbContext))]
    partial class NumberGenDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("NumberGen")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NumberGen.Model.NgPrime", b =>
                {
                    b.Property<decimal>("Number")
                        .HasColumnType("decimal(20,0)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("GenerationTime")
                        .HasColumnType("bigint");

                    b.HasKey("Number");

                    b.ToTable("NgPrime", "NumberGen");
                });
#pragma warning restore 612, 618
        }
    }
}
