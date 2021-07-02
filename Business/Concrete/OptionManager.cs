using Business.Abstract;
using Business.Constants;
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
    public class OptionManager : IOptionService
    {
        IOptionDal _optionDal;
        public OptionManager(IOptionDal optionDal)
        {
            _optionDal = optionDal;
        }

        [ValidationAspect(typeof(OptionValidator))]
        public IResult Add(Option option)
        {
            var result = BusinessRules.Run(ChechIfOptionExistsOnQuestion(option));
            if (result != null)
            {
                return result;
            }
            _optionDal.Add(option);
            return new SuccessResult(Messages.OptionAdded);
        }

        private IResult ChechIfOptionExistsOnQuestion(Option option)
        {
            var result = this.GetAllByQuestionId(option.QuestionId);

            foreach (var optionChecked in result.Data)
            {
                if (optionChecked.OptionText == option.OptionText)
                {
                    return new ErrorResult(Messages.OptionExists);
                }
            }
            return new SuccessResult();
        }

        public IResult Delete(Option option)
        {
            _optionDal.Delete(option);
            return new SuccessResult(Messages.OptionDeleted);
        }

        public IDataResult<Option> Get(int id)
        {
            return new SuccessDataResult<Option>(_optionDal.Get(o => o.Id == id), Messages.OptionGot);
        }

        public IDataResult<List<Option>> GetAll()
        {
            return new SuccessDataResult<List<Option>>(_optionDal.GetAll(), Messages.OptionsGot);
        }

        public IDataResult<Option> GetOptionByCorrectOption(int questionId)
        {
            return new SuccessDataResult<Option>(_optionDal.Get(o => o.QuestionId == questionId && o.Accuracy));
        }

        public IDataResult<List<Option>> GetAllByQuestionId(int questionId)
        {
            return new SuccessDataResult<List<Option>>(_optionDal.GetAll(o => o.QuestionId == questionId));
        }

        public IResult Update(Option option)
        {
            _optionDal.Update(option);
            return new SuccessResult(Messages.OptionUpdated);
        }
    }
}
