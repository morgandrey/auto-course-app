using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
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
        Users = new ObservableCollection<User>(context.Users.ToList());
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
}