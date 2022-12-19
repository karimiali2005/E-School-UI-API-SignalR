using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatabaseLayer.Models
{
    public partial class ESchoolContext : DbContext
    {
        public ESchoolContext()
        {
        }

        public ESchoolContext(DbContextOptions<ESchoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accessories> Accessories { get; set; }
        //public virtual DbSet<BrowserType> BrowserType { get; set; }
        public virtual DbSet<Calendar> Calendar { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Degree> Degree { get; set; }
        //public virtual DbSet<DeviceType> DeviceType { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<GroupObject> GroupObject { get; set; }
        public virtual DbSet<LogSms> LogSms { get; set; }
        public virtual DbSet<LogSmsResult> LogSmsResult { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<LoginPlatform> LoginPlatform { get; set; }
        public virtual DbSet<Object> Object { get; set; }
        public virtual DbSet<ParentStatus> ParentStatus { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleAccess> RoleAccess { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomChat> RoomChat { get; set; }
        public virtual DbSet<RoomChatGroup> RoomChatGroup { get; set; }
        public virtual DbSet<RoomDetail> RoomDetail { get; set; }
        public virtual DbSet<RoomTeacher> RoomTeacher { get; set; }
        public virtual DbSet<RoomType> RoomType { get; set; }
        public virtual DbSet<RoomUser> RoomUser { get; set; }
        public virtual DbSet<StudyField> StudyField { get; set; }
        public virtual DbSet<TeacherCourse> TeacherCourse { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Week> Week { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Need.Codes.GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accessories>(entity =>
            {
                entity.Property(e => e.AccessoriesId)
                    .HasColumnName("AccessoriesID")
                    .HasComment("کلید اصلی جدول دسترسی کاربران")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccessoriesNameEn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("نام دسترسی انگلیسی");

                entity.Property(e => e.AccessoriesNameFa)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasComment("نام دسترسی");

                entity.Property(e => e.ObjectId)
                    .HasColumnName("ObjectID")
                    .HasComment("کلید خارجی جدول فرم");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.Accessories)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accessories_Object");
            });

            //modelBuilder.Entity<BrowserType>(entity =>
            //{
            //    entity.Property(e => e.BrowserTypeId)
            //        .HasColumnName("BrowserTypeID")
            //        .HasComment("کلید اصلی جدول نوع مرورگر")
            //        .ValueGeneratedNever();

            //    entity.Property(e => e.BrowserTypeTitle)
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .HasComment("عنوان نوع مرورگر");
            //});

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.HasComment("جدول ثبت روزهای تعطیل");

                entity.Property(e => e.CalendarId)
                    .HasColumnName("CalendarID")
                    .HasComment("کلید اصلی جدول");

                entity.Property(e => e.CalendarComment).HasComment("توضیحات");

                entity.Property(e => e.CalendarDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ تعطیلی");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasComment("تعریف درس ها");

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .HasComment("کلید اصلی");

                entity.Property(e => e.CourseTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("عنوان درس");
            });

            modelBuilder.Entity<Degree>(entity =>
            {
                entity.HasComment("مقطع تحصیلی-متوسطه اول");

                entity.Property(e => e.DegreeId)
                    .HasColumnName("DegreeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DegreeTitle)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            //modelBuilder.Entity<DeviceType>(entity =>
            //{
            //    entity.Property(e => e.DeviceTypeId)
            //        .HasColumnName("DeviceTypeID")
            //        .HasComment("کلید اصلی نوع سخت افزار")
            //        .ValueGeneratedNever();

            //    entity.Property(e => e.DeviceTypeTitle)
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .HasComment("عنوان نوع درایور");
            //});

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasComment("پایه تحصیلی-نهم");

                entity.Property(e => e.GradeId)
                    .HasColumnName("GradeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DegreeId).HasColumnName("DegreeID");

                entity.Property(e => e.GradeTitle)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.Grade)
                    .HasForeignKey(d => d.DegreeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_Degree");
            });

            modelBuilder.Entity<GroupObject>(entity =>
            {
                entity.Property(e => e.GroupObjectId)
                    .HasColumnName("GroupObjectID")
                    .HasComment(" کلید اصلی جدول گروه فرم");

                entity.Property(e => e.GroupObjectName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("نام گروه فرم");

                entity.Property(e => e.GroupObjectOrder).HasComment("ترتیب گروه فرم ها");
            });

            modelBuilder.Entity<LogSms>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Explain)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MessageText).IsRequired();

                entity.Property(e => e.MobileNumbers).IsRequired();

                entity.Property(e => e.RecId).IsRequired();

                entity.Property(e => e.SendNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.LogSmsResult)
                    .WithMany(p => p.LogSms)
                    .HasForeignKey(d => d.LogSmsResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LogSms_LogSmsResult");
            });

            modelBuilder.Entity<LogSmsResult>(entity =>
            {
                entity.Property(e => e.LogSmsResultId).ValueGeneratedNever();

                entity.Property(e => e.LogSmsResultText).IsRequired();
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.LoginId)
                    .HasColumnName("LoginID")
                    .HasComment("کلید اصلی جدول ورود به سامانه");

                entity.Property(e => e.AppVersion)
                    .HasColumnName("appVersion")
                    .HasComment("ورژن نسخه طراحی شده ما وب-اندروید-iOS");


                entity.Property(e => e.BrowserVersion)
                    .HasMaxLength(50)
                    .HasComment("ورژن مرورگر-اندروید-iOS");

                entity.Property(e => e.ComputerName)
                    .HasMaxLength(50)
                    .HasComment("نام کامپیوتر");

                entity.Property(e => e.DateExit)
                    .HasColumnType("datetime")
                    .HasComment("زمان خروج");

                entity.Property(e => e.DateLogin)
                    .HasColumnType("datetime")
                    .HasComment("زمان ورود");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasComment("توضیحات");

                

                entity.Property(e => e.DomainName)
                    .HasMaxLength(50)
                    .HasComment("نام دامنه");

                entity.Property(e => e.Ips)
                    .HasColumnName("IPs")
                    .HasMaxLength(50)
                    .HasComment("IPهای ورود");

                entity.Property(e => e.Ipv6)
                    .HasColumnName("IPv6")
                    .HasMaxLength(50)
                    .HasComment("IP ورژن 6");

                entity.Property(e => e.LoginPlatformId)
                    .HasColumnName("LoginPlatformID")
                    .HasComment("کلید خاریجی نوع platform");

                entity.Property(e => e.LoginSuccess).HasComment("آیا ورود با موفقیت بوده؟");

                entity.Property(e => e.MobileName)
                    .HasColumnName("mobileName")
                    .HasMaxLength(100)
                    .HasComment("نام موبایل");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasComment("کلید خارجی به جدول کاربر");

              

                entity.HasOne(d => d.LoginPlatform)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.LoginPlatformId)
                    .HasConstraintName("FK_Login_LoginPlatform");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Login_User");
            });

            modelBuilder.Entity<LoginPlatform>(entity =>
            {
                entity.Property(e => e.LoginPlatformId)
                    .HasColumnName("LoginPlatformID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LoginPlatformTitle)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Object>(entity =>
            {
                entity.Property(e => e.ObjectId)
                    .HasColumnName("ObjectID")
                    .HasComment("کلید اصلی جدول اشیا ");

                entity.Property(e => e.GroupObjectId)
                    .HasColumnName("GroupObjectID")
                    .HasComment("کلید خارجی جدول گروه اشیا");

                entity.Property(e => e.MyObjectOrder).HasComment(" ترتیب اشیا");

                entity.Property(e => e.ObjectNameEn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("نام اشیا");

                entity.Property(e => e.ObjectNameFa)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("نام فارسی اشیا ");

                entity.HasOne(d => d.GroupObject)
                    .WithMany(p => p.Object)
                    .HasForeignKey(d => d.GroupObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Object_GroupObject");
            });

            modelBuilder.Entity<ParentStatus>(entity =>
            {
                entity.HasComment("وضعیت والدین-در قید حیات-شهید-جانباز-فوت");

                entity.Property(e => e.ParentStatusId).HasColumnName("ParentStatusID");

                entity.Property(e => e.ParentStatusTitle)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasComment("کلید اصلی جدول نقش");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("نام نقش ");
            });

            modelBuilder.Entity<RoleAccess>(entity =>
            {
                entity.Property(e => e.RoleAccessId)
                    .HasColumnName("RoleAccessID")
                    .HasComment("کلید اصلی جدول دسترسی نقش");

                entity.Property(e => e.AccessoriesId)
                    .HasColumnName("AccessoriesID")
                    .HasComment("کلید خارجی جدول دسترسی ها");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasComment("کلید خارجی جدول نقش");

                entity.HasOne(d => d.Accessories)
                    .WithMany(p => p.RoleAccess)
                    .HasForeignKey(d => d.AccessoriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleAccess_Accessories");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleAccess)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleAccess_Role");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasComment("اتاق های اموزش و سخنرانی");

                entity.Property(e => e.RoomId)
                    .HasColumnName("RoomID")
                    .HasComment("کلید اصلی");

                entity.Property(e => e.CloseOnTime)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("کلاس سرساعت شروع و قطع شود");

                entity.Property(e => e.DegreeId)
                    .HasColumnName("DegreeID")
                    .HasComment("مقطع تحصیلی-متوسطه اول");

                entity.Property(e => e.GradeId)
                    .HasColumnName("GradeID")
                    .HasComment("پایه تحصیلی-نهم");

                entity.Property(e => e.PinRoomChatId).HasColumnName("PinRoomChatID");

                entity.Property(e => e.RoomFinishDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ پایان کلاس ها");

                entity.Property(e => e.RoomStartDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ شروع کلاس ها");

                entity.Property(e => e.RoomTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("عنوان");

                entity.Property(e => e.RoomTypeId)
                    .HasColumnName("RoomTypeID")
                    .HasComment("کلید خارجی نوع اتاق درس");

                entity.Property(e => e.StudyFieldId)
                    .HasColumnName("StudyFieldID")
                    .HasComment("رشته تحصیلی-تجربی");

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.DegreeId)
                    .HasConstraintName("FK_Room_Degree");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK_Room_Grade");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.RoomTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_RoomType");

                entity.HasOne(d => d.StudyField)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.StudyFieldId)
                    .HasConstraintName("FK_Room_StudyField");
            });

            modelBuilder.Entity<RoomChat>(entity =>
            {
                entity.Property(e => e.RoomChatId).HasColumnName("RoomChatID");

                entity.Property(e => e.Filename).HasMaxLength(500);
                entity.Property(e => e.Filename).HasMaxLength(500);

                entity.Property(e => e.RecieverId).HasColumnName("RecieverID");

                entity.Property(e => e.RecieverName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RoomChatDate).HasColumnType("datetime");

                entity.Property(e => e.RoomChatDelete).HasComment("حذف پیام");

                entity.Property(e => e.RoomChatParentId)
                    .HasColumnName("RoomChatParentID")
                    .HasComment("برای Reply استقاده شود");

                entity.Property(e => e.RoomChatUpdate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ و ویرایش زمان چت");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.Property(e => e.SenderName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.Property(e => e.TextChat).IsRequired();

                entity.HasOne(d => d.Reciever)
                    .WithMany(p => p.RoomChatReciever)
                    .HasForeignKey(d => d.RecieverId)
                    .HasConstraintName("FK_RoomChat_User1");

                entity.HasOne(d => d.RoomChatParent)
                    .WithMany(p => p.InverseRoomChatParent)
                    .HasForeignKey(d => d.RoomChatParentId)
                    .HasConstraintName("FK_RoomChat_RoomChat");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomChat)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomChat_Room");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.RoomChatSender)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomChat_User");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.RoomChatTeacher)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomChat_User2");
            });

            modelBuilder.Entity<RoomChatGroup>(entity =>
            {
                entity.Property(e => e.RoomChatGroupId).HasColumnName("RoomChatGroupID");

              
            });

            modelBuilder.Entity<RoomDetail>(entity =>
            {
                entity.HasComment("جزئیات روزها و ساعت های ");

                entity.Property(e => e.RoomDetailId)
                    .HasColumnName("RoomDetailID")
                    .HasComment("کلید اصلی");

                entity.Property(e => e.CloseOnTime)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("کلاس سرساعت شروع و قطع شود");

                entity.Property(e => e.Comment).HasComment("توضیحات کلاس وطرح درس");

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .HasComment("درس");

                entity.Property(e => e.RoomDetailDate)
                    .HasColumnType("date")
                    .HasComment("تاریخ کلاس");

                entity.Property(e => e.RoomDetailTimeFinish)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasComment("ساعت پایان کلاس");

                entity.Property(e => e.RoomDetailTimeStart)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasComment("ساعت شروع کلاس");

                entity.Property(e => e.RoomId)
                    .HasColumnName("RoomID")
                    .HasComment("کلید خارجی به جدول اتاق ها");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasComment("معلم کلاس");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.RoomDetail)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_RoomDetail_Course");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomDetail)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomDetail_Room");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoomDetail)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RoomDetail_User");
            });

            modelBuilder.Entity<RoomTeacher>(entity =>
            {
                entity.HasComment("جدول کلاس های هر استاد");

                entity.Property(e => e.RoomTeacherId).HasColumnName("RoomTeacherID");

                entity.Property(e => e.AdobeLiveAddress)
                    .HasMaxLength(1000)
                    .HasComment("آدرس لایو Adobe");

                entity.Property(e => e.AdobeLivePass)
                    .HasMaxLength(50)
                    .HasComment("پسورد لایو Adobe");

                entity.Property(e => e.AdobeLiveUsername)
                    .HasMaxLength(50)
                    .HasComment("یوزر لایو Adobe");

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .HasComment("درس");

                entity.Property(e => e.ExamAddress).HasMaxLength(1000);
                entity.Property(e => e.ZoomAddress).HasMaxLength(1000);

                entity.Property(e => e.JitsiLiveAddress)
                    .HasMaxLength(1000)
                    .HasComment("آدرس لایو jitsi");

                entity.Property(e => e.JitsiLivePassword)
                    .HasMaxLength(50)
                    .HasComment("پسورد لایو jitsi");

                entity.Property(e => e.RoomId)
                    .HasColumnName("RoomID")
                    .HasComment("کلاس");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasComment("استاد");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.RoomTeacher)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_RoomTeacher_Course");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomTeacher)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomTeacher_Room");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoomTeacher)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RoomTeacher_User");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.HasComment("نوع اتاق درس");

                entity.Property(e => e.RoomTypeId)
                    .HasColumnName("RoomTypeID")
                    .HasComment("کلید اصلی");

                entity.Property(e => e.RoomTypeTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("عنوان");
            });

            modelBuilder.Entity<RoomUser>(entity =>
            {
                entity.HasComment("جدول دانش اموزان و افراد داخل کلاس");

                entity.Property(e => e.RoomUserId)
                    .HasColumnName("RoomUserID")
                    .HasComment("کلید اصلی");

                entity.Property(e => e.RoomId)
                    .HasColumnName("RoomID")
                    .HasComment("کلاس ها");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasComment("دانش اموزان");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomUser)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomUser_Room");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoomUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomUser_User");
            });

            modelBuilder.Entity<StudyField>(entity =>
            {
                entity.HasComment("رشته تحصیلی-تجربی");

                entity.Property(e => e.StudyFieldId)
                    .HasColumnName("StudyFieldID")
                    .ValueGeneratedNever();

                entity.Property(e => e.StudyFieldTitle)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TeacherCourse>(entity =>
            {
                entity.HasComment("انتخاب درس های معلم");

                entity.Property(e => e.TeacherCourseId)
                    .HasColumnName("TeacherCourseID")
                    .HasComment("کلید اصلی");

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .HasComment("انتخاب درس های معلم");

                entity.Property(e => e.DegreeId)
                    .HasColumnName("DegreeID")
                    .HasComment("مقطع تحصیلی-متوسطه اول");

                entity.Property(e => e.GradeId)
                    .HasColumnName("GradeID")
                    .HasComment("پایه تحصیلی-نهم");

                entity.Property(e => e.StudyFieldId)
                    .HasColumnName("StudyFieldID")
                    .HasComment("رشته تحصیلی-تجربی");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasComment("انتخاب کاربر معلم");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TeacherCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherCourse_Course");

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.TeacherCourse)
                    .HasForeignKey(d => d.DegreeId)
                    .HasConstraintName("FK_TeacherCourse_Degree");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.TeacherCourse)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK_TeacherCourse_Grade");

                entity.HasOne(d => d.StudyField)
                    .WithMany(p => p.TeacherCourse)
                    .HasForeignKey(d => d.StudyFieldId)
                    .HasConstraintName("FK_TeacherCourse_StudyField");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TeacherCourse)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherCourse_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasComment("جدول کاربران");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasComment("کلید اصلی");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .HasComment("آدرس");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ تولد");

                entity.Property(e => e.BirthPlace)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("محل تولد");

                entity.Property(e => e.DegreeId)
                    .HasColumnName("DegreeID")
                    .HasComment("مقطع تحصیلی-متوسطه اول");

                entity.Property(e => e.FamilyNumber).HasComment("تعداد اعضای خانواده");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("نام");

                entity.Property(e => e.GradeId)
                    .HasColumnName("GradeID")
                    .HasComment("پایه تحصیلی-نهم");

                entity.Property(e => e.IdcardPlace)
                    .IsRequired()
                    .HasColumnName("IDCardPlace")
                    .HasMaxLength(50)
                    .HasComment("محل صدور شناسنامه");

                entity.Property(e => e.IdcardSerial)
                    .IsRequired()
                    .HasColumnName("IDCardSerial")
                    .HasMaxLength(10)
                    .HasComment("شماره سریال شناسنامه");

                entity.Property(e => e.IdcardSeriesLetter)
                    .IsRequired()
                    .HasColumnName("IDCardSeriesLetter")
                    .HasMaxLength(10)
                    .HasComment("حرف سری شناسنامه");

                entity.Property(e => e.IdcardSeriesNumber)
                    .IsRequired()
                    .HasColumnName("IDCardSeriesNumber")
                    .HasMaxLength(10)
                    .HasComment("شماره سری شناسنامه");

                entity.Property(e => e.Insurance)
                    .HasMaxLength(50)
                    .HasComment("بیمه پایه");

                entity.Property(e => e.Irnational)
                    .IsRequired()
                    .HasColumnName("IRNational")
                    .HasMaxLength(10)
                    .HasComment("کدملی");

                entity.Property(e => e.IsCityPlace).HasComment("محل سکونت شهر است");

                entity.Property(e => e.IsRelativesParents).HasComment("نسبت خویشاوندی بین والدین وجود دارد؟");

                entity.Property(e => e.IsStudentInsurance).HasComment("بیمه تحصیلی دارد");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("نام خانوادگی");

                entity.Property(e => e.LivePlace)
                    .HasMaxLength(50)
                    .HasComment("با چه کسی زندگی میکنید");

                entity.Property(e => e.LivePlaceOther)
                    .HasMaxLength(50)
                    .HasComment("با چه کسی زندگی میکنید");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasComment("شماره موبایل");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(50)
                    .HasComment("ملیت");

                entity.Property(e => e.ParentDegree)
                    .HasMaxLength(50)
                    .HasComment("مدرک تحصیلی پدر مادر");

                entity.Property(e => e.ParentJob)
                    .HasMaxLength(500)
                    .HasComment("شغل و محل کار");

                entity.Property(e => e.ParentStatusId)
                    .HasColumnName("ParentStatusID")
                    .HasComment("وضعیت والدین-در قید حیات-شهید-جانباز-فوت");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasComment("رمز عبور");

                entity.Property(e => e.PasswordToken)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.PersianLanguage).HasComment("زبان فارسی هست");

                entity.Property(e => e.PicName)
                    .HasMaxLength(200);

                /*entity.Property(e => e.Pic).HasComment("عکس اصلی");

                entity.Property(e => e.PicThumb)
                    .HasColumnName("Pic_Thumb")
                    .HasComment("عکس کوچیک شده");*/

                entity.Property(e => e.PreschoolEducation).HasComment("آموزش قبل از دبستان دارد");

                entity.Property(e => e.ReasonInactive)
                    .HasMaxLength(200)
                    .HasComment("علت غیر فعال بودن");

                entity.Property(e => e.ReferralReasonNew).HasComment("علت مراجعه -نوآموز جدید");

                entity.Property(e => e.RightHanded).HasComment("راست دست هست");

                entity.Property(e => e.SeveralChildren).HasComment("چندمین فرزند");

                entity.Property(e => e.StudyFieldId)
                    .HasColumnName("StudyFieldID")
                    .HasComment("رشته تحصیلی-تجربی");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(10)
                    .HasComment("تلفن");

                entity.Property(e => e.UserActive)
                    .HasDefaultValueSql("((0))")
                    .HasComment("0-غیر فعال1-فعال2-معلق");

                entity.Property(e => e.UserIdfather)
                    .HasColumnName("UserIDFather")
                    .HasComment("کلید خارجی برای مشخص شدن پدر");

                entity.Property(e => e.UserIdmother)
                    .HasColumnName("UserIDMother")
                    .HasComment("کلید خارجی برای مشخص شدن مادر");

                entity.Property(e => e.UserTypeId)
                    .HasColumnName("UserTypeID")
                    .HasComment("نوع کاربر-دانش آموز-پدر-مادر");

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.DegreeId)
                    .HasConstraintName("FK_User_Degree");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK_User_Grade");

                entity.HasOne(d => d.ParentStatus)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ParentStatusId)
                    .HasConstraintName("FK_User_ParentStatus");

                entity.HasOne(d => d.StudyField)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.StudyFieldId)
                    .HasConstraintName("FK_User_StudyField");

                entity.HasOne(d => d.UserIdfatherNavigation)
                    .WithMany(p => p.InverseUserIdfatherNavigation)
                    .HasForeignKey(d => d.UserIdfather)
                    .HasConstraintName("FK_User_User");

                entity.HasOne(d => d.UserIdmotherNavigation)
                    .WithMany(p => p.InverseUserIdmotherNavigation)
                    .HasForeignKey(d => d.UserIdmother)
                    .HasConstraintName("FK_User_User1");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserType");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_User");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasComment("نوع کاربر-دانش آموز-پدر-مادر");

                entity.Property(e => e.UserTypeId)
                    .HasColumnName("UserTypeID")
                    .HasComment("کلید اصلی")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserTypeTitle)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Week>(entity =>
            {
                entity.HasComment("جدول تعریف روزهای هفته");

                entity.Property(e => e.WeekId)
                    .HasColumnName("WeekID")
                    .HasComment("کلید اصلی")
                    .ValueGeneratedNever();

                entity.Property(e => e.WeekTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("عنوان روزهای هفته");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
