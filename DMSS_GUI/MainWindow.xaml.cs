using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMSS_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   private int flag = 0;
        private bool isManualMode = false;
        public MainWindow()
        {
            InitializeComponent();
            UpdateProgress(70, ReceiverA_Bar);
            UpdateProgress(30, ReceiverB_Bar);
        }

        private void UpdateProgress(double percentage, System.Windows.Controls.ProgressBar progressBar)
        {
            progressBar.Value = percentage;
        }

        private void UpdateSignalValues()
        {
            ReceiverA_Value.Text = ReceiverA_Bar.Value.ToString("0");
            ReceiverB_Value.Text = ReceiverB_Bar.Value.ToString("0");
        }

        private void HighlightCloserReceiver()
        {
            if (ReceiverA_Bar.Value > ReceiverB_Bar.Value)
            {
                ReceiverA_Border.Background = Brushes.Yellow; // 가까운 수신부를 노란색으로 강조
                ReceiverB_Border.Background = Brushes.White;
            }
            else
            {
                ReceiverA_Border.Background = Brushes.White;
                ReceiverB_Border.Background = Brushes.Yellow;
            }
        }
        private async Task AddLogAsync(string message)
        {
            Problem_Log.Items.Add(message);

            await Task.Delay(10); // UI 업데이트를 기다림
            await Dispatcher.BeginInvoke(new Action(() =>
            {
                Problem_Log.SelectedIndex = Problem_Log.Items.Count - 1;
                Problem_Log.ScrollIntoView(Problem_Log.SelectedItem);
            }), DispatcherPriority.Background);
        }

        private void ModeChanged(object sender, RoutedEventArgs e)
        {
            if(Mode_Manual != null)
            {
                if (Mode_Manual.IsChecked == true)
                {
                    isManualMode = true;
                    SelectModePanel.Visibility = Visibility.Visible;
                    Target_A.IsEnabled = true;
                    Target_B.IsEnabled = true;
                }
                else
                {
                    isManualMode = false;
                    SelectModePanel.Visibility = Visibility.Collapsed;
                    Target_A.IsEnabled = false;
                    Target_B.IsEnabled = false;
                }
            }
        }

        private async void FireButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("발사 명령을 내리시겠습니까?", "발사 확인", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                if (flag == 0)
                {
                    SystemStatus_Text.Text = "발사 실패!";
                    SystemStatus_Text.Foreground = System.Windows.Media.Brushes.Red;
                    await AddLogAsync("발사 명령 수행 실패");
                }
                else
                {
                    SystemStatus_Text.Text = "발사 성공!";
                    SystemStatus_Text.Foreground = System.Windows.Media.Brushes.Green;
                    await AddLogAsync("발사 명령 수행 성공");           
                }

                flag = new Random().Next(0, 2);

                // Progress Bar 값 테스트 변경 (임의 값)
                ReceiverA_Bar.Value = new Random().Next(30, 100);
                ReceiverB_Bar.Value = new Random().Next(10, 80);

                // 신호 비교 후 가까운 표적 표시
                UpdateSignalValues();
                HighlightCloserReceiver();

                //Thread.Sleep(3000);
                //SystemStatus_Text.Text = "발사 준비";
                //SystemStatus_Text.Foreground = System.Windows.Media.Brushes.Blue;

            }
            else
            {
                MessageBox.Show("취소되었습니다.", "발사 취소", MessageBoxButton.OK, MessageBoxImage.Information);
                await AddLogAsync("발사 명령 취소");
            }
          
        }

        private void ReceiverA_Bar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void ReceiverB_Bar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}