using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OhmCalculator.Models
{
    public class Band
    {
        public string Color { get; set; }
        public int SignificantFigure { get; set; }
        public int Multiplier { get; set; }
        public double Tolerance { get; set; }
    }

    public interface IBandEntities
    {
        IEnumerable<Band> GetBands();

        Band GetBandByColor(string color);

        int GetSignificantFigureByColor(string color);
        double GetMultiPlierByColor(string color);
        double GetToleranceByColor(string color);
        //long CalculateOHMValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor);
    }

    public class BandEntities : IBandEntities
    {
        private IEnumerable<Band> bands;
        public BandEntities()
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

        //public long CalculateOHMValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        //{
        //    throw new NotImplementedException();
        //}

        public Band GetBandByColor(string color)
        {
            return bands.Where(b => b.Color.Equals(color.ToUpper())).FirstOrDefault();
        }

        public IEnumerable<Band> GetBands()
        {
            return bands;
        }

        public double GetMultiPlierByColor(string color)
        {
            var band = GetBandByColor(color);
            if (band == null) return 0;
            return (Math.Pow(10, band.Multiplier));
        }

        public int GetSignificantFigureByColor(string color)
        {
            var band = GetBandByColor(color);
            if (band == null) return 0;
            return band.SignificantFigure;
        }

        public double GetToleranceByColor(string color)
        {
            var band = GetBandByColor(color);
            if (band == null) return 0;
            return band.Tolerance;
        }
    }
}