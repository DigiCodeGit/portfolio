﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Portfolio.Data;

#nullable disable

namespace Portfolio.Migrations
{
    [DbContext(typeof(PortalECommerceDbSql))]
    [Migration("20230531045208_CartTables")]
    partial class CartTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Portfolio.Models.Artwork", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Key"), 1L, 1);

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.ToTable("Artwork");
                });

            modelBuilder.Entity("Portfolio.Models.Cart", b =>
                {
                    b.Property<int>("CartKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartKey"), 1L, 1);

                    b.Property<int>("CartItemKey")
                        .HasColumnType("int");

                    b.Property<int>("UserKey")
                        .HasColumnType("int");

                    b.HasKey("CartKey");

                    b.HasIndex("CartItemKey");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Portfolio.Models.CartItem", b =>
                {
                    b.Property<int>("CartItemKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemKey"), 1L, 1);

                    b.Property<int>("Key")
                        .HasColumnType("int");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.HasKey("CartItemKey");

                    b.HasIndex("Key");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("Portfolio.Models.Cart", b =>
                {
                    b.HasOne("Portfolio.Models.CartItem", "CartItem")
                        .WithMany()
                        .HasForeignKey("CartItemKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartItem");
                });

            modelBuilder.Entity("Portfolio.Models.CartItem", b =>
                {
                    b.HasOne("Portfolio.Models.Artwork", "Artwork")
                        .WithMany()
                        .HasForeignKey("Key")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artwork");
                });
#pragma warning restore 612, 618
        }
    }
}
