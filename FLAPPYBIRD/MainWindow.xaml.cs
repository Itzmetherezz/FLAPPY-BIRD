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
using System.Windows.Threading;

namespace FLAPPYBIRD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();

        double score;
        int gravity;
        bool gameOver;
        Rect flappyBirdHitBox;
        //this project took a lot of effort and difficult coding as it is made purely on c#. please consider voting

       
        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += MainEventTimer;
            StartGame();
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainEventTimer(object sender, EventArgs e)
        {
            
            txtScore.Content = "Score: " + score;
            flappyBirdHitBox = new Rect(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width - 5, flappyBird.Height -5);

            Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravity);

            if (Canvas.GetTop(flappyBird) > MyCanvas.ActualHeight - flappyBird.ActualHeight|| Canvas.GetTop(flappyBird) <0)
            {
                StopGame();
            }


            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "obs1" || (string)x.Tag == "obs2" || (string)x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);

                    if (Canvas.GetLeft(x) < -100)
                    {
                        Canvas.SetLeft(x, 800);

                        score += .5;
                    }
                    Rect pipeHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (flappyBirdHitBox.IntersectsWith(pipeHitBox))
                    {
                        StopGame();
                    }



                }
                if ((string)x.Tag == "cloud")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 2);
                    if (Canvas.GetLeft(x) < -250)
                    {
                         Canvas.SetLeft(x, 800);
                    }


                }
                        
                        


                
            
            }


                
            



            
        }


        private void KeyIsDown(object sender, KeyEventArgs e) 
        {
            if (e.Key == Key.Space)
            {
                flappyBird.RenderTransform = new RotateTransform(-20, flappyBird.Width / 2, flappyBird.Height / 2);
                gravity = -11;
            }
            if (e.Key == Key.R && gameOver == true)
            {
                StartGame();    
            }




        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
                gravity = 10;
            }
            if (e.Key == Key.R && gameOver == true)
            {
                StartGame(); 
            }   

        }


        private void StartGame()
        {
            MyCanvas.Focus();
            int temp = 300;
            score = 0;
            gameOver = false;
            Canvas.SetTop(flappyBird, 190); 

            foreach (var x in MyCanvas.Children.OfType<Image>()) 
            {
                if ((string)x.Tag == "obs1")
                {
                    Canvas.SetLeft(x, 500);




                }
                if((string)x.Tag == "obs2")
                {
                    Canvas.SetLeft(x, 800);     
                }

                if((string)x.Tag == "obs3")
                {
                    Canvas.SetLeft (x, 1100);
                }

                if ((string)x.Tag == "cloud")
                {
                    Canvas.SetLeft (x, 300 + temp);
                    temp = 800;


                }


            }
            GameOverScreen.Visibility = Visibility.Collapsed;

            gameTimer.Start();
            




        }
        private void StopGame()

        {
            gameTimer.Stop(); 
            gameOver = true;
            txtScore.Content += " Game Over !! Press R to Retry";
            GameOverScreen.Visibility = Visibility.Visible;


            
        }


         



        
    }
}