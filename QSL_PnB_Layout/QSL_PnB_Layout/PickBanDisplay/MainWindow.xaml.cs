using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Smite_PnB_Layout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Variables

        private SettingsWindow settingsWindow;
        private InGameOverlay inGameOverlay;
        private List<ComboBox> pickCombos = new List<ComboBox>();
        private List<PickDisplay> pickDisplays = new List<PickDisplay>();
        private List<CheckBox> lockedCheckboxes = new List<CheckBox>();
        private List<ComboBox> banCombos = new List<ComboBox>();
        private List<BanDisplay> banDisplays = new List<BanDisplay>();
        private double volume;
        private string resourcesPath = "", animType = "None";
        private VerticalAlignment namesVertAlign = VerticalAlignment.Bottom;
        private HorizontalAlignment namesHorAlign = HorizontalAlignment.Center;
        private int namesHorAlignIndex = 1, namesVerAlignInex = 2, animTypeIndex = 0, resolutionIndex = 0;
        private List<BanData> banDatas = new List<BanData>();
        private bool exitingApp = false, showGodNames = true;
        //private SubmissionHistory submissionHistory;

        #endregion

        #region Public Variables

        public double Volume { get => volume; set => volume = value; }
        public string ResourcesPath
        {
            get => resourcesPath;
            set
            {
                ChangeToSmallerRes();
                resourcesPath = value;
                TryFileUpdate();
            }
        }
        public int ResolutionIndex { get => resolutionIndex; set => resolutionIndex = value; }
        public VerticalAlignment NamesVertAlign { get => namesVertAlign; set => namesVertAlign = value; }
        public HorizontalAlignment NamesHorAlign { get => namesHorAlign; set => namesHorAlign = value; }
        public int NamesHorAlignIndex { get => namesHorAlignIndex; set => namesHorAlignIndex = value; }
        public int NamesVerAlignIndex { get => namesVerAlignInex; set => namesVerAlignInex = value; }
        public int AnimTypeIndex { get => animTypeIndex; set => animTypeIndex = value; }
        public List<PickDisplay> PickDisplays { get => pickDisplays; }
        public List<BanDisplay> BanDisplays { get => banDisplays; }
        public List<ComboBox> PickCombos { get => pickCombos; }
        public List<ComboBox> BanCombos { get => banCombos; }
        public string AnimType
        {
            get => animType;
            set
            {
                animType = value;
                try
                {
                    foreach (PickDisplay pick in pickDisplays)
                        pick.AnimType = animType;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        public bool ShowGodNames
        {
            get => showGodNames; 
            set
            {
                showGodNames = value;
                SetGodNamesDisplayOnPicks(showGodNames);
            }
        }

        #endregion

        #region Constructor/Auto Methods
        /// <summary>
        /// Constructor. This is the head window so needs to initialize the other windows.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            settingsWindow = new SettingsWindow(this);
            inGameOverlay = new InGameOverlay(this, this.settingsWindow);

            inGameOverlay.Show();
            settingsWindow.volumeSlider.Value = 0.25f;
            settingsWindow.Show();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader("Data.txt");
                string line = "";
                try
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line == "-resources_path-")
                        {
                            ResourcesPath = reader.ReadLine();
                        }
                        else if (line == "-resolution-")
                        {
                            resolutionIndex = int.Parse(reader.ReadLine());
                        }
                        else if (line == "-show_god_names-")
                        {
                            if (reader.ReadLine() == "True")
                                ShowGodNames = true;
                            else
                                ShowGodNames = false;
                        }
                    }
                }
                catch (Exception e2)
                {
                    Console.WriteLine(e2.StackTrace);
                }
                finally
                {
                    reader.Close();
                    //UpdateResolution("1600x900");
                    if (resolutionIndex == 0)
                        UpdateResolution("1600x900");
                    else
                        UpdateResolution("1920x1080");
                }
            }
            catch (DirectoryNotFoundException dnfe)
            {
                File.CreateText("Data.txt").Close();
                Console.WriteLine(dnfe.StackTrace);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                exitingApp = true;
                File.CreateText("Data.txt").Close();
                StreamWriter writer = new StreamWriter("Data.txt");
                writer.WriteLine("-resources_path-");
                writer.WriteLine(resourcesPath);
                writer.WriteLine("-resolution-");
                writer.WriteLine(resolutionIndex.ToString());
                writer.WriteLine("-show_god_names-");
                if (showGodNames)
                    writer.WriteLine("True");
                else
                    writer.WriteLine("False");
                writer.Close();

                foreach (BanData data in banDatas)
                {
                    data.SaveToFile(resourcesPath + "/Teams/" + data.TeamName + "/BanData.json");
                }
                //submissionHistory.SaveToFile("HistoryData.json");

            }
            catch (Exception e3)
            {
                Console.WriteLine(e3.StackTrace);
            }
            finally
            {
                inGameOverlay.Close();
                ChangeToSmallerRes();
            }
        }

        #endregion

        #region UI Interaction

        public void SetGodNamesDisplayOnPicks(bool set)
        {
            foreach (PickDisplay pick in pickDisplays)
            {
                pick.SetNameDisplayStatus(set);
            }
            showGodNames = set;
        }

        public void UpdateResolution(string resolution = "")
        {
            try
            {
                this.Left = 0;
                this.Top = 0;

                if (resolution == "1600x900")
                {
                    if (this.Width != 1600)
                    {
                        this.MinWidth = 1600;
                        this.Width = 1600;
                        this.MinHeight = 900;
                        this.Height = 900;
                        this.backgroundCanvas.Width = 1600;
                        this.backgroundCanvas.Height = 900;
                        this.mainBackground.Width = 1600;
                        this.mainBackground.Height = 900;
                        this.resolutionIndex = 0;
                        if (settingsWindow != null)
                            settingsWindow.resolution_Combo.SelectedIndex = 0;

                        if (File.Exists(resourcesPath + "/Display/.layout"))
                        {
                            string[] lines = File.ReadAllLines(resourcesPath + "/Display/.layout");
                            for (int i = 0; i < lines.Count(); i++)
                            {
                                if (float.TryParse(lines[i], out float f) && i != 22 && i != 72)
                                    lines[i] = Math.Round(f * (1600f / 1920f), 2).ToString();
                            }
                            File.WriteAllLines(resourcesPath + "/Display/.layout", lines);

                        }

                        playerNamesGrid.Width = Math.Round(playerNamesGrid.Width * (1600f / 1920f), 2);
                        playerNamesGrid.Height = Math.Round(playerNamesGrid.Height * (1600f / 1920f), 2);
                        foreach (UIElement obj in playerNamesGrid.Children)
                        {
                            float fl;
                            float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                            obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1600f / 1920f), 2));
                            float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                            obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1600f / 1920f), 2));
                            float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                            obj.SetValue(WidthProperty, Math.Round(fl * (1600f / 1920f), 2));
                            float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                            obj.SetValue(HeightProperty, Math.Round(fl * (1600f / 1920f), 2));
                            if (obj.GetType() == typeof(Label))
                                (obj as Label).FontSize *= (1600f / 1920f);
                            else if (obj.GetType() == typeof(TextBlock))
                                (obj as TextBlock).FontSize *= (1600f / 1920f);
                        }

                        leftTeamTopBans.Width = Math.Round(leftTeamTopBans.Width * (1600f / 1920f), 2);
                        leftTeamTopBans.Height = Math.Round(leftTeamTopBans.Height * (1600f / 1920f), 2);
                        foreach (UIElement obj in leftTeamTopBans.Children)
                        {
                            float fl;
                            float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                            obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1600f / 1920f), 2));
                            float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                            obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1600f / 1920f), 2));
                            float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                            obj.SetValue(WidthProperty, Math.Round(fl * (1600f / 1920f), 2));
                            float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                            obj.SetValue(HeightProperty, Math.Round(fl * (1600f / 1920f), 2));
                            if (obj.GetType() == typeof(Label))
                                (obj as Label).FontSize *= (1600f / 1920f);
                            else if (obj.GetType() == typeof(TextBlock))
                                (obj as TextBlock).FontSize *= (1600f / 1920f);
                        }

                        rightTeamTopBans.Width = Math.Round(rightTeamTopBans.Width * (1600f / 1920f), 2);
                        rightTeamTopBans.Height = Math.Round(rightTeamTopBans.Height * (1600f / 1920f), 2);
                        foreach (UIElement obj in rightTeamTopBans.Children)
                        {
                            float fl;
                            float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                            obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1600f / 1920f), 2));
                            float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                            obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1600f / 1920f), 2));
                            float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                            obj.SetValue(WidthProperty, Math.Round(fl * (1600f / 1920f), 2));
                            float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                            obj.SetValue(HeightProperty, Math.Round(fl * (1600f / 1920f), 2));
                            if (obj.GetType() == typeof(Label))
                                (obj as Label).FontSize *= (1600f / 1920f);
                            else if (obj.GetType() == typeof(TextBlock))
                                (obj as TextBlock).FontSize *= (1600f / 1920f);
                        }
                    }
                }
                else if (resolution == "1920x1080")
                {
                    if (this.Width != 1920)
                    {
                        this.MinWidth = 1920;
                        this.Width = 1920;
                        this.MinHeight = 1080;
                        this.Height = 1080;
                        this.backgroundCanvas.Width = 1920;
                        this.backgroundCanvas.Height = 1080;
                        this.mainBackground.Width = 1920;
                        this.mainBackground.Height = 1080;
                        this.resolutionIndex = 1;
                        if (settingsWindow != null)
                            settingsWindow.resolution_Combo.SelectedIndex = 1;

                        if (File.Exists(resourcesPath + "/Display/.layout"))
                        {
                            string[] lines = File.ReadAllLines(resourcesPath + "/Display/.layout");
                            for (int i = 0; i < lines.Count(); i++)
                            {
                                if (float.TryParse(lines[i], out float f) && i != 22 && i != 72)
                                    lines[i] = Math.Round(f * (1920f / 1600f), 2).ToString();
                            }
                            File.WriteAllLines(resourcesPath + "/Display/.layout", lines);

                        }

                        playerNamesGrid.Width = Math.Round(playerNamesGrid.Width * (1920f / 1600f), 2);
                        playerNamesGrid.Height = Math.Round(playerNamesGrid.Height * (1920f / 1600f), 2);
                        foreach (UIElement obj in playerNamesGrid.Children)
                        {
                            float fl;
                            float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                            obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1920f / 1600f), 2));
                            float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                            obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1920f / 1600f), 2));
                            float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                            obj.SetValue(WidthProperty, Math.Round(fl * (1920f / 1600f), 2));
                            float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                            obj.SetValue(HeightProperty, Math.Round(fl * (1920f / 1600f), 2));
                            if (obj.GetType() == typeof(Label))
                                (obj as Label).FontSize *= (1920f / 1600f);
                            else if (obj.GetType() == typeof(TextBlock))
                                (obj as TextBlock).FontSize *= (1920f / 1600f);
                        }

                        leftTeamTopBans.Width = Math.Round(leftTeamTopBans.Width * (1920f / 1600f), 2);
                        leftTeamTopBans.Height = Math.Round(leftTeamTopBans.Height * (1920f / 1600f), 2);
                        foreach (UIElement obj in leftTeamTopBans.Children)
                        {
                            float fl;
                            float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                            obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1920f / 1600f), 2));
                            float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                            obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1920f / 1600f), 2));
                            float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                            obj.SetValue(WidthProperty, Math.Round(fl * (1920f / 1600f), 2));
                            float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                            obj.SetValue(HeightProperty, Math.Round(fl * (1920f / 1600f), 2));
                            if (obj.GetType() == typeof(Label))
                                (obj as Label).FontSize *= (1920f / 1600f);
                            else if (obj.GetType() == typeof(TextBlock))
                                (obj as TextBlock).FontSize *= (1920f / 1600f);
                        }

                        rightTeamTopBans.Width = Math.Round(rightTeamTopBans.Width * (1920f / 1600f), 2);
                        rightTeamTopBans.Height = Math.Round(rightTeamTopBans.Height * (1920f / 1600f), 2);
                        foreach (UIElement obj in rightTeamTopBans.Children)
                        {
                            float fl;
                            float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                            obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1920f / 1600f), 2));
                            float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                            obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1920f / 1600f), 2));
                            float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                            obj.SetValue(WidthProperty, Math.Round(fl * (1920f / 1600f), 2));
                            float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                            obj.SetValue(HeightProperty, Math.Round(fl * (1920f / 1600f), 2));
                            if (obj.GetType() == typeof(Label))
                                (obj as Label).FontSize *= (1920f / 1600f);
                            else if (obj.GetType() == typeof(TextBlock))
                                (obj as TextBlock).FontSize *= (1920f / 1600f);
                        }

                    }
                }
                else
                {

                    ComboBoxItem resSelected = (ComboBoxItem)(settingsWindow.resolution_Combo.SelectedItem);
                    if (resSelected.Content.ToString() == "1600x900" && !exitingApp)
                    {
                        ChangeToSmallerRes();
                    }
                    else if (resSelected.Content.ToString() == "1920x1080" && !exitingApp)
                    {
                        ChangeToLargerRes();
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void ChangeToSmallerRes()
        {
            if (this.Width != 1600)
            {
                this.MinWidth = 1600;
                this.Width = 1600;
                this.MinHeight = 900;
                this.Height = 900;
                this.backgroundCanvas.Width = 1600;
                this.backgroundCanvas.Height = 900;
                this.mainBackground.Width = 1600;
                this.mainBackground.Height = 900;
                if (settingsWindow != null)
                    settingsWindow.resolution_Combo.SelectedIndex = 0;

                if (File.Exists(resourcesPath + "/Display/.layout"))
                {
                    string[] lines = File.ReadAllLines(resourcesPath + "/Display/.layout");
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        if (float.TryParse(lines[i], out float f) && i != 22 && i != 72)
                            lines[i] = Math.Round(f * (1600f / 1920f), 2).ToString();
                    }
                    File.WriteAllLines(resourcesPath + "/Display/.layout", lines);

                }

                playerNamesGrid.Width = Math.Round(playerNamesGrid.Width * (1600f / 1920f), 2);
                playerNamesGrid.Height = Math.Round(playerNamesGrid.Height * (1600f / 1920f), 2);
                foreach (UIElement obj in playerNamesGrid.Children)
                {
                    float fl;
                    float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                    obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1600f / 1920f), 2));
                    float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                    obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1600f / 1920f), 2));
                    float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                    obj.SetValue(WidthProperty, Math.Round(fl * (1600f / 1920f), 2));
                    float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                    obj.SetValue(HeightProperty, Math.Round(fl * (1600f / 1920f), 2));
                    if (obj.GetType() == typeof(Label))
                        (obj as Label).FontSize *= (1600f / 1920f);
                    else if (obj.GetType() == typeof(TextBlock))
                        (obj as TextBlock).FontSize *= (1600f / 1920f);
                }

                leftTeamTopBans.Width = Math.Round(leftTeamTopBans.Width * (1600f / 1920f), 2);
                leftTeamTopBans.Height = Math.Round(leftTeamTopBans.Height * (1600f / 1920f), 2);
                foreach (UIElement obj in leftTeamTopBans.Children)
                {
                    float fl;
                    float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                    obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1600f / 1920f), 2));
                    float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                    obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1600f / 1920f), 2));
                    float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                    obj.SetValue(WidthProperty, Math.Round(fl * (1600f / 1920f), 2));
                    float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                    obj.SetValue(HeightProperty, Math.Round(fl * (1600f / 1920f), 2));
                    if (obj.GetType() == typeof(Label))
                        (obj as Label).FontSize *= (1600f / 1920f);
                    else if (obj.GetType() == typeof(TextBlock))
                        (obj as TextBlock).FontSize *= (1600f / 1920f);
                }

                rightTeamTopBans.Width = Math.Round(rightTeamTopBans.Width * (1600f / 1920f), 2);
                rightTeamTopBans.Height = Math.Round(rightTeamTopBans.Height * (1600f / 1920f), 2);
                foreach (UIElement obj in rightTeamTopBans.Children)
                {
                    float fl;
                    float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                    obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1600f / 1920f), 2));
                    float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                    obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1600f / 1920f), 2));
                    float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                    obj.SetValue(WidthProperty, Math.Round(fl * (1600f / 1920f), 2));
                    float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                    obj.SetValue(HeightProperty, Math.Round(fl * (1600f / 1920f), 2));
                    if (obj.GetType() == typeof(Label))
                        (obj as Label).FontSize *= (1600f / 1920f);
                    else if (obj.GetType() == typeof(TextBlock))
                        (obj as TextBlock).FontSize *= (1600f / 1920f);
                }

                settingsWindow.AdvancedSet = new AdvancedSettings(this, settingsWindow);
                settingsWindow.AdvancedSet.Show();
                settingsWindow.AdvancedSet.ReadFromFile();
                settingsWindow.AdvancedSet.Preview();
                settingsWindow.AdvancedSet.SaveToFile();
                settingsWindow.AdvancedSet.Close();


            }
        }

        private void ChangeToLargerRes()
        {
            if (this.Width != 1920)
            {
                this.MinWidth = 1920;
                this.Width = 1920;
                this.MinHeight = 1080;
                this.Height = 1080;
                this.backgroundCanvas.Width = 1920;
                this.backgroundCanvas.Height = 1080;
                this.mainBackground.Width = 1920;
                this.mainBackground.Height = 1080;
                if (settingsWindow != null)
                    settingsWindow.resolution_Combo.SelectedIndex = 1;

                if (File.Exists(resourcesPath + "/Display/.layout"))
                {
                    string[] lines = File.ReadAllLines(resourcesPath + "/Display/.layout");
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        if (float.TryParse(lines[i], out float f) && i != 22 && i != 72)
                            lines[i] = Math.Round(f * (1920f / 1600f), 2).ToString();
                    }
                    File.WriteAllLines(resourcesPath + "/Display/.layout", lines);

                }

                playerNamesGrid.Width = Math.Round(playerNamesGrid.Width * (1920f / 1600f), 2);
                playerNamesGrid.Height = Math.Round(playerNamesGrid.Height * (1920f / 1600f), 2);
                foreach (UIElement obj in playerNamesGrid.Children)
                {
                    float fl;
                    float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                    obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1920f / 1600f), 2));
                    float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                    obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1920f / 1600f), 2));
                    float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                    obj.SetValue(WidthProperty, Math.Round(fl * (1920f / 1600f), 2));
                    float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                    obj.SetValue(HeightProperty, Math.Round(fl * (1920f / 1600f), 2));
                    if (obj.GetType() == typeof(Label))
                        (obj as Label).FontSize *= (1920f / 1600f);
                    else if (obj.GetType() == typeof(TextBlock))
                        (obj as TextBlock).FontSize *= (1920f / 1600f);
                }

                leftTeamTopBans.Width = Math.Round(leftTeamTopBans.Width * (1920f / 1600f), 2);
                leftTeamTopBans.Height = Math.Round(leftTeamTopBans.Height * (1920f / 1600f), 2);
                foreach (UIElement obj in leftTeamTopBans.Children)
                {
                    float fl;
                    float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                    obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1920f / 1600f), 2));
                    float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                    obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1920f / 1600f), 2));
                    float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                    obj.SetValue(WidthProperty, Math.Round(fl * (1920f / 1600f), 2));
                    float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                    obj.SetValue(HeightProperty, Math.Round(fl * (1920f / 1600f), 2));
                    if (obj.GetType() == typeof(Label))
                        (obj as Label).FontSize *= (1920f / 1600f);
                    else if (obj.GetType() == typeof(TextBlock))
                        (obj as TextBlock).FontSize *= (1920f / 1600f);
                }

                rightTeamTopBans.Width = Math.Round(rightTeamTopBans.Width * (1920f / 1600f), 2);
                rightTeamTopBans.Height = Math.Round(rightTeamTopBans.Height * (1920f / 1600f), 2);
                foreach (UIElement obj in rightTeamTopBans.Children)
                {
                    float fl;
                    float.TryParse(obj.GetValue(Canvas.LeftProperty).ToString(), out fl);
                    obj.SetValue(Canvas.LeftProperty, Math.Round(fl * (1920f / 1600f), 2));
                    float.TryParse(obj.GetValue(Canvas.TopProperty).ToString(), out fl);
                    obj.SetValue(Canvas.TopProperty, Math.Round(fl * (1920f / 1600f), 2));
                    float.TryParse(obj.GetValue(WidthProperty).ToString(), out fl);
                    obj.SetValue(WidthProperty, Math.Round(fl * (1920f / 1600f), 2));
                    float.TryParse(obj.GetValue(HeightProperty).ToString(), out fl);
                    obj.SetValue(HeightProperty, Math.Round(fl * (1920f / 1600f), 2));
                    if (obj.GetType() == typeof(Label))
                        (obj as Label).FontSize *= (1920f / 1600f);
                    else if (obj.GetType() == typeof(TextBlock))
                        (obj as TextBlock).FontSize *= (1920f / 1600f);
                }


                settingsWindow.AdvancedSet = new AdvancedSettings(this, settingsWindow);
                settingsWindow.AdvancedSet.Show();
                settingsWindow.AdvancedSet.ReadFromFile();
                settingsWindow.AdvancedSet.Preview();
                settingsWindow.AdvancedSet.SaveToFile();
                settingsWindow.AdvancedSet.Close();

            }
        }

        public void SwapTeams()
        {
            try
            {
                int leftSelectedIndex = settingsWindow.teamOneSelection.SelectedIndex;
                settingsWindow.teamOneSelection.SelectedIndex = settingsWindow.teamTwoSelection.SelectedIndex;
                settingsWindow.teamTwoSelection.SelectedIndex = leftSelectedIndex;

                string leftTeamScore = settingsWindow.teamOneScore.Text;
                settingsWindow.teamOneScore.Text = settingsWindow.teamTwoScore.Text;
                settingsWindow.teamTwoScore.Text = leftTeamScore;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void SwapPicks()
        {
            for (int i = 0; i < 5; i++)
            {
                int leftTeamPick = pickCombos[i].SelectedIndex;
                pickCombos[i].SelectedIndex = pickCombos[i + 5].SelectedIndex;
                pickCombos[i + 5].SelectedIndex = leftTeamPick;

                bool leftPickLocked = (bool)lockedCheckboxes[i].IsChecked;
                lockedCheckboxes[i].IsChecked = lockedCheckboxes[i + 5].IsChecked;
                lockedCheckboxes[i + 5].IsChecked = leftPickLocked;
            }
        }

        public void SwapBans()
        {
            for (int i = 0; i < 5; i++)
            {
                int leftTeamBan = banCombos[i].SelectedIndex;
                banCombos[i].SelectedIndex = banCombos[i + 5].SelectedIndex;
                banCombos[i + 5].SelectedIndex = leftTeamBan;
            }
        }

        public void SwapPlayers()
        {
            string leftSolo = settingsWindow.leftTeamSolo.Text;
            string leftJung = settingsWindow.leftTeamJung.Text;
            string leftMid = settingsWindow.leftTeamMid.Text;
            string leftSupp = settingsWindow.leftTeamSupp.Text;
            string leftCarry = settingsWindow.leftTeamCarry.Text;
            settingsWindow.leftTeamSolo.Text = settingsWindow.rightTeamSolo.Text;
            settingsWindow.leftTeamJung.Text = settingsWindow.rightTeamJung.Text;
            settingsWindow.leftTeamMid.Text = settingsWindow.rightTeamMid.Text;
            settingsWindow.leftTeamSupp.Text = settingsWindow.rightTeamSupp.Text;
            settingsWindow.leftTeamCarry.Text = settingsWindow.rightTeamCarry.Text;
            settingsWindow.rightTeamSolo.Text = leftSolo;
            settingsWindow.rightTeamJung.Text = leftJung;
            settingsWindow.rightTeamMid.Text = leftMid;
            settingsWindow.rightTeamSupp.Text = leftSupp;
            settingsWindow.rightTeamCarry.Text = leftCarry;
        }

        private void AdjustTeamOneDisplay(string selectedTeam)
        {
            this.leftTeamDisplay.Source = new BitmapImage(new Uri(resourcesPath + "/Teams/" + selectedTeam + "/Left.png", UriKind.Absolute));
            this.leftTeamPicks.Source = new BitmapImage(new Uri(resourcesPath + "/Teams/" + selectedTeam + "/PnBLeft.png", UriKind.Absolute));

            if (File.Exists(resourcesPath + "/Teams/" + selectedTeam + "/InGameDisplay.png"))
                inGameOverlay.leftTeamScoreboardLogo.Source = new BitmapImage(new Uri(resourcesPath + "/Teams/" + selectedTeam + "/InGameDisplay.png", UriKind.Absolute));

            if (File.Exists(resourcesPath + "/Teams/" + selectedTeam + "/roster.txt"))
            {
                try
                {
                    string[] lines = File.ReadAllLines(resourcesPath + "/Teams/" + selectedTeam + "/roster.txt");
                    settingsWindow.leftTeamSolo.Text = lines[0];
                    settingsWindow.leftTeamJung.Text = lines[1];
                    settingsWindow.leftTeamMid.Text = lines[2];
                    settingsWindow.leftTeamSupp.Text = lines[3];
                    settingsWindow.leftTeamCarry.Text = lines[4];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    settingsWindow.leftTeamSolo.Text = "Player 1";
                    settingsWindow.leftTeamJung.Text = "Player 2";
                    settingsWindow.leftTeamMid.Text = "Player 3";
                    settingsWindow.leftTeamSupp.Text = "Player 4";
                    settingsWindow.leftTeamCarry.Text = "Player 5";
                }
            }
            else
            {
                settingsWindow.leftTeamSolo.Text = "Player 1";
                settingsWindow.leftTeamJung.Text = "Player 2";
                settingsWindow.leftTeamMid.Text = "Player 3";
                settingsWindow.leftTeamSupp.Text = "Player 4";
                settingsWindow.leftTeamCarry.Text = "Player 5";
            }
        }

        private void AdjustTeamTwoDisplay(string selectedTeam)
        {
            this.rightTeamDisplay.Source = new BitmapImage(new Uri(resourcesPath + "/Teams/" + selectedTeam + "/Right.png", UriKind.Absolute));
            this.rightTeamPicks.Source = new BitmapImage(new Uri(resourcesPath + "/Teams/" + selectedTeam + "/PnBRight.png", UriKind.Absolute));


            if (File.Exists(resourcesPath + "/Teams/" + selectedTeam + "/InGameDisplay.png"))
                inGameOverlay.rightTeamScoreboardLogo.Source = new BitmapImage(new Uri(resourcesPath + "/Teams/" + selectedTeam + "/InGameDisplay.png", UriKind.Absolute));

            if (File.Exists(resourcesPath + "/Teams/" + selectedTeam + "/roster.txt"))
            {
                try
                {
                    string[] lines = File.ReadAllLines(resourcesPath + "/Teams/" + selectedTeam + "/roster.txt");
                    settingsWindow.rightTeamSolo.Text = lines[0];
                    settingsWindow.rightTeamJung.Text = lines[1];
                    settingsWindow.rightTeamMid.Text = lines[2];
                    settingsWindow.rightTeamSupp.Text = lines[3];
                    settingsWindow.rightTeamCarry.Text = lines[4];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    settingsWindow.rightTeamSolo.Text = "Player 1";
                    settingsWindow.rightTeamJung.Text = "Player 2";
                    settingsWindow.rightTeamMid.Text = "Player 3";
                    settingsWindow.rightTeamSupp.Text = "Player 4";
                    settingsWindow.rightTeamCarry.Text = "Player 5";
                }
            }
            else
            {
                settingsWindow.rightTeamSolo.Text = "Player 1";
                settingsWindow.rightTeamJung.Text = "Player 2";
                settingsWindow.rightTeamMid.Text = "Player 3";
                settingsWindow.rightTeamSupp.Text = "Player 4";
                settingsWindow.rightTeamCarry.Text = "Player 5";
            }
        }

        public void ResetPicks()
        {
            foreach (ComboBox box in this.pickCombos)
                box.SelectedIndex = 0;
            foreach (CheckBox cBox in this.lockedCheckboxes)
                cBox.IsChecked = false;
            foreach (PickDisplay pick in this.pickDisplays)
                pick.Reset();
        }

        public void ResetBans()
        {
            foreach (ComboBox box in this.banCombos)
                box.SelectedIndex = 0;
            foreach (BanDisplay ban in this.banDisplays)
                ban.Reset();
        }

        public void UpdateTeams()
        {
            try
            {
                ComboBoxItem teamOneSelection = (ComboBoxItem)(settingsWindow.teamOneSelection.SelectedItem);
                AdjustTeamOneDisplay(teamOneSelection.Content.ToString());
                inGameOverlay.teamOneNameDisplay.Content = teamOneSelection.Content.ToString();
                ComboBoxItem teamTwoSelection = (ComboBoxItem)(settingsWindow.teamTwoSelection.SelectedItem);
                AdjustTeamTwoDisplay(teamTwoSelection.Content.ToString());
                inGameOverlay.teamTwoNameDisplay.Content = teamTwoSelection.Content.ToString();
                try
                {
                    this.leftTeamTopBansLabel.Text = teamOneSelection.Content.ToString();
                    Dictionary<string, float> teamOneTopBans = GetTopNBansFromTeam(teamOneSelection.Content.ToString(), 6);
                    this.leftTopBan1.percentText.Content = (teamOneTopBans[teamOneTopBans.Keys.ElementAt(0)] * 100).ToString() + "%";
                    this.leftTopBan1.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamOneTopBans.Keys.ElementAt(0) + ".png", UriKind.Absolute));
                    this.leftTopBan2.percentText.Content = (teamOneTopBans[teamOneTopBans.Keys.ElementAt(1)] * 100).ToString() + "%";
                    this.leftTopBan2.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamOneTopBans.Keys.ElementAt(1) + ".png", UriKind.Absolute));
                    this.leftTopBan3.percentText.Content = (teamOneTopBans[teamOneTopBans.Keys.ElementAt(2)] * 100).ToString() + "%";
                    this.leftTopBan3.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamOneTopBans.Keys.ElementAt(2) + ".png", UriKind.Absolute));
                    this.leftTopBan4.percentText.Content = (teamOneTopBans[teamOneTopBans.Keys.ElementAt(3)] * 100).ToString() + "%";
                    this.leftTopBan4.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamOneTopBans.Keys.ElementAt(3) + ".png", UriKind.Absolute));
                    this.leftTopBan5.percentText.Content = (teamOneTopBans[teamOneTopBans.Keys.ElementAt(4)] * 100).ToString() + "%";
                    this.leftTopBan5.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamOneTopBans.Keys.ElementAt(4) + ".png", UriKind.Absolute));
                    this.leftTopBan6.percentText.Content = (teamOneTopBans[teamOneTopBans.Keys.ElementAt(5)] * 100).ToString() + "%";
                    this.leftTopBan6.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamOneTopBans.Keys.ElementAt(5) + ".png", UriKind.Absolute));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                try
                {
                    this.rightTeamTopBansLabel.Text = teamTwoSelection.Content.ToString();
                    Dictionary<string, float> teamTwoTopBans = GetTopNBansFromTeam(teamTwoSelection.Content.ToString(), 6);
                    this.rightTopBan1.percentText.Content = (teamTwoTopBans[teamTwoTopBans.Keys.ElementAt(0)] * 100).ToString() + "%";
                    this.rightTopBan1.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamTwoTopBans.Keys.ElementAt(0) + ".png", UriKind.Absolute));
                    this.rightTopBan2.percentText.Content = (teamTwoTopBans[teamTwoTopBans.Keys.ElementAt(1)] * 100).ToString() + "%";
                    this.rightTopBan2.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamTwoTopBans.Keys.ElementAt(1) + ".png", UriKind.Absolute));
                    this.rightTopBan3.percentText.Content = (teamTwoTopBans[teamTwoTopBans.Keys.ElementAt(2)] * 100).ToString() + "%";
                    this.rightTopBan3.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamTwoTopBans.Keys.ElementAt(2) + ".png", UriKind.Absolute));
                    this.rightTopBan4.percentText.Content = (teamTwoTopBans[teamTwoTopBans.Keys.ElementAt(3)] * 100).ToString() + "%";
                    this.rightTopBan4.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamTwoTopBans.Keys.ElementAt(3) + ".png", UriKind.Absolute));
                    this.rightTopBan5.percentText.Content = (teamTwoTopBans[teamTwoTopBans.Keys.ElementAt(4)] * 100).ToString() + "%";
                    this.rightTopBan5.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamTwoTopBans.Keys.ElementAt(4) + ".png", UriKind.Absolute));
                    this.rightTopBan6.percentText.Content = (teamTwoTopBans[teamTwoTopBans.Keys.ElementAt(5)] * 100).ToString() + "%";
                    this.rightTopBan6.godImage.Source = new BitmapImage(new Uri(resourcesPath + "/CharacterImages/TopBans/" + teamTwoTopBans.Keys.ElementAt(5) + ".png", UriKind.Absolute));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void UpdateScores()
        {
            try
            {
                leftTeamScore.Content = settingsWindow.teamOneScore.Text;
                rightTeamScore.Content = settingsWindow.teamTwoScore.Text;
                inGameOverlay.teamOneScore.Content = settingsWindow.teamOneScore.Text;
                inGameOverlay.teamTwoScore.Content = settingsWindow.teamTwoScore.Text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void UpdatePlayerNames()
        {
            try
            {
                Visibility visib = Visibility.Visible;
                if (settingsWindow.playerNameActiveBox.IsChecked == false)
                {
                    visib = Visibility.Hidden;
                }
                this.playerNamesGrid.Visibility = visib;

                this.leftTeamSolo.Content = settingsWindow.leftTeamSolo.Text;
                this.leftTeamJung.Content = settingsWindow.leftTeamJung.Text;
                this.leftTeamMid.Content = settingsWindow.leftTeamMid.Text;
                this.leftTeamSupp.Content = settingsWindow.leftTeamSupp.Text;
                this.leftTeamCarry.Content = settingsWindow.leftTeamCarry.Text;
                this.rightTeamSolo.Content = settingsWindow.rightTeamSolo.Text;
                this.rightTeamJung.Content = settingsWindow.rightTeamJung.Text;
                this.rightTeamMid.Content = settingsWindow.rightTeamMid.Text;
                this.rightTeamSupp.Content = settingsWindow.rightTeamSupp.Text;
                this.rightTeamCarry.Content = settingsWindow.rightTeamCarry.Text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void UpdateSeriesDisplay()
        {
            try
            {
                inGameOverlay.centerText.Content = settingsWindow.seriesInput.Text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void UpdateBanDatas()
        {
            try
            {
                if (settingsWindow.leftTeamBanDataActive.IsChecked == false)
                {
                    this.leftTeamTopBans.Visibility = Visibility.Hidden;
                }
                else
                    this.leftTeamTopBans.Visibility = Visibility.Visible;

                if (settingsWindow.rightTeamBanDataActive.IsChecked == false)
                {
                    this.rightTeamTopBans.Visibility = Visibility.Hidden;
                }
                else
                    this.rightTeamTopBans.Visibility = Visibility.Visible;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initializes all the lists and adds the UI elements into them.
        /// </summary>
        public void InitLists()
        {
            try
            {
                pickCombos = new List<ComboBox>();
                pickCombos.Add(settingsWindow.teamOnePick1);
                pickCombos.Add(settingsWindow.teamOnePick2);
                pickCombos.Add(settingsWindow.teamOnePick3);
                pickCombos.Add(settingsWindow.teamOnePick4);
                pickCombos.Add(settingsWindow.teamOnePick5);
                pickCombos.Add(settingsWindow.teamTwoPick1);
                pickCombos.Add(settingsWindow.teamTwoPick2);
                pickCombos.Add(settingsWindow.teamTwoPick3);
                pickCombos.Add(settingsWindow.teamTwoPick4);
                pickCombos.Add(settingsWindow.teamTwoPick5);

                pickDisplays = new List<PickDisplay>();
                pickDisplays.Add(this.leftTeamPick1);
                pickDisplays.Add(this.leftTeamPick2);
                pickDisplays.Add(this.leftTeamPick3);
                pickDisplays.Add(this.leftTeamPick4);
                pickDisplays.Add(this.leftTeamPick5);
                pickDisplays.Add(this.rightTeamPick1);
                pickDisplays.Add(this.rightTeamPick2);
                pickDisplays.Add(this.rightTeamPick3);
                pickDisplays.Add(this.rightTeamPick4);
                pickDisplays.Add(this.rightTeamPick5);

                lockedCheckboxes = new List<CheckBox>();
                lockedCheckboxes.Add(settingsWindow.teamOneLock1);
                lockedCheckboxes.Add(settingsWindow.teamOneLock2);
                lockedCheckboxes.Add(settingsWindow.teamOneLock3);
                lockedCheckboxes.Add(settingsWindow.teamOneLock4);
                lockedCheckboxes.Add(settingsWindow.teamOneLock5);
                lockedCheckboxes.Add(settingsWindow.teamTwoLock1);
                lockedCheckboxes.Add(settingsWindow.teamTwoLock2);
                lockedCheckboxes.Add(settingsWindow.teamTwoLock3);
                lockedCheckboxes.Add(settingsWindow.teamTwoLock4);
                lockedCheckboxes.Add(settingsWindow.teamTwoLock5);

                banCombos = new List<ComboBox>();
                banCombos.Add(settingsWindow.teamOneBan1);
                banCombos.Add(settingsWindow.teamOneBan2);
                banCombos.Add(settingsWindow.teamOneBan3);
                banCombos.Add(settingsWindow.teamOneBan4);
                banCombos.Add(settingsWindow.teamOneBan5);
                banCombos.Add(settingsWindow.teamTwoBan1);
                banCombos.Add(settingsWindow.teamTwoBan2);
                banCombos.Add(settingsWindow.teamTwoBan3);
                banCombos.Add(settingsWindow.teamTwoBan4);
                banCombos.Add(settingsWindow.teamTwoBan5);

                banDisplays = new List<BanDisplay>();
                banDisplays.Add(this.leftTeamBan1);
                banDisplays.Add(this.leftTeamBan2);
                banDisplays.Add(this.leftTeamBan3);
                banDisplays.Add(this.leftTeamBan4);
                banDisplays.Add(this.leftTeamBan5);
                banDisplays.Add(this.rightTeamBan1);
                banDisplays.Add(this.rightTeamBan2);
                banDisplays.Add(this.rightTeamBan3);
                banDisplays.Add(this.rightTeamBan4);
                banDisplays.Add(this.rightTeamBan5);

                for (int i = 0; i < pickDisplays.Count; i++)
                {
                    pickDisplays[i].MainWindow = this;
                    pickDisplays[i].PickCombo = pickCombos[i];
                    pickDisplays[i].LockCheck = lockedCheckboxes[i];
                    if (i > 4)
                        pickDisplays[i].Reversed = true;
                    else
                        pickDisplays[i].Reversed = false;

                    banDisplays[i].MainWindow = this;
                    banDisplays[i].BanCombo = banCombos[i];
                    if (i > 4)
                        banDisplays[i].Reversed = true;
                    else
                        banDisplays[i].Reversed = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private Dictionary<string, float> GetTopNBansFromTeam(string team, int n)
        {
            foreach (BanData banData in banDatas)
            {
                if (banData.TeamName == team)
                    return banData.GetTopNBans(n);
            }
            return null;
        }

        public bool SubmitDraftToBanData()
        {
            foreach (ComboBox combo in banCombos)
            {
                if (combo.Text == "Empty")
                    return false;
            }

            foreach (BanData banData in banDatas)
                banData.CheckForAllGods(resourcesPath);

            List<string> gods = new List<string>();
            ComboBoxItem teamItem = (ComboBoxItem)settingsWindow.teamOneSelection.SelectedItem;
            ComboBoxItem team2Item = (ComboBoxItem)settingsWindow.teamTwoSelection.SelectedItem;
            string team = teamItem.Content.ToString();
            string team2 = team2Item.Content.ToString();
            for (int i = 0; i < banCombos.Count / 2; i++)
            {
                AddBanToTeamBanData(team2, banCombos[i].Text);
                gods.Add(banCombos[i].Text);
            }
            AddGameToTeamBanData(team2);
            //submissionHistory.AddSubmission(new BanDataSubmission(team, gods));
            List<string> gods2 = new List<string>();
            for (int j = banCombos.Count / 2; j < banCombos.Count; j++)
            {
                AddBanToTeamBanData(team, banCombos[j].Text);
                gods2.Add(banCombos[j].Text);
            }
            AddGameToTeamBanData(team);
            //submissionHistory.AddSubmission(new BanDataSubmission(team, gods2));

            foreach (BanData data in banDatas)
            {
                data.SaveToFile(resourcesPath + "/Teams/" + data.TeamName + "/BanData.json");
            }
            //submissionHistory.SaveToFile("HistoryData.json");
            return true;
        }

        private void AddBanToTeamBanData(string team, string godBanned)
        {
            foreach (BanData banData in banDatas)
            {
                if (team == banData.TeamName)
                    banData.AddBan(godBanned);
            }
        }

        private void AddGameToTeamBanData(string team)
        {
            foreach (BanData banData in banDatas)
            {
                if (team == banData.TeamName)
                    banData.TotalGames += 1;
            }
        }

        public void TryFileUpdate()
        {
            try
            {
                if (resourcesPath != "")
                {
                    VerifyResources();
                    banDatas = new List<BanData>();
                    settingsWindow.soundPathDisplay.Text = resourcesPath;
                    int teamOneIndex = settingsWindow.teamOneCombo.SelectedIndex;
                    int teamTwoIndex = settingsWindow.teamTwoCombo.SelectedIndex;
                    settingsWindow.teamOneCombo.Items.Clear();
                    settingsWindow.teamTwoCombo.Items.Clear();
                    string[] teamFolders = Directory.GetDirectories(resourcesPath + "/Teams");
                    foreach (string team in teamFolders)
                    {
                        string teamFormatted = team.Replace("\\", "/");
                        string[] teamSplit = teamFormatted.Split('/');
                        string teamName = teamSplit[teamSplit.Length - 1];
                        Style comboStyle = FindResource("comboDesign") as Style;
                        ComboBoxItem teamComboItem = new ComboBoxItem();
                        teamComboItem.Content = teamName;
                        teamComboItem.Style = comboStyle;
                        settingsWindow.teamOneCombo.Items.Add(teamComboItem);

                        try
                        {
                            if (File.Exists(teamFormatted + "/BanData.json"))
                            {
                                BanData toAdd = new BanData(teamName, resourcesPath);
                                toAdd = BanData.CreateFromJson(teamFormatted + "/BanData.json");
                                banDatas.Add(toAdd);
                            }
                            else
                            {
                                BanData toAdd = new BanData(teamName, resourcesPath);
                                banDatas.Add(toAdd);
                            }
                        }
                        catch (Exception e5)
                        {
                            Console.WriteLine(e5.StackTrace);
                        }
                    }
                    foreach (string team in teamFolders)
                    {
                        string teamFormatted = team.Replace("\\", "/");
                        string[] teamSplit = teamFormatted.Split('/');
                        string teamName = teamSplit[teamSplit.Length - 1];
                        Style comboStyle = FindResource("comboDesign") as Style;
                        ComboBoxItem teamComboItem = new ComboBoxItem();
                        teamComboItem.Content = teamName;
                        teamComboItem.Style = comboStyle;
                        settingsWindow.teamTwoCombo.Items.Add(teamComboItem);
                    }

                    settingsWindow.teamOneCombo.SelectedIndex = teamOneIndex;
                    settingsWindow.teamTwoCombo.SelectedIndex = teamTwoIndex;

                    string[] lines = File.ReadAllLines(resourcesPath + "/CharactersList.txt");
                    foreach (ComboBox box in this.pickCombos)
                    {
                        box.Items.Clear();
                        ComboBoxItem empty = new ComboBoxItem();
                        empty.Content = "Empty";
                        empty.Style = FindResource("comboDesign") as Style;
                        box.Items.Add(empty);
                        foreach (string god in lines)
                        {
                            if (god != "NameOverlay")
                            {
                                Style comboStyle = FindResource("comboDesign") as Style;
                                ComboBoxItem comboItem = new ComboBoxItem();
                                comboItem.Style = comboStyle;
                                comboItem.Content = god;
                                box.Items.Add(comboItem);
                            }
                        }
                        box.SelectedIndex = 0;
                    }
                    foreach (ComboBox box in this.banCombos)
                    {
                        box.Items.Clear();
                        ComboBoxItem empty = new ComboBoxItem();
                        empty.Content = "Empty";
                        empty.Style = FindResource("comboDesign") as Style;
                        box.Items.Add(empty);
                        foreach (string god in lines)
                        {
                            if (god != "NameOverlay")
                            {
                                Style comboStyle = FindResource("comboDesign") as Style;
                                ComboBoxItem comboItem = new ComboBoxItem();
                                comboItem.Style = comboStyle;
                                comboItem.Content = god;
                                box.Items.Add(comboItem);
                            }
                        }
                        box.SelectedIndex = 0;
                    }

                    mainBackground.Source = new BitmapImage(new Uri(resourcesPath + "/Display/LayoutTemplate.png", UriKind.Absolute));
                    ResetBans();
                    ResetPicks();

                    /*
                    if(File.Exists("HistoryData.json"))
                    {
                        submissionHistory = SubmissionHistory.CreateFromJson("HistoryData.json");
                    }
                    else
                    {
                        submissionHistory = new SubmissionHistory(10);
                    }*/

                }
                settingsWindow.GrabAdvancedSettings();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void VerifyResources()
        {
            List<string> errors = new List<string>();

            char[] notAllowed = { '^', '&', '\'', '@', '{', '}', '[', ']', ',', '$', '=', '!', '-', '#', '(', ')', '%', '.', '+', '~', '_' };
            string[] pngFiles = { "Left.png", "Right.png", "PnBLeft.png", "PnBRight.png" };

            string[] teamFolders = Directory.GetDirectories(resourcesPath + "/Teams");
            foreach (string team in teamFolders)
            {
                string teamFormatted = team.Replace("\\", "/");
                string[] teamSplit = teamFormatted.Split('/');
                string teamName = teamSplit[teamSplit.Length - 1];
                foreach (char character in notAllowed)
                {
                    if (teamName.Contains(character))
                        errors.Add("Team Folder: " + teamName + " contains illegal character: " + character);
                }
                foreach (string file in pngFiles)
                {
                    if (!File.Exists(team + "/" + file))
                    {
                        errors.Add("Team Folder: " + teamName + " missing file: " + file);
                    }
                }
            }

            // Verify that all god resources are there
            string[] lines = File.ReadAllLines(resourcesPath + "/CharactersList.txt");
            string[] paths = { "/CharacterImages/Bans", "/CharacterImages/Picks", "/CharacterImages/TopBans" };

            foreach (string path in paths)
            {
                string[] files = Directory.GetFiles(resourcesPath + path);
                List<string> linesDuplicate = new List<string>(lines);
                foreach (string file in files)
                {
                    string fileFormatted = file.Replace("\\", "/");
                    string[] fileSplit = fileFormatted.Split('/');
                    string fileName = fileSplit[fileSplit.Length - 1];
                    bool found = false;
                    foreach (string line in lines)
                    {
                        if (fileName.Contains(line))
                        {
                            linesDuplicate.Remove(line);
                            found = true;
                        }
                    }
                    if (!found)
                        errors.Add("Unnecessary File in " + path + ": " + fileName);

                }
                if (linesDuplicate.Count > 0)
                {
                    foreach (string line in linesDuplicate)
                        errors.Add("Mssing file in " + path + " for Character: " + line);
                }
            }

            if (errors.Count > 0)
            {
                string message = "";
                foreach (string error in errors)
                    message += error + "\n";
                MessageBox.Show(message);
            }
        }

        #endregion

    }

}
