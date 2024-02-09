using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Controls.Primitives;

namespace Prakticheskaya_2
{
    public partial class MainWindow : Window
    {
        Notebook notebook = new Notebook();
        List<Notebook> NotebookList = new List<Notebook>();
        public MainWindow()
        {
            InitializeComponent();
            DeserializeJsonFile();
            DateChoice.Text = DateTime.Now.ToString();
            ShowItemsInListBox();
        }
        
        public void SerializeJsonFile()
        {
            JSONchik.JsonSerialize(NotebookList);
        }

        public void ShowItemsInListBox()
        {
            DeserializeJsonFile();
            string selectedDate = DateChoice.Text;
            var filteredNotebooks = NotebookList.Where(notebook => notebook.DateOfCreation == selectedDate);
            ListBox.ItemsSource = filteredNotebooks.Select(item => $"#{item.ID}: {item.NameOfNotebook}");
        }

        //ListBox.ItemsSource = NotebookList.Select(item => item.NameOfNotebook);

        public void DeserializeJsonFile()
        {
            if (File.ReadAllText("C:\\Users\\erton\\Desktop\\Notebooks.json") != "")
            {
                NotebookList = JSONchik.JsonDeserialize<List<Notebook>>();
            }
        }

        private void CreateNotebook_Click(object sender, RoutedEventArgs e)
        {
            DeserializeJsonFile();
            notebook.ID = NotebookList.Count+1;
            notebook.DateOfCreation = DateChoice.Text;
            notebook.NameOfNotebook = TextBoxName.Text;
            notebook.DescriptionOfNotebook = TextBoxDescription.Text;
            NotebookList.Add(notebook);
            SerializeJsonFile();
            MessageBox.Show("Заметка создана");
            ShowItemsInListBox();
            TextBoxName.Text = "";
            TextBoxDescription.Text = "";
        }
        public int SearchID(string NotebookStr) //fix
        {
            string selectedIDStr = NotebookStr;
            int index = selectedIDStr.IndexOf("#");
            char nextChar = selectedIDStr[index + 1];
            int selectedID = int.Parse(nextChar.ToString());
            return selectedID;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //fix
        {
            string NotebookStr = ListBox.SelectedItem.ToString();
            int selectedID = SearchID(NotebookStr);
            TextBoxName.Text = NotebookList[selectedID].NameOfNotebook;
            TextBoxDescription.Text = NotebookList[selectedID].DescriptionOfNotebook;
        }

        private void DeleteNotebook_Click(object sender, RoutedEventArgs e) //fix
        {
            if (ListBox.SelectedIndex != -1 && ListBox.SelectedIndex < NotebookList.Count)
            {
                var selectedNotebook = NotebookList[ListBox.SelectedIndex];
                NotebookList.RemoveAt(ListBox.SelectedIndex);
                SerializeJsonFile();
                MessageBox.Show("Заметка удалена");
                ShowItemsInListBox();
                TextBoxName.Text = "";
                TextBoxDescription.Text = "";
            }
        }

        private void SaveNotebook_Click(object sender, RoutedEventArgs e) //fix
        {
            if (ListBox.SelectedIndex != -1 && ListBox.SelectedIndex < NotebookList.Count)
            {
                var selectedNotebook = NotebookList[ListBox.SelectedIndex];
                selectedNotebook.NameOfNotebook = TextBoxName.Text;
                selectedNotebook.DescriptionOfNotebook = TextBoxDescription.Text;
                selectedNotebook.DateOfCreation = DateChoice.Text;
                SerializeJsonFile();
                MessageBox.Show("Заметка сохранена");
                ShowItemsInListBox();
                TextBoxName.Text = "";
                TextBoxDescription.Text = "";
            }
        }

        private void DateChoice_SelectedDateChanged(object sender, SelectionChangedEventArgs e) //zbs
        {
            ShowItemsInListBox();
        }
    }
}