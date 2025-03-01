﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YouTubeWebAPI.Models;

#nullable disable

namespace YouTubeWebAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250116113005_WeatherFifthMigration")]
    partial class WeatherFifthMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YouTubeWebAPI.Models.Documents.SearchedDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedAT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastAccessTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastWriteTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Length")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfflineUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OnlineUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SearchedDocuments");
                });

            modelBuilder.Entity("YouTubeWebAPI.Models.Prompts.Prompt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PdfPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PromptString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Prompts");
                });

            modelBuilder.Entity("YouTubeWebAPI.Models.Prompts.PromptRawResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PromptString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PromptRawResults");
                });

            modelBuilder.Entity("YouTubeWebAPI.Models.Search.NewsAPI.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Birthdate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("YouTubeWebAPI.Models.Search.SearchLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MaxResults")
                        .HasColumnType("int");

                    b.Property<string>("Search")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SearchDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SearchLogs");
                });

            modelBuilder.Entity("YouTubeWebAPI.Models.Search.Weather.CityModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Growth_from_2000_to_2013")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("YouTubeWebAPI.Models.Search.Weather.Staistics.Humidity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Max")
                        .HasColumnType("float");

                    b.Property<double>("Mean")
                        .HasColumnType("float");

                    b.Property<double>("Median")
                        .HasColumnType("float");

                    b.Property<double>("Min")
                        .HasColumnType("float");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Std_dev")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Humidities");
                });

            modelBuilder.Entity("YouTubeWebAPI.Models.Search.Weather.Staistics.WeatherTemperature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Max")
                        .HasColumnType("float");

                    b.Property<double>("Mean")
                        .HasColumnType("float");

                    b.Property<double>("Median")
                        .HasColumnType("float");

                    b.Property<double>("Min")
                        .HasColumnType("float");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Std_dev")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Temperatures");
                });

            modelBuilder.Entity("YouTubeWebAPI.Models.Search.Weather.Staistics.WindSpeed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Max")
                        .HasColumnType("float");

                    b.Property<double>("Mean")
                        .HasColumnType("float");

                    b.Property<double>("Median")
                        .HasColumnType("float");

                    b.Property<double>("Min")
                        .HasColumnType("float");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Std_dev")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("WindSpeeds");
                });

            modelBuilder.Entity("YouTubeWebAPI.Models.Search.Weather.WeatherVisualization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EncodedImageCurrent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EncodedImageHourly")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HumidityStats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Interpretation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemperatureStats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WindSpeedStats")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Weathers");
                });
#pragma warning restore 612, 618
        }
    }
}
