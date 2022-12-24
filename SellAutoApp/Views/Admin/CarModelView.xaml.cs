using SellAutoApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using SellAutoApp.DataAccess;

namespace SellAutoApp.Views.Admin;

public partial class CarModelView : Window, INotifyPropertyChanged {
    private ObservableCollection<CarModel> _carModels;
    public ObservableCollection<CarModel> CarModels {
        get => _carModels;
        set {
            _carModels = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Transmission> _transmissions;
    public ObservableCollection<Transmission> Transmissions {
        get => _transmissions;
        set {
            _transmissions = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Color> _colors;
    public ObservableCollection<Color> Colors {
        get => _colors;
        set {
            _colors = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Manufacturer> _manufacturers;
    public ObservableCollection<Manufacturer> Manufacturers {
        get => _manufacturers;
        set {
            _manufacturers = value;
            OnPropertyChanged();
        }
    }

    public CarModelView() {
        InitializeComponent();
        DataContext = this;
        LoadData();
    }

    private void LoadData() {
        using var dbContext = new SellCarDbContext();
        Colors = new ObservableCollection<Color>(dbContext.Colors.ToList());
        Transmissions = new ObservableCollection<Transmission>(dbContext.Transmissions.ToList());
        Manufacturers = new ObservableCollection<Manufacturer>(dbContext.Manufacturers.ToList());
        CarModels = new ObservableCollection<CarModel>(dbContext.CarModels.ToList());
    }

    private void AddCarModel_OnClick(object sender, RoutedEventArgs e) {

        try {
            using var dbContext = new SellCarDbContext();
            dbContext.Database.BeginTransaction();
            var carModel = new CarModel {
                CarModelName = carModelName.Text,
                ManufacturerId = (int)manufacturerComboBox.SelectedValue,
                ColorId = (int)colorComboBox.SelectedValue,
                TransmissionId = (int)transmissionComboBox.SelectedValue
            };
            dbContext.Add(carModel);
            dbContext.SaveChanges();

            var carModelPrice = new CarModelPrice {
                CarModelPriceUpdate = DateTime.Now,
                CarModelPrice1 = float.Parse(modelPrice.Text),
                CarModelId = carModel.CarModelId
            };
            dbContext.Add(carModelPrice);
            dbContext.SaveChanges();

            dbContext.Database.CommitTransaction();
            MessageBox.Show("Модель добавлена");
            LoadData();
        } catch (Exception ex) {
            MessageBox.Show(ex.Message);
        }
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

    private void TransmissionView_OnClick(object sender, RoutedEventArgs e) {
        var transmissionView = new TransmissionView();
        transmissionView.Show();
        Close();
    }
}