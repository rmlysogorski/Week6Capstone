using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week6Capstone.Models
{
    public class DAO
    {
        private static TaskListDbEntities ORM = new TaskListDbEntities();

        public static List<User> GetUsersAsList()
        {
            return ORM.Users.ToList();
        }

        public static int FindUserInDb(UserLogin thisUser)
        {
            User found = ORM.Users.ToList().Find(u => u.Email == thisUser.Email);
            if (found != null)
            {
                return found.Id;
            }
            else
            {
                return -1;
            }

        }

        public static bool AddUserToDb(User newUser)
        {
            User found = ORM.Users.ToList().Find(u => u.Email == newUser.Email);
            if (found == null)
            {
                ORM.Users.Add(newUser);
                ORM.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public static List<Task> GetTasksAsList(int UserId)
        {
            return ORM.Tasks.ToList().FindAll(t => t.UserId == UserId).ToList();
        }

        public static void AddTaskToDb(Task newTask)
        {
            ORM.Tasks.Add(newTask);
            ORM.SaveChanges();
        }

        public static void DeleteTaskFromDb(int TaskId)
        {
            Task deleteMe = ORM.Tasks.Find(TaskId);
            ORM.Tasks.Remove(deleteMe);
            ORM.SaveChanges();
        }

        public static Task FindTaskFromDb(int TaskId)
        {
            return ORM.Tasks.Find(TaskId);

        }

        public static void MarkCompleted(int TaskId)
        {
            Task original = ORM.Tasks.Find(TaskId);
            if (original.Complete == true)
            {
                original.Complete = false;
            }
            else
            {
                original.Complete = true;
            }
            ORM.SaveChanges();
        }

        public static List<Task> SearchDesc(string queryString, int UserId)
        {
            if (UserId != -1)
            {
                return GetTasksAsList(UserId).ToList().FindAll(x => x.Description.ToUpper().Contains(queryString.ToUpper())).ToList();

            }
            else
            {
                return new List<Task>();
            }
        }
    }
}