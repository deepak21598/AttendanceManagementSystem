using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace AttendanceManagementSystem.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        AttendanceManagementSystem.AppCode.Global obj = new AttendanceManagementSystem.AppCode.Global();
        public ActionResult Profile()
        {
            return View();
        }
        public JsonResult GetProfile()
        {
            string query = "SELECT USERCODE,USERNAME,MOBILE,EMAIL,UT.DESCRYPTION FROM USERMASTER UM LEFT JOIN USERTYPE UT ON UM.USERTYPE=UT.USERTYPE WHERE UM.USERCODE='"+ Session["usercode"].ToString() + "'";
            DataTable dt = new DataTable();
            dt = obj.GetData(query);
            string JSONresult = JsonConvert.SerializeObject(dt);
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
    }
}