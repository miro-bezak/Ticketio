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

        public MainWindow()
        {
            InitializeComponent();

            Header.Text += ServerSide.Facade.GetCurrentCity();

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
    }
}
