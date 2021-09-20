﻿/*************************************************************************************
   
   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Collections.Generic;

namespace Xceed.Wpf.AvalonDock.Controls
{
  internal interface IOverlayWindow
  {
    IEnumerable<IDropTarget> GetTargets();

    void DragEnter( LayoutFloatingWindowControl floatingWindow );
    void DragLeave( LayoutFloatingWindowControl floatingWindow );

    void DragEnter( IDropArea area );
    void DragLeave( IDropArea area );

    void DragEnter( IDropTarget target );
    void DragLeave( IDropTarget target );
    void DragDrop( IDropTarget target );
  }
}
