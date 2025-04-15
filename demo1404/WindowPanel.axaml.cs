using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using demo1404.Models;
using System.Numerics;

namespace demo1404;

public partial class WindowPanel : Window
{
    private Phone phone;
    public WindowPanel()
    {
        InitializeComponent();
        phone = new Phone();

        TypeComboBox.ItemsSource = Helper.User017Context.Phones;
        PartnerPanel.DataContext = phone;
    }

    public WindowPanel(Phone phone)
    {
        InitializeComponent();
        phone = new Phone();

        TypeComboBox.ItemsSource = Helper.User017Context.Phones;
        PartnerPanel.DataContext = phone;
        this.phone = phone;
    }


   private void Button_OnClick_Back(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void Button_OnClick_Save(object? sender, RoutedEventArgs e)
   {
        if (phone.Id == 0)
        {
            Helper.User017Context.Phones.Add(phone);
            Helper.User017Context.SaveChanges();
        }
        else
        {
            Helper.User017Context.Phones.Update(phone);
            Helper.User017Context.SaveChanges();
        }

        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

}