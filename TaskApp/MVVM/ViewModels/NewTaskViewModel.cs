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
                    CategoryName = "House Chores",
                    Color = "#FFC436"
                },

                new Category
                {
                    Id = 2,
                    CategoryName = "Work",
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
                TaskName = "Washing",
                Completed = false,
                CategoryId = 1,
            },
            new MyTask
            {
                TaskName = "Dusting",
                Completed = false,
                CategoryId = 1,
            },
            new MyTask
            {
                TaskName = "Complete patching",
                Completed = false,
                CategoryId = 2,
            },
            new MyTask
            {
                TaskName = "Finsh Tasker App",
                Completed = false,
                CategoryId = 2,
            },
            new MyTask
            {
                TaskName = "Complete Weather app gif",
                Completed = false,
                CategoryId = 2,
            },
            new MyTask
            {
                TaskName = "Buy Steak",
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
