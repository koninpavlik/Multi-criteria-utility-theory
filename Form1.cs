using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mapack;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            fillGrid();
        }

        private void fillGrid()
        {
            for (int i = 0; i < numericUpDownElements.Value; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), i.ToString());
                dataGridView1.Columns[i].DefaultCellStyle.NullValue = "0";
                dataGridView4.Columns.Add(i.ToString(), i.ToString());
                dataGridView4.Columns[i].DefaultCellStyle.NullValue = "0";
            }
            for (int i = 0; i < numericUpDownCriteries.Value; i++)
            {
                dataGridView3.Columns.Add(i.ToString(), i.ToString());
                dataGridView3.Columns[i].DefaultCellStyle.NullValue = "0";
            }
            for (int i = 0; i < numericUpDownCriteries.Value; i++)
            {
                dataGridView1.Rows.Add();
            }

            dataGridView3.Rows.Add();
            for (int i = 0; i < numericUpDownCriteries.Value; i++)
            {
                dataGridView4.Rows.Add();
            }
        }

        private void numericUpDownCriteries_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(((UpDownBase)sender).Text) < numericUpDownCriteries.Value)
            {
                dataGridView1.Rows.Add();
                dataGridView3.Columns.Add((dataGridView3.Columns.Count).ToString(), (dataGridView3.Columns.Count).ToString());
                dataGridView3.Columns[dataGridView3.Columns.Count - 1].DefaultCellStyle.NullValue = "0";
                dataGridView4.Rows.Add();
            }
            else
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                dataGridView3.Columns.Remove((Convert.ToInt32(((UpDownBase)sender).Text) - 1).ToString());
                dataGridView4.Rows.RemoveAt(dataGridView4.Rows.Count - 1);
            }
        }

        private void numericUpDownElements_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(((UpDownBase)sender).Text) < numericUpDownElements.Value)
            {
                dataGridView1.Columns.Add((dataGridView1.Columns.Count).ToString(), (dataGridView1.Columns.Count).ToString());
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].DefaultCellStyle.NullValue = "0";
                dataGridView4.Columns.Add((dataGridView4.Columns.Count).ToString(), (dataGridView4.Columns.Count).ToString());
                dataGridView4.Columns[dataGridView4.Columns.Count - 1].DefaultCellStyle.NullValue = "0";

            }
            else
            {
                dataGridView1.Columns.Remove((Convert.ToInt32(((UpDownBase)sender).Text) - 1).ToString());
                dataGridView4.Columns.Remove((Convert.ToInt32(((UpDownBase)sender).Text) - 1).ToString());
            }
        }

        private static List<List<double>> dgwTo2dListOfDouble(DataGridView dataGridView)
        {
            List<List<double>> list = new List<List<double>>();
            for (int i2 = 0; i2 < dataGridView.Rows.Count; i2++)
            {
                list.Add(new List<double>());
                for (int j2 = 0; j2 < dataGridView.Columns.Count; j2++)
                {
                    list[i2].Add(Convert.ToDouble(dataGridView.Rows[i2].Cells[j2].Value));
                }
            }
            return list;
        }

        private void buttonRandomize_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    dataGridView1.Rows[j].Cells[i].Value = rnd.Next(0, 3).ToString();
                    dataGridView4.Rows[j].Cells[i].Value = rnd.Next(0, 3).ToString();
                }
            }
            for (int i = 0; i < numericUpDownCriteries.Value; i++)
            {
                dataGridView3.Rows[0].Cells[i].Value = rnd.Next(0, 3).ToString();
            }
        }

        private static double GetSumOfVector(List<double> vector)
        {
            double sum = 0;
            foreach (var element in vector)
            {
                sum += element;
            }
            return sum;
        }

        private static Matrix GetMatrixFromListOfLists(List<List<double>> matrix)
        {
            var mtrx = new Matrix(matrix.Count, matrix[0].Count);

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    mtrx[i, j] = Math.Round(matrix[i][j], 2);
                }
            }

            return mtrx;
        }

        private static void PrintMatrix(Matrix matrix)
        {
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    Console.Write(matrix[i, j].ToString("0.00") + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            var matrix = dgwTo2dListOfDouble(dataGridView1);
            var mtrx = GetMatrixFromListOfLists(matrix);
            PrintMatrix(mtrx);

            var matrixTrans = mtrx;

            for (int i = 0; i < matrixTrans.Rows; i++)
            {
                for (int j = 0; j < matrixTrans.Columns; j++)
                {
                    matrixTrans[i, j] = Convert.ToDouble(dataGridView4.Rows[i].Cells[j].Value);
                }
            }

            mtrx = matrixTrans.Transpose();

            var finalResult = new List<double>();

            for (int i = 0; i < mtrx.Rows; i++)
            {
                double sum = 0;
                for (int j = 0; j < mtrx.Columns; j++)
                {
                    sum += mtrx[i, j] * Convert.ToDouble(dataGridView3.Rows[0].Cells[j].Value);
                }
                finalResult.Add(sum);
            }

            int UIndex = 0;

            richTextBox1.Text = "";
            
            foreach (var item in finalResult)
            {
                richTextBox1.Text += $"U({UIndex++}) = {item} \n";
            }

        }
    }
}
