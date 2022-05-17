using ServerSide.Data;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<TicketType> _availableTickets = new();
        private string? _currentUserEmail = null;
        private readonly ObservableCollection<PurchasedTicket> _ticketHistory = new();

        public MainWindow()
        {
            InitializeComponent();

            Header.Text += ServerSide.Facade.GetCurrentCity();
            PurchaseTicketButton.IsEnabled = false;

            FetchSingleTickets();
            AvailableTicketsGrid.DataContext = _availableTickets;
            TicketHistoryGrid.DataContext = _ticketHistory;
        }

        private void FetchSingleTickets()
        {
            _availableTickets.Clear();

            var fetchedTickets = ServerSide.Facade.GetSingleTickets();
            foreach (var ticket in fetchedTickets)
            {
                _availableTickets.Add(ticket);
            }
        }

        private void FetchTravelCards()
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
            FetchTravelCards();
        }

        private void SingleTicketButton_Click(object sender, RoutedEventArgs e)
        {
            FetchSingleTickets();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUserEmail != null)
            {
                // Act as a logout button
                _currentUserEmail = null;
                LoginButton.Content = "Login";
                LoginTitle.Text = "Log into your account or create a new one";
                TicketHistoryHeader.Text = "Login to view your ticket history";
                return;
            }
            if (ServerSide.Facade.Authenticate(EnteredEmail.Text, EnteredPassword.Password))
            {
                _currentUserEmail = EnteredEmail.Text;
                MessageBox.Show("You have been successfully logged in.", "Login success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                EnteredEmail.Text = "";
                EnteredPassword.Password = "";
                LoginTitle.Text = $"Welcome {_currentUserEmail}!";
                LoginButton.Content = "Logout";
                TicketHistoryHeader.Text = "History of all purchased tickets";
            }
            else
            {
                MessageBox.Show("You have entered an invalid email and password comibination.", "Invalid credentials",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var emailChecker = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (!emailChecker.IsValid(EnteredEmail.Text))
            {
                MessageBox.Show("You did not input a valid email adress, try again please.", "Invalid email input",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (ServerSide.Facade.Register(EnteredEmail.Text, EnteredPassword.Password))
                {
                    MessageBox.Show("You have been successfully registered.", "Registration success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("You already have an existing account, please use the login button.",
                        "Account already existing", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void PurchaseTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUserEmail == null)
            {
                var userResponse = MessageBox.Show("You must have an account in order to purchase a ticket. " +
                    "Do you want to create an account now?",
                    "Purchase attempt without account",
                    MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (userResponse == MessageBoxResult.Yes)
                {
                    TicketioTabs.SelectedIndex = 1;
                }
            }
            else
            {
                var ticketPurchaseWindow = new TicketPurchase(_availableTickets[AvailableTicketsGrid.SelectedIndex],
                    _currentUserEmail);
                ticketPurchaseWindow.Owner = this;
                ticketPurchaseWindow.ShowDialog();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurrentTicketsTab.IsSelected)
            {
                CurrentTicketHeader.Text = FetchCurrentTicketHeader();
                var currentTicket = ServerSide.Facade.GetCurrentTicket(_currentUserEmail);
                if (currentTicket == null)
                {
                    CurrentTicketImage.Source = null;
                }
                else
                {
                    Uri ticketUri = new(currentTicket.QRCodePath);
                    CurrentTicketImage.Source = new BitmapImage(ticketUri);
                }
            }
            if (TicketHistoryTab.IsSelected)
            {
                FetchTicketHistory();
            }
        }

        private string FetchCurrentTicketHeader()
        {
            if (_currentUserEmail == null)
            {
                return "Login to view your current ticket";
            }
            if (ServerSide.Facade.GetCurrentTicket(_currentUserEmail) == null)
            {
                return "You currently don't have any valid ticket";
            }
            return "Your valid ticket is listed below";
        }

        private void FetchTicketHistory()
        {
            if (_currentUserEmail != null)
            {
                _ticketHistory.Clear();

                var userTickets = ServerSide.Facade.GetTicketHistory(_currentUserEmail);
                foreach (var ticket in userTickets)
                {
                    _ticketHistory.Add(ticket);
                }
            }
        }

        private void TicketHistoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs? e)
        {
            FetchTicketHistory();
        }
    }
}
