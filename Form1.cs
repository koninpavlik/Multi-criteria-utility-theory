using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
                dataGridView3.Columns.Add(i.ToString(), i.ToString());
                dataGridView3.Columns[i].DefaultCellStyle.NullValue = "0";
                dataGridView4.Columns.Add(i.ToString(), i.ToString());
                dataGridView4.Columns[i].DefaultCellStyle.NullValue = "0";
            }
            for (int i = 0; i < numericUpDownCriteries.Value; i++)
            {
                dataGridView1.Rows.Add();
            }

            dataGridView3.Rows.Add();
            dataGridView4.Rows.Add();
        }

        private void numericUpDownCriteries_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(((UpDownBase)sender).Text) < numericUpDownCriteries.Value)
            {
                dataGridView1.Rows.Add();
            }
            else
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            }
        }

        private void numericUpDownElements_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(((UpDownBase)sender).Text) < numericUpDownElements.Value)
            {
                dataGridView1.Columns.Add((dataGridView1.Columns.Count).ToString(), (dataGridView1.Columns.Count).ToString());
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].DefaultCellStyle.NullValue = "0";
                dataGridView3.Columns.Add((dataGridView3.Columns.Count).ToString(), (dataGridView3.Columns.Count).ToString());
                dataGridView3.Columns[dataGridView3.Columns.Count - 1].DefaultCellStyle.NullValue = "0";
                dataGridView4.Columns.Add((dataGridView3.Columns.Count).ToString(), (dataGridView3.Columns.Count).ToString());
                dataGridView4.Columns[dataGridView3.Columns.Count - 1].DefaultCellStyle.NullValue = "0";

            }
            else
            {
                dataGridView1.Columns.Remove((Convert.ToInt32(((UpDownBase)sender).Text) - 1).ToString());
                dataGridView3.Columns.Remove((Convert.ToInt32(((UpDownBase)sender).Text) - 1).ToString());
                dataGridView4.Columns.Remove((Convert.ToInt32(((UpDownBase)sender).Text) - 1).ToString());
            }
        }

        private void buttonRandomize_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    dataGridView1.Rows[j].Cells[i].Value = rnd.Next(0, 15).ToString();
                    dataGridView3.Rows[0].Cells[i].Value = rnd.Next(0, 15).ToString();
                    dataGridView4.Rows[0].Cells[i].Value = rnd.Next(0, 15).ToString();
                }
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

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Refresh();

            List<List<double>> matrix = new List<List<double>>();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                matrix.Add(new List<double>());
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    var value = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value) * Convert.ToDouble(dataGridView3.Rows[dataGridView3.RowCount - 1].Cells[j].Value);
                    matrix[i].Add(value);
                }
            }

            double result = 0;

            int index = 0;

            for (int j = 0; j < matrix.Count; j++)
            {
                double localResult = 0;
                for (int i = 0; i < matrix[j].Count; i++)
                {
                    localResult += Math.Pow(matrix[j][i] - Convert.ToDouble(dataGridView4.Rows[0].Cells[i].Value), 2);
                }

                if (localResult > result)
                {
                    result = localResult;
                    index = j;
                }
            }

            for (int i = 0; i <= numericUpDownElements.Value; i++)
            {
                dataGridView2.Columns.Add(i.ToString(), i.ToString());
                dataGridView2.Columns[i].DefaultCellStyle.NullValue = "0";
            }


            dataGridView2.Rows.Add();
            for (int j = 0; j < matrix[index].Count; j++)
            {
                dataGridView2.Rows[0].Cells[j].Value = matrix[index][j];
            }

            richTextBox1.Text = $"Оптимальный вектор {index} \n";
            richTextBox1.Text += $"Результат: sqrt({result}) = {Math.Sqrt(result)}";
        }
    }
}
