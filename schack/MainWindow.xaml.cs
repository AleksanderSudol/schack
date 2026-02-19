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
        public MainWindow()
        {
            InitializeComponent();
            createchessboard();

        }
        private void createchessboard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    
                    Button square = new Button();

                    //kollar om rad och kolumn kordinaternas summa är ett jämnt eller ojämnt tal
                    if ((row + col) % 2 == 0)
                        square.Background = Brushes.White;
                    else
                        square.Background = Brushes.Black;

                    Grid.SetRow(square, row);
                    Grid.SetColumn(square, col);

                    chessboard.Children.Add(square);

                }
            }
        }
    }
}