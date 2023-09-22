using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate.Models;

namespace RealEstate.AllPages;

public partial class EstateViewPage : Page
{
    public EstateViewPage()
    {
        InitializeComponent();

        _estates = DB.Context.Estates.Include(c => c.Type).Include(c => c.Address).ToList();
        EstateDataGrid.ItemsSource = _estates;

        List<string> typeList = new List<string>() { "Все" };
        typeList.AddRange(DB.Context.EstateTypes.Select(c => c.Name).ToList());
        List<string> addressList = new List<string>() { "Все" };
        addressList.AddRange(DB.Context.Addresses.Select(c => c.FullAddress).ToList());

        TypeComboBox.ItemsSource = typeList;
        AddressComboBox.ItemsSource = addressList;

        TypeComboBox.SelectedIndex = 0;
        AddressComboBox.SelectedIndex = 0;
    }

    private List<Estate> _estates;

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        Estate cuurentEstate = (Estate)((Button)sender).DataContext;
        _estates.Remove(cuurentEstate);
        DB.Context.Estates.Remove(cuurentEstate);
        DB.Context.SaveChanges();
        DataUpdate();
    }

    private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
    {
        Estate cuurentEstate = (Estate)((Button)sender).DataContext;
        NavigationService.Navigate(new EstateAddPage(cuurentEstate));
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new EstateAddPage());

    private void TypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => DataUpdate();

    private void AddressComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => DataUpdate();

    private void DataUpdate()
    {
        if (AddressComboBox.SelectedItem == null)
            return;

        if (AddressComboBox.SelectedIndex == 0 && TypeComboBox.SelectedIndex == 0)
            EstateDataGrid.ItemsSource = _estates;
        if (AddressComboBox.SelectedIndex != 0 && TypeComboBox.SelectedIndex == 0)
            EstateDataGrid.ItemsSource = _estates.Where(c => c.Address.FullAddress.ToString() == AddressComboBox.SelectedItem.ToString()).ToList();
        if (AddressComboBox.SelectedIndex == 0 && TypeComboBox.SelectedIndex != 0)
            EstateDataGrid.ItemsSource = _estates.Where(c => c.Type.Name.ToString() == TypeComboBox.SelectedItem.ToString()).ToList();
        if (AddressComboBox.SelectedIndex != 0 && TypeComboBox.SelectedIndex != 0)
            EstateDataGrid.ItemsSource = _estates.Where(c =>
                    c.Type.Name.ToString() == TypeComboBox.SelectedItem.ToString() &&
                    c.Address.FullAddress.ToString() == AddressComboBox.SelectedItem.ToString()).ToList();
    }
}