using SellAutoApp.DataAccess;
using SellAutoApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SellAutoApp.Views.Admin;

public partial class TransmissionView : Window, INotifyPropertyChanged {

    private ObservableCollection<Transmission> _transmissions;
    public ObservableCollection<Transmission> Transmissions {
        get => _transmissions;
        set {
            _transmissions = value;
            OnPropertyChanged();
        }
    }

    public TransmissionView() {
        InitializeComponent();
        DataContext = this;
        LoadData();
    }

    private void LoadData()
    {
        using var dbContext = new SellCarDbContext();
        Transmissions = new ObservableCollection<Transmission>(dbContext.Transmissions.ToList());
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

    private void RegisterView_OnClick(object sender, RoutedEventArgs e)
    {
        var registerView = new RegisterView();
        registerView.Show();
        Close();
    }

    private void AddTransmission_OnClick(object sender, RoutedEventArgs e)
    {
        try {
            using var dbContext = new SellCarDbContext();
            var transmission = new Transmission {
                TransmissionName = transmissionName.Text
            };
            dbContext.Add(transmission);
            dbContext.SaveChanges();
            LoadData();
        } catch (Exception ex) {
            MessageBox.Show(ex.Message);
        }
    }
}