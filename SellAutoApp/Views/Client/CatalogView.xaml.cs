using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SellAutoApp.DataAccess;
using SellAutoApp.Models;

namespace SellAutoApp.Views.Client {
    public partial class CatalogView : Window, INotifyPropertyChanged {
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

        private void LoadData()
        {
            using var dbContext = new SellCarDbContext();
            CarModels = new ObservableCollection<CarModel>(dbContext.CarModels
                .Include(x => x.Transmission)
                .Include(x => x.Manufacturer)
                .Include(x => x.Color)
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

        private void ProfileView_OnClick(object sender, RoutedEventArgs e)
        {
            var profileView = new ClientProfileView();
            profileView.Show();
            Close();
        }
    }
}
