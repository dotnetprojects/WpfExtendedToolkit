using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace Xceed.Wpf.Toolkit.PropertyGrid
{
    public class PropertyItemAutomationPeer : ItemAutomationPeer, IValueProvider
    {
        private PropertyItem PropertyItem
        {
            get
            {
                return (PropertyItem)Item;
            }
        }

        public PropertyItemAutomationPeer(object item, ItemsControlAutomationPeer itemsControlAutomationPeer) : base(item, itemsControlAutomationPeer)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.ListItem;
        }

        protected override string GetClassNameCore()
        {
            return "PropertyItem";
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            if(patternInterface == PatternInterface.Value)
            {
                return this;
            }

            return base.GetPattern(patternInterface);
        }

        #region IValueProvider
        void IValueProvider.SetValue(string value)
        {
            throw new NotSupportedException();
        }


        string IValueProvider.Value
        {
            get
            {
                return PropertyItem != null && PropertyItem.Value != null ? PropertyItem.Value.ToString() : null;
            }
        }

        bool IValueProvider.IsReadOnly
        {
            get
            {
                return true;
            }
        }
        #endregion
    }
}
