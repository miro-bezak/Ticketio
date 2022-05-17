using ServerSide.Data;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for TicketPurchase.xaml
    /// </summary>
    public partial class TicketPurchase : Window
    {
        private readonly TicketType _ticketType;
        private readonly string _userEmail;
        public TicketPurchase(TicketType selectedTicket, string userEmail)
        {
            InitializeComponent();
            _ticketType = selectedTicket;
            _userEmail = userEmail;

            DurationTextBlock.Text = _ticketType.Duration;
            TariffTextBlock.Text = _ticketType.Tariff;
            PriceTextBlock.Text = _ticketType.Price;
        }

        private async void ConfirmPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServerSide.Facade.GetCurrentTicket(_userEmail) != null)
            {
                var result = MessageBox.Show("You already have an active ticket, do you really want to buy another one?",
                    "Active ticket found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    Close();
                    return;
                }
            }

            var ticketTask = Task.Run(() => ServerSide.Facade.PurchaseTicket(_userEmail, _ticketType));
            Close();
            await ticketTask;
            MessageBox.Show("The new ticket is available.", "New ticket",
                   MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
