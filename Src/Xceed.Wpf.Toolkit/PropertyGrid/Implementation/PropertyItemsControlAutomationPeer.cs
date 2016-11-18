using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace Xceed.Wpf.Toolkit.PropertyGrid
{
    public class PropertyItemsControlAutomationPeer : ItemsControlAutomationPeer
    {
        public PropertyItemsControlAutomationPeer(ItemsControl owner) : base(owner)
        {
        }

        protected override ItemAutomationPeer CreateItemAutomationPeer(object item)
        {
            var propertyItem = item as PropertyItem;
            if (propertyItem != null)
            {
                return (ItemAutomationPeer)CreatePeerForElement(propertyItem);
            }

            return null;
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.List;
        }

        protected override string GetClassNameCore()
        {
            return "PropertyItemsControl";
        }
    }
}
