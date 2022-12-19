using ElmahCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESchool.Need.CutomFilter
{
    public class MyErrorLog : ElmahCore.ErrorLog
    {
        public override ErrorLogEntry GetError(string id)
        {
            throw new NotImplementedException();
        }

        public override int GetErrors(int pageIndex, int pageSize, ICollection<ErrorLogEntry> errorEntryList)
        {
            throw new NotImplementedException();
        }

        public override string Log(Error error)
        {
            return this.Log(error);
        }
    }
}
