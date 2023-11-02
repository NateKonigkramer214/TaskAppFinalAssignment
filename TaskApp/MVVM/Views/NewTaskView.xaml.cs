using TaskApp.MVVM.ViewModels;
using TaskApp.MVVM.Models;
using System.Linq;
using System;


namespace TaskApp.MVVM.Views
{
    public partial class NewTaskView : ContentPage
    {
        public NewTaskView()
        {
            InitializeComponent();
        }

        private async void btnAddTask_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as NewTaskViewModel;
            var selectedCategory = vm.Categories.Where(x => x.IsSelected).FirstOrDefault();

            if (selectedCategory != null)
            {
                var task = new MyTask
                {
                    TaskName = vm.Task,
                    CategoryId = selectedCategory.Id,
                    TaskColor = selectedCategory.Color
                };

                vm.Tasks.Add(task);

                // Manually call UpdateData from MainViewModel
                var mainViewModel = App.Current.MainPage.BindingContext as MainViewModel;
                mainViewModel.UpdateData();

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Invalid Selection", "You must select a category", "OK");
            }
        }

        private async void btnAddCategory_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as NewTaskViewModel;

            string category = await DisplayPromptAsync("New Category", "Write the category name", maxLength: 50, keyboard: Keyboard.Text);

            if (!string.IsNullOrEmpty(category))
            {
                var random = new Random(); // Create a Random instance

                var newCategory = new Category
                {
                    Id = vm.Categories.Max(x => x.Id) + 1,
                    Color = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)).ToHex(), // Use 'random' to generate colors
                    CategoryName = category
                };
                vm.Categories.Add(newCategory);

                // Manually call UpdateData from MainViewModel
                var mainViewModel = App.Current.MainPage.BindingContext as MainViewModel;
                mainViewModel.UpdateData();
            }
        }
    }
}
