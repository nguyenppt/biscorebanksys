using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankProject.Business
{
    public interface ICommonBusiness<T> where T : class
    {
        T Entity { get; set; }
        void loadEntity(ref T entry );
        void loadEntrities(ref List<T> entries);
        void commitProcess(int userID);
        void previewProcess(int userID);
        void revertProcess(int userID);
        void authorizeProcess(int userID);

    }
}
