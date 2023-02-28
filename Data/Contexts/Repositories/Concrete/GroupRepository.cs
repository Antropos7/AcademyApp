using Core.Entities;
using Data.Contexts.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts.Repositories.Concrete
{
    public class GroupRepository : IGroupRepository
    {
        static int id;
        public List<Group> GetAll()
        {
            return DbContext.Groups;
        }
        public Group Get(int id)
        {
            return DbContext.Groups.FirstOrDefault(g => g.Id == id);
        }
        public Group GetByName(string name)
        {
            return DbContext.Groups.FirstOrDefault(g => g.Name.ToLower() == name.ToLower());
        }

      

        public void Add(Group group)
        {
            id++;
            group.CreatedAt = DateTime.Now;
            group.Id = id;

            DbContext.Groups.Add(group);
        }
        public void Update(Group group)
        {
            var dbGroup = DbContext.Groups.FirstOrDefault(g => g.Id == group.Id);
            if (dbGroup == null) return;
            dbGroup.Name = group.Name;
            dbGroup.MaxSize = group.MaxSize;
            dbGroup.StartDate = group.StartDate;
            dbGroup.EndDate = group.EndDate;
            dbGroup.ModifiedAt = DateTime.Now;

        }


        public void Delete(Group group)
        {
            DbContext.Groups.Remove(group);
        }


    }
}
