using System.Collections.ObjectModel;
using TaskApp.MVVM.Models;
using PropertyChanged;
using System;

public class NewTaskViewModel
{
    public event EventHandler<TaskAddedEventArgs> TaskAdded;

    public string Task { get; set; }
    public ObservableCollection<MyTask> Tasks { get; set; }
    public ObservableCollection<Category> Categories { get; set; }
    public Category SelectedCategory { get; set; }

    public NewTaskViewModel()
    {
        Categories = new ObservableCollection<Category>()
        {
                new Category
                {
                    Id = 1,
                    CategoryName = "Finance",
                    Color = "#FFC436"
                },

                new Category
                {
                    Id = 2,
                    CategoryName = "Inventory Tracking",
                    Color = "#0174BE"
                },

                new Category
                {
                    Id = 3,
                    CategoryName = "Shoping",
                    Color = "#0C356A"
                }
        };

        Tasks = new ObservableCollection<MyTask>()
            {
                new MyTask
            {
                TaskName = "Weekly Sales Report",
                Completed = false,
                CategoryId = 1,
            },
            new MyTask
            {
                TaskName = "Taking Money to Bank",
                Completed = false,
                CategoryId = 1,
            },
            new MyTask
            {
                TaskName = "Emptying registers",
                Completed = false,
                CategoryId = 1,
            },
            new MyTask
            {
                TaskName = "Checking stock levels",
                Completed = false,
                CategoryId = 2,
            },
            new MyTask
            {
                TaskName = "Checking fridge is stocked up",
                Completed = false,
                CategoryId = 2,
            },
            new MyTask
            {
                TaskName = "Buy more dough",
                Completed = false,
                CategoryId = 3,
            },
            new MyTask
            {
                TaskName = "Buy more milk",
                Completed = false,
                CategoryId = 3,
            }
            };
    
    }

    public class TaskAddedEventArgs : EventArgs
    {
        public MyTask NewTask { get; }

        public TaskAddedEventArgs(MyTask newTask)
        {
            NewTask = newTask;
        }
    }

   public void AddTask()
    {
        if (SelectedCategory != null)
        {
            var newTask = new MyTask { TaskName = Task, Completed = false, CategoryId = SelectedCategory.Id };
            SelectedCategory.AddTask(newTask);
            TaskAdded?.Invoke(this, new TaskAddedEventArgs(newTask));
            Task = string.Empty;
        }
    }
}
