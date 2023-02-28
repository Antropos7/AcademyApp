using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Data.Contexts.Repositories.Abstract;

namespace Data.Contexts.Repositories.Concrete
{
    public class TeacherRepository : ITeacherRepository
    {
        static int id;

        public List<Teacher> GetAll()
        {
            return DbContext.Teachers;
        }

        public Teacher Get(int id)
        {
            return DbContext.Teachers.FirstOrDefault(t => t.Id == id);
        }
        public void Add(Teacher teacher)
        {
            id++;
            teacher.Id = id;
            DbContext.Teachers.Add(teacher);
        }

        public void Update(Teacher teacher)
        {
            var dbTeacher = DbContext.Teachers.FirstOrDefault(t => t.Id == teacher.Id);
            if (dbTeacher != null)
            {
                dbTeacher.Name = teacher.Name;
                dbTeacher.Surname = teacher.Surname;
                dbTeacher.BirthDate = teacher.BirthDate;
                dbTeacher.Specialty = teacher.Specialty;
            }
        }

        public void Delete(Teacher teacher)
        {
            DbContext.Teachers.Remove(teacher);
        }
    }
}
