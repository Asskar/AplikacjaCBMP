using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Model;

namespace AppCBMP.Helpers.Converters
{
    public class CollectionToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
//            if (targetType != typeof(string))
//                throw new InvalidOperationException("The target must be a String");
//            return string.Join(", ", ((ObservableCollection<Position>.n)value).ToArray());
            ObservableCollection<Position> listaPozycji = value as ObservableCollection<Position>;
            string pozycyjeString = "";
            if (listaPozycji == null)
                return pozycyjeString;
            return listaPozycji.Count>1
                ? "operatorskie"
                : listaPozycji[0].Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}