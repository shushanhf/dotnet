// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Xunit;

namespace System.Numerics.Tests
{
    public class multiplyTest
    {
        private static int s_samples = 10;
        private static int s_seed = 100;

        [Fact]
        public static void RunMultiply_TwoLargeBigIntegers()
        {
            Random random = new Random(s_seed);
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Multiply Method - One Large BigInteger
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(random);
                VerifyMultiplyString(Print(tempByteArray1) + "uMultiply");
            }

            // Multiply Method - Two Large BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(random);
                tempByteArray2 = GetRandomByteArray(random);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "bMultiply");
            }
        }

        [Fact]
        public static void RunMultiply_TwoSmallBigIntegers()
        {
            Random random = new Random(s_seed);
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Multiply Method - Two Small BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(random, 2);
                tempByteArray2 = GetRandomByteArray(random, 2);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "bMultiply");
            }
        }

        [Fact]
        public static void RunMultiply_OneLargeOneSmall()
        {
            Random random = new Random(s_seed);
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Multiply Method - One large and one small BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                try
                {
                    tempByteArray1 = GetRandomByteArray(random);
                    tempByteArray2 = GetRandomByteArray(random, 2);
                    VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "bMultiply");

                    tempByteArray1 = GetRandomByteArray(random, 2);
                    tempByteArray2 = GetRandomByteArray(random);
                    VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "bMultiply");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Array1: " + Print(tempByteArray1));
                    Console.WriteLine("Array2: " + Print(tempByteArray2));
                    throw;
                }
            }
        }

        [Fact]
        public static void RunMultiply_OneLargeOneZero()
        {
            Random random = new Random(s_seed);
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Multiply Method - One large BigIntegers and zero
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(random);
                tempByteArray2 = new byte[] { 0 };
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "bMultiply");

                tempByteArray1 = new byte[] { 0 };
                tempByteArray2 = GetRandomByteArray(random);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "bMultiply");
            }
        }

        [Fact]
        public static void RunMultiply_OneSmallOneZero()
        {
            Random random = new Random(s_seed);
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Multiply Method - One small BigIntegers and zero
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(random, 2);
                tempByteArray2 = new byte[] { 0 };
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "bMultiply");

                tempByteArray1 = new byte[] { 0 };
                tempByteArray2 = GetRandomByteArray(random, 2);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "bMultiply");
            }
        }

        [Fact]
        public static void RunMultiply_AxiomXX1()
        {
            Random random = new Random(s_seed);
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Axiom: X*1 = X
            VerifyIdentityString(int.MaxValue + " " + BigInteger.One + " bMultiply", Int32.MaxValue.ToString());
            VerifyIdentityString(long.MaxValue + " " + BigInteger.One + " bMultiply", Int64.MaxValue.ToString());

            for (int i = 0; i < s_samples; i++)
            {
                string randBigInt = Print(GetRandomByteArray(random));
                VerifyIdentityString(randBigInt + BigInteger.One + " bMultiply", randBigInt + "u+");
            }
        }

        [Fact]
        public static void RunMultiply_AxiomXX0()
        {
            Random random = new Random(s_seed);
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Axiom: X*0 = 0
            VerifyIdentityString(int.MaxValue + " " + BigInteger.Zero + " bMultiply", BigInteger.Zero.ToString());
            VerifyIdentityString(long.MaxValue + " " + BigInteger.Zero + " bMultiply", BigInteger.Zero.ToString());

            for (int i = 0; i < s_samples; i++)
            {
                string randBigInt = Print(GetRandomByteArray(random));
                VerifyIdentityString(randBigInt + BigInteger.Zero + " bMultiply", BigInteger.Zero.ToString());
            }
        }

        [Fact]
        public static void RunMultiply_Commutat()
        {
            Random random = new Random(s_seed);
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Axiom: a*b = b*a
            VerifyIdentityString(int.MaxValue + " " + long.MaxValue + " bMultiply", long.MaxValue + " " + int.MaxValue + " bMultiply");

            for (int i = 0; i < s_samples; i++)
            {
                string randBigInt1 = Print(GetRandomByteArray(random));
                string randBigInt2 = Print(GetRandomByteArray(random));
                VerifyIdentityString(randBigInt1 + randBigInt2 + "bMultiply", randBigInt2 + randBigInt1 + "bMultiply");
            }
        }

        [Fact]
        public static void RunMultiply_Boundary()
        {
            Random random = new Random(s_seed);
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Check interesting cases for boundary conditions
            // You'll either be shifting a 0 or 1 across the boundary
            // 32 bit boundary  n2=0
            VerifyMultiplyString(Math.Pow(2, 32) + " 2 bMultiply");

            // 32 bit boundary  n1=0 n2=1
            VerifyMultiplyString(Math.Pow(2, 33) + " 2 bMultiply");
        }

        [Fact]
        public static void RunMultiplyKaratsubaBoundary()
        {
            Random random = new Random(s_seed);
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Multiply Method - One Large BigInteger
            for (int i = 0; i < s_samples; i++)
            {
                for (int d1 = -2; d1 <= 2; d1++)
                {
                    tempByteArray1 = GetRandomByteArray(random, BigIntegerCalculator.MultiplyKaratsubaThreshold + d1);
                    for (int d2 = -4; d2 <= 4; d2++)
                    {
                        tempByteArray2 = GetRandomByteArray(random, (BigIntegerCalculator.MultiplyKaratsubaThreshold + 1) * 2 + d2);
                        VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "bMultiply");
                    }
                }
            }
        }

        [Fact]
        public static void RunMultiply_OnePositiveOneNegative()
        {
            Random random = new Random(s_seed);
            byte[] tempByteArray1 = new byte[0];

            // Multiply Method - One positive and one negative BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                try
                {
                    tempByteArray1 = GetRandomByteArray(random);
                    string randBigInt = Print(tempByteArray1);
                    VerifyMultiplyString(randBigInt + randBigInt + "uNegate bMultiply");

                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Array1: " + Print(tempByteArray1));
                    throw;
                }
            }
        }

        private static void VerifyMultiplyString(string opstring)
        {
            StackCalc sc = new StackCalc(opstring);
            while (sc.DoNextOperation())
            {
                Assert.Equal(sc.snCalc.Peek().ToString(), sc.myCalc.Peek().ToString());
            }
        }

        private static void VerifyIdentityString(string opstring1, string opstring2)
        {
            StackCalc sc1 = new StackCalc(opstring1);
            while (sc1.DoNextOperation())
            {
                //Run the full calculation
                sc1.DoNextOperation();
            }

            StackCalc sc2 = new StackCalc(opstring2);
            while (sc2.DoNextOperation())
            {
                //Run the full calculation
                sc2.DoNextOperation();
            }

            Assert.Equal(sc1.snCalc.Peek().ToString(), sc2.snCalc.Peek().ToString());
        }

        private static byte[] GetRandomByteArray(Random random)
        {
            return GetRandomByteArray(random, random.Next(0, 100));
        }

        private static byte[] GetRandomByteArray(Random random, int size)
        {
            return MyBigIntImp.GetRandomByteArray(random, size);
        }

        private static string Print(byte[] bytes)
        {
            return MyBigIntImp.Print(bytes);
        }
    }

    [Collection(nameof(DisableParallelization))]
    public class multiplyTestThreshold
    {
        [Fact]
        public static void RunMultiply_TwoLargeBigIntegers()
        {
            // Again, with lower threshold
            BigIntTools.Utils.RunWithFakeThreshold(BigIntegerCalculator.SquareThreshold, 8, () =>
                BigIntTools.Utils.RunWithFakeThreshold(BigIntegerCalculator.MultiplyKaratsubaThreshold, 8, multiplyTest.RunMultiply_TwoLargeBigIntegers)
            );

            // Again, with lower threshold
            BigIntTools.Utils.RunWithFakeThreshold(BigIntegerCalculator.SquareThreshold, 8, () =>
                BigIntTools.Utils.RunWithFakeThreshold(BigIntegerCalculator.MultiplyKaratsubaThreshold, 8, () =>
                    BigIntTools.Utils.RunWithFakeThreshold(BigIntegerCalculator.StackAllocThreshold, 8, multiplyTest.RunMultiply_TwoLargeBigIntegers)
                )
            );
        }
    }
}
