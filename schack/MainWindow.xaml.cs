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


namespace schack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button[,] squares = new Button[8, 8];
        public MainWindow()
        {
           
            InitializeComponent();
            createchessboard();
            DeployPieces(); 


        }
        private void createchessboard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    //buttons eftersom man klickar på och det ser bra ut med hover effekten
                    Button square = new Button();
                    square.FontSize = 40;

                    //kollar om positionen är jämn eller udda för att bestämma färgen på rutan, använder modulo för att det inte hade funkat med vanlig division.
                    if ((row + col) % 2 == 0)
                        square.Background = Brushes.Beige;
                    else
                        square.Background = Brushes.Brown;

                    Grid.SetRow(square, row);
                    Grid.SetColumn(square, col);

                    chessboard.Children.Add(square);

                    squares[row, col] = square;  
                }
            }
        }
            private void DeployPieces()
        {
            // placera ut svarta pjäser som inte är bönder
            squares[0, 0].Content = "♜";
            squares[0, 1].Content = "♞";
            squares[0, 2].Content = "♝";
            squares[0, 3].Content = "♛";
            squares[0, 4].Content = "♚";
            squares[0, 5].Content = "♝";
            squares[0, 6].Content = "♞";
            squares[0, 7].Content = "♜";

            // placera ut svarta bönder
            for (int col = 0; col < 8; col++)
                squares[1, col].Content = "♟";

            // placera ut vita bönder
            for (int col = 0; col < 8; col++)
                squares[6, col].Content = "♙";

            // placera ut vita pjäser som inte är bönder
            squares[7, 0].Content = "♖";
            squares[7, 1].Content = "♘";
            squares[7, 2].Content = "♗";
            squares[7, 3].Content = "♕";
            squares[7, 4].Content = "♔";
            squares[7, 5].Content = "♗";
            squares[7, 6].Content = "♘";
            squares[7, 7].Content = "♖";
        }

        
    }
}
