using ServerSide.Data;
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
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for TicketPurchase.xaml
    /// </summary>
    public partial class TicketPurchase : Window
    {
        private readonly TicketType _ticketType;
        private readonly string _user;
        public TicketPurchase(TicketType selectedTicket)
        {
            InitializeComponent();
            _ticketType = selectedTicket;

            DurationTextBlock.Text = _ticketType.Duration;
            TariffTextBlock.Text = _ticketType.Tariff;
            PriceTextBlock.Text = _ticketType.Price;
        }

        private void ConfirmPurchaseButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
