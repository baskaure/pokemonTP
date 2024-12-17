using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace pokemonTP.MVVM.View
{
    public partial class RegisterView : Page
    {
        private readonly string connectionString =
            "Server=DESKTOP-AUR\\SQLEXPRESS01;Database=ExerciceMonster;Trusted_Connection=True;TrustServerCertificate=True;";

        public RegisterView()
        {
            InitializeComponent();
        }

       
        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsernamePlaceholder.Visibility = string.IsNullOrEmpty(UsernameTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }


        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Tous les champs doivent être remplis.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string hashedPassword = HashPassword(password);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string checkUserQuery = "SELECT COUNT(1) FROM Login WHERE Username = @Username";
                    SqlCommand checkUserCmd = new SqlCommand(checkUserQuery, connection);
                    checkUserCmd.Parameters.AddWithValue("@Username", username);
                    int userExists = Convert.ToInt32(checkUserCmd.ExecuteScalar());

                    if (userExists > 0)
                    {
                        MessageBox.Show("Ce nom d'utilisateur est déjà pris.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

 
                    string insertQuery = "INSERT INTO Login (Username, PasswordHash) VALUES (@Username, @PasswordHash)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                    insertCmd.Parameters.AddWithValue("@Username", username);
                    insertCmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    int result = insertCmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Inscription réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.NavigationService?.Navigate(new LoginView());
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de l'inscription.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder hashStringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashStringBuilder.Append(b.ToString("X2"));
                }
                return hashStringBuilder.ToString();
            }
        }
    }
}
