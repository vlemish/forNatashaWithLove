using System;
using System.Collections.Generic;
using System.Text;

namespace RationalNumberLib
{
    class RationalNumber
    {
        private int Numerator { get; set; }
        private int Denumerator { get; set; }
        private int Integer { get; set; }


        #region Constructors
        public RationalNumber() { }

        public RationalNumber(int numerator,int denumerator) //for right RationalNumbers
        {
            Numerator = numerator;
            Denumerator = denumerator;
        }

        public RationalNumber(int integer, int num, int denum):this(num,denum) //for mixed fraction
        {
            Integer = integer;
            if (Integer > 0)
            {
                Numerator = ToImproper(Integer, Numerator, Denumerator);
                Integer = 0;
            }

        }
        #endregion


        #region Overloading operators '+.../'

        #region +
        public static RationalNumber operator +(RationalNumber r1,RationalNumber r2)
        {
            int lcm = GetLCM(r1.Denumerator, r2.Denumerator);
            int af1 = AdditionalFactor(r1.Denumerator, lcm);
            int af2 = AdditionalFactor(r2.Denumerator, lcm);
            return new RationalNumber {Numerator=(r1.Numerator*af1)+(r2.Numerator*af2), Denumerator=lcm};
        }

        #endregion

        #region -

        public static RationalNumber operator -(RationalNumber r1,RationalNumber r2)
        {
            int lcm = GetLCM(r1.Denumerator, r2.Denumerator);
            int af1 = AdditionalFactor(r1.Denumerator, lcm);
            int af2 = AdditionalFactor(r2.Denumerator, lcm);
         
            return new RationalNumber { Numerator = (r1.Numerator * af1) - (r2.Numerator * af2), Denumerator = lcm };
        }

        #endregion;

        #region *
        public static RationalNumber operator *(RationalNumber r1,RationalNumber r2)
        {
            return new RationalNumber { Numerator = r1.Numerator * r2.Numerator, Denumerator = r1.Denumerator * r2.Denumerator };
        }

        #endregion

        #region /
        public static RationalNumber operator /(RationalNumber r1,RationalNumber r2)
        {
            return new RationalNumber { Numerator = r1.Numerator * r2.Denumerator, Denumerator = r2.Numerator * r1.Denumerator };
        }

        #endregion

        #region implicit overloading
        public static implicit operator RationalNumber(int x)
        {
            return new RationalNumber
            {
                Numerator = x,
                Denumerator = 1
            };
        }
        #endregion
        #endregion

        #region methods
        public static int GetLCM(int x, int y) //to find theLowestCommonMultiplier
        {

            return (x * y) / GetGCD(x, y);
        }

       public static int GetGCD(int x, int y) //to find theGretestCommonDivisor
        {
            while (x != 0 && y != 0)
            {
                if (x > y)
                {
                    x %= y;
                }
                else
                {
                    y %= x;
                }
            }
            return x + y;
        }

        

        public static int AdditionalFactor(int denum,int lcm) //to find AdditioanlFactor
        {
            return lcm / denum;
        }

        public static int ReduceFraction(int x,int y) // to reduce the fraction
        {
            int point;
            if (x > y)
            {
                point = x;
            }
            else
            {
                point = y;
            }
            for (int i = point; i > 1; i--)
            {
                if (x % i == 0 && y % i == 0)
                {
                    return i;
                }
            }
            return 1;
        }

        public static int ToImproper(int integer,int num,int denum)
        {
            return (integer * denum) + num;
        }


        private string HowToPrint()
        {
            int point = 0;
            if (Numerator > Denumerator)
            {
                Integer = Numerator / Denumerator;
                Numerator %= Denumerator;
                point = ReduceFraction(Numerator, Denumerator);
                Numerator /= point;
                Denumerator /= point;
                return Numerator==0?$"{Integer}":$"{Integer} {Numerator}\n  __\n  {Denumerator}"; //for Improper fractions
            }
            else
            {
                point = ReduceFraction(Numerator, Denumerator);
                Numerator /= point;
                Denumerator /= point;
                return Denumerator==1?$"{Numerator}":$"{Numerator}\n___\n{Denumerator}"; // this return just a number if IsInteger had returned true
            }
        }
        public override string ToString()
        {
            return HowToPrint();
        }

#endregion
    
    }

}
