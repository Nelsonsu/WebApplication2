
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using WebApplication2.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Homepage()
        {        
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Testing()
        {
            string username = Request.Form["emailaddressinput"];
            string password = Request.Form["passwordinput"];
            ViewBag.Email = username;
            ViewBag.Pass = password;
            UIADbEntities entities = new UIADbEntities();
            return View(from Customer in entities.Customers.Take(10) select Customer);
        }
        public ActionResult ConfirmBooking(string flightid1, string flightid2)
        {
            List<Flightmodel> flights = new List<Flightmodel>();
            string CS = ConfigurationManager.ConnectionStrings["DDAC"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Execute GetSelectedFlight '" + flightid1 + "', '" + flightid2 + "'", con);
                using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        string flightid = sqlDataReader["flightID"].ToString();
                        string fromtimes = sqlDataReader["times"].ToString();
                        string reachtimes = sqlDataReader["reachtime"].ToString();
                        string durationHours = sqlDataReader["durationHour"].ToString();
                        string durationMins = sqlDataReader["durationMin"].ToString();
                        string froms = sqlDataReader["source"].ToString();
                        string tos = sqlDataReader["destination"].ToString();
                        string price = sqlDataReader["fareAdult"].ToString();
                        string aircaft = sqlDataReader["aircID"].ToString();
                        string logo = sqlDataReader["airlLogo"].ToString();
                        string duration = durationHours + "H " + durationMins + "Min";
                        flights.Add(new Flightmodel() { FlightID = flightid, FromTime = fromtimes, ToTime = reachtimes, Duration = duration, FromLocation = froms, ToLocation = tos, Price = price, AircraftID = aircaft, AirlineLogo = logo });
                    }
                    ViewBag.flight1 = flightid1;
                    ViewBag.flight2 = flightid2;
                }
                con.Close();
            }
            return View(flights);
        }

        public JsonResult Loginaction(string Email, string Password)
        {
            UIADbEntities db = new UIADbEntities();
            var cus = db.GetCustomerLogin(Email, Password);
            var result = cus.FirstOrDefault();
            return Json(new { id=result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Registeraction(Customermodel model)
        {
            if(model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("Password", "The confirm password does not match");
                ModelState.AddModelError("ConfirmPassword", "The password does not match");
            }

            if (ModelState.IsValid)
            {
                UIADbEntities db = new UIADbEntities();
                string firstname = model.FirstName;
                string lastname = model.LastName;
                string password = model.Password;
                string confirmpassword = model.ConfirmPassword;
                string email = model.Email;
                var newcus = db.RegisterAccount(firstname, lastname, password, email);
                var result = newcus.FirstOrDefault();
                if(result.Equals("Repeated Email"))
                {
                    ModelState.AddModelError("Email", "The email has been registered by another account!");
                }
                else
                {
                    return View("Login");
                }
            }
            return View("Register");
        }

        public ActionResult Flightresult(Flightinput flightinput)
        {
            string fromloca = flightinput.FromWhere;
            string toloca = flightinput.ToWhere;
            string dates = flightinput.DepartDate;
            string returndates = flightinput.ReturnDate;
            if (returndates == null)
            {
                returndates = "noreturn";
            }
            List<Flightmodel> flights = new List<Flightmodel>();
            string CS = ConfigurationManager.ConnectionStrings["DDAC"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Execute GetFlight '"+dates+"', '"+fromloca+"','"+toloca+"'", con);
                using(SqlDataReader sqlDataReader = cmd.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        string flightid = sqlDataReader["flightID"].ToString();
                        string fromtimes = sqlDataReader["times"].ToString();
                        string reachtimes = sqlDataReader["reachtime"].ToString();
                        string durationHours = sqlDataReader["durationHour"].ToString();
                        string durationMins = sqlDataReader["durationMin"].ToString();
                        string froms = sqlDataReader["source"].ToString();
                        string tos = sqlDataReader["destination"].ToString();
                        string price = sqlDataReader["fareAdult"].ToString();
                        string aircaft = sqlDataReader["aircID"].ToString();
                        string logo = sqlDataReader["airlLogo"].ToString();
                        string duration = durationHours + "H " + durationMins + "Min";
                        flights.Add(new Flightmodel() { FlightID = flightid,FromTime = fromtimes, ToTime = reachtimes, Duration = duration, FromLocation = froms, ToLocation = tos, Price = price, AircraftID = aircaft, AirlineLogo=logo });
                    }
                    ViewBag.Fromwhr = fromloca;
                    ViewBag.Towhr = toloca;
                    ViewBag.Dates = dates;
                    ViewBag.Returns = returndates;
                }
                con.Close();
            }
            return View(flights);
        }

        public ActionResult BackFlightresult(string id, string fromplace, string toplace, string returntime)
        {
            List<Flightmodel> flights = new List<Flightmodel>();
            if (returntime == "noreturn")
            {
                return RedirectToAction("ConfirmBooking", new { flightid1 = id, flightid2 = returntime});
            }
            else
            {
                string CS = ConfigurationManager.ConnectionStrings["DDAC"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Execute GetFlight '" + returntime + "', '" + fromplace + "','" + toplace + "'", con);
                    using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            string flightid = sqlDataReader["flightID"].ToString();
                            string fromtimes = sqlDataReader["times"].ToString();
                            string reachtimes = sqlDataReader["reachtime"].ToString();
                            string durationHours = sqlDataReader["durationHour"].ToString();
                            string durationMins = sqlDataReader["durationMin"].ToString();
                            string froms = sqlDataReader["source"].ToString();
                            string tos = sqlDataReader["destination"].ToString();
                            string price = sqlDataReader["fareAdult"].ToString();
                            string aircaft = sqlDataReader["aircID"].ToString();
                            string logo = sqlDataReader["airlLogo"].ToString();
                            string duration = durationHours + "H " + durationMins + "Min";
                            flights.Add(new Flightmodel() { FlightID = flightid, FromTime = fromtimes, ToTime = reachtimes, Duration = duration, FromLocation = froms, ToLocation = tos, Price = price, AircraftID = aircaft, AirlineLogo = logo });
                        }
                        ViewBag.Fromwhr = fromplace;
                        ViewBag.Towhr = toplace;
                        ViewBag.Dates = returntime;
                        ViewBag.FirstFlight = id;
                    }
                    con.Close();
                }
            }
            return View(flights);
        }

        public ActionResult Booking(string Flight1, string Flight2)
        {
            ViewBag.Flight1 = Flight1;
            ViewBag.Flight2 = Flight2;
            return View();
        }

        public ActionResult InsertBook(FlightPassenger passenger)
        {
            string fname = passenger.Firstname;
            string lname = passenger.Lastname;
            string passport = passenger.Passport;
            int age = passenger.Age;
            string gender = passenger.Gender;
            string email = passenger.Email;
            string flightid1 = passenger.Flightsid1;
            string flightid2 = passenger.Flightid2;
            string CS = ConfigurationManager.ConnectionStrings["DDAC"].ConnectionString;
            if (flightid2 == "noreturn")
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Execute BookFlightOneway '" + fname + "','" + lname + "','" + passport + "','" + age + "','" + gender + "','" + email + "','" + flightid1 + "'", con);
                    using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                    {

                    }
                    con.Close();
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Execute BookFlightMultiway '" + fname + "','" + lname + "','" + passport + "','" + age + "','" + gender + "','" + email + "','" + flightid1 + "','" + flightid2 + "'", con);
                    using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                    {

                    }
                    con.Close();
                }
            }
           
                
            return View("Homepage");
        }
        
        public ActionResult ViewMyFlight(string email)
        {
            List<Flightmodel> flights = new List<Flightmodel>();
            string CS = ConfigurationManager.ConnectionStrings["DDAC"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Execute GetBookedFlight '"+email+"'", con);
                using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        string flightid = sqlDataReader["flightID"].ToString();
                        string fromtimes = sqlDataReader["times"].ToString();
                        string reachtimes = sqlDataReader["reachtime"].ToString();
                        string durationHours = sqlDataReader["durationHour"].ToString();
                        string durationMins = sqlDataReader["durationMin"].ToString();
                        string froms = sqlDataReader["source"].ToString();
                        string tos = sqlDataReader["destination"].ToString();
                        string price = sqlDataReader["fareAdult"].ToString();
                        string aircaft = sqlDataReader["aircID"].ToString();
                        string logo = sqlDataReader["airlLogo"].ToString();
                        string duration = durationHours + "H " + durationMins + "Min";
                        flights.Add(new Flightmodel() { FlightID = flightid, FromTime = fromtimes, ToTime = reachtimes, Duration = duration, FromLocation = froms, ToLocation = tos, Price = price, AircraftID = aircaft, AirlineLogo = logo });
                    }
                }
                con.Close();
            }
            return View(flights);
        }
    }
}