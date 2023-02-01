using Windows.UI.Xaml.Controls;
using Crypto_task.ViewModels;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Crypto_task.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navView);
        }

        public MainViewModel ViewModel { get; } = new MainViewModel();
                
    }
}
