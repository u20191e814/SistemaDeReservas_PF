using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReservaEntidades;
using SistemaDeReservas_PF.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SistemaDeReservas_PF.Controllers
{
    public class ProyectoController : Controller
    {
        // GET: Proyecto
        public ActionResult Reserva()
        {
            return View();
        }
        public ActionResult Promocion()
        {

            return View();
        }
        public ActionResult Viaje()
        {
            return View();
        }
        public ActionResult FullDayDetalles(int Pk_FullDayPromocion=0 ,int Pk_ViajesProgramados=0 )
        {
            Estructura_Promocion_FullDay full = new Estructura_Promocion_FullDay();
            if (Pk_FullDayPromocion !=0 )
            {
                
                using (ServiceFullDay.ServiceSoap_FullDayClient c = new ServiceFullDay.ServiceSoap_FullDayClient())
                {
                    full = c.obtenerPromocionFullDayPorId(Pk_FullDayPromocion);
                }
                               
                Session["Pk_FullDayPromocion"] = Pk_FullDayPromocion;
                Session["pk_FullDayPromagado"] = 0;
                double precio = 0;
                if (full.data != null)
                {
                    precio = full.data.Precio;
                    if (!string.IsNullOrEmpty(full.data.tipoPromocion))
                    {
                        full.data.tipoPromocion = " - " + full.data.tipoPromocion;
                    }
                    
                }
                Session["PrecioUnitario"] = precio;
                
            }
            else
            {
                Session["Pk_FullDayPromocion"] = 0;
                ViajesProgramados_FullDay vProg = new ViajesProgramados_FullDay();
                using (ServiceFullDay.ServiceSoap_FullDayClient cs = new ServiceFullDay.ServiceSoap_FullDayClient())
                {
                    Estructura_ViajesProgramados_FullDay rr = cs.obtenerViajeProgramadosFullDay(Pk_ViajesProgramados);
                    vProg = rr.data;
                }
                Promocion_FullDay pp = new Promocion_FullDay();
                pp.Cupos = vProg.cupos;
                pp.ruta = vProg.Ruta;
                pp.Precio = vProg.Precio;
                pp.fecha_valida = "";
                pp.rutaImagen = vProg.rutaImagen;
                pp.Descripcion = "Información de fullday ";
                Session["PrecioUnitario"] = pp.Precio;
                full.data=pp;
                Session["pk_FullDayPromagado"] = vProg.Pk_ViajesProgramados_FullDay;
            }
            ViewBag.Promocion_FullDay = full.data;
            DateTime fecha = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(full.data.fecha_valida))
            {
                string[] separar = full.data.fecha_valida.Split('/');
                fecha = new DateTime(int.Parse(separar[2]), int.Parse(separar[1]), int.Parse(separar[0]));
            }
            Session["FechaMaximaValida"] = fecha.ToString("yyyy-MM-dd");
            
            return View();
        }

        public ActionResult MensajeFinal(string mensajeError, int pK_ReservaGenerado)
        {
            ViewBag.mensaje = mensajeError;
            ViewBag.Pk_Reserva = pK_ReservaGenerado;
            return View();
        }
        public ActionResult PromocionViaje(int pk_promocionViaje)
        {
           
            ViewBag.Pk_PromocionViaje = pk_promocionViaje;
            return View();
        }
        public ActionResult ReservarFullDay()
        {
            if (Session["Pk_FullDayPromocion"]==null  || Session["pk_FullDayPromagado"]==null)
            {
                return RedirectToAction("FullDay");
            }
            int Pk_FullDayPromocion = int.Parse(Session["Pk_FullDayPromocion"].ToString());
            int pk_FullDayPromagado = int.Parse(Session["pk_FullDayPromagado"].ToString());
            ViewBag.FechaMaxima = Session["FechaMaximaValida"].ToString();
            if (Pk_FullDayPromocion != 0 || pk_FullDayPromagado!=0)
            {
                if (Pk_FullDayPromocion != 0)
                {
                    ViewBag.FechaViaje = true;
                }
                else
                {
                    ViewBag.FechaViaje = false;
                }
                return View();

            }

            else
            {
                return RedirectToAction("FullDay");
            }

        }

       
        [HttpPost]
        public ActionResult ReservarFullDay(ReservaFullDay newReservaFullDay)
        {
            
            double preciounitario= (double)Session["PrecioUnitario"];
            newReservaFullDay.precioUnitario = preciounitario;
            int Pk_FullDayPromocion = (int)Session["Pk_FullDayPromocion"];
            string mensajeError = string.Empty;

            int pk_origen =(int)Session["pk_origen"]  ;
            int  pk_destino= (int)Session["pk_destino"]  ;
           
            newReservaFullDay.Fk_Origen = pk_origen;
            newReservaFullDay.Fk_Destino = pk_destino;
            if (newReservaFullDay.Fecha_Viaje==DateTime.MinValue)
            {
                newReservaFullDay.Fecha_Viaje = (DateTime)Session["Fecha_Consultado"];
            }
           
            newReservaFullDay.Fecha_Generado = DateTime.Now;
            newReservaFullDay.Estado = "Registrado";
            newReservaFullDay.precioTotal = preciounitario * newReservaFullDay.cantidad;
            int pK_ReservaGenerado = 0;
            ViewBag.FechaMaxima = Session["FechaMaximaValida"].ToString();
            if (Pk_FullDayPromocion != 0)
            {
                ViewBag.FechaViaje = true;
            }
            else
            {
                ViewBag.FechaViaje = false;
            }
            if (newReservaFullDay.Dni_cliente.Length!=8)
            {
                ViewBag.ErrorMensaje = "Deben ser 8 numeros del dni";
                return View();
            }
            if (newReservaFullDay.Telefono.Length!=9)
            {
                ViewBag.ErrorMensaje = "Deben ser 9 numeros del celular";
                return View();
            }
            if (!newReservaFullDay.Telefono.StartsWith("9"))
            {
                ViewBag.ErrorMensaje = "El numero de celular es invalido debe iniciar con 9";
                return View();
            }

            bool validaciondni = false;//validacion DNI
            if (Settings.Default.ActivarValidacionDni)
            {
                using (HttpClient cliente = new HttpClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    string ruta = string.Format("https://apiperu.dev/api/dni/{0}?api_token={1}", newReservaFullDay.Dni_cliente, Settings.Default.TokenConsultarDni);

                    var tarea = cliente.GetAsync(ruta);
                    tarea.Wait();

                    var tarea2 = tarea.Result.Content.ReadAsStringAsync();
                    tarea2.Wait();

                    string texto = tarea2.Result;
                    DNI dni= Newtonsoft.Json.JsonConvert.DeserializeObject<DNI>(texto);
                    if (dni!=null && dni.success )
                    {
                        validaciondni = true;
                    }
                    else
                    {
                        ViewBag.ErrorMensaje = "DNI: "+ dni.message;
                    }

                }
            }
            if (validaciondni==false)
            {
                return View();
            }



            using (ServiceFullDay.ServiceSoap_FullDayClient c = new ServiceFullDay.ServiceSoap_FullDayClient())
            {
                 
               Estructura_Post_int insertado= c.RegistrarReservaFullDay(newReservaFullDay);
                if (insertado.codigo!="OK")
                {
                    mensajeError = insertado.mensaje;
                }
                
                pK_ReservaGenerado = insertado.data;
                if (pK_ReservaGenerado > 0)
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = Settings.Default.IpRabbitServer,
                        Port = 5672,
                        VirtualHost = "/",
                        UserName = "admin",
                        Password = "admin"
                    };
                    using (var conection = factory.CreateConnection())
                    {
                        using (var channel = conection.CreateModel())
                        {
                            channel.QueueDeclare("MensajeDeReserva", false, false, false, null);
                            var body = Encoding.UTF8.GetBytes("Su reserva se ha realizado correctamente con N° " + pK_ReservaGenerado);
                            channel.BasicPublish("", "MensajeDeReserva", null, body);

                        }
                    }
                }
                //Envio de mensaje de prueba 


            }
            return RedirectToAction("MensajeFinal",new { mensajeError,pK_ReservaGenerado });

            //return MensajeFinal(mensajeError, pK_ReservaGenerado);
        }
        public ActionResult FullDay()
        {
            return View();

        }
        public ActionResult Consultas()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getDestino(string Pk_origen)
        {
            List<SelectListItem> listaDestino = new List<SelectListItem>();
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.Timeout = new TimeSpan(0, 0, 30);
                    var taa1 = cliente.GetAsync(Settings.Default.UrlServiceRest + string.Format("api/GetDestino?pk_origen={0}", Pk_origen));
                    taa1.Wait();

                    if (taa1.Result.IsSuccessStatusCode)
                    {
                        var t2 = taa1.Result.Content.ReadAsStringAsync();
                        t2.Wait();

                        if (!string.IsNullOrEmpty(t2.Result))
                        {
                            Estructura_Destino_Lista estructura = JsonConvert.DeserializeObject<Estructura_Destino_Lista>(t2.Result);
                            List<Destino> Listagates = estructura.data;
                            if (Listagates != null)
                            {
                                listaDestino = Listagates.ConvertAll(x =>
                                {
                                    return new SelectListItem() { Text = x.Nombre, Value = x.Pk_Destino.ToString(), Selected = false };
                                });
                                listaDestino[0].Selected = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            //try
            //{

            //    Estructura_Destino_Lista estructura = new ReservasDb.Db("Server=database-test.c94qouacmsdm.us-east-2.rds.amazonaws.com; User Id=admin; Password=123456789;").obtenerDestino(int.Parse(Pk_origen));
            //    List<Destino> Listagates = estructura.data;
            //    if (Listagates != null)
            //    {
            //        listaDestino = Listagates.ConvertAll(x =>
            //        {
            //            return new SelectListItem() { Text = x.Nombre, Value = x.Pk_Destino.ToString(), Selected = false };
            //        });
            //        listaDestino[0].Selected = true;
            //    }

            //}
            //catch (Exception ex)
            //{
            //}
            return Json(listaDestino, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getOrigen()
        {
            List<SelectListItem> listaOrigen = new List<SelectListItem>();
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.Timeout = new TimeSpan(0, 0, 30);
                    var taa1 = cliente.GetAsync(Settings.Default.UrlServiceRest + string.Format("api/GetOrigen"));
                    taa1.Wait();

                    if (taa1.Result.IsSuccessStatusCode)
                    {
                        var t2 = taa1.Result.Content.ReadAsStringAsync();
                        t2.Wait();

                        if (!string.IsNullOrEmpty(t2.Result))
                        {
                            ReservaEntidades.Estructura_Origen_Lista estructura = JsonConvert.DeserializeObject<Estructura_Origen_Lista>(t2.Result);
                            List<Origen> Lista = estructura.data;
                            if (Lista != null)
                            {
                                listaOrigen = Lista.ConvertAll(x =>
                                {
                                    return new SelectListItem() { Text = x.Nombre, Value = x.Pk_Origen.ToString(), Selected = false };
                                });
                                listaOrigen[0].Selected = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            
            //try
            //{

            //    ReservaEntidades.Estructura_Origen_Lista estructura = new ReservasDb.Db("Server=database-test.c94qouacmsdm.us-east-2.rds.amazonaws.com; User Id=admin; Password=123456789;").obtenerOrigen();
            //    List<Origen> Lista = estructura.data;
            //    if (Lista != null)
            //    {
            //        listaOrigen = Lista.ConvertAll(x =>
            //        {
            //            return new SelectListItem() { Text = x.Nombre, Value = x.Pk_Origen.ToString(), Selected = false };
            //        });
            //        listaOrigen[0].Selected = true;
            //    }

            //}
            //catch (Exception ex)
            //{

            //}

            return Json(listaOrigen, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getPromocionViajes(int pk_origen, int pk_destino, DateTime fecha)
        {
             
            List<Promocion_viaje> listaviajes = new List<Promocion_viaje>();
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.Timeout = new TimeSpan(0, 0, 30);
                    var taa1 = cliente.GetAsync(Settings.Default.UrlServiceRest + string.Format("api/GetPromocionViajes?pk_origen={0}&pk_destino={1}&fecha={2}", pk_origen, pk_destino, fecha.ToString("yyyy-MM-dd")));
                    taa1.Wait();

                    if (taa1.Result.IsSuccessStatusCode)
                    {
                        var t2 = taa1.Result.Content.ReadAsStringAsync();
                        t2.Wait();

                        if (!string.IsNullOrEmpty(t2.Result))
                        {
                            ReservaEntidades.Estructura_Promocion_Viaje_Lista estructura = JsonConvert.DeserializeObject<Estructura_Promocion_Viaje_Lista>(t2.Result);
                            listaviajes = estructura.data;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            //try
            //{

            //    ReservaEntidades.Estructura_Promocion_Viaje_Lista estructura = new ReservasDb.Db("Server=database-test.c94qouacmsdm.us-east-2.rds.amazonaws.com; User Id=admin; Password=123456789;").obtenerPromocionDeViajes(pk_origen, pk_destino, fecha);
            //    listaviajes = estructura.data;
            //}
            //catch (Exception ex)
            //{
            //}

            return Json(listaviajes, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getPromocionFullDay(int pk_origen, int pk_destino, DateTime fecha)
        {

            Session["pk_origen"] = pk_origen;
            Session["pk_destino"] = pk_destino;
            Session["fecha_consultado"] = fecha;
            List<Promocion_FullDay> listaviajes = new List<Promocion_FullDay>();


            try
            {
                using (ServiceFullDay.ServiceSoap_FullDayClient sfull = new ServiceFullDay.ServiceSoap_FullDayClient())
                {
                     
                    Estructura_Promocion_FullDay_Lista pfullLista = sfull.obtenerPromocionFullDay(pk_origen, pk_destino, fecha);
                    if (pfullLista!=null )
                    {
                        listaviajes = pfullLista.data;
                    }
                }
                

            }
            catch (Exception ex)
            {
            }

            return Json(listaviajes, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getViajesProgramadosFullDay(int pk_origen, int pk_destino, DateTime fecha)
        {
            List<ViajesProgramados_FullDay> Lista = new List<ViajesProgramados_FullDay>();
            try
            {
                using (ServiceFullDay.ServiceSoap_FullDayClient servicio = new ServiceFullDay.ServiceSoap_FullDayClient())
                {
                    Estructura_ViajesProgramados_FullDay_Lista Estructura_FullDay = servicio.obtenerViajesProgramadosFullDay(pk_origen, pk_destino, fecha);
                    if (Estructura_FullDay!=null)
                    {
                        Lista = Estructura_FullDay.data;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Json(Lista, JsonRequestBehavior.AllowGet);
        }
        
    
    [AcceptVerbs(HttpVerbs.Get)]
    public JsonResult getReservasPorDni(string Dni)
        {
            List<ConsultaReservas> lista = new List<ConsultaReservas>();
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.Timeout = new TimeSpan(0, 0, 30);
                    var taa1 = cliente.GetAsync(Settings.Default.UrlServiceRest + string.Format("api/GetReservasPorDni?Dni={0}", Dni));
                    taa1.Wait();

                    if (taa1.Result.IsSuccessStatusCode)
                    {
                        var t2 = taa1.Result.Content.ReadAsStringAsync();
                        t2.Wait();

                        if (!string.IsNullOrEmpty(t2.Result))
                        {
                            ReservaEntidades.Estructura_ConsultaReservas estructura = JsonConvert.DeserializeObject<Estructura_ConsultaReservas>(t2.Result);
                            lista = estructura.data;
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }



            //try
            //{

            //    ReservaEntidades.Estructura_ConsultaReservas estructura = new ReservasDb.Db("Server=database-test.c94qouacmsdm.us-east-2.rds.amazonaws.com; User Id=admin; Password=123456789;").GetReservasPorDni(Dni);
            //    lista = estructura.data as List<ConsultaReservas>;

            //}
            //catch (Exception ex)
            //{

            //}

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}