using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace AttendanceManagementSystem.Controllers
{
    [Authorize]
    public class ChangePasswordController : Controller
    {
        // GET: ChangePassword
        AttendanceManagementSystem.AppCode.Global obj = new AttendanceManagementSystem.AppCode.Global();
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangePassword(string oldpassword, string newpassword)
        {
            string query = "USP_CHANGEPASSWORD '" + oldpassword + "','" + newpassword + "','"+ Session["usercode"] .ToString()+ "'";
            int i = obj.ExecuteNonQuery(query);
            string str;
            if (i > 0)
            {
                str = "Password Changed Successfully";
            }
            else
            {
                str = "Old Password does not match";
            }
            string JSONresult = JsonConvert.SerializeObject(str);
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
    }
}