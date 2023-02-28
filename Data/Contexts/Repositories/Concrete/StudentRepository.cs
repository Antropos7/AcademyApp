using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Data.Contexts.Repositories.Abstract;

namespace Data.Contexts.Repositories.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        static int id;
        
        public List<Student> GetAll()
        {
            return DbContext.Students;
        }

        public Student Get(int id)
        {
            return DbContext.Students.FirstOrDefault(s => s.Id == id);
        }
        public void Add(Student student)
        {
            id ++;
            student.Id = id;
            DbContext.Students.Add(student);
        }



        public void Update(Student student)
        {
            var dbStudent = DbContext.Students.FirstOrDefault(s => s.Id == student.Id);
            if (dbStudent != null)
            {
                dbStudent.Name = student.Name;
                dbStudent.Surname = student.Surname;
                dbStudent.BirthDate = student.BirthDate;
                dbStudent.Group = student.Group;
                dbStudent.GroupId = student.GroupId;
            }
        }

        public void Delete(Student student)
        {
            DbContext.Students.Remove(student);
        }

        public bool isDublicatedEmail(string email)
        {
            return DbContext.Students.Any(s => s.Email == email);
        }
    }
}
