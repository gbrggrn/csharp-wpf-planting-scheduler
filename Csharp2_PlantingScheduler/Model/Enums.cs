using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp2_PlantingScheduler.Model
{
    /// <summary>
    /// Enums for this program. Names are kind of self explanatory.
    /// </summary>
    public class Enums
    {
        public enum PlantCategory
        {
            //Flower,
            Vegetable
        }

        public enum GrowthType
        {
            Annual,
            Perennieal,
            Biennial
        }

        public enum SowType
        {
            Coldstart,
            Indoorstart,
            Directsow
        }

        public enum FlowerType
        {
            Lily
        }

        public enum VegetableType
        {
            Radish,
            Onion,
            Aubergine,
            Beet,
            Cucumber,
            Tomato,
            Kale,
            Cabbage,
            Carrot,
            Salad,
            Leaf,
            Pumpkin,
            Squash,
            Zucchini,
            Chili,
            Pepper,
            Pea,
            Bean,
            Herb
        }

        //Value = first frost free week (first transplant to outdoors possible)
        public enum GrowZone
        {
            Zone1 = 16,
            Zone2 = 17,
            Zone3 = 18,
            Zone4 = 19,
            Zone5 = 20,
            Zone6 = 21,
            Zone7 = 22,
            Zone8 = 23,
            Zone9 = 24,
            Mountain = 25
        }
    }
}
