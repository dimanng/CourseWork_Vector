using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_Vector
{
    public class Vector
    {
        private double[] vector;
        private int n;

        public Vector(int n)
        {
            try
            {
                //this.n = n;
                vector = new double[n];
                if (n <= 0)
                {
                    throw new ArgumentException("Указана недопустимая размерность вектора");
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
            
        }
        public Vector(Vector copyVector) //!!!
        {
            vector = copyVector.vector;
        }
        public Vector(double[] vector)
        {
            this.vector = vector;
        }
        public Vector(int n, double[] vector)
        {
            try
            {
                this.vector = new double[n];
                for (int i = 0; i < vector.Length; i++)
                {
                    this.vector[i] = vector[i];
                }
                if (n <= 0)
                {
                    throw new ArgumentException("Указана недопустимая размерность вектора");
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
            
        }
        public override string ToString()
        {
            return $"{{ {string.Join(", ", vector)} }}";
        }
        public int GetSize()
        {
            return vector.Length;
        }
        public void AddVector(Vector addVector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = vector[i] + addVector.vector[i];
            }
        }
        public void RemoveVector(Vector addVector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = vector[i] - addVector.vector[i];
            }
        }
        public void MultiplicationVector(int n)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = vector[i] * n;
            }
        }
        public void ReverseVector()
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = vector[i] * -1;
            }
        }
        public double GetLength()
        {
            double sum = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum = sum + Math.Pow(vector[i], 2);
            }
            return Math.Sqrt(sum);
        }
        public double GetComponent(int index)
        {
            return vector[index];
        }
        public void SetComponent(double number, int index)
        {
            vector[index] = number;
        }
        public double[] GetVector()
        {
            return vector;
        }
        public void SetVector(double[] vector)
        {
            this.vector = vector;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            Vector v = (Vector)obj;

            bool componentEquals = true;

            if (vector.Length == v.vector.Length)
            {
                for (int i = 0; i < vector.Length; i++)
                {
                    if (vector[i] != v.vector[i])
                    {
                        componentEquals = false;
                    }
                }
                return componentEquals;
            }
            else
            {
                return false;
            }
            
        }
        public override int GetHashCode()
        {
            const int prime = 14;
            int hash = 1;
            for (int i = 0; i < vector.Length; i++)
            {
                hash = prime * hash + vector[i].GetHashCode();
            }
            return hash;
        }
        public static Vector AddsVector(Vector vc1, Vector vc2)
        {
            int lengthNew;
            if (vc1.vector.Length >= vc2.vector.Length)
            {
                lengthNew = vc1.vector.Length;
            }
            else
            {
                lengthNew = vc2.vector.Length;
            }
            double[] array = new double[lengthNew];
            for (int i = 0; i < lengthNew; i++)
            {
                array[i] = vc1.vector[i] + vc2.vector[i];
            }
            Vector vector = new Vector(array);
            return vector;
        }
        public static Vector RemsVector(Vector vc1, Vector vc2)
        {
            int lengthNew;
            if (vc1.vector.Length >= vc2.vector.Length)
            {
                lengthNew = vc1.vector.Length;
            }
            else
            {
                lengthNew = vc2.vector.Length;
            }
            double[] array = new double[lengthNew];
            for (int i = 0; i < lengthNew; i++)
            {
                array[i] = vc1.vector[i] - vc2.vector[i];
            }
            Vector vector = new Vector(array);
            return vector;
        }
        public static Double ScalarMultiplication(Vector vc1, Vector vc2)
        {
            int lengthNew;
            double sum = 0;
            if (vc1.vector.Length >= vc2.vector.Length)
            {
                lengthNew = vc1.vector.Length;
            }
            else
            {
                lengthNew = vc2.vector.Length;
            }
            for (int i = 0; i < lengthNew; i++)
            {
                sum = sum + vc1.vector[i] * vc2.vector[i];
            }
            return sum;
            
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Vector vc1 = new Vector(new double[] { 1, 2, 8 });
            Vector vc2 = new Vector(new double[] { -2, 5, 0 });
            Vector vc3 = new Vector(new double[] { 2, 5, 4 });

            Vector vc4 = new Vector(new double[] { 1, 1, 1 });
            Vector vc5 = new Vector(new double[] { 1, 1, 1 });
            Vector vc6 = new Vector(new double[] { 1, 1, 1 });



            Matrix m1 = new Matrix(3, 3);
             m1.SetVector(0, vc1);
             m1.SetVector(1, vc2);
             m1.SetVector(2, vc3);
             m1.ShowMatrix();
             Console.WriteLine();
            /*
            Matrix m2 = new Matrix(3, 3);
            m2.SetVector(0, vc4);
            m2.SetVector(1, vc5);
            m2.SetVector(2, vc6);
            m2.ShowMatrix();

            Console.WriteLine();
            */

            m1.TransposeMatrix();
            m1.ShowMatrix();

            Console.ReadKey();
        }
    }
}
