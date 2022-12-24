using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using SellAutoApp.DataAccess;
using SellAutoApp.Models;

namespace SellAutoApp.Views.Admin;

public partial class RegisterView : Window, INotifyPropertyChanged {
    private ObservableCollection<UserRole> _userRoles;
    public ObservableCollection<UserRole> UserRoles {
        get => _userRoles;
        set {
            _userRoles = value;
            OnPropertyChanged();
        }
    }
    public RegisterView() {
        InitializeComponent();
        DataContext = this;
        using var dbContext = new SellCarDbContext();
        UserRoles = new ObservableCollection<UserRole>(dbContext.UserRoles.ToList());
    }

    private void CarModelView_OnClick(object sender, RoutedEventArgs e) {
        var carModelView = new CarModelView();
        carModelView.Show();
        Close();
    }

    private void Exit_OnClick(object sender, RoutedEventArgs e) {
        Application.Current.Shutdown();
    }

    private void AdminUserView_OnClick(object sender, RoutedEventArgs e) {
        var adminUserView = new AdminUserView();
        adminUserView.Show();
        Close();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private void Register_OnClick(object sender, RoutedEventArgs e) {
        try {
            if (string.IsNullOrEmpty(login.Text) ||
                string.IsNullOrEmpty(password.Password) ||
                string.IsNullOrEmpty(name.Text) ||
                string.IsNullOrEmpty(surname.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            using var dbContext = new SellCarDbContext();
            var generatedHash = GenerateHash(Encoding.UTF8.GetBytes(password.Password));
            var user = new User {
                UserName = name.Text,
                UserSurname = surname.Text,
                UserLogin = login.Text,
                UserHash = Convert.ToBase64String(generatedHash),
                UserRoleId = (int)roleComboBox.SelectedValue
            };
            dbContext.Add(user);
            dbContext.SaveChanges();
            MessageBox.Show("Пользователь зарегистрирован");
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

    private void TransmissionView_OnClick(object sender, RoutedEventArgs e) {
        var transmissionView = new TransmissionView();
        transmissionView.Show();
        Close();
    }
}