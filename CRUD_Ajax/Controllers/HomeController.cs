using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUD_Ajax.Models;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace CRUD_Ajax.Controllers
{
    public class HomeController : Controller
    {

        EmployeeDB empDB = new EmployeeDB();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar()
        {
            return Json(empDB.ListAll(),JsonRequestBehavior.AllowGet);
        }

        public JsonResult Insertar(Employee empleado)
        {
            return Json(empDB.Add(empleado), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Actualizar(Employee empleado)
        {
            return Json(empDB.Update(empleado), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerPorID(int ID)
        {
            var Employee = empDB.ListAll().Find(x => x.EmployeeID.Equals(ID));

            return Json(Employee, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(int id)
        {
            return Json(empDB.Delete(id), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Cargar(HttpPostedFileBase archivo)
        {

            if (archivo == null || archivo.ContentLength == 0)
            {
                ViewBag.Error = "Seleccione un archivo";

                return View("Index");
            }
            else {
                if (archivo.FileName.EndsWith("xls") || archivo.FileName.EndsWith("xlsx"))
                {
                    String ruta = Server.MapPath("~/Content/" + Path.GetFileName(archivo.FileName));

                    if (System.IO.File.Exists(ruta)) {
                        System.IO.File.Delete(ruta);
                    }

                    archivo.SaveAs(ruta);

                    ViewBag.Error = "Correcto";

                    return View("Index");
                }
                else {
                    ViewBag.Error = "Cargue un archivo excel";

                    return View("Index");
                }
            }
        }

    }
}