using System;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace pokemonTP.MVVM.View
{
    public partial class ConnexionBDD : Page
    {
        public ConnexionBDD()
        {
            InitializeComponent();
        }

        private void BtnValiderConnexion_Click(object sender, RoutedEventArgs e)
        {
            
            string connectionString = ConnexionStringTextBox.Text;

            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Veuillez entrer une chaîne de connexion.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Connexion à la base de données réussie.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                    
                    this.NavigationService?.Navigate(new LoginView());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
