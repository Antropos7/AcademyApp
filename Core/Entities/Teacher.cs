using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Teacher : BaseEntity
    {
        public Teacher()
        {
            Groups = new List<Group>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        public string Specialty { get; set; }

        public List<Group> Groups { get; set; }
    }
}
