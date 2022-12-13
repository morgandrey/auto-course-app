using SellAutoApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SellAutoApp.DataAccess;

namespace SellAutoApp.Views.Employee;

public partial class EmployeeStatisticsView : Window, INotifyPropertyChanged {

    private ObservableCollection<CarModelOrder> _carModelOrders;
    public ObservableCollection<CarModelOrder> CarModelOrders {
        get => _carModelOrders;
        set {
            _carModelOrders = value;
            OnPropertyChanged();
        }
    }
    public EmployeeStatisticsView() {
        InitializeComponent();
        DataContext = this;
        LoadData();
    }

    private void LoadData()
    {
        using var dbContext = new SellCarDbContext();
        CarModelOrders = new ObservableCollection<CarModelOrder>(dbContext.CarModelOrders
            .Include(x => x.Order.User)
            .Include(x => x.CarModel)
            .ThenInclude(x => x.CarModelPrices)
            .ToList());
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
}