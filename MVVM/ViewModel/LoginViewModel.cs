using CommunityToolkit.Mvvm.Input;
using pokemonTP.Model;
using System.ComponentModel;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using System.Windows.Controls;

public class LoginViewModel : BaseViewModel
{
    private string _username;
    private string _password;
    private PasswordBox _passwordBox;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public PasswordBox PasswordBox
    {
        get => _passwordBox;
        set
        {
            _passwordBox = value;
            OnPropertyChanged(nameof(PasswordBox));
        }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new RelayCommand(ExecuteLogin);
    }

    private void ExecuteLogin()
    {
        if (PasswordBox == null)
        {
            MessageBox.Show("Le mot de passe n'est pas initialisé");
            return;
        }

        using (var context = new ExerciceMonsterContext())
        {
            var hashedPassword = HashPassword(PasswordBox.Password);
            var user = context.Logins
                .FirstOrDefault(u => u.Username == Username && u.PasswordHash == hashedPassword);
            if (user != null)
            {
                MessageBox.Show("Connexion réussie !");
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe invalide.");
            }
        }
    }


    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
