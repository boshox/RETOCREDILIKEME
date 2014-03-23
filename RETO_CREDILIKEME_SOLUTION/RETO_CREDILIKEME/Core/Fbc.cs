using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Converters;
using RETO_CREDILIKEME.Models;
using System.Data.SqlClient;

namespace RETO_CREDILIKEME.Core
{
    /// <summary>
    /// CORE DE LA API
    /// </summary>
    public class Fbc
    {
        private SqlConnection connection;

        public Fbc() 
        {
            this.connection = new SqlConnection("Data Source=XXX.XXX.XXX.XXX;Initial Catalog=XXX;Persist Security Info=True;User ID=XXX;Password=XXX");
        }

        /// <summary>
        /// Valida informacion proporcionada con Facebook
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dynamic getUserInfo(Identificador id)
        {
            try
            {
                var client = new Facebook.FacebookClient(id.token);
                dynamic me = client.Get(id.id_facebook);
                string info = Convert.ToString(me);
                FacebookResponse user = JsonConvert.DeserializeObject<FacebookResponse>(info);
                return user;
            }

            catch (FacebookOAuthException)
            {
                return null;
            }
        }

        /// <summary>
        /// Valida la informacion que se obtiene de Facebook
        /// con la informacion de la bdd interna
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Jugador validate(Identificador id)
        {
            FacebookResponse response = getUserInfo(id);
            Jugador jugador;
            if (response != null)
            {
                jugador = verifyUser(id);
                if (jugador != null)
                {
                    if (response.email.ToLower().Equals(jugador.email.ToLower()))
                    {
                        return jugador;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// proc_reto_ObtenerJugador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Jugador verifyUser(Identificador id)
        {
            var command = new SqlCommand(StoredProcedures.proc_reto_ObtenerJugador.ToString(),
                                        connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@token", id.token);
            command.Parameters.AddWithValue("@id_facebook", id.id_facebook);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Jugador player = new Jugador();

            while (reader.Read())
            {
                player.nombre = (String)reader.GetValue(0);
                player.apellido_paterno = (String)reader.GetValue(1);
                player.apellido_materno = (String)reader.GetValue(2);
                player.fecha_nacimiento = (DateTime)reader.GetValue(3);
                player.sexo = (String)reader.GetValue(4);
                player.tel_celular = (String)reader.GetValue(5);
                player.ocupacion = (String)reader.GetValue(6);
                player.email = (String)reader.GetValue(7);
                player.twitter = (String)reader.GetValue(8);
                player.email_adicional = (String)reader.GetValue(9);
                player.cuenta_stp = (String)reader.GetValue(10);
            }
            connection.Close();
            if (player.nombre != null)
                return player;

            return null;
        }

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// proc_reto_ObtenerClienteFacebookId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClienteFacebook getFbClientId(Identificador id) 
        {
            String s = StoredProcedures.proc_reto_ObtenerClienteFacebookId.ToString();
            var command = new SqlCommand(StoredProcedures.proc_reto_ObtenerClienteFacebookId.ToString(),
                connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@token", id.token);
            command.Parameters.AddWithValue("@id_facebook", id.id_facebook);

            ClienteFacebook cliente = new ClienteFacebook();
            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cliente.id_cliente = (int)reader.GetValue(0);
                    cliente.id_usuario = (String)reader.GetValue(1);
                    cliente.id_facebook = (String)reader.GetValue(2);
                    cliente.nb_facebook = (String)reader.GetValue(3);
                    cliente.link_facebook = (String)reader.GetValue(4);
                    cliente.fecha_ingreso = (DateTime)reader.GetValue(5);
                    cliente.link_identificacion = (String)reader.GetValue(6);
                    cliente.localidad = (String)reader.GetValue(7);
                    cliente.entidad = (String)reader.GetValue(8);
                    cliente.tipo_movimiento = (String)reader.GetValue(9);
                    cliente.usuario_registro = (String)reader.GetValue(10);
                    cliente.fecha_ultimo_movimiento = (DateTime)reader.GetValue(11);
                }
                return cliente;
            }
            catch 
            {
                return null;
            }
            finally { connection.Close(); }
        }

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// proc_reto_ObtenerBancoDisponible.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Banco getBank(Identificador id)
        {
            var command = new SqlCommand(StoredProcedures.proc_reto_ObtenerBancoDisponible.ToString(),
                connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@token", id.token);

            Banco banco = new Banco();
            connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                //System.Threading.Tasks.Task<SqlDataReader> r = command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    banco.id = (int)reader.GetValue(0);
                    banco.descripcion = (String)reader.GetValue(1);
                    banco.status = (char)reader.GetValue(2);
                    banco.recibe_pagos = (Int16)reader.GetValue(3);
                    banco.num_cuenta = (String)reader.GetValue(4);
                    banco.clabe = (String)reader.GetValue(5);
                    banco.referencia = (String)reader.GetValue(6);
                }
                return banco;
            }
            catch 
            {
                return null;
            }
            finally
            { connection.Close(); }
        }

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// PROC_RETO_CONSULTA_SALDO_JUGADOR.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Saldo getBalance(Identificador id) 
        {
            var command = new SqlCommand(StoredProcedures.PROC_RETO_CONSULTA_SALDO_CAJERO.ToString(),
                connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@token", id.token);
            command.Parameters.AddWithValue("@id_cliente", id.id_cliente);

            Saldo saldo = new Saldo();
            connection.Open();

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    saldo.total_pagar = (Decimal)reader.GetValue(0);
                    saldo.cuotas_pagadas = (int)reader.GetValue(1);
                    saldo.num_cuotas = (int)reader.GetValue(2);
                    saldo.monto = (Decimal)reader.GetValue(3);
                    saldo.proximo_pago = (char)reader.GetValue(4);
                    saldo.status_cumplimiento = (int)reader.GetValue(5);
                }
                return saldo;
            }
            catch 
            {
                return null;
            }
            finally { connection.Close(); }
        }

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// proc_reto_consultagarantiasliquidas.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GarantiaLiquida getWarranty(Identificador id)
        {
            var command = new SqlCommand(StoredProcedures.proc_reto_consultargarantiasliquidas.ToString(),
                connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@token", id.token);
            command.Parameters.AddWithValue("@id_cliente", id.id_cliente);

            GarantiaLiquida garantia = new GarantiaLiquida();
            connection.Open();

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    garantia.id_cliente = (int)reader.GetValue(0);
                    garantia.tipo_movimiento = (int)reader.GetValue(1);
                    garantia.monto = (int)reader.GetValue(2);
                    garantia.status = (int)reader.GetValue(3);
                    garantia.fecha_registro = (DateTime)reader.GetValue(4);
                    garantia.fecha_modifica = (DateTime)reader.GetValue(5);
                }
                return garantia;
            }
            catch 
            {
                return null;
            }
            finally { connection.Close(); }
        }

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// proc_reto_guardargarantialiquida.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int saveWarranty(GarantiaLiquida garantia)
        {
            var command = new SqlCommand(StoredProcedures.proc_reto_guardargarantialiquida.ToString(),
                connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@token", garantia.token);
            command.Parameters.AddWithValue("@id_cliente", garantia.id_cliente);
            command.Parameters.AddWithValue("@tipoMovimiento", garantia.tipo_movimiento);
            command.Parameters.AddWithValue("@monto", garantia.monto);

            int result = 0;
            try
            {
                connection.Open();
                var returnValue = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                returnValue.Direction = System.Data.ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();
                result = Convert.ToInt32(returnValue);
                return 1;
            }
            catch 
            {
                return 0;
            }
            finally { connection.Close(); }
        }

        /// <summary>
        /// Acción que ejecuta el procedimiento almacenado
        /// proc_reto_GuardarAbono.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int savePayment(Abono abono)
        {
            var command = new SqlCommand(StoredProcedures.proc_reto_GuardarAbono.ToString(),
                connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@token", abono.token);
            command.Parameters.AddWithValue("@id_cliente", abono.id_cliente);
            command.Parameters.AddWithValue("@importe", abono.importe);
            command.Parameters.AddWithValue("@observacion", abono.observacion);
            command.Parameters.AddWithValue("@id_forma_pago", abono.id_forma_pago);
            command.Parameters.AddWithValue("@referencia", abono.referencia);
            command.Parameters.AddWithValue("@id_banco", abono.id_banco);

            int result = 0;
            try
            {
                connection.Open();
                var returnValue = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                returnValue.Direction = System.Data.ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();
                result = Convert.ToInt32(returnValue);
                return 1;
            }
            catch 
            {
                return 0;
            }
            finally { connection.Close(); }
        }
    }
}