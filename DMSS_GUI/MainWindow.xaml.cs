using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMSS_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
        private void AddLog(string message)
        {
            Problem_Log.Items.Add(message);

            // 새로 추가된 항목을 자동으로 포커스(스크롤) 시킴
            Problem_Log.SelectedIndex = Problem_Log.Items.Count - 1;
            Problem_Log.ScrollIntoView(Problem_Log.SelectedItem);
        }

        int flag = 0;

        private void FireButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("발사 명령을 내리시겠습니까?", "발사 확인", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                if (flag == 0)
                {
                    SystemStatus_Text.Text = "발사 실패!";
                    SystemStatus_Text.Foreground = System.Windows.Media.Brushes.Red;
                    AddLog("발사 명령 수행 실패");
                }
                else
                {
                    SystemStatus_Text.Text = "발사 성공!";
                    SystemStatus_Text.Foreground = System.Windows.Media.Brushes.Green;
                    AddLog("발사 명령 수행 성공");
                }

                flag = new Random().Next(0, 2);

                // Progress Bar 값 테스트 변경 (임의 값)
                ReceiverA_Bar.Value = new Random().Next(30, 100);
                ReceiverB_Bar.Value = new Random().Next(10, 80);

                // 신호 비교 후 가까운 표적 표시
                UpdateSignalValues();
                HighlightCloserReceiver();
            }
            else
            {
                MessageBox.Show("취소되었습니다.", "발사 취소", MessageBoxButton.OK, MessageBoxImage.Information);
                AddLog("발사 명령 취소");
            }
               

            
        }
    }
}