using Student.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student.DAL.EntityContext
{
   public class DataContext<T> where T: Student.DAL.Model.Student
    {
        private  ISaveStrategy<T> _strategySave;

        public DataContext()
        {

        }

        public DataContext(ISaveStrategy<T> saveStrategy)
        {
            this._strategySave = saveStrategy;
        }

        public void ChangeStarategy(ISaveStrategy<T> anyStrategy)
        {
            this._strategySave = anyStrategy;
        }

    }
}
