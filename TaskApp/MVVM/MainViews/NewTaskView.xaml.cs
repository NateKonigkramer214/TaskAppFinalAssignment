using TaskApp.MVVM.ViewModels; // Import ViewModel namespace
using TaskApp.MVVM.Models; // Import Models namespace
using System.Linq; // Import LINQ functionalities
using System; // Import System namespace

namespace TaskApp.MVVM.Views
{
    public partial class NewTaskView : ContentPage
    {
        public NewTaskView()
        {
            InitializeComponent(); // Initialize the NewTaskView
        }

        // Method for handling 'Add Task' button click event
        private async void btnAddTask_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as NewTaskViewModel; // Get the binding context as NewTaskViewModel

            // Get the selected category from the view model
            var selectedCategory = vm.Categories.Where(x => x.IsSelected).FirstOrDefault();

            if (selectedCategory != null)
            {
                // Create a new task with details from the view model
                var task = new MyTask
                {
                    TaskName = vm.Task,
                    CategoryId = selectedCategory.Id,
                    TaskColor = selectedCategory.Color
                };

                vm.Tasks.Add(task); // Add the new task to the tasks list in the view model

                // Manually trigger the UpdateData method in MainViewModel
                var mainViewModel = App.Current.MainPage.BindingContext as MainViewModel;
                mainViewModel.UpdateData(); // Update the data

                await Navigation.PopAsync(); // Navigate back to the previous page
            }
            else
            {
                // Show an alert if no category is selected
                await DisplayAlert("Invalid Selection", "You must select a category", "OK");
            }
        }

        // Method for handling 'Add Category' button click event
        private async void btnAddCategory_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as NewTaskViewModel; // Get the binding context as NewTaskViewModel

            // Prompt the user to enter a new category name
            string category = await DisplayPromptAsync("New Category", "Write the category name", maxLength: 50, keyboard: Keyboard.Text);

            if (!string.IsNullOrEmpty(category))
            {
                var random = new Random(); // Create a Random instance

                // Create a new category with a randomly generated color and provided name
                var newCategory = new Category
                {
                    Id = vm.Categories.Max(x => x.Id) + 1,
                    Color = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)).ToHex(),
                    CategoryName = category
                };
                vm.Categories.Add(newCategory); // Add the new category to the categories list in the view model

                // Manually trigger the UpdateData method in MainViewModel
                var mainViewModel = App.Current.MainPage.BindingContext as MainViewModel;
                mainViewModel.UpdateData(); // Update the data
            }
        }
    }
}
