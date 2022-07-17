using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF_learning_notes.Form;

public partial class BlockMove
{
    public BlockMove()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 键盘事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BlockMove_OnKeyDown(object sender, KeyEventArgs e)
    {
        // 获取GridColumn
        var children = gridContent.Children;
        Border? currentBorder = null;
        foreach (UIElement child in children)
        {
            if (child is Border border && (border?.Background as SolidColorBrush)!.Color.Equals(Colors.White))
            {
                currentBorder = border;
            }
        }

        // 获取坐标
        var coordinate = currentBorder?.Name.Split("_");
        var row = Convert.ToInt32(coordinate?[1]);
        var column = Convert.ToInt32(coordinate?[2]);


        switch (e.Key)
        {
            // 判断键盘方向
            case Key.Up:
                row = row - 1 >= 0 ? row - 1 : row;
                break;
            case Key.Down:
                row = row + 1 <= 2 ? row + 1 : row;
                break;
            case Key.Left:
                column = column - 1 >= 0 ? column - 1 : column;
                break;
            case Key.Right:
                column = column + 1 <= 2 ? column + 1 : column;
                break;
            case Key.Escape:
            {
                var messageBoxResult = MessageBox.Show("确定退出吗？", "提示", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }

                break;
            }
        }

        var moveBorder = gridContent.FindName("b_" + row + "_" + column)!;
        currentBorder!.Background = new SolidColorBrush(Colors.Transparent);
        (moveBorder as Border)!.Background = new SolidColorBrush(Colors.White);
    }

    /// <summary>
    /// 方块初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BlockMove_OnLoaded(object sender, RoutedEventArgs e)
    {
        var random = new Random();
        var row = random.Next(0, 2);
        var column = random.Next(0, 2);

        var border = gridContent.FindName("b_" + row + "_" + column);
        (border as Border)!.Background = new SolidColorBrush(Colors.White);
    }
}