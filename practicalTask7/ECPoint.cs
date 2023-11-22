namespace practicalTask7;
using System.Numerics;
public class ECPoint
{
    public BigInteger X { get; set; }
    public BigInteger Y { get; set; }
    public static BigInteger x { get; set; } //потрібно буде знайти ек для прикладу
    public static BigInteger y { get; set; }
    public static BigInteger a { get; set; }
    public static BigInteger b { get; set; }
    public static BigInteger p { get; set; }


    public static ECPoint BasePointGGet(BigInteger x, BigInteger y)
    {
        return new ECPoint { X = x, Y = y };
    }

    public static ECPoint ECPointGen(BigInteger x, BigInteger y)
    {
        return new ECPoint { X = x, Y = y };
    }

    public static bool IsOnCurveCheck(ECPoint ECurve_point)
    {

        BigInteger lhs = BigInteger.ModPow(ECurve_point.Y, 2, p);

        BigInteger rhs = (BigInteger.ModPow(ECurve_point.X, 3, p) + a * ECurve_point.X + b) % p;

        return lhs == rhs;
    }


    public static ECPoint AddECPoints(ECPoint a, ECPoint b)
    {
        BigInteger p = new BigInteger();

        BigInteger slope = (b.Y - a.Y) * BigInteger.ModPow(b.X - a.X, p - 2, p) % p;

        BigInteger x = (slope * slope - a.X - b.X) % p;
        BigInteger y = (slope * (a.X - x) - a.Y) % p;

        return new ECPoint { X = x, Y = y };
    }


    public static ECPoint DoubleECPoints(ECPoint ECurve_point)
    {

        BigInteger slope = (3 * ECurve_point.X * ECurve_point.X + a) * BigInteger.ModPow(2 * ECurve_point.Y, p - 2, p) % p;

        BigInteger x = (slope * slope - 2 * ECurve_point.X) % p;
        BigInteger y = (slope * (ECurve_point.X - x) - ECurve_point.Y) % p;

        return new ECPoint { X = x, Y = y };
    }


    public static ECPoint ScalarMult(BigInteger k, ECPoint ECurve_point)
    {
        ECPoint result = null;

        while (k > 0)
        {
            if ((k & 1) == 1)
            {
                if (result == null)
                {
                    result = ECurve_point;
                }
                else
                {
                    result = AddECPoints(result, ECurve_point);
                }
            }

            ECurve_point = DoubleECPoints(ECurve_point);
            k >>= 1;
        }

        return result;
    }


    // public static string ECPointToString(ECPoint ECurve_point) {}

    // public static ECPoint StringToECPoint(string s) {}
    
    // public static void PrintECPoint(ECPoint ECurve_point) {}
}