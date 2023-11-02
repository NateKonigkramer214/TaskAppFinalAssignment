using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TaskApp.MVVM.Models;
using PropertyChanged;
using System.Linq;

namespace TaskApp.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }

        public MainViewModel()
        {
            FileData();
            Tasks.CollectionChanged += Tasks_CollectionChanged;

            // Initialize the Tasks collection within each Category
            foreach (var category in Categories)
            {
                category.Tasks = new ObservableCollection<MyTask>(Tasks.Where(task => task.CategoryId == category.Id));
                category.UpdateTotalTasks();
            }
        }

        private void Tasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Handle tasks collection changes
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (MyTask task in e.NewItems)
                {
                    Category category = Categories.FirstOrDefault(c => c.Id == task.CategoryId);
                    if (category != null)
                    {
                        category.Tasks.Add(task);
                        category.PendingTasks = category.Tasks.Count(t => !t.Completed);
                        category.Percentage = (float)category.Tasks.Count(t => t.Completed) / category.Tasks.Count;
                        category.UpdateTotalTasks();
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (MyTask task in e.OldItems)
                {
                    Category category = Categories.FirstOrDefault(c => c.Id == task.CategoryId);
                    if (category != null)
                    {
                        category.Tasks.Remove(task);
                        category.PendingTasks = category.Tasks.Count(t => !t.Completed);
                        category.Percentage = (float)category.Tasks.Count(t => t.Completed) / category.Tasks.Count;
                        category.UpdateTotalTasks();
                    }
                }
            }
        }


        private void FileData()
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
                    Color = "#008000"
                },

                new Category
                {
                    Id = 3,
                    CategoryName = "Groceries",
                    Color = "#007BFF"
                },
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
            UpdateData();
        }

        public void UpdateData()
        {
            foreach (var c in Categories)
            {
                var tasks = from t in Tasks
                            where t.CategoryId == c.Id
                            select t;

                var completed = from t in tasks
                                where t.Completed == true
                                select t;

                var noCompleted = from t in tasks
                                  where t.Completed == false
                                  select t;

                c.PendingTasks = noCompleted.Count();
                c.Percentage = (float)completed.Count() / (float)tasks.Count();
            }

            foreach (var t in Tasks)
            {
                var catColor =
                    (
                        from c in Categories
                        where c.Id == t.CategoryId
                        select c.Color
                    ).FirstOrDefault();
                t.TaskColor = catColor;
            }
        }
    }
}