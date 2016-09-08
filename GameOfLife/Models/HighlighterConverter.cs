namespace GameOfLife
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public class HighlighterConverter : IMultiValueConverter
    {
        #region Implementation of IMultiValueConverter

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[1] is DataRow)
            {
                var cell = (DataGridCell)values[0];
                var row = (DataRow)values[1];
                var columnName = cell.Column.SortMemberPath;

                if (row[columnName] != DBNull.Value)
                    return new SolidColorBrush(Colors.Black);
            }
            return SystemColors.AppWorkspaceColor;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}