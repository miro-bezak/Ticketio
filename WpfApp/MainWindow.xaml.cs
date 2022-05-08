using ServerSide.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<TicketType> _availableTickets = new();
        private string? _currentUser = null;

        public MainWindow()
        {
            InitializeComponent();

            Header.Text += ServerSide.Facade.GetCurrentCity();
            PurchaseTicketButton.IsEnabled = false;

            fetchSingleTickets();
            AvailableTicketsGrid.DataContext = _availableTickets;
        }

        private void fetchSingleTickets()
        {
            _availableTickets.Clear();

            var fetchedTickets = ServerSide.Facade.GetSingleTickets();
            foreach (var ticket in fetchedTickets)
            {
                _availableTickets.Add(ticket);
            }
        }

        private void fetchTravelCards()
        {
            _availableTickets.Clear();

            var fetchedTickets = ServerSide.Facade.GetTravelCards();
            foreach (var ticket in fetchedTickets)
            {
                _availableTickets.Add(ticket);
            }
        }

        private void AvailableTicketsGrid_SelectionChanged(object sender, SelectionChangedEventArgs? e)
        {
            if (AvailableTicketsGrid.SelectedIndex == -1)
            {
                PurchaseTicketButton.IsEnabled = false;
            }
            else
            {
                PurchaseTicketButton.IsEnabled = true;
            }
        }

        private void TravelCardButton_Click(object sender, RoutedEventArgs e)
        {
            fetchTravelCards();
        }

        private void SingleTicketButton_Click(object sender, RoutedEventArgs e)
        {
            fetchSingleTickets();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServerSide.Facade.Authenticate(EnteredEmail.Text, EnteredPassword.Password))
            {
                _currentUser = EnteredEmail.Text;
            }
            else
            {
                MessageBox.Show("You have entered an invalid email and password comibination.", "Wrong password", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ServerSide.Facade.Register(EnteredEmail.Text, EnteredPassword.Password);
        }
    }
}
