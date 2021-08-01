using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfLanguageDal:EfEntityRepositoryBase<Language, SqlContext>, ILanguageDal
    {
    }
}
