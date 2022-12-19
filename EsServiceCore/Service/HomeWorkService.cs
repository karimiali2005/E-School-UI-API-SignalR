using Es.DataLayerCore.Context;
using Es.DataLayerCore.DTOs;
using Es.DataLayerCore.DTOs.HomeWork;
using Es.DataLayerCore.Model;
using EsServiceCore.DTOs.HomeWork;
using EsServiceCore.Service.Interface;
using EsServiceCore.Utility.Convertor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsServiceCore.Service
{
    public class HomeWorkService : IHomeWorkService
    {

       private readonly ESchoolContext _context;
        public HomeWorkService(ESchoolContext context)
        {
            _context = context;
        }

        public async Task<bool> updateAnswerHomeWork(vm_AnsverhomeWork vmhomework, int userID)
        {
            try
            {
                HomeworkAnswer ha =await _context.HomeworkAnswer.Where(e=>e.HomeworkAnswerId==vmhomework.HomeworkAnswerId&&e.UserId==userID&&e.HomeworkId==vmhomework.HomeworkId).Select(e=>e).FirstOrDefaultAsync();
                if (ha != null)
                {
                    ha.HomeworkAnswerComment = vmhomework.HomeworkAnswerComment;
                    ha.HomeworkAnswerStatusId = 3;
                    ha.HomeworkAnswerTeacherSee = false;
                    ha.SendDatetime = DateTime.Now;
                    ha.EditDateTime = DateTime.Now;
                    if (vmhomework.HomeworkTypeID == 1)//اگر فایل متنی بود
                    {
                        ha.HomeworkResponse = vmhomework.HomeworkResponse;
                    }
                    else
                    {
                        //ToDo insert file AnswerHomeWork

                    }
                    _context.Entry(ha).State = EntityState.Modified;
                    //_context.HomeworkAnswer.Update(ha);
                    await _context.SaveChangesAsync();


                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> addHomework(vm_homework vm_Homework,int user,DateTime startdate, DateTime enddate,int roomchatgrupeid)
        {
            try
            {

                Homework homework = new Homework();
                homework.CourseId = vm_Homework.CourseId;
                homework.HomeworkDescription = vm_Homework.HomeworkDescription;
                homework.HomeworkFinishDate = enddate;
                homework.HomeworkStartDate =startdate;
                homework.HomeWorkFileName = vm_Homework.HomeWorkFileName;
                homework.HomeworkTile = vm_Homework.HomeworkTile;
                homework.HomeworkTypeId = vm_Homework.HomeworkTypeId;
                homework.RoomId = vm_Homework.RoomId;
                homework.ScoreTypeId = vm_Homework.ScoreTypeId;
                homework.RoomChatGroupId = roomchatgrupeid;
                homework.UserId = user;
                homework.HomeworkDelete = false;

                await _context.Homework.AddAsync(homework);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<vm_AnsverhomeWork> getHomeworkByID(int id, int iduser)
        {
            try
            {
                var x =await  _context.HomeworkByIDAsync(iduser, id);
                HomeworkByIDResult homework = x.FirstOrDefault();
                vm_AnsverhomeWork vm = new vm_AnsverhomeWork();
                if (homework == null)
                {

                    return vm;
                        }
                if (homework.HomeworkAnswerID == null||homework.HomeworkAnswerID==0)
                {
                    //TODO insert homeworkAnswer
                    var homeworkget = await _context.Homework.Where(e => e.HomeworkId == homework.HomeworkID).FirstOrDefaultAsync();
                    HomeworkAnswer ha = new HomeworkAnswer();
                    ha.HomeworkId = homework.HomeworkID;
                    ha.HomeworkAnswerStatusId = 2;//مشاهده 
                    ha.VisitDateTime = DateTime.Now;
                    ha.HomeworkAnswerSartDate = homeworkget.HomeworkStartDate;
                    ha.HomeworkAnswerFinishDate = homeworkget.HomeworkFinishDate;
                    ha.UserId = iduser;
                    await _context.HomeworkAnswer.AddAsync(ha);
                    var xx= await _context.SaveChangesAsync();
                    homework.HomeworkAnswerID = ha.HomeworkAnswerId;

                }
               
                //  vm.HomeworkId = x.HomeworkID;
                vm.HomeworkAnswerComment = homework.HomeworkAnswerComment;
                vm.HomeworkAnswerId = (int)homework.HomeworkAnswerID;
                vm.HomeworkAnswerScore = homework.HomeworkAnswerScore;
                vm.HomeworkAnswerStatusId = (int)homework.HomeworkAnswerStatusID;
                vm.HomeworkId = homework.HomeworkID;
                vm.HomeworkResponse = homework.HomeworkResponse;
                vm.homeworktitel = homework.HomeworkTile;
                vm.HomeworkTypeID = homework.HomeworkTypeID;
                vm.HomeworkDescription= homework.HomeworkDescription;
                vm.RoomId = homework.RoomID;
                vm.CourseId = homework.CourseID;
                vm.HomeWorkFileName = homework.HomeWorkFileName;
                vm.HomeworkResponseFile = new List<vmResponsFile>();
                foreach (var item in x)
                {
                    if (!String.IsNullOrEmpty(item.FileName))
                    {
                        vmResponsFile vmf = new vmResponsFile();
                        vmf.id =(int) item.HomeworkAnswerFileID;
                        vmf.filename = item.FileName;
                        vm.HomeworkResponseFile.Add(vmf);

                        
                    }
                }
                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<HomeworkShowResult>> getListHomeWorkByRoomid(int userid, int roomid, int courseid,int pagenumber,int pagesize=10)
        {

            try
            {
              
                var lhw = await _context.HomeworkShowAsync(userid,courseid,roomid,pagenumber,pagesize);
                //         string products = _context.ScoreTypeDetail
                //.FromSql("EXECUTE dbo.GetProducts")
                //.ToString();
                return lhw;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<HomeworkAnswerStudentShowResult>> getListHomeworkForStudents(int userid, int roomid, int courseid,int pagenumber,int pagesize=10)
        {
            try
            {
                var x =await  _context.HomeworkAnswerStudentShowAsync(userid,courseid,roomid,pagenumber,pagesize);
                //foreach (var homework in x.homeworkAnswers)
                //{
                //    if (homework.HomeworkAnswerID == null || homework.HomeworkAnswerID == 0)
                //    {
                //        //TODO insert homeworkAnswer
                      
                //        HomeworkAnswer ha = new HomeworkAnswer();                      
                //        ha.HomeworkId = homework.HomeworkID;
                //        ha.HomeworkAnswerStatusId = 2;//مشاهده 
                //        ha.VisitDateTime = DateTime.Now;                       
                //        ha.HomeworkAnswerFinishDate = homework.HomeworkFinishDate;
                //       ha.HomeworkAnswerSartDate = homework.HomeworkStartDate;
                //        ha.UserId = userid;
                //        await _context.HomeworkAnswer.AddAsync(ha);
                //        await _context.SaveChangesAsync();                     

                //    }
                //}
                return x;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ScoreTypeDetailShowResult>> getnomretype()
        {
            try
            {
                var x =await _context.ScoreTypeDetailShowAsync();
                return x;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<HomeworkType>> getTypeHomeWork()
        {
            try
            {
                var x = await _context.HomeworkType.ToListAsync();
                return x;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> addAnsverFile(string filename, int homeworkAnsverID)
        {
            try
            {
                HomeworkAnswerFile haf = new HomeworkAnswerFile();
                haf.FileName = filename;
                haf.HomeworkAnswerId = homeworkAnsverID;
               await _context.HomeworkAnswerFile.AddAsync(haf);
                await _context.SaveChangesAsync();
                return haf.HomeworkAnswerFileId;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

       

        public async Task<string> removeAnsverFile(int idfileanswer, int idhomeworkanswer)
        {
            try
            {
                var x = await _context.HomeworkAnswerFile.Where(e => e.HomeworkAnswerId == idhomeworkanswer && e.HomeworkAnswerFileId == idfileanswer).FirstOrDefaultAsync();
                if (x != null)
                {
                    _context.HomeworkAnswerFile.Remove(x);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return null;
                }

                return x.FileName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<HomeworkDetailShowResult>> getDetailsHomework(int idHomework, int idTeacher)
        {
            try
            {
              var d=await  _context.HomeworkDetailShowAsync(idHomework, idTeacher);

                return d;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<vm_HomeworkDetailsShowByIDResult> getDetailsAnswerHomework(int homeworkId=0,int studentid=0)
        {
            try
            {
                if (homeworkId == 0 || studentid == 0)
                {
                    return new vm_HomeworkDetailsShowByIDResult();
                }

                var x = await _context.HomeworkDetailsShowByIDAsync(homeworkId,studentid);

                if (x.Count == 0)
                {
                    var homework =await _context.Homework.Where(e => e.HomeworkId == homeworkId).FirstOrDefaultAsync();
                    HomeworkAnswer ha = new HomeworkAnswer();
                    ha.HomeworkId = homeworkId;
                    ha.HomeworkAnswerStatusId = 1;//جدید 
                    
                    ha.HomeworkAnswerSartDate = homework.HomeworkStartDate;
                    ha.HomeworkAnswerFinishDate = homework.HomeworkFinishDate;
                    ha.UserId = studentid;
                    ha.HomeworkAnswerTeacherSee = true;
                    await _context.HomeworkAnswer.AddAsync(ha);
                    var xx = await _context.SaveChangesAsync();                 
                    var x2 = await _context.HomeworkDetailsShowByIDAsync(ha.HomeworkId,ha.UserId);
                    vm_HomeworkDetailsShowByIDResult h = x2.Select(e => new vm_HomeworkDetailsShowByIDResult
                    {
                        EditDateTime = e.EditDateTime,
                        FileAnswers = x.Select(z => new FileAnswer { FileName = z.FileName, HomeworkAnswerFileID = z.HomeworkAnswerFileID }).ToList(),
                        FullName = e.FullName,
                        HomeworkAnswerComment = e.HomeworkAnswerComment,
                        HomeworkAnswerDescriptiveID = e.HomeworkAnswerDescriptiveID,
                        HomeworkAnswerFinishDate = e.HomeworkAnswerFinishDate,
                        HomeworkAnswerID = e.HomeworkAnswerID,
                        HomeworkAnswerSartDate = e.HomeworkAnswerSartDate,
                        HomeworkAnswerScore = e.HomeworkAnswerScore,
                        HomeworkAnswerStatusID = e.HomeworkAnswerStatusID,
                        HomeworkAnswerStatusTitle = e.HomeworkAnswerStatusTitle,
                        HomeworkResponse = e.HomeworkResponse,
                        IsNumber = e.IsNumber,
                        PicName = e.PicName,
                        ScoreTypeID = e.ScoreTypeID,
                        SendDatetime = e.SendDatetime,
                        TeacherComment = e.TeacherComment,
                        UserID = e.UserID,
                        VisitDateTime = e.VisitDateTime,
                        scoreTypes = (e.IsNumber) ? (new List<ScoreTypes>()) : _context.ScoreTypeDetail.Where(s => s.ScoreTypeId == e.ScoreTypeID).Select(s => new ScoreTypes { id = s.ScoreTypeDetailId, value = s.ScoreTypeDetailTitle }).ToList()
                        

                    }).FirstOrDefault();
                    return h;

                }
                else
                {
                    vm_HomeworkDetailsShowByIDResult h = x.Select(e => new vm_HomeworkDetailsShowByIDResult
                    {
                        EditDateTime = e.EditDateTime,
                        FileAnswers = x.Select(z => new FileAnswer { FileName = z.FileName, HomeworkAnswerFileID = z.HomeworkAnswerFileID }).ToList(),
                        FullName = e.FullName,
                        HomeworkAnswerComment = e.HomeworkAnswerComment,
                        HomeworkAnswerDescriptiveID = e.HomeworkAnswerDescriptiveID,
                        HomeworkAnswerFinishDate = e.HomeworkAnswerFinishDate,
                        HomeworkAnswerID = e.HomeworkAnswerID,
                        HomeworkAnswerSartDate = e.HomeworkAnswerSartDate,
                        HomeworkAnswerScore = e.HomeworkAnswerScore,
                        HomeworkAnswerStatusID = e.HomeworkAnswerStatusID,
                        HomeworkAnswerStatusTitle = e.HomeworkAnswerStatusTitle,
                        HomeworkResponse = e.HomeworkResponse,
                        IsNumber = e.IsNumber,
                        PicName = e.PicName,
                        ScoreTypeID = e.ScoreTypeID,
                        SendDatetime = e.SendDatetime,
                        TeacherComment = e.TeacherComment,
                        UserID = e.UserID,
                        VisitDateTime = e.VisitDateTime,
                        scoreTypes = (e.IsNumber) ? (new List<ScoreTypes>()) : _context.ScoreTypeDetail.Where(s => s.ScoreTypeId == e.ScoreTypeID).Select(s => new ScoreTypes { id = s.ScoreTypeDetailId, value = s.ScoreTypeDetailTitle }).ToList()


                    }).FirstOrDefault();
                    return h;
                }
               



              
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> updateAnswerHomeWork_fromTeacher(vm_HomeworkDetailsShowByIDResult vmhomework,bool isnumberic,DateTime edndate)
        {
            try
            {
                var x = await _context.HomeworkAnswer.Where(e => e.HomeworkAnswerId == vmhomework.HomeworkAnswerID).FirstOrDefaultAsync();

                if (x != null)
                {
                    if (isnumberic)
                    {
                        x.HomeworkAnswerScore = vmhomework.HomeworkAnswerScore;
                    }
                    else
                    {
                        if (vmhomework.HomeworkAnswerDescriptiveID != 0)
                        { 
                        x.HomeworkAnswerDescriptiveId = (int) vmhomework.HomeworkAnswerDescriptiveID;
                        }
                    }
                    x.TeacherComment = vmhomework.TeacherComment;
                    x.HomeworkAnswerFinishDate = edndate;
                    x.HomeworkAnswerTeacherSee = true;

                    _context.HomeworkAnswer.Update(x);

                    await _context.SaveChangesAsync();
                    return true;


                                    }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<vm_homework> GetHomeworkByid(int userid, int homeworkid)
        {
            try
            {
                vm_homework vm = await _context.Homework.Where(e => e.UserId == userid && e.HomeworkId == homeworkid)
                    .Select(e=>new vm_homework
                    {
                    CourseId=e.CourseId,
                    end=e.HomeworkFinishDate.getPersianDateByMinutes(),
                   start=e.HomeworkStartDate.getPersianDateByMinutes(),
                   HomeworkId=e.HomeworkId,
                   HomeworkDescription=e.HomeworkDescription,
                   HomeworkTile=e.HomeworkTile,
                   HomeworkTypeId=e.HomeworkTypeId,
                  RoomId=e.RoomId,
                  ScoreTypeId=e.ScoreTypeId,
                  HomeWorkFileName=e.HomeWorkFileName


                    })
                    .FirstOrDefaultAsync();
                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task<bool> updatehomework(int userid, vm_homework homework,DateTime Start, DateTime end)
        {
            try
            {

                /*  var h = await _context.Homework.Where(e => e.UserId == userid && e.HomeworkId == homework.HomeworkId).FirstOrDefaultAsync();
                  h.HomeworkId = homework.HomeworkId;
                  h.HomeworkDescription = homework.HomeworkDescription;
                  h.HomeworkFinishDate = end;
                  h.HomeworkStartDate = Start;
                  h.HomeworkTile = homework.HomeworkTile;
                  h.HomeworkTypeId = homework.HomeworkTypeId;
                  h.RoomId = homework.RoomId;
                  h.ScoreTypeId = homework.ScoreTypeId;
                  h.CourseId = homework.CourseId;
                  _context.Homework.Update(h);
                  await _context.SaveChangesAsync();*/

                _context.HomeworkUpdate(homework.HomeworkId, Start, end, homework.HomeworkDescription, homework.HomeworkTile, homework.HomeworkTypeId, homework.ScoreTypeId,homework.HomeWorkFileName);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Deletehomework(int userid,int homeworkanswerid)
        {
            try
            {

                var x =await  _context.Homework.Where(e => e.UserId == userid && e.HomeworkId == homeworkanswerid).FirstOrDefaultAsync();
                if (x != null)
                {
                    x.HomeworkDelete = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
