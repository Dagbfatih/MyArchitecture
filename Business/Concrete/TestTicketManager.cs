using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Services;
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
    public class TestTicketManager :BusinessMessagesService, ITestTicketService
    {
        ITestTicketDal _testTicketDal;
        public TestTicketManager(ITestTicketDal testTicketDal)
        {
            _testTicketDal = testTicketDal;
        }

        [ValidationAspect(typeof(TestTicketValidator))]
        [SecuredOperation("admin, instructor")]
        public IResult Add(TestTicket entity)
        {
            _testTicketDal.Add(entity);
            return new SuccessResult(_messages.TestTicketAdded);
        }

        [SecuredOperation("admin, instructor")]
        public IResult Delete(TestTicket entity)
        {
            _testTicketDal.Delete(entity);
            return new SuccessResult(_messages.TestTicketDeleted);
        }

        public IDataResult<TestTicket> Get(int id)
        {
            return new SuccessDataResult<TestTicket>(_testTicketDal.Get(tt => tt.Id == id));
        }

        public IDataResult<List<TestTicket>> GetAll()
        {
            return new SuccessDataResult<List<TestTicket>>(_testTicketDal.GetAll());
        }

        [ValidationAspect(typeof(TestTicketValidator))]
        [SecuredOperation("admin, instructor")]
        public IResult Update(TestTicket entity)
        {
            _testTicketDal.Update(entity);
            return new SuccessResult(_messages.TestTicketUpdated);
        }
    }
}
