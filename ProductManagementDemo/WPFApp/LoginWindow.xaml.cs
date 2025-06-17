using System;
using System.Windows;
using System.Windows.Controls; // Assuming txtUser and txtPass are TextBoxes or similar
using BusinessObjects; // Assuming AccountMember is defined here
using Services; // Assuming IAccountService and AccountService are defined here

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAccountService _iAccountService; // Changed name for consistency with C# naming conventions

        public LoginWindow()
        {
            InitializeComponent();
            _iAccountService = new AccountService(); // Initialize the service in the constructor
        }

        // Event handler for the Login button click
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Assuming txtUser and txtPass are the names of your UI elements for username and password
            // You might need to cast sender to Button if you want to access its properties,
            // but for this logic, it's not strictly necessary.

            // Get account details by ID (username)
            // It's good practice to trim whitespace from user input
            AccountMember account = _iAccountService.GetAccountById(txtUser.Text.Trim());

            // Check login credentials and role
            if (account != null && account.MemberPassword.Equals(txtPass.Password) && account.MemberRole == 1)
            {
                // Login successful and user has the required role (e.g., administrator)
                this.Hide(); // Hide the current login window

                MainWindow mainWindow = new MainWindow(); // Create a new instance of the main window
                mainWindow.Show(); // Display the main window
            }
            else
            {
                // Login failed or user does not have the required permission
                // Corrected the message for better English grammar
                MessageBox.Show("Login failed or you do not have permission!", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for the Cancel button click
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the current login window
        }
    }
}