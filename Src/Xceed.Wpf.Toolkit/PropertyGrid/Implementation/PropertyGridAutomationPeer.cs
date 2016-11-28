using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation.Peers;

namespace Xceed.Wpf.Toolkit.PropertyGrid
{
    public class PropertyGridAutomationPeer : FrameworkElementAutomationPeer
    {
        public PropertyGridAutomationPeer(PropertyGrid owner) : base(owner)
        {
        }

        protected override string GetClassNameCore()
        {
            return "PropertyGrid";
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Custom;
        }

        protected override List<AutomationPeer> GetChildrenCore()
        {
            var control = (PropertyGrid)Owner;

            var itemsControl = control.Template.FindName("PART_PropertyItemsControl", control) as PropertyItemsControl;
            if(itemsControl != null)
            {
                var peer = CreatePeerForElement(itemsControl);
                return new[] { peer }.ToList();
            }

            return null;
        }
    }
}
