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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Smite_PnB_Layout
{
    /// <summary>
    /// Interaction logic for BanDisplay.xaml
    /// </summary>
    public partial class BanDisplay : UserControl
    {
        private bool hasPlayed, reversed = false;
        private MainWindow mainWindow;
        private ComboBox banCombo;

        public MainWindow MainWindow { get => mainWindow; set => mainWindow = value; }

        public bool Reversed
        {
            set
            {
                reversed = value;
                if (reversed)
                {
                    godImage.RenderTransformOrigin = new Point(0.5, 0.5);
                    ScaleTransform flipTrans = new ScaleTransform();
                    flipTrans.ScaleX = -1;
                    godImage.RenderTransform = flipTrans;
                }
                else
                {
                    godImage.RenderTransformOrigin = new Point(0.5, 0.5);
                    ScaleTransform flipTrans = new ScaleTransform();
                    flipTrans.ScaleX = 1;
                    godImage.RenderTransform = flipTrans;
                }
            }
        }

        public ComboBox BanCombo
        {
            get => banCombo;
            set
            {
                if (banCombo != null)
                    banCombo.SelectionChanged -= BanCombo_SelectionChanged;
                banCombo = value;
                banCombo.SelectionChanged += BanCombo_SelectionChanged;
            }
        }

        private void BanCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem banSelection = (ComboBoxItem)banCombo.SelectedItem;
                if (banSelection.Content.ToString() == "Empty")
                    this.Visibility = Visibility.Hidden;
                else
                {
                    this.Visibility = Visibility.Visible;
                    godImage.Source = new BitmapImage(new Uri(mainWindow.ResourcesPath + "/CharacterImages/Bans/"
                        + banSelection.Content.ToString() + ".png", UriKind.Absolute));
                    soundPlayer.Source = new Uri(mainWindow.ResourcesPath + "/Sounds/" + "hover.mp3", UriKind.Absolute);
                    soundPlayer.Volume = mainWindow.Volume;
                    soundPlayer.Play();
                    hasPlayed = true;
                    FadeIn();
                }
            }
            catch(Exception e3)
            {
                Console.WriteLine(e3.StackTrace);
            }
        }

        public BanDisplay()
        {
            InitializeComponent();
            hasPlayed = false;
        }

        public void Reset()
        {
            hasPlayed = false;
        }

        private void soundPlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Console.WriteLine(e.ErrorException.Message);
        }

        public void FadeIn()
        {
            godImage.BeginAnimation(Image.OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3f)));
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            godImage.Width = BanControl.Width;
            godImage.Height = BanControl.Height;
            canvas.Height = BanControl.Height;
            canvas.Width = BanControl.Width;
        }
    }
}
