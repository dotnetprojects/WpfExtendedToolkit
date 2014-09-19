/*************************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
    public class IpTextBoxEditor : TypeEditor<TextBox>
    {
        public IpTextBoxEditor()
        {
            _ipAddressConverter = new IpAddressConverter(this);
        }

        protected override void SetControlProperties()
        {
            Editor.BorderThickness = new System.Windows.Thickness(0);
            //this.Editor.ValueDataType = typeof (IPAddress);
            //this.Editor.Mask = "990\\.990\\.990\\.999";
            //this.Editor.PromptChar = ' ';
        }

        protected override void SetValueDependencyProperty()
        {
            this.ValueProperty = TextBox.TextProperty;
        }

        protected override void ResolveValueBinding(PropertyItem propertyItem)
        {
            var _binding = new Binding("Value");
            _binding.Source = propertyItem;
            _binding.UpdateSourceTrigger = UpdateSourceTrigger.Default;
            _binding.Mode = BindingMode.TwoWay;
            _binding.Converter = CreateValueConverter();
            BindingOperations.SetBinding(Editor, ValueProperty, _binding);
        }

        protected override IValueConverter CreateValueConverter()
        {
            return _ipAddressConverter;
        }

        private static IpAddressConverter _ipAddressConverter;

        private class IpAddressConverter : IValueConverter
        {
            private IpTextBoxEditor _ipTextBoxEditor;

            public IpAddressConverter(IpTextBoxEditor ipTextBoxEditor)
            {
                this._ipTextBoxEditor = ipTextBoxEditor;
            }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var ipAdress = value as IPAddress;
                if (ipAdress != null)
                {
                    //_ipTextBoxEditor.Editor.Value = ipAdress; //.ToString();
                    return ipAdress.ToString();
                }
                return null;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                IPAddress retVal;
                if (value != null && !string.IsNullOrWhiteSpace((string)value))
                {
                    if (IPAddress.TryParse(value.ToString().Replace(" ", ""), out retVal))
                        return retVal;
                }
                return null;
            }
        }
    }
}
