using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstract;
using System.Linq;
using Core.Entities.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTranslateDal : EfEntityRepositoryBase<Translate, SqlContext>, ITranslateDal
    {
        public List<Translate> GetAllByCode(string code)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from t in context.Translates
                             join l in context.Languages
                             on t.LanguageId equals l.Id
                             where l.Code == code
                             select new Translate
                             {
                                 Id = t.Id,
                                 Key = t.Key,
                                 LanguageId = l.Id,
                                 Value = t.Value
                             };
                return result.ToList();
            }
        }

        public List<TranslateDetailsDto> GetAllDetails()
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from t in context.Translates
                             join l in context.Languages
                             on t.LanguageId equals l.Id
                             select new TranslateDetailsDto
                             {
                                 Translate = t,
                                 Language = l
                             };
                return result.ToList();
            }
        }

        public List<TranslateDetailsDto> GetAllDetailsByCode(string code)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from t in context.Translates
                             join l in context.Languages
                             on t.LanguageId equals l.Id
                             where l.Code == code
                             select new TranslateDetailsDto
                             {
                                 Translate = t,
                                 Language = l
                             };
                return result.ToList();
            }
        }

        public List<TranslateDetailsDto> GetAllDetailsByLanguageId(int languageId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from t in context.Translates
                             join l in context.Languages
                             on t.LanguageId equals l.Id
                             where l.Id == languageId
                             select new TranslateDetailsDto
                             {
                                 Translate = t,
                                 Language = l
                             };
                return result.ToList();
            }
        }

        public Translate GetByKeyAndCode(string key, string code)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from t in context.Translates
                             join l in context.Languages
                             on t.LanguageId equals l.Id
                             where t.Key == key
                             where l.Code == code
                             select new Translate
                             {
                                 Id = t.Id,
                                 Key = t.Key,
                                 LanguageId = l.Id,
                                 Value = t.Value
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
