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

namespace Xceed.Wpf.AvalonDock.Layout
{
  public enum ChildrenTreeChange
  {
    /// <summary>
    /// Direct insert/remove operation has been perfomed to the group
    /// </summary>
    DirectChildrenChanged,

    /// <summary>
    /// An element below in the hierarchy as been added/removed
    /// </summary>
    TreeChanged
  }

  public class ChildrenTreeChangedEventArgs : EventArgs
  {
    public ChildrenTreeChangedEventArgs( ChildrenTreeChange change )
    {
      Change = change;
    }

    public ChildrenTreeChange Change
    {
      get; private set;
    }
  }
}
