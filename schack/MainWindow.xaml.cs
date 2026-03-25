using System.Collections;
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
        private Piece selectedPiece = null;
        private int selectedRow = -1;
        private int selectedCol = -1;
        private Button[,] squares = new Button[8, 8];
        private Piece[,] board = new Piece[8, 8];

        public MainWindow()
        {
           
            InitializeComponent();
            createChessboard();
            InitializeGame();
            RefreshBoard();



        }
        private void createChessboard()
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

                    square.Click += Square_Click;

                    // Tag för att hålla koll på positionen av pjäs
                    square.Tag = new Point(row, col);

                    chessboard.Children.Add(square);
                    squares[row, col] = square;

                }
            }
            }


        private void RefreshBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (board[row, col] != null)
                    {
                        // placerar ut symbolen
                        squares[row, col].Content = board[row, col].Symbol;
                    }
                    else
                    {
                        // toma rutor
                        squares[row, col].Content = "";
                    }
                }
            }
        }
        private void InitializeGame()
        {
            // svarta pjjäserna


            board[0, 0] = new Rook("Black", 0, 0);
            board[0, 1] = new Knight("Black", 0, 1);
            board[0, 2] = new Bishop("Black", 0, 2);
            board[0, 3] = new Queen("Black", 0, 3);
            board[0, 4] = new King("Black", 0, 4);
            board[0, 5] = new Bishop("Black", 0, 5);
            board[0, 6] = new Knight("Black", 0, 6);
            board[0, 7] = new Rook("Black", 0, 7);

            // placerar ut alla svarta bönder dynamiskt
            for (int col = 0; col < 8; col++)
            {
                board[1, col] = new Pawn("Black", 1, col);
            }

            board[7, 0] = new Rook("White", 7, 0);
            board[7, 1] = new Knight("White", 7, 1);
            board[7, 2] = new Bishop("White", 7, 2);
            board[7, 3] = new Queen("White", 7, 3);
            board[7, 4] = new King("White", 7, 4);
            board[7, 5] = new Bishop("White", 7, 5);
            board[7, 6] = new Knight("White", 7, 6);
            board[7, 7] = new Rook("White", 7, 7);

            // placera ut alla vita bönder dynamiskt
            for (int col = 0; col < 8; col++)
            {
                board[6, col] = new Pawn("White", 6, col);
            }

        }
        private void Square_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point pos = (Point)clickedButton.Tag;
            int row = (int)pos.X;
            int col = (int)pos.Y;

            if (selectedPiece == null)
            {
                // 
                if (board[row, col] != null)
                {
                    selectedPiece = board[row, col];
                    selectedRow = row;
                    selectedCol = col;

                }
            }
            else
            {
                // kollar om det är ett gyltigt drag
                if (selectedPiece.IsValidMove(row, col, board))
                {
                    // Updaterar brädan visuellt
                    board[row, col] = selectedPiece;
                    board[selectedRow, selectedCol] = null;

                    // Updaterar pjäsens position
                    selectedPiece.Row = row;
                    selectedPiece.Col = col;
                }

                // 
                selectedPiece = null;
                RefreshBoard();
            }
        }
    } 
}

