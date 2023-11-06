using TaskApp.MVVM.ViewModels; // Import the necessary ViewModel namespace

namespace TaskApp.MVVM.Views
{
    public partial class MainView : ContentPage
    {
        private MainViewModel _mainViewModel = new MainViewModel(); // Instantiate the MainViewModel

        // Constructor for the MainView
        public MainView()
        {
            InitializeComponent(); // Initialize the components
            BindingContext = _mainViewModel; // Set the binding context to the MainViewModel
        }

        // Event handler for the CheckBox's CheckedChanged event
        private void checkBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _mainViewModel.UpdateData(); // Call the UpdateData method in the MainViewModel
        }

        // Event handler for the button click to add a new task
        private void Adding_Clicked(object sender, EventArgs e)
        {
            var taskView = new NewTaskView() // Create an instance of the NewTaskView
            {
                BindingContext = new NewTaskViewModel // Set the binding context to a new instance of NewTaskViewModel
                {
                    // Pass the Tasks and Categories from the MainViewModel to the NewTaskViewModel
                    Tasks = _mainViewModel.Tasks,
                    Categories = _mainViewModel.Categories
                }
            };

            // Check if the taskView was successfully created
            if (taskView != null)
            {
                Navigation.PushAsync(taskView); // Navigate to the NewTaskView
            }
            else
            {
                DisplayAlert("ERROR", "Loading page", "Ok"); // Show an error alert if the page failed to load
            }
        }
    }
}
