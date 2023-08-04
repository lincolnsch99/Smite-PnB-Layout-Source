using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Smite_PnB_Layout
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private MainWindow mainWindow;
        private AdvancedSettings advancedSet;
        
        public AdvancedSettings AdvancedSet { get; set; }

        public System.Windows.Controls.ComboBox teamOneSelection { get => this.teamOneCombo; set => this.teamOneCombo = value; }
        public System.Windows.Controls.ComboBox teamTwoSelection { get => this.teamTwoCombo; set => this.teamTwoCombo = value; }

        public SettingsWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            advancedSet = new AdvancedSettings(mainWindow, this);
            teamOneCombo.SelectedIndex = 0;
            teamTwoCombo.SelectedIndex = 0;
            resolution_Combo.SelectedIndex = mainWindow.ResolutionIndex;
            godNamesStatusCheck.IsChecked = mainWindow.ShowGodNames;
            if (!File.Exists(mainWindow.ResourcesPath + "/Display/.layout"))
            {
                resolution_Combo.IsEnabled = false;
            }
            else
                resolution_Combo.IsEnabled = true;
        }

        private void teamUpdated(object sender, SelectionChangedEventArgs e)
        {
            mainWindow.UpdateTeams();
        }

        private void scoreUpdated(object sender, TextChangedEventArgs e)
        {
            mainWindow.UpdateScores();
        }

        private void banReset_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ResetBans();
        }

        private void submitBans_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Current bans will be submitted to file, continue?\nThis cannot be undone.", 
                "Confirm Selection", MessageBoxButton.OKCancel);
            switch(result)
            {
                case MessageBoxResult.OK:
                    try
                    {
                        bool res = mainWindow.SubmitDraftToBanData();
                        if (!res)
                        {
                            System.Windows.MessageBox.Show("Submission Cancelled\nAll bans must be filled in.");
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Submission Successful");
                        }
                    }
                    catch(Exception e2)
                    {
                        System.Windows.MessageBox.Show("An error occured during submission.\nError Code: " + e2.StackTrace);
                    }
                    break;
                case MessageBoxResult.Cancel:
                    System.Windows.MessageBox.Show("Submission Cancelled");
                    break;
            }
        }

        private void pickReset_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ResetPicks();
        }

        private void swapTeams(object sender, RoutedEventArgs e)
        {
            mainWindow.SwapTeams();
        }

        private void swapPicks(object sender, RoutedEventArgs e)
        {
            mainWindow.SwapPicks();
        }

        private void swapBans(object sender, RoutedEventArgs e)
        {
            mainWindow.SwapBans();
        }

        private void swapPlayers(object sender, RoutedEventArgs e)
        {
            mainWindow.SwapPlayers();
        }

        private void soundPathBrowse_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    mainWindow.ResourcesPath = fbd.SelectedPath.Replace("\\", "/");
                    this.soundPathDisplay.Text = fbd.SelectedPath.Replace("\\", "/");
                }
            }
            if (!File.Exists(mainWindow.ResourcesPath + "/Display/.layout"))
            {
                resolution_Combo.IsEnabled = false;
            }
            else
                resolution_Combo.IsEnabled = true;
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            volumeDisplay.Content = Math.Round(volumeSlider.Value, 2).ToString();
            mainWindow.Volume = Math.Round(volumeSlider.Value, 2);
        }

        private void playerNameChanged(object sender, TextChangedEventArgs e)
        {
            mainWindow.UpdatePlayerNames();
        }

        private void playerNamesActive(object sender, RoutedEventArgs e)
        {
            mainWindow.UpdatePlayerNames();
        }

        private void banDataActive1(object sender, RoutedEventArgs e)
        {
            mainWindow.UpdateBanDatas();
        }

        private void banDataActive2(object sender, RoutedEventArgs e)
        {
            mainWindow.UpdateBanDatas();
        }

        private void updateFileData(object sender, RoutedEventArgs e)
        {
            mainWindow.TryFileUpdate();
            GrabAdvancedSettings();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow.InitLists();
            mainWindow.TryFileUpdate();
            mainWindow.UpdatePlayerNames();
            mainWindow.UpdateBanDatas();
            mainWindow.SetGodNamesDisplayOnPicks(mainWindow.ShowGodNames);
            GrabAdvancedSettings();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Close();
            if (advancedSet != null)
                advancedSet.Close();
            System.Windows.Application.Current.Shutdown();
        }

        private void advancedSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (advancedSet != null)
                advancedSet.Close();
            advancedSet = new AdvancedSettings(mainWindow, this);
            advancedSet.Show();
            advancedSet.ReadFromFile();
            advancedSettingsButton.IsHitTestVisible = false;

            double temp = mainWindow.Volume;
            mainWindow.Volume = 0;
            foreach (System.Windows.Controls.ComboBox box in mainWindow.PickCombos)
                box.SelectedIndex = 1;
            foreach (System.Windows.Controls.ComboBox box in mainWindow.BanCombos)
                box.SelectedIndex = 1;
            mainWindow.Volume = temp;
        }

        public void GrabAdvancedSettings()
        {
            advancedSet.ReadFromFile();
            advancedSet.Preview();
        }

        private void seriesChanged(object sender, TextChangedEventArgs e)
        {
            mainWindow.UpdateSeriesDisplay();
        }

        private void resolution_Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mainWindow.ResolutionIndex = resolution_Combo.SelectedIndex;
            mainWindow.UpdateResolution();
        }

        private void godNamesStatusCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            mainWindow.SetGodNamesDisplayOnPicks(false);
        }

        private void godNamesStatusCheck_Checked(object sender, RoutedEventArgs e)
        {
            mainWindow.ShowGodNames = (bool)godNamesStatusCheck.IsChecked;
            mainWindow.SetGodNamesDisplayOnPicks(true);
        }

        private void duelModeStatusCheck_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void duelModeStatusCheck_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
