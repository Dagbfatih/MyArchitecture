using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OptionManager : BusinessMessagesService, IOptionService
    {
        private readonly IOptionDal _optionDal;

        public OptionManager(IOptionDal optionDal)
        {
            _optionDal = optionDal;
        }

        [ValidationAspect(typeof(OptionValidator))]
        [SecuredOperation("instructor")]
        public IResult Add(Option option)
        {
            var result = BusinessRules.Run(ChechIfOptionExistsOnQuestion(option));
            if (result != null)
            {
                return result;
            }
            _optionDal.Add(option);
            return new SuccessResult(_messages.OptionAdded);
        }

        private IResult ChechIfOptionExistsOnQuestion(Option option)
        {
            var result = this.GetAllByQuestionId(option.QuestionId);

            foreach (var optionChecked in result.Data)
            {
                if (optionChecked.OptionText == option.OptionText)
                {
                    return new ErrorResult(_messages.OptionExists);
                }
            }
            return new SuccessResult();
        }

        [SecuredOperation("instructor")]
        public IResult Delete(Option option)
        {
            _optionDal.Delete(option);
            return new SuccessResult(_messages.OptionDeleted);
        }

        public IDataResult<Option> Get(int id)
        {
            return new SuccessDataResult<Option>(_optionDal.Get(o => o.Id == id), _messages.OptionGot);
        }

        public IDataResult<List<Option>> GetAll()
        {
            return new SuccessDataResult<List<Option>>(_optionDal.GetAll(), _messages.OptionsGot);
        }

        public IDataResult<Option> GetOptionByCorrectOption(int questionId)
        {
            return new SuccessDataResult<Option>(_optionDal.Get(o => o.QuestionId == questionId && o.Accuracy));
        }

        public IDataResult<List<Option>> GetAllByQuestionId(int questionId)
        {
            return new SuccessDataResult<List<Option>>(_optionDal.GetAll(o => o.QuestionId == questionId));
        }

        [SecuredOperation("instructor")]
        public IResult Update(Option option)
        {
            _optionDal.Update(option);
            return new SuccessResult(_messages.OptionUpdated);
        }
    }
}
