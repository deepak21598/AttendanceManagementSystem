using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using AttendanceManagementSystem.AppCode;

namespace AttendanceManagementSystem.Controllers
{
    [Authorize]
    [CustomFilter("1")]
    public class AttendanceController : Controller
    {
        // GET: Attendance
        AttendanceManagementSystem.AppCode.Global obj = new AttendanceManagementSystem.AppCode.Global();
        public ActionResult Attendance()
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
            string query = "select USERCODE,USERNAME+'--'+USERCODE AS USERNAME from usermaster WHERE USERCODE='"+ Session["usercode"].ToString() + "'";
            DataTable dt = new DataTable();
            dt = obj.GetData(query);
            string JSONresult = JsonConvert.SerializeObject(dt);
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckAttendance()
        {
            string query = "select isnull(convert(nvarchar(5),CHECKINDATETIME,108),'') CHECKINDATETIME,isnull(convert(nvarchar(5),CHECKOUTDATETIME,108),'') CHECKOUTDATETIME,WORKTYPE from ATTENDANCEENTRY where cast(CHECKINDATETIME as date)= cast(getdate() as date) and USERCODE ='" + Session["usercode"].ToString() + "'";
            DataTable dt = new DataTable();
            dt = obj.GetData(query);
            string JSONresult = JsonConvert.SerializeObject(dt);
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BtnSubmit_Click(string checkindate,string checkintime,string checkoutdate,string checkouttime,string userid,string work)
        {
            string query = "USSP_INS_ATTENDANCEENTRY '" + checkindate + "','" + checkintime + "','" + checkoutdate + "','" + checkouttime + "','" + userid + "','" + work + "'";
            int i = obj.ExecuteNonQuery(query);
            if (i > 0)
            {

            }
            return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);
        }
    }
}