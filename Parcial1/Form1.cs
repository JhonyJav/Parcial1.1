using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Parcial1
{
    public partial class Form1 : Form
    {
        List<Dar> dars = new List<Dar>();
        List<Libro> libros = new List<Libro>();
        List<Alumno> alumnos = new List<Alumno>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Guardar()
        {
            FileStream stream = new FileStream("Prestados.txt", FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter writer = new StreamWriter(stream);

            foreach (var presta in dars )
            {
                writer.WriteLine(presta.Carnet1);
                writer.WriteLine(presta.Codigo);
                writer.WriteLine(presta.FechaInicio2);
                writer.WriteLine(presta.FechaFinal2);

            }

            writer.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dar TempDar = new Dar();

            TempDar.Carnet1 = Convert.ToInt32(textBox1.Text);
            TempDar.Codigo = Convert.ToInt32(textBox2.Text);
            TempDar.FechaInicio2 = dateTimePicker1.Value;
            TempDar.FechaFinal2 = dateTimePicker2.Value;

            dars.Add(TempDar);

            Guardar();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists("Prestados.txt"))
            {
                FileStream stream = new FileStream("Prestados.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);

                while (reader.Peek() > -1)
                {
                    Dar TempDar = new Dar();
                    TempDar.Carnet1 = Convert.ToInt32(reader.ReadLine());
                    TempDar.Codigo = Convert.ToInt32(reader.ReadLine());
                    TempDar.FechaInicio2 = Convert.ToDateTime(reader.ReadLine());
                    TempDar.FechaFinal2 = Convert.ToDateTime(reader.ReadLine());

                    dars.Add(TempDar);

                }
                reader.Close();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dars;
                dataGridView1.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var rep = dars.GroupBy(p => p.Carnet1);

            int max = 0;
            int pos = 0;

            for (int i = 0; i < rep.Count(); i++)
            {
                if (rep.ToList()[i].Count() > max)
                {
                    max = rep.ToList()[i].Count();
                    pos = i;
                }
            }

            label5.Text = "Disponibilidad es:  " + max.ToString() + "  de libros";
        }
    }
}
