using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RealEstate.Models;

namespace RealEstate.AllPages;

public partial class EstateAddPage : Page
{
    public EstateAddPage()
    {
        InitializeComponent();
        TypeComboBox.ItemsSource = DB.Context.EstateTypes.ToList();
        TypeComboBox.SelectedIndex = 0;
    }

    public EstateAddPage(Estate estate)
    {
        InitializeComponent();

        TypeComboBox.ItemsSource = DB.Context.EstateTypes.ToList();
        TypeComboBox.SelectedItem = estate.Type;
        CityTextBox.Text = estate.Address.City;
        StreetTextBox.Text = estate.Address.Street;
        LaitudeTextBox.Text = estate.Latitude.ToString();
        LogitudeTextBox.Text = estate.Logitude.ToString();
        FloorCountTextBox.Text = estate.TotalFloors.ToString();
        FloorTextBox.Text = estate.Floor.ToString();
        RoomCountTextBox.Text = estate.TotalRooms.ToString();
        AresTextBox.Text = estate.TotalArea.ToString();
    }

    private void TypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (TypeComboBox.SelectedIndex == 0)
        {
            FloorCountTextBox.IsEnabled = true;
            FloorTextBox.IsEnabled = true;
            RoomCountTextBox.IsEnabled = true;
            AresTextBox.IsEnabled = true;
        }

        if (TypeComboBox.SelectedIndex == 1)
        {
            FloorCountTextBox.IsEnabled = true;
            FloorTextBox.IsEnabled = false;
            RoomCountTextBox.IsEnabled = true;
            AresTextBox.IsEnabled = true;
        }

        if (TypeComboBox.SelectedIndex == 2)
        {
            FloorCountTextBox.IsEnabled = false;
            FloorTextBox.IsEnabled = false;
            RoomCountTextBox.IsEnabled = false;
            AresTextBox.IsEnabled = true;
        }
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (CityTextBox.Text == String.Empty || StreetTextBox.Text == String.Empty)
            {
                MessageBox.Show("Местоположение не может быть пустым");
                return;
            }

            Address address = new Address(CityTextBox.Text, StreetTextBox.Text);
            DB.Context.Addresses.Add(address);

            Estate estate = new Estate
            {
                Address = address,
                Type = (EstateType)TypeComboBox.SelectedItem,
                Latitude = Convert.ToInt32(LaitudeTextBox.Text),
                Logitude = Convert.ToInt32(LogitudeTextBox.Text),
                TotalFloors = Convert.ToInt32(FloorCountTextBox.Text),
                Floor = Convert.ToInt32(FloorTextBox.Text),
                TotalRooms = Convert.ToInt32(RoomCountTextBox.Text),
                TotalArea = Convert.ToDouble(AresTextBox.Text)
            };
            DB.Context.Estates.Add(estate);

            DB.Context.SaveChanges();
            NavigationService.Navigate(new EstateViewPage());
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }
}