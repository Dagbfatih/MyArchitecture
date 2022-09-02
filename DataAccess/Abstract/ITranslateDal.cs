using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ITranslateDal : IEntityRepository<Translate>
    {
        List<TranslateDetailsDto> GetAllDetails();
        List<TranslateDetailsDto> GetAllDetailsByLanguageId(int languageId);
        List<TranslateDetailsDto> GetAllDetailsByCode(string code);
        List<Translate> GetAllByCode(string code);
        Translate GetByKeyAndCode(string key, string code);
    }
}
