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

namespace Smite_PnB_Layout
{
    /// <summary>
    /// Interaction logic for TopBanDisplay.xaml
    /// </summary>
    public partial class TopBanDisplay : UserControl
    {
        private float nameWidthRatio = 1f;
        private float nameHeightRatio = 22f / 60f;
        private float fontRatio = 10f / 60f;
        private float rectHeightRatio = 20f / 60f;

        public TopBanDisplay()
        {
            InitializeComponent();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            godImage.Width = UserControlObject.Width;
            godImage.Height = UserControlObject.Height;
            Canvas.Width = UserControlObject.Width;
            Canvas.Height = UserControlObject.Height;
            percentText.Width = UserControlObject.Width * nameWidthRatio;
            percentText.Height = UserControlObject.Width * nameHeightRatio;
            percentText.FontSize = UserControlObject.Width * fontRatio;
            rectang.Width = UserControlObject.Width;
            rectang.Height = UserControlObject.Height * rectHeightRatio;
        }
    }
}
