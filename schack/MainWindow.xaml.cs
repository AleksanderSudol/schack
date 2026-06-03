using System;
using System.Collections;
using System.Collections.ObjectModel;
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
    public partial class MainWindow : Window
    {
        // uppdaterar listan automatiskt
        public ObservableCollection<Drag> MoveHistory { get; set; } = new ObservableCollection<Drag>();
        private int currentTurnNumber = 1;

        private string currentTurn = "White";
        private Piece selectedPiece = null;
        private int selectedRow = -1;
        private int selectedCol = -1;
        private Button[,] squares = new Button[8, 8];
        private Piece[,] board = new Piece[8, 8];

        // === NÄTVERK: Verktyg för att skicka data över internet ===
        private static readonly System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            createChessboard();
            InitializeGame();
            RefreshBoard();
        }

        //visuella bräddan
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
            // svarta pjäserna
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

        // MAsync för att inte frysa UIet
        private async void Square_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point pos = (Point)clickedButton.Tag;
            int row = (int)pos.X;
            int col = (int)pos.Y;

            if (selectedPiece == null)
            {
                // kollar om det finns en pjäs där och om det är din tur
                if (board[row, col] != null && board[row, col].Color == currentTurn)
                {
                    selectedPiece = board[row, col];
                    selectedRow = row;
                    selectedCol = col;
                }
            }
            else
            {
                // kollar om det är ett giltigt drag
                if (selectedPiece.IsValidMove(row, col, board))
                {
                    string moveNotation = GenerateMoveNotation(selectedPiece, row, col);

                    if (currentTurn == "White")
                    {
                        MoveHistory.Add(new Drag
                        {
                            TurnNumber = currentTurnNumber,
                            WhiteMove = moveNotation,
                            BlackMove = ""
                        });
                    }
                    else
                    {
                        // 1. Hämta ut sista draget från listan
                        var lastTurn = MoveHistory[MoveHistory.Count - 1];

                        // 2. Uppdatera svarts drag i minnet
                        lastTurn.BlackMove = moveNotation;

                        // 3. Ta bort den gamla raden och lägg till den uppdaterade raden igen.
                        // Detta tvingar listan på skärmen att rita om raden och visa svarts drag!
                        MoveHistory.RemoveAt(MoveHistory.Count - 1);
                        MoveHistory.Add(lastTurn);

                        currentTurnNumber++;
                    }

                    // === NÄTVERK: Paketera draget i din schackDrag-modell ===
                    var networkMove = new schackDrag
                    {
                        TurnColor = currentTurn,
                        Notation = moveNotation
                    };

                    try
                    {
                        
                        await System.Net.Http.Json.HttpClientJsonExtensions.PostAsJsonAsync(_client, "https://localhost:7293/api/moves", networkMove);
                    }
                    catch (Exception ex)
                    {
                        // Visar felmeddelande i stället för att krascha spelet om API:et inte körs
                        MessageBox.Show("Kunde inte skicka draget till API:et: " + ex.Message);
                    }

                    // Uppdaterar brädan visuellt
                    board[row, col] = selectedPiece;
                    board[selectedRow, selectedCol] = null;

                    // Uppdaterar pjäsens position
                    selectedPiece.Row = row;
                    selectedPiece.Col = col;

                    // Byter tur
                    if (currentTurn == "White")
                    {
                        currentTurn = "Black";
                    }
                    else
                    {
                        currentTurn = "White";
                    }
                }

                selectedPiece = null;
                RefreshBoard();
            }
        }

        // översätter till schackspråk
        private string GetSquareName(int row, int col)
        {
            char file = (char)('a' + col);
            int rank = 8 - row;
            return $"{file}{rank}";
        }

        // skapar själva strängen som representerar draget i schacknotation
        private string GenerateMoveNotation(Piece piece, int toRow, int toCol)
        {
            string squareName = GetSquareName(toRow, toCol);

            // bönderna skrivs bara ut som rutans namn
            if (piece.GetType().Name == "Pawn")
            {
                return squareName;
            }

            // K är upptaget av kungen så knight får N
            if (piece.GetType().Name == "Knight")
            {
                return "N" + squareName; // e.g., "Nf3"
            }

            //Tar första bokstaven av de resterande pjäserna
            string pieceLetter = piece.GetType().Name.Substring(0, 1);
            return pieceLetter + squareName;
        }
    }

    // === NÄTVERK: Lokal spegling av din API-modell så att WPF kan paketera datan ===
    public class schackDrag
    {
        public string TurnColor { get; set; }
        public string Notation { get; set; }
    }
}