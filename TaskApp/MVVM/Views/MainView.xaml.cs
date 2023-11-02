using TaskApp.MVVM.ViewModels;

namespace TaskApp.MVVM.Views;

public partial class MainView : ContentPage
{
    private MainViewModel _mainViewModel = new MainViewModel();
    public MainView()
	{
		InitializeComponent();
        BindingContext = _mainViewModel;

    }

    private void checkBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        _mainViewModel.UpdateData();
    }

    private void Adding_Clicked(object sender, EventArgs e)
    {
        var taskView = new NewTaskView()
        {
            BindingContext = new NewTaskViewModel
            {
                Tasks = _mainViewModel.Tasks,
                Categories = _mainViewModel.Categories
            }
        };

        if (taskView != null )
        {
            Navigation.PushAsync(taskView);
        }
        else
        {
            DisplayAlert("ERROR", "Loading page", "Ok");
        }

        
    }
}