using System.Windows;
using System.Windows.Data;

namespace Backuper
{
    public class BindingProxy : Freezable
    {
        protected override BindingProxy CreateInstanceCore() => new();

        public object DataContext
        {
            get => GetValue(ProxyProperty);
            set => SetValue(ProxyProperty, value);
        }
        public static readonly DependencyProperty ProxyProperty = DependencyProperty.Register("Proxy", typeof(object), typeof(BindingProxy));

        public BindingProxy() => BindingOperations.SetBinding(this, ProxyProperty, new Binding());
    }
}
