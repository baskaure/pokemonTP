using System.Windows;
using System.Windows.Controls;

namespace pokemonTP.MVVM.View
{
    public partial class PageAccueil : Page
    {
        public PageAccueil()
        {
            InitializeComponent();
        }

        private void BtnConnexionBDD_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Connexion à la base de données...");
        }
    }
}
