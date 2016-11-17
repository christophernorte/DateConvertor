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

namespace DateConvertor
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tbTimestamp_KeyUp(object sender, KeyEventArgs e)
        {
            if(tbTimestamp.Text.Length >= 10)
            {
                try
                {
                    Double timestamp = Convert.ToDouble(tbTimestamp.Text);

                    DateTime datetime = UnixTimeStampToDateTime(timestamp);

                    tblResultDate.Text = datetime.ToString("dd MMMM yyyy H:mm:ss"); // Make format configurable
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);// Raised form exception
                }
            }else
            {
                tblResultDate.Text = String.Empty;
            }
        }

        public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? datetime = calendarPicker.SelectedDate;

            if(datetime.HasValue)
            {
                long timestamp = DateTimeToUnixTimestamp(datetime.Value);
                lblTimestampResult.Text = Convert.ToString(timestamp);

            }

        }

        private long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }
    }
}
