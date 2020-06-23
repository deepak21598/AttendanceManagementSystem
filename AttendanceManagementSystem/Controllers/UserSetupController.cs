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
    [CustomFilter("0")]
    public class UserSetupController : Controller
    {
        // GET: UserSetup
        AttendanceManagementSystem.AppCode.Global obj = new AttendanceManagementSystem.AppCode.Global();
        public ActionResult UserSetup()
        {
            return View();
        }

        public JsonResult BtnSubmit_Click(string mode,string usercode, string username, string mobile, string email, string password, string usertype)
        {
            string query = "CREATE_UPDATE_USER '"+mode+ "','" + usercode + "','" + username + "','" + mobile + "','" + email + "','" + password + "','" + usertype + "'";
            int i = obj.ExecuteNonQuery(query);
            string str;
            if (i > 0 && mode=="1")
            {
                str = "User Updated Successfully";
            }
            else if (i > 0 && mode == "0")
            {
                str = "User Created Successfully";
            }
            else
            {
                str = "User Already Exist";
            }
            var v = "[{'check':'" + str + "'}]";
            string JSONresult = JsonConvert.SerializeObject(str);
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindGrid()
        {          
            string query = "GET_USERDETAILS";
            DataTable dt = new DataTable();
            dt = obj.GetData(query);
            string JSONresult = JsonConvert.SerializeObject(dt);
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DropdownBindUserType()
        {
            string query = "select USERTYPE,DESCRYPTION from USERTYPE";
            DataTable dt = new DataTable();
            dt = obj.GetData(query);
            string JSONresult = JsonConvert.SerializeObject(dt);
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteBlockUser(string check,string usercode)
        {
            string query = "DeleteBlockUser '" + check + "','" + usercode + "'";
            int i = obj.ExecuteNonQuery(query);
            if (i > 0)
            {

            }
            return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);
        }
    }
}