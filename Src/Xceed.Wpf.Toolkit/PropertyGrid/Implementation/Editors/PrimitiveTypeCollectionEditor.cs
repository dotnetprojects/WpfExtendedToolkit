/*************************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Collections.Generic;
using System.Linq;

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
  public class PrimitiveTypeCollectionEditor : TypeEditor<PrimitiveTypeCollectionControl>
  {
    protected override void SetControlProperties()
    {
      Editor.BorderThickness = new System.Windows.Thickness( 0 );
      Editor.Content = "(Collection)";
    }

    protected override void SetValueDependencyProperty()
    {
      ValueProperty = PrimitiveTypeCollectionControl.ItemsSourceProperty;
    }

    protected override void ResolveValueBinding( PropertyItem propertyItem )
    {
      Editor.ItemsSourceType = propertyItem.PropertyType;

      var icollection = propertyItem.PropertyType.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>));
      if (icollection != null)
      {
          Editor.ItemType = icollection.GetGenericArguments()[0];
      }
      else
      {
          Editor.ItemType = propertyItem.PropertyType.GetGenericArguments()[ 0 ];
      }
      base.ResolveValueBinding( propertyItem );
    }
  }
}
