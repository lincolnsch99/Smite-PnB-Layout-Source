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
using System.Windows.Shapes;

namespace Smite_PnB_Layout
{
    /// <summary>
    /// Interaction logic for InGameOverlay.xaml
    /// </summary>
    public partial class InGameOverlay : Window
    {
        private MainWindow mainWindow;
        private SettingsWindow settingsWindow;

        public InGameOverlay(MainWindow mainWindow, SettingsWindow settingsWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.settingsWindow = settingsWindow;
        }

        public void AdjustDisplay()
        {

        }
    }
}
