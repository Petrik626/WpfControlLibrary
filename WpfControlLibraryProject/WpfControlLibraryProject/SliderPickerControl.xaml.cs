using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlLibraryProject
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class SliderPicker : UserControl
    {
        #region CONSTRUCTORS
        public SliderPicker()
        {
            InitializeComponent();
        }

        static SliderPicker()
        {
            FrameworkPropertyMetadata titleMetadata = new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleChanged);
            FrameworkPropertyMetadata valueMetadata = new FrameworkPropertyMetadata(new double(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSliderValueChanged);
            FrameworkPropertyMetadata contentMetadata = new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnContentValueSliderChanged);
            FrameworkPropertyMetadata minimumMetadata = new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnMinimumValueChanged);
            FrameworkPropertyMetadata maximumMetadata = new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnMaximumValueChanged);
            FrameworkPropertyMetadata contentMinimum = new FrameworkPropertyMetadata("0,0", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnContentMinimumValuePropertyChanged);
            FrameworkPropertyMetadata contentMaximum = new FrameworkPropertyMetadata("1,0", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnContentMaximumValuePropertyChanged);


            SliderValueProperty = DependencyProperty.Register("SliderValue", typeof(double), typeof(SliderPicker), valueMetadata);
            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SliderPicker), titleMetadata);
            ContentValueSliderProperty = DependencyProperty.Register("ContentValueSlider", typeof(string), typeof(SliderPicker), contentMetadata);
            MaximumValueProperty = DependencyProperty.Register("MaximumValue", typeof(double), typeof(SliderPicker), maximumMetadata);
            MinimumValueProperty = DependencyProperty.Register("MinimumValue", typeof(double), typeof(SliderPicker), minimumMetadata);
            ContentMaximumValueProperty = DependencyProperty.Register("ContentMaximumValue", typeof(double), typeof(SliderPicker), contentMaximum);
            ContentMinimumValueProperty = DependencyProperty.Register("ContentMinimumValue", typeof(double), typeof(SliderPicker), contentMinimum);

            SliderValueChangedEvent = EventManager.RegisterRoutedEvent("SliderValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(SliderPicker));
            TitleChangedEvent = EventManager.RegisterRoutedEvent("TitleChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(SliderPicker));
            ContentValueSliderChangedEvent = EventManager.RegisterRoutedEvent("ContentValueSliderChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(SliderPicker));
            MinimumValueChangedEvent = EventManager.RegisterRoutedEvent("MinimunValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(SliderPicker));
            MaximumValueChangedEvent = EventManager.RegisterRoutedEvent("MaximumValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(SliderPicker));
            ContentMinimumValueChangedEvent = EventManager.RegisterRoutedEvent("ContentMinimumValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(SliderPicker));
            ContentMaximumValueChangedEvent = EventManager.RegisterRoutedEvent("ContentMaximumValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(SliderPicker));

        }
        #endregion
        #region STATIC PROPERTIES
        public static readonly DependencyProperty SliderValueProperty;
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty ContentValueSliderProperty;
        public static readonly DependencyProperty MinimumValueProperty;
        public static readonly DependencyProperty MaximumValueProperty;
        public static readonly DependencyProperty ContentMinimumValueProperty;
        public static readonly DependencyProperty ContentMaximumValueProperty;
        #endregion
        #region STATIC EVENTS
        public static readonly RoutedEvent SliderValueChangedEvent;
        public static readonly RoutedEvent TitleChangedEvent;
        public static readonly RoutedEvent ContentValueSliderChangedEvent;
        public static readonly RoutedEvent MinimumValueChangedEvent;
        public static readonly RoutedEvent MaximumValueChangedEvent;
        public static readonly RoutedEvent ContentMinimumValueChangedEvent;
        public static readonly RoutedEvent ContentMaximumValueChangedEvent;
        #endregion
        #region STATIC METHODS
        private static void OnSliderValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = sender as SliderPicker;
            var args = new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue);
            args.RoutedEvent = SliderValueChangedEvent;

            if(sliderPicker != null)
            {
                sliderPicker.SliderValue = (double)e.NewValue;
                sliderPicker.ContentValueSlider = e.NewValue.ToString();

                sliderPicker.RaiseEvent(args);
            }
        }

        private static void OnTitleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = sender as SliderPicker;
            var args = new RoutedPropertyChangedEventArgs<string>(e.OldValue.ToString(), e.NewValue.ToString());
            args.RoutedEvent = TitleChangedEvent;

            if(sliderPicker != null)
            {
                sliderPicker.Title = e.NewValue.ToString();
                sliderPicker.RaiseEvent(args);
            }
        }

        private static void OnContentValueSliderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = sender as SliderPicker;
            var args = new RoutedPropertyChangedEventArgs<string>(e.OldValue.ToString(), e.NewValue.ToString());
            args.RoutedEvent = ContentValueSliderChangedEvent;

            if(sliderPicker != null)
            {
                sliderPicker.ContentValueSlider = e.NewValue.ToString();
                sliderPicker.SliderValue = (double)e.NewValue;

                sliderPicker.RaiseEvent(args);
            }
        }

        private static void OnMinimumValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = sender as SliderPicker;
            double oldValue = (double)e.OldValue;
            double newValue = (double)e.NewValue;
            var args = new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue);
            args.RoutedEvent = MinimumValueChangedEvent;

            if (sliderPicker != null)
            {
                if (newValue >= sliderPicker.MaximumValue)
                {
                    sliderPicker.MinimumValue = oldValue;
                    sliderPicker.ContentMinimumValue = oldValue.ToString();
                }
                else
                {
                    sliderPicker.MinimumValue = newValue;
                    sliderPicker.ContentMinimumValue = newValue.ToString();
                }

                sliderPicker.RaiseEvent(args);
            }
        }

        private static void OnMaximumValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = sender as SliderPicker;
            double oldValue = (double)e.OldValue;
            double newValue = (double)e.NewValue;
            var args = new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue);
            args.RoutedEvent = MaximumValueChangedEvent;

            if (sliderPicker != null)
            {
                if(newValue <= sliderPicker.MinimumValue)
                {
                    sliderPicker.MaximumValue = oldValue;
                    sliderPicker.ContentMaximumValue = oldValue.ToString();
                }
                else
                {
                    sliderPicker.MaximumValue = newValue;
                    sliderPicker.ContentMaximumValue = newValue.ToString();
                }

                sliderPicker.RaiseEvent(args);
            }
        }

        private static void OnContentMinimumValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = sender as SliderPicker;
            double oldValue, newValue;
            double.TryParse(e.OldValue.ToString(), out oldValue);
            double.TryParse(e.NewValue.ToString(), out newValue);

            var args = new RoutedPropertyChangedEventArgs<string>(e.OldValue.ToString(), e.NewValue.ToString());
            args.RoutedEvent = ContentMinimumValueChangedEvent;

            if (sliderPicker != null)
            {
                if (newValue >= sliderPicker.MaximumValue)
                {
                    sliderPicker.MinimumValue = oldValue;
                    sliderPicker.ContentMinimumValue = oldValue.ToString();
                }
                else
                {
                    sliderPicker.MinimumValue = newValue;
                    sliderPicker.ContentMinimumValue = newValue.ToString();
                }

                sliderPicker.RaiseEvent(args);
            }
        }

        private static void OnContentMaximumValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = sender as SliderPicker;
            double oldValue, newValue;
            double.TryParse(e.OldValue.ToString(), out oldValue);
            double.TryParse(e.NewValue.ToString(), out newValue);

            var args = new RoutedPropertyChangedEventArgs<string>(e.OldValue.ToString(), e.NewValue.ToString());
            args.RoutedEvent = ContentMaximumValueChangedEvent;

            if (sliderPicker != null)
            {
                if (newValue <= sliderPicker.MinimumValue)
                {
                    sliderPicker.MaximumValue = oldValue;
                    sliderPicker.ContentMaximumValue = oldValue.ToString();
                }
                else
                {
                    sliderPicker.MaximumValue = newValue;
                    sliderPicker.ContentMaximumValue = newValue.ToString();
                }

                sliderPicker.RaiseEvent(args);
            }
        }
        #endregion
        #region PROPERTIES
        public double SliderValue
        {
            get => (double)GetValue(SliderValueProperty);
            set => SetValue(SliderValueProperty, value);
        }

        public string Title
        {
            get => GetValue(TitleProperty).ToString();
            set => SetValue(TitleProperty, value);
        }

        public string ContentValueSlider
        {
            get => GetValue(ContentValueSliderProperty).ToString();
            private set => SetValue(ContentValueSliderProperty, value);
        }

        public double MinimumValue
        {
            get => (double)GetValue(MinimumValueProperty);
            set => SetValue(MinimumValueProperty, value);
        }

        public double MaximumValue
        {
            get => (double)GetValue(MaximumValueProperty);
            set => SetValue(MaximumValueProperty, value);
        }

        public string ContentMinimumValue
        {
            get => GetValue(ContentMinimumValueProperty).ToString();
            set => SetValue(ContentMinimumValueProperty, value);
        }

        public string ContentMaximumValue
        {
            get => GetValue(ContentMaximumValueProperty).ToString();
            set => SetValue(ContentMaximumValueProperty, value);
        }
        #endregion
        #region EVENTS
        public event RoutedPropertyChangedEventHandler<double> SliderValueChanged
        {
            add => AddHandler(SliderValueChangedEvent, value);
            remove => RemoveHandler(SliderValueChangedEvent, value);
        }

        public event RoutedPropertyChangedEventHandler<string> TitleChanged
        {
            add => AddHandler(TitleChangedEvent, value);
            remove => RemoveHandler(TitleChangedEvent, value);
        }

        public event RoutedPropertyChangedEventHandler<string> ContentValueSliderChanged
        {
            add => AddHandler(ContentValueSliderChangedEvent, value);
            remove => RemoveHandler(ContentValueSliderChangedEvent, value);
        }

        public event RoutedPropertyChangedEventHandler<double> MinimumValueChanged
        {
            add => AddHandler(MinimumValueChangedEvent, value);
            remove => RemoveHandler(MinimumValueChangedEvent, value);
        }

        public event RoutedPropertyChangedEventHandler<double> MaximumValueChanged
        {
            add => AddHandler(MaximumValueChangedEvent, value);
            remove => RemoveHandler(MaximumValueChangedEvent, value);
        }

        public event RoutedPropertyChangedEventHandler<string> ContentMinimumValueChanged
        {
            add => AddHandler(ContentMinimumValueChangedEvent, value);
            remove => RemoveHandler(ContentMinimumValueChangedEvent, value);
        }

        public event RoutedPropertyChangedEventHandler<string> ContentMaximumValueChanged
        {
            add => AddHandler(ContentMaximumValueChangedEvent, value);
            remove => RemoveHandler(ContentMaximumValueChangedEvent, value);
        }
        #endregion
    }
}
