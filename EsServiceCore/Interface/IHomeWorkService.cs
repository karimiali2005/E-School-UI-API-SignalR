using Es.DataLayerCore.DTOs;
using Es.DataLayerCore.DTOs.HomeWork;
using Es.DataLayerCore.Model;
using EsServiceCore.DTOs.HomeWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsServiceCore.Service.Interface
{
   public interface IHomeWorkService
    {
        Task<List<HomeworkShowResult>> getListHomeWorkByRoomid(int userid, int roomid,int courseid, int pagenumber, int pagesize = 10);
        Task<List<HomeworkAnswerStudentShowResult>> getListHomeworkForStudents(int userid, int roomid,int courseid, int pagenumber, int pagesize = 10);
        Task<List<HomeworkType>> getTypeHomeWork();
        Task<List<ScoreTypeDetailShowResult>> getnomretype();
        Task<vm_AnsverhomeWork> getHomeworkByID(int id, int iduser);
        Task<bool> addHomework(vm_homework vmhomework,int user, DateTime startdate, DateTime enddate,int roomchatgrupeid);
        Task<bool> updateAnswerHomeWork(vm_AnsverhomeWork vmhomework,int userID);
        Task<bool> updateAnswerHomeWork_fromTeacher(vm_HomeworkDetailsShowByIDResult vmhomework, bool isnumberic,DateTime edndate);
        Task<int> addAnsverFile(string filename,int homeworkAnsverID);
        Task<string> removeAnsverFile(int idfileanswer,int idhomeworkanswer);


        #region Teacher
        Task<List<HomeworkDetailShowResult>> getDetailsHomework(int idHomework, int idTeacher);
        Task<vm_HomeworkDetailsShowByIDResult> getDetailsAnswerHomework(int homeworkId = 0, int studentid = 0);
        Task<vm_homework> GetHomeworkByid(int userid, int homeworkid);
        Task<bool> updatehomework(int userid, vm_homework homework, DateTime Start, DateTime end);
        Task<bool> Deletehomework(int userid, int homeworkanswerid);

        #endregion

        //Task CreatHomeWork();
    }
}
