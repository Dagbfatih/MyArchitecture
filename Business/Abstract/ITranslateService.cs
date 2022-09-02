using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ITranslateService : IBusinessServiceRepository<Translate>
    {
        IDataResult<List<TranslateDetailsDto>> GetAllDetails();
        IDataResult<List<TranslateDetailsDto>> GetAllDetailsByLanguage(int languageId);
        IDataResult<List<TranslateDetailsDto>> GetAllDetailsByCode(string code);
        IDataResult<List<Translate>> GetAllByCode(string code);
        IDataResult<List<Translate>> GetAllByLanguage(int languageId);
        IDataResult<Dictionary<string, string>> GetAllByCodeAsDictionary(string code);
        IDataResult<Dictionary<string, string>> GetAllByLanguageAsDictionary(int languageId);
        IDataResult<string> GetByKeyAsString(string key, int languageId);
        IDataResult<string> GetByKeyAsString(string key, string code);
    }
}
