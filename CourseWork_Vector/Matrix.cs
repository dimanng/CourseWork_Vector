using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_Vector
{
    class Matrix
    {
        private int n;
        private int m;
        private double[,] matrix;
        public Matrix(int n, int m)
        {
            matrix = new double[n, m];          
        }
        public Matrix(Matrix copyMatrix)
        {
            matrix = new double[copyMatrix.matrix.GetLength(0), copyMatrix.matrix.GetLength(1)];
            for (int i = 0; i < copyMatrix.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < copyMatrix.matrix.GetLength(1); j++)
                {
                    this.matrix[i, j] = copyMatrix.matrix[i, j];
                }
            }
        }
        public Matrix(double[,] matrix) // переделать на копирование массивов - см. конструктор 1
        {
            this.matrix = matrix;
        }
        public Matrix(Vector[] vectors)
        {
            for (int i = 0; i < vectors.Length; i++)
            {
                Vector v = vectors[i];
                for (int j = 0; j < v.GetSize(); j++)
                {
                    matrix[i, j] = v.GetComponent(j);
                }
            }
        }
        public int[] GetSize()
        {
            return new int[] { matrix.GetLength(0), matrix.GetLength(1) };
        }
        public Vector GetVector(int index)
        {
            Vector vector = new Vector(matrix.GetLength(1));
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                vector.SetComponent(matrix[index, i], i);
            }
            return vector;
        }
        public void SetVector(int index, Vector vector)
        {
            for (int i = 0; i < vector.GetSize(); i++)
            {
                matrix[index, i] = vector.GetComponent(i);
            
            }
        }
        public Vector GetVectorColumn(int index)
        {
            Vector vector = new Vector(matrix.GetLength(0));
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                vector.SetComponent(matrix[i, index], i);
            }
            return vector;
        }
        public void TransposeMatrix()
        {
            Matrix subMatrix = new Matrix(this);
            for (int i = 0; i < subMatrix.matrix.GetLength(0); i++)
            {
               SetVector(i, subMatrix.GetVectorColumn(i));
            }
        }
        public void ShowMatrix()
        {
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        Console.Write(matrix[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
        public void MultiplicationOnScalar(double scalar)
        {
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = matrix[i, j] * scalar;
                    }
                    Console.WriteLine();
                }
            }
        }
        public Matrix CutColumnMatrix(int indexColumn)
        {
            var resultMatrix = new Matrix(matrix.GetLength(0), matrix.GetLength(1) - 1);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    if (j < indexColumn)
                    {
                        resultMatrix.matrix[i, j] = matrix[i, j];
                    }
                    else
                    {
                        resultMatrix.matrix[i, j] = matrix[i, j + 1];
                    }
                    
                }
                
            }

            return resultMatrix;
        }
        public Matrix CutRowMatrix(int indexRow)
        {
            var resultMatrix = new Matrix(matrix.GetLength(0) - 1, matrix.GetLength(1));

            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i < indexRow)
                    {
                        resultMatrix.matrix[i, j] = matrix[i, j];
                    }
                    else
                    {
                        resultMatrix.matrix[i, j] = matrix[i + 1, j];
                    }
                }
            }

            return resultMatrix;
        }
        public double GetDeterminant()
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new ArgumentException("Определитель возможно вычислить только у квадратной матрицы!");
            }
            if (matrix.GetLength(0) == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            double result = 0;
            for(int j = 0; j < matrix.GetLength(1); j++)
            {
                result = result + matrix[1, j] * Math.Pow(-1, 1 + j) * CutColumnMatrix(j).CutRowMatrix(1).GetDeterminant();
                Console.WriteLine(matrix.GetLength(0));
                Console.WriteLine(matrix.GetLength(1));

            }
            
            return result;
            
        }
        public override string ToString()
        {
            string[] arr = new string[matrix.GetLength(0)]; 
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                arr[i] = GetVector(i).ToString();
            }
            return $"{{ {string.Join(", ", arr)} }}"; ;
        }
        public Vector MultiplicationOnVector(Vector vector)
        {
            if (matrix.GetLength(1) != vector.GetSize())
            {
                throw new ArgumentException("Кол-во строк матрицы должно совпадать с кол-вом столбцов вектора");
            }
            
            double[] resultArray = new double[vector.GetSize()];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    resultArray[i] = resultArray[i] + matrix[i, j] * vector.GetComponent(i);  
                }
            }
            Vector resultVector = new Vector(resultArray);
            return resultVector;
        }
        public void AdditionMatrix(Matrix addsMatrix)
        {
            if (matrix.GetLength(0) != addsMatrix.matrix.GetLength(0) || matrix.GetLength(1) != addsMatrix.matrix.GetLength(1))
            {
                throw new ArgumentException("Размеры матриц не совпадают");
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = matrix[i, j] + addsMatrix.matrix[i, j];
                }
            }
        }
        public void SubstractMatrix(Matrix subsMatrix)
        {
            if (matrix.GetLength(0) != subsMatrix.matrix.GetLength(0) || matrix.GetLength(1) != subsMatrix.matrix.GetLength(1))
            {
                throw new ArgumentException("Размеры матриц не совпадают");
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = matrix[i, j] - subsMatrix.matrix[i, j];
                }
            }
        }
        public static Matrix MatrixAddMatrix(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.matrix.GetLength(0) != matrix2.matrix.GetLength(0) || matrix1.matrix.GetLength(1) != matrix2.matrix.GetLength(1))
            {
                throw new ArgumentException("Размеры матриц не совпадают");
            }
            Matrix resultMatrix = new Matrix(matrix1.matrix.GetLength(0), matrix1.matrix.GetLength(1));
            for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.matrix.GetLength(1); j++)
                {
                    resultMatrix.matrix[i, j] = matrix1.matrix[i, j] + matrix2.matrix[i, j];
                }
            }
            return resultMatrix;
        }
        public static Matrix MatrixDelMatrix(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.matrix.GetLength(0) != matrix2.matrix.GetLength(0) || matrix1.matrix.GetLength(1) != matrix2.matrix.GetLength(1))
            {
                throw new ArgumentException("Размеры матриц не совпадают");
            }
            Matrix resultMatrix = new Matrix(matrix1.matrix.GetLength(0), matrix1.matrix.GetLength(1));
            for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.matrix.GetLength(1); j++)
                {
                    resultMatrix.matrix[i, j] = matrix1.matrix[i, j] - matrix2.matrix[i, j];
                }
            }
            return resultMatrix;
        }
        public static Matrix MatrixToMatrix(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.matrix.GetLength(1) != matrix2.matrix.GetLength(0))
            {
                throw new ArgumentException("Кол-во столбцов первой матрицы не совпадают с кол-вом строк второй");
            }
            Matrix resultMatrix = new Matrix(matrix1.matrix.GetLength(0), matrix2.matrix.GetLength(1));
            for (int i = 0; i < matrix1.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.matrix.GetLength(1); j++)
                {
                    for (int k = 0; k < matrix1.matrix.GetLength(1); k++)
                    {
                        resultMatrix.matrix[i, j] = resultMatrix.matrix[i, j] + matrix1.matrix[i, k] * matrix2.matrix[k, j];
                    }
                }
            }
            return resultMatrix;
        }

    }
}
