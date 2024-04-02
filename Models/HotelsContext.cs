using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hotels.Models
{
    public partial class HotelsContext : DbContext
    {
        public HotelsContext()
        {
        }

        public HotelsContext(DbContextOptions<HotelsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; } = null!;
        public virtual DbSet<AdministratorHist> AdministratorHists { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingOfService> BookingOfServices { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Excursion> Excursions { get; set; } = null!;
        public virtual DbSet<Floor> Floors { get; set; } = null!;
        public virtual DbSet<Hotel> Hotels { get; set; } = null!;
        public virtual DbSet<Malfunction> Malfunctions { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostHist> PostHists { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomClass> RoomClasses { get; set; } = null!;
        public virtual DbSet<RoomClassHist> RoomClassHists { get; set; } = null!;
        public virtual DbSet<RoomCleaning> RoomCleanings { get; set; } = null!;
        public virtual DbSet<RoomCleaningHist> RoomCleaningHists { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-MPQU2K2E\\DB;Initial Catalog=Hotels;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.IdAdministrator);

                entity.ToTable("Administrator");

                entity.HasIndex(e => e.LoginAdministrator, "UQ_Login_Administrator")
                    .IsUnique();

                entity.Property(e => e.IdAdministrator).HasColumnName("ID_Administrator");

                entity.Property(e => e.FirstNameAdministrator)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First_Name_Administrator");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.LoginAdministrator)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Login_Administrator");

                entity.Property(e => e.MailAdministrator)
                    .IsUnicode(false)
                    .HasColumnName("Mail_Administrator");

                entity.Property(e => e.MiddleNameAdministrator)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name_Administrator");

                entity.Property(e => e.PasswordAdministrator)
                    .IsUnicode(false)
                    .HasColumnName("Password_Administrator");

                entity.Property(e => e.Salt)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SecondNameAdministrator)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name_Administrator");
            });

            modelBuilder.Entity<AdministratorHist>(entity =>
            {
                entity.HasKey(e => e.IdAdministratorHist)
                    .HasName("PK__Administ__1A7B309FC5F8FFA6");

                entity.ToTable("Administrator_hist");

                entity.Property(e => e.IdAdministratorHist).HasColumnName("ID_Administrator_hist");

                entity.Property(e => e.FirstNameAdministratorHist)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First_Name_Administrator_hist");

                entity.Property(e => e.IsDeletedHist).HasColumnName("Is_Deleted_hist");

                entity.Property(e => e.LoginAdministratorHist)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Login_Administrator_hist");

                entity.Property(e => e.MailAdministratorHist)
                    .IsUnicode(false)
                    .HasColumnName("Mail_Administrator_hist");

                entity.Property(e => e.MiddleNameAdministratorHist)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name_Administrator_hist");

                entity.Property(e => e.PasswordAdministratorHist)
                    .IsUnicode(false)
                    .HasColumnName("Password_Administrator_hist");

                entity.Property(e => e.SaltHist)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("Salt_hist");

                entity.Property(e => e.SecondNameAdministratorHist)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name_Administrator_hist");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.IdBooking);

                entity.ToTable("Booking");

                entity.Property(e => e.IdBooking).HasColumnName("ID_Booking");

                entity.Property(e => e.CheckInDateBooking)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Check-in_Date_Booking");

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.DateBooking)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Date_Booking");

                entity.Property(e => e.EvictionDateBooking)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Eviction_Date_Booking");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.NumberBooking)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Number_Booking");

                entity.Property(e => e.PriceBooking)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Price_Booking");

                entity.Property(e => e.RoomId).HasColumnName("Room_ID");

                entity.Property(e => e.TimeBooking)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Time_Booking");

                //entity.HasOne(d => d.Client)
                //    .WithMany(p => p.Bookings)
                //    .HasForeignKey(d => d.ClientId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Client_Booking");

                //entity.HasOne(d => d.Room)
                //    .WithMany(p => p.Bookings)
                //    .HasForeignKey(d => d.RoomId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Room_Booking");
            });

            modelBuilder.Entity<BookingOfService>(entity =>
            {
                entity.HasKey(e => e.IdBookingOfServices);

                entity.ToTable("Booking_Of_Services");

                entity.Property(e => e.IdBookingOfServices).HasColumnName("ID_Booking_Of_Services");

                entity.Property(e => e.BookingId).HasColumnName("Booking_ID");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.ReservationDateBookingOfServices)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Reservation_Date_Booking_Of_Services");

                entity.Property(e => e.ServicesId).HasColumnName("Services_ID");

                //entity.HasOne(d => d.Booking)
                //    .WithMany(p => p.BookingOfServices)
                //    .HasForeignKey(d => d.BookingId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Booking_Booking_Of_Services");

                //entity.HasOne(d => d.Services)
                //    .WithMany(p => p.BookingOfServices)
                //    .HasForeignKey(d => d.ServicesId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Services_Booking_Of_Services");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.ToTable("Client");

                entity.HasIndex(e => e.LoginClient, "UQ_Login_Client")
                    .IsUnique();

                entity.Property(e => e.IdClient).HasColumnName("ID_Client");

                entity.Property(e => e.FirstNameClient)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First_Name_Client");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.LoginClient)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Login_Client");

                entity.Property(e => e.MailClient)
                    .IsUnicode(false)
                    .HasColumnName("Mail_Client");

                entity.Property(e => e.MiddleNameClient)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name_Client");

                entity.Property(e => e.PasswordClient)
                    .IsUnicode(false)
                    .HasColumnName("Password_Client");

                entity.Property(e => e.Salt)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.SecondNameClient)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name_Client");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee);

                entity.ToTable("Employee");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");

                entity.Property(e => e.AdministratorId).HasColumnName("Administrator_ID");

                entity.Property(e => e.FirstNameEmployee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First_Name_Employee");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.MiddleNameEmployee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name_Employee");

                entity.Property(e => e.PostId).HasColumnName("Post_ID");

                entity.Property(e => e.SecondNameEmployee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name_Employee");

                //entity.HasOne(d => d.Administrator)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.AdministratorId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Administrator_Employee");

                //entity.HasOne(d => d.Post)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.PostId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Post_Employee");
            });

            modelBuilder.Entity<Excursion>(entity =>
            {
                entity.HasKey(e => e.IdExcursion);

                entity.ToTable("Excursion");

                entity.Property(e => e.IdExcursion).HasColumnName("ID_Excursion");

                entity.Property(e => e.DateExcursion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Date_Excursion");

                entity.Property(e => e.HotelId).HasColumnName("Hotel_ID");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.RouteExcursion)
                    .IsUnicode(false)
                    .HasColumnName("Route_Excursion");

                entity.Property(e => e.SecondNameGuideExcursion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name_Guide_Excursion");

                entity.Property(e => e.TimeExcursion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Time_Excursion");

                //entity.HasOne(d => d.Hotel)
                //    .WithMany(p => p.Excursions)
                //    .HasForeignKey(d => d.HotelId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Hotel_Excursion");
            });

            modelBuilder.Entity<Floor>(entity =>
            {
                entity.HasKey(e => e.IdFloor);

                entity.ToTable("Floor");

                entity.Property(e => e.IdFloor).HasColumnName("ID_Floor");

                entity.Property(e => e.HotelId).HasColumnName("Hotel_ID");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.NumberFloor)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Number_Floor");

                entity.Property(e => e.NumberOfRoomsFloor)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Number_Of_Rooms_Floor");

                //entity.HasOne(d => d.Hotel)
                //    .WithMany(p => p.Floors)
                //    .HasForeignKey(d => d.HotelId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Hotel_Floor");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.IdHotel);

                entity.ToTable("Hotel");

                entity.HasIndex(e => e.NameHotel, "UQ_Name_Hotel")
                    .IsUnique();

                entity.Property(e => e.IdHotel).HasColumnName("ID_Hotel");

                entity.Property(e => e.AdministratorId).HasColumnName("Administrator_ID");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.NameHotel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Hotel");

                entity.Property(e => e.NumberOfFloorsHotel)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Number_Of_Floors_Hotel");

                entity.Property(e => e.NumberOfRoomsFloorHotel)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("Number_Of_Rooms_Floor_Hotel");

                entity.Property(e => e.NumberOfStarsHotel)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Number_Of_Stars_Hotel");

                //entity.HasOne(d => d.Administrator)
                //    .WithMany(p => p.Hotels)
                //    .HasForeignKey(d => d.AdministratorId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Administrator_Hotel");
            });

            modelBuilder.Entity<Malfunction>(entity =>
            {
                entity.HasKey(e => e.IdMalfunctions);

                entity.Property(e => e.IdMalfunctions).HasColumnName("ID_Malfunctions");

                entity.Property(e => e.DescriptionMalfunctions)
                    .IsUnicode(false)
                    .HasColumnName("Description_Malfunctions");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.PriceMalfunctions)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Price_Malfunctions");

                entity.Property(e => e.RoomId).HasColumnName("Room_ID");

                entity.Property(e => e.TypeMalfunctions)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Type_Malfunctions");

                //entity.HasOne(d => d.Room)
                //    .WithMany(p => p.Malfunctions)
                //    .HasForeignKey(d => d.RoomId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Room_Malfunctions");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.IdPost);

                entity.ToTable("Post");

                entity.HasIndex(e => e.NamePost, "UQ_Name_Post")
                    .IsUnique();

                entity.Property(e => e.IdPost).HasColumnName("ID_Post");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.NamePost)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Name_Post");

                entity.Property(e => e.ResponsibilitiesPost)
                    .IsUnicode(false)
                    .HasColumnName("Responsibilities_Post");

                entity.Property(e => e.SalaryPost)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Salary_Post");
            });

            modelBuilder.Entity<PostHist>(entity =>
            {
                entity.HasKey(e => e.IdPostHist)
                    .HasName("PK__Post_his__A37B660264FE3796");

                entity.ToTable("Post_hist");

                entity.Property(e => e.IdPostHist).HasColumnName("ID_Post_hist");

                entity.Property(e => e.NamePostHist)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Name_Post_hist");

                entity.Property(e => e.ResponsibilitiesPostHist)
                    .IsUnicode(false)
                    .HasColumnName("Responsibilities_Post_hist");

                entity.Property(e => e.SalaryPostHist).HasColumnName("Salary_Post_hist");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.HasIndex(e => e.NameRole, "UQ_Name_Role")
                    .IsUnique();

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Role");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.IdRoom);

                entity.ToTable("Room");

                entity.Property(e => e.IdRoom).HasColumnName("ID_Room");

                entity.Property(e => e.FloorId).HasColumnName("Floor_ID");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.PriceRoom)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("Price_Room");

                entity.Property(e => e.RoomClassId).HasColumnName("Room_Class_ID");

                entity.Property(e => e.RoomCleaningId).HasColumnName("Room_Cleaning_ID");

                //entity.HasOne(d => d.Floor)
                //    .WithMany(p => p.Rooms)
                //    .HasForeignKey(d => d.FloorId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Floor_Room");

                //entity.HasOne(d => d.RoomClass)
                //    .WithMany(p => p.Rooms)
                //    .HasForeignKey(d => d.RoomClassId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Room_Class_Room");

                //entity.HasOne(d => d.RoomCleaning)
                //    .WithMany(p => p.Rooms)
                //    .HasForeignKey(d => d.RoomCleaningId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Room_Cleaning_Room");
            });

            modelBuilder.Entity<RoomClass>(entity =>
            {
                entity.HasKey(e => e.IdRoomClass);

                entity.ToTable("Room_Class");

                entity.HasIndex(e => e.NameRoomClass, "UQ_Name_Room_Class")
                    .IsUnique();

                entity.Property(e => e.IdRoomClass).HasColumnName("ID_Room_Class");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.NameRoomClass)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Name_Room_Class");
            });

            modelBuilder.Entity<RoomClassHist>(entity =>
            {
                entity.HasKey(e => e.IdRoomClassHist)
                    .HasName("PK__Room_Cla__9CB46F09C6BE2D0D");

                entity.ToTable("Room_Class_hist");

                entity.Property(e => e.IdRoomClassHist).HasColumnName("ID_Room_Class_hist");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.NameRoomClassHist)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Name_Room_Class_hist");
            });

            modelBuilder.Entity<RoomCleaning>(entity =>
            {
                entity.HasKey(e => e.IdRoomCleaning);

                entity.ToTable("Room_Cleaning");

                entity.Property(e => e.IdRoomCleaning).HasColumnName("ID_Room_Cleaning");

                entity.Property(e => e.DateOfChangeOfBedLinenRoomCleaning)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Date_Of_Change_Of_Bed_Linen_Room_Cleaning");

                entity.Property(e => e.DateRoomCleaning)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Date_Room_Cleaning");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            });

            modelBuilder.Entity<RoomCleaningHist>(entity =>
            {
                entity.HasKey(e => e.IdRoomClassHist)
                    .HasName("PK__Room_Cle__9CB46F094CE0BEAF");

                entity.ToTable("Room_Cleaning_hist");

                entity.Property(e => e.IdRoomClassHist).HasColumnName("ID_Room_Class_hist");

                entity.Property(e => e.DateOfChangeOfBedLinenRoomCleaningHistHist)
                    .HasColumnType("date")
                    .HasColumnName("Date_Of_Change_Of_Bed_Linen_Room_Cleaning_hist_hist");

                entity.Property(e => e.DateRoomCleaningHist)
                    .HasColumnType("date")
                    .HasColumnName("Date_Room_Cleaning_hist");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.IdServices);

                entity.HasIndex(e => e.NameServices, "UQ_Name_Services")
                    .IsUnique();

                entity.Property(e => e.IdServices).HasColumnName("ID_Services");

                entity.Property(e => e.DescriptionServices)
                    .IsUnicode(false)
                    .HasColumnName("Description_Services");

                entity.Property(e => e.HotelId).HasColumnName("Hotel_ID");

                entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");

                entity.Property(e => e.NameServices)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Services");

                entity.Property(e => e.PriceServices)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("Price_Services");

                //entity.HasOne(d => d.Hotel)
                //    .WithMany(p => p.Services)
                //    .HasForeignKey(d => d.HotelId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Hotel_Services");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.Token1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.TokenDatetime)
                    .HasColumnName("token_datetime")
                    .HasDefaultValueSql("(sysdatetime())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
