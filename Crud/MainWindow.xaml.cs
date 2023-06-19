using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace Crud
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Person> peoples = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click_Guardar(object sender, RoutedEventArgs e)
        {
            bool ban = false;
            int i = 0;
            try
            {
                if(!id_txt.Text.Equals("") && !nombre_txt.Text.Equals("") && !edad_txt.Text.Equals("") && !email_txt.Text.Equals(""))
                {
                    Person p = new Person(Int16.Parse(id_txt.Text), nombre_txt.Text, Int16.Parse(edad_txt.Text), email_txt.Text);

                    for (i = 0; i < peoples.Count; i++)
                    {
                        if (peoples[i].Id == p.Id)
                        {
                            ban = true; break;
                        }
                    }

                    if (ban != true)
                    {
                        peoples.Add(p);
                        dataGrid.ItemsSource = peoples.ToList();
                    }
                    else
                    {
                        peoples[i] = p;
                    }
                    Limpiar();
                }
            }
            catch (Exception ex) { }
        }
        private void Button_Click_Nuevo(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        private void Button_Click_Eliminar(object sender, RoutedEventArgs e)
        {
            int i;
            try
            {
                var result = MessageBox.Show("Seguro que deseas eliminar este registro!", "Message", MessageBoxButton.YesNo);
                
                if (result == MessageBoxResult.Yes)
                {
                    var row = (dataGrid.SelectedItem as Person).Id;
                    Console.WriteLine("row: " + row);
                    for (i = 0; i < peoples.Count; i++)
                    {
                        if (row == peoples[i].Id)
                        {
                            Cargar(peoples[i]);
                            peoples.RemoveAt(i);
                            dataGrid.ItemsSource = peoples.ToList();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Person person = (dataGrid.SelectedItem as Person);
            Cargar(person);
        }
        public void Limpiar()
        {
            id_txt.Clear();
            nombre_txt.Clear();
            edad_txt.Clear();
            email_txt.Clear();
        }
        private void Cargar(Person person)
        {
            id_txt.Text = person.Id.ToString();
            edad_txt.Text = person.Edad.ToString();
            nombre_txt.Text = person.Nombre.ToString();
            email_txt.Text = person.Email.ToString();
        }
    }
}
