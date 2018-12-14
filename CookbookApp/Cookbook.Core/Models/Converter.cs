using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.Models
{
    public static class Converter
    {
        public enum Units
        {
            //Mass
            Grams = 1,
            KiloGrams = 2,
            Pounds = 3,
            Ounces = 4,
            Tonnes = 5,
            Hectogram = 0,

            //Length
            Metres = 101,
            Millimetres = 102,
            Centimeters = 103,
            Kilometers = 104,
            Miles = 105,
            Feet = 106,
            Inches = 107,
            LightYears = 100,

            //Volume
            Litres = 201,
            Millilitres = 202,
            Cups = 203,
            FluidOunces = 204,
            TeaSpoon = 205,
            TableSpoon = 206,
            CubicFeet = 207,
            CubicInch = 208,
            CubicCentimetres = 209,
            CubicMetres = 200,

            //Area
            SquareMillimetre = 301,
            SquareCentimetre = 302,
            SquareMetre = 303,
            Acre = 304,
            Hectare = 305,
            SquareMile = 306,
            SquareKilometer = 300



        }

        class UnitConverter
        {

            const int MassRange = 0;
            const int LengthRange = 100;
            const int VolumeRange = 200;
            const int AreaRange = 300;

            static readonly double[] ToBaseWeight = new double[6];
            static readonly double[] ToBaseLength = new double[8];
            static readonly double[] ToBaseVolume = new double[10];
            static readonly double[] ToBaseArea = new double[7];


            static UnitConverter()
            {
                ToBaseWeight[(int)Units.Grams] = 1;
                ToBaseWeight[(int)Units.Hectogram] = 100;
                ToBaseWeight[(int)Units.KiloGrams] = 1000;
                ToBaseWeight[(int)Units.Ounces] = 28.3495231;
                ToBaseWeight[(int)Units.Pounds] = 453.59237;
                ToBaseWeight[(int)Units.Tonnes] = 1000000;


                ToBaseLength[(int)Units.Metres - LengthRange] = 1;
                ToBaseLength[(int)Units.Millimetres - LengthRange] = 0.001;
                ToBaseLength[(int)Units.Centimeters - LengthRange] = 0.01;
                ToBaseLength[(int)Units.Kilometers - LengthRange] = 1000;
                ToBaseLength[(int)Units.Feet - LengthRange] = 0.3048;
                ToBaseLength[(int)Units.Inches - LengthRange] = 0.0254;
                ToBaseLength[(int)Units.LightYears - LengthRange] = 9460730472580000;
                ToBaseLength[(int)Units.Miles - LengthRange] = 1609.344;

                ToBaseVolume[(int)Units.Litres - VolumeRange] = 1000;
                ToBaseVolume[(int)Units.Millilitres - VolumeRange] = 1;
                ToBaseVolume[(int)Units.Cups - VolumeRange] = 236.588236;
                ToBaseVolume[(int)Units.FluidOunces - VolumeRange] = 29.5735296;
                ToBaseVolume[(int)Units.TeaSpoon - VolumeRange] = 4.92892159;
                ToBaseVolume[(int)Units.TableSpoon - VolumeRange] = 14.7867648;
                ToBaseVolume[(int)Units.CubicFeet - VolumeRange] = 28316.8466;
                ToBaseVolume[(int)Units.CubicInch - VolumeRange] = 16.387064;
                ToBaseVolume[(int)Units.CubicMetres - VolumeRange] = 1000000;
                ToBaseVolume[(int)Units.CubicCentimetres - VolumeRange] = 1;


                ToBaseArea[(int)Units.SquareMillimetre - AreaRange] = 0.000001;
                ToBaseArea[(int)Units.SquareCentimetre - AreaRange] = 0.0001;
                ToBaseArea[(int)Units.SquareMetre - AreaRange] = 1;
                ToBaseArea[(int)Units.Acre - AreaRange] = 100;
                ToBaseArea[(int)Units.Hectare - AreaRange] = 10000;
                ToBaseArea[(int)Units.SquareKilometer - AreaRange] = 1000000;
                ToBaseArea[(int)Units.SquareMile - AreaRange] = 64000;
            }


            public static double ConvertUnits(double amount, Units from, Units to)
            {
                if (from == to) return amount;

                ValidateUnitMatch(from, to);

                int range = GetConversionRange(from);

                double[] rateArray = GetConversionArray(range);

                double answer = 0;
                //Convert From to base,
                answer = rateArray[(int)from - range] * amount;

                //Convert base to To
                answer = answer * (1.0 / rateArray[(int)to - range]);

                return answer;


            }

            static void ValidateUnitMatch(Units from, Units to)
            {
                int frange = ((((int)from) / 100) * 100);
                int trange = ((((int)to) / 100) * 100);

                if (frange != trange)
                {
                    throw new ArgumentException("The from unit and to unit are not compatible. They need to be of the same type (Volume/Area/Mass/Length)");
                }
            }
            static int GetConversionRange(Units unit)
            {
                int range = ((((int)unit) / 100) * 100);
                return range;
            }

            static double[] GetConversionArray(int range)
            {
                switch (range)
                {
                    case VolumeRange: return ToBaseVolume;
                    case AreaRange: return ToBaseArea;
                    case LengthRange: return ToBaseLength;
                    case MassRange: return ToBaseWeight;
                }
                throw new ArgumentOutOfRangeException("Unknown unit type");
            }
        }
    }
}

