using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
   public class ProfileInfoWebsite:IEntity
    {
        public int Id { get; set; }
        public int ProfieInfoId { get; set; }
        public int WebsiteId { get; set; }
    }
}
