using System;
using System.Collections.Generic;
using System.Globalization;
using Data.Contexts.Repositories.Concrete;
using System.Linq;
using Core.Helpers;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Services
{
    public class GroupService
    {

        private readonly GroupRepository _groupRepository;
        private readonly StudentRepository _studentRepository;
        private readonly TeacherRepository _teacherRepository;
        private readonly GroupFieldRepository _groupFieldRepository;

        public Admin admin;


        public GroupService(Admin admin)
        {

            _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();
            _teacherRepository = new TeacherRepository();
            _groupFieldRepository = new GroupFieldRepository();
            this.admin = admin;
        }
        public void GetAll()
        {
            var groups = _groupRepository.GetAll();
            ConsoleHelper.WriteWithColor("===========", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("All groups:", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("===========", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("", ConsoleColor.White);

            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor("------------------------------------------------------------------------------------------------------", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor($"Id: {group.Id}, Name: {group.Name}, Max size: {group.MaxSize}, Start date: {group.StartDate}, Created by: {group.CreatedBy} End date: {group.EndDate}", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("------------------------------------------------------------------------------------------------------", ConsoleColor.DarkCyan);
                Console.WriteLine();
            }
        }
        public void GetAllGroupsByTeacher()
        {
            var teachers = _teacherRepository.GetAll();
            if (teachers.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any groups!", ConsoleColor.Red);
                return;
            }
            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($"Teacher's id {teacher.Id} Fullname: {teacher.Name}, {teacher.Surname}", ConsoleColor.Cyan);
            }

        TeacherIdDesc: ConsoleHelper.WriteWithColor("--- Enter teacher id ---", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format", ConsoleColor.Red);
                goto TeacherIdDesc;

            }

            var dbTeacher = _teacherRepository.Get(id);
            if (dbTeacher == null)
            {
                ConsoleHelper.WriteWithColor("There is no teacher in this id", ConsoleColor.Red);
                goto TeacherIdDesc;
            }
            else
            {
                ConsoleHelper.WriteWithColor("Groups by teacher:", ConsoleColor.Cyan);
                foreach (var group in dbTeacher.Groups)
                {

                    ConsoleHelper.WriteWithColor($"Group id {group.Id} Name: {group.Name} ", ConsoleColor.Cyan);


                }
            }




        }




        public void GetAllGroupsByField()
        {
            var groupFields = _groupFieldRepository.GetAll();
            foreach (var groupField in groupFields)
            {
                ConsoleHelper.WriteWithColor($"Group Field Id: {groupField.Id}, Name: {groupField.Name}", ConsoleColor.Cyan);
            }

        GroupFieldIdDesc:

            ConsoleHelper.WriteWithColor("---------------------", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("Enter group field id:", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("---------------------", ConsoleColor.DarkCyan);
            Console.WriteLine();
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("===============================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("Group id is not correct format!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                goto GroupFieldIdDesc;
            }


            var dbGroupField = _groupFieldRepository.Get(id);
            if (dbGroupField == null)
            {
                ConsoleHelper.WriteWithColor("===================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("There is no group field in this id!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("===================================", ConsoleColor.DarkRed);
                Console.WriteLine();
                goto GroupFieldIdDesc;
            }




            foreach (var group in dbGroupField.Groups)
            {
                ConsoleHelper.WriteWithColor("--------------------------------------------", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor($"Group Id: {group.Id}, Name: {group.Name}", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("--------------------------------------------", ConsoleColor.DarkCyan);
                Console.WriteLine();
            }

        }
        public void GetGroupById()
        {
            var groups = _groupRepository.GetAll();
            if (groups.Count == 0)
            {
            AreYouSureDescription:

                ConsoleHelper.WriteWithColor("==============================================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("There is no group in this id. Do you want to create new group?", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("==============================================================", ConsoleColor.DarkRed);
                Console.WriteLine();
                ConsoleHelper.WriteWithColor("*****************", ConsoleColor.DarkGreen);
                ConsoleHelper.WriteWithColor("Select 'y' or 'n'", ConsoleColor.Green);
                ConsoleHelper.WriteWithColor("*****************", ConsoleColor.DarkGreen);
                Console.WriteLine();


                char decision;
                bool isSucceeded = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("==========================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("Wrong format! Try again...", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("==========================", ConsoleColor.DarkRed);
                    Console.WriteLine();
                    goto AreYouSureDescription;

                }

                if (!(decision == 'y' || decision == 'n'))
                {
                    ConsoleHelper.WriteWithColor("========================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("Your choice is not correct! Try again...", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("========================================", ConsoleColor.DarkRed);
                    Console.WriteLine();

                    goto AreYouSureDescription;
                }

                if (decision == 'y')
                {
                    Create();
                    return;
                }
            }
            else
            {

                GetAll();
            EnterIdDescription:

                ConsoleHelper.WriteWithColor("----------------", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("--- Enter ID ---", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("----------------", ConsoleColor.DarkCyan);
                Console.WriteLine();
                int id;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {

                    ConsoleHelper.WriteWithColor("==========================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("=== Invalid ID format ===", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("==========================", ConsoleColor.DarkRed);
                    goto EnterIdDescription;

                }

                var group = _groupRepository.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("=======================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("No any group in this ID", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("=======================", ConsoleColor.DarkRed);
                    Console.WriteLine();

                    ConsoleHelper.WriteWithColor("=============================", ConsoleColor.DarkGreen);
                    ConsoleHelper.WriteWithColor("Try again? Select 'y'' or 'n'", ConsoleColor.Green);
                    ConsoleHelper.WriteWithColor("=============================", ConsoleColor.DarkGreen);
                    Console.WriteLine();
                    char des;
                    bool tryAgain = char.TryParse(Console.ReadLine(), out des);
                    {
                        if (des == 'y')
                        {
                            goto EnterIdDescription;
                        }

                        return;

                    }

                }
                ConsoleHelper.WriteWithColor("-----------------------------------------------------------------------------------------------------------", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor($"Group ID: {group.Id} \n Name: {group.Name} \n Max size: {group.MaxSize} \n Start Date: {group.StartDate.ToShortDateString()} \n End Date: {group.EndDate}", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("-----------------------------------------------------------------------------------------------------------", ConsoleColor.DarkCyan);
                Console.WriteLine();
            }





        }
        public void GetGroupByName()
        {

            GetAll();
        GroupNameDesc:

            ConsoleHelper.WriteWithColor("----------------", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("Enter group name", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("----------------", ConsoleColor.DarkCyan);
            Console.WriteLine();
            string name = Console.ReadLine();



            var group = _groupRepository.GetByName(name);
            if (group == null)
            {
                ConsoleHelper.WriteWithColor("==================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("There is no any group in this name", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("==================================", ConsoleColor.DarkRed);
                Console.WriteLine();
                goto GroupNameDesc;
            }
            ConsoleHelper.WriteWithColor("===============================================================================================", ConsoleColor.DarkCyan);

            ConsoleHelper.WriteWithColor($"Id: {group.Id}, Name: {group.Name}, Max size: {group.MaxSize}, Start date: {group.StartDate}, End date: {group.EndDate}",
                ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("===============================================================================================", ConsoleColor.DarkCyan);


        }

        public void Create()
        {
            if (_teacherRepository.GetAll().Count == 0)
            {

                ConsoleHelper.WriteWithColor("======================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("=== You must create teacher first! ===", color: ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("======================================", ConsoleColor.DarkRed);
                Console.WriteLine();

            }
            if (_groupFieldRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("===========================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("==== You must create group field first! ===", color: ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("===========================================", ConsoleColor.DarkRed);
                Console.WriteLine();

            }
            else
            {

            NameDesc:
                ConsoleHelper.WriteWithColor("==================", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("=== Enter name ===", color: ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("==================", ConsoleColor.DarkCyan);
                string name = Console.ReadLine();
                var group = _groupRepository.GetByName(name);
                if (group != null)
                {
                    ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("=== This group is already added! ===", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);


                    goto NameDesc;
                }

                int maxSize;
            MaxSizeDescription:
                ConsoleHelper.WriteWithColor("====================", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("Enter group max size", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("====================", ConsoleColor.DarkCyan);
                bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);

                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("=======================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("=== Max size is not correct format! ===", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("=======================================", ConsoleColor.DarkRed);
                    goto MaxSizeDescription;
                }

                if (maxSize > 18)
                {
                    ConsoleHelper.WriteWithColor("====================================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("=== Max size should be less than or equals to 18 ===", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("====================================================", ConsoleColor.DarkRed);
                    goto MaxSizeDescription;
                }

            StartDateDescription:
                ConsoleHelper.WriteWithColor("================", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("Enter start date", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("================", ConsoleColor.DarkCyan);
                DateTime startDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out startDate);

                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("Start date is not correct format", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("================================", ConsoleColor.DarkRed);

                    goto StartDateDescription;
                }

                DateTime boundaryDate = new DateTime(2015, 1, 1);

                if (startDate < boundaryDate)
                {
                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("Start date is not chosen right", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                    goto StartDateDescription;
                }

            EndDateDescription:
                ConsoleHelper.WriteWithColor("==============", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("Enter end date", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("==============", ConsoleColor.DarkCyan);
                DateTime endDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out endDate);


                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("End date is not correct format", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                    goto EndDateDescription;
                }

                if (startDate > endDate)
                {
                    ConsoleHelper.WriteWithColor("========================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("End date must be bigger than start date!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("========================================", ConsoleColor.DarkRed);
                    goto EndDateDescription;
                }

                var teachers = _teacherRepository.GetAll();
                foreach (var teacher in teachers)
                {
                    ConsoleHelper.WriteWithColor("-------------------------------------------------------------------------", ConsoleColor.DarkCyan);
                    ConsoleHelper.WriteWithColor($"Teacher's id {teacher.Id} Fullname: {teacher.Name}, {teacher.Surname}", ConsoleColor.Cyan);
                    ConsoleHelper.WriteWithColor("-------------------------------------------------------------------------", ConsoleColor.DarkCyan);

                }

            TeacherIdDesc:
                ConsoleHelper.WriteWithColor("----------------", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("Enter teacher id", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("----------------", ConsoleColor.DarkCyan);
                Console.WriteLine();

                int teacherId;
                isSucceeded = int.TryParse(Console.ReadLine(), out teacherId);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("=======================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("Wrong teacher id format", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("=======================", ConsoleColor.DarkRed);

                    Console.WriteLine();
                    goto TeacherIdDesc;
                }
                var dbTeacher = _teacherRepository.Get(teacherId);
                if (dbTeacher == null)
                {
                    ConsoleHelper.WriteWithColor("===============================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("There is no teacher in this id!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("================================", ConsoleColor.DarkRed);
                    Console.WriteLine();
                    goto TeacherIdDesc;
                }

                var groupFields = _groupFieldRepository.GetAll();
                foreach (var groupField in groupFields)
                {
                    ConsoleHelper.WriteWithColor("-----------------------===-------------------------------", ConsoleColor.DarkCyan);
                    ConsoleHelper.WriteWithColor($"Group Field Id: {groupField.Id}, Name: {groupField.Name}", ConsoleColor.Cyan);
                    ConsoleHelper.WriteWithColor("--------------------------===----------------------------", ConsoleColor.DarkCyan);
                    Console.WriteLine();


                }

            GroupFieldIdDesc:
                ConsoleHelper.WriteWithColor("---------------------", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("Enter group field id:", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("---------------------", ConsoleColor.DarkCyan);
                Console.WriteLine();

                int id;
                isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("===============================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("Group id is not correct format!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("===============================", ConsoleColor.DarkRed);
                    Console.WriteLine();
                    goto GroupFieldIdDesc;

                }
                var dbGroupField = _groupFieldRepository.Get(id);
                if (dbGroupField == null)
                {
                    ConsoleHelper.WriteWithColor("===================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("There is no group field in this id!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("===================================", ConsoleColor.DarkRed);
                    Console.WriteLine();

                    goto GroupFieldIdDesc;
                }


                group = new Group
                {
                    Name = name,
                    MaxSize = maxSize,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreatedBy = admin.Username,
                    Teacher = dbTeacher,
                    Field = dbGroupField
                };
                dbTeacher.Groups.Add(group);
                dbGroupField.Groups.Add(group);
                _groupRepository.Add(group);
                ConsoleHelper.WriteWithColor("=================================", ConsoleColor.DarkGreen);
                ConsoleHelper.WriteWithColor($"Group successfully created with: \n ****************\n Name: {group.Name} \n ****************\n Max size: {group.MaxSize} \n ****************\n Start date: {group.StartDate.ToShortTimeString()} \n ****************\n End date: {group.EndDate.ToShortTimeString()} \n ****************\n ",
                    ConsoleColor.Green);
                ConsoleHelper.WriteWithColor("=================================", ConsoleColor.DarkGreen);

            }

        }
        public void Update()
        {
            GetAll();
        EnterGroupsDesc:
            ConsoleHelper.WriteWithColor("----------------------------------", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("Enter group (1) - Id or (2) - Name", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("----------------------------------", ConsoleColor.DarkCyan);
            Console.WriteLine();

            int number;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("Imputed number is not correct format", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                Console.WriteLine();

                goto EnterGroupsDesc;
            }

            if (!(number == 1 || number == 2))
            {
                ConsoleHelper.WriteWithColor("=============================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("Imputed number is not correct", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("=============================", ConsoleColor.DarkRed);
                Console.WriteLine();

                goto EnterGroupsDesc;
            }

            if (number == 1)
            {
                ConsoleHelper.WriteWithColor("--------------", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("Enter group Id", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("--------------", ConsoleColor.DarkCyan);

                int id;
                isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("=================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("Imputed Id is not correct format!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("=================================", ConsoleColor.DarkRed);
                    goto EnterGroupsDesc;

                }

                var group = _groupRepository.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("=================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("There is no any group in this Id!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("==================================", ConsoleColor.DarkRed);
                    Console.WriteLine();
                }
                ConsoleHelper.WriteWithColor("--------------", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("Enter new name", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("--------------", ConsoleColor.DarkCyan);
                string name = Console.ReadLine();
                Console.WriteLine();

            MaxSizeDesc:

                ConsoleHelper.WriteWithColor("------------------", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("Enter new max size", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("------------------", ConsoleColor.DarkCyan);

                int maxSize;
                isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);


                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("Max size is not correct format", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                    Console.WriteLine();
                    goto MaxSizeDesc;
                }

            StartDateDesc:
                ConsoleHelper.WriteWithColor("----------------", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("Enter start date", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("----------------", ConsoleColor.DarkCyan);
                DateTime startDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out startDate);
                Console.WriteLine();

                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("Start date is not correct format", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("================================", ConsoleColor.DarkRed);
                    Console.WriteLine();
                    goto StartDateDesc;
                }

            EndDateDesc:
                ConsoleHelper.WriteWithColor("--------------", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("Enter end date", ConsoleColor.Cyan);
                ConsoleHelper.WriteWithColor("--------------", ConsoleColor.DarkCyan);


                DateTime endDate;
                isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out endDate);

                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("End date is not correct format", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);

                    goto EndDateDesc;
                }
                ConsoleHelper.WriteWithColor("*********************************************", ConsoleColor.DarkGreen);
                ConsoleHelper.WriteWithColor($"Group: {group.Name} is successfully updated!", ConsoleColor.Green);
                ConsoleHelper.WriteWithColor("*********************************************", ConsoleColor.DarkGreen);


                group.Name = name;
                group.MaxSize = maxSize;
                group.StartDate = startDate;
                group.EndDate = endDate;
                _groupRepository.Update(group);
            }


        }
        public void Delete()
        {
            GetAll();
        IdDescription:
            ConsoleHelper.WriteWithColor("========", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("Enter Id", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("========", ConsoleColor.DarkCyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine();

            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("========================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("Id is not correct format", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("========================", ConsoleColor.DarkRed);
                Console.WriteLine();
                goto IdDescription;

            }

            var dbGroup = _groupRepository.Get(id);
            if (dbGroup == null)
            {
                ConsoleHelper.WriteWithColor("================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("There is no any group in this id", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("================================", ConsoleColor.DarkRed);
                Console.WriteLine();
            }
            else
            {
                foreach (var student in dbGroup.Students)
                {
                    student.Group = null;
                    _studentRepository.Update(student);
                }

                _groupRepository.Delete(dbGroup);
                ConsoleHelper.WriteWithColor("**********************************************", ConsoleColor.DarkGreen);
                ConsoleHelper.WriteWithColor($"Group {dbGroup.Name} is successfully deleted!", ConsoleColor.Green);
                ConsoleHelper.WriteWithColor("**********************************************", ConsoleColor.DarkGreen);
                Console.WriteLine();

            }
        }
        public void Exit()
        {
        AreYouSureDescription:
            ConsoleHelper.WriteWithColor("==========================", ConsoleColor.DarkRed);
            ConsoleHelper.WriteWithColor("Are you sure? -- Y or N --", ConsoleColor.Red);
            ConsoleHelper.WriteWithColor("==========================", ConsoleColor.DarkRed);


            char decision;
            bool isSucceeded = char.TryParse(Console.ReadLine().ToLower(), out decision);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("==========================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("Wrong format! Try again...", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("==========================", ConsoleColor.DarkRed);
                Console.WriteLine();

                goto AreYouSureDescription;
            }

            if (!(decision == 'y' || decision == 'n'))
            {
                ConsoleHelper.WriteWithColor("========================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("Your choice is not correct! Try again...", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("========================================", ConsoleColor.DarkRed);
                Console.WriteLine();
                goto AreYouSureDescription;
            }

            if (decision == 'y')
                return;
        }







    }

}
