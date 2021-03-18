using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio_tres_repaso
{
    public partial class Form1 : Form
    {
        List<Propietarios> datos_dueno = new List<Propietarios>();
        List<Datos_Casa> datos_casa = new List<Datos_Casa>();
        List<datos_en__conjunto> ultimo = new List<datos_en__conjunto>();

        public Form1()
        {
            InitializeComponent();
        }

        // guardado de datos del dueno y de su casa

        void guardar_propietario() {
            Propietarios proptempo = new Propietarios();
            proptempo.Numero_de_casa = Convert.ToInt32(textBox1.Text);
            proptempo.Dpi = Convert.ToInt32(textBox2.Text);
            proptempo.Nombre = textBox3.Text;
            proptempo.Apellido = textBox4.Text;
            proptempo.Cantidad_casas = 1;
            datos_dueno.Add(proptempo);
        }
        void escribir_propietarios() {
            FileStream canal_crear = new FileStream("Datos_propietario.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter escribir = new StreamWriter(canal_crear);
            foreach (var p in datos_dueno) {
                escribir.WriteLine(p.Numero_de_casa);
                escribir.WriteLine(p.Dpi);
                escribir.WriteLine(p.Nombre);
                escribir.WriteLine(p.Apellido);
                escribir.WriteLine(p.Cantidad_casas);
            }
            escribir.Close();
        }

        void guardar_casa (){
            Datos_Casa casatempo = new Datos_Casa();
            casatempo.Numero_de_casa = Convert.ToInt32(textBox1.Text);
            casatempo.Dpi_dueno_casa = Convert.ToInt32(textBox2.Text);
            casatempo.Cuota_mantenimiento = double.Parse(textBox5.Text);
            datos_casa.Add(casatempo);
        }
        void escribir_casas() {
            FileStream canal_casa = new FileStream("Datos_casa.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter escribe_casa = new StreamWriter(canal_casa);
            foreach (var a in datos_casa) {
                escribe_casa.WriteLine(a.Numero_de_casa);
                escribe_casa.WriteLine(a.Dpi_dueno_casa);
                escribe_casa.WriteLine(a.Cuota_mantenimiento);
            }
            escribe_casa.Close();
        }
     
        void guardar_general()
        {
            datos_en__conjunto conjuntotempo = new datos_en__conjunto();
            conjuntotempo.Numero_de_casa = Convert.ToInt32(textBox1.Text);
            conjuntotempo.Dpi = Convert.ToInt32(textBox2.Text);
            conjuntotempo.Nombre = textBox3.Text;
            conjuntotempo.Apellido = textBox4.Text;
            conjuntotempo.Cuota_mantenimiento = double.Parse(textBox5.Text);
            conjuntotempo.Cantidad_casas = 1;
            ultimo.Add(conjuntotempo);
        }


        void escritura_general() { 
            FileStream canal_c = new FileStream("Datos_en_Conjunto.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter escribe_c = new StreamWriter(canal_c);
            foreach (var s in ultimo)
            {
                escribe_c.WriteLine(s.Numero_de_casa);
                escribe_c.WriteLine(s.Dpi);
                escribe_c.WriteLine(s.Nombre);
                escribe_c.WriteLine(s.Apellido);
                escribe_c.WriteLine(s.Cuota_mantenimiento);
                escribe_c.WriteLine(s.Cantidad_casas);
            }
            escribe_c.Close();
        }
        void lectura_general() { 
            FileStream leer_ = new FileStream("Datos_en_Conjunto.txt", FileMode.Open, FileAccess.Read);
            StreamReader kleer = new StreamReader(leer_);
            while (kleer.Peek() > -1)
            {
                datos_en__conjunto datos_ = new datos_en__conjunto();
                datos_.Numero_de_casa = Convert.ToInt32(kleer.ReadLine());
                datos_.Dpi = Convert.ToInt32(kleer.ReadLine());
                datos_.Nombre = kleer.ReadLine();
                datos_.Apellido = kleer.ReadLine();
                datos_.Cuota_mantenimiento =double.Parse( kleer.ReadLine());
                datos_.Cantidad_casas = Convert.ToInt32(kleer.ReadLine());

                ultimo.Add(datos_);
            }
            kleer.Close();
        }
        void imprimir() {
 
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ultimo;
            dataGridView1.Refresh();
        }

        private void MostrarReporte(bool ordenada = false) {

            ultimo.Clear();
            lectura_general();

            if (ordenada)
                ultimo = ultimo.OrderBy(r => r.Cuota_mantenimiento).ToList();

            imprimir();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lectura_general();
            imprimir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                guardar_propietario();
                escribir_propietarios();
                guardar_casa();
                escribir_casas();

                guardar_general();
                escritura_general();
                imprimir();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            MostrarReporte(true);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            var repetidos = ultimo.GroupBy(r => r.Dpi);
            int max = 0;
            int pos = 0;

            for (int b = 0; b < repetidos.Count(); b++) {
                if (repetidos.ToList()[b].Count() > max) {
                    max = repetidos.ToList()[b].Count();
                    pos = b;
                }
            }
            label7.Text = "El DPI:" + repetidos.ToList()[pos].Key;
            label8.Text = " Posee " + max.ToString() + " propiedades"; 
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            var agrupado = ultimo.GroupBy(r => r.Dpi);

            double maxCuota = 0;
            int maxDpi=0 ;


            foreach (var grupo in agrupado)
            {

                double sumaCuota = 0;
                int dpi=0 ;

                foreach (var p in grupo)
                {
                    sumaCuota += p.Cuota_mantenimiento;
                    dpi = p.Dpi;
                }

                if (sumaCuota > maxCuota)
                {
                    maxCuota = sumaCuota;
                    maxDpi = dpi;
                }
            }


            label11.Text ="DPI: "+ maxDpi.ToString();
            label12.Text = "Cuota: "+maxCuota.ToString();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            MostrarReporte(true);
            int cuantos = ultimo.Count();
            label9.Text = "Mas Bajas: " + ultimo[0].Cuota_mantenimiento.ToString()+"," 
                                        + ultimo[1].Cuota_mantenimiento.ToString()+"," 
                                        + ultimo[3].Cuota_mantenimiento.ToString();
            label10.Text = "Mas Alta: " + ultimo[cuantos - 1].Cuota_mantenimiento.ToString() + ","
                                        + ultimo[cuantos - 2].Cuota_mantenimiento.ToString() + ","
                                        + ultimo[cuantos - 3].Cuota_mantenimiento.ToString();
        }

 
    }
}
