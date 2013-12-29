/*****************   NCore Softwares Pvt. Ltd., India   **************************

   ColorBox

   Copyright (C) 2013 NCore Softwares Pvt. Ltd.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://colorbox.codeplex.com/license

***********************************************************************************/

using System;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace ColorBox
{
    class GradientStopAdder : Button
    {
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            if (e.Source is GradientStopAdder && this.ColorBox != null)
            {
                Button btn = e.Source as Button;
                
                GradientStop _gs = new GradientStop();
                _gs.Offset = Mouse.GetPosition(btn).X / btn.ActualWidth;
                _gs.Color = this.ColorBox.Color;
                this.ColorBox.Gradients.Add(_gs);
                this.ColorBox.SelectedGradient = _gs;
                this.ColorBox.SetBrush();
            }
        }

        public ColorBox ColorBox
        {
            get { return (ColorBox)GetValue(ColorBoxProperty); }
            set { SetValue(ColorBoxProperty, value); }
        }
        public static readonly DependencyProperty ColorBoxProperty =
            DependencyProperty.Register("ColorBox", typeof(ColorBox), typeof(GradientStopAdder));       
    }    
}
