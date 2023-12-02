using System;
using System.Text;

namespace ConsoleApplication6
{
    class GaussianJordanElimination
    {
        private double[,] a;
        private int rows;
        private int cols;

        public GaussianJordanElimination()
        {
        }

        public void SetArray(double[,] arr)
        {
            this.a = arr;
            this.rows = arr.GetLength(0);
            this.cols = arr.GetLength(1);
        }

        public void SwapRows(ref double[,] arr, int row)
        {
            for (int col = 0; col < cols; col++)
            {
                if (row + 1 < rows)
                {
                    double temp = arr[row, col];
                    arr[row, col] = arr[row + 1, col];
                    arr[row + 1, col] = temp;
                }
            }
        }

        public string MatToString(double[,] mat)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    sb.Append(mat[i, j] + "  ");
                }
                sb.Append("\n");
            }
            sb.Append("\n");
            return sb.ToString();
        }

        public string Solve()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Displaying input Matrix \n");
            sb.Append(MatToString(a));

            bool CheckNoSolution()
            {
                int count;
                for (int i = 0; i < rows; i++)
                {
                    count = 0;
                    for (int j = 0; j < cols; j++)
                    {
                        if (a[i, j] == 0)
                        {
                            count++;
                            if (count == cols && j + 1 < cols)
                            {
                                if (a[i, j + 1] != 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }

            bool CheckInfiniteSolutions()
            {
                int count;
                for (int i = 0; i < rows; i++)
                {
                    count = 0;
                    for (int j = 0; j < cols; j++)
                    {
                        if (a[i, j] == 0)
                        {
                            count++;
                            if (count == cols + 1)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }

            if (CheckInfiniteSolutions())
            {
                sb.Append("The system has an infinite set of solutions.\n");
                return sb.ToString();
            }


            for (int i = 0; i < rows; i++)
            {
                if (a[i, i] == 0)
                {
                    SwapRows(ref a, i);

                    sb.Append($"Interchange R{i + 1} <-> R{i + 2}\n");

                    break;
                }

                if (CheckNoSolution())
                {
                    sb.Append("The system has no solution.\n");
                    return sb.ToString();
                }


                double temp = a[i, i];

                for (int j = 0; j < cols; j++)
                {
                    if (temp != 0)
                        a[i, j] = a[i, j] / temp;
                }

                sb.Append($"1/{temp} * R{i + 1} --> R{i + 1}");
                sb.Append(MatToString(a));

                for (int k = 0; k < rows; k++)
                {
                    if (i != k)
                    {
                        temp = a[k, i];
                        for (int j = 0; j < cols; j++)
                        {
                            a[k, j] = a[k, j] - (temp * a[i, j]);
                        }

                        if (temp != 0)
                        {
                            if (temp == 1)
                            {
                                sb.Append($"R{k + 1} - R{i + 1} --> R{k + 1}\n");
                                sb.Append(MatToString(a));
                            }
                            else
                            {
                                sb.Append($"R{k + 1} * {temp} - R{i + 1} --> R{k + 1}\n");
                                sb.Append(MatToString(a));

                            }
                        }
                    }
                }
                sb.Append("Solved matrix \n");
                sb.Append(MatToString(a));

                int numVariables = cols - 1;

                for (int m = 0; m < rows; m++)
                {
                    sb.Append($"x{m + 1} = {a[m,cols-1]}\n");
                }
            }
            return sb.ToString();
        }
       
     
    }
}
