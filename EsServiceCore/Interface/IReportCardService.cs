using Es.DataLayerCore.DTOs.ReportCard;
using Es.DataLayerCore.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface IReportCardService
    {
        Task<List<ReportCardShowAllResult>> ReportCardShowAll(string q);
        Task<List<Grade>> GradeShowAll();
        Task<List<ScoreType>> ScoreTypeShowAll();
        Task<bool> ReportCardInsert(ReportCard reportCard);
        Task<ReportCard> ReportCardGetById(int reportCardId);
        Task<bool> ReportCardUpdate(ReportCard reportCard);
        Task<List<ReportCardTeacherCourseShowResult>> ReportCardTeacherCourseShow(int reportCardId);
        Task<List<ReportCardTeacherCourseShowResult>> ReportCardTeacherCourseShow(int reportCardId, List<int> ReportCardTeacherCourseIds);
        Task<bool> ReportCardTeacherCourseDelete(List<int> ReportCardTeacherCourseIds);
        Task<List<RoomChatGroupByGradeIDResult>> RoomChatGroupByGradeID(int gradeId);
        Task<List<Course>> CourseShowAll();
        Task<bool> ReportCardTeacherCourseInsert(ReportCardTeacherCourse reportCardTeacherCourse);
        bool HasReportCardTeacherCourse(int reportCardId, int roomChatGroupId, int courseId);
        Task<List<ReportCardTeacherCourseByRoomChatGroupIDResult>> ReportCardTeacherCourseByRoomChatGroupID(int roomchatGroupId, int teacherId);
        Task<List<ReportCardTeacherShowResult>> ReportCardTeacherShow(int roomchatGroupId, int teacherId, int courseId);
        Task<List<ReportCardTeacherScoreShowResult>> ReportCardTeacherScoreShow(int reportCardId, int roomchatGroupId, int teacherId, int courseId);
        Task<List<ScoreTypeDetail>> ScoreTypeDetailShow();
        Task<bool> ReportCardTeacherScoreSave(List<ReportCardDetail> listreportCardDetails, int teacherId);
        Task<List<ReportCardStudentShowResult>> ReportCardStudentShow(int userId);
        Task<List<ReportCardStudentDetailShowResult>> ReportCardStudentDetailShow(int reportCardId, int userId);
        Task<List<ReportCardParentStudentShowResult>> ReportCardParentStudentShow(int userId, int userTypeId);
        Task<List<ReportCardPaperShowResult>> ReportCardPaperShow(int reportCardId, int roomchatGroupId, int userId);
        Task<bool> ReportCardPaperSave(ReportCardPaper reportCardPaper);
    }
}
