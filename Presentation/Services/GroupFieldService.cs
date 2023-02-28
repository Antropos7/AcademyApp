using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
using Data.Contexts;
using Data.Contexts.Repositories.Abstract;
using Data.Contexts.Repositories.Concrete;

namespace Presentation.Services
{
    public class GroupFieldService
    {
        private readonly GroupFieldRepository _groupFieldRepository;
        public GroupFieldService()
        {
            _groupFieldRepository = new GroupFieldRepository();
        }
        public void Create()
        {
        GroupFieldDesc:

            ConsoleHelper.WriteWithColor("----------------", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("Enter Field Name", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("----------------", ConsoleColor.DarkCyan);
            Console.WriteLine();
            string name = Console.ReadLine();

            var groupField = _groupFieldRepository.GetByName(name);
            {
                if (groupField != null)
                {

                    ConsoleHelper.WriteWithColor("=======================================", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteWithColor("This group field name is already exist!", ConsoleColor.Red);
                    ConsoleHelper.WriteWithColor("=======================================", ConsoleColor.DarkRed);
                    Console.WriteLine();
                    goto GroupFieldDesc;

                }

                groupField = new GroupField
                {
                    Name = name,
                };
                _groupFieldRepository.Add(groupField);
                ConsoleHelper.WriteWithColor("*****************************************", ConsoleColor.DarkGreen);
                ConsoleHelper.WriteWithColor($"{groupField.Name} is successfully added!", ConsoleColor.Green);
                ConsoleHelper.WriteWithColor("*****************************************", ConsoleColor.DarkGreen);
                Console.WriteLine();

            }
        }

        public void Update()
        {
            _groupFieldRepository.GetAll();
        FieldIdDesc:
            ConsoleHelper.WriteWithColor("--------------", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("Enter group id", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("--------------", ConsoleColor.DarkCyan);
            Console.WriteLine();

            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {

                ConsoleHelper.WriteWithColor("======================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("Wrong field id format!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("======================", ConsoleColor.DarkRed);

                goto FieldIdDesc;
            }

            var field = _groupFieldRepository.Get(id);
            if (field == null)
            {

                ConsoleHelper.WriteWithColor("===================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("There is no group field in this id!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("===================================", ConsoleColor.DarkRed);
                Console.WriteLine();
                goto FieldIdDesc;
            }

            ConsoleHelper.WriteWithColor("--------------------------", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("Enter group field new name", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("--------------------------", ConsoleColor.DarkCyan);
            Console.WriteLine();

            string name = Console.ReadLine();


            field.Name = name;

            ConsoleHelper.WriteWithColor("**************************************************", ConsoleColor.DarkGreen);
            ConsoleHelper.WriteWithColor($"Field name: {field.Name} is successfully updated!", ConsoleColor.Green);
            ConsoleHelper.WriteWithColor("**************************************************", ConsoleColor.DarkGreen);
            Console.WriteLine();


            _groupFieldRepository.Update(field);

        }

        public void GetAll()
        {
            var groupFields = _groupFieldRepository.GetAll();
            if (groupFields.Count == 0)
            {

                ConsoleHelper.WriteWithColor("========================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("These is no group field!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("========================", ConsoleColor.DarkRed);
                Console.WriteLine();
            }

            else
            {
                foreach (var groupField in groupFields)
                {

                    ConsoleHelper.WriteWithColor("------------------------------------------------------", ConsoleColor.DarkCyan);
                    ConsoleHelper.WriteWithColor($"Group field id {groupField.Id} Name: {groupField.Name}", ConsoleColor.Cyan);
                    ConsoleHelper.WriteWithColor("------------------------------------------------------", ConsoleColor.DarkCyan);

                    Console.WriteLine();
                }                                                     
            }
        }

        public void Remove()
        {
            GetAll();

        DeleteDesc:

            ConsoleHelper.WriteWithColor("--------------------------------", ConsoleColor.DarkCyan);
            ConsoleHelper.WriteWithColor("Select group field for deleting:", ConsoleColor.Cyan);
            ConsoleHelper.WriteWithColor("--------------------------------", ConsoleColor.DarkCyan);
            Console.WriteLine();

            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {

                ConsoleHelper.WriteWithColor("==================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("Group field is not correct format!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("==================================", ConsoleColor.DarkRed);
                Console.WriteLine();
                goto DeleteDesc;

            }
            var groupField = _groupFieldRepository.Get(id);
            if (groupField is null)
            {
                ConsoleHelper.WriteWithColor("===================================", ConsoleColor.DarkRed);
                ConsoleHelper.WriteWithColor("There is no group field in this id!", ConsoleColor.Red);
                ConsoleHelper.WriteWithColor("===================================", ConsoleColor.DarkRed);
                Console.WriteLine();

                goto DeleteDesc;
            }
            _groupFieldRepository.Delete(groupField);
            ConsoleHelper.WriteWithColor("*******************************************", ConsoleColor.DarkGreen);
            ConsoleHelper.WriteWithColor($"{groupField.Name} is successfully deleted!", ConsoleColor.Green);
            ConsoleHelper.WriteWithColor("*******************************************", ConsoleColor.DarkGreen);
            Console.WriteLine();

        }
    }
}
