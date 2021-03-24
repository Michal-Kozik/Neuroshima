using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Neuroshima
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentObject = "gray";

        public MainWindow()
        {
            InitializeComponent();
            //CreateBoard();
        }

        private void CreateBoard(object sender, RoutedEventArgs e)
        {
            //GameboardCreator creator = new GameboardCreator();
            int rows, cols;
            bool rowsLoaded = Int32.TryParse(InputRows.Text, out rows);
            bool colsLoaded = Int32.TryParse(InputCols.Text, out cols);

            if (rowsLoaded && colsLoaded)
            {
                if (rows > 0 && rows < 26 && cols > 0 && cols < 26)
                {
                    for (int i = 0; i < rows + 1; i++)
                    {
                        for (int j = 0; j < cols + 1; j++)
                        {
                            // Oznaczenie pionowe.
                            if (j == 0 && i != 0)
                            {
                                char character = (char)(i + 65 - 1);
                                Border border = new Border()
                                {
                                    Width = 25,
                                    Height = 25,
                                    Child = new TextBlock() { Text = $"{character}", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center },
                                };
                                GameBoard.Children.Add(border);
                                Canvas.SetTop(border, i * 25);
                                Canvas.SetLeft(border, j * 25);
                            }
                            // Oznaczenie poziome.
                            else if (i == 0 && j != 0)
                            {
                                Border border = new Border()
                                {
                                    Width = 25,
                                    Height = 25,
                                    Child = new TextBlock() { Text = $"{j}", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center },
                                };
                                GameBoard.Children.Add(border);
                                Canvas.SetTop(border, i * 25);
                                Canvas.SetLeft(border, j * 25);
                            }
                            // Pola do interakcji.
                            else
                            {
                                if (i == 0 && j == 0)
                                    continue;
                                Rectangle rec = new Rectangle()
                                {
                                    Name = $"R_{i}_{j}",
                                    Width = 25,
                                    Height = 25,
                                    //Fill = Brushes.Gray,
                                    Fill = Brushes.Transparent,
                                    Stroke = Brushes.Black,
                                    StrokeThickness = 1,
                                };
                                rec.MouseEnter += new MouseEventHandler(MouseEnterField);
                                rec.MouseLeave += new MouseEventHandler(MouseLeftField);
                                rec.MouseDown += new MouseButtonEventHandler(ClickedDownFiled);
                                rec.MouseUp += new MouseButtonEventHandler(ClickedUpField);
                                GameBoard.Children.Add(rec);
                                Canvas.SetTop(rec, i * 25);
                                Canvas.SetLeft(rec, j * 25);
                                //Point point = new Point(j, i);
                                //creator.AddRectangle(point, rec);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Wartości powinny być z zakresu od 1 do 25.", "Niepoprawny przedział", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"Niepoprawne wartości!", "Niepoprawne wartości", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MouseEnterField(object sender, MouseEventArgs e)
        {
            Rectangle rec = sender as Rectangle;
            rec.StrokeThickness = 3;
        }

        private void MouseLeftField(object sender, MouseEventArgs e)
        {
            Rectangle rec = sender as Rectangle;
            rec.StrokeThickness = 1;
        }

        private void ClickedDownFiled(object sender, RoutedEventArgs e)
        {
            Rectangle rec = sender as Rectangle;
            rec.StrokeThickness = 5;
        }

        private void ClickedUpField(object sender, RoutedEventArgs e)
        {
            Rectangle rec = sender as Rectangle;
            rec.StrokeThickness = 3;
            PlaceObject(rec);
        }

        private void PlaceObject(Rectangle rec)
        {
            switch (currentObject)
            {
                case "green":
                    rec.Fill = Brushes.Green;
                    break;
                case "blue":
                    rec.Fill = Brushes.Blue;
                    break;
                case "yellow":
                    rec.Fill = Brushes.Yellow;
                    break;
                default:
                    rec.Fill = Brushes.Gray;
                    break;
            }
        }

        private void ChoosenObject(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            switch (rb.Name)
            {
                case "Green":
                    currentObject = "green";
                    break;
                case "Yellow":
                    currentObject = "yellow";
                    break;
                case "Blue":
                    currentObject = "blue";
                    break;
                default:
                    currentObject = "gray";
                    break;
            }
        }
    }
}
