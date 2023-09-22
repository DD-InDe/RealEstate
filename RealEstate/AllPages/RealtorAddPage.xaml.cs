using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate.Models;
using Key = System.Windows.Input.Key;

namespace RealEstate.AllPages;

public partial class RealtorAddPage : Page
{
    public RealtorAddPage()
    {
        InitializeComponent();
        Title = "Добавление риэлтора";
    }

    public RealtorAddPage(Realtor realtor)
    {
        InitializeComponent();
        Title = "Редактирование риэлтора";
        
        FirstNameTextBox.Text = realtor.FirstName;
        LastNameTextBox.Text = realtor.LastName;
        MiddleNameTextBox.Text = realtor.MiddleName;
        ShareTextBox.Text = realtor.Share.ToString();
    }

    private void ShareTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (FirstNameTextBox.Text == String.Empty || LastNameTextBox.Text == String.Empty || MiddleNameTextBox.Text == String.Empty ||
                ShareTextBox.Text == String.Empty)
            {
                MessageBox.Show("Поля не могут быть пустыми");
                return;
            }

            if (Convert.ToInt32(ShareTextBox.Text) > 100)
            {
                MessageBox.Show("Доля от комиссии может быть от 0 до 100");
                return;
            }

            Realtor realtor = new Realtor(FirstNameTextBox.Text, LastNameTextBox.Text, MiddleNameTextBox.Text, Convert.ToInt32(ShareTextBox.Text));
            DB.Context.Realtors.Add(realtor);
            DB.Context.SaveChanges();
            NavigationService.Navigate(new RealtorViewPage());
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    private void ShareTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
            e.Handled = true;
    }
}