using Avalonia.Controls;
using Avalonia.Interactivity;

namespace MoveTransition.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        int columnIndex = (sender as Button)!.GetValue(Grid.ColumnProperty);
        int rowIndex = (sender as Button)!.GetValue(Grid.RowProperty);

        Element.SetValue(Grid.ColumnProperty, columnIndex);
        Element.SetValue(Grid.RowProperty, rowIndex);
    }
}
