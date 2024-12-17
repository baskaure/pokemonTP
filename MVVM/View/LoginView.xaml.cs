using System.Windows;
using System.Windows.Controls;

namespace pokemonTP.MVVM.View
{
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
        }

        
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            
        }

        
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
         
            this.NavigationService?.Navigate(new RegisterView());
        }
    }
}
