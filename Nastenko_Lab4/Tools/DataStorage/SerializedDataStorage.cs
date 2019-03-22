using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Managers;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.DataStorage
{
    internal class SerializedDataStorage:IDataStorage
    {
        private readonly List<User> _users;

        internal SerializedDataStorage()
        {
            try
            {
                _users = SerializationManager.Deserialize<List<User>>(FileFolderHelper.StorageFilePath);
                _users.Clear();
                Random rnd = new Random();
                string[] lastNames = { "Zadontseva", "Nastenko", "Shevchuk", "Adamova", "Pilipets", "Kravchuk", "Ostapenko", "Kolpakova", "Voronchuk", "Chernaenko" };
                string[] firstNames = { "Lera", "Karina", "Nastya", "Kristina", "Taras", "Dima", "Kiril", "Yana", "Nazar", "Danil" };
                for (int i = 0; i < 50; i++)
                    _users.Add(new User(lastNames[rnd.Next(0, 10)], firstNames[rnd.Next(0, 10)], $"user{i}@gmail.com", new DateTime(rnd.Next(DateTime.Today.Year - 135, DateTime.Today.Year - 1), rnd.Next(1, 13), rnd.Next(1, 30))));


            }
            catch (FileNotFoundException)
            {
                _users = new List<User>();
            }
        }
        
        public bool UserExists(string login)
        {
            return _users.Exists(u => u.Login == login);
        }

        public User GetUserByLogin(string login)
        {
            return _users.FirstOrDefault(u => u.Login == login);
        }

        public void AddUser(User user)
        {
            _users.Add(user);
            SaveChanges();
        }

        public List<User> UsersList
        {
            get { return _users.ToList(); }
        }

        private void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }
        
    }
}

