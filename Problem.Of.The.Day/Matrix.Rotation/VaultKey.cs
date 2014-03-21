using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem.Of.The.Day.Matrix.Rotation
{
    /// <summary>
    /// Matrix Rotation
    /// The bank manager at my local bank recently gave me the algorithm to access the bank's vault. I thought I'd pass on the algorithm to you all for "safe keeping". Basically the vault has a USB port which you'll need to plug in to. Once plugged in the vault will send you an NxN matrix such as the one below.
    /// Monday-Friday the key to the vault is to rotate the matrix 90 degrees clockwise. On Saturday and Sunday you have to rotate the matrix 90 degrees counter-clockwise. My dog accidentally got locked in the vault and the bank manager is no where to be found. Can someone help me write a program to get him out?
    /// </summary>
    public sealed class VaultKey
    {
        public int[][] GetCode(int[][] input, DayOfWeek day)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (input.Any(i => i == null))
                throw new ArgumentNullException("input", "Can't pass an input with null row");
            if (input.Length == 0)
                throw new ArgumentOutOfRangeException("input", "Can't work with an empty array");
            if (!IsSquare(input))
                throw new ArgumentOutOfRangeException("input", "Input must be of N x N");

            var code = (int[][])input.Clone();
            if (code.Length == 1)
                return code;

            return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday ?
                TransformCounterClockwise(input) : TransformClockwise(input);
        }

        private bool IsSquare(int[][] input)
        {
            return input.All(row => row.Length == input.Length);
        }

        private int[][] TransformClockwise(int[][] input)
        {
            int n = input.Length;
            var code = new int[n][];

            for (int row = 0; row < n; row++)
            {
                code[row] = new int[n];

                for (int column = 0; column < n; column++)
                {
                    code[row][column] = input[n - 1 - column][row];
                }
            }

            return code;
        }

        private int[][] TransformCounterClockwise(int[][] input)
        {
            int n = input.Length;
            var code = new int[n][];

            for (int row = 0; row < n; row++)
            {
                code[row] = new int[n];
                
                for (int column = 0; column < n; column++)
                {
                    code[row][column] = input[column][n - 1 - row];
                }
            }

            return code;
        }
    }
}
