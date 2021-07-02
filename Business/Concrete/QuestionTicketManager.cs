using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class QuestionTicketManager : IQuestionTicketService
    {
        IQuestionTicketDal _questionTicketDal;
        public QuestionTicketManager(IQuestionTicketDal questionTicketDal)
        {
            _questionTicketDal = questionTicketDal;
        }

        [ValidationAspect(typeof(QuestionTicketValidator))]
        public IResult Add(QuestionTicket entity)
        {
            _questionTicketDal.Add(entity);
            return new SuccessResult(Messages.QuestionTicketAdded);
        }

        public IResult Delete(QuestionTicket entity)
        {
            _questionTicketDal.Delete(entity);
            return new SuccessResult(Messages.QuestionTicketDeleted);
        }

        public IDataResult<QuestionTicket> Get(int id)
        {
            return new SuccessDataResult<QuestionTicket>(_questionTicketDal.Get(qt => qt.Id == id));
        }

        public IDataResult<List<QuestionTicket>> GetAll()
        {
            return new SuccessDataResult<List<QuestionTicket>>(_questionTicketDal.GetAll());
        }

        [ValidationAspect(typeof(QuestionTicketValidator))]

        public IResult Update(QuestionTicket entity)
        {
            _questionTicketDal.Update(entity);
            return new SuccessResult(Messages.QuestionTicketUpdated);
        }
    }
}
