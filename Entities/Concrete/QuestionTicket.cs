using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class QuestionTicket:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string TicketName { get; set; }
        
    }
}
