using System.Windows;
using pokemonTP.MVVM.View;

namespace pokemonTP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ConnexionBDD()); 
        }
    }
}
