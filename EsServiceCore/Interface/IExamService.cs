using Es.DataLayerCore.DTOs.Exam;
using Es.DataLayerCore.Model;
using EsServiceCore.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface IExamService
    {
        Task<IEnumerable<RoomTeacherExamShowResult>> RoomTeacherExamShow(int userId);
        Task<List<Select2Model>> RoomChatGroups(int userId);

        //درج و ویرایش ازمون
        Task<bool> AddExam(vm_ExamByQuestionsAndChoices model, DateTime examStartDate);

        //نمایش ازمون
        Task<Exam> GetOnlyExamById(int examId);

        //رد ازمون
        bool CancelExam(int examId, string reason);
        //نمایش ازمون
        Task<vm_ExamByQuestionsAndChoices> GetExamById(int examId);

        //لیست ازمونهای هر معلم
        Task<IEnumerable<Exam>> TeacherExamShow(int userId);

        //حذف ازمون
        bool DeleteExam(int userId, int examId, ref string status);
        //روشن و خاموش کردن ازمون
        bool TurnOnOffExam(int userId, int examId, ref string status);

        ////تنظیماتازمون
        //Task<bool> SettingExam(int userId, int examId);

        //لیست ازمونهای هر دانش آموز
        Task<List<ExamStudentShowResult>> ExamStudentShow(int userId);


        //نمایش سوالات در پاسخ دهی
        Task<vm_ResponseByQuestionsAndChoices> GetExamByQuestionForResponse(int userId, int examId, int? questionId);

        //ذخیره پاسخ دهی
        Task<bool> SaveResponse(vm_ResponseByQuestionsAndChoices model);

        //نمایش سوالات در نمره دهی
        Task<vm_ScoreResponseByQuestionsAndChoices> GetExamByQuestionForScore(int responseId,int userId, int? questionId);
        
        //ذخیره نمره دهی
        Task<bool> SaveScore(vm_ScoreResponseByQuestionsAndChoices model);

        Task<List<ScoreTypesDetails>> ScoreTypesDetailsShow();

        Task<vm_ExamResponders> GetExamResponders(int examId);
        bool ExamSetScoreAuto(int examId);
    }
}
