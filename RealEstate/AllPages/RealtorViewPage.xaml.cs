using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RealEstate.Models;

namespace RealEstate.AllPages;

public partial class RealtorViewPage : Page
{
    public RealtorViewPage()
    {
        InitializeComponent();
        _realtors = DB.Context.Realtors.ToList();
        RealtorDataGrid.ItemsSource = _realtors;
    }

    private List<Realtor> _realtors;
    private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        Search();
    }

    private void Search()
    {
        if (String.IsNullOrEmpty(SearchTextBox.Text))
        {
            RealtorDataGrid.ItemsSource = _realtors;
            return;
        }

        List<Realtor> tempList = new List<Realtor>();
        foreach (var realtor in _realtors)
        {
            int miss = 0;
            string search = SearchTextBox.Text.ToLower();
            int max = search.Length;
            
            if (max <= realtor.FirstName.Length)
            {
                for (int i = 0; i < search.Length; i++)
                {
                    if (realtor.FirstName.ToLower()[i] != SearchTextBox.Text[i]) miss++;
                    if (miss == 4) break;
                }

                if (miss < 4)
                    tempList.Add(realtor);
            }
            
            if (max <= realtor.LastName.Length)
            {
                for (int i = 0; i < search.Length; i++)
                {
                    if (realtor.LastName.ToLower()[i] != SearchTextBox.Text[i]) miss++;
                    if (miss == 4) break;
                }

                if (miss < 4)
                    tempList.Add(realtor);
            }
            
            if (max <= realtor.MiddleName.Length)
            {
                for (int i = 0; i < search.Length; i++)
                {
                    if (realtor.MiddleName.ToLower()[i] != SearchTextBox.Text[i]) miss++;
                    if (miss == 4) break;
                }

                if (miss < 4)
                    tempList.Add(realtor);
            }
        }

        tempList = tempList.Distinct().ToList();
        RealtorDataGrid.ItemsSource = tempList;
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new RealtorAddPage());

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        Realtor currentRealtor = (Realtor)((Button)sender).DataContext;
        _realtors.Remove(currentRealtor);
        DB.Context.Realtors.Remove(currentRealtor);
        DB.Context.SaveChanges();
        RealtorDataGrid.ItemsSource = null;
        Search();
    }

    private void UpdateButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new RealtorAddPage((Realtor)((Button)sender).DataContext));
}