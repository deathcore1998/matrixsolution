using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix_app
{
    internal class Slau
    {
        int m;         // количество уравнений
        int n;         // количество переменных
        Matrix a;      // матрица коэффициентов
        Matrix b;      // вектор правой части
        Matrix x;      // вектор решений
        bool isSolved; // признак совместности
        int rang;      // ранг матрицы коэффициентов

        // конструктор
        public Slau(int m1, int n1)
        {
            m = m1;     // инициализация количества уравнений
            n = n1;     // инициализация количества переменных
            // выделение памяти под матрицу коэффициентов
            a = new Matrix(m1, n1);
            // выделение памяти под вектор свободных членов
            b = new Matrix(1, m1);
            // выделение памяти под вектор-решения
            x = new Matrix(1, n1);

            
        }

        // метод вывода системы уравнения и ее решения
        public void Print()
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write("" + a[i, j] + "\t");
                Console.WriteLine("\t" + b[0, i]);
            }
            try
            {
                Console.WriteLine("Решение СЛАУ: ");
                PrintSolution();
            }
            catch (Exception e)
            {
                // печать возможной ошибки
                Console.WriteLine(e.Message);
            }
        }

        // метод вывода полученного решения
        public string PrintSolution()
        {
            string res = "";
            if (!isSolved)
            {
                res = "Система несовместна";
                return res;
            }
            else
            {

                // получен единственный вектор решений
                res+="(";
                for (int i = 0; i < n - 1; i++)
                   res+=("" + x[0, i] + ", ");
                res+=("" + x[0, n - 1] + ")");
            }
            return res;
        }

        public void InverseMatrix()//метод обратной матрицы
        {
            // проверка, является ли матрица прямоугольной
            if (m != n)
                throw new Exception("Матрица не является квадратной");
            // вычисление обратной матрицы
            Matrix obr = ~a;
            Matrix B = !b;
            // получение решения СЛАУ
            x = obr * B;
            x = !x;
            rang = m;
            isSolved = true;
        }

        public void Kramer()// метод Крамера
        {
            // проверка, является ли матрица прямоугольной
            if (m != n)
                throw new Exception("Матрица не является квадратной");
            double det = a.Determinant(); // вычисление определителя
                                          // матрицы коэффициентов
                                          // проверка определенности системы
            if (det == 0)
                throw new Exception("Деление на 0");

            rang = m;
            // вычисление корней по формулам Крамера
            Matrix temp = a.Copy();
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                    temp[i, j] = b[0, i];
                x[0, j] = temp.Determinant() / det;
                for (int i = 0; i < n; i++)
                    temp[i, j] = a[i, j];
            }
            isSolved = true;
        }

        public void Input(Matrix mat1,Matrix mat2)
        {
            a = mat1.Copy();
            b = mat2.Copy();
        }
    }
}
