using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Es.DataLayerCore.Model;
using Object = Es.DataLayerCore.Model.Object;

namespace Es.DataLayerCore.Context
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
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<BannerType> BannerType { get; set; }
        public virtual DbSet<BrowserType> BrowserType { get; set; }
        public virtual DbSet<Calendar> Calendar { get; set; }
        public virtual DbSet<Ceremony> Ceremony { get; set; }
        public virtual DbSet<CeremonyType> CeremonyType { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Degree> Degree { get; set; }
        public virtual DbSet<DeviceType> DeviceType { get; set; }
        public virtual DbSet<Discipline> Discipline { get; set; }
        public virtual DbSet<DisciplineType> DisciplineType { get; set; }
        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<ExamLoginType> ExamLoginType { get; set; }
        public virtual DbSet<Financial> Financial { get; set; }
        public virtual DbSet<FinancialSendType> FinancialSendType { get; set; }
        public virtual DbSet<Gallery> Gallery { get; set; }
        public virtual DbSet<GalleryDetail> GalleryDetail { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<GroupObject> GroupObject { get; set; }
        public virtual DbSet<HomeWorkViewInfo> HomeWorkViewInfo { get; set; }
        public virtual DbSet<Homework> Homework { get; set; }
        public virtual DbSet<HomeworkAnswer> HomeworkAnswer { get; set; }
        public virtual DbSet<HomeworkAnswerFile> HomeworkAnswerFile { get; set; }
        public virtual DbSet<HomeworkAnswerStatus> HomeworkAnswerStatus { get; set; }
        public virtual DbSet<HomeworkType> HomeworkType { get; set; }
        public virtual DbSet<Introduction> Introduction { get; set; }
        public virtual DbSet<IntroductionType> IntroductionType { get; set; }
        public virtual DbSet<LogSms> LogSms { get; set; }
        public virtual DbSet<LogSmsResult> LogSmsResult { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<LoginPlatform> LoginPlatform { get; set; }
        public virtual DbSet<Object> Object { get; set; }
        public virtual DbSet<ParentStatus> ParentStatus { get; set; }
        public virtual DbSet<PreRegistration> PreRegistration { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionChoice> QuestionChoice { get; set; }
        public virtual DbSet<QuestionType> QuestionType { get; set; }
        public virtual DbSet<ReportCard> ReportCard { get; set; }
        public virtual DbSet<ReportCardDetail> ReportCardDetail { get; set; }
        public virtual DbSet<ReportCardPaper> ReportCardPaper { get; set; }
        public virtual DbSet<ReportCardTeacherCourse> ReportCardTeacherCourse { get; set; }
        public virtual DbSet<Response> Response { get; set; }
        public virtual DbSet<ResponseQuestion> ResponseQuestion { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleAccess> RoleAccess { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomChat> RoomChat { get; set; }
        public virtual DbSet<RoomChatGroup> RoomChatGroup { get; set; }
        public virtual DbSet<RoomChatMessageLast> RoomChatMessageLast { get; set; }
        public virtual DbSet<RoomChatView> RoomChatView { get; set; }
        public virtual DbSet<RoomChatViewCount> RoomChatViewCount { get; set; }
        public virtual DbSet<RoomChatViewInfo> RoomChatViewInfo { get; set; }
        public virtual DbSet<RoomDetail> RoomDetail { get; set; }
        public virtual DbSet<RoomTeacher> RoomTeacher { get; set; }
        public virtual DbSet<RoomTeacherExam> RoomTeacherExam { get; set; }
        public virtual DbSet<RoomType> RoomType { get; set; }
        public virtual DbSet<RoomUser> RoomUser { get; set; }
        public virtual DbSet<ScoreType> ScoreType { get; set; }
        public virtual DbSet<ScoreTypeDetail> ScoreTypeDetail { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<StudyField> StudyField { get; set; }
        public virtual DbSet<TeacherCourse> TeacherCourse { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserPlatform> UserPlatform { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Versioning> Versioning { get; set; }
        public virtual DbSet<View1> View1 { get; set; }
        public virtual DbSet<Week> Week { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sql2019;Database=ESchool;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddModelCreatingConfiguration(modelBuilder);
            modelBuilder.Entity<Accessories>(entity =>
            {
                entity.Property(e => e.AccessoriesId)
                    .HasComment("کلید اصلی جدول دسترسی کاربران")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccessoriesNameEn).HasComment("نام دسترسی انگلیسی");

                entity.Property(e => e.AccessoriesNameFa).HasComment("نام دسترسی");

                entity.Property(e => e.ObjectId).HasComment("کلید خارجی جدول فرم");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.Accessories)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accessories_Object");
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.HasOne(d => d.BannerType)
                    .WithMany(p => p.Banner)
                    .HasForeignKey(d => d.BannerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Banner_BannerType");

                entity.HasOne(d => d.RoomChat)
                    .WithMany(p => p.Banner)
                    .HasForeignKey(d => d.RoomChatId)
                    .HasConstraintName("FK_Banner_RoomChat");
            });

            modelBuilder.Entity<BrowserType>(entity =>
            {
                entity.Property(e => e.BrowserTypeId).HasComment("کلید اصلی جدول نوع مرورگر");

                entity.Property(e => e.BrowserTypeTitle).HasComment("عنوان نوع مرورگر");
            });

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.HasComment("جدول ثبت روزهای تعطیل");

                entity.Property(e => e.CalendarId).HasComment("کلید اصلی جدول");

                entity.Property(e => e.CalendarComment).HasComment("توضیحات");

                entity.Property(e => e.CalendarDate).HasComment("تاریخ تعطیلی");
            });

            modelBuilder.Entity<Ceremony>(entity =>
            {
                entity.HasOne(d => d.CeremonyType)
                    .WithMany(p => p.Ceremony)
                    .HasForeignKey(d => d.CeremonyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ceremony_CeremonyType");

                entity.HasOne(d => d.RoomChat)
                    .WithMany(p => p.Ceremony)
                    .HasForeignKey(d => d.RoomChatId)
                    .HasConstraintName("FK_Ceremony_RoomChat");
            });

            modelBuilder.Entity<CeremonyType>(entity =>
            {
                entity.Property(e => e.CeremonyTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasComment("تعریف درس ها");

                entity.Property(e => e.CourseId).HasComment("کلید اصلی");

                entity.Property(e => e.CourseTitle).HasComment("عنوان درس");
            });

            modelBuilder.Entity<Degree>(entity =>
            {
                entity.HasComment("مقطع تحصیلی-متوسطه اول");

                entity.Property(e => e.DegreeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<DeviceType>(entity =>
            {
                entity.Property(e => e.DeviceTypeId).HasComment("کلید اصلی نوع سخت افزار");

                entity.Property(e => e.DeviceTypeTitle).HasComment("عنوان نوع درایور");
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.HasOne(d => d.DisciplineType)
                    .WithMany(p => p.Discipline)
                    .HasForeignKey(d => d.DisciplineTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Discipline_DisciplineType");

                entity.HasOne(d => d.RoomChat)
                    .WithMany(p => p.DisciplineRoomChat)
                    .HasForeignKey(d => d.RoomChatId)
                    .HasConstraintName("FK_Discipline_RoomChat");

                entity.HasOne(d => d.RoomChatId2Navigation)
                    .WithMany(p => p.DisciplineRoomChatId2Navigation)
                    .HasForeignKey(d => d.RoomChatId2)
                    .HasConstraintName("FK_Discipline_RoomChat1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Discipline)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Discipline_User");
            });

            modelBuilder.Entity<DisciplineType>(entity =>
            {
                entity.Property(e => e.DisciplineTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasComment("آزمون ها");

                entity.Property(e => e.CompanyId).HasComment("کلید خارجی شرکت یا مدرسه");

                entity.Property(e => e.DelayDeadline).HasComment("زمان تاخیر در آزمون");

                entity.Property(e => e.ExamCancel).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExamFinishDateTime).HasComment("تاریخ پایان آزمون");

                entity.Property(e => e.ExamLoginTypeId).HasComment("طریقه ورود به آزمون");

                entity.Property(e => e.ExamOn)
                    .HasDefaultValueSql("((0))")
                    .HasComment("آزمون روشن باشد یا خاموش");

                entity.Property(e => e.ExamPic).HasComment("عکس برای آزمون");

                entity.Property(e => e.ExamStartDateTime).HasComment("تاریخ شروع آزمون");

                entity.Property(e => e.ExamTime).HasComment("زمان آزمون");

                entity.Property(e => e.ExamTitle).HasComment("عنوان");

                entity.Property(e => e.OpeningNumber).HasComment("تعداد باز شدن آزمون");

                entity.Property(e => e.RandomNumber).HasComment("تعداد سوالات رندوم برای نمایش");

                entity.Property(e => e.RoomChatGroupId).HasComment("کلید خارجی به گروهها و کلاس های درس");

                entity.Property(e => e.UserId).HasComment("کاربر ایجاد کننده");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Exam)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_Company");

                entity.HasOne(d => d.ExamLoginType)
                    .WithMany(p => p.Exam)
                    .HasForeignKey(d => d.ExamLoginTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_ExamLoginType");

                entity.HasOne(d => d.RoomChatGroup)
                    .WithMany(p => p.Exam)
                    .HasForeignKey(d => d.RoomChatGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_RoomChatGroup");

                entity.HasOne(d => d.ScoreType)
                    .WithMany(p => p.Exam)
                    .HasForeignKey(d => d.ScoreTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_ScoreType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Exam)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_User");
            });

            modelBuilder.Entity<ExamLoginType>(entity =>
            {
                entity.HasComment("جدول نوع ورود به آزمون");

                entity.Property(e => e.ExamLoginTypeTitle).HasComment("نوع ورود به آزمون");
            });

            modelBuilder.Entity<Financial>(entity =>
            {
                entity.HasComment("جدول ثبت هشدارهای مالی");

                entity.Property(e => e.FinancialDate).HasComment("تاریخ");

                entity.Property(e => e.FinancialSendTypeId).HasComment("1.دانش آموز 2.والدین 3. هردو");

                entity.Property(e => e.FinancialText).HasComment("متن هشدار");

                entity.HasOne(d => d.FinancialSendType)
                    .WithMany(p => p.Financial)
                    .HasForeignKey(d => d.FinancialSendTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Financial_FinancialSendType");

                entity.HasOne(d => d.RoomChat)
                    .WithMany(p => p.FinancialRoomChat)
                    .HasForeignKey(d => d.RoomChatId)
                    .HasConstraintName("FK_Financial_RoomChat");

                entity.HasOne(d => d.RoomChatId2Navigation)
                    .WithMany(p => p.FinancialRoomChatId2Navigation)
                    .HasForeignKey(d => d.RoomChatId2)
                    .HasConstraintName("FK_Financial_RoomChat1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Financial)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Financial_User");
            });

            modelBuilder.Entity<FinancialSendType>(entity =>
            {
                entity.Property(e => e.FinancialSendTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<GalleryDetail>(entity =>
            {
                entity.Property(e => e.GalleryDetailType)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1-pic,2-film");

                entity.HasOne(d => d.Gallery)
                    .WithMany(p => p.GalleryDetail)
                    .HasForeignKey(d => d.GalleryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GalleryDetail_Gallery");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasComment("پایه تحصیلی-نهم");

                entity.Property(e => e.GradeId).ValueGeneratedNever();

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.Grade)
                    .HasForeignKey(d => d.DegreeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_Degree");
            });

            modelBuilder.Entity<GroupObject>(entity =>
            {
                entity.Property(e => e.GroupObjectId).HasComment(" کلید اصلی جدول گروه فرم");

                entity.Property(e => e.GroupObjectName).HasComment("نام گروه فرم");

                entity.Property(e => e.GroupObjectOrder).HasComment("ترتیب گروه فرم ها");
            });

            modelBuilder.Entity<HomeWorkViewInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HomeWorkViewInfo");
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasComment("جدول تعریف تکالیف");

                entity.Property(e => e.HomeworkId).HasComment("کلید اصلی");

                entity.Property(e => e.CourseId).HasComment("درس");

                entity.Property(e => e.HomeworkDescription).HasComment("توضیحات در مورد تکلیف");

                entity.Property(e => e.HomeworkFinishDate).HasComment("تاریخ و ساعت پایان ارسال تکالیف");

                entity.Property(e => e.HomeworkStartDate).HasComment("تاریخ و ساعت شروع ارسال تکالیف");

                entity.Property(e => e.HomeworkTile).HasComment("عنوان تکلیف");

                entity.Property(e => e.HomeworkTypeId).HasComment("نوع تکالیف");

                entity.Property(e => e.RoomId).HasComment("کلاس");

                entity.Property(e => e.ScoreTypeId).HasComment("نوع نمره دهی");

                entity.Property(e => e.UserId).HasComment("معلم");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Homework_Course");

                entity.HasOne(d => d.HomeworkType)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.HomeworkTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Homework_HomeworkType");

                entity.HasOne(d => d.RoomChatGroup)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.RoomChatGroupId)
                    .HasConstraintName("FK_Homework_RoomChatGroup");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Homework_Room");

                entity.HasOne(d => d.ScoreType)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.ScoreTypeId)
                    .HasConstraintName("FK_Homework_ScoreType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Homework_User");
            });

            modelBuilder.Entity<HomeworkAnswer>(entity =>
            {
                entity.HasComment("پاسخ تکالیف");

                entity.Property(e => e.HomeworkAnswerId).HasComment("کلید اصلی");

                entity.Property(e => e.EditDateTime).HasComment("تاریخ ویرایش");

                entity.Property(e => e.HomeworkAnswerComment).HasComment("توضیحات ارسال");

                entity.Property(e => e.HomeworkAnswerDescriptiveId).HasComment("نمره توصیفی");

                entity.Property(e => e.HomeworkAnswerFinishDate).HasComment("تاریخ پایان ارسال");

                entity.Property(e => e.HomeworkAnswerSartDate).HasComment("تاریخ شروع ارسال");

                entity.Property(e => e.HomeworkAnswerScore).HasComment("نمره معلم");

                entity.Property(e => e.HomeworkAnswerStatusId).HasComment("وضعیت");

                entity.Property(e => e.HomeworkId).HasComment("کلید حارجی جدول تکالیف");

                entity.Property(e => e.HomeworkResponse).HasComment("جواب متنی تکلیف");

                entity.Property(e => e.SendDatetime).HasComment("تاریخ ارسال");

                entity.Property(e => e.TeacherComment).HasComment("توضیحات معلم");

                entity.Property(e => e.UserId).HasComment("کاربر-دانش اموز");

                entity.Property(e => e.VisitDateTime).HasComment("تاریخ مشاهده");

                entity.HasOne(d => d.HomeworkAnswerDescriptive)
                    .WithMany(p => p.HomeworkAnswer)
                    .HasForeignKey(d => d.HomeworkAnswerDescriptiveId)
                    .HasConstraintName("FK_HomeworkAnswer_ScoreTypeDetail");

                entity.HasOne(d => d.HomeworkAnswerStatus)
                    .WithMany(p => p.HomeworkAnswer)
                    .HasForeignKey(d => d.HomeworkAnswerStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HomeworkAnswer_HomeworkAnswerStatus");

                entity.HasOne(d => d.Homework)
                    .WithMany(p => p.HomeworkAnswer)
                    .HasForeignKey(d => d.HomeworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HomeworkAnswer_Homework");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HomeworkAnswer)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HomeworkAnswer_User");
            });

            modelBuilder.Entity<HomeworkAnswerFile>(entity =>
            {
                entity.HasOne(d => d.HomeworkAnswer)
                    .WithMany(p => p.HomeworkAnswerFile)
                    .HasForeignKey(d => d.HomeworkAnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HomeworkAnswerFile_HomeworkAnswer");
            });

            modelBuilder.Entity<HomeworkAnswerStatus>(entity =>
            {
                entity.HasComment("وضعیت ارسال تکالیف");

                entity.Property(e => e.HomeworkAnswerStatusId)
                    .HasComment("کلید اصلی")
                    .ValueGeneratedNever();

                entity.Property(e => e.HomeworkAnswerStatusTitle).HasComment("وضعیت");
            });

            modelBuilder.Entity<HomeworkType>(entity =>
            {
                entity.HasComment("نوع کالیف");

                entity.Property(e => e.HomeworkTypeId)
                    .HasComment("کلید اصلی")
                    .ValueGeneratedNever();

                entity.Property(e => e.HomeworkTypeTitle).HasComment("نوع تکلیف");
            });

            modelBuilder.Entity<Introduction>(entity =>
            {
                entity.HasOne(d => d.IntroductionType)
                    .WithMany(p => p.Introduction)
                    .HasForeignKey(d => d.IntroductionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Introduction_IntroductionType");
            });

            modelBuilder.Entity<LogSms>(entity =>
            {
                entity.Property(e => e.Explain).IsFixedLength();

                entity.Property(e => e.SendNumber).IsUnicode(false);

                entity.HasOne(d => d.LogSmsResult)
                    .WithMany(p => p.LogSms)
                    .HasForeignKey(d => d.LogSmsResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LogSms_LogSmsResult");
            });

            modelBuilder.Entity<LogSmsResult>(entity =>
            {
                entity.Property(e => e.LogSmsResultId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.LoginId).HasComment("کلید اصلی جدول ورود به سامانه");

                entity.Property(e => e.AppVersion).HasComment("ورژن نسخه طراحی شده ما وب-اندروید-iOS");

                entity.Property(e => e.BrowserName).HasComment("کلید خارجی به جدول نوع مرورگر");

                entity.Property(e => e.BrowserVersion).HasComment("ورژن مرورگر-اندروید-iOS");

                entity.Property(e => e.ComputerName).HasComment("نام کامپیوتر");

                entity.Property(e => e.DateExit).HasComment("زمان خروج");

                entity.Property(e => e.DateLogin).HasComment("زمان ورود");

                entity.Property(e => e.Description).HasComment("توضیحات");

                entity.Property(e => e.DeviceName).HasComment("کلید خارجی نوع سخت افزار پاسخ دهی");

                entity.Property(e => e.DomainName).HasComment("نام دامنه");

                entity.Property(e => e.Ips).HasComment("IPهای ورود");

                entity.Property(e => e.Ipv6).HasComment("IP ورژن 6");

                entity.Property(e => e.LoginPlatformId).HasComment("کلید خاریجی نوع platform");

                entity.Property(e => e.LoginSuccess).HasComment("آیا ورود با موفقیت بوده؟");

                entity.Property(e => e.MobileName).HasComment("نام موبایل");

                entity.Property(e => e.SesseionEndAuto).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasComment("کلید خارجی به جدول کاربر");

                entity.HasOne(d => d.LoginPlatform)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.LoginPlatformId)
                    .HasConstraintName("FK_Login_LoginPlatform");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK_Login_Session");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Login_User");
            });

            modelBuilder.Entity<LoginPlatform>(entity =>
            {
                entity.Property(e => e.LoginPlatformId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Object>(entity =>
            {
                entity.Property(e => e.ObjectId).HasComment("کلید اصلی جدول اشیا ");

                entity.Property(e => e.GroupObjectId).HasComment("کلید خارجی جدول گروه اشیا");

                entity.Property(e => e.MyObjectOrder).HasComment(" ترتیب اشیا");

                entity.Property(e => e.ObjectNameEn).HasComment("نام اشیا");

                entity.Property(e => e.ObjectNameFa).HasComment("نام فارسی اشیا ");

                entity.HasOne(d => d.GroupObject)
                    .WithMany(p => p.Object)
                    .HasForeignKey(d => d.GroupObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Object_GroupObject");
            });

            modelBuilder.Entity<ParentStatus>(entity =>
            {
                entity.HasComment("وضعیت والدین-در قید حیات-شهید-جانباز-فوت");
            });

            modelBuilder.Entity<PreRegistration>(entity =>
            {
                entity.Property(e => e.ClassRoom).HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasComment("جدول سوالات");

                entity.Property(e => e.QuestionId).HasComment("شماره سوال");

                entity.Property(e => e.IsRandomQuestion).HasComment("سوال تصادفی هست یا نه؟");

                entity.Property(e => e.QuestionScore).HasComment("نمره هر سوال");

                entity.Property(e => e.QuestionTitle).HasComment("عنوان سوال");

                entity.Property(e => e.QuestionTypeId).HasComment("کلید خارجی نوع سوال");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_Exam");

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_QuestionType");
            });

            modelBuilder.Entity<QuestionChoice>(entity =>
            {
                entity.Property(e => e.IsAnswer).HasComment("جواب درست گزینه");

                entity.Property(e => e.QuestionChoiceFile).HasComment("فایل یا عکس برای سوال");

                entity.Property(e => e.QuestionChoiceTitle).HasComment("عنوان گزینه ها");

                entity.Property(e => e.QuestionId).HasComment("کلید خارجی جدول Question");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionChoice)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionChoice_Question");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.Property(e => e.QuestionTypeId).ValueGeneratedNever();

                entity.Property(e => e.QuestionTypeTitle).HasComment("عنوان نوع سوال");
            });

            modelBuilder.Entity<ReportCard>(entity =>
            {
                entity.HasComment("کارنامه");

                entity.Property(e => e.ReportCardAuto).HasComment("در صورتی که انتخاب درس خودکار باشد بدون انتخاب معلم و درس");

                entity.Property(e => e.ReportCardDateTimeFinish).HasComment("تاریخ پایان");

                entity.Property(e => e.ReportCardDateTimeStart).HasComment("تاریخ شروع");

                entity.Property(e => e.ReportCardScoreEnable).HasComment("فعال کردن نمره");

                entity.Property(e => e.ReportCardShowUser).HasComment("نمایش کارنامه");

                entity.Property(e => e.ReportCardTitle).HasComment("عنوان کارنامه");

                entity.Property(e => e.ScoreTypeId).HasComment("نوع نمره دهی");

                entity.HasOne(d => d.ScoreType)
                    .WithMany(p => p.ReportCard)
                    .HasForeignKey(d => d.ScoreTypeId)
                    .HasConstraintName("FK_ReportCard_Grade");
            });

            modelBuilder.Entity<ReportCardDetail>(entity =>
            {
                entity.Property(e => e.CourseId).HasComment("درس");

                entity.Property(e => e.ReportCardDescriptiveId).HasComment("نمره توصیفی");

                entity.Property(e => e.ReportCardScore).HasComment("نمره معلم");

                entity.Property(e => e.ReportCardTeacherComment).HasComment("توضیحات معلم");

                entity.Property(e => e.TeacherId).HasComment("معلمی که نمره داده");

                entity.Property(e => e.UserId).HasComment("دانش آموز");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.ReportCardDetail)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCardDetail_Course");

                entity.HasOne(d => d.ReportCardDescriptive)
                    .WithMany(p => p.ReportCardDetail)
                    .HasForeignKey(d => d.ReportCardDescriptiveId)
                    .HasConstraintName("FK_ReportCardDetail_ScoreTypeDetail");

                entity.HasOne(d => d.ReportCard)
                    .WithMany(p => p.ReportCardDetail)
                    .HasForeignKey(d => d.ReportCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCardDetail_ReportCard");

                entity.HasOne(d => d.RoomChatGroup)
                    .WithMany(p => p.ReportCardDetail)
                    .HasForeignKey(d => d.RoomChatGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCardDetail_RoomChatGroup");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.ReportCardDetailTeacher)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCardDetail_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReportCardDetailUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCardDetail_User");
            });

            modelBuilder.Entity<ReportCardPaper>(entity =>
            {
                entity.HasOne(d => d.ReportCard)
                    .WithMany(p => p.ReportCardPaper)
                    .HasForeignKey(d => d.ReportCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCardPaper_ReportCard");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReportCardPaper)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCardPaper_User");
            });

            modelBuilder.Entity<ReportCardTeacherCourse>(entity =>
            {
                entity.HasComment("درس های معلم");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.ReportCardTeacherCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCardTeacherCourse_Course");

                entity.HasOne(d => d.ReportCard)
                    .WithMany(p => p.ReportCardTeacherCourse)
                    .HasForeignKey(d => d.ReportCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCardTeacherCourse_ReportCard");

                entity.HasOne(d => d.RoomChatGroup)
                    .WithMany(p => p.ReportCardTeacherCourse)
                    .HasForeignKey(d => d.RoomChatGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportCardTeacherCourse_RoomChatGroup");
            });

            modelBuilder.Entity<Response>(entity =>
            {
                entity.Property(e => e.OpeningCount).HasComment("تعداد باز شدن آزمون");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Response)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Response_Exam");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Response)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Response_User");
            });

            modelBuilder.Entity<ResponseQuestion>(entity =>
            {
                entity.Property(e => e.QuestionTeacherComment).HasComment("توضیحات معلم برای پاسخ دانش آموز");

                entity.Property(e => e.ResponseQuestionDescriptiveId).HasComment("نمره طیفی");

                entity.Property(e => e.ResponseQuestionScore).HasComment("نمره عددی");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ResponseQuestion)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResponseQuestion_Question");

                entity.HasOne(d => d.Response)
                    .WithMany(p => p.ResponseQuestion)
                    .HasForeignKey(d => d.ResponseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResponseQuestion_Response");

                entity.HasOne(d => d.ResponseQuestionDescriptive)
                    .WithMany(p => p.ResponseQuestion)
                    .HasForeignKey(d => d.ResponseQuestionDescriptiveId)
                    .HasConstraintName("FK_ResponseQuestion_ScoreTypeDetail");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasComment("کلید اصلی جدول نقش");

                entity.Property(e => e.RoleName).HasComment("نام نقش ");
            });

            modelBuilder.Entity<RoleAccess>(entity =>
            {
                entity.Property(e => e.RoleAccessId).HasComment("کلید اصلی جدول دسترسی نقش");

                entity.Property(e => e.AccessoriesId).HasComment("کلید خارجی جدول دسترسی ها");

                entity.Property(e => e.RoleId).HasComment("کلید خارجی جدول نقش");

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

                entity.HasIndex(e => e.DegreeId);

                entity.HasIndex(e => e.GradeId);

                entity.HasIndex(e => e.PinRoomChatId);

                entity.HasIndex(e => e.RoomTypeId);

                entity.HasIndex(e => e.StudyFieldId);

                entity.Property(e => e.RoomId).HasComment("کلید اصلی");

                entity.Property(e => e.CloseOnTime)
                    .HasDefaultValueSql("((1))")
                    .HasComment("کلاس سرساعت شروع و قطع شود");

                entity.Property(e => e.DegreeId).HasComment("مقطع تحصیلی-متوسطه اول");

                entity.Property(e => e.GradeId).HasComment("پایه تحصیلی-نهم");

                entity.Property(e => e.RoomFinishDate).HasComment("تاریخ پایان کلاس ها");

                entity.Property(e => e.RoomStartDate).HasComment("تاریخ شروع کلاس ها");

                entity.Property(e => e.RoomTitle).HasComment("عنوان");

                entity.Property(e => e.RoomTypeId).HasComment("کلید خارجی نوع اتاق درس");

                entity.Property(e => e.StudyFieldId).HasComment("رشته تحصیلی-تجربی");

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
                entity.HasIndex(e => e.CourseId);

                entity.HasIndex(e => e.RecieverId);

                entity.HasIndex(e => e.RoomChatGroupId);

                entity.HasIndex(e => e.RoomChatParentId);

                entity.HasIndex(e => e.RoomId);

                entity.HasIndex(e => e.SenderId);

                entity.HasIndex(e => e.TeacherId);

                entity.Property(e => e.MimeType).IsUnicode(false);

                entity.Property(e => e.RoomChatDelete).HasComment("حذف پیام");

                entity.Property(e => e.RoomChatParentId).HasComment("برای Reply استقاده شود");

                entity.Property(e => e.RoomChatUpdate).HasComment("تاریخ و ویرایش زمان چت");

                entity.Property(e => e.RoomChatViewNumber).HasComment("تعداد بازدید");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.RoomChat)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_RoomChat_Course");

                entity.HasOne(d => d.Reciever)
                    .WithMany(p => p.RoomChatReciever)
                    .HasForeignKey(d => d.RecieverId)
                    .HasConstraintName("FK_RoomChat_User1");

                entity.HasOne(d => d.RoomChatGroup)
                    .WithMany(p => p.RoomChat)
                    .HasForeignKey(d => d.RoomChatGroupId)
                    .HasConstraintName("FK_RoomChat_RoomChatGroup");

                entity.HasOne(d => d.RoomChatParent)
                    .WithMany(p => p.InverseRoomChatParent)
                    .HasForeignKey(d => d.RoomChatParentId)
                    .HasConstraintName("FK_RoomChat_RoomChat");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomChat)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_RoomChat_Room");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.RoomChatSender)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomChat_User");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.RoomChatTeacher)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_RoomChat_User2");
            });

            modelBuilder.Entity<RoomChatGroup>(entity =>
            {
                entity.HasComment("گروه برای چت");

                entity.HasIndex(e => e.CourseId);

                entity.HasIndex(e => e.RoomId);

                entity.HasIndex(e => e.StudentId);

                entity.HasIndex(e => e.TeacherId);

                entity.Property(e => e.CourseId).HasComment("درس");

                entity.Property(e => e.RoomChatGroupCreateDateTime).HasComment("تاریخ ایجاد گروه");

                entity.Property(e => e.RoomChatGroupTitle).HasComment("عنوان گروه");

                entity.Property(e => e.RoomChatGroupType)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1-گروه 2-خصوصی3-مالی");

                entity.Property(e => e.RoomId).HasComment("کلاس");

                entity.Property(e => e.TeacherId).HasComment("معلم");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.RoomChatGroup)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_RoomChatGroup_Course");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomChatGroup)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_RoomChatGroup_Room");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RoomChatGroupStudent)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_RoomChatGroup_User1");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.RoomChatGroupTeacher)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_RoomChatGroup_User");
            });

            modelBuilder.Entity<RoomChatMessageLast>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RoomChatMessageLast");

                entity.Property(e => e.Mimetype).IsUnicode(false);
            });

            modelBuilder.Entity<RoomChatView>(entity =>
            {
                entity.HasComment("جدول مشاهده چت ها");

                entity.HasIndex(e => e.RoomChatGroupId)
                    .HasName("IX_RoomChatView");

                entity.HasIndex(e => e.RoomChatId);

                entity.HasIndex(e => e.RoomChatViewId)
                    .HasName("IX_RoomChatView_UserID");

                entity.Property(e => e.RoomChatViewId).HasComment("کلید اصلی");

                entity.Property(e => e.RoomChatGroupId).HasDefaultValueSql("((1))");

                entity.Property(e => e.RoomChatId).HasComment("کلاس");

                entity.Property(e => e.RoomChatViewDelete).HasComment("حذف چت");

                entity.Property(e => e.UserId).HasComment("کاربر");

                entity.Property(e => e.ViewDateTime).HasComment("تاریخ مشاهده");

                entity.HasOne(d => d.RoomChatGroup)
                    .WithMany(p => p.RoomChatView)
                    .HasForeignKey(d => d.RoomChatGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomChatView_RoomChatGroup");

                entity.HasOne(d => d.RoomChat)
                    .WithMany(p => p.RoomChatView)
                    .HasForeignKey(d => d.RoomChatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomChatView_RoomChat");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoomChatView)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomChatView_User");
            });

            modelBuilder.Entity<RoomChatViewCount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RoomChatViewCount");
            });

            modelBuilder.Entity<RoomChatViewInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RoomChatViewInfo");
            });

            modelBuilder.Entity<RoomDetail>(entity =>
            {
                entity.HasComment("جزئیات روزها و ساعت های ");

                entity.Property(e => e.RoomDetailId).HasComment("کلید اصلی");

                entity.Property(e => e.CloseOnTime)
                    .HasDefaultValueSql("((1))")
                    .HasComment("کلاس سرساعت شروع و قطع شود");

                entity.Property(e => e.Comment).HasComment("توضیحات کلاس وطرح درس");

                entity.Property(e => e.CourseId).HasComment("درس");

                entity.Property(e => e.RoomDetailDate).HasComment("تاریخ کلاس");

                entity.Property(e => e.RoomDetailTimeFinish).HasComment("ساعت پایان کلاس");

                entity.Property(e => e.RoomDetailTimeStart).HasComment("ساعت شروع کلاس");

                entity.Property(e => e.RoomId).HasComment("کلید خارجی به جدول اتاق ها");

                entity.Property(e => e.UserId).HasComment("معلم کلاس");

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

                entity.Property(e => e.AdobeLiveAddress).HasComment("آدرس لایو Adobe");

                entity.Property(e => e.AdobeLivePass).HasComment("پسورد لایو Adobe");

                entity.Property(e => e.AdobeLiveUsername).HasComment("یوزر لایو Adobe");

                entity.Property(e => e.CourseId).HasComment("درس");

                entity.Property(e => e.JitsiLiveAddress).HasComment("آدرس لایو jitsi");

                entity.Property(e => e.JitsiLivePassword).HasComment("پسورد لایو jitsi");

                entity.Property(e => e.RoomId).HasComment("کلاس");

                entity.Property(e => e.UserId).HasComment("استاد");

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

            modelBuilder.Entity<RoomTeacherExam>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RoomTeacherExam");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.HasComment("نوع اتاق درس");

                entity.Property(e => e.RoomTypeId).HasComment("کلید اصلی");

                entity.Property(e => e.RoomTypeTitle).HasComment("عنوان");
            });

            modelBuilder.Entity<RoomUser>(entity =>
            {
                entity.HasComment("جدول دانش اموزان و افراد داخل کلاس");

                entity.HasIndex(e => e.RoomId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.RoomUserId).HasComment("کلید اصلی");

                entity.Property(e => e.RoomId).HasComment("کلاس ها");

                entity.Property(e => e.UserId).HasComment("دانش اموزان");

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

            modelBuilder.Entity<ScoreType>(entity =>
            {
                entity.HasComment("انواع نمرات");

                entity.Property(e => e.ScoreTypeId).HasComment("کلید اصلی");

                entity.Property(e => e.IsNumber).HasComment("عددی هست یا نه");

                entity.Property(e => e.ScoreTypeTitle).HasComment("عنوان");
            });

            modelBuilder.Entity<ScoreTypeDetail>(entity =>
            {
                entity.HasComment("جدول تعریف بازه ارزشی نمره توصیفی");

                entity.Property(e => e.ScoreTypeDetailId).HasComment("کلید اصلی");

                entity.Property(e => e.ScoreFinish).HasComment("نمره پایان");

                entity.Property(e => e.ScoreStart).HasComment("نمره شروع");

                entity.Property(e => e.ScoreTypeDetailTitle).HasComment("عنوان عبارت توصیفی");

                entity.HasOne(d => d.ScoreType)
                    .WithMany(p => p.ScoreTypeDetail)
                    .HasForeignKey(d => d.ScoreTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScoreTypeDetail_ScoreType");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.Property(e => e.BrowserName).HasComment("کلید خارجی به جدول نوع مرورگر");

                entity.Property(e => e.BrowserVersion).HasComment("ورژن مرورگر-اندروید-iOS");

                entity.Property(e => e.DeviceName).HasComment("کلید خارجی نوع سخت افزار پاسخ دهی");

                entity.Property(e => e.Ips).HasComment("IPهای ورود");

                entity.Property(e => e.LoginPlatformId).HasComment("کلید خاریجی نوع platform");

                entity.Property(e => e.SesseionEndAuto).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<StudyField>(entity =>
            {
                entity.HasComment("رشته تحصیلی-تجربی");

                entity.Property(e => e.StudyFieldId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TeacherCourse>(entity =>
            {
                entity.HasComment("انتخاب درس های معلم");

                entity.Property(e => e.TeacherCourseId).HasComment("کلید اصلی");

                entity.Property(e => e.CourseId).HasComment("انتخاب درس های معلم");

                entity.Property(e => e.DegreeId).HasComment("مقطع تحصیلی-متوسطه اول");

                entity.Property(e => e.GradeId).HasComment("پایه تحصیلی-نهم");

                entity.Property(e => e.StudyFieldId).HasComment("رشته تحصیلی-تجربی");

                entity.Property(e => e.UserId).HasComment("انتخاب کاربر معلم");

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

                entity.HasIndex(e => e.DegreeId);

                entity.HasIndex(e => e.GradeId);

                entity.HasIndex(e => e.Irnational);

                entity.HasIndex(e => e.StudyFieldId);

                entity.HasIndex(e => e.UserTypeId);

                entity.Property(e => e.UserId).HasComment("کلید اصلی");

                entity.Property(e => e.Address).HasComment("آدرس");

                entity.Property(e => e.BirthDate).HasComment("تاریخ تولد");

                entity.Property(e => e.BirthPlace).HasComment("محل تولد");

                entity.Property(e => e.DegreeId).HasComment("مقطع تحصیلی-متوسطه اول");

                entity.Property(e => e.FamilyNumber).HasComment("تعداد اعضای خانواده");

                entity.Property(e => e.FirstName).HasComment("نام");

                entity.Property(e => e.GradeId).HasComment("پایه تحصیلی-نهم");

                entity.Property(e => e.IdcardPlace).HasComment("محل صدور شناسنامه");

                entity.Property(e => e.IdcardSerial).HasComment("شماره سریال شناسنامه");

                entity.Property(e => e.IdcardSeriesLetter).HasComment("حرف سری شناسنامه");

                entity.Property(e => e.IdcardSeriesNumber).HasComment("شماره سری شناسنامه");

                entity.Property(e => e.Insurance).HasComment("بیمه پایه");

                entity.Property(e => e.Irnational).HasComment("کدملی");

                entity.Property(e => e.IsCityPlace).HasComment("محل سکونت شهر است");

                entity.Property(e => e.IsRelativesParents).HasComment("نسبت خویشاوندی بین والدین وجود دارد؟");

                entity.Property(e => e.IsStudentInsurance).HasComment("بیمه تحصیلی دارد");

                entity.Property(e => e.LastName).HasComment("نام خانوادگی");

                entity.Property(e => e.LivePlace).HasComment("با چه کسی زندگی میکنید");

                entity.Property(e => e.LivePlaceOther).HasComment("با چه کسی زندگی میکنید");

                entity.Property(e => e.MobileNumber).HasComment("شماره موبایل");

                entity.Property(e => e.Nationality).HasComment("ملیت");

                entity.Property(e => e.ParentDegree).HasComment("مدرک تحصیلی پدر مادر");

                entity.Property(e => e.ParentJob).HasComment("شغل و محل کار");

                entity.Property(e => e.ParentStatusId).HasComment("وضعیت والدین-در قید حیات-شهید-جانباز-فوت");

                entity.Property(e => e.Password).HasComment("رمز عبور");

                entity.Property(e => e.PasswordToken).IsUnicode(false);

                entity.Property(e => e.PersianLanguage).HasComment("زبان فارسی هست");

                entity.Property(e => e.PreschoolEducation).HasComment("آموزش قبل از دبستان دارد");

                entity.Property(e => e.ReasonInactive).HasComment("علت غیر فعال بودن");

                entity.Property(e => e.ReferralReasonNew).HasComment("علت مراجعه -نوآموز جدید");

                entity.Property(e => e.RightHanded).HasComment("راست دست هست");

                entity.Property(e => e.SeveralChildren).HasComment("چندمین فرزند");

                entity.Property(e => e.StudyFieldId).HasComment("رشته تحصیلی-تجربی");

                entity.Property(e => e.Telephone).HasComment("تلفن");

                entity.Property(e => e.UserActive)
                    .HasDefaultValueSql("((0))")
                    .HasComment("0-غیر فعال1-فعال2-معلق");

                entity.Property(e => e.UserIdfather).HasComment("کلید خارجی برای مشخص شدن پدر");

                entity.Property(e => e.UserIdmother).HasComment("کلید خارجی برای مشخص شدن مادر");

                entity.Property(e => e.UserTypeId).HasComment("نوع کاربر-دانش آموز-پدر-مادر");

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

            modelBuilder.Entity<UserPlatform>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPlatform)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPlatform_User");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
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
                    .HasComment("کلید اصلی")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<View1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_1");
            });

            modelBuilder.Entity<Week>(entity =>
            {
                entity.HasComment("جدول تعریف روزهای هفته");

                entity.Property(e => e.WeekId)
                    .HasComment("کلید اصلی")
                    .ValueGeneratedNever();

                entity.Property(e => e.WeekTitle).HasComment("عنوان روزهای هفته");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
