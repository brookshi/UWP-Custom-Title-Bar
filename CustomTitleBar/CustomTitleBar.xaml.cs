using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CustomTitleBar
{
    [ContentProperty(Name = "TitleBarControl")]
    public sealed partial class CustomTitleBar : UserControl, INotifyPropertyChanged
    {
        private CoreApplicationViewTitleBar _titleBar = CoreApplication.GetCurrentView().TitleBar;
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty TitleBarBackgroundColorProperty = DependencyProperty.Register("TitleBarBackgroundColor", 
            typeof(Brush), typeof(CustomTitleBar), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        public Brush TitleBarBackgroundColor
        {
            get { return GetValue(TitleBarBackgroundColorProperty) as Brush; }
            set { SetValue(TitleBarBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title",
            typeof(string), typeof(CustomTitleBar), new PropertyMetadata(""));
        public string Title
        {
            get { return GetValue(TitleProperty).ToString(); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleMarginProperty = DependencyProperty.Register("TitleMargin",
            typeof(Thickness), typeof(CustomTitleBar), new PropertyMetadata(new Thickness(10, 0, 0, 0)));
        public Thickness TitleMargin
        {
            get { return (Thickness)GetValue(TitleMarginProperty); }
            set { SetValue(TitleMarginProperty, value); }
        }

        public static readonly DependencyProperty TitleBarControlProperty = DependencyProperty.Register("TitleBarControl",
            typeof(object), typeof(CustomTitleBar), new PropertyMetadata(null));
        public object TitleBarControl
        {
            get { return (Color)GetValue(TitleBarControlProperty); }
            set { SetValue(TitleBarControlProperty, value); }
        }

        public CustomTitleBar()
        {
            this.InitializeComponent();
        }

        void InitBarStyle()
        {
            var bgColor = (TitleBarBackgroundColor as SolidColorBrush).Color;
            ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = bgColor;
            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = bgColor;
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
        }

        private void CustomTitleBarControl_Loaded(object sender, RoutedEventArgs e)
        {
            _titleBar.LayoutMetricsChanged += (s, o) => UpdateLayoutMetrics();
            UpdateLayoutMetrics();
            InitBarStyle();
            InitTitleBar();
        }

        private void UpdateLayoutMetrics()
        {
            Notify("TitleBarHeight");
            Notify("TitleBarPadding");
        }

        private void Notify(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public double TitleBarHeight
        {
            get { return _titleBar.Height; }
        }

        public Thickness TitleBarPadding
        {
            get
            {
                if (FlowDirection == FlowDirection.LeftToRight)
                {
                    return new Thickness(_titleBar.SystemOverlayLeftInset, 0, _titleBar.SystemOverlayRightInset, 0);
                }
                else
                {
                    return new Thickness(_titleBar.SystemOverlayRightInset, 0, _titleBar.SystemOverlayLeftInset, 0);
                }
            }
        }

        public void InitTitleBar()
        {
            Window.Current.SetTitleBar(TitleBarBackground);
        }

        private void TitleBarBackground_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }
    }
}
