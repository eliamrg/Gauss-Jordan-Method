using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GaussJordan
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            actualizarMatriz(Convert.ToInt32(numericUpDown1.Value));
        }

        //Cuando se cambia el valor del numericUpDown se actualiza la dimensión de la matriz
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            actualizarMatriz(Convert.ToInt32(numericUpDown1.Value));
        }
        private void actualizarMatriz(int tamano)
        {
            dgv_matriz.Rows.Clear();
            dgv_solucion.Rows.Clear();
            rtb_proceso.Clear();
            dgv_matriz.ColumnCount = tamano + 1;
            dgv_matriz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < tamano; i++)
            {
                dgv_matriz.Columns[i].Name = "X" + (i + 1);
                ArrayList fila = new ArrayList();
                for (int j = 0; j <= tamano; j++)
                {
                    fila.Add(0);
                }
                dgv_matriz.Rows.Add(fila.ToArray());
            }
            dgv_matriz.Columns[tamano].Name = "Valor(=)";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            dgv_solucion.Rows.Clear();
            rtb_proceso.Clear();
            try
            {
                //n Va a ser la cantidad de variables, las que diga el usuario maximo 10
                int n = Convert.ToInt32(numericUpDown1.Value);


                //Declaracion de la matriz como un arreglo de dos dimensiones
                double[,] matriz = new double[n, n + 1];

                //Llenado de la matriz en base a la info ingresada por el usuario
                foreach (DataGridViewRow row in dgv_matriz.Rows)
                {
                    for (int i = 0; i <= n; i++)
                    {
                        matriz[row.Index, i] = Convert.ToDouble(dgv_matriz.Rows[row.Index].Cells[i].Value.ToString().Trim()); ;
                    }

                }
                rtb_proceso.AppendText("\nRESOLVIENDO LA MATRIZ:\n");
                //Console.WriteLine("\nRESOLVIENDO LA MATRIZ:");
                imprimirMatriz(matriz, n);
                rtb_proceso.AppendText("\nRESOLUCIÓN\n");
                //Console.WriteLine("\nRESOLUCION");
                double pivote = 0;
                //x es una variable que dice por cuanto hay que multiplicar para hacer 0 los numeros de la columna del pivote
                double x = 0;

                for (int i = 0; i < n; i++)
                {

                    //se obtiene el pivote en la posicion i,i para ir por la diagonal
                    pivote = matriz[i, i];

                    for (int j = i; j <= n; j++)
                    {
                        //Se divide a la fila completa entre el pivote, esto lo hace aunque el pivote sea 1, no afecta:)
                        matriz[i, j] = matriz[i, j] / pivote;

                    }

                    for (int k = 0; k < n; k++)
                    {
                        if (k != i)//Mientras no estemos en el pivote
                        {
                            x = matriz[k, i];//Se obtiene el valor de la posicion actual para multiplicar y hacer 0 los valores de la columna del pivote
                            for (int l = i; l <= n; l++)
                            {
                                //Se actualizan todos los valores haciendo la resta de 1 fila menos la otra
                                matriz[k, l] = matriz[k, l] - x * matriz[i, l];
                            }
                        }
                        imprimirMatriz(matriz, n);
                    }
                }

                
                for (int i = 0; i < n; i++)
                {
                    dgv_solucion.Rows.Add( "X"+(i+1), matriz[i,n]);
                }
                
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Mensaje de Error \n"+ex,"Error al Calcular", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        void imprimirMatriz(double[,] m, int n)
        {
            
            for (int i = 0;i<n;i++)
            {
                for (int j = 0;j<=n;j++) {
                    rtb_proceso.AppendText(m[i, j] + "\t");
                    //Console.Write(m[i,j]+"\t");
                }
                //Console.WriteLine();
                rtb_proceso.AppendText("\n");
            }
            rtb_proceso.AppendText("\n\n");
            //Console.WriteLine("\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Al cambiar el valor se reinicia todo
            numericUpDown1.Value = 3;
            numericUpDown1.Value = 2;
        }

        private void px_info_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.ShowDialog();

        }
    }
}
