using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CompaniaPrueba
{
    public partial class Form1 : Form
    {
        private int parsedProductoID;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void label1_Click_3(object sender, EventArgs e)
        {
            

        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=192.168.1.115;Initial Catalog=Compania;Persist Security Info=True;User ID=ITdemetech;Password=ITdemetech2019!";
            SqlConnection sqlCon = new SqlConnection(connectionString);
            sqlCon.Open();
            string commandString = "SELECT id, descripcion, precio FROM Compania.dbo.Producto WHERE codigoProducto = '" + txtCodigo.Text + "'";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader read = sqlCmd.ExecuteReader();
            while (read.Read())
            {
                txtDescripcion.Text = read["descripcion"].ToString();
                txtPrecio.Text = read["precio"].ToString();
                txtIdProducto.Text = read["id"].ToString();
            }
            sqlCon.Close();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            // Inicializando conneccion.
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("dbo.usp_CrearProducto", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.NVarChar, 255));
                    sqlCommand.Parameters["@descripcion"].Value = txtDescripcion.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("@codigoProducto", SqlDbType.NVarChar, 20));
                    sqlCommand.Parameters["@codigoProducto"].Value = txtCodigo.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("@precio", SqlDbType.Int));
                    sqlCommand.Parameters["@precio"].Value = txtPrecio.Text;

                    try
                    {
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("El producto no se ha podido guardar");
                    }
                    finally
                    {
                        connection.Close();
                        MessageBox.Show("PRODUCTO GUARDADO CORRECTAMENTE");
                    }
                }
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdProducto.Clear();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            this.parsedProductoID = 0;
        }

        private void label1_Click_4(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'companiaDataSet.Producto' table. You can move, or remove it, as needed.
            this.productoTableAdapter.Fill(this.companiaDataSet.Producto);

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Inicializando conneccion.
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("dbo.usp_EliminarProducto", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@IDProducto", SqlDbType.Int));
                    sqlCommand.Parameters["@IDProducto"].Value = txtIdProducto.Text;

                    try
                    {
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("El producto no se ha podido eliminar");
                    }
                    finally
                    {
                        connection.Close();
                        MessageBox.Show("PRODUCTO ELIMINADO CORRECTAMENTE");
                    }
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            // Inicializando conneccion.
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("dbo.usp_ActualizarProducto", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@IDProducto", SqlDbType.Int));
                    sqlCommand.Parameters["@IDProducto"].Value = txtIdProducto.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.NVarChar, 255));
                    sqlCommand.Parameters["@descripcion"].Value = txtDescripcion.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("@codigoProducto", SqlDbType.NVarChar, 20));
                    sqlCommand.Parameters["@codigoProducto"].Value = txtCodigo.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("@precio", SqlDbType.Int));
                    sqlCommand.Parameters["@precio"].Value = txtPrecio.Text;

                    try
                    {
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("El producto no se ha podido actualizar");
                    }
                    finally
                    {
                        connection.Close();
                        dataGridView1.DataSource = null;
                        dataGridView1.Rows.Clear();
                        dataGridView1.Refresh();
                        dataGridView1.Show();
                        MessageBox.Show("PRODUCTO ACTUALIZADO CORRECTAMENTE");
                    }
                }

            }
        }
    }
}
