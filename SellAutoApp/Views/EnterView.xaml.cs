using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SellAutoApp.DataAccess;
using SellAutoApp.Views.Admin;

namespace SellAutoApp.Views;

public partial class EnterView : Window {
    public EnterView() {
        InitializeComponent();
    }

    private void Login_OnClick(object sender, RoutedEventArgs e) {
        TryLogin();
    }

    private void Exit_OnClick(object sender, RoutedEventArgs e) {
        Application.Current.Shutdown();
    }

    private void TryLogin() {
        try {
            using var context = new SellCarDbContext();
            var user = context.Users
                .Include(x => x.UserRole)
                .FirstOrDefault(x => x.UserName == login.Text);
            if (user != null) {
                var generatedHash = GenerateHash(Encoding.UTF8.GetBytes(password.Password));
                var hash = Convert.FromBase64String(user.UserHash);
                if (!CompareByteArrays(generatedHash, hash)) {
                    MessageBox.Show("Неправильный пароль");
                    return;
                }

                switch (user.UserRole.UserRoleName)
                {
                    case "Admin":
                        var adminUserView = new AdminUserView();
                        adminUserView.Show();
                        Close();
                        break;
                    case "Client":
                        break;
                    default:
                        throw new Exception("Unknown role type");
                }

            } else {
                MessageBox.Show("Такого пользователя не существует");
            }
        } catch (Exception ex) {
            MessageBox.Show(ex.Message);
        }
    }

    private static byte[] GenerateHash(byte[] password) {
        var algorithm = new SHA256Managed();

        var plainTextWithSaltBytes = new byte[password.Length];

        for (var i = 0; i < password.Length; i++) {
            plainTextWithSaltBytes[i] = password[i];
        }

        return algorithm.ComputeHash(plainTextWithSaltBytes);
    }

    private static bool CompareByteArrays(byte[] array1, byte[] array2) {
        if (array1.Length != array2.Length) {
            return false;
        }

        return !array1.Where((t, i) => t != array2[i]).Any();
    }
}