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
                CategoryName = "Cleaning",
                Color = "#ff0000"
            },
            new Category
            {
                Id = 2,
                CategoryName = "Work/Uni",
                Color = "#007BFF"
            },
            new Category
            {
                Id = 3,
                CategoryName = "Groceries",
                Color = "#008000"
            }
        };

        Tasks = new ObservableCollection<MyTask>()
        {
            new MyTask
            {
                TaskName = "Dishes",
                Completed = false,
                CategoryId = 1,
            },
            new MyTask
            {
                TaskName = "Vacuum",
                Completed = false,
                CategoryId = 1,
            },
            new MyTask
            {
                TaskName = "Study for exam",
                Completed = false,
                CategoryId = 2,
            },
            new MyTask
            {
                TaskName = "Finish Working on UI",
                Completed = false,
                CategoryId = 2,
            },
            new MyTask
            {
                TaskName = "Buy Flour",
                Completed = false,
                CategoryId = 3,
            },
            new MyTask
            {
                TaskName = "Buy Protein Powder",
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
