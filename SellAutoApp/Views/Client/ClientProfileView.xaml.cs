using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SellAutoApp.Views.Client;

public partial class ClientProfileView : Window, INotifyPropertyChanged {
    public ClientProfileView() {
        InitializeComponent();
        DataContext = this;
        LoadData();
    }

    private void LoadData()
    {
        clientName.Content = EnterView.CurrentUser.UserName;
        clientSurname.Content = EnterView.CurrentUser.UserSurname;
    }

    private void Exit_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
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

    private void LoginWindow_OnClick(object sender, RoutedEventArgs e)
    {
        var enterView = new EnterView();
        enterView.Show();
        Close();
    }

    private void CatalogView_OnClick(object sender, RoutedEventArgs e)
    {
        var catalogView = new CatalogView();
        catalogView.Show();
        Close();
    }
}