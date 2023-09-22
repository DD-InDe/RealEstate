using System;
using System.Windows;
using System.Windows.Controls;
using RealEstate.Models;

namespace RealEstate.AllPages;

public partial class ClientAddPage : Page
{
    public ClientAddPage()
    {
        InitializeComponent();
        
        Title = "Добавление клиента";
    }

    public ClientAddPage(Client client)
    {
        InitializeComponent();

        Title = "Редактирование клиента";
        FirstNameTextBox.Text = client.FirstName;
        LastNameTextBox.Text = client.LastName;
        MiddleNameTextBox.Text = client.MiddleName;
        EmailTextBox.Text = client.Email;
        PhoneTextBox.Text = client.Phone;
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (EmailTextBox.Text == String.Empty && PhoneTextBox.Text == String.Empty)
            {
                MessageBox.Show("Почта или номер телефона должны быть указаны");
                return;
            }

            Client client = new Client(FirstNameTextBox.Text, LastNameTextBox.Text, MiddleNameTextBox.Text,
                PhoneTextBox.Text, EmailTextBox.Text);
            DB.Context.Clients.Add(client);
            DB.Context.SaveChanges();
            NavigationService.Navigate(new ClientViewPage());
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }
}