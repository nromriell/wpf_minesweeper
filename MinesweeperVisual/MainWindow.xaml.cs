using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MinesweeperVisualGit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    /* Enumeration for different difficulty levels*/
    enum Difficulty
    {
        Easy,
        Medium,
        Hard,
    }
    public partial class MainWindow : Window, GameDelegate
    {

        private GameController game;
        private static Action EmptyDelegate = delegate () { };
        private Grid grid;
        private Timer gameTimer;
        private int time;
        private int currentDepth, maxDepth;
        private int lasthighscore;
        double cellSize, topBarHeight;

        /*View setup, grid initialization*/
        public MainWindow()
        {
            InitializeComponent();
            grid = new Grid();
            grid.Background = new SolidColorBrush(Colors.Gray);
            grid.Width = 320;
            grid.Height = 320;
            cellSize = 40;
            topBarHeight = 120;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;
            menubutton.Click += showMenu;
            gamebutton.Click += resetGame;
            newMGameButton.Click += resetGame;
            newEGameButton.Click += resetGame;
            newHGameButton.Click += resetGame;
            Image gameButtonImage = new Image();
            gameButtonImage.Source = new BitmapImage(new Uri(@"images/face_normal.png", UriKind.Relative));
            gamebutton.Content = gameButtonImage;
            mainArea.Children.Add(grid);
            setupWithDifficulty(Difficulty.Easy);
            Canvas.SetLeft(resultsCanvas, this.Width * 0.50-resultsLabel.ActualWidth*0.50);
            Canvas.SetTop(resultsCanvas, this.Height * 0.50);
        }
        /* On click event for showing dropdown menu*/
        private void showMenu(object c, EventArgs e)
        {
            if(gamemenu.Visibility == Visibility.Visible)
            {
                gamemenu.Visibility = Visibility.Hidden;
            }
            else
            {
                gamemenu.Visibility = Visibility.Visible;
            }
        }

        private void hideMenu()
        {
            gamemenu.Visibility = Visibility.Hidden;
        }
        /*Clears game data and resets the game*/
        private void resetGame(object sender, EventArgs e)
        {
            hideMenu();
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            if (sender == gamebutton)
            {
                setupWithDifficulty(game.difficulty);
            }else if(sender == newEGameButton)
            {
                setupWithDifficulty(Difficulty.Easy);
            }else if(sender == newMGameButton)
            {
                setupWithDifficulty(Difficulty.Medium);
            }else if(sender == newHGameButton)
            {
                setupWithDifficulty(Difficulty.Hard);
            }
        }

        private void resetTimer()
        {
            gameTimer = new Timer();
            gameTimer.Elapsed += updateTimer;
            gameTimer.Interval = 1000;
            gameTimer.Start();
        }

        private void updateTimer(object source, ElapsedEventArgs e)
        {
            time += 1;
            this.Dispatcher.Invoke((Action)delegate ()
            {
                timelabel.Text = time.ToString();
            });
        }

        private void updateTime()
        {
            timelabel.Text = time.ToString();
        }
        /*Creates new game with given difficulty*/
        private void setupWithDifficulty(Difficulty difficulty)
        {
            resultsLabel.Visibility = Visibility.Hidden;
            resultsLabel.Text = "You won!!!";
            if (gameTimer  != null) gameTimer.Stop();
            game = new GameController(difficulty);
            game.setDelegate(this);
            game.setMinesLeftLabel(minesleftlabel);
            grid.IsHitTestVisible = true;
            time = 0;
            updateTime();
            switch (difficulty)
            {
                case Difficulty.Easy:
                    difficultylabel.Text = "Easy";
                    break;
                case Difficulty.Medium:
                    difficultylabel.Text = "Medium";
                    break;
                case Difficulty.Hard:
                    difficultylabel.Text = "Hard";
                    break;
                default:
                    break;
            }
            double width = cellSize * game.width;
            double height = cellSize * game.height;
            double windowHeight = height + topBarHeight;
            this.Width = width+cellSize*0.35f;
            this.Height = windowHeight-cellSize*0.10f;
            grid.Width = width;
            grid.Height = height;
            for (int i = 0; i < game.width; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                grid.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i < game.height; i++)
            {
                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);
            }
            for (int i = 0; i < game.width; i++)
            {
                for (int j = 0; j < game.height; j++)
                {
                    Button button = new Button();
                    button.Background = new SolidColorBrush(Colors.White);
                    button.FontSize = cellSize * 0.60f;
                    button.HorizontalAlignment = HorizontalAlignment.Stretch;
                    button.VerticalAlignment = VerticalAlignment.Stretch;
                    button.BorderThickness = new Thickness(1);
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("images/cellnormal.png", UriKind.Relative));
                    button.Content = image;
                    Grid.SetColumn(button, i); //Gives the button its column position
                    Grid.SetRow(button, j); //Gives the button its row position
                    grid.Children.Add(button);
                    game.setCellButton(i, j, button, image);
                }
            }
        }



        private void addResultsLabel()
        {
            Canvas.SetLeft(resultsCanvas, this.Width * 0.50 - resultsLabel.ActualWidth * 0.50);
            resultsLabel.Visibility = Visibility.Visible;
        }
        

        void GameDelegate.gameEnded(bool won)
        {
            grid.IsHitTestVisible = false;
            if(gameTimer != null) gameTimer.Stop();
            if (won)
            {
                resultsLabel.Text = "You won!";
                resultsLabel.Foreground = new SolidColorBrush(Colors.Green);
                resultsLabel.Background = new SolidColorBrush(Colors.LightGray);
                addResultsLabel();
            }else
            {
                resultsLabel.Text = "You Lost";
                resultsLabel.Background = new SolidColorBrush(Colors.LightGray);
                resultsLabel.Foreground = new SolidColorBrush(Colors.Red);
                addResultsLabel();
            }
        }

        void GameDelegate.gameStarted()
        {
            resetTimer();
        }
    }
}
