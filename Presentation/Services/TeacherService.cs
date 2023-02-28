using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
using Data.Contexts.Repositories.Concrete;

namespace Presentation.Services
{
    public class TeacherService
    {
        private readonly TeacherRepository _teacherRepository;
        public TeacherService()
        {
            _teacherRepository = new TeacherRepository();
        }

        public void GetAll()
        {
            var teachers = _teacherRepository.GetAll();
            if (teachers.Count == 0)
            {
                ConsoleHelper.WriteWithColor("--- There is no teacher in academy --- ", ConsoleColor.Red);
            }


            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($"Id: {teacher.Id} Name: {teacher.Name},Surname: {teacher.Surname}, Specialty: {teacher.Specialty}, Birth date {teacher.BirthDate}", ConsoleColor.Cyan);
                if (teacher.Groups.Count == 0)
                {
                    ConsoleHelper.WriteWithColor("--- Teacher has not the group! --- ", ConsoleColor.Red);

                }
                else
                {

                    foreach (var group in teacher.Groups)
                    {
                        ConsoleHelper.WriteWithColor($"Group Id: {group.Id} Name: {group.Name}", ConsoleColor.Cyan);

                    }
                    Console.WriteLine();
                }

            }
        }


        public void Create()
        {
            ConsoleHelper.WriteWithColor("--- Enter Teacher Name --- ", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter Teacher Surname --- ", ConsoleColor.Cyan);
            string surname = Console.ReadLine();
        BirthDateDescription:
            ConsoleHelper.WriteWithColor("--- Enter Teacher Birth Date --- ", ConsoleColor.Cyan);


            DateTime birthDate = new DateTime(1900, 1, 1);


            bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);

            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format", ConsoleColor.Red);

                goto BirthDateDescription;
            }
            ConsoleHelper.WriteWithColor("--- Enter Teacher Specialty --- ", ConsoleColor.Cyan);
            string specialty = Console.ReadLine();

            var teacher = new Teacher
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                Specialty = specialty,
                CreatedAt = DateTime.Now,
            };

            _teacherRepository.Add(teacher);
            ConsoleHelper.WriteWithColor($"Name: {teacher.Name},Surname: {teacher.Surname}, Specialty: {teacher.Specialty}, Birth date {teacher.BirthDate}", ConsoleColor.Cyan);

        }

        public void Delete()
        {
            GetAll();
            if (_teacherRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("--- There is no teacher in academy --- ", ConsoleColor.Red);
            }
            else
            {

            idDesc: ConsoleHelper.WriteWithColor("--- Enter teacher id --- ", ConsoleColor.Cyan);
                int id;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("--- Id is not correct format! --- ", ConsoleColor.Red);
                    goto idDesc;
                }

                var teacher = _teacherRepository.Get(id);
                if (teacher == null)
                {
                    ConsoleHelper.WriteWithColor("--- There is no teacher in this id --- ", ConsoleColor.Red);
                    goto idDesc;

                }
                _teacherRepository.Delete(teacher);
                ConsoleHelper.WriteWithColor($"Teacher: {teacher.Name} is deleted", ConsoleColor.Green);
            }

        }

        public void Update()
        {
            GetAll();

        EnterGroupIdDesc: ConsoleHelper.WriteWithColor("--- Enter teacher id ---", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("--- Wrong teacher id format! ---", ConsoleColor.Red);
                goto EnterGroupIdDesc;
            }

            var teacher = _teacherRepository.Get(id);
            if (teacher is null)
            {
                ConsoleHelper.WriteWithColor("--- There is no teacher in this id! ---", ConsoleColor.Red);
                goto EnterGroupIdDesc;
            }
            ConsoleHelper.WriteWithColor("--- Enter new name ---", ConsoleColor.Cyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter new surname ---", ConsoleColor.Cyan);
            string surname = Console.ReadLine();
            
            BirthDateDesc: ConsoleHelper.WriteWithColor("--- Enter new birth date ---", ConsoleColor.Cyan);
            DateTime birthDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format", ConsoleColor.DarkRed);
                goto BirthDateDesc;
            }
            ConsoleHelper.WriteWithColor("--- Enter new specialty ---", ConsoleColor.Cyan);
            string specialty = Console.ReadLine();

            teacher.Name = name;
            teacher.Surname = surname;
            teacher.BirthDate = birthDate;
            teacher.Specialty = specialty;

            _teacherRepository.Update(teacher);
            ConsoleHelper.WriteWithColor($"{teacher.Name}, {teacher.Surname} is successfully updated!", ConsoleColor.Green);

        }
    }
}

