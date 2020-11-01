using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TinyGeekGame.L1.Models;

namespace TinyGeekGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool drag = false;
        Nullable<Point> dragStart = null; 
        Storyboard story;
        CorrectAnswer answer;
        long totalScore;
        int positivePoint = 20;
        int negetivePoint = 5;
        CancellationTokenSource source;
        public MainWindow()
        {
            InitializeComponent();
            totalScore = 0;
            
        }

      

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            StartGameAsync();
            totalScore = 0;
            Score.Content = "TotalScore: " + totalScore.ToString();
            StartBtn.Visibility = Visibility.Hidden;
            TotalScore.Visibility = Visibility.Hidden;
        }

        public async Task StartGameAsync()
        {
            JapaneseButton.Visibility = Visibility.Visible;
            ChineseButton.Visibility = Visibility.Visible;
            KoreanButton.Visibility = Visibility.Visible;
            ThaiButton.Visibility = Visibility.Visible;

            MyImage.Visibility = Visibility.Visible;
            Score.Visibility = Visibility.Visible;

            int count = 0;
            while (count < 12)
            {

                MyImage.Dispatcher.Invoke(new Action(() =>
                {
                    ShowAnImage();
                    dragStart = null;
                    drag = false;
                }));
                source = new CancellationTokenSource();
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(10), source.Token);
                }
                catch
                {

                }
                
               
                Score.Content = "TotalScore: " + totalScore.ToString();

                count++;
            }
            GameEnd();
        }



        private void GameEnd()
        {
            JapaneseButton.Visibility = Visibility.Hidden;
            ChineseButton.Visibility = Visibility.Hidden;
            KoreanButton.Visibility = Visibility.Hidden;
            ThaiButton.Visibility = Visibility.Hidden;

            MyImage.Visibility = Visibility.Hidden;
            Score.Visibility = Visibility.Hidden;

            StartBtn.Content = "Play Again";
            StartBtn.Visibility = Visibility.Visible;
            TotalScore.Content= "TotalScore: " + totalScore.ToString();
            TotalScore.Visibility = Visibility.Visible;
        }

        private void ShowAnImage()
        {
            if (story != null)
            {
                Animations.SetAnimationToNull(MyImage);
            }
            MyImage.Opacity = 1;
            Random r = new Random();
            int DataNumber = r.Next(1, 12);
             answer= Data.MainData[DataNumber-1];
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,"+answer.ImagePath));
            MyImage.Fill = imgBrush;
            Canvas.SetLeft(MyImage, 400);
            Canvas.SetTop(MyImage, 0);
            story = Animations.MoveDown(MyImage, MainCanvas.ActualHeight);
            story.Begin(this,true);
            
        }

       

        private void rectangle_MouseMove(object sender, MouseEventArgs e)

        {
            if (dragStart != null && e.LeftButton == MouseButtonState.Pressed)
            {
                var element = (UIElement)sender;
                var p2 = e.GetPosition(MainCanvas);
                Canvas.SetLeft(element, p2.X - dragStart.Value.X);
                Canvas.SetTop(element, p2.Y - dragStart.Value.Y);
            }
          
        }

      

        private bool IsinThaiRectangle()
        {
            if (IntersectWith(ThaiButton, MyImage))
                return true;
            else
                return false;
        }

       

        private bool IsinChineseRectangle()
        {
            if (IntersectWith(ChineseButton, MyImage))
                return true;
            else
                return false;
        }

        private bool IsinKoreanRectangle()
        {
            if (IntersectWith(KoreanButton, MyImage))
                return true;
            else
                return false;
        }

        private bool IsinJapaneseRectangle()
        {
            if (IntersectWith(JapaneseButton, MyImage))
                return true;
            else
                return false;
        }

        private bool IntersectWith(Rectangle rect1, Rectangle rect2)
        {
            Rect r1 = new Rect(Canvas.GetLeft(rect1)-20, Canvas.GetTop(rect1)-20, rect1.Width+20, rect1.Height+20);
            Rect r2 = new Rect(Canvas.GetLeft(rect2), Canvas.GetTop(rect2), rect2.Width, rect2.Height);
            if (r1.IntersectsWith(r2))
                return true;
            else
                return false;
        }

        private void MyImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            drag = false;
            if (IsinJapaneseRectangle())
            {
                ScoringAsync("Japanese", 0,0);
            }
            else if (IsinKoreanRectangle())
            {
                ScoringAsync("Korean", 0,800);
            }
            else if (IsinChineseRectangle())
            {
                ScoringAsync("Chinese", 800,0);
            }
            else if (IsinThaiRectangle())
            {
                ScoringAsync("Thai",800,800);
                
            }
            else
            {
                Canvas.SetLeft(MyImage, 400);
                story.Resume(this);
            }

        }

        private async Task ScoringAsync(string Nationality,double toX,double toY)
        {
            story.Stop();
                story = Animations.TowardBoxAndFadeOut( MyImage,toX,toY);
                if (answer.Nationality.Equals(Nationality))
                    totalScore += positivePoint;
                else
                    totalScore -= negetivePoint;
            
            story.Begin(this);
            await Task.Delay(TimeSpan.FromSeconds(1));
            source.Cancel();
        }

        private void MyImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            story.Stop(this);
            var element = (UIElement)sender;
            dragStart = e.GetPosition(element);
            element.CaptureMouse();
        }

      
    }
}
