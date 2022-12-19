using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using DatabaseLayer.Need;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Access
{
    public class UserOp : IDisposable
    {
        ESchoolContext db = new ESchoolContext();       
        
        public List<User> GetUsers()
        {
            var query = db.User.Include(s => s.UserType)                
                .Select(p => new User
                {
                    Irnational = p.Irnational,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    //Address = p.Address,
                    BirthDate = p.BirthDate,
                    //BirthPlace = p.BirthPlace,
                    Degree = p.Degree,
                    DegreeId = p.DegreeId,
                    //FamilyNumber = p.FamilyNumber,
                    Grade = p.Grade,
                    GradeId = p.GradeId,
                    //IdcardPlace = p.IdcardPlace,
                    //IdcardSerial = p.IdcardSerial,
                    //IdcardSeriesLetter = p.IdcardSeriesLetter,
                    //IdcardSeriesNumber = p.IdcardSeriesNumber,
                    //LivePlace = p.LivePlace,
                    //LivePlaceOther = p.LivePlaceOther,
                    MobileNumber = p.MobileNumber,
                    //ParentDegree = p.ParentDegree,
                    //ParentJob = p.ParentJob,
                    //ParentStatus = p.ParentStatus,
                    //ParentStatusId = p.ParentStatusId,
                    StudyField = p.StudyField,
                    StudyFieldId = p.StudyFieldId,
                    Telephone = p.Telephone,
                    //UserIdfather = p.UserIdfather,
                    //UserIdmother = p.UserIdmother,
                    UserId = p.UserId,
                    UserType = p.UserType,
                    UserTypeId = p.UserTypeId,
                    UserActive = p.UserActive
                })
                .AsEnumerable();            

            return query.ToList();
        }

        public List<User> GetUsers(int degreeId, int gradeId, int studyFieldId, string q, int usertypeid = 1)
        {
            var query = db.User.Include(s => s.Degree).Include(s => s.Grade).Include(s => s.ParentStatus).Include(s => s.StudyField).Include(s => s.UserType)
                .Where(p => p.UserTypeId == usertypeid)
                .Select(p => new User {
                    Irnational = p.Irnational,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Address = p.Address,
                    BirthDate = p.BirthDate,
                    BirthPlace = p.BirthPlace,
                    Degree = p.Degree,
                    DegreeId = p.DegreeId,
                    FamilyNumber = p.FamilyNumber,
                    Grade = p.Grade,
                    GradeId = p.GradeId,
                    IdcardPlace = p.IdcardPlace,
                    IdcardSerial = p.IdcardSerial,
                    IdcardSeriesLetter = p.IdcardSeriesLetter,
                    IdcardSeriesNumber = p.IdcardSeriesNumber,                    
                    LivePlace = p.LivePlace,
                    LivePlaceOther = p.LivePlaceOther,                    
                    MobileNumber = p.MobileNumber,                    
                    ParentDegree = p.ParentDegree,
                    ParentJob = p.ParentJob,
                    ParentStatus = p.ParentStatus,
                    ParentStatusId = p.ParentStatusId,
                    StudyField = p.StudyField,
                    StudyFieldId = p.StudyFieldId,                    
                    Telephone = p.Telephone,      
                    UserIdfather = p.UserIdfather,
                    UserIdmother = p.UserIdmother,
                    UserId = p.UserId,                    
                    UserType = p.UserType,
                    UserTypeId = p.UserTypeId,
                    UserActive = p.UserActive
                })
                .AsEnumerable();
            query = degreeId > 0 ? query.Where(p => p.DegreeId == degreeId) : query;
            query = gradeId > 0 ? query.Where(p => p.GradeId == gradeId) : query;
            query = studyFieldId > 0 ? query.Where(p => p.StudyFieldId == studyFieldId) : query;

            q = Codes.ReplaceForSearch(q);
            query = !string.IsNullOrEmpty(q) ? query.Where(p => p.GetType().GetProperties().Any(a =>
            {
                var value = a.GetValue(p);
                return value != null && Codes.ReplaceForSearch(value.ToString()).Contains(q);
            }) ) : query;

            return query.ToList();
        }


        public List<User> GetUsers(string q, List<int> usertype_ids)
        {
            var query = db.User.Include(s => s.ParentStatus).Include(s => s.UserType)
                .Where(p => usertype_ids.Contains(p.UserTypeId))
                .Select(p => new User
                {
                    Irnational = p.Irnational,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Address = p.Address,
                    BirthDate = p.BirthDate,
                    BirthPlace = p.BirthPlace,
                    Degree = p.Degree,
                    DegreeId = p.DegreeId,
                    FamilyNumber = p.FamilyNumber,
                    Grade = p.Grade,
                    GradeId = p.GradeId,
                    IdcardPlace = p.IdcardPlace,
                    IdcardSerial = p.IdcardSerial,
                    IdcardSeriesLetter = p.IdcardSeriesLetter,
                    IdcardSeriesNumber = p.IdcardSeriesNumber,
                    LivePlace = p.LivePlace,
                    LivePlaceOther = p.LivePlaceOther,
                    MobileNumber = p.MobileNumber,
                    ParentDegree = p.ParentDegree,
                    ParentJob = p.ParentJob,
                    ParentStatus = p.ParentStatus,
                    ParentStatusId = p.ParentStatusId,
                    StudyField = p.StudyField,
                    StudyFieldId = p.StudyFieldId,
                    Telephone = p.Telephone,
                    UserIdfather = p.UserIdfather,
                    UserIdmother = p.UserIdmother,
                    UserId = p.UserId,
                    UserType = p.UserType,
                    UserTypeId = p.UserTypeId,
                    UserActive = p.UserActive
                })
                .AsEnumerable();            

            q = Codes.ReplaceForSearch(q);
            query = !string.IsNullOrEmpty(q) ? query.Where(p => p.GetType().GetProperties().Any(a =>
            {
                var value = a.GetValue(p);
                return value != null && Codes.ReplaceForSearch(value.ToString()).Contains(q);
            })) : query;

            return query.ToList();
        }

        public List<User> GetUsersWityUserType(int usertype)
        {
            var query = db.User.Where(p => p.UserTypeId == usertype)
                .Select(p => new User { UserId = p.UserId, FirstName = p.FirstName, LastName = p.LastName, Irnational = p.Irnational });
            return query.ToList();
                
        }

        public User Login(string username, string password)
        {
            var query = db.User.Where(p => p.Irnational == username);
            if(query.Count() > 0)
            {
                var q = query.First();
                var UserPassword = q.Password;
                if (string.IsNullOrEmpty(UserPassword))
                {
                    string BirthDate = Codes.getPersianDate(q.BirthDate);
                    string Year = BirthDate.Split('/')[0];
                    UserPassword = Year;
                }
                else
                {
                    password = Codes.Hash(password);
                }

                if(password == UserPassword)
                {
                    return q;
                }
            }

            return null;
        }

        public User GetUser(int UserId)
        {
            try
            {
                var query = db.User.Where(p => p.UserId == UserId).Include(s => s.UserType);
                if (query.Count() > 0)
                {
                    return query.First();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }        

        public User GetUser(string irnational)
        {
            try
            {
                var query = db.User.Where(p => p.Irnational == irnational);
                if(query.Count() > 0)
                {
                    return query.First();
                }
                else
                {
                    return null;
                }                
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }


        public User GetUser(string irnational, string MobileNumber)
        {
            try
            {
                var query = db.User.Where(p => p.Irnational == irnational && p.MobileNumber == MobileNumber);
                if (query.Count() > 0)
                {
                    return query.First();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public IEnumerable<User> GetUsers(int UserId)
        {
            try
            {
                var query = db.User.Where(p => p.UserId == UserId)
                    .Select(p => new User
                    {
                        Irnational = p.Irnational,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Address = p.Address,
                        BirthDate = p.BirthDate,
                        BirthPlace = p.BirthPlace,
                        Degree = p.Degree,
                        DegreeId = p.DegreeId,
                        FamilyNumber = p.FamilyNumber,
                        Grade = p.Grade,
                        GradeId = p.GradeId,                        
                        IdcardPlace = p.IdcardPlace,
                        IdcardSerial = p.IdcardSerial,
                        IdcardSeriesLetter = p.IdcardSeriesLetter,
                        IdcardSeriesNumber = p.IdcardSeriesNumber,
                        Insurance = p.Insurance,
                        InverseUserIdfatherNavigation = p.InverseUserIdfatherNavigation,
                        InverseUserIdmotherNavigation = p.InverseUserIdmotherNavigation,
                        IsCityPlace = p.IsCityPlace,
                        IsRelativesParents = p.IsRelativesParents,
                        IsStudentInsurance = p.IsStudentInsurance,
                        LivePlace = p.LivePlace,
                        LivePlaceOther = p.LivePlaceOther,
                        Login = p.Login,
                        MobileNumber = p.MobileNumber,
                        Nationality = p.Nationality,
                        ParentDegree = p.ParentDegree,
                        ParentJob = p.ParentJob,
                        ParentStatus = p.ParentStatus,
                        ParentStatusId = p.ParentStatusId,
                        Password = p.Password,
                        PasswordToken = p.PasswordToken,
                        PersianLanguage = p.PersianLanguage,
                        /*Pic = null, 
                        PicThumb = null,*/
                        PicName = p.PicName,
                        PreschoolEducation = p.PreschoolEducation,
                        ReasonInactive = p.ReasonInactive,
                        ReferralReasonNew = p.ReferralReasonNew,
                        RightHanded = p.RightHanded,
                        RoomDetail = p.RoomDetail,
                        SeveralChildren = p.SeveralChildren,
                        StudyField = p.StudyField,
                        StudyFieldId = p.StudyFieldId,
                        TeacherCourse = p.TeacherCourse,
                        Telephone = p.Telephone,
                        UserActive = p.UserActive,
                        UserId = p.UserId,
                        UserIdfather = p.UserIdfather,
                        UserIdfatherNavigation = p.UserIdfatherNavigation,
                        UserIdmother = p.UserIdmother,
                        UserIdmotherNavigation = p.UserIdmotherNavigation,
                        UserRole = p.UserRole,
                        UserType = p.UserType,
                        UserTypeId = p.UserTypeId
                });
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public IEnumerable<User> GetUsers(List<int> UserIds)
        {
            try
            {
                var query = db.User.Include(p => p.UserType).Where(p => UserIds.Contains(p.UserId));
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public IEnumerable<User> GetChilds(int UserId)
        {
            try
            {
                var query = db.User.Include(p => p.UserType).Where(p => p.UserIdfather == UserId || p.UserIdmother == UserId);
                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public bool HasChild(int UserId)
        {
            try
            {
                return db.User.Any(p => p.UserIdfather == UserId || p.UserIdmother == UserId);                
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return false;
            }
        }

        public User GetUserWithParent(int UserId)
        {
            try
            {
                var query = db.User.Where(p => p.UserId == UserId);
                if (query.Count() > 0)
                {
                    var q = query.First();
                    q.UserIdfatherNavigation = q.UserIdfather != null ? GetUser(q.UserIdfather.Value) : null;
                    q.UserIdmotherNavigation = q.UserIdmother != null ? GetUser(q.UserIdmother.Value) : null;
                    return q;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return null;
            }
        }

        public string GetToken(string irnational)
        {
            try
            {
                var query = db.User.Where(p => p.Irnational == irnational);
                if (query.Count() > 0)
                {
                    return query.First().PasswordToken;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "";
            }
        }

        public async Task<bool> SetToken(string irnational, string token)
        {
            try
            {
                var query = db.User.Where(p => p.Irnational == irnational);
                if (query.Count() > 0)
                {
                    query.First().PasswordToken = token;
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return false;
            }
        }

        public async Task<bool> ChangePassword(string irnational, string token, string password)
        {
            try
            {
                var query = db.User.Where(p => p.Irnational == irnational && p.PasswordToken == token);
                if (query.Count() > 0)
                {
                    query.First().Password = Codes.Hash(password);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return false;
            }
        }

        public async Task<bool> ChangePassword(int user_id, string currentpassword, string password)
        {
            try
            {
                var query = db.User.Where(p => p.UserId == user_id);
                if (query.Count() > 0)
                {
                    var q = query.First();
                    var UserPassword = q.Password;
                    if (string.IsNullOrEmpty(UserPassword))
                    {
                        string BirthDate = Codes.getPersianDate(q.BirthDate);
                        string Year = BirthDate.Split('/')[0];
                        UserPassword = Year;
                    }
                    else
                    {
                        currentpassword = Codes.Hash(currentpassword);
                    }

                    if(UserPassword == currentpassword)
                    {
                        q.Password = Codes.Hash(password);
                        await db.SaveChangesAsync();
                        return true;
                    }
                                    
                }

                return false;
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return false;
            }
        }

        public async Task<string> InsertUser(User user)
        {
            try
            {
                user.MobileNumber = string.IsNullOrEmpty(user.MobileNumber) ? "" : user.MobileNumber;
                user.StudyFieldId = user.StudyFieldId == 0 ? null : user.StudyFieldId;
                user.GradeId = user.GradeId == 0 ? null : user.GradeId;
                db.User.Add(user);
                await db.SaveChangesAsync();
                return user.UserId.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<string> DeleteUser(int UserID)
        {
            try
            {
                var user = db.User.Single(p => p.UserId == UserID);
                db.User.Remove(user);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<int> DeleteUsers(List<User> users)
        {
            try
            {
                db.User.RemoveRange(users);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        public async Task<string> UpdateUser(User user)
        {
            try
            {
                db.User.Update(user);
                int count = await db.SaveChangesAsync();
                return count.ToString();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return "Error: " + message;
            }
        }

        public async Task<int> UpdateUsers(List<User> users)
        {
            try
            {
                db.User.UpdateRange(users);
                int count = await db.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return 0;
            }
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
