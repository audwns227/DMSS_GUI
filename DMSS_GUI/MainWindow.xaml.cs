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

        int flag = 0;

        private void FireButton_Click(object sender, RoutedEventArgs e)
        {
            if(flag == 0)
            {
                SystemStatus_Text.Text = "발사 실패!";
                SystemStatus_Text.Foreground = System.Windows.Media.Brushes.Red;
                Problem_Log.Items.Add("발사 명령 수행 실패");
            }
            else
            {
                SystemStatus_Text.Text = "발사 성공!";
                SystemStatus_Text.Foreground = System.Windows.Media.Brushes.Green;
                Problem_Log.Items.Add("발사 명령 수행됨");
            }

            flag = new Random().Next(0, 2);

            // Progress Bar 값 테스트 변경 (임의 값)
            ReceiverA_Bar.Value = new Random().Next(30, 100);
            ReceiverB_Bar.Value = new Random().Next(10, 80);

            // 신호 비교 후 가까운 표적 표시
            if (ReceiverA_Bar.Value > ReceiverB_Bar.Value)
                TargetIndicator.Text = "A와 더 가까움";
            else
                TargetIndicator.Text = "B와 더 가까움";
        }
    }
}