using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace matrix_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void MatrixMathStart()
        {
            try
            {
                Matrix a = new Matrix(3, 3);
                a[0, 0] = (double)numericUpDown1.Value;
                a[0, 1] = (double)numericUpDown4.Value;
                a[0, 2] = (double)numericUpDown7.Value;
                a[1, 0] = (double)numericUpDown2.Value;
                a[1, 1] = (double)numericUpDown5.Value;
                a[1, 2] = (double)numericUpDown8.Value;
                a[2, 0] = (double)numericUpDown3.Value;
                a[2, 1] = (double)numericUpDown6.Value;
                a[2, 2] = (double)numericUpDown9.Value;

                Matrix b = new Matrix(1, 3);
                b[0,0] = (double)numericUpDown10.Value;
                b[0, 1] = (double)numericUpDown11.Value;
                b[0, 2] = (double)numericUpDown12.Value;

                Slau res = new Slau(3, 3);
                res.Input(a, b);

                res.Kramer();
                textBox1.Text = res.PrintSolution();
                res.InverseMatrix();
                textBox2.Text = res.PrintSolution();
            }
            catch
            {
                MessageBox.Show("Данную матрицу не удалось решить указанными методами..." + Environment.NewLine + "Проверьте правильность введенных данных.", "Ошибка при вычислении");
                return;
            }
        }

        private void вычислитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatrixMathStart();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
