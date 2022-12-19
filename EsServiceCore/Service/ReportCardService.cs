using Es.DataLayerCore.Context;
using Es.DataLayerCore.DTOs.ReportCard;
using Es.DataLayerCore.Model;
using EsServiceCore.Interface;
using EsServiceCore.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class ReportCardService: IReportCardService
    {
        private readonly ESchoolContext _context;
        private readonly IMessageService _messageService;

        public ReportCardService(ESchoolContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }


        public async Task<List<ReportCardShowAllResult>> ReportCardShowAll(string q)
        {
            try
            {
                var reportCards = await _context.ReportCardShowAllAsync();
                q = Codes.ReplaceForSearch(q);
                reportCards =( !string.IsNullOrEmpty(q) ? reportCards.AsEnumerable().Where(p => p.GetType().GetProperties().Any(a =>
                {
                    var value = a.GetValue(p);
                    return value != null && Codes.ReplaceForSearch(value.ToString()).Contains(q);
                })) : reportCards).ToList();
                return reportCards;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<Grade>> GradeShowAll()
        {
            try
            {
                var grades = await _context.Grade.ToListAsync();
            
                return grades;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ScoreType>> ScoreTypeShowAll()
        {
            try
            {
                var scoreTypes = await _context.ScoreType.ToListAsync();

                return scoreTypes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> ReportCardInsert(ReportCard reportCard)
        {
            try
            {

                await _context.ReportCard.AddAsync(reportCard);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ReportCard> ReportCardGetById(int reportCardId)
        {
            try
            {
                var reportCard = await _context.ReportCard.FindAsync(reportCardId);
                return reportCard;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> ReportCardUpdate(ReportCard reportCard)
        {
            try
            {

                _context.ReportCard.Update(reportCard);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ReportCardTeacherCourseShowResult>> ReportCardTeacherCourseShow(int reportCardId)
        {
            try
            {
                var reportCardTeacherCourses = await _context.ReportCardTeacherCourseShowAsync(reportCardId);
                return reportCardTeacherCourses;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ReportCardTeacherCourseShowResult>> ReportCardTeacherCourseShow(int reportCardId,List<int> ReportCardTeacherCourseIds)
        {
            try
            { 

                var reportCardTeacherCourses = await _context.ReportCardTeacherCourseShowAsync(reportCardId);
                var queryfilter=reportCardTeacherCourses.Where(p => ReportCardTeacherCourseIds.Contains(p.ReportCardTeacherCourseID)).ToList();
                 return queryfilter;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> ReportCardTeacherCourseDelete(List<int> ReportCardTeacherCourseIds)
        {
            try
            {
                foreach(var id in ReportCardTeacherCourseIds)
                {
                    var reportCardTeacherCourse = await _context.ReportCardTeacherCourse.FindAsync(id);
                     _context.ReportCardTeacherCourse.Remove(reportCardTeacherCourse);
                    await _context.SaveChangesAsync();
                }
              
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<RoomChatGroupByGradeIDResult>> RoomChatGroupByGradeID(int gradeId)
        {
            try
            {
                var roomChatGroups = await _context.RoomChatGroupByGradeIDAsync(gradeId);
                return roomChatGroups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<List<Course>> CourseShowAll()
        {
            try
            {
                var courses = await _context.Course.ToListAsync();
                return courses;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> ReportCardTeacherCourseInsert(ReportCardTeacherCourse reportCardTeacherCourse)
        {
            try
            {

                await _context.ReportCardTeacherCourse.AddAsync(reportCardTeacherCourse);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool HasReportCardTeacherCourse(int reportCardId, int roomChatGroupId, int courseId)
        {
            try
            {
                return _context.ReportCardTeacherCourse.Any(r => r.ReportCardId == reportCardId && r.RoomChatGroupId == roomChatGroupId && r.CourseId == courseId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ReportCardTeacherCourseByRoomChatGroupIDResult>> ReportCardTeacherCourseByRoomChatGroupID(int roomchatGroupId,int teacherId)
        {
            try
            {
                var courses = await _context.ReportCardTeacherCourseByRoomChatGroupIDAsync(roomchatGroupId, teacherId);
                return courses;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ReportCardTeacherShowResult>> ReportCardTeacherShow(int roomchatGroupId, int teacherId, int courseId)
        {
            try
            {
                var reportCards = await _context.ReportCardTeacherShowAsync(roomchatGroupId,teacherId,courseId);
                return reportCards;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ReportCardTeacherScoreShowResult>> ReportCardTeacherScoreShow(int reportCardId,int roomchatGroupId, int teacherId, int courseId)
        {
            try
            {
                var reportCardTeacherScores = await _context.ReportCardTeacherScoreShowAsync(reportCardId,roomchatGroupId,teacherId,courseId);
                return reportCardTeacherScores;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ScoreTypeDetail>> ScoreTypeDetailShow()
        {
            try
            {
                var scoreTypeDetails = await _context.ScoreTypeDetail.Where(s=>s.ScoreTypeId==2).ToListAsync();
                return scoreTypeDetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> ReportCardTeacherScoreSave(List<ReportCardDetail> listreportCardDetails,int teacherId)
        {
            try
            {
                foreach(var listreportCardDetail in listreportCardDetails)
                {
                    var ReportCard = await _context.ReportCard.FindAsync(listreportCardDetail.ReportCardId);
                    if (listreportCardDetail.ReportCardDetailId == 0)
                    {
                        if(listreportCardDetail.ReportCardScore!=null|| listreportCardDetail.ReportCardDescriptiveId!=null|| listreportCardDetail.ReportCardTeacherComment!=null)
                        {
                            await _context.ReportCardDetail.AddAsync(new ReportCardDetail()
                            {
                                ReportCardId = listreportCardDetail.ReportCardId,
                                RoomChatGroupId = listreportCardDetail.RoomChatGroupId,
                                CourseId = listreportCardDetail.CourseId,
                                TeacherId = teacherId,
                                UserId = listreportCardDetail.UserId,
                                ReportCardScore = listreportCardDetail.ReportCardScore,
                                ReportCardDescriptiveId = listreportCardDetail.ReportCardDescriptiveId,
                                ReportCardTeacherComment = listreportCardDetail.ReportCardTeacherComment
                            }); 
                            if(ReportCard.ReportCardScoreEnable)
                              await _messageService.SendMessage(new List<int> { listreportCardDetail.UserId }, "کارنامه", 5, "نمره جدید", 0);
                        }
                        
                        
                    }
                    else
                    {
                        var reportCardDetail = await _context.ReportCardDetail.FindAsync(listreportCardDetail.ReportCardDetailId);
                        reportCardDetail.ReportCardId = listreportCardDetail.ReportCardId;
                        reportCardDetail.RoomChatGroupId = listreportCardDetail.RoomChatGroupId;
                        reportCardDetail.CourseId = listreportCardDetail.CourseId;
                        reportCardDetail.TeacherId = teacherId;
                        reportCardDetail.UserId = listreportCardDetail.UserId;
                        reportCardDetail.ReportCardScore = listreportCardDetail.ReportCardScore;
                        reportCardDetail.ReportCardDescriptiveId = listreportCardDetail.ReportCardDescriptiveId;
                        reportCardDetail.ReportCardTeacherComment = listreportCardDetail.ReportCardTeacherComment;
                        if (ReportCard.ReportCardScoreEnable)
                            await _messageService.SendMessage(new List<int> { listreportCardDetail.UserId }, "کارنامه", 5, "نمره اصلاح شد", 0);
                    }
                    await _context.SaveChangesAsync();
                }
                
              
                

               

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<List<ReportCardStudentShowResult>> ReportCardStudentShow(int userId)
        {
            try
            {
                var reportCards = await _context.ReportCardStudentShowAsync(userId);
                return reportCards;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ReportCardStudentDetailShowResult>> ReportCardStudentDetailShow(int reportCardId,int userId)
        {
            try
            {
                var reportCardDetail = await _context.ReportCardStudentDetailShowAsync(reportCardId,userId);
                return reportCardDetail;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ReportCardParentStudentShowResult>> ReportCardParentStudentShow(int userId,int userTypeId)
        {
            try
            {
                var reportCardParentStudentShows = await _context.ReportCardParentStudentShowAsync(userId,userTypeId);
                return reportCardParentStudentShows;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ReportCardPaperShowResult>> ReportCardPaperShow(int reportCardId,int roomchatGroupId,int userId)
        {
            try
            {
                var reportCardPapers = await _context.ReportCardPaperShowAsync(reportCardId,roomchatGroupId,userId);
                return reportCardPapers;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> ReportCardPaperSave(ReportCardPaper reportCardPaper)
        {
            try
            {
                if (reportCardPaper.ReportCardPaperId == 0)
                {

                    await _context.ReportCardPaper.AddAsync(new ReportCardPaper()
                    {
                        ReportCardPaperDateTime = reportCardPaper.ReportCardPaperDateTime,
                        ReportCardPaperFileName = reportCardPaper.ReportCardPaperFileName,
                        ReportCardId = reportCardPaper.ReportCardId,
                        UserId = reportCardPaper.UserId,
                    });


                }
                else
                {
                    var reportCardPaperNew = await _context.ReportCardPaper.FindAsync(reportCardPaper.ReportCardPaperId);
                    reportCardPaperNew.ReportCardPaperDateTime = reportCardPaper.ReportCardPaperDateTime;
                    reportCardPaperNew.ReportCardPaperFileName = reportCardPaper.ReportCardPaperFileName;
                    reportCardPaperNew.UserId = reportCardPaper.UserId;

                }

                await _context.SaveChangesAsync();
          






                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
