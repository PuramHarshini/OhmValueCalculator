using System;
using System.Collections.Generic;
using System.Linq;

namespace OhmCalculator.Models
{
    public class Band
    {
        public string Color { get; set; }
        public int SignificantFigure { get; set; }
        public int Multiplier { get; set; }
        public double Tolerance { get; set; }
    }
        
    public interface IOhmValueCalculator
    {  
        ///<summary>
        /// <param name="bandAColor">The color of the first figure of component value band.</param>
        /// <param name="bandBColor">The color of the second significant figure band.</param>
        /// <param name="bandCColor">The color of the decimal multiplier band.</param>
        /// <param name="bandDColor">The color of the tolerance value band.</param>         /// 
        /// </summary>
        /// <returns>Returns a range of OhmValues as a dictionary object</returns>
        Dictionary<string, long> CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor);

        /// <summary>
        /// Get Bands - Get all the colors of te resistor bands
        /// </summary>
        /// <returns>Returns a list of colors(bands)</returns>
        IEnumerable<Band> GetBands();
    }

    public class OhmValueCalculator : IOhmValueCalculator
    {
        IEnumerable<Band> bands;

        /// <summary>
        /// Populating all the colors of the resistor into the bands list
        /// </summary>
        public OhmValueCalculator()
        {
            bands = new List<Band>()
            {
                new Band(){ Color="BLACK", SignificantFigure= 0, Multiplier=0, Tolerance=0},
                new Band(){ Color="BROWN", SignificantFigure= 1, Multiplier=1, Tolerance=1},
                new Band(){ Color="RED", SignificantFigure= 2, Multiplier=2, Tolerance=2},
                new Band(){ Color="ORANGE", SignificantFigure= 3, Multiplier=3, Tolerance=0},
                new Band(){ Color="YELLOW", SignificantFigure= 4, Multiplier=4, Tolerance=5},
                new Band(){ Color="GREEN", SignificantFigure= 5, Multiplier=5, Tolerance=0.5},
                new Band(){ Color="BLUE", SignificantFigure= 6, Multiplier=6, Tolerance=0.25},
                new Band(){ Color="VOILET", SignificantFigure= 7, Multiplier=7, Tolerance=0.1},
                new Band(){ Color="GRAY", SignificantFigure= 8, Multiplier=8, Tolerance=0.05},
                new Band(){ Color="WHITE", SignificantFigure= 9, Multiplier=9, Tolerance=0},
                new Band(){ Color="GOLD", SignificantFigure= 0, Multiplier=-1, Tolerance=5},
                new Band(){ Color="SILVER", SignificantFigure= 0, Multiplier=-2, Tolerance=10},
                new Band(){ Color="PINK", SignificantFigure= 0, Multiplier=-3, Tolerance=0}
            };
        }

        /// <summary>
        /// Get the list of bands
        /// </summary>
        /// <returns>Returns the list of bands</returns>
        public IEnumerable<Band> GetBands()
        {
            return bands;
        }

        /// <summary>
        /// Significant Figure - The numerical value based on the color of the band
        /// </summary>
        /// <param name="color"></param>
        /// <returns>Returns the significant figure</returns>
        public int GetSignificantFigureByColor(string color)
        {
            var band = GetBandByColor(color);

            if (band == null) return 0;

            return band.SignificantFigure;
        }

        /// <summary>
        /// Multiplier - The multiplier value based on the band color
        /// </summary>
        /// <param name="color"></param>
        /// <returns>Returns the multiplier value</returns>
        public double GetMultiPlierByColor(string color)
        {
            var band = GetBandByColor(color);

            if (band == null) return 1;

            return (Math.Pow(10, band.Multiplier));
        }

        /// <summary>
        /// Tolerance - Tolerance value based on the band color
        /// </summary>
        /// <param name="color"></param>
        /// <returns>Returns the tolerance value</returns>
        public double GetToleranceByColor(string color)
        {
            var band = GetBandByColor(color);

            if (band == null) return 0;

            return band.Tolerance;
        }

        /// <summary>
        /// Fetches all the data based on the band color
        /// </summary>
        /// <param name="color"></param>
        /// <returns>Returns the band related values</returns>
        public Band GetBandByColor(string color)
        {
            return bands.Where(b => b.Color.Equals(color.ToUpper())).FirstOrDefault();
        }

        /// <summary>
        /// Calculates the Ohm value of a resistor based on the band colors
        /// </summary>
        /// <param name="bandAColor"></param>
        /// <param name="bandBColor"></param>
        /// <param name="bandCColor"></param>
        /// <param name="bandDColor"></param>
        /// <returns>Returns the range of the resistance value</returns>
        public Dictionary<string, long> CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            try
            {
                var ohmValue = ((GetSignificantFigureByColor(bandAColor) * 10) + (GetSignificantFigureByColor(bandBColor))) * (GetMultiPlierByColor(bandCColor));

                var tolerance = GetToleranceByColor(bandDColor);

                var maxValue = ohmValue + (ohmValue * tolerance) / 100;
                var minValue = ohmValue - (ohmValue * tolerance) / 100;

                Dictionary<string, long> ohmValueRange = new Dictionary<string, long>()
            {
                { "minValue", Convert.ToInt64(minValue) },
                { "maxValue", Convert.ToInt64(maxValue) }
            };

                return ohmValueRange;
            }
            catch
            {
                return new Dictionary<string, long>()
                {
                    {"minValue", 0 },
                    {"maxValue", 0}
                };
            }

        }
    }
}