
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Core.Constants;
using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using Data;
using Data.Contexts;
using Data.Contexts.Repositories.Concrete;
using Presentation.Services;


namespace Presentation

{
    public static class Program
    {
        
        private readonly static GroupService _groupService;
        private readonly static StudentService _studentService;
        private readonly static AdminService _adminService;
        private readonly static TeacherService _teacherService;
        private readonly static GroupFieldService _groupFieldService;
        private static Admin admin;
        static Program()
        {
            DbInitializer.SeedAdmins();
            _adminService = new AdminService();
            admin = _adminService.Authorize();
            _groupService = new GroupService(admin);
            _studentService = new StudentService(admin);
            _teacherService = new TeacherService();
            _groupFieldService = new GroupFieldService();
        }

        static void Main()
        {
            o.a();
        AuthorizeDescription: if (admin is null)
            {
                admin = _adminService.Authorize();
                _groupService.admin = admin;
            }
            if (admin != null)
            {
                ConsoleHelper.WriteWithColor($"Welcome {admin.Username}", ConsoleColor.Blue);

                while (true)
                {
                MainMenuDesc:
                    Console.Beep();
                    ConsoleHelper.WriteWithColor("█▓▒░(1) Groups               ░░░░░▒▓█", ConsoleColor.DarkMagenta);
                    ConsoleHelper.WriteWithColor("█▓▒░(2) Students             ░░░░░▒▓█", ConsoleColor.DarkMagenta);
                    ConsoleHelper.WriteWithColor("█▓▒░(3) Teachers             ░░░░░▒▓█", ConsoleColor.DarkMagenta);
                    ConsoleHelper.WriteWithColor("█▓▒░(4) Group Fields         ░░░░░▒▓█", ConsoleColor.DarkMagenta);


                    ConsoleHelper.WriteWithColor("█▓▒░(0) Return to login page ░░░░░▒▓█", ConsoleColor.DarkMagenta);
                    int number;
                    bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                        ConsoleHelper.WriteWithColor("Imputed number is not correct format", ConsoleColor.Red);
                        ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                        ConsoleHelper.WriteWithColor("", ConsoleColor.Red);
                        goto MainMenuDesc;
                    }

                    else
                    {
                        switch (number)
                        {
                            case (int)MainMenuOptions.Groups:
                                Console.Beep();
                                while (true)
                                {

                                GroupDescription:
                                    ConsoleHelper.WriteWithColor("█▓▒░(1) Create Group      ░░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(2) Update Group       ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(3) Delete Group       ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(4) Get ALL            ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(5) Get Group by Id     ░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(6) Get Group by Name     ░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(7) Get Groups by Teacher ░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(8) Get Groups by Field   ░▒▓█", ConsoleColor.DarkMagenta);

                                    ConsoleHelper.WriteWithColor("█▓▒░(0) Back to main menu     ░▒▓█", ConsoleColor.DarkMagenta);

                                    ConsoleHelper.WriteWithColor("█▓▒▒▒░░  Select Option     ░░▒▒▒▓█", ConsoleColor.Cyan);


                                    isSucceeded = int.TryParse(Console.ReadLine(), out number); // тот самый метод тут 
                                    if (!isSucceeded)
                                    {
                                        Console.Beep(2000, 100);
                                        Console.Beep(1000, 150);
                                        ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                                        ConsoleHelper.WriteWithColor("Imputed number is not correct format", ConsoleColor.Red);
                                        ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                                        ConsoleHelper.WriteWithColor("", ConsoleColor.Red);
                                    }




                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)GroupOptions.CreateGroup:
                                                _groupService.Create();
                                                break;

                                            case (int)GroupOptions.UpdateGroup:
                                                _groupService.Update();
                                                break;

                                            case (int)GroupOptions.DeleteGroup:
                                                _groupService.Delete();
                                                break;

                                            case (int)GroupOptions.GetAllGroups:
                                                _groupService.GetAll();
                                                break;


                                            case (int)GroupOptions.GetGroupById:
                                                _groupService.GetGroupById();
                                                break;

                                            case (int)GroupOptions.GerGroupByName:
                                                _groupService.GetGroupByName();
                                                break;
                                            case (int)GroupOptions.GetAllGroupsByTeacher:
                                                _groupService.GetAllGroupsByTeacher();
                                                break;
                                            case (int)GroupOptions.GetAllGroupsByGroupField:
                                                _groupService.GetAllGroupsByField();
                                                break;
                                            case (int)GroupOptions.BackToMainMenu:
                                                goto MainMenuDesc;

                                            default:
                                                ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                                ConsoleHelper.WriteWithColor("Imputed number does not exist!", ConsoleColor.Red);
                                                ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                                ConsoleHelper.WriteWithColor("", ConsoleColor.Red);
                                                goto GroupDescription;

                                        }

                                    }
                                }
                            case (int)MainMenuOptions.Students:
                                while (true)
                                {


                                StudentDescription: 
                                    ConsoleHelper.WriteWithColor("█▓▒░(1) Create Student          ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(2) Update Student          ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(3) Delete Student          ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(4) Get all Students        ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(5) Get all Students by ID  ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(0) Back to Main Menu       ░░░░▒▓█", ConsoleColor.DarkMagenta);


                                    isSucceeded = int.TryParse(Console.ReadLine(), out number); // тот самый метод тут 
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                                        ConsoleHelper.WriteWithColor("Imputed number is not correct format", ConsoleColor.Red);
                                        ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                                        ConsoleHelper.WriteWithColor("", ConsoleColor.Red);
                                    }

                                    switch (number)
                                    {
                                        case (int)StudentOptions.CreateStudent:
                                            _studentService.Create();
                                            break;

                                        case (int)StudentOptions.UpdateStudent:
                                            _studentService.Update();
                                            break;

                                        case (int)StudentOptions.DeleteStudent:
                                            _studentService.Delete();
                                            break;

                                        case (int)StudentOptions.GetAllStudents:
                                            _studentService.GetAll();
                                            break;

                                        case (int)StudentOptions.GetAllStudentsByGroup:
                                            _studentService.GetAllByGroup();
                                            break;

                                        case (int)StudentOptions.BackToMainMenu:
                                            goto MainMenuDesc;
                                            break;

                                        default:
                                            ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                            ConsoleHelper.WriteWithColor("Imputed number does not exist!", ConsoleColor.Red);
                                            ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                            ConsoleHelper.WriteWithColor("", ConsoleColor.Red);
                                            goto StudentDescription;

                                    }
                                }
                            case (int)MainMenuOptions.Teachers:
                                while (true)
                                {
                                StudentDescription: ConsoleHelper.WriteWithColor("█▓▒░(1) Create Teacher          ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(2) Update Teacher          ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(3) Delete Teacher          ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(4) Get all Teachers        ░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(0) Back to Main Menu       ░░░░▒▓█", ConsoleColor.DarkMagenta);



                                    isSucceeded = int.TryParse(Console.ReadLine(), out number); // тот самый метод тут 
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                                        ConsoleHelper.WriteWithColor("Imputed number is not correct format", ConsoleColor.Red);
                                        ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                                        ConsoleHelper.WriteWithColor("", ConsoleColor.Red);
                                    }
                                    switch (number)
                                    {
                                        case (int)TeacherOptions.CreateTeacher:
                                            _teacherService.Create();
                                            break;

                                        case (int)TeacherOptions.UpdateTeacher:
                                            _studentService.Update();
                                            break;

                                        case (int)TeacherOptions.DeleteTeacher:
                                            _teacherService.Delete();
                                            break;

                                        case (int)TeacherOptions.GetAllTeachers:
                                            _teacherService.GetAll();
                                            break;

                                        case (int)StudentOptions.BackToMainMenu:
                                            goto MainMenuDesc;


                                        default:
                                            ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                            ConsoleHelper.WriteWithColor("Imputed number does not exist!", ConsoleColor.Red);
                                            ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                            ConsoleHelper.WriteWithColor("", ConsoleColor.Red);
                                            goto StudentDescription;

                                    }
                                }
                            case (int)MainMenuOptions.GroupFields:
                                while (true)
                                {
                                GroupFieldDesc:
                                    ConsoleHelper.WriteWithColor("█▓▒░(1) Create Group Field      ░░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(2) Delete Group Field      ░░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(3) GetAll Group Fields     ░░░░░▒▓█", ConsoleColor.DarkMagenta);
                                    ConsoleHelper.WriteWithColor("█▓▒░(4) Update Group Field      ░░░░░▒▓█", ConsoleColor.DarkMagenta);

                                    ConsoleHelper.WriteWithColor("█▓▒░(0) Back to Main Menu       ░░░░░▒▓█", ConsoleColor.DarkMagenta);

                                    int selection;
                                    isSucceeded = int.TryParse(Console.ReadLine(), out selection);
                                    if (!isSucceeded)
                                    {

                                        ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);
                                        ConsoleHelper.WriteWithColor("Imputed number is not correct format!", ConsoleColor.Red);
                                        ConsoleHelper.WriteWithColor("====================================", ConsoleColor.DarkRed);

                                        goto GroupFieldDesc;
                                    }
                                    else
                                    {
                                        switch (selection)
                                        {
                                            case (int)GroupFieldOptions.AddGroupField:
                                                _groupFieldService.Create();
                                                break;
                                            case (int)GroupFieldOptions.RemoveGroupField:
                                                _groupFieldService.Remove();
                                                break;
                                            case (int)GroupFieldOptions.GetAllGroupFields:
                                                _groupFieldService.GetAll();
                                                break;
                                            case (int)GroupFieldOptions.UpdateGroupField:
                                                _groupFieldService.Update();
                                                break;
                                            case (int)GroupFieldOptions.BackToMainMenu:
                                                goto MainMenuDesc;
                                            default:

                                                ConsoleHelper.WriteWithColor("================", ConsoleColor.DarkRed);
                                                ConsoleHelper.WriteWithColor("Wrong selection!", ConsoleColor.Red);
                                                ConsoleHelper.WriteWithColor("================", ConsoleColor.DarkRed);

                                                break;

                                        }
                                    }




                                }
                            case (int)MainMenuOptions.Logout:
                                admin = null;
                                goto AuthorizeDescription;
                            default:

                                ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                ConsoleHelper.WriteWithColor("Imputed number does not exist!", ConsoleColor.Red);
                                ConsoleHelper.WriteWithColor("==============================", ConsoleColor.DarkRed);
                                ConsoleHelper.WriteWithColor("", ConsoleColor.Red);
                                goto MainMenuDesc;










                        }

                    }

                }

            }








        }

    }

}