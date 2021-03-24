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
            CreateBoard();
        }

        private void CreateBoard()
        {
            GameboardCreator creator = new GameboardCreator();
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    if (j == 0 && i != 0)
                    {
                        Border border = new Border()
                        {
                            Width = 25,
                            Height = 25,
                            Child = new TextBlock() { Text = $"Y{i}", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center },
                        };
                        GameBoard.Children.Add(border);
                        Canvas.SetTop(border, i * 25);
                        Canvas.SetLeft(border, j * 25);
                    }
                    else if (i == 0 && j != 0)
                    {
                        Border border = new Border()
                        {
                            Width = 25,
                            Height = 25,
                            Child = new TextBlock() { Text = $"X{j}", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center },
                        };
                        GameBoard.Children.Add(border);
                        Canvas.SetTop(border, i * 25);
                        Canvas.SetLeft(border, j * 25);
                    }
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
                            Fill = null,
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
                        Point point = new Point(j, i);
                        creator.AddRectangle(point, rec);
                    }
                }
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
