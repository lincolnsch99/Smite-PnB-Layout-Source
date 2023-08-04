using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Smite_PnB_Layout
{
    /// <summary>
    /// Interaction logic for AdvancedSettings.xaml
    /// </summary>
    public partial class AdvancedSettings : Window
    {
        private MainWindow mainWindow;
        private SettingsWindow settingsWindow;
        private List<TextBox> leftPicksTopInputs;
        private List<TextBox> leftPicksLeftInputs;
        private List<TextBox> rightPicksTopInputs;
        private List<TextBox> rightPicksLeftInputs;
        private List<TextBox> leftBansTopInputs;
        private List<TextBox> leftBansLeftInputs;
        private List<TextBox> rightBansTopInputs;
        private List<TextBox> rightBansLeftInputs;

        public AdvancedSettings(MainWindow mainWindow, SettingsWindow settingsWindow)
        {
            this.mainWindow = mainWindow;
            this.settingsWindow = settingsWindow;
            InitializeComponent();
            InitLists();
        }

        private void previewPressed(object sender, RoutedEventArgs e)
        {
            Preview();
        }

        public void Preview()
        {
            if (CheckForInvalidInputs())
            {
                try
                {
                    AdjustPicksAndBans();
                    AdjustTeamAndScore();
                    AdjustVarious();
                    AdjustFontColors();
                }
                catch (Exception e2)
                {
                    Console.Write(e2.StackTrace);
                }
            }
        }

        private void InitLists()
        {
            leftPicksTopInputs = new List<TextBox>();
            leftPicksTopInputs.Add(leftPick1TopInput);
            leftPicksTopInputs.Add(leftPick2TopInput);
            leftPicksTopInputs.Add(leftPick3TopInput);
            leftPicksTopInputs.Add(leftPick4TopInput);
            leftPicksTopInputs.Add(leftPick5TopInput);

            leftPicksLeftInputs = new List<TextBox>();
            leftPicksLeftInputs.Add(leftPick1LeftInput);
            leftPicksLeftInputs.Add(leftPick2LeftInput);
            leftPicksLeftInputs.Add(leftPick3LeftInput);
            leftPicksLeftInputs.Add(leftPick4LeftInput);
            leftPicksLeftInputs.Add(leftPick5LeftInput);

            rightPicksTopInputs = new List<TextBox>();
            rightPicksTopInputs.Add(rightPick1TopInput);
            rightPicksTopInputs.Add(rightPick2TopInput);
            rightPicksTopInputs.Add(rightPick3TopInput);
            rightPicksTopInputs.Add(rightPick4TopInput);
            rightPicksTopInputs.Add(rightPick5TopInput);

            rightPicksLeftInputs = new List<TextBox>();
            rightPicksLeftInputs.Add(rightPick1LeftInput);
            rightPicksLeftInputs.Add(rightPick2LeftInput);
            rightPicksLeftInputs.Add(rightPick3LeftInput);
            rightPicksLeftInputs.Add(rightPick4LeftInput);
            rightPicksLeftInputs.Add(rightPick5LeftInput);

            rightBansTopInputs = new List<TextBox>();
            rightBansTopInputs.Add(rightBan1TopInput);
            rightBansTopInputs.Add(rightBan2TopInput);
            rightBansTopInputs.Add(rightBan3TopInput);
            rightBansTopInputs.Add(rightBan4TopInput);
            rightBansTopInputs.Add(rightBan5TopInput);
            rightBansTopInputs.Add(rightBan6TopInput);

            rightBansLeftInputs = new List<TextBox>();
            rightBansLeftInputs.Add(rightBan1LeftInput);
            rightBansLeftInputs.Add(rightBan2LeftInput);
            rightBansLeftInputs.Add(rightBan3LeftInput);
            rightBansLeftInputs.Add(rightBan4LeftInput);
            rightBansLeftInputs.Add(rightBan5LeftInput);
            rightBansLeftInputs.Add(rightBan6LeftInput);

            leftBansTopInputs = new List<TextBox>();
            leftBansTopInputs.Add(leftBan1TopInput);
            leftBansTopInputs.Add(leftBan2TopInput);
            leftBansTopInputs.Add(leftBan3TopInput);
            leftBansTopInputs.Add(leftBan4TopInput);
            leftBansTopInputs.Add(leftBan5TopInput);
            leftBansTopInputs.Add(leftBan6TopInput);

            leftBansLeftInputs = new List<TextBox>();
            leftBansLeftInputs.Add(leftBan1LeftInput);
            leftBansLeftInputs.Add(leftBan2LeftInput);
            leftBansLeftInputs.Add(leftBan3LeftInput);
            leftBansLeftInputs.Add(leftBan4LeftInput);
            leftBansLeftInputs.Add(leftBan5LeftInput);
            leftBansLeftInputs.Add(leftBan6LeftInput);
        }

        private void AdjustPicksAndBans()
        {
            double widthP = double.Parse(pickSizeXInput.Text);
            double heightP = double.Parse(pickSizeYInput.Text);
            double widthB = double.Parse(banSizeXInput.Text);
            double heightB = double.Parse(banSizeYInput.Text);
            for (int i = 0; i < 5; i++)
            {
                mainWindow.PickDisplays[i].SetValue(Canvas.LeftProperty, double.Parse(leftPicksLeftInputs[i].Text));
                mainWindow.PickDisplays[i].SetValue(Canvas.TopProperty, double.Parse(leftPicksTopInputs[i].Text));
                mainWindow.PickDisplays[i].SetValue(WidthProperty, widthP);
                mainWindow.PickDisplays[i].SetValue(HeightProperty, heightP);

                mainWindow.BanDisplays[i].SetValue(Canvas.LeftProperty, double.Parse(leftBansLeftInputs[i].Text));
                mainWindow.BanDisplays[i].SetValue(Canvas.TopProperty, double.Parse(leftBansTopInputs[i].Text));
                mainWindow.BanDisplays[i].SetValue(WidthProperty, widthB);
                mainWindow.BanDisplays[i].SetValue(HeightProperty, heightB);
            }

            for (int i = 5; i < 10; i++)
            {
                mainWindow.PickDisplays[i].SetValue(Canvas.LeftProperty, double.Parse(rightPicksLeftInputs[i - 5].Text));
                mainWindow.PickDisplays[i].SetValue(Canvas.TopProperty, double.Parse(rightPicksTopInputs[i - 5].Text));
                mainWindow.PickDisplays[i].SetValue(WidthProperty, widthP);
                mainWindow.PickDisplays[i].SetValue(HeightProperty, heightP);

                mainWindow.BanDisplays[i].SetValue(Canvas.LeftProperty, double.Parse(rightBansLeftInputs[i - 5].Text));
                mainWindow.BanDisplays[i].SetValue(Canvas.TopProperty, double.Parse(rightBansTopInputs[i - 5].Text));
                mainWindow.BanDisplays[i].SetValue(WidthProperty, widthB);
                mainWindow.BanDisplays[i].SetValue(HeightProperty, heightB);
            }

            mainWindow.leftTeamPicks.Width = double.Parse(leftBackgroundXInput.Text);
            mainWindow.leftTeamPicks.Height = double.Parse(leftBackgroundYInput.Text);
            mainWindow.leftTeamPicks.SetValue(Canvas.LeftProperty, double.Parse(leftBackgroundLeftInput.Text));
            mainWindow.leftTeamPicks.SetValue(Canvas.TopProperty, double.Parse(leftBackgroundTopInput.Text));

            mainWindow.rightTeamPicks.Width = double.Parse(rightBackgroundXInput.Text);
            mainWindow.rightTeamPicks.Height = double.Parse(rightBackgroundYInput.Text);
            mainWindow.rightTeamPicks.SetValue(Canvas.LeftProperty, double.Parse(rightBackgroundLeftInput.Text));
            mainWindow.rightTeamPicks.SetValue(Canvas.TopProperty, double.Parse(rightBackgroundTopInput.Text));
        }

        private void AdjustTeamAndScore()
        {
            mainWindow.leftTeamDisplay.Width = double.Parse(teamSizeXInput.Text);
            mainWindow.leftTeamDisplay.Height = double.Parse(teamSizeYInput.Text);
            mainWindow.leftTeamDisplay.SetValue(Canvas.LeftProperty, double.Parse(leftTeamLeftInput.Text));
            mainWindow.leftTeamDisplay.SetValue(Canvas.TopProperty, double.Parse(leftTeamTopInput.Text));

            mainWindow.rightTeamDisplay.Width = double.Parse(teamSizeXInput.Text);
            mainWindow.rightTeamDisplay.Height = double.Parse(teamSizeYInput.Text);
            mainWindow.rightTeamDisplay.SetValue(Canvas.LeftProperty, double.Parse(rightTeamLeftInput.Text));
            mainWindow.rightTeamDisplay.SetValue(Canvas.TopProperty, double.Parse(rightTeamTopInput.Text));

            mainWindow.leftTeamScore.Width = double.Parse(scoreSizeXInput.Text);
            mainWindow.leftTeamScore.Height = double.Parse(scoreSizeYInput.Text);
            mainWindow.leftTeamScore.FontSize = double.Parse(scoreFontSizeInput.Text);
            mainWindow.leftTeamScore.SetValue(Canvas.LeftProperty, double.Parse(leftScoreLeftInput.Text));
            mainWindow.leftTeamScore.SetValue(Canvas.TopProperty, double.Parse(leftScoreTopInput.Text));

            mainWindow.rightTeamScore.Width = double.Parse(scoreSizeXInput.Text);
            mainWindow.rightTeamScore.Height = double.Parse(scoreSizeYInput.Text);
            mainWindow.rightTeamScore.FontSize = double.Parse(scoreFontSizeInput.Text);
            mainWindow.rightTeamScore.SetValue(Canvas.LeftProperty, double.Parse(rightScoreLeftInput.Text));
            mainWindow.rightTeamScore.SetValue(Canvas.TopProperty, double.Parse(rightScoreTopInput.Text));
        }

        private void AdjustVarious()
        {
            mainWindow.leftTeamTopBans.SetValue(Canvas.LeftProperty, double.Parse(leftTopBansLeftInput.Text));
            mainWindow.leftTeamTopBans.SetValue(Canvas.TopProperty, double.Parse(leftTopBansTopInput.Text));
            mainWindow.rightTeamTopBans.SetValue(Canvas.LeftProperty, double.Parse(rightTopBansLeftInput.Text));
            mainWindow.rightTeamTopBans.SetValue(Canvas.TopProperty, double.Parse(rightTopBansTopInput.Text));
            mainWindow.playerNamesGrid.SetValue(Canvas.LeftProperty, double.Parse(playerNamesLeftInput.Text));
            mainWindow.playerNamesGrid.SetValue(Canvas.TopProperty, double.Parse(playerNamesTopInput.Text));

            mainWindow.soloDisplay.Content = playerNamesInput1.Text;
            mainWindow.jungleDisplay.Content = playerNamesInput2.Text;
            mainWindow.midDisplay.Content = playerNamesInput3.Text;
            mainWindow.supportDisplay.Content = playerNamesInput4.Text;
            mainWindow.carryDisplay.Content = playerNamesInput5.Text;

            if (mainWindow.soloDisplay.Content.ToString() == "")
                mainWindow.soloDisplay.BorderThickness = new Thickness(0, 0, 0, 0);
            else
                mainWindow.soloDisplay.BorderThickness = new Thickness(2, 0, 2, 0);
            if (mainWindow.jungleDisplay.Content.ToString() == "")
                mainWindow.jungleDisplay.BorderThickness = new Thickness(0, 0, 0, 0);
            else
                mainWindow.jungleDisplay.BorderThickness = new Thickness(2, 0, 2, 0);
            if (mainWindow.midDisplay.Content.ToString() == "")
                mainWindow.midDisplay.BorderThickness = new Thickness(0, 0, 0, 0);
            else
                mainWindow.midDisplay.BorderThickness = new Thickness(2, 0, 2, 0);
            if (mainWindow.supportDisplay.Content.ToString() == "")
                mainWindow.supportDisplay.BorderThickness = new Thickness(0, 0, 0, 0);
            else
                mainWindow.supportDisplay.BorderThickness = new Thickness(2, 0, 2, 0);
            if (mainWindow.carryDisplay.Content.ToString() == "")
                mainWindow.carryDisplay.BorderThickness = new Thickness(0, 0, 0, 0);
            else
                mainWindow.carryDisplay.BorderThickness = new Thickness(2, 0, 2, 0);
        }

        private bool CheckForInvalidInputs()
        {
            return true;
        }

        public bool SaveToFile()
        {
            bool result = true;
            try
            {
                if (!File.Exists(mainWindow.ResourcesPath + "/Display/.layout"))
                    File.CreateText(mainWindow.ResourcesPath + "/Display/.layout").Close();
                StreamWriter writer = new StreamWriter(mainWindow.ResourcesPath + "/Display/.layout");
                writer.WriteLine(pickSizeXInput.Text.ToString() + "\n" + pickSizeYInput.Text.ToString());
                for (int i = 0; i < leftPicksLeftInputs.Count; i++)
                {
                    writer.WriteLine(leftPicksLeftInputs[i].Text.ToString());
                    writer.WriteLine(leftPicksTopInputs[i].Text.ToString());
                }
                for (int i = 0; i < rightPicksLeftInputs.Count; i++)
                {
                    writer.WriteLine(rightPicksLeftInputs[i].Text.ToString());
                    writer.WriteLine(rightPicksTopInputs[i].Text.ToString());
                }
                writer.WriteLine(animationTypeCombo.SelectedIndex.ToString());


                writer.WriteLine(banSizeXInput.Text.ToString() + "\n" + banSizeYInput.Text.ToString());
                for (int i = 0; i < 5; i++)
                {
                    writer.WriteLine(leftBansLeftInputs[i].Text.ToString());
                    writer.WriteLine(leftBansTopInputs[i].Text.ToString());
                }
                for (int i = 0; i < 5; i++)
                {
                    writer.WriteLine(rightBansLeftInputs[i].Text.ToString());
                    writer.WriteLine(rightBansTopInputs[i].Text.ToString());
                }

                writer.WriteLine(leftBackgroundXInput.Text.ToString() + "\n" + leftBackgroundYInput.Text.ToString());
                writer.WriteLine(leftBackgroundLeftInput.Text.ToString() + "\n" + leftBackgroundTopInput.Text.ToString());
                writer.WriteLine(rightBackgroundXInput.Text.ToString() + "\n" + rightBackgroundYInput.Text.ToString());
                writer.WriteLine(rightBackgroundLeftInput.Text.ToString() + "\n" + rightBackgroundTopInput.Text.ToString());

                writer.WriteLine(teamSizeXInput.Text.ToString() + "\n" + teamSizeYInput.Text.ToString());
                writer.WriteLine(leftTeamLeftInput.Text.ToString() + "\n" + leftTeamTopInput.Text.ToString());
                writer.WriteLine(rightTeamLeftInput.Text.ToString() + "\n" + rightTeamTopInput.Text.ToString());
                writer.WriteLine(scoreSizeXInput.Text.ToString() + "\n" + scoreSizeYInput.Text.ToString());
                writer.WriteLine(scoreFontSizeInput.Text.ToString());
                writer.WriteLine(leftScoreLeftInput.Text.ToString() + "\n" + leftScoreTopInput.Text.ToString());
                writer.WriteLine(rightScoreLeftInput.Text.ToString() + "\n" + rightScoreTopInput.Text.ToString());
                writer.WriteLine(leftTopBansLeftInput.Text.ToString() + "\n" + leftTopBansTopInput.Text.ToString());
                writer.WriteLine(rightTopBansLeftInput.Text.ToString() + "\n" + rightTopBansTopInput.Text.ToString());
                writer.WriteLine(playerNamesLeftInput.Text.ToString() + "\n" + playerNamesTopInput.Text.ToString());
                writer.WriteLine(fontColorCombo.SelectedIndex.ToString());
                writer.WriteLine(playerNamesInput1.Text.ToString());
                writer.WriteLine(playerNamesInput2.Text.ToString());
                writer.WriteLine(playerNamesInput3.Text.ToString());
                writer.WriteLine(playerNamesInput4.Text.ToString());
                writer.WriteLine(playerNamesInput5.Text.ToString());
                writer.WriteLine(leftBansLeftInputs[5].Text.ToString());
                writer.WriteLine(leftBansTopInputs[5].Text.ToString());
                writer.WriteLine(rightBansTopInputs[5].Text.ToString());
                writer.WriteLine(rightBansTopInputs[5].Text.ToString());

                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                result = false;
            }
            return result;
        }

        public void ReadFromFile()
        {
            string filepath = "default.layout";
            if (File.Exists(mainWindow.ResourcesPath + "/Display/.layout"))
                filepath = mainWindow.ResourcesPath + "/Display/.layout";
            StreamReader reader = new StreamReader(filepath);
            try
            {
                pickSizeXInput.Text = reader.ReadLine();
                pickSizeYInput.Text = reader.ReadLine();

                for (int i = 0; i < 5; i++)
                {
                    leftPicksLeftInputs[i].Text = reader.ReadLine();
                    leftPicksTopInputs[i].Text = reader.ReadLine();
                }

                for (int i = 0; i < 5; i++)
                {
                    rightPicksLeftInputs[i].Text = reader.ReadLine();
                    rightPicksTopInputs[i].Text = reader.ReadLine();
                }
                animationTypeCombo.SelectedIndex = int.Parse(reader.ReadLine());

                banSizeXInput.Text = reader.ReadLine();
                banSizeYInput.Text = reader.ReadLine();

                for (int i = 0; i < 5; i++)
                {
                    leftBansLeftInputs[i].Text = reader.ReadLine();
                    leftBansTopInputs[i].Text = reader.ReadLine();
                }

                for (int i = 0; i < 5; i++)
                {
                    rightBansLeftInputs[i].Text = reader.ReadLine();
                    rightBansTopInputs[i].Text = reader.ReadLine();
                }

                leftBackgroundXInput.Text = reader.ReadLine();
                leftBackgroundYInput.Text = reader.ReadLine();
                leftBackgroundLeftInput.Text = reader.ReadLine();
                leftBackgroundTopInput.Text = reader.ReadLine();

                rightBackgroundXInput.Text = reader.ReadLine();
                rightBackgroundYInput.Text = reader.ReadLine();
                rightBackgroundLeftInput.Text = reader.ReadLine();
                rightBackgroundTopInput.Text = reader.ReadLine();

                // Team/Score settings
                teamSizeXInput.Text = reader.ReadLine();
                teamSizeYInput.Text = reader.ReadLine();
                leftTeamLeftInput.Text = reader.ReadLine();
                leftTeamTopInput.Text = reader.ReadLine();
                rightTeamLeftInput.Text = reader.ReadLine();
                rightTeamTopInput.Text = reader.ReadLine();
                scoreSizeXInput.Text = reader.ReadLine();
                scoreSizeYInput.Text = reader.ReadLine();
                scoreFontSizeInput.Text = reader.ReadLine();
                leftScoreLeftInput.Text = reader.ReadLine();
                leftScoreTopInput.Text = reader.ReadLine();
                rightScoreLeftInput.Text = reader.ReadLine();
                rightScoreTopInput.Text = reader.ReadLine();

                // Various settings
                leftTopBansLeftInput.Text = reader.ReadLine();
                leftTopBansTopInput.Text = reader.ReadLine();
                rightTopBansLeftInput.Text = reader.ReadLine();
                rightTopBansTopInput.Text = reader.ReadLine();
                playerNamesLeftInput.Text = reader.ReadLine();
                playerNamesTopInput.Text = reader.ReadLine();
                fontColorCombo.SelectedIndex = int.Parse(reader.ReadLine());
                playerNamesInput1.Text = reader.ReadLine();
                playerNamesInput2.Text = reader.ReadLine();
                playerNamesInput3.Text = reader.ReadLine();
                playerNamesInput4.Text = reader.ReadLine();
                playerNamesInput5.Text = reader.ReadLine();

                leftBansLeftInputs[5].Text = reader.ReadLine();
                leftBansTopInputs[5].Text = reader.ReadLine();

                rightBansLeftInputs[5].Text = reader.ReadLine();
                rightBansTopInputs[5].Text = reader.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                reader.Close();
            }
        }

        private void cancelPressed(object sender, RoutedEventArgs e)
        {
            Cancel();
            mainWindow.ResetBans();
            mainWindow.ResetPicks();
        }

        private void applyPressed(object sender, RoutedEventArgs e)
        {
            Apply();
            mainWindow.ResetBans();
            mainWindow.ResetPicks();
        }

        public void Apply()
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Current changes will be submitted to file, continue?\nThis cannot be undone.",
                "Confirm Selection", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    Preview();
                    bool res = SaveToFile();
                    Close();
                    settingsWindow.advancedSettingsButton.IsHitTestVisible = true;
                    if (!res)
                    {
                        System.Windows.MessageBox.Show("There was an error saving your data.\nPlease contact developer to report this error.");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Your changes have been applied successfully.");
                        settingsWindow.resolution_Combo.IsEnabled = true;
                    }
                    break;
                case MessageBoxResult.Cancel:
                    System.Windows.MessageBox.Show("Cancelled current action.");
                    break;
            }
        }

        private void Cancel()
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Cancelling will discard any unsaved changes, continue?",
                "Confirm Selection", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    ReadFromFile();
                    Preview();
                    Close();
                    settingsWindow.advancedSettingsButton.IsHitTestVisible = true;
                    break;
                case MessageBoxResult.Cancel:
                    System.Windows.MessageBox.Show("Cancelled current action.");
                    break;
            }
        }

        private void animationTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selection = (ComboBoxItem)animationTypeCombo.SelectedItem;
            mainWindow.AnimType = selection.Content.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ReadFromFile();
            Preview();
            if (settingsWindow != null)
                settingsWindow.advancedSettingsButton.IsHitTestVisible = true;
            mainWindow.ResetBans();
            mainWindow.ResetPicks();
        }

        private void AdjustFontColors()
        {
            ComboBoxItem selected = (ComboBoxItem)fontColorCombo.SelectedItem;
            Brush color = Brushes.White;
            if (selected.Content.ToString() == "Black")
                color = Brushes.Black;
            else if (selected.Content.ToString() == "Grey")
                color = Brushes.Gray;

            mainWindow.leftTeamScore.Foreground = color;
            mainWindow.rightTeamScore.Foreground = color;
            mainWindow.leftTeamSolo.Foreground = color;
            mainWindow.leftTeamJung.Foreground = color;
            mainWindow.leftTeamMid.Foreground = color;
            mainWindow.leftTeamSupp.Foreground = color;
            mainWindow.leftTeamCarry.Foreground = color;
            mainWindow.rightTeamSolo.Foreground = color;
            mainWindow.rightTeamJung.Foreground = color;
            mainWindow.rightTeamMid.Foreground = color;
            mainWindow.rightTeamSupp.Foreground = color;
            mainWindow.rightTeamCarry.Foreground = color;
            mainWindow.leftTeamTopBansLabel.Foreground = color;
            mainWindow.leftTopBansLabel.Foreground = color;
            mainWindow.rightTeamTopBansLabel.Foreground = color;
            mainWindow.rightTopBansLabel.Foreground = color;

            mainWindow.soloDisplay.Foreground = color;
            mainWindow.soloDisplay.BorderBrush = color;
            mainWindow.jungleDisplay.Foreground = color;
            mainWindow.jungleDisplay.BorderBrush = color;
            mainWindow.midDisplay.Foreground = color;
            mainWindow.midDisplay.BorderBrush = color;
            mainWindow.supportDisplay.Foreground = color;
            mainWindow.supportDisplay.BorderBrush = color;
            mainWindow.carryDisplay.Foreground = color;
            mainWindow.carryDisplay.BorderBrush = color;
        }

    }
}
