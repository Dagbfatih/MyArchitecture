using Core.Entities.Abstract;


namespace Entities.Concrete
{
    public class Language:IEntity
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }
        public string Code { get; set; }

    }
}
