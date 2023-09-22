using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RealEstate.AllPages;

public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void ClientAddButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new ClientAddPage());

    private void RealtorAddButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new RealtorAddPage());

    private void ClientViewButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new ClientViewPage());

    private void RealtorViewButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new RealtorViewPage());
}