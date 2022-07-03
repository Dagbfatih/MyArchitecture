using Core.Utilities.Results.Abstract;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Mail
{
    public interface IEmailService
    {
        IDataResult<List<EmailMessage>> ReceiveEmail(int maxCount = 10);
        IResult Send(EmailMessage emailMessage);
    }
}
