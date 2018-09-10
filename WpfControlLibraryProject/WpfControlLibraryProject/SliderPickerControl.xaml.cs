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
    public sealed partial class SliderPicker : UserControl
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
            FrameworkPropertyMetadata contentMinimum = new FrameworkPropertyMetadata((0.0).ToString(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnContentMinimumValuePropertyChanged);
            FrameworkPropertyMetadata contentMaximum = new FrameworkPropertyMetadata((1.0).ToString(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnContentMaximumValuePropertyChanged);
            FrameworkPropertyMetadata smallChange = new FrameworkPropertyMetadata(new double(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSmallChangeValueChanged);
            FrameworkPropertyMetadata largeChange = new FrameworkPropertyMetadata(new double(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnLargeChangeValueChanged);
            FrameworkPropertyMetadata borderColor = new FrameworkPropertyMetadata(Colors.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBorderColorChanged);
            FrameworkPropertyMetadata titleColor = new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleColorChanged);
            FrameworkPropertyMetadata contentColor = new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnContentColorChanged);
            FrameworkPropertyMetadata valueSliderColor = new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueSliderColorChanged);



            SliderValueProperty = DependencyProperty.Register("SliderValue", typeof(double), typeof(SliderPicker), valueMetadata);
            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SliderPicker), titleMetadata);
            ContentValueSliderProperty = DependencyProperty.Register("ContentValueSlider", typeof(string), typeof(SliderPicker), contentMetadata);
            MaximumValueProperty = DependencyProperty.Register("MaximumValue", typeof(double), typeof(SliderPicker), maximumMetadata);
            MinimumValueProperty = DependencyProperty.Register("MinimumValue", typeof(double), typeof(SliderPicker), minimumMetadata);
            ContentMaximumValueProperty = DependencyProperty.Register("ContentMaximumValue", typeof(string), typeof(SliderPicker), contentMaximum);
            ContentMinimumValueProperty = DependencyProperty.Register("ContentMinimumValue", typeof(string), typeof(SliderPicker), contentMinimum);
            SmallChangeValueProperty = DependencyProperty.Register("SmallChangeValue", typeof(double), typeof(SliderPicker), smallChange);
            LargeChangeValueProperty = DependencyProperty.Register("LargeChangeValue", typeof(double), typeof(SliderPicker), largeChange);
            BorderColorProperty = DependencyProperty.Register("BorderColor", typeof(Color), typeof(SliderPicker), borderColor);
            TitleColorProperty = DependencyProperty.Register("TitleColor", typeof(Brush), typeof(SliderPicker), titleColor);
            ContentColorProperty = DependencyProperty.Register("ContentColor", typeof(Brush), typeof(SliderPicker), contentColor);
            ValueSliderColorProperty = DependencyProperty.Register("ValueSliderColor", typeof(Brush), typeof(SliderPicker), valueSliderColor);

            SliderValueChangedEvent = EventManager.RegisterRoutedEvent("SliderValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(SliderPicker));
            TitleChangedEvent = EventManager.RegisterRoutedEvent("TitleChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(SliderPicker));
            ContentValueSliderChangedEvent = EventManager.RegisterRoutedEvent("ContentValueSliderChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(SliderPicker));
            MinimumValueChangedEvent = EventManager.RegisterRoutedEvent("MinimunValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(SliderPicker));
            MaximumValueChangedEvent = EventManager.RegisterRoutedEvent("MaximumValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(SliderPicker));
            ContentMinimumValueChangedEvent = EventManager.RegisterRoutedEvent("ContentMinimumValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(SliderPicker));
            ContentMaximumValueChangedEvent = EventManager.RegisterRoutedEvent("ContentMaximumValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<string>), typeof(SliderPicker));
            SmallChangeValueChangedEvent = EventManager.RegisterRoutedEvent("SmallChangeValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(SliderPicker));
            LargeChangeValueChangedEvent = EventManager.RegisterRoutedEvent("LargeChangeValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(SliderPicker));
            BorderColorChangedEvent = EventManager.RegisterRoutedEvent("BorderColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(SliderPicker));
            TitleColorChangedEvent = EventManager.RegisterRoutedEvent("TitleColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Brush>), typeof(SliderPicker));
            ContentColorChangedEvent = EventManager.RegisterRoutedEvent("ContentColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Brush>), typeof(SliderPicker));
            ValueSliderColorChangedEvent = EventManager.RegisterRoutedEvent("ValueSliderColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Brush>), typeof(SliderPicker));

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
        public static readonly DependencyProperty SmallChangeValueProperty;
        public static readonly DependencyProperty LargeChangeValueProperty;
        public static readonly DependencyProperty BorderColorProperty;
        public static readonly DependencyProperty TitleColorProperty;
        public static readonly DependencyProperty ContentColorProperty;
        public static readonly DependencyProperty ValueSliderColorProperty;       
        #endregion
        #region STATIC EVENTS
        public static readonly RoutedEvent SliderValueChangedEvent;
        public static readonly RoutedEvent TitleChangedEvent;
        public static readonly RoutedEvent ContentValueSliderChangedEvent;
        public static readonly RoutedEvent MinimumValueChangedEvent;
        public static readonly RoutedEvent MaximumValueChangedEvent;
        public static readonly RoutedEvent ContentMinimumValueChangedEvent;
        public static readonly RoutedEvent ContentMaximumValueChangedEvent;
        public static readonly RoutedEvent SmallChangeValueChangedEvent;
        public static readonly RoutedEvent LargeChangeValueChangedEvent;
        public static readonly RoutedEvent BorderColorChangedEvent;
        public static readonly RoutedEvent TitleColorChangedEvent;
        public static readonly RoutedEvent ContentColorChangedEvent;
        public static readonly RoutedEvent ValueSliderColorChangedEvent;
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

            if (sliderPicker != null)
            {
                sliderPicker.ContentValueSlider = e.NewValue.ToString();
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

        private static void OnSmallChangeValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker picker = sender as SliderPicker;
            var args = new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue);
            args.RoutedEvent = SmallChangeValueChangedEvent;

            if(picker != null)
            {
                picker.SmallChangeValue = (double)e.NewValue;
                picker.RaiseEvent(args);
            }
        }

        private static void OnLargeChangeValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker picker = sender as SliderPicker;
            var args = new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue);
            args.RoutedEvent = LargeChangeValueChangedEvent;

            if(picker != null)
            {
                picker.LargeChangeValue = (double)e.NewValue;
                picker.RaiseEvent(args);
            }
        }

        private static void OnBorderColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker picker = sender as SliderPicker;
            Color newValue = (Color)e.NewValue;
            var args = new RoutedPropertyChangedEventArgs<Color>((Color)e.OldValue, newValue);
            args.RoutedEvent = BorderColorChangedEvent;

            if(picker != null)
            {
                picker.BorderColor = newValue;
                picker.Border1.BorderBrush = new SolidColorBrush(newValue);
                picker.Border2.BorderBrush = new SolidColorBrush(newValue);
                picker.Border3.BorderBrush = new SolidColorBrush(newValue);
                picker.Border4.BorderBrush = new SolidColorBrush(newValue);

                picker.RaiseEvent(args);
            }
        }

        private static void OnTitleColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = sender as SliderPicker;
            Brush newValue = (Brush)e.NewValue;
            var args = new RoutedPropertyChangedEventArgs<Brush>((Brush)e.OldValue, newValue);
            args.RoutedEvent = TitleColorChangedEvent;

            if(sliderPicker != null)
            {
                sliderPicker.TitleColor = newValue;
                sliderPicker.RaiseEvent(args);
            }
        }

        private static void OnContentColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = sender as SliderPicker;
            Brush newValue = (Brush)e.NewValue;
            var args = new RoutedPropertyChangedEventArgs<Brush>((Brush)e.OldValue, newValue);
            args.RoutedEvent = ContentColorChangedEvent;

            if(sliderPicker != null)
            {
                sliderPicker.ContentColor = newValue;
                sliderPicker.RaiseEvent(args);
            }
        }

        private static void OnValueSliderColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SliderPicker sliderPicker = sender as SliderPicker;
            Brush newValue = (Brush)e.NewValue;
            var args = new RoutedPropertyChangedEventArgs<Brush>((Brush)e.OldValue, newValue);
            args.RoutedEvent = ValueSliderColorChangedEvent;

            if(sliderPicker != null)
            {
                sliderPicker.ValueSliderColor = newValue;
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
            set => SetValue(ContentValueSliderProperty, value);
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

        public double SmallChangeValue
        {
            get => (double)GetValue(SmallChangeValueProperty);
            set => SetValue(SmallChangeValueProperty, value);
        }

        public double LargeChangeValue
        {
            get => (double)GetValue(LargeChangeValueProperty);
            set => SetValue(LargeChangeValueProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Brush TitleColor
        {
            get => (Brush)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }

        public Brush ContentColor
        {
            get => (Brush)GetValue(ContentColorProperty);
            set => SetValue(ContentColorProperty, value);
        }

        public Brush ValueSliderColor
        {
            get => (Brush)GetValue(ValueSliderColorProperty);
            set => SetValue(ValueSliderColorProperty, value);
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

        public event RoutedPropertyChangedEventHandler<double> SmallChangeValueChanged
        {
            add => AddHandler(SmallChangeValueChangedEvent, value);
            remove => RemoveHandler(SmallChangeValueChangedEvent, value);
        }

        public event RoutedPropertyChangedEventHandler<double> LargeChangeValueChanged
        {
            add => AddHandler(LargeChangeValueChangedEvent, value);
            remove => RemoveHandler(LargeChangeValueChangedEvent, value);
        }

        public event RoutedPropertyChangedEventHandler<Color> BorderColorChanged
        {
            add => AddHandler(BorderColorChangedEvent, value);
            remove => RemoveHandler(BorderColorChangedEvent, value);
        }

        public event RoutedPropertyChangedEventHandler<Brush> TitleColorChanged
        {
            add => AddHandler(TitleColorChangedEvent, value);
            remove => RemoveHandler(TitleColorChangedEvent, value);
        }

        public event RoutedPropertyChangedEventHandler<Brush> ContentColorChanged
        {
            add => AddHandler(ContentColorChangedEvent, value);
            remove => RemoveHandler(ContentColorChangedEvent, value);
        }

        public event RoutedPropertyChangedEventHandler<Brush> ValueSliderColorChanged
        {
            add => AddHandler(ValueSliderColorChangedEvent, value);
            remove => RemoveHandler(ValueSliderColorChangedEvent, value);
        }
        #endregion
        #region METHODS
        public override string ToString()
        {
            return typeof(SliderPicker).FullName;
        }
        #endregion
    }
}
