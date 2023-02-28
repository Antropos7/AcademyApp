using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using Data.Contexts.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Presentation.Services
{
    public class StudentService
    {
        private readonly GroupService _groupService;
        private readonly GroupRepository _groupRepository;
        private readonly StudentRepository _studentRepository;
        private readonly Admin admin;
        public StudentService(Admin admin)
        {
            this.admin = admin;
            _groupService = new GroupService(admin);
            _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();
        }
        public void GetAll()
        {
            var students = _studentRepository.GetAll();
            if (students.Count is 0)
            {
                ConsoleHelper.WriteWithColor("There is no any student!", ConsoleColor.Red);

            }
            else
            {

                ConsoleHelper.WriteWithColor("--- ALL STUDENTS ---", ConsoleColor.Cyan);

                foreach (var student in students)
                {
                    if (student.Group is null)
                    {
                        ConsoleHelper.WriteWithColor($"ID: {student.Id} Fullname:{student.Name} {student.Surname} Created By: {admin.Username}, No Group ", ConsoleColor.Cyan);

                    }
                    else
                    {

                        ConsoleHelper.WriteWithColor($"ID: {student.Id} Fullname:{student.Name} {student.Surname}, Created By: {admin.Username}Group: {student.Group.Name}", ConsoleColor.Cyan);
                    }

                }
            }
        }

        public void GetAllByGroup()
        {
            _groupService.GetAll();

        GroupDesc: ConsoleHelper.WriteWithColor("Enter group id:", ConsoleColor.Cyan);

            int groupId;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Group Id is not correct format:", ConsoleColor.Red);
                goto GroupDesc;
            }

            var group = _groupRepository.Get(groupId);
            if (group == null)
            {
                ConsoleHelper.WriteWithColor("There is no group in this id", ConsoleColor.Red);
                goto GroupDesc;
            }

            if (group.Students.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no students in this group ", ConsoleColor.Red);

            }
            foreach (var student in group.Students)
            {
                ConsoleHelper.WriteWithColor($"ID: {student.Id} Fullname:{student.Name} {student.Surname}, Group: {student.Group?.Name}", ConsoleColor.Cyan);
            }
        }

        public void Create()
        {
            if (_groupRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("You must create group first!", ConsoleColor.Red);
                return;
            }
            ConsoleHelper.WriteWithColor("Enter student name:", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter student surname:", ConsoleColor.Cyan);
            string surname = Console.ReadLine();

        EmailDesc: ConsoleHelper.WriteWithColor("Enter student email: ", ConsoleColor.Cyan);
            string email = Console.ReadLine();

            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Email is not correct format! Try again: ", ConsoleColor.Red);
                goto EmailDesc;
            }

            if (_studentRepository.isDublicatedEmail(email))
            {
                ConsoleHelper.WriteWithColor("This email is already used!", ConsoleColor.Red);
                goto EmailDesc;
            }

        BirthDateDescription:
            ConsoleHelper.WriteWithColor("================", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("Enter birth date", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("================", ConsoleColor.DarkCyan);


            DateTime birthDate;

            bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);

            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("Birth date is not correct format", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("================================", ConsoleColor.DarkRed);

                goto BirthDateDescription;
            }
            DateTime boundaryDate = new DateTime(1900, 1, 1);

            if (birthDate < boundaryDate)
            {
                ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("Birth date is not chosen right", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                goto BirthDateDescription;
            }
        GroupDescription: _groupService.GetAll();
            ConsoleHelper.WriteWithColor("Choose group for adding: ", ConsoleColor.Cyan);

            ConsoleHelper.WriteWithColor("Enter group ID: ", ConsoleColor.Cyan);
            int groupId;
            isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Group Id is not correct format!", ConsoleColor.DarkRed);
                goto GroupDescription;
            }

            var group = _groupRepository.Get(groupId);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("There is no group in this id!", ConsoleColor.DarkRed);
                goto GroupDescription;
            }

            if (group.MaxSize <= group.Students.Count)
            {
                ConsoleHelper.WriteWithColor($"This group is full! Students in group: {group.MaxSize}", ConsoleColor.DarkRed);
                goto GroupDescription;
            }

            var student = new Student
            {
                Name = name,
                Surname = surname,
                Email = email,
                BirthDate = birthDate,
                Group = group,
                GroupId = group.Id,
                CreatedBy = admin.Username
            };

            group.Students.Add(student);
            _studentRepository.Add(student);
            ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname} is successfully added!", ConsoleColor.Green);

        }
        public void Update()
        {
        IdDescription: GetAll();
            ConsoleHelper.WriteWithColor("Enter student ID: ", ConsoleColor.Cyan);

            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("ID format is not correct!", ConsoleColor.DarkRed);
                goto IdDescription; ;
            }
            var student = _studentRepository.Get(id);
            if (student == null)
            {
                ConsoleHelper.WriteWithColor("There is no student in this id!", ConsoleColor.DarkRed);
                goto IdDescription;
            }
            ConsoleHelper.WriteWithColor("Enter new name:", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("Enter new surname:", ConsoleColor.Cyan);
            string surname = Console.ReadLine();
            ConsoleHelper.WriteWithColor("================", ConsoleColor.DarkCyan);

        BirthDateDesc: ConsoleHelper.WriteWithColor("Enter birth date", ConsoleColor.Cyan);
            
            DateTime birthDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format", ConsoleColor.DarkRed);
                goto BirthDateDesc;
            }
            _groupService.GetAll();
        GroupIdDesc: ConsoleHelper.WriteWithColor("Enter group id for new update", ConsoleColor.Cyan);
            int groupId;
            isSucceeded = int.TryParse(Console.ReadLine(), out groupId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Group ID is not correct format", ConsoleColor.DarkRed);
                goto GroupIdDesc;
            }

            var group = _groupRepository.Get(groupId);
            if (group == null)
            {
                ConsoleHelper.WriteWithColor("There is no group in this id!", ConsoleColor.DarkRed);
                goto GroupIdDesc;
            }

            student.Name = name;
            student.Surname = surname;
            student.BirthDate = birthDate;
            student.Group = group;
            student.GroupId = groupId;
            student.ModifiedBy = admin.Username;

            _studentRepository.Update(student);
            ConsoleHelper.WriteWithColor($"{student.Name}, {student.Surname} Group: {student.Group.Name} successfully updated", ConsoleColor.Green);

        }

        public void Delete()
        {
            GetAll();
        EnterIdDesc: ConsoleHelper.WriteWithColor("Enter student id", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Wrong id format!", ConsoleColor.Cyan);
                goto EnterIdDesc;
            }

            var student = _studentRepository.Get(id);
            if (student == null)
            {
                ConsoleHelper.WriteWithColor("There is no student in this id", ConsoleColor.Cyan);
                goto EnterIdDesc;
            }
            _studentRepository.Delete(student);
            ConsoleHelper.WriteWithColor($"{student.Name}, {student.Surname} is successfully deleted!", ConsoleColor.Green);

        }
    }
}
