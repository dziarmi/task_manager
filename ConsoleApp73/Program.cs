using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.Json;

namespace ConsoleApp73
{
    internal class Program
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        static void Main(string[] args)
        {
            bool running = true;

            while(running)
            {
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. View tasks list");
                Console.WriteLine("3. Mark task as completed");
                Console.WriteLine("4. Delete task");
                Console.WriteLine("5. Save / Load from file");
                Console.WriteLine("6. Exit");
                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ShowList();
                        break;
                    case "3":
                        MarkAsDone();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "5":
                        //save / load from file
                        break;
                    case "6":
                        Console.WriteLine("Goodbye!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void AddTask()
        {
            Console.Write("Add task description: ");
            string desc = Console.ReadLine();

            bool taskExists = false;

            foreach(TaskItem t in tasks)
            {
                if(t.Description == desc)
                {
                    taskExists = true;
                    break;
                }
            }

            if(taskExists)
            {
                Console.Write("Task with this description already exists.");
            }
            else
            {
                Console.WriteLine("Task added successfully!");
                TaskItem newTask = new TaskItem(desc);
                tasks.Add(newTask);
            }
            Console.WriteLine();
        }

        static void ShowList()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
            }
            else
            {
                int index = 1;
                foreach (TaskItem t in tasks)
                {
                    Console.WriteLine(index + ". " + t.Description + " | status: " + t.Status());
                    index++;
                }
            }
            Console.WriteLine();
        }

        static void MarkAsDone()
        {
            ShowList();
            Console.WriteLine("Task you want to mark as completed: ");
            int completedTask = Convert.ToInt32(Console.ReadLine());

            if(tasks.Count < completedTask || tasks.Count <= 0)
            {
                Console.WriteLine("Invalid task number.");
            }
            else
            {
                Console.WriteLine("Task marked as completed.");
                tasks[completedTask - 1].TaskDone();
            }
        }

        static void DeleteTask()
        {
            ShowList();

            Console.Write("Task you want to delete: ");
            int deletedTask = Convert.ToInt32(Console.ReadLine());

            if (tasks.Count > deletedTask)
            {
                Console.WriteLine("Invalid task number.");
            }
            else {
                Console.WriteLine("Task deleted successfully!");
                tasks.RemoveAt(deletedTask - 1);
            }
        }

        class TaskItem
        {
            public string Description { get; private set; }
            private bool Completed { get; set; }

            public TaskItem(string desc)
            {
                Description = desc;
                Completed = false;
            }

            public void TaskDone()
            {
                Completed = true;
            }

            public void TaskUndone()
            {
                Completed = false;
            }

            public string Status()
            {
                if (Completed)
                {
                    return "Completed";
                }
                else
                {
                    return "In progress";
                }
            }
        }
    }
}
