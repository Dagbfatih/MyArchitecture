using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class TestTicket : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public string TicketName { get; set; }
        

    }
}
