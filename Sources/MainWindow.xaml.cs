using System.Windows;

namespace Backuper
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModelMainWindow();
        }

        void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // bugfix: if window is shown by left clicking TaskbarIcon, then it is not activated
            // TODO: refactor as behavior
            if (IsVisible)
                Activate();
        }
    }
}
