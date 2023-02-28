using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Data.Contexts.Repositories.Concrete;

namespace Presentation.Services
{
    public class AdminService
    {
        private readonly AdminRepository _adminRepository;
        public AdminService()
        {
            _adminRepository = new AdminRepository();
        }
        public Admin Authorize()
        {
            ConsoleHelper.WriteWithColor("█▓▒▒▒▒▒░░  Welcome!  ░░▒▒▒▒▒▓█", ConsoleColor.Cyan);

            ConsoleHelper.WriteWithColor("-------------", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("--- Login ---", ConsoleColor.Blue);
            ConsoleHelper.WriteWithColor("-------------", ConsoleColor.DarkCyan);
           

        LoginDescription:
            ConsoleHelper.WriteWithColor("---------------", ConsoleColor.DarkBlue);
            ConsoleHelper.WriteWithColor("Enter username:", ConsoleColor.Blue);
            ConsoleHelper.WriteWithColor("---------------", ConsoleColor.DarkBlue);
            string username = Console.ReadLine();
            

            ConsoleHelper.WriteWithColor("--------------", ConsoleColor.DarkBlue);
            ConsoleHelper.WriteWithColor("Enter password", ConsoleColor.Blue);
            ConsoleHelper.WriteWithColor("--------------", ConsoleColor.DarkBlue);
            string password = Console.ReadLine();
            

            var admin = _adminRepository.GetByUsernameAndPassword(username, password);
            if (admin is null)
            {

                ConsoleHelper.WriteWithColor("***********************************************", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("Username or password is incorrect! Try again...", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("***********************************************", ConsoleColor.DarkRed);
                Console.WriteLine();
                goto LoginDescription;
            }
            return admin;
        }

    }
}
