﻿/*************************************************************************************
   
   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Windows;

namespace Xceed.Wpf.Toolkit.Panels
{
  public class ChildEnteredEventArgs : RoutedEventArgs
  {
    #region Constructors

    public ChildEnteredEventArgs( UIElement child, Rect arrangeRect )
    {
      _child = child;
      _arrangeRect = arrangeRect;
    }

    #endregion

    #region ArrangeRect Property

    public Rect ArrangeRect
    {
      get
      {
        return _arrangeRect;
      }
    }

    private readonly Rect _arrangeRect;

    #endregion

    #region Child Property

    public UIElement Child
    {
      get
      {
        return _child;
      }
    }

    private readonly UIElement _child;

    #endregion
  }
}
