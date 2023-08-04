using System.Windows;

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
