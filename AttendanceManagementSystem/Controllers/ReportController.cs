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
    public class ReportController : Controller
    {
        // GET: Report
        AttendanceManagementSystem.AppCode.Global obj = new AttendanceManagementSystem.AppCode.Global();
        public ActionResult Report()
        {
            return View();
        }
        public JsonResult DropdownBindWorkType()
        {
            string query = "select WORKCODE,WORKDESCRYPTION FROM WORKTYPEMASTER";
            DataTable dt = new DataTable();
            dt = obj.GetData(query);
            string JSONresult = JsonConvert.SerializeObject(dt);
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DropdownBindUser()
        {
            string query = "IF('" + Session["usertype"].ToString() + "'='0') BEGIN select USERCODE,USERNAME + '--' + USERCODE AS USERNAME from usermaster END ELSE BEGIN select USERCODE,USERNAME + '--' + USERCODE AS USERNAME from usermaster WHERE USERCODE = '" + Session["usercode"].ToString() + "' END";
            DataTable dt = new DataTable();
            dt = obj.GetData(query);
            string JSONresult = JsonConvert.SerializeObject(dt);
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BtnSubmit_Click(string fromdate, string todate,string userid, string work)
        {
            string query = "USP_GET_ATTENDANCEREPORT '" + fromdate + "','" + todate + "','" + userid + "','" + work + "','"+ Session["usertype"].ToString() + "'";
            DataTable dt = new DataTable();
            dt = obj.GetData(query);
            string JSONresult = JsonConvert.SerializeObject(dt);
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
    }
}