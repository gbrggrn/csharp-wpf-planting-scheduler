using Csharp2_PlantingScheduler.Control.Managers;
using Csharp2_PlantingScheduler.Helpers;
using Csharp2_PlantingScheduler.Model;
using Csharp2_PlantingScheduler.Model.Plants.TypesOfPlants.Flower;
using Csharp2_PlantingScheduler.Model.Plants.TypesOfPlants.Vegetable;
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

namespace Csharp2_PlantingScheduler
{
    /// <summary>
    /// Interaction logic for PlantWindow.xaml
    /// </summary>
    public partial class PlantWindow : Window
    {
        private readonly PlantManager plantManager;
        private const int nameMaxChar = 30;
        private bool editingFlag = false;
        private readonly int editingIndex;

        public PlantWindow(PlantManager currentPlantManager)
        {
            InitializeComponent();
            plantManager = currentPlantManager;
            InitComboBoxes();
            categoryComboBox.SelectionChanged += SwitchTypesComboBox;
            sowTypeComboBox.SelectionChanged += ToggleComboBoxes;
        }

        /// <summary>
        /// Second constructor if window is opened in editing mode
        /// </summary>
        /// <param name="currentPlantManager">The current instance of plantManager</param>
        /// <param name="index">Index of the plant to be edited</param>
        public PlantWindow(PlantManager currentPlantManager, int index)
        {
            InitializeComponent();
            plantManager = currentPlantManager;
            editingIndex = index;
            InitComboBoxes();
            categoryComboBox.SelectionChanged += SwitchTypesComboBox;
            sowTypeComboBox.SelectionChanged += ToggleComboBoxes;
            LoadPlantWhenEditing(editingIndex);
            windowName.Content = "Editing Plant";
        }

        /// <summary>
        /// Toggles combo boxes coldstart + indoorstart upon selection of sowType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleComboBoxes(object sender, RoutedEventArgs e)
        {
            if (sowTypeComboBox.SelectedIndex == 0)
            {
                coldStartComboBox.IsEnabled = true;
                indoorWeeksComboBox.IsEnabled = false;
            }
            if (sowTypeComboBox.SelectedIndex == 1)
            {
                coldStartComboBox.IsEnabled = false;
                indoorWeeksComboBox.IsEnabled = true;
            }
            if (sowTypeComboBox.SelectedIndex == 2)
            {
                coldStartComboBox.IsEnabled = false;
                indoorWeeksComboBox.IsEnabled = false;
            }
        }

        /// <summary>
        /// Loads the properties of the plant to edit to the UI
        /// </summary>
        /// <param name="index">Index of the plant to edit</param>
        private void LoadPlantWhenEditing(int index)
        {
            editingFlag = true;
            Plant plant = plantManager.GetAt(index);

            if (plant is Vegetable veg)
            {
                categoryComboBox.SelectedItem = veg.Category;
                sowTypeComboBox.SelectedItem = veg.SowType;
                nameTxtBox.Text = veg.SpeciesName;
                weeksToHarvestComboBox.SelectedItem = veg.WeeksToHarvest;
                coldStartComboBox.SelectedItem = veg.ColdStartWeeks;
                indoorWeeksComboBox.SelectedItem = veg.IndoorWeeks;
            }
            else if (plant is Flower flower)
            {
                //TO DO
            }
        }

        /// <summary>
        /// Initializes comboboxes and sets preselections to avoid nulls
        /// </summary>
        private void InitComboBoxes()
        {
            categoryComboBox.ItemsSource = Enum.GetNames(typeof(Enums.PlantCategory));
            sowTypeComboBox.ItemsSource = Enum.GetNames(typeof(Enums.SowType));
            typeComboBox.ItemsSource = Enum.GetNames(typeof(Enums.VegetableType));

            List<int> weeks = Enumerable.Range(1, 52).ToList();
            weeksToHarvestComboBox.ItemsSource = weeks;
            coldStartComboBox.ItemsSource = weeks;
            indoorWeeksComboBox.ItemsSource = weeks;

            indoorWeeksComboBox.SelectedIndex = 0;
            weeksToHarvestComboBox.SelectedIndex = 0;
            coldStartComboBox.SelectedIndex = 0;
            categoryComboBox.SelectedIndex = 0;
            sowTypeComboBox.SelectedIndex = 0;
            typeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Switches the content of the type combobox upon selection of category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchTypesComboBox(object sender, RoutedEventArgs e)
        {
            if (categoryComboBox.SelectedIndex == 1)
            {
                typeComboBox.ItemsSource = Enum.GetNames(typeof(Enums.VegetableType));
            }
            /*else
            {
                typeComboBox.ItemsSource = Enum.GetNames(typeof(Enums.FlowerType));
            }*/

            typeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// If the user wants: exits the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxes.DisplayQuestion("Are you sure? Unsaved changes will be lost", "Are you sure?"))
            {
                this.Close();
            }
        }

        /// <summary>
        /// Saves a new plant, or edits (replaces) the edited one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTxtBox.Text;

            if (!string.IsNullOrEmpty(name) && name.Length <= nameMaxChar)
            {
                Vegetable vegetable = new()
                {
                    SpeciesName = name,
                    Category = (Enums.PlantCategory)Enum.Parse(typeof(Enums.PlantCategory), categoryComboBox.SelectedItem.ToString()!),
                    SowType = (Enums.SowType)Enum.Parse(typeof(Enums.SowType), sowTypeComboBox.SelectedItem.ToString()!),
                    Type = (Enums.VegetableType)Enum.Parse(typeof(Enums.VegetableType), typeComboBox.SelectedItem.ToString()!),
                    WeeksToHarvest = (int)weeksToHarvestComboBox.SelectedItem
                };

                if (vegetable.SowType == Enums.SowType.Indoorstart)
                {
                    vegetable.IndoorWeeks = (int)indoorWeeksComboBox.SelectedItem;
                }
                else if (vegetable.SowType == Enums.SowType.Coldstart)
                {
                    vegetable.ColdStartWeeks = (int)coldStartComboBox.SelectedItem;
                }

                if (editingFlag)
                {
                    plantManager.ChangeAt(vegetable, editingIndex);
                }
                else
                {
                    plantManager.Add(vegetable);
                }

                MessageBoxes.DisplayInfoBox($"The {vegetable.Type.ToString()} {vegetable.SpeciesName} added!", "Success!");
                this.Close();
            }
            else
            {
                MessageBoxes.DisplayErrorBox($"Name can not be empty and max {nameMaxChar} characters");
            }
        }
    }
}
