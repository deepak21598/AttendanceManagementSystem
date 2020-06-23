using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.Security;

namespace AttendanceManagementSystem.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string inputUser, string inputPassword,string checkRemember)
        {
            AttendanceManagementSystem.AppCode.Global obj = new AttendanceManagementSystem.AppCode.Global();
            //string query = "USP_GETLOGINDETAILS '" + inputUser + "','" + inputPassword + "' ";
            string query = "select * from USERMASTER WHERE USERCODE='"+ inputUser + "' and PASSWORD='"+ inputPassword + "' and isblocked=0";
            DataTable dt = new DataTable();
            dt = obj.GetData(query);
            if (dt.Rows.Count == 0)
            {
                return View();
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Session["usercode"] = dr["usercode"];
                    Session["usertype"] = dr["usertype"];
                }
                string query1 = "SELECT MENU,CONTROLLER,ACTION FROM ROLUMENUMASTER WHERE USERTYPE="+ Session["usertype"] + "";
                DataTable dt1 = new DataTable();
                dt1 = obj.GetData(query1);
                Session["menu"] = dt1;
                FormsAuthentication.SetAuthCookie(inputUser, false);              
                return RedirectToAction("Home", "Dashboard");
            }
            
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}