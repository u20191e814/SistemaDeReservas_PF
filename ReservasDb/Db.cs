using Dapper;
using ReservaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasDb
{
    public class Db:IDB
    {
        private string sqlconexion = string.Empty;
        public Db(string sqlconexion)
        {
            this.sqlconexion = sqlconexion;
        }
        public Estructura_Post_int RegistrarReservaFullDay(ReservaFullDay reserva)
        {
            Estructura_Post_int estructura = new Estructura_Post_int();
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(sqlconexion))
                {
                    string squery = string.Format(" insert into [Proyecto].[dbo].[Reserva_FullDay] (Fecha_Viaje, Fk_Origen, Fk_Destino, precioUnitario, cantidad, precioTotal, Dni_cliente,Nombre, Apellido, Telefono,correo,Estado ) " +
                        "  output inserted.Pk_Reserva_FullDay   values(@fechaViaje, {0},{1},@precioUnitario, {2},@precioTotal, @dni,@nombre, @apellido, @telefono, @correo, @estado )", reserva.Fk_Origen, reserva.Fk_Destino, reserva.cantidad);
                    var param = new DynamicParameters();
                    param.Add("@fechaViaje", reserva.Fecha_Viaje);
                    param.Add("@precioUnitario", reserva.precioUnitario);
                    param.Add("@precioTotal", reserva.precioTotal);
                    param.Add("@dni", reserva.Dni_cliente);
                    param.Add("@nombre", reserva.Nombre);
                    param.Add("@apellido", reserva.Apellido);
                    param.Add("@telefono", reserva.Telefono);
                    param.Add("@correo", reserva.correo);
                    param.Add("@estado", reserva.Estado);
                    estructura.data = cn.QueryFirstOrDefault<int>(squery, param, null, 0, System.Data.CommandType.Text);
                }

                estructura.codigo = "Ok";
                estructura.mensaje = "Ok";
            }
            catch (Exception ex)
            {
                estructura.codigo = "400";
                estructura.mensaje = ex.Message;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            return estructura;
        }
        public Estructura_Post_Bool ModificarReservaFullDay(ReservaFullDay reserva)
        {
            Estructura_Post_Bool estructura = new Estructura_Post_Bool();
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(sqlconexion))
                {
                    string squery = string.Format("  update [Proyecto].[dbo].[Reserva_FullDay] set Fecha_Viaje = @fechaViaje, Fk_Origen ={0},Fk_Destino ={1}, precioUnitario = @precioUnitario, cantidad ={2}, precioTotal = @precioTotal " +
                        "  , Dni_cliente = @dni, Nombre = @nombre,Apellido = @apellido, Telefono = @telefono,correo = @correo, Estado = @estado   where Pk_Reserva_FullDay = {3}", reserva.Fk_Origen, reserva.Fk_Destino, reserva.cantidad, reserva.Pk_Reserva_FullDay);

                    var param = new DynamicParameters();
                    param.Add("@fechaViaje", reserva.Fecha_Viaje);
                    param.Add("@precioUnitario", reserva.precioUnitario);
                    param.Add("@precioTotal", reserva.precioTotal);
                    param.Add("@dni", reserva.Dni_cliente);
                    param.Add("@nombre", reserva.Nombre);
                    param.Add("@apellido", reserva.Apellido);
                    param.Add("@telefono", reserva.Telefono);
                    param.Add("@correo", reserva.correo);
                    param.Add("@estado", reserva.Estado);

                    cn.Execute(squery, param, null, 0, System.Data.CommandType.Text);

                }
                estructura.data = true;
                estructura.codigo = "OK";
                estructura.mensaje = "OK";
            }
            catch (Exception ex)
            {

                estructura.codigo = "400";
                estructura.mensaje = ex.Message;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            return estructura;
        }
        public Estructura_ReservaFullDay ObtenerReservaFullDay(int pk_Reserva_FullDay)
        {
            Estructura_ReservaFullDay estructura = new Estructura_ReservaFullDay();
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(sqlconexion))
                {
                    string squery = string.Format("  select Pk_Reserva_FullDay,Fecha_Viaje, Fecha_Generado,Fk_Origen, Fk_Destino, precioUnitario, cantidad, precioTotal, Dni_cliente, Nombre, Apellido, Telefono, correo, Estado from [Proyecto].[dbo].[Reserva_FullDay] where Pk_Reserva_FullDay={0}", pk_Reserva_FullDay);
                    estructura.data = cn.QueryFirstOrDefault<ReservaFullDay>(squery, null, null, 0, System.Data.CommandType.Text);
                }
                estructura.codigo = "OK";
                estructura.mensaje = "OK";
            }
            catch (Exception ex)
            {

                estructura.codigo = "400";
                estructura.mensaje = ex.Message;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            return estructura;
        }
        
        public Estructura_Post_Bool EliminarReservaFullDay(int pk_Reserva_FullDay)
        {
            Estructura_Post_Bool estructura = new Estructura_Post_Bool();
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(sqlconexion))
                {
                    string squery = string.Format("    delete from [Proyecto].[dbo].[Reserva_FullDay] where Pk_Reserva_FullDay={0} ",pk_Reserva_FullDay);
                     
                    cn.Execute(squery, null, null, 0, System.Data.CommandType.Text);

                }
                estructura.data = true;
                estructura.codigo = "OK";
                estructura.mensaje = "OK";
            }
            catch (Exception ex)
            {

                estructura.codigo = "400";
                estructura.mensaje = ex.Message;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            return estructura;
        }

        public Estructura_ReservaFullDay_Lista ObtenerReservasFullDay()
        {
            
            Estructura_ReservaFullDay_Lista estructura = new Estructura_ReservaFullDay_Lista();
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(sqlconexion))
                {
                    string squery = string.Format("  select Pk_Reserva_FullDay,Fecha_Viaje, Fecha_Generado,Fk_Origen, Fk_Destino, precioUnitario, cantidad, precioTotal, Dni_cliente, Nombre, Apellido, Telefono, correo, Estado from [Proyecto].[dbo].[Reserva_FullDay] ");
                    estructura.data = cn.Query<ReservaFullDay>(squery, null, null,true, 0, System.Data.CommandType.Text).ToList();
                }
                estructura.codigo = "OK";
                estructura.mensaje = "OK";
            }
            catch (Exception ex)
            {

                estructura.codigo = "400";
                estructura.mensaje = ex.Message;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            return estructura;
            
        }

       
        public Estructura_Promocion_FullDay obtenerPromocionFullDayPorId(int pk_promocionFullDay)
        {
            Estructura_Promocion_FullDay estructura = new Estructura_Promocion_FullDay();
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(sqlconexion))
                {
                    string squery = string.Format("    select Pk_promocionFullDay, format (FechaValido,'dd/MM/yyyy') fecha_valida, Precio, Descripcion, rutaImagen,Cupos, o.Nombre + ' - ' + d.Nombre as ruta ,tipoPromocion " +
                        "    from [Proyecto].[dbo].[Promocion_FullDay] v inner join Proyecto.dbo.Origen o on(v.Fk_origen = o.PK_origen) " +
                        "    inner join Proyecto.dbo.Destino d on(v.Fk_destino = d.PK_destino) " +
                        "    where v.Pk_promocionFullDay={0}   ", pk_promocionFullDay);
                    
                    estructura.data = cn.QueryFirstOrDefault<Promocion_FullDay>(squery, null, null, 0, System.Data.CommandType.Text);
                }

                estructura.codigo = "OK";
                estructura.mensaje = "OK";
            }
            catch (Exception ex)
            {
                estructura.codigo = "400";
                estructura.mensaje = ex.Message;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            if (estructura.data == null)
            {
                estructura.data = new Promocion_FullDay();
            }
            return estructura;
        }

        public Estructura_Origen_Lista obtenerOrigen()
        {
            Estructura_Origen_Lista estructura = new Estructura_Origen_Lista();
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(sqlconexion))
                {
                    string squery = string.Format("  select Pk_Origen,Nombre from [Proyecto].[dbo].[Origen] ");
                    estructura.data = cn.Query<Origen>(squery, null, null, true, 0, System.Data.CommandType.Text).ToList();
                }

                estructura.codigo = "OK";
                estructura.mensaje = "OK";
            }
            catch (Exception ex)
            {
                estructura.codigo = "400";
                estructura.mensaje = ex.Message;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            if (estructura.data == null)
            {
                estructura.data = new List<Origen>();
            }
            return estructura;
        }
        public Estructura_Destino_Lista obtenerDestino(int pk_origen)
        {
            Estructura_Destino_Lista estructura = new Estructura_Destino_Lista();
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(sqlconexion))
                {
                    string squery = string.Format("select Pk_Destino,Nombre from [Proyecto].[dbo].[Destino] where Fk_Origen={0}", pk_origen);
                    estructura.data = cn.Query<Destino>(squery, null, null, true, 0, System.Data.CommandType.Text).ToList();
                }

                estructura.codigo = "OK";
                estructura.mensaje = "OK";
            }
            catch (Exception ex)
            {
                estructura.codigo = "400";
                estructura.mensaje = ex.Message;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            if (estructura.data == null)
            {
                estructura.data = new List<Destino>();
            }
            return estructura;
        }

        public Estructura_Promocion_Viaje_Lista obtenerPromocionDeViajes(int pk_origen, int pk_destino, DateTime fecha)
        {
            Estructura_Promocion_Viaje_Lista estructura = new Estructura_Promocion_Viaje_Lista();
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(sqlconexion))
                {
                    string squery = string.Format("  select Pk_Promocion_viaje, format (FechaValido,'dd/MM/yyyy') fecha_valida, Precio, Descripcion, rutaImagen,Cupos, o.Nombre + ' - ' + d.Nombre as ruta "+
                        "  from[Proyecto].[dbo].[Promocion_viaje] v inner join Proyecto.dbo.Origen o on(v.Fk_origen = o.PK_origen) "+
                        "  inner join Proyecto.dbo.Destino d on(v.Fk_destino = d.PK_destino) "+
                        "  where v.Fk_origen = {0} and Fk_destino = {1} and  CONVERT(date, FechaValido) >= CONVERT(date, @fecha )", pk_origen, pk_destino);
                    var param = new DynamicParameters();
                    param.Add("@fecha", fecha);
                    estructura.data = cn.Query<Promocion_viaje>(squery, param, null, true, 0, System.Data.CommandType.Text).ToList();
                }

                estructura.codigo = "OK";
                estructura.mensaje = "OK";
            }
            catch (Exception ex)
            {
                estructura.codigo = "400";
                estructura.mensaje = ex.Message;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            if (estructura.data == null)
            {
                estructura.data = new List<Promocion_viaje>();
            }
            return estructura;
        }

        public Estructura_Promocion_FullDay_Lista obtenerPromocionDeFullDay(int pk_origen, int pk_destino, DateTime fecha)
        {
            Estructura_Promocion_FullDay_Lista estructura = new Estructura_Promocion_FullDay_Lista();
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(sqlconexion))
                {
                    string squery = string.Format("    select Pk_promocionFullDay, format (FechaValido,'dd/MM/yyyy') fecha_valida, Precio, Descripcion, rutaImagen,Cupos, o.Nombre + ' - ' + d.Nombre as ruta ,tipoPromocion "+
                        "    from [Proyecto].[dbo].[Promocion_FullDay] v inner join Proyecto.dbo.Origen o on(v.Fk_origen = o.PK_origen) "+
                        "    inner join Proyecto.dbo.Destino d on(v.Fk_destino = d.PK_destino) " +
                        "    where v.Fk_origen = {0} and Fk_destino = {1} and  CONVERT(date, FechaValido) >= CONVERT(date, @fecha) ", pk_origen, pk_destino);
                    var param = new DynamicParameters();
                    param.Add("@fecha", fecha);
                    estructura.data = cn.Query<Promocion_FullDay>(squery, param, null, true, 0, System.Data.CommandType.Text).ToList();
                }

                estructura.codigo = "OK";
                estructura.mensaje = "OK";
            }
            catch (Exception ex)
            {
                estructura.codigo = "400";
                estructura.mensaje = ex.Message;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            if (estructura.data == null)
            {
                estructura.data = new List<Promocion_FullDay>();
            }
            return estructura;
        }

        public Estructura_Promocion_Viaje obtenerPromocionDeViaje(int Pk_Promocion_viaje)
        {
            Estructura_Promocion_Viaje estructura = new Estructura_Promocion_Viaje();
            SqlConnection cn = null;
            try
            {
                using (cn = new SqlConnection(sqlconexion))
                {
                    string squery = string.Format("  select Pk_Promocion_viaje, format (FechaValido,'dd/MM/yyyy') fecha_valida, Precio, Descripcion, rutaImagen,Cupos, o.Nombre + ' - ' + d.Nombre as ruta " +
                        "  from[Proyecto].[dbo].[Promocion_viaje] v inner join Proyecto.dbo.Origen o on(v.Fk_origen = o.PK_origen) " +
                        "  inner join Proyecto.dbo.Destino d on(v.Fk_destino = d.PK_destino) " +
                        "  where v.Pk_Promocion_viaje = {0} ", Pk_Promocion_viaje);
                    
                    estructura.data = cn.QueryFirst<Promocion_viaje>(squery, null, null, 0, System.Data.CommandType.Text);
                }

                estructura.codigo = "OK";
                estructura.mensaje = "OK";
            }
            catch (Exception ex)
            {
                estructura.codigo = "400";
                estructura.mensaje = ex.Message;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }
            if (estructura.data == null)
            {
                estructura.data = new Promocion_viaje();
            }
            return estructura;
        }
    }
}
