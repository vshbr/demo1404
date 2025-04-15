using Avalonia.Controls;
using System.Collections.Generic;
using Avalonia.Markup.Xaml;
using System.Linq;
using demo1404;
using System.Threading.Tasks;
using demo1404.Models;
using System.Collections.ObjectModel;
using System.Collections;
using System;
using Microsoft.EntityFrameworkCore;
using demo1404.Context;

namespace demo1404
{
    public partial class MainWindow : Window
    {
        // 1. �������� �������� �� Phone
        private ObservableCollection<Phone> _displayList { get; set; }
        private List<Phone> _originalList;
        private ObservableCollection<string> _genders;

        public MainWindow()
        {
            InitializeComponent();
            LoadPhones(); // ��������������� ����� ��������
            Filter.ItemsSource = _genders;
            Filter.SelectedIndex = 0;
        }

        // 2. ��������������� ����� �������� ������
        private void LoadPhones()
        {
            using var dbContext = new User017Context();

            // 3. ��������� �������� � ����������
            _originalList = dbContext.Phones
                .Include(x => x.Company)
                .ToList();

            _displayList = new ObservableCollection<Phone>(_originalList);
            PartnerListBox.ItemsSource = _displayList;

            // 4. �������� �������� (���� �����)
            var manufacturers = dbContext.Companies
                .Select(p => p.Name)
                .ToList();

            _genders = new ObservableCollection<string>(manufacturers);
        }

        public void Display()
        {
            // 5. �������� � ������ ������������� ������
            var filtered = _originalList.AsEnumerable();

            // ����� �� ������
            if (!string.IsNullOrEmpty(Find.Text))
            {
                var searchWord = Find.Text.Trim().ToLower();

                filtered = filtered.Where(phone =>
                    phone.Model.ToLower().Contains(searchWord) ||
                    phone.Company.Name.ToLower().Contains(searchWord) ||
                    phone.Company.Fio.ToLower().Contains(searchWord)
                );
            }

            // ���������� �� �������������
            if (Filter.SelectedIndex != 0 && Filter.SelectedItem != null)
            {
                var selectedManufacturer = Filter.SelectedItem.ToString();
                filtered = filtered.Where(p =>
                    p.Company.Name == selectedManufacturer
                );
            }

            // ����������
            switch (Sort.SelectedIndex)
            {
                case 1:
                    filtered = filtered.OrderBy(p => p.Model);
                    break;
                case 2:
                    filtered = filtered.OrderByDescending(p => p.ReleaseDate);
                    break;
            }

            // 6. ��������� ������������ ������
            _displayList.Clear();
            foreach (var phone in filtered.ToList())
            {
                _displayList.Add(phone);
            }

        }

        // ��������� ������ �������� ��� ���������
        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => Display();
        private void FiltrComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => Display();
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => Display();
    


    private void Button_Click_Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            WindowPanel windowPanel = new WindowPanel();
            windowPanel.Show();
            Close();
        }
        private void ListBox_DoubleTapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var phone = PartnerListBox.SelectedItem as Phone;
            if (phone != null)
            {
                WindowPanel windowPanel = new WindowPanel(phone);
                windowPanel.Show();
                Close();
            }

        }
        private void Button_Click_History(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var phone = PartnerListBox.SelectedItem as Phone;
            if (phone != null)
            {
                WindowPanel windowPanel = new WindowPanel(phone);
                windowPanel.ShowDialog(this);
            }
        }
    }
}