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


        public Form1()
        {
            InitializeComponent();
        }

        // guardado de datos del dueno y de su casa

        void guardar_datos_list() {
            Propietarios proptempo = new Propietarios();
            proptempo.Numero_de_casa = Convert.ToInt32(textBox1.Text);
            proptempo.Dpi = Convert.ToInt32(textBox2.Text);
            proptempo.Nombre = textBox3.Text;
            proptempo.Apellido = textBox4.Text;
            datos_dueno.Add(proptempo);
        }
        void escribir_datos_propietarios() {
            FileStream canal_crear = new FileStream("Datos_propietario.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter escribir = new StreamWriter(canal_crear);
            foreach (var p in datos_dueno) {
                escribir.WriteLine(p.Dpi);
                escribir.WriteLine(p.Nombre);
                escribir.WriteLine(p.Apellido);
            }
            escribir.Close();
        }
        void leer_datos_dueno(){
            FileStream leeren = new FileStream("Datos_propietario.txt", FileMode.Open, FileAccess.Read);
            StreamReader abrir = new StreamReader(leeren);
            while (abrir.Peek() > -1 ){
                Propietarios casatempo = new Propietarios();
                casatempo.Numero_de_casa = Convert.ToInt32(abrir.Read());
                casatempo.Dpi =Convert.ToInt32( abrir.Read());
                casatempo.Nombre = abrir.Read().ToString();
                casatempo.Apellido = abrir.Read().ToString();
                datos_dueno.Add(casatempo);
            }
            abrir.Close();
        }

        // guardar datos de las casas
        void guardar_casas_list (){
            Datos_Casa casatempo = new Datos_Casa();
            casatempo.Numero_de_casa = Convert.ToInt32(textBox1.Text);
            casatempo.Dpi_dueno_casa = Convert.ToInt32(textBox2.Text);
            casatempo.Cuota_mantenimiento = double.Parse(textBox5.Text);
            datos_casa.Add(casatempo);
        }
        void escribir_datos_casas() {
            FileStream canal_casa = new FileStream("Datos_casa.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter escribe_casa = new StreamWriter(canal_casa);
            foreach (var a in datos_casa) {
                escribe_casa.WriteLine(a.Numero_de_casa);
                escribe_casa.WriteLine(a.Dpi_dueno_casa);
                escribe_casa.WriteLine(a.Cuota_mantenimiento);
            }
            escribe_casa.Close();
        }
        void leer_dato_casa() {
            FileStream leer_casa = new FileStream("Datos_casa.txt", FileMode.Open, FileAccess.Read);
            StreamReader kleer = new StreamReader(leer_casa);
            while (kleer.Peek() >- 1) {
                Datos_Casa datos_ = new Datos_Casa();
                datos_.Numero_de_casa = Convert.ToInt32(kleer.ReadLine());
                datos_.Dpi_dueno_casa = Convert.ToInt32(kleer.ReadLine());
                datos_.Cuota_mantenimiento = double.Parse(kleer.ReadLine());
                datos_casa.Add(datos_);
            }
            kleer.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != ""
                && textBox4.Text != "" && textBox5.Text != "")
            {
                guardar_datos_list();
                guardar_casas_list();
                escribir_datos_propietarios();
                escribir_datos_casas();
            }
            else { 
                MessageBox.Show ("Complete los campos requeridos");
            }
        }
    }
}
