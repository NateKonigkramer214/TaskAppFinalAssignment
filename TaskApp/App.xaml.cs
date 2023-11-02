	using TaskApp.MVVM.Views;
using TaskApp.MVVM.ViewModels;
namespace TaskApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new NavigationPage(new MainView());
		BindingContext = new MainViewModel();
    }
}
