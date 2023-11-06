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
            // Handle changes in the tasks collection
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // If tasks are added to the collection
                foreach (MyTask task in e.NewItems)
                {
                    // Find the category related to the new task being added
                    Category category = Categories.FirstOrDefault(c => c.Id == task.CategoryId);

                    // If the related category exists
                    if (category != null)
                    {
                        // Add the new task to the category's task collection
                        category.Tasks.Add(task);

                        // Update the count of pending tasks in the category
                        category.PendingTasks = category.Tasks.Count(t => !t.Completed);

                        // Calculate the completion percentage for the category
                        category.Percentage = (float)category.Tasks.Count(t => t.Completed) / category.Tasks.Count;

                        // Update the total tasks count in the category
                        category.UpdateTotalTasks();
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                // If tasks are removed from the collection
                foreach (MyTask task in e.OldItems)
                {
                    // Find the category related to the task being removed
                    Category category = Categories.FirstOrDefault(c => c.Id == task.CategoryId);

                    // If the related category exists
                    if (category != null)
                    {
                        // Remove the task from the category's task collection
                        category.Tasks.Remove(task);

                        // Update the count of pending tasks in the category
                        category.PendingTasks = category.Tasks.Count(t => !t.Completed);

                        // Calculate the completion percentage for the category
                        category.Percentage = (float)category.Tasks.Count(t => t.Completed) / category.Tasks.Count;

                        // Update the total tasks count in the category
                        category.UpdateTotalTasks();
                    }
                }
            }
        }



        private void FileData()
        {
            //Categories_list - names
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
                },
            };
            //List of tasks
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
            UpdateData();
        }

        public void UpdateData()
        {
            // Loop through each category in the Categories collection
            foreach (var c in Categories)
            {
                // Filter tasks based on the current category ID
                var tasks = from t in Tasks
                            where t.CategoryId == c.Id
                            select t;

                // Filter completed tasks within the selected category
                var completed = from t in tasks
                                where t.Completed == true
                                select t;

                // Filter incomplete tasks within the selected category
                var noCompleted = from t in tasks
                                  where t.Completed == false
                                  select t;

                // Set the count of incomplete tasks for the current category
                c.PendingTasks = noCompleted.Count();

                // Calculate the percentage of completed tasks for the current category
                // Percentage is calculated as the ratio of completed tasks to total tasks in the category
                c.Percentage = (float)completed.Count() / (float)tasks.Count();
            }



            foreach (var t in Tasks)
            {
                // For each task in the Tasks collection,
                // find the associated category's color based on the task's CategoryId

                // Retrieve the color of the category associated with the current task
                var catColor =
                    (
                        from c in Categories
                        where c.Id == t.CategoryId
                        select c.Color
                    ).FirstOrDefault();

                // Set the TaskColor property of the current task to the found category color
                t.TaskColor = catColor;
            }
        }
    }
}