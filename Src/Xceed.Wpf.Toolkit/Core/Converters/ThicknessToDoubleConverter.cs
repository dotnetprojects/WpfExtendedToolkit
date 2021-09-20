﻿/*************************************************************************************
   
   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace Xceed.Wpf.Toolkit.Core.Converters
{
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double thickness = 1.0;

            if (value != null)
                thickness = ((Thickness)value).Top;

            return thickness;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double thickness = 1.0;

            if (value != null)
                thickness = (double)value;

            return new Thickness(thickness);
        }
    }
}
