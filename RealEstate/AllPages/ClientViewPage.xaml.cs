using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RealEstate.Models;

namespace RealEstate.AllPages;

public partial class ClientViewPage : Page
{
    public ClientViewPage()
    {
        InitializeComponent();

        _clients = DB.Context.Clients.ToList();
        ClientDataGrid.ItemsSource = _clients;
    }

    private List<Client> _clients;

    private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e) => Search();

    private void Search()
    {
        if (String.IsNullOrEmpty(SearchTextBox.Text))
        {
            ClientDataGrid.ItemsSource = _clients;
            return;
        }

        List<Client> tempList = new List<Client>();
        foreach (var client in _clients)
        {
            int miss = 0;
            string search = SearchTextBox.Text.ToLower();
            int max = search.Length;
            
            if (max <= client.FirstName.Length)
            {
                for (int i = 0; i < search.Length; i++)
                {
                    if (client.FirstName.ToLower()[i] != SearchTextBox.Text[i]) miss++;
                    if (miss == 4) break;
                }

                if (miss < 4)
                    tempList.Add(client);
            }
            
            if (max <= client.LastName.Length)
            {
                for (int i = 0; i < search.Length; i++)
                {
                    if (client.LastName.ToLower()[i] != SearchTextBox.Text[i]) miss++;
                    if (miss == 4) break;
                }

                if (miss < 4)
                    tempList.Add(client);
            }
            
            if (max <= client.MiddleName.Length)
            {
                for (int i = 0; i < search.Length; i++)
                {
                    if (client.MiddleName.ToLower()[i] != SearchTextBox.Text[i]) miss++;
                    if (miss == 4) break;
                }

                if (miss < 4)
                    tempList.Add(client);
            }
        }

        tempList = tempList.Distinct().ToList();
        ClientDataGrid.ItemsSource = tempList;
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        Client currentClient = (Client)((Button)sender).DataContext;
        _clients.Remove(currentClient);
        DB.Context.Clients.Remove(currentClient);
        DB.Context.SaveChanges();
        ClientDataGrid.ItemsSource = null;
        Search();
    }

    private void UpdateButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new ClientAddPage((Client)((Button)sender).DataContext));

    private void AddButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new ClientAddPage());
}