using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SellAutoApp.DataAccess;
using SellAutoApp.Models;

namespace SellAutoApp.Views.Client;

public partial class CatalogView : Window, INotifyPropertyChanged {

    public static CarModel CurrentCarModel { get; set; }

    private ObservableCollection<CarModel> _carModels;
    public ObservableCollection<CarModel> CarModels {
        get => _carModels;
        set {
            _carModels = value;
            OnPropertyChanged();
        }
    }

    public CatalogView() {
        InitializeComponent();
        DataContext = this;
        LoadData();
    }

    private void LoadData() {
        using var dbContext = new SellCarDbContext();
        CarModels = new ObservableCollection<CarModel>(dbContext.CarModels
            .Include(x => x.Transmission)
            .Include(x => x.Manufacturer)
            .Include(x => x.Color)
            .Include(x => x.CarModelPrices)
            .ToList());
    }

    private void Exit_OnClick(object sender, RoutedEventArgs e) {
        Application.Current.Shutdown();
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

    private void ProfileView_OnClick(object sender, RoutedEventArgs e) {
        var profileView = new ClientProfileView();
        profileView.Show();
        Close();
    }

    private void CreateOrder_OnClick(object sender, RoutedEventArgs e) {
        try {
            CurrentCarModel = carModelDataGrid.SelectedItem as CarModel;
            using var dbContext = new SellCarDbContext();
            dbContext.Database.BeginTransaction();

            var order = new Order {
                UserId = EnterView.CurrentUser.UserId,
                OrderDate = DateTime.Now,
            };
            dbContext.Add(order);
            dbContext.SaveChanges();

            var carModelOrder = new CarModelOrder {
                OrderId = order.OrderId,
                CarModelId = CurrentCarModel.CarModelId
            };
            dbContext.Add(carModelOrder);
            dbContext.SaveChanges();

            dbContext.Database.CommitTransaction();

            MessageBox.Show("Транзакция прошла!");
        } catch (Exception ex) {
            MessageBox.Show(ex.Message);
        }
    }
}