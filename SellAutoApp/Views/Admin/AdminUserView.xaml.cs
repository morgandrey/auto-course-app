using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SellAutoApp.DataAccess;
using SellAutoApp.Models;

namespace SellAutoApp.Views.Admin;

public partial class AdminUserView : Window, INotifyPropertyChanged
{
    private ObservableCollection<User> users;

    public ObservableCollection<User> Users
    {
        get => users;
        set
        {
            users = value;
            OnPropertyChanged();
        }
    }

    public AdminUserView()
    {
        InitializeComponent();
        DataContext = this;
        LoadData();
    }

    private void LoadData()
    {
        using var context = new SellCarDbContext();
        Users = new ObservableCollection<User>(context.Users
            .Include(x => x.UserRole)
            .ToList());
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private void Exit_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void CarModelView_OnClick(object sender, RoutedEventArgs e)
    {
        var carModelView = new CarModelView();
        carModelView.Show();
        Close();
    }

    private void RegisterView_OnClick(object sender, RoutedEventArgs e) {
        var registerView = new RegisterView();
        registerView.Show();
        Close();
    }

    private void TransmissionView_OnClick(object sender, RoutedEventArgs e) {
        var transmissionView = new TransmissionView();
        transmissionView.Show();
        Close();
    }
}