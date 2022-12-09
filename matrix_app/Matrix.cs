using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix_app
{
    class Matrix
    {

        // массив элементов матрицы
        public double[,] a;
        // конструктор матрицы с указанием ее размеров
        public Matrix(int rows, int cols)
        {
            a = new double[rows, cols];
        }
        // метод заполнения матрицы случайными числами
        public void InputMatrix()
        {
            int rows = Rows, cols = Cols;
            Random r = new Random();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    a[i, j] = (double)r.Next(10);
        }

        public void SelfInputMatrix()// метод ввода матрицы с консоли
        {
            int rows = Rows, cols = Cols;
            Console.WriteLine("Введите Матрицу:");
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    Console.Write("a[{0},{1}] = ", i + 1, j + 1);
                    a[i, j] = double.Parse(Console.ReadLine());
                    Console.WriteLine();
                }
        }

        // метод вывода матрицы
        public void OutputMatrix()
        {
            int rows = Rows, cols = Cols;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    Console.Write("{0, 4:f2} ", a[i, j]);
                Console.WriteLine();
            }
        }

        // свойство получения количества строк матрицы
        public int Rows
        {
            get { return a.GetLength(0); }
        }
        // свойство получения количества столбцов матрицы
        public int Cols
        {
            get { return a.GetLength(1); }
        }

        // индексатор для доступа к элементам матрицы
        public double this[int i, int j]
        {
            get
            {
                int rows = Rows, cols = Cols;
                // проверяется корректность индексов
                if (i < 0 || i >= rows || j < 0 || j >= cols)
                    throw new Exception("Индексы выходят из диапазона");
                return a[i, j];
            }
            set
            {
                int rows = Rows, cols = Cols;
                // проверяется корректность индексов
                if (i < 0 || i >= rows || j < 0 || j >= cols)
                    throw new Exception("Индексы выходят из диапазона");
                a[i, j] = value;
            }
        }

        // операция сложения двух матриц
        static public Matrix operator +(Matrix m1, Matrix m2)
        {
            int rows1 = m1.Rows, cols1 = m1.Cols,
           rows2 = m2.Rows, cols2 = m2.Cols;
            // размеры матриц должны совпадать
            if (rows1 != rows2 || cols1 != cols2)
                throw new Exception("Матрицы таких размеров складывать нельзя");

            Matrix newM = new Matrix(rows1, cols1);
            for (int i = 0; i < rows1; i++)
                for (int j = 0; j < cols1; j++)
                    newM[j, i] = m1[i, j] + m2[i, j];
            return newM;
        }

        // метод, перегружающий операцию траспонирование матрицы
        static public Matrix operator !(Matrix m)
        {
            int rows = m.Rows, cols = m.Cols;
            Matrix newM = new Matrix(cols, rows);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    newM[j, i] = m[i, j];
            return newM;
        }
        // операция перемножения двух матриц
        static public Matrix operator *(Matrix m1, Matrix m2)
        {
            int rows1 = m1.Rows, cols1 = m1.Cols,
            rows2 = m2.Rows, cols2 = m2.Cols;
            if (cols1 == rows2)
            {
                // создание матрицы-результата
                Matrix newM = new Matrix(rows1, cols2);
                // заполнение матрицы-результата
                for (int i = 0; i < rows1; i++)
                    for (int j = 0; j < cols2; j++)
                        for (int k = 0; k < cols1; k++)
                            newM[i, j] += m1[i, k] * m2[k, j];
                return newM;
            }
            else
            {
                // количество столбцов первой матрицы должно
                // быть равно количеству строк второй матрицы
                throw new Exception("Матрицы таких размеров перемножать нельзя");
            }
        }
        // операция перемножения матрицы на число
        static public Matrix operator *(Matrix m, double d)
        {
            // создание матрицы-результата
            int rows = m.Rows, cols = m.Cols;
            Matrix newM = new Matrix(rows, cols);
            // заполнение матрицы-результата
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    newM[i, j] = m[i, j] * d;
            return newM;
        }
        // операция перемножения числа на матрицу
        static public Matrix operator *(double d, Matrix m)
        {
            int rows = m.Rows, cols = m.Cols;
            // создание матрицы-результата
            Matrix newM = new Matrix(rows, cols);
            // заполнение матрицы-результата
            newM = m * d;
            return newM;
        }

        Matrix SubMatrix(int i1, int j1)
        {
            int rows = Rows;
            // матрица-результат имеет порядок
            // на 1 меньше исходной
            Matrix temp = new Matrix(rows - 1, rows - 1);
            // формируем новую матрицу, игнорируя
            // строку с номером i1 и столбец с номером j1
            for (int i = 0; i < i1; i++)
            {
                for (int j = 0; j < j1; j++)
                    temp[i, j] = a[i, j];
                for (int j = j1 + 1; j < rows; j++)
                    temp[i, j - 1] = a[i, j];
            }
            for (int i = i1 + 1; i < rows; i++)
            {
                for (int j = 0; j < j1; j++)
                    temp[i - 1, j] = a[i, j];
                for (int j = j1 + 1; j < rows; j++)
                    temp[i - 1, j - 1] = a[i, j];
            }
            return temp;
        }
        public double Determinant()
        {
            double det = 0;
            int rows = Rows;
            // определитель 1-ого порядка совпадает
            // с единственным элементом матрицы
            if (rows == 1)
                return a[0, 0];
            Matrix temp = new Matrix(rows - 1, rows - 1);
            // раскладываем определитель по 0-ой строке
            for (int j = 0; j < rows; j++)
            {
                // получаем матрицу для вычисления
                // минора элемента a0j
                temp = SubMatrix(0, j);
                // добавляем очередное произведение элемента
                // на его алгебраическое дополнение
                if (j % 2 == 0)
                    det += temp.Determinant() * a[0, j];
                else
                    det -= temp.Determinant() * a[0, j];
            }
            return det;
        }

        public Matrix Copy()//метод копирования матрицы
        {
            Matrix a1 = new Matrix(this.Rows, this.Cols);
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                    a1[i, j] = a[i, j];
            }
            return a1;
        }

        public static Matrix operator ~(Matrix m)
        {
            int rows = m.Rows;
            Matrix res = new Matrix(rows, rows);
            // вычисление определителя матрицы
            double det = m.Determinant();
            // если матрица вырожденная,
            // обратной матрицы не существует
            if (det == 0)
                throw new Exception("Матрица вырожденная");
            // вычисление транспонированной матрицы
            // алгебраических дополнений
            Matrix temp = new Matrix(rows - 1, rows - 1);
            int z;
            for (int i = 0; i < rows; i++)
            {
                z = i % 2 == 0 ? 1 : -1;
                for (int j = 0; j < rows; j++)
                {
                    temp = m.SubMatrix(i, j);
                    res[j, i] = z * temp.Determinant() / det;
                    z = -z;
                }
            }
            return res;
        }

    }
}
