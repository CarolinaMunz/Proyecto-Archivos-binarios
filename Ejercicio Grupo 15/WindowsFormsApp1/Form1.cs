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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos Binarios (*.bin)|*.bin";
                    saveFileDialog.Title = "Crea un nuevo archivo";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (FileStream fs = File.Create(saveFileDialog.FileName))
                        {
                            //Archivo cerrado
                        }
                        MessageBox.Show("Archivo creado exitosamente.");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error al seleccionar el archivo: {ex.Message}");
            }

        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                using(OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Archivos Binarios (*.bin)|*.bin";
                    openFileDialog.Title = "Selecciona un archivo existente";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        txtPathFile.Text = openFileDialog.FileName;
                        MessageBox.Show("Archivo seleccionado exitosamente.");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error al seleccionar el archivo: {ex.Message}");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPathFile.Text))
                {
                    MessageBox.Show("Por favor, selecciona un archivo antes de guardar los datos.");
                    return;
                }

                string nombre = txtNombre.Text;

                if ( !int.TryParse(txtDNI.Text, out int dni) || !int.TryParse(txtNumeroTelefono.Text, out int numTel))
                {
                    MessageBox.Show("Por favor, complete el DNI y el nmero de teléfono con números enteros.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("Por favor, complete todos los campos.");
                    return;
                }
                
                using (BinaryWriter writer = new BinaryWriter(File.Open(txtPathFile.Text, FileMode.Append)))
                {
                    writer.Write(nombre);
                    writer.Write(dni);
                    writer.Write(numTel);
                }

                MessageBox.Show("Datos guardados exitosamente.");

                txtNombre.Clear();
                txtDNI.Clear();
                txtNumeroTelefono.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}");
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPathFile.Text))
                {
                    MessageBox.Show("Por favor, selecciona un archivo antes de leer los datos.");
                    return;
                }

                if (!File.Exists(txtPathFile.Text))
                {
                    MessageBox.Show("El archivo especificado no existe.");
                    return;
                }

                using (BinaryReader reader = new BinaryReader(File.Open(txtPathFile.Text, FileMode.Open)))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length) 
                    {
                        string nombre = reader.ReadString();
                        int dni = reader.ReadInt32();
                        int numTel = reader.ReadInt32();

                        lbAlumnos.Items.Add($"Nombre: {nombre}, DNI: {dni}, Teléfono: {numTel}");
                    }
                }

                MessageBox.Show("Datos leídos exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer los datos: {ex.Message}");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lbAlumnos.Items.Clear();
        }
    }
}
