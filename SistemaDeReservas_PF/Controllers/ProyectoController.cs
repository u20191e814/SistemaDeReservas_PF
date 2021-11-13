using Newtonsoft.Json;
using ReservaEntidades;
using SistemaDeReservas_PF.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public ActionResult FullDayDetalles(int Pk_FullDayPromocion)
        {
            Estructura_Promocion_FullDay full = new Estructura_Promocion_FullDay();
            using (ServiceFullDay.ServiceSoap_FullDayClient c = new ServiceFullDay.ServiceSoap_FullDayClient())
            {
                full=c.obtenerPromocionFullDayPorId(Pk_FullDayPromocion);
            }
            
            ViewBag.Promocion_FullDay = full.data;
            return View();
        }
        public ActionResult PromocionViaje(int pk_promocionViaje)
        {
            ViewBag.Pk_PromocionViaje = pk_promocionViaje;
            return View();
        }
        public ActionResult TestVista()
        {
            return View();
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
            //try
            //{
            //    using (HttpClient cliente = new HttpClient())
            //    {
            //        cliente.Timeout = new TimeSpan(0, 0, 30);
            //        var taa1 = cliente.GetAsync(Settings.Default.UrlServiceRest + string.Format("api/GetDestino?pk_origen={0}", Pk_origen));
            //        taa1.Wait();

            //        if (taa1.Result.IsSuccessStatusCode)
            //        {
            //            var t2 = taa1.Result.Content.ReadAsStringAsync();
            //            t2.Wait();

            //            if (!string.IsNullOrEmpty(t2.Result))
            //            {
            //                Estructura_Destino_Lista  estructura = JsonConvert.DeserializeObject<Estructura_Destino_Lista>(t2.Result);
            //                List<Destino> Listagates = estructura.data;
            //                if (Listagates != null)
            //                {
            //                    listaDestino = Listagates.ConvertAll(x =>
            //                    {
            //                        return new SelectListItem() { Text = x.Nombre, Value = x.Pk_Destino.ToString(), Selected = false };
            //                    });
            //                    listaDestino[0].Selected = true;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}



            try
            {
                
                Estructura_Destino_Lista estructura = new ReservasDb.Db("Server=database-test.c94qouacmsdm.us-east-2.rds.amazonaws.com; User Id=admin; Password=123456789;").obtenerDestino(int.Parse(Pk_origen));
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
            catch (Exception ex)
            {

            }
            return Json(listaDestino, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getOrigen()
        {
            List<SelectListItem> listaOrigen = new List<SelectListItem>();
            //try
            //{
            //    using (HttpClient cliente = new HttpClient())
            //    {
            //        cliente.Timeout = new TimeSpan(0,0,30);
            //        var taa1 = cliente.GetAsync(Settings.Default.UrlServiceRest + string.Format("api/GetOrigen"));
            //        taa1.Wait();

            //        if (taa1.Result.IsSuccessStatusCode)
            //        {
            //            var t2 = taa1.Result.Content.ReadAsStringAsync();
            //            t2.Wait();

            //            if (!string.IsNullOrEmpty(t2.Result))
            //            {
            //                ReservaEntidades.Estructura_Origen_Lista  estructura = JsonConvert.DeserializeObject<Estructura_Origen_Lista>(t2.Result);
            //                List<Origen> Lista = estructura.data;
            //                if (Lista != null)
            //                {
            //                    listaOrigen = Lista.ConvertAll(x =>
            //                    {
            //                        return new SelectListItem() { Text = x.Nombre, Value = x.Pk_Origen.ToString(), Selected = false };
            //                    });
            //                    listaOrigen[0].Selected = true;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

            try
            {

                ReservaEntidades.Estructura_Origen_Lista estructura = new ReservasDb.Db("Server=database-test.c94qouacmsdm.us-east-2.rds.amazonaws.com; User Id=admin; Password=123456789;").obtenerOrigen();
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
            catch (Exception ex)
            {
            }

            return Json(listaOrigen, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getPromocionViajes(int pk_origen, int pk_destino, DateTime fecha)
        {
             
            List<Promocion_viaje> listaviajes = new List<Promocion_viaje>();
                    

            try
            {

                ReservaEntidades.Estructura_Promocion_Viaje_Lista estructura = new ReservasDb.Db("Server=database-test.c94qouacmsdm.us-east-2.rds.amazonaws.com; User Id=admin; Password=123456789;").obtenerPromocionDeViajes(pk_origen, pk_destino, fecha);
                listaviajes = estructura.data;

            }
            catch (Exception ex)
            {
            }

            return Json(listaviajes, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getPromocionFullDay(int pk_origen, int pk_destino, DateTime fecha)
        {

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

    }
}