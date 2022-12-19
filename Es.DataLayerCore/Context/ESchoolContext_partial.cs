using Es.DataLayerCore.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Es.DataLayerCore.DTOs.RoomChat;
using Es.DataLayerCore.DTOs.User;
using Es.DataLayerCore.DTOs.Exam;
using Es.DataLayerCore.DTOs.Gallery;
using Es.DataLayerCore.DTOs.Statist;
using Es.DataLayerCore.DTOs.Article;
using Es.DataLayerCore.DTOs.Introduction;
using Es.DataLayerCore.DTOs.ReportCard;
using Es.DataLayerCore.DTOs.Finacial;
using Es.DataLayerCore.DTOs.Discipline;
using Es.DataLayerCore.DTOs.Ceremony;

namespace Es.DataLayerCore.Context
{
    //Add This in OnModel Creating  AddModelCreatingConfiguration(modelBuilder);
    public partial class ESchoolContext
    {
        private DbSet<ArticleShowTopResult> ArticleShowTop { get; set; }
        private DbSet<CeremonyShowResult> CeremonyShow { get; set; }
        private DbSet<DisciplineShowResult> DisciplineShow { get; set; }
        private DbSet<ExamInfoResult> ExamInfo { get; set; }
        private DbSet<ExamResponseResult> ExamResponse { get; set; }
        private DbSet<ExamStudentShowResult> ExamStudentShow { get; set; }
        private DbSet<FinancialShowResult> FinancialShow { get; set; }
        private DbSet<GalleryDetailShowResult> GalleryDetailShow { get; set; }
        private DbSet<GalleryShowResult> GalleryShow { get; set; }
        private DbSet<HomeworkAnswerStudentShowResult> HomeworkAnswerStudentShow { get; set; }
        private DbSet<HomeworkByIDResult> HomeworkByID { get; set; }
        private DbSet<HomeworkDetailShowResult> HomeworkDetailShow { get; set; }
        private DbSet<HomeworkDetailsShowByIDResult> HomeworkDetailsShowByID { get; set; }
        private DbSet<HomeworkShowResult> HomeworkShow { get; set; }
        private DbSet<IntroductionGroupShowResult> IntroductionGroupShow { get; set; }
        private DbSet<IntroductionShowAllResult> IntroductionShowAll { get; set; }
        private DbSet<LoginUserResult> LoginUser { get; set; }
        private DbSet<ReportCardPaperShowResult> ReportCardPaperShow { get; set; }
        private DbSet<ReportCardParentStudentShowResult> ReportCardParentStudentShow { get; set; }
        private DbSet<ReportCardShowAllResult> ReportCardShowAll { get; set; }
        private DbSet<ReportCardStudentDetailShowResult> ReportCardStudentDetailShow { get; set; }
        private DbSet<ReportCardStudentShowResult> ReportCardStudentShow { get; set; }
        private DbSet<ReportCardTeacherCourseByRoomChatGroupIDResult> ReportCardTeacherCourseByRoomChatGroupID { get; set; }
        private DbSet<ReportCardTeacherCourseShowResult> ReportCardTeacherCourseShow { get; set; }
        private DbSet<ReportCardTeacherScoreShowResult> ReportCardTeacherScoreShow { get; set; }
        private DbSet<ReportCardTeacherShowResult> ReportCardTeacherShow { get; set; }
        private DbSet<RoomChatContactResult> RoomChatContact { get; set; }
        private DbSet<RoomChatGroupByGradeIDResult> RoomChatGroupByGradeID { get; set; }
        private DbSet<RoomChatGroupByIDResult> RoomChatGroupByID { get; set; }
        private DbSet<RoomChatGroupMemberResult> RoomChatGroupMember { get; set; }
        private DbSet<RoomChatLeftPropertyResult> RoomChatLeftProperty { get; set; }
        private DbSet<RoomChatLeftShowResult> RoomChatLeftShow { get; set; }
        private DbSet<RoomChatRightShowResult> RoomChatRightShow { get; set; }
        private DbSet<RoomChatRightShowNewResult> RoomChatRightShowNew { get; set; }
        private DbSet<RoomLiveShowResult> RoomLiveShow { get; set; }
        private DbSet<RoomTeacherExamShowResult> RoomTeacherExamShow { get; set; }
        private DbSet<ScoreTypeDetailShowResult> ScoreTypeDetailShow { get; set; }
        private DbSet<StatisticsShowResult> StatisticsShow { get; set; }
        private DbSet<UserFireBaseShowResult> UserFireBaseShow { get; set; }


        internal void AddModelCreatingConfiguration(ModelBuilder modelBuilder)
        {
            // No key   
            modelBuilder.Query<ArticleShowTopResult>().HasNoKey();
            modelBuilder.Query<CeremonyShowResult>().HasNoKey();
            modelBuilder.Query<DisciplineShowResult>().HasNoKey();
            modelBuilder.Query<ExamInfoResult>().HasNoKey();
            modelBuilder.Query<ExamResponseResult>().HasNoKey();
            modelBuilder.Query<ExamStudentShowResult>().HasNoKey();
            modelBuilder.Query<FinancialShowResult>().HasNoKey();
            modelBuilder.Query<GalleryDetailShowResult>().HasNoKey();
            modelBuilder.Query<GalleryShowResult>().HasNoKey();
            modelBuilder.Query<HomeworkAnswerStudentShowResult>().HasNoKey();
            modelBuilder.Query<HomeworkByIDResult>().HasNoKey();
            modelBuilder.Query<HomeworkDetailShowResult>().HasNoKey();
            modelBuilder.Query<HomeworkDetailsShowByIDResult>().HasNoKey();
            modelBuilder.Query<HomeworkShowResult>().HasNoKey();
            modelBuilder.Query<IntroductionGroupShowResult>().HasNoKey();
            modelBuilder.Query<IntroductionShowAllResult>().HasNoKey();
            modelBuilder.Query<LoginUserResult>().HasNoKey();
            modelBuilder.Query<ReportCardPaperShowResult>().HasNoKey();
            modelBuilder.Query<ReportCardParentStudentShowResult>().HasNoKey();
            modelBuilder.Query<ReportCardShowAllResult>().HasNoKey();
            modelBuilder.Query<ReportCardStudentDetailShowResult>().HasNoKey();
            modelBuilder.Query<ReportCardStudentShowResult>().HasNoKey();
            modelBuilder.Query<ReportCardTeacherCourseByRoomChatGroupIDResult>().HasNoKey();
            modelBuilder.Query<ReportCardTeacherCourseShowResult>().HasNoKey();
            modelBuilder.Query<ReportCardTeacherScoreShowResult>().HasNoKey();
            modelBuilder.Query<ReportCardTeacherShowResult>().HasNoKey();
            modelBuilder.Query<RoomChatContactResult>().HasNoKey();
            modelBuilder.Query<RoomChatGroupByGradeIDResult>().HasNoKey();
            modelBuilder.Query<RoomChatGroupByIDResult>().HasNoKey();
            modelBuilder.Query<RoomChatGroupMemberResult>().HasNoKey();
            modelBuilder.Query<RoomChatLeftPropertyResult>().HasNoKey();
            modelBuilder.Query<RoomChatLeftShowResult>().HasNoKey();
            modelBuilder.Query<RoomChatRightShowResult>().HasNoKey();
            modelBuilder.Query<RoomChatRightShowNewResult>().HasNoKey();
            modelBuilder.Query<RoomLiveShowResult>().HasNoKey();
            modelBuilder.Query<RoomTeacherExamShowResult>().HasNoKey();
            modelBuilder.Query<ScoreTypeDetailShowResult>().HasNoKey();
            modelBuilder.Query<StatisticsShowResult>().HasNoKey();
            modelBuilder.Query<UserFireBaseShowResult>().HasNoKey();
            //Thanks Valecass!!!
            base.OnModelCreating(modelBuilder);
        }
        public async Task<List<ArticleShowTopResult>> ArticleShowTopAsync()
        {
            //Initialize Result 
            List<ArticleShowTopResult> lst = new List<ArticleShowTopResult>();


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ArticleShowTop] ";

            //Output Data
            lst = await this.ArticleShowTop.FromSqlRaw(sqlQuery).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<CeremonyShowResult>> CeremonyShowAsync()
        {
            //Initialize Result 
            List<CeremonyShowResult> lst = new List<CeremonyShowResult>();


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[CeremonyShow] ";

            //Output Data
            lst = await this.CeremonyShow.FromSqlRaw(sqlQuery).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<DisciplineShowResult>> DisciplineShowAsync(int? UserID)
        {
            //Initialize Result 
            List<DisciplineShowResult> lst = new List<DisciplineShowResult>();

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[DisciplineShow] @UserID";

            //Output Data
            lst = await this.DisciplineShow.FromSqlRaw(sqlQuery, p_UserID).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<ExamInfoResult>> ExamInfoAsync(int? ExamID)
        {
            //Initialize Result 
            List<ExamInfoResult> lst = new List<ExamInfoResult>();

            // Parameters
            SqlParameter p_ExamID = new SqlParameter("@ExamID", ExamID ?? (object)DBNull.Value);
            p_ExamID.Direction = ParameterDirection.Input;
            p_ExamID.DbType = DbType.Int32;
            p_ExamID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ExamInfo] @ExamID";

            //Output Data
            lst = await this.ExamInfo.FromSqlRaw(sqlQuery, p_ExamID).ToListAsync();

            //Return
            return lst;
        }

        public async Task<List<ExamResponseResult>> ExamResponseAsync(int? ExamID)
        {
            //Initialize Result 
            List<ExamResponseResult> lst = new List<ExamResponseResult>();

            // Parameters
            SqlParameter p_ExamID = new SqlParameter("@ExamID", ExamID ?? (object)DBNull.Value);
            p_ExamID.Direction = ParameterDirection.Input;
            p_ExamID.DbType = DbType.Int32;
            p_ExamID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ExamResponse] @ExamID";

            //Output Data
            lst = await this.ExamResponse.FromSqlRaw(sqlQuery, p_ExamID).ToListAsync();

            //Return
            return lst;
        }

        public void ExamSetScoreAuto(int? ExamID)
        {

            // Parameters
            SqlParameter p_ExamID = new SqlParameter("@ExamID", ExamID ?? (object)DBNull.Value);
            p_ExamID.Direction = ParameterDirection.Input;
            p_ExamID.DbType = DbType.Int32;
            p_ExamID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ExamSetScoreAuto] @ExamID";
            //Execution
            this.Database.ExecuteSqlRaw(sqlQuery, p_ExamID);

            //Return
        }

        public async Task<List<ExamStudentShowResult>> ExamStudentShowAsync(int? UserID)
        {
            //Initialize Result 
            List<ExamStudentShowResult> lst = new List<ExamStudentShowResult>();

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ExamStudentShow] @UserID";

            //Output Data
            lst = await this.ExamStudentShow.FromSqlRaw(sqlQuery, p_UserID).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<FinancialShowResult>> FinancialShowAsync(int? UserID)
        {
            //Initialize Result 
            List<FinancialShowResult> lst = new List<FinancialShowResult>();

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[FinancialShow] @UserID";

            //Output Data
            lst = await this.FinancialShow.FromSqlRaw(sqlQuery, p_UserID).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<GalleryDetailShowResult>> GalleryDetailShowAsync(int? GalleryID)
        {
            //Initialize Result 
            List<GalleryDetailShowResult> lst = new List<GalleryDetailShowResult>();

            // Parameters
            SqlParameter p_GalleryID = new SqlParameter("@GalleryID", GalleryID ?? (object)DBNull.Value);
            p_GalleryID.Direction = ParameterDirection.Input;
            p_GalleryID.DbType = DbType.Int32;
            p_GalleryID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[GalleryDetailShow] @GalleryID";

            //Output Data
            lst = await this.GalleryDetailShow.FromSqlRaw(sqlQuery, p_GalleryID).ToListAsync();

            //Return
            return lst;
        }
        

        public async Task<List<GalleryShowResult>> GalleryShowAsync()
        {
            //Initialize Result 
            List<GalleryShowResult> lst = new List<GalleryShowResult>();


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[GalleryShow] ";

            //Output Data
            lst = await this.GalleryShow.FromSqlRaw(sqlQuery).ToListAsync();

            //Return
            return lst;
        }

        public async Task<List<HomeworkAnswerStudentShowResult>> HomeworkAnswerStudentShowAsync(int? UserID, int? CourseID, int? RoomID, int? PageNumber, int? PageSize)
        {
            //Initialize Result 
            List<HomeworkAnswerStudentShowResult> lst = new List<HomeworkAnswerStudentShowResult>();
            try
            {
                // Parameters
                SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
                p_UserID.Direction = ParameterDirection.Input;
                p_UserID.DbType = DbType.Int32;
                p_UserID.Size = 4;

                SqlParameter p_CourseID = new SqlParameter("@CourseID", CourseID ?? (object)DBNull.Value);
                p_CourseID.Direction = ParameterDirection.Input;
                p_CourseID.DbType = DbType.Int32;
                p_CourseID.Size = 4;

                SqlParameter p_RoomID = new SqlParameter("@RoomID", RoomID ?? (object)DBNull.Value);
                p_RoomID.Direction = ParameterDirection.Input;
                p_RoomID.DbType = DbType.Int32;
                p_RoomID.Size = 4;

                SqlParameter p_PageNumber = new SqlParameter("@PageNumber", PageNumber ?? (object)DBNull.Value);
                p_PageNumber.Direction = ParameterDirection.Input;
                p_PageNumber.DbType = DbType.Int32;
                p_PageNumber.Size = 4;

                SqlParameter p_PageSize = new SqlParameter("@PageSize", PageSize ?? (object)DBNull.Value);
                p_PageSize.Direction = ParameterDirection.Input;
                p_PageSize.DbType = DbType.Int32;
                p_PageSize.Size = 4;


                // Processing 
                string sqlQuery = $@"EXEC [dbo].[HomeworkAnswerStudentShow] @UserID, @CourseID, @RoomID, @PageNumber, @PageSize";

                //Output Data
                lst = await this.HomeworkAnswerStudentShow.FromSqlRaw(sqlQuery, p_UserID, p_CourseID, p_RoomID, p_PageNumber, p_PageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Return
            return lst;
        }

        public async Task<List<HomeworkByIDResult>> HomeworkByIDAsync(int? UserID, int? HoomeworkID)
        {
            //Initialize Result 
            List<HomeworkByIDResult> lst = new List<HomeworkByIDResult>();
            try
            {
                // Parameters
                SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
                p_UserID.Direction = ParameterDirection.Input;
                p_UserID.DbType = DbType.Int32;
                p_UserID.Size = 4;

                SqlParameter p_HoomeworkID = new SqlParameter("@HoomeworkID", HoomeworkID ?? (object)DBNull.Value);
                p_HoomeworkID.Direction = ParameterDirection.Input;
                p_HoomeworkID.DbType = DbType.Int32;
                p_HoomeworkID.Size = 4;


                // Processing 
                string sqlQuery = $@"EXEC [dbo].[HomeworkByID] @UserID, @HoomeworkID";

                //Output Data
                lst = await this.HomeworkByID.FromSqlRaw(sqlQuery, p_UserID, p_HoomeworkID).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Return
            return lst;
        }

        public async Task<List<HomeworkDetailShowResult>> HomeworkDetailShowAsync(int? HomeworkID, int? UserID)
        {
            //Initialize Result 
            List<HomeworkDetailShowResult> lst = new List<HomeworkDetailShowResult>();
            try
            {
                // Parameters
                SqlParameter p_HomeworkID = new SqlParameter("@HomeworkID", HomeworkID ?? (object)DBNull.Value);
                p_HomeworkID.Direction = ParameterDirection.Input;
                p_HomeworkID.DbType = DbType.Int32;
                p_HomeworkID.Size = 4;

                SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
                p_UserID.Direction = ParameterDirection.Input;
                p_UserID.DbType = DbType.Int32;
                p_UserID.Size = 4;


                // Processing 
                string sqlQuery = $@"EXEC [dbo].[HomeworkDetailShow] @HomeworkID, @UserID";

                //Output Data
                lst = await this.HomeworkDetailShow.FromSqlRaw(sqlQuery, p_HomeworkID, p_UserID).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Return
            return lst;
        }

        public async Task<List<HomeworkDetailsShowByIDResult>> HomeworkDetailsShowByIDAsync(int? HomeworkID, int? UserID)
        {
            //Initialize Result 
            List<HomeworkDetailsShowByIDResult> lst = new List<HomeworkDetailsShowByIDResult>();
            try
            {
                // Parameters
                SqlParameter p_HomeworkID = new SqlParameter("@HomeworkID", HomeworkID ?? (object)DBNull.Value);
                p_HomeworkID.Direction = ParameterDirection.Input;
                p_HomeworkID.DbType = DbType.Int32;
                p_HomeworkID.Size = 4;

                SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
                p_UserID.Direction = ParameterDirection.Input;
                p_UserID.DbType = DbType.Int32;
                p_UserID.Size = 4;


                // Processing 
                string sqlQuery = $@"EXEC [dbo].[HomeworkDetailsShowByID] @HomeworkID, @UserID";

                //Output Data
                lst = await this.HomeworkDetailsShowByID.FromSqlRaw(sqlQuery, p_HomeworkID, p_UserID).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Return
            return lst;
        }
        public async Task<List<HomeworkShowResult>> HomeworkShowAsync(int? UserID, int? CourseID, int? RoomID, int? PageNumber, int? PageSize)
        {
            //Initialize Result 
            List<HomeworkShowResult> lst = new List<HomeworkShowResult>();
            try
            {
                // Parameters
                SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
                p_UserID.Direction = ParameterDirection.Input;
                p_UserID.DbType = DbType.Int32;
                p_UserID.Size = 4;

                SqlParameter p_CourseID = new SqlParameter("@CourseID", CourseID ?? (object)DBNull.Value);
                p_CourseID.Direction = ParameterDirection.Input;
                p_CourseID.DbType = DbType.Int32;
                p_CourseID.Size = 4;

                SqlParameter p_RoomID = new SqlParameter("@RoomID", RoomID ?? (object)DBNull.Value);
                p_RoomID.Direction = ParameterDirection.Input;
                p_RoomID.DbType = DbType.Int32;
                p_RoomID.Size = 4;

                SqlParameter p_PageNumber = new SqlParameter("@PageNumber", PageNumber ?? (object)DBNull.Value);
                p_PageNumber.Direction = ParameterDirection.Input;
                p_PageNumber.DbType = DbType.Int32;
                p_PageNumber.Size = 4;

                SqlParameter p_PageSize = new SqlParameter("@PageSize", PageSize ?? (object)DBNull.Value);
                p_PageSize.Direction = ParameterDirection.Input;
                p_PageSize.DbType = DbType.Int32;
                p_PageSize.Size = 4;


                // Processing 
                string sqlQuery = $@"EXEC [dbo].[HomeworkShow] @UserID, @CourseID, @RoomID, @PageNumber, @PageSize";

                //Output Data
                lst = await this.HomeworkShow.FromSqlRaw(sqlQuery, p_UserID, p_CourseID, p_RoomID, p_PageNumber, p_PageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Return
            return lst;
        }
        public async Task<List<IntroductionGroupShowResult>> IntroductionGroupShowAsync(int? IntroductionTypeID)
        {
            //Initialize Result 
            List<IntroductionGroupShowResult> lst = new List<IntroductionGroupShowResult>();

            // Parameters
            SqlParameter p_IntroductionTypeID = new SqlParameter("@IntroductionTypeID", IntroductionTypeID ?? (object)DBNull.Value);
            p_IntroductionTypeID.Direction = ParameterDirection.Input;
            p_IntroductionTypeID.DbType = DbType.Int32;
            p_IntroductionTypeID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[IntroductionGroupShow] @IntroductionTypeID";

            //Output Data
            lst = await this.IntroductionGroupShow.FromSqlRaw(sqlQuery, p_IntroductionTypeID).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<IntroductionShowAllResult>> IntroductionShowAllAsync()
        {
            //Initialize Result 
            List<IntroductionShowAllResult> lst = new List<IntroductionShowAllResult>();


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[IntroductionShowAll] ";

            //Output Data
            lst = await this.IntroductionShowAll.FromSqlRaw(sqlQuery).ToListAsync();

            //Return
            return lst;
        }

        //public async Task<vm_HomeworkShowResult> HomeworkShowAsync(int? UserID, int? CourseID, int? RoomID, int? PageNumber, int? PageSize)
        //{
        //     int? RecordCount = 0;
        //    //Initialize Result 
        //  vm_HomeworkShowResult   lst = new vm_HomeworkShowResult();
        //    try
        //    {
        //        // Parameters
        //        SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
        //        p_UserID.Direction = ParameterDirection.Input;
        //        p_UserID.DbType = DbType.Int32;
        //        p_UserID.Size = 4;

        //        SqlParameter p_CourseID = new SqlParameter("@CourseID", CourseID ?? (object)DBNull.Value);
        //        p_CourseID.Direction = ParameterDirection.Input;
        //        p_CourseID.DbType = DbType.Int32;
        //        p_CourseID.Size = 4;

        //        SqlParameter p_RoomID = new SqlParameter("@RoomID", RoomID ?? (object)DBNull.Value);
        //        p_RoomID.Direction = ParameterDirection.Input;
        //        p_RoomID.DbType = DbType.Int32;
        //        p_RoomID.Size = 4;

        //        SqlParameter p_PageNumber = new SqlParameter("@PageNumber", PageNumber ?? (object)DBNull.Value);
        //        p_PageNumber.Direction = ParameterDirection.Input;
        //        p_PageNumber.DbType = DbType.Int32;
        //        p_PageNumber.Size = 4;

        //        SqlParameter p_PageSize = new SqlParameter("@PageSize", PageSize ?? (object)DBNull.Value);
        //        p_PageSize.Direction = ParameterDirection.Input;
        //        p_PageSize.DbType = DbType.Int32;
        //        p_PageSize.Size = 4;

        //        SqlParameter p_RecordCount = new SqlParameter("@RecordCount", RecordCount ?? (object)DBNull.Value);
        //        p_RecordCount.Direction = ParameterDirection.Output;
        //        p_RecordCount.DbType = DbType.Int32;
        //        p_RecordCount.Size = 4;


        //        // Processing 
        //        string sqlQuery = $@"EXEC [dbo].[HomeworkShow] @UserID, @CourseID, @RoomID, @PageNumber, @PageSize, @RecordCount OUTPUT";

        //        //Output Data
        //        lst.homeworkShowResults = await this.HomeworkShow.FromSqlRaw(sqlQuery, p_UserID, p_CourseID, p_RoomID, p_PageNumber, p_PageSize, p_RecordCount).ToListAsync();

        //        //Output Params
        //        lst.pageCount = (int)p_RecordCount.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    //Return
        //    return lst;
        //}
        public void LoginEnd(int? UserID)
        {

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[LoginEnd] @UserID";
            //Execution
            this.Database.ExecuteSqlRaw(sqlQuery, p_UserID);

            //Return
        }
        public async Task<List<LoginUserResult>> LoginUserAsync(string UserName, string UserPass)
        {
            //Initialize Result 
            List<LoginUserResult> lst = new List<LoginUserResult>();
            try
            {
                // Parameters
                SqlParameter p_UserName = new SqlParameter("@UserName", UserName ?? (object)DBNull.Value);
                p_UserName.Direction = ParameterDirection.Input;
                p_UserName.DbType = DbType.String;
                p_UserName.Size = 20;

                SqlParameter p_UserPass = new SqlParameter("@UserPass", UserPass ?? (object)DBNull.Value);
                p_UserPass.Direction = ParameterDirection.Input;
                p_UserPass.DbType = DbType.String;
                p_UserPass.Size = 1000;


                // Processing 
                string sqlQuery = $@"EXEC [dbo].[LoginUser] @UserName, @UserPass";

                //Output Data
                lst = await this.LoginUser.FromSqlRaw(sqlQuery, p_UserName, p_UserPass).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Return
            return lst;
        }

        public async Task<List<ReportCardPaperShowResult>> ReportCardPaperShowAsync(int? ReportCardID, int? RoomChatGroupID, int? TeacherID)
        {
            //Initialize Result 
            List<ReportCardPaperShowResult> lst = new List<ReportCardPaperShowResult>();

            // Parameters
            SqlParameter p_ReportCardID = new SqlParameter("@ReportCardID", ReportCardID ?? (object)DBNull.Value);
            p_ReportCardID.Direction = ParameterDirection.Input;
            p_ReportCardID.DbType = DbType.Int32;
            p_ReportCardID.Size = 4;

            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;

            SqlParameter p_TeacherID = new SqlParameter("@TeacherID", TeacherID ?? (object)DBNull.Value);
            p_TeacherID.Direction = ParameterDirection.Input;
            p_TeacherID.DbType = DbType.Int32;
            p_TeacherID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ReportCardPaperShow] @ReportCardID, @RoomChatGroupID, @TeacherID";

            //Output Data
            lst = await this.ReportCardPaperShow.FromSqlRaw(sqlQuery, p_ReportCardID, p_RoomChatGroupID, p_TeacherID).ToListAsync();

            //Return
            return lst;
        }

        public async Task<List<ReportCardParentStudentShowResult>> ReportCardParentStudentShowAsync(int? UserID, int? UserTypeID)
        {
            //Initialize Result 
            List<ReportCardParentStudentShowResult> lst = new List<ReportCardParentStudentShowResult>();

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;

            SqlParameter p_UserTypeID = new SqlParameter("@UserTypeID", UserTypeID ?? (object)DBNull.Value);
            p_UserTypeID.Direction = ParameterDirection.Input;
            p_UserTypeID.DbType = DbType.Int32;
            p_UserTypeID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ReportCardParentStudentShow] @UserID, @UserTypeID";

            //Output Data
            lst = await this.ReportCardParentStudentShow.FromSqlRaw(sqlQuery, p_UserID, p_UserTypeID).ToListAsync();

            //Return
            return lst;
        }

        public async Task<List<ReportCardShowAllResult>> ReportCardShowAllAsync()
        {
            //Initialize Result 
            List<ReportCardShowAllResult> lst = new List<ReportCardShowAllResult>();


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ReportCardShowAll] ";

            //Output Data
            lst = await this.ReportCardShowAll.FromSqlRaw(sqlQuery).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<ReportCardStudentDetailShowResult>> ReportCardStudentDetailShowAsync(int? ReportCardID, int? UserID)
        {
            //Initialize Result 
            List<ReportCardStudentDetailShowResult> lst = new List<ReportCardStudentDetailShowResult>();

            // Parameters
            SqlParameter p_ReportCardID = new SqlParameter("@ReportCardID", ReportCardID ?? (object)DBNull.Value);
            p_ReportCardID.Direction = ParameterDirection.Input;
            p_ReportCardID.DbType = DbType.Int32;
            p_ReportCardID.Size = 4;

            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ReportCardStudentDetailShow] @ReportCardID, @UserID";

            //Output Data
            lst = await this.ReportCardStudentDetailShow.FromSqlRaw(sqlQuery, p_ReportCardID, p_UserID).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<ReportCardStudentShowResult>> ReportCardStudentShowAsync(int? UserID)
        {
            //Initialize Result 
            List<ReportCardStudentShowResult> lst = new List<ReportCardStudentShowResult>();

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ReportCardStudentShow] @UserID";

            //Output Data
            lst = await this.ReportCardStudentShow.FromSqlRaw(sqlQuery, p_UserID).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<ReportCardTeacherCourseByRoomChatGroupIDResult>> ReportCardTeacherCourseByRoomChatGroupIDAsync(int? RoomChatGroupID, int? TeacherID)
        {
            //Initialize Result 
            List<ReportCardTeacherCourseByRoomChatGroupIDResult> lst = new List<ReportCardTeacherCourseByRoomChatGroupIDResult>();

            // Parameters
            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;

            SqlParameter p_TeacherID = new SqlParameter("@TeacherID", TeacherID ?? (object)DBNull.Value);
            p_TeacherID.Direction = ParameterDirection.Input;
            p_TeacherID.DbType = DbType.Int32;
            p_TeacherID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ReportCardTeacherCourseByRoomChatGroupID] @RoomChatGroupID, @TeacherID";

            //Output Data
            lst = await this.ReportCardTeacherCourseByRoomChatGroupID.FromSqlRaw(sqlQuery, p_RoomChatGroupID, p_TeacherID).ToListAsync();

            //Return
            return lst;
        }

        public async Task<List<ReportCardTeacherCourseShowResult>> ReportCardTeacherCourseShowAsync(int? ReportCardID)
        {
            //Initialize Result 
            List<ReportCardTeacherCourseShowResult> lst = new List<ReportCardTeacherCourseShowResult>();

            // Parameters
            SqlParameter p_ReportCardID = new SqlParameter("@ReportCardID", ReportCardID ?? (object)DBNull.Value);
            p_ReportCardID.Direction = ParameterDirection.Input;
            p_ReportCardID.DbType = DbType.Int32;
            p_ReportCardID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ReportCardTeacherCourseShow] @ReportCardID";

            //Output Data
            lst = await this.ReportCardTeacherCourseShow.FromSqlRaw(sqlQuery, p_ReportCardID).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<ReportCardTeacherScoreShowResult>> ReportCardTeacherScoreShowAsync(int? ReportCardID, int? RoomChatGroupID, int? TeacherID, int? CourseID)
        {
            //Initialize Result 
            List<ReportCardTeacherScoreShowResult> lst = new List<ReportCardTeacherScoreShowResult>();

            // Parameters
            SqlParameter p_ReportCardID = new SqlParameter("@ReportCardID", ReportCardID ?? (object)DBNull.Value);
            p_ReportCardID.Direction = ParameterDirection.Input;
            p_ReportCardID.DbType = DbType.Int32;
            p_ReportCardID.Size = 4;

            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;

            SqlParameter p_TeacherID = new SqlParameter("@TeacherID", TeacherID ?? (object)DBNull.Value);
            p_TeacherID.Direction = ParameterDirection.Input;
            p_TeacherID.DbType = DbType.Int32;
            p_TeacherID.Size = 4;

            SqlParameter p_CourseID = new SqlParameter("@CourseID", CourseID ?? (object)DBNull.Value);
            p_CourseID.Direction = ParameterDirection.Input;
            p_CourseID.DbType = DbType.Int32;
            p_CourseID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ReportCardTeacherScoreShow] @ReportCardID, @RoomChatGroupID, @TeacherID, @CourseID";

            //Output Data
            lst = await this.ReportCardTeacherScoreShow.FromSqlRaw(sqlQuery, p_ReportCardID, p_RoomChatGroupID, p_TeacherID, p_CourseID).ToListAsync();

            //Return
            return lst;
        }

        public async Task<List<ReportCardTeacherShowResult>> ReportCardTeacherShowAsync(int? RoomChatGroupID, int? TeacherID, int? CourseID)
        {
            //Initialize Result 
            List<ReportCardTeacherShowResult> lst = new List<ReportCardTeacherShowResult>();

            // Parameters
            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;

            SqlParameter p_TeacherID = new SqlParameter("@TeacherID", TeacherID ?? (object)DBNull.Value);
            p_TeacherID.Direction = ParameterDirection.Input;
            p_TeacherID.DbType = DbType.Int32;
            p_TeacherID.Size = 4;

            SqlParameter p_CourseID = new SqlParameter("@CourseID", CourseID ?? (object)DBNull.Value);
            p_CourseID.Direction = ParameterDirection.Input;
            p_CourseID.DbType = DbType.Int32;
            p_CourseID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[ReportCardTeacherShow] @RoomChatGroupID, @TeacherID, @CourseID";

            //Output Data
            lst = await this.ReportCardTeacherShow.FromSqlRaw(sqlQuery, p_RoomChatGroupID, p_TeacherID, p_CourseID).ToListAsync();

            //Return
            return lst;
        }

        public async Task<List<RoomChatContactResult>> RoomChatContactAsync(int? UserID, int? UserTypeID)
        {
            //Initialize Result 
            List<RoomChatContactResult> lst = new List<RoomChatContactResult>();

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;

            SqlParameter p_UserTypeID = new SqlParameter("@UserTypeID", UserTypeID ?? (object)DBNull.Value);
            p_UserTypeID.Direction = ParameterDirection.Input;
            p_UserTypeID.DbType = DbType.Int32;
            p_UserTypeID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomChatContact] @UserID, @UserTypeID";

            //Output Data
            lst = await this.RoomChatContact.FromSqlRaw(sqlQuery, p_UserID, p_UserTypeID).ToListAsync();

            //Return
            return lst;
        }
        public void RoomChatDeleteAll(int? RoomChatGroupID, int? UserID)
        {

            // Parameters
            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;

            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomChatDeleteAll] @RoomChatGroupID, @UserID";
            //Execution
            this.Database.ExecuteSqlRaw(sqlQuery, p_RoomChatGroupID, p_UserID);

            //Return
        }
        public async Task<List<RoomChatGroupByGradeIDResult>> RoomChatGroupByGradeIDAsync(int? GradeID)
        {
            //Initialize Result 
            List<RoomChatGroupByGradeIDResult> lst = new List<RoomChatGroupByGradeIDResult>();

            // Parameters
            SqlParameter p_GradeID = new SqlParameter("@GradeID", GradeID ?? (object)DBNull.Value);
            p_GradeID.Direction = ParameterDirection.Input;
            p_GradeID.DbType = DbType.Int32;
            p_GradeID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomChatGroupByGradeID] @GradeID";

            //Output Data
            lst = await this.RoomChatGroupByGradeID.FromSqlRaw(sqlQuery, p_GradeID).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<RoomChatGroupByIDResult>> RoomChatGroupByIDAsync(int? UserID, int? UserTypeID)
        {
            //Initialize Result 
            List<RoomChatGroupByIDResult> lst = new List<RoomChatGroupByIDResult>();

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;

            SqlParameter p_UserTypeID = new SqlParameter("@UserTypeID", UserTypeID ?? (object)DBNull.Value);
            p_UserTypeID.Direction = ParameterDirection.Input;
            p_UserTypeID.DbType = DbType.Int32;
            p_UserTypeID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomChatGroupByID] @UserID, @UserTypeID";

            //Output Data
            lst = await this.RoomChatGroupByID.FromSqlRaw(sqlQuery, p_UserID, p_UserTypeID).ToListAsync();

            //Return
            return lst;
        }
        public void RoomChatGroupInsert()
        {
            try
            {

                // Processing 
                string sqlQuery = $@"EXEC [dbo].[RoomChatGroupInsert] ";
                //Execution
                this.Database.ExecuteSqlRaw(sqlQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Return
        }

        public async Task<List<RoomChatGroupMemberResult>> RoomChatGroupMemberAsync(int? RoomChatGroupID)
        {
            //Initialize Result 
            List<RoomChatGroupMemberResult> lst = new List<RoomChatGroupMemberResult>();

            // Parameters
            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomChatGroupMember] @RoomChatGroupID";

            //Output Data
            lst = await this.RoomChatGroupMember.FromSqlRaw(sqlQuery, p_RoomChatGroupID).ToListAsync();

            //Return
            return lst;
        }
        public void RoomChatGroupUpdate(int? RoomChatGroupID, int? RoomChatID)
        {

            // Parameters
            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;

            SqlParameter p_RoomChatID = new SqlParameter("@RoomChatID", RoomChatID ?? (object)DBNull.Value);
            p_RoomChatID.Direction = ParameterDirection.Input;
            p_RoomChatID.DbType = DbType.Int32;
            p_RoomChatID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomChatGroupUpdate] @RoomChatGroupID, @RoomChatID";
            //Execution
            this.Database.ExecuteSqlRaw(sqlQuery, p_RoomChatGroupID, p_RoomChatID);

            //Return
        }
        public async Task<List<RoomChatLeftPropertyResult>> RoomChatLeftPropertyAsync(int? RoomID, int? RoomChatGroupID)
        {
            //Initialize Result 
            List<RoomChatLeftPropertyResult> lst = new List<RoomChatLeftPropertyResult>();

            // Parameters
            SqlParameter p_RoomID = new SqlParameter("@RoomID", RoomID ?? (object)DBNull.Value);
            p_RoomID.Direction = ParameterDirection.Input;
            p_RoomID.DbType = DbType.Int32;
            p_RoomID.Size = 4;

            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomChatLeftProperty] @RoomID, @RoomChatGroupID";

            //Output Data
            lst = await this.RoomChatLeftProperty.FromSqlRaw(sqlQuery, p_RoomID, p_RoomChatGroupID).ToListAsync();

            //Return
            return lst;
        }
        public async Task<Tuple<List<RoomChatLeftShowResult>, int?>> RoomChatLeftShowAsync(int? RoomChatGroupID, bool? TagLearn, int? UserID,int? NewChatCount,int? PageNumber, int? PageSize,string SearchText)
        {
            //Initialize Result 
            List<RoomChatLeftShowResult> lst = new List<RoomChatLeftShowResult>();

            // Parameters
            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;

            SqlParameter p_TagLearn = new SqlParameter("@TagLearn", TagLearn ?? (object)DBNull.Value);
            p_TagLearn.Direction = ParameterDirection.Input;
            p_TagLearn.DbType = DbType.Boolean;
            p_TagLearn.Size = 1;

            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;

            SqlParameter p_NewChatCount = new SqlParameter("@NewChatCount", NewChatCount ?? (object)DBNull.Value);
            p_NewChatCount.Direction = ParameterDirection.Input;
            p_NewChatCount.DbType = DbType.Int32;
            p_NewChatCount.Size = 4;

            SqlParameter p_PageNumber = new SqlParameter("@PageNumber", PageNumber ?? (object)DBNull.Value);
            p_PageNumber.Direction = ParameterDirection.Input;
            p_PageNumber.DbType = DbType.Int32;
            p_PageNumber.Size = 4;

            SqlParameter p_PageSize = new SqlParameter("@PageSize", PageSize ?? (object)DBNull.Value);
            p_PageSize.Direction = ParameterDirection.Input;
            p_PageSize.DbType = DbType.Int32;
            p_PageSize.Size = 4;

            SqlParameter p_SearchText = new SqlParameter("@SearchText", SearchText ?? (object)DBNull.Value);
            p_SearchText.Direction = ParameterDirection.Input;
            p_SearchText.DbType = DbType.String;
            p_SearchText.Size = -1;

            int? RoomChatViewLast = null;

            SqlParameter p_RoomChatViewLast = new SqlParameter("@RoomChatViewLast", RoomChatViewLast ?? (object)DBNull.Value);
            p_RoomChatViewLast.Direction = ParameterDirection.Output;
            p_RoomChatViewLast.DbType = DbType.Int32;
            p_RoomChatViewLast.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomChatLeftShow] @RoomChatGroupID, @TagLearn, @UserID, @NewChatCount, @PageNumber, @PageSize, @SearchText, @RoomChatViewLast OUTPUT";

            //Output Data
            lst = await this.RoomChatLeftShow.FromSqlRaw(sqlQuery, p_RoomChatGroupID, p_TagLearn, p_UserID, p_NewChatCount, p_PageNumber, p_PageSize, p_SearchText, p_RoomChatViewLast).ToListAsync();

           // //Output Params
           //var  RoomChatViewLast = (int?)p_RoomChatViewLast.Value;


            //Return
            return new Tuple<List<RoomChatLeftShowResult>, int?>(lst, (int?)(p_RoomChatViewLast== (object)DBNull.Value|| p_RoomChatViewLast.Value== (object)DBNull.Value ? 0: p_RoomChatViewLast.Value));
        }

        public async Task<List<RoomChatRightShowResult>> RoomChatRightShowAsync(int? UserID, int? UserTypeID)
        {
            //Initialize Result 
            List<RoomChatRightShowResult> lst = new List<RoomChatRightShowResult>();
            try
            {
                // Parameters
                SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
                p_UserID.Direction = ParameterDirection.Input;
                p_UserID.DbType = DbType.Int32;
                p_UserID.Size = 4;

                SqlParameter p_UserTypeID = new SqlParameter("@UserTypeID", UserTypeID ?? (object)DBNull.Value);
                p_UserTypeID.Direction = ParameterDirection.Input;
                p_UserTypeID.DbType = DbType.Int32;
                p_UserTypeID.Size = 4;


                // Processing 
                string sqlQuery = $@"EXEC [dbo].[RoomChatRightShow] @UserID, @UserTypeID";

                //Output Data
                lst = await this.RoomChatRightShow.FromSqlRaw(sqlQuery, p_UserID, p_UserTypeID).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Return
            return lst;
        }
        public async Task<List<RoomChatRightShowNewResult>> RoomChatRightShowNewAsync(int? UserID, int? UserTypeID, int? PageNumber, int? PageSize)
        {
            //Initialize Result 
            List<RoomChatRightShowNewResult> lst = new List<RoomChatRightShowNewResult>();

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;

            SqlParameter p_UserTypeID = new SqlParameter("@UserTypeID", UserTypeID ?? (object)DBNull.Value);
            p_UserTypeID.Direction = ParameterDirection.Input;
            p_UserTypeID.DbType = DbType.Int32;
            p_UserTypeID.Size = 4;

            SqlParameter p_PageNumber = new SqlParameter("@PageNumber", PageNumber ?? (object)DBNull.Value);
            p_PageNumber.Direction = ParameterDirection.Input;
            p_PageNumber.DbType = DbType.Int32;
            p_PageNumber.Size = 4;

            SqlParameter p_PageSize = new SqlParameter("@PageSize", PageSize ?? (object)DBNull.Value);
            p_PageSize.Direction = ParameterDirection.Input;
            p_PageSize.DbType = DbType.Int32;
            p_PageSize.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomChatRightShowNew] @UserID, @UserTypeID, @PageNumber, @PageSize";

            //Output Data
            lst = await this.RoomChatRightShowNew.FromSqlRaw(sqlQuery, p_UserID, p_UserTypeID, p_PageNumber, p_PageSize).ToListAsync();

            //Return
            return lst;
        }
        public void RoomChatViewInsert(int? RoomChatID, int? RoomChatGroupID, int? UserID)
        {

            // Parameters
            SqlParameter p_RoomChatID = new SqlParameter("@RoomChatID", RoomChatID ?? (object)DBNull.Value);
            p_RoomChatID.Direction = ParameterDirection.Input;
            p_RoomChatID.DbType = DbType.Int32;
            p_RoomChatID.Size = 4;

            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;

            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomChatViewInsert] @RoomChatID, @RoomChatGroupID, @UserID";
            //Execution
            this.Database.ExecuteSqlRaw(sqlQuery, p_RoomChatID, p_RoomChatGroupID, p_UserID);

            //Return
        }
        public void RoomChatViewInsertAll(int? UserID, int? RoomChatGroupID)
        {

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;

            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomChatViewInsertAll] @UserID, @RoomChatGroupID";
            //Execution
            this.Database.ExecuteSqlRaw(sqlQuery, p_UserID, p_RoomChatGroupID);

            //Return
        }
        public async Task<List<RoomLiveShowResult>> RoomLiveShowAsync(int? UserID)
        {
            //Initialize Result 
            List<RoomLiveShowResult> lst = new List<RoomLiveShowResult>();

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomLiveShow] @UserID";

            //Output Data
            lst = await this.RoomLiveShow.FromSqlRaw(sqlQuery, p_UserID).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<RoomTeacherExamShowResult>> RoomTeacherExamShowAsync(int? UserID)
        {
            //Initialize Result 
            List<RoomTeacherExamShowResult> lst = new List<RoomTeacherExamShowResult>();

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[RoomTeacherExamShow] @UserID";

            //Output Data
            lst = await this.RoomTeacherExamShow.FromSqlRaw(sqlQuery, p_UserID).ToListAsync();

            //Return
            return lst;
        }
        public async Task<List<ScoreTypeDetailShowResult>> ScoreTypeDetailShowAsync()
        {
            //Initialize Result 
            List<ScoreTypeDetailShowResult> lst = new List<ScoreTypeDetailShowResult>();
            try
            {

                // Processing 
                string sqlQuery = $@"EXEC [dbo].[ScoreTypeDetailShow] ";

                //Output Data
                lst = await this.ScoreTypeDetailShow.FromSqlRaw(sqlQuery).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Return
            return lst;
        }
        public void HomeworkUpdate(int? HomeworkID, DateTime? HomeworkStartDate, DateTime? HomeworkFineshDate, string HomeworkDescription, string HomeworkTile, int? HomeworkTypeId, int? ScoreTypeId,string HomeWorkFileName)
        {
            try
            {
                // Parameters
                SqlParameter p_HomeworkID = new SqlParameter("@HomeworkID", HomeworkID ?? (object)DBNull.Value);
                p_HomeworkID.Direction = ParameterDirection.Input;
                p_HomeworkID.DbType = DbType.Int32;
                p_HomeworkID.Size = 4;

                SqlParameter p_HomeworkStartDate = new SqlParameter("@HomeworkStartDate", HomeworkStartDate ?? (object)DBNull.Value);
                p_HomeworkStartDate.Direction = ParameterDirection.Input;
                p_HomeworkStartDate.DbType = DbType.DateTime;
                p_HomeworkStartDate.Size = 8;

                SqlParameter p_HomeworkFineshDate = new SqlParameter("@HomeworkFineshDate", HomeworkFineshDate ?? (object)DBNull.Value);
                p_HomeworkFineshDate.Direction = ParameterDirection.Input;
                p_HomeworkFineshDate.DbType = DbType.DateTime;
                p_HomeworkFineshDate.Size = 8;

                SqlParameter p_HomeworkDescription = new SqlParameter("@HomeworkDescription", HomeworkDescription ?? (object)DBNull.Value);
                p_HomeworkDescription.Direction = ParameterDirection.Input;
                p_HomeworkDescription.DbType = DbType.String;
                p_HomeworkDescription.Size = -1;

                SqlParameter p_HomeWorkFileName = new SqlParameter("@HomeWorkFileName", HomeWorkFileName ?? (object)DBNull.Value);
                p_HomeWorkFileName.Direction = ParameterDirection.Input;
                p_HomeWorkFileName.DbType = DbType.String;
                p_HomeWorkFileName.Size = -1;

                SqlParameter p_HomeworkTile = new SqlParameter("@HomeworkTile", HomeworkTile ?? (object)DBNull.Value);
                p_HomeworkTile.Direction = ParameterDirection.Input;
                p_HomeworkTile.DbType = DbType.String;
                p_HomeworkTile.Size = 400;

                SqlParameter p_HomeworkTypeId = new SqlParameter("@HomeworkTypeId", HomeworkTypeId ?? (object)DBNull.Value);
                p_HomeworkTypeId.Direction = ParameterDirection.Input;
                p_HomeworkTypeId.DbType = DbType.Int32;
                p_HomeworkTypeId.Size = 4;

                SqlParameter p_ScoreTypeId = new SqlParameter("@ScoreTypeId", ScoreTypeId ?? (object)DBNull.Value);
                p_ScoreTypeId.Direction = ParameterDirection.Input;
                p_ScoreTypeId.DbType = DbType.Int32;
                p_ScoreTypeId.Size = 4;

      


                // Processing 
                string sqlQuery = $@"EXEC [dbo].[HomeworkUpdate] @HomeworkID, @HomeworkStartDate, @HomeworkFineshDate, @HomeworkDescription, @HomeworkTile, @HomeworkTypeId, @ScoreTypeId,@HomeWorkFileName";
                //Execution
                this.Database.ExecuteSqlRaw(sqlQuery, p_HomeworkID, p_HomeworkStartDate, p_HomeworkFineshDate, p_HomeworkDescription, p_HomeworkTile, p_HomeworkTypeId, p_ScoreTypeId,p_HomeWorkFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Return
        }

        public async Task<List<StatisticsShowResult>> StatisticsShowAsync()
        {
            //Initialize Result 
            List<StatisticsShowResult> lst = new List<StatisticsShowResult>();


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[StatisticsShow] ";

            //Output Data
            lst = await this.StatisticsShow.FromSqlRaw(sqlQuery).ToListAsync();

            //Return
            return lst;
        }
        

        public async Task<List<UserFireBaseShowResult>> UserFireBaseShowAsync(int? RoomChatGroupID, int? UserID)
        {
            //Initialize Result 
            List<UserFireBaseShowResult> lst = new List<UserFireBaseShowResult>();

            // Parameters
            SqlParameter p_RoomChatGroupID = new SqlParameter("@RoomChatGroupID", RoomChatGroupID ?? (object)DBNull.Value);
            p_RoomChatGroupID.Direction = ParameterDirection.Input;
            p_RoomChatGroupID.DbType = DbType.Int32;
            p_RoomChatGroupID.Size = 4;

            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[UserFireBaseShow] @RoomChatGroupID, @UserID";

            //Output Data
            lst = await this.UserFireBaseShow.FromSqlRaw(sqlQuery, p_RoomChatGroupID, p_UserID).ToListAsync();

            //Return
            return lst;
        }
        public void UserFireBaseUpdate(int? UserID, string MobileName, string PlatfornName, string TokenFireBase)
        {

            // Parameters
            SqlParameter p_UserID = new SqlParameter("@UserID", UserID ?? (object)DBNull.Value);
            p_UserID.Direction = ParameterDirection.Input;
            p_UserID.DbType = DbType.Int32;
            p_UserID.Size = 4;

            SqlParameter p_MobileName = new SqlParameter("@MobileName", MobileName ?? (object)DBNull.Value);
            p_MobileName.Direction = ParameterDirection.Input;
            p_MobileName.DbType = DbType.String;
            p_MobileName.Size = 300;

            SqlParameter p_PlatfornName = new SqlParameter("@PlatfornName", PlatfornName ?? (object)DBNull.Value);
            p_PlatfornName.Direction = ParameterDirection.Input;
            p_PlatfornName.DbType = DbType.String;
            p_PlatfornName.Size = 300;

            SqlParameter p_TokenFireBase = new SqlParameter("@TokenFireBase", TokenFireBase ?? (object)DBNull.Value);
            p_TokenFireBase.Direction = ParameterDirection.Input;
            p_TokenFireBase.DbType = DbType.String;
            p_TokenFireBase.Size = 1000;


            // Processing 
            string sqlQuery = $@"EXEC [dbo].[UserFireBaseUpdate] @UserID, @MobileName, @PlatfornName, @TokenFireBase";
            //Execution
            this.Database.ExecuteSqlRaw(sqlQuery, p_UserID, p_MobileName, p_PlatfornName, p_TokenFireBase);

            //Return
        }

    }
}
