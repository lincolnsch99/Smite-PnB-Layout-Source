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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PickDisplay : UserControl
    {
        private MainWindow mainWindow;
        private bool hasPlayed, reversed = false;
        private List<List<Image>> checkers;
        private string animType;
        private ComboBox pickCombo;
        private CheckBox lockCheck;
        private int checkerAnimCount = 51;
        private float checkerOverlap = 1f;
        private float nameHeightRatio = 25f / 250f;
        private float nameWidthRatio = 160f / 335f;
        private float fontRatio = 12f / 250f;

        public MainWindow MainWindow { get => mainWindow; 
            set 
            {
                mainWindow = value;
                //godName.VerticalAlignment = mainWindow.NamesVertAlign;
                //godName.HorizontalAlignment = mainWindow.NamesHorAlign;
            }
        }
        public string AnimType { get => animType; set => animType = value; }

        public bool Reversed { 
            set 
            {
                reversed = value;
                if(reversed)
                {
                    godImage.RenderTransformOrigin = new Point(0.5, 0.5);
                    ScaleTransform flipTrans = new ScaleTransform();
                    flipTrans.ScaleX = -1;
                    godImage.RenderTransform = flipTrans;
                    godName.SetValue(Canvas.LeftProperty, double.Parse("0"));
                }
                else
                {
                    godImage.RenderTransformOrigin = new Point(0.5, 0.5);
                    ScaleTransform flipTrans = new ScaleTransform();
                    flipTrans.ScaleX = 1;
                    godImage.RenderTransform = flipTrans;
                    godName.SetValue(Canvas.RightProperty, double.Parse("0"));
                }
            } 
        }

        public ComboBox PickCombo
        {
            get => pickCombo;
            set
            {
                if (pickCombo != null)
                    pickCombo.SelectionChanged -= PickCombo_SelectionChanged;
                pickCombo = value;
                pickCombo.SelectionChanged += PickCombo_SelectionChanged;
            }
        }

        public CheckBox LockCheck
        {
            get => lockCheck;
            set
            {
                if (lockCheck != null)
                {
                    lockCheck.Checked -= LockCheck_Checked;
                    lockCheck.Unchecked -= LockCheck_Unchecked;
                }
                lockCheck = value;
                lockCheck.Checked += LockCheck_Checked;
                lockCheck.Unchecked += LockCheck_Unchecked;
            }
        }

        private void LockCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            hoveredRect.Visibility = Visibility.Visible;
        }

        private void LockCheck_Checked(object sender, RoutedEventArgs e)
        {
            ComboBoxItem pickSelection = (ComboBoxItem)pickCombo.SelectedItem;
            if (pickSelection.Content.ToString() != "Empty")
            {
                hoveredRect.Visibility = Visibility.Hidden;
                soundPlayer.Source = new Uri(mainWindow.ResourcesPath + "/Sounds/" + godName.Content + ".mp3", UriKind.Absolute);
                soundPlayer.Volume = mainWindow.Volume;
                switch (animType)
                {
                    case "Fade":
                        FadeIn();
                        break;
                    case "Checkered Fade":
                        CheckeredFadeIn(checkerAnimCount, 1f, reversed, 1.5f);
                        break;
                    case "Light Checkered Fade":
                        CheckeredFadeIn(checkerAnimCount, 1f, reversed, 0.9f);
                        break;
                    case "Slow Checkered Fade":
                        CheckeredFadeIn(checkerAnimCount, 4, reversed, 1.5f);
                        break;
                    default:
                        break;
                }
                soundPlayer.Play();
            }
            else
            {
                if (hoveredRect.Visibility == Visibility.Hidden)
                    hoveredRect.Visibility = Visibility.Visible;
                else
                    hoveredRect.Visibility = Visibility.Hidden;
            }
        }

        private void PickCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem pickSelection = (ComboBoxItem)pickCombo.SelectedItem;

                if (pickSelection.Content.ToString() == "Empty")
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    godImage.Source = new BitmapImage(new Uri(mainWindow.ResourcesPath + "/CharacterImages/Picks/"
                        + pickSelection.Content.ToString() + ".png", UriKind.Absolute));
                    godName.Content = pickSelection.Content.ToString();
                    if (lockCheck.IsChecked == true)
                    {
                        soundPlayer.Source = new Uri(mainWindow.ResourcesPath + "/Sounds/" + godName.Content + ".mp3", UriKind.Absolute);
                        soundPlayer.Volume = mainWindow.Volume;
                        switch (animType)
                        {
                            case "Fade":
                                FadeIn();
                                break;
                            case "Checkered Fade":
                                CheckeredFadeIn(checkerAnimCount, 1f, reversed, 1.5f);
                                break;
                            case "Light Checkered Fade":
                                CheckeredFadeIn(checkerAnimCount, 1f, reversed, 0.9f);
                                break;
                            case "Slow Checkered Fade":
                                CheckeredFadeIn(checkerAnimCount, 4, reversed, 1.5f);
                                break;
                            default:
                                break;
                        }
                        soundPlayer.Play();
                    }
                    else
                    {
                        godImage.BeginAnimation(Image.OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3f)));
                        godName.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3f)));
                    }
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2.StackTrace);
            }
        }

        public PickDisplay()
        {
            InitializeComponent();
            hasPlayed = false;
            checkers = new List<List<Image>>();
            animType = "Fade";
        }

        public void Reset()
        {
            hasPlayed = false;
            animType = mainWindow.AnimType;
            if (checkers.Count > 0)
            {
                foreach (List<Image> rects in checkers)
                {
                    for (int i = 0; i < rects.Count; i++)
                        canvas.Children.Remove(rects[i]);
                }
                checkers = new List<List<Image>>();
            }
        }

        public void FadeIn()
        {
            godImage.BeginAnimation(Image.OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1)));
        }

        private void UserControlObject_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            godImage.Width = UserControlObject.Width;
            godImage.Height = UserControlObject.Height;
            hoveredRect.Width = UserControlObject.Width;
            hoveredRect.Height = UserControlObject.Height; 
            canvas.Width = UserControlObject.Width;
            canvas.Height = UserControlObject.Height;
            godName.Width = UserControlObject.Width * nameWidthRatio;
            godName.Height = UserControlObject.Width * nameHeightRatio;
            godName.FontSize = UserControlObject.Width * fontRatio;
        }

        public void CheckeredFadeIn(int split, float totalTime, bool reversed, float startOpacity)
        {
            float checkerSize = (float)canvas.Width / (float)split;
            if(checkers.Count > 0)
            {
                foreach(List<Image> rects in checkers)
                {
                    for (int i = 0; i < rects.Count; i++)
                        canvas.Children.Remove(rects[i]);
                }
                checkers = new List<List<Image>>();
            }

            List<List<int>> patternIndices = new List<List<int>>();
            if (reversed)
            {
                for (int i = 0; i < (int)(canvas.Height / checkerSize) + 2; i++)
                {
                    List<int> indices = new List<int>();
                    for (int j = (int)(canvas.Width / checkerSize) + 1; j >= 0 - ((int)(canvas.Height / checkerSize) + 2); j--)
                        indices.Add(i + j);
                    patternIndices.Add(indices);
                }
            }
            else
            {
                for (int i = ((int)(canvas.Height / checkerSize) + 2) - 1; i >= 0; i--)
                {
                    List<int> indices = new List<int>();
                    for (int j = 0 - ((int)(canvas.Height / checkerSize) + 2); j < (int)(canvas.Width / checkerSize) + 1; j++)
                        indices.Add(i + j);
                    patternIndices.Add(indices);
                }
            }

            while (checkers.Count < (int)(canvas.Height / checkerSize) + 2)
            {
                List<Image> rects = new List<Image>();
                Random rand = new Random();
                BitmapImage image = new BitmapImage(new Uri(mainWindow.ResourcesPath +
                        "/CharacterImages/CheckeredFadeShape.png", UriKind.Absolute));
                while (rects.Count < (int)(canvas.Width / checkerSize) + 1)
                {
                    Image newRect = new Image();
                    canvas.Children.Add(newRect);
                    newRect.Source = image;
                    newRect.Height = checkerSize * checkerOverlap;
                    newRect.Width = checkerSize * checkerOverlap;
                    newRect.SetValue(Canvas.TopProperty, (double)((checkers.Count) * checkerSize));
                    newRect.SetValue(Canvas.LeftProperty, (double)((rects.Count - 1) * checkerSize));
                    rects.Add(newRect);                    
                }
                checkers.Add(rects);
            }


            int totalCount = patternIndices[0].Count;
            float deltaTime = totalTime / (float)totalCount;
            float runningCount = 0;

            for (int i = patternIndices[0].Count - 1; i >=0 ; i--)
            {
                for(int j = patternIndices.Count - 1; j >= 0; j--)
                {
                    if (patternIndices[j][i] >= 0 && patternIndices[j][i] < checkers[0].Count)
                        checkers[j][patternIndices[j][i]].BeginAnimation(OpacityProperty, new DoubleAnimation((double)startOpacity, 0, 
                            TimeSpan.FromSeconds(deltaTime + runningCount)));
                }
                runningCount += deltaTime;
            }
        }
    }
}
