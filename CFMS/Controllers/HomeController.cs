using CFMS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Data.Entity;

using System.Web.Mvc;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace CFMS.Controllers
{
    public class HomeController : Controller
    {
        //GET: Home
        //public JsonResult GetCP()
        //{
        //    StringBuilder randomText = new StringBuilder();
        //    string alphabets = "012345679";
        //    Random r = new Random();
        //    for (int j = 0; j <= 3; j++)
        //    {
        //        randomText.Append(alphabets[r.Next(alphabets.Length)]);
        //    }
        //    Session["CAPTCHA"] = randomText.ToString();
        //    return Json(RedirectToAction("GetCaptchaImage") ,JsonRequestBehavior.AllowGet);
        //}

        //public FileResult GetCaptchaImage()
        //{
        //    StringBuilder randomText = new StringBuilder();
        //    string alphabets = "012345679";
        //    Random r = new Random();
        //    for (int j = 0; j <= 3; j++)
        //    {
        //        randomText.Append(alphabets[r.Next(alphabets.Length)]);
        //    }
        //    Session["CAPTCHA"] = randomText.ToString();

        //    string text = Session["CAPTCHA"].ToString();


        //    Image img = new Bitmap(1, 1);
        //    Graphics drawing = Graphics.FromImage(img);

        //    Font font = new Font("Arial", 15);

        //    SizeF textSize = drawing.MeasureString(text, font);


        //    img.Dispose();
        //    drawing.Dispose();

        //    img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
        //    drawing = Graphics.FromImage(img);

        //    Color backColor = Color.SeaShell;
        //    Color textColor = Color.Red;

        //    drawing.Clear(backColor);


        //    Brush textBrush = new SolidBrush(textColor);

        //    drawing.DrawString(text, font, textBrush, 20, 10);

        //    drawing.Save();

        //    font.Dispose();
        //    textBrush.Dispose();
        //    drawing.Dispose();

        //    MemoryStream ms = new MemoryStream();
        //    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //    img.Dispose();

        //    return File(ms.ToArray(), "image/png");
        //}
        public ActionResult about()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registraionall()
        {
            return View();
        }
        //each user can this login page to some other page redirection.
        public ActionResult Login()
        {
          
            return View();
        }
        
        public JsonResult ValidLogin(Registration k)
        {
            try
            {

                DataCon con = new DataCon();
                var a = con.Reg.Where(x => x.monum == k.monum && x.pass == k.pass && (x.adgroupsatus == 1 || x.adcompnystatus == 1 || x.gamemsatus == 1)).FirstOrDefault();
                if (a != null)
                {
                    Session["UID"] = a.rid;
                    Session["UTYPE"] = a.rtype;
                    Session["UNAME"] = a.name;
                    int noti = con.carts.Where(x => x.stucart == 1 && x.rid == a.rid).Count();
                    Session["noti"] = noti;
                    if (Session["UTYPE"].ToString()== "Member")
                    {
                        int l = Convert.ToInt32(a.gname);
                        var o = con.Reg.Find(l);
                        Session["GNAME"] = o.gname;
                    }
                    else
                    {
                        Session["GNAME"] = a.gname;
                    }

                    // return Json(a, JsonRequestBehavior.AllowGet);
                    return Json(RedirectToAction("Index"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string l ="worng";
                    //  return Json(l, JsonRequestBehavior.AllowGet);
                    return Json(RedirectToAction("Index"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                var a = "Connection problem please try again ...!";
                return Json(a, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult  Logout()
        {
            if (Session["UID"] != null || Session["UNAME"] != null || Session["UTYPE"] != null || Session["GNAME"] != null)
            {

                Session.Clear();
            }
            
            return RedirectToAction("Index");

                
            
        }
        public JsonResult GetGruopName()
        {

            try
            {
                DataCon con = new DataCon();
                var K = con.Reg.Where(x => x.gpnamestust == 1).Select(x => new {x.rid ,x.gname }).Distinct().OrderBy(x=>x.gname).ToList();

                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public JsonResult Register(Registration r)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                if(r.rtype=="Member")
                {
                    var mm = con.Reg.Where(x => x.gname == r.gname).FirstOrDefault();
                    r.gname = mm.rid.ToString();
                }            
              
                con.Reg.Add(r);
                con.SaveChanges();
                k ="Success";
            }
            catch(Exception)
            {
                k = "File";
            }
            
            return Json(k,JsonRequestBehavior.AllowGet);
        }

        //dispaly for Registaion of Company and New Group
        public ActionResult DisplayReg()
        {
            return View();
        }
        //display for new Group request accept or not

        public JsonResult GetNewGP()
        {
            try
            {
                DataCon con = new DataCon();
                var K = con.Reg.Where(x => x.adgroupsatus == 0 && x.gpnamestust == 0 && x.rtype== "NewGroup").ToList();

                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }
           
        }

        //display for new compny request accept or not
        public JsonResult GetNewCP()
        {
            try
            {
                DataCon con = new DataCon();
                var K = con.Reg.Where(x => x.adcompnystatus == 0 && x.rtype == "Company").ToList();

                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }
        }

        //Accept requset for new group created and send message from system only see Adminostetor
       [HttpPost]
        public JsonResult AcceptNewGroup(string rid)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                var a = con.Reg.Find(Convert.ToInt32(rid));

                k = "Requset is Accepted Suucessfully and Send E-mail to User By System";
                //System.Threading.Thread.Sleep(7000);

                if (a.rtype == "NewGroup")
                {
                    //new group allow code

                    a.adgroupsatus = 1;
                    a.gpnamestust = 1;

                   // email send By system Auto matic by authenticate each user
                    WebMail.SmtpServer = "smtp.gmail.com";

                    WebMail.SmtpPort = 587;
                    WebMail.SmtpUseDefaultCredentials = true;

                    WebMail.EnableSsl = true;

                    WebMail.UserName = "rathvasandip147@student.aau.in";
                    WebMail.Password = "8141849471";


                    WebMail.From = "rathvasandip147@student.aau.in";

                    WebMail.Send(to: a.email, subject: "Your Requset is Appruval For Created New Group", body: "You Can Valid And Created New Group of Farmer Admin  ", isBodyHtml: true);

                }
                else
                {
                    a.adcompnystatus = 1;
                    WebMail.SmtpServer = "smtp.gmail.com";

                    WebMail.SmtpPort = 587;
                    WebMail.SmtpUseDefaultCredentials = true;

                    WebMail.EnableSsl = true;

                    WebMail.UserName = "rathvasandip147@student.aau.in";
                    WebMail.Password = "8141849471";


                    WebMail.From = "rathvasandip147@student.aau.in";

                    WebMail.Send(to: a.email, subject: "Your Requset is Appruval", body: "You Can Valid User and seling and buying Agriculture Poduct Company ", isBodyHtml: true);

                }
                UpdateModel(a);
                con.SaveChanges();
                return Json(k,JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                k = "Connection problem please taile agin.";
                return Json(k, JsonRequestBehavior.AllowGet);

            }
        }

        //reject new group or company in system

        public JsonResult RejectNewCmp(string rid)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                var a = con.Reg.Find(Convert.ToInt32(rid));
                con.Reg.Remove(a);
                con.SaveChanges();
                k = "Requset is Rejected successfully";
            }
            catch
            {
                k = "Connection problem please try agin.";
                
            }
            return Json(k,JsonRequestBehavior.AllowGet);
        }

        //add item type for adminostatre
        public ActionResult ItemTypeAdd()
        {
            return View();
        }

        public JsonResult AddItemType(ItemType i)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                con.ItemTypes.Add(i);
                con.SaveChanges();
                k = "success";
            }
            catch(Exception)
            {
                k = "fail";

            }
            return Json(k,JsonRequestBehavior.AllowGet);
        }


        //display  ItemType for Adminostore

        public JsonResult GetItemType()
        {
            try
            {
                DataCon con = new DataCon();
                var K = con.ItemTypes.Where(x=>x.deletst==0).ToList();

                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }

        }
        //edite fro iTemType
        [HttpPost]
        public JsonResult EditeIype(ItemType m)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(m.ItypeId);
                var ab = con.ItemTypes.Find(a);
                UpdateModel(ab);
                con.SaveChanges();
                k = "Your Data Successfully Update";
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult DeleteType(string id)
        {
            string k;

            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(id);
                var ab = con.ItemTypes.Find(a);
                 ab.deletst = 1;
                
                var mn = con.ItemMaserts.Where(x => x.ItypeId == a).ToList();
                mn.ForEach(x => x.stusdele = 1);
                con.SaveChanges();
                k = "Your Item is Delete Successfully";
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);
        }

        //get item type for add item by user(secretery and company)
        public JsonResult GetItemTypeReg()
        {
            string k;

            try
            {
                DataCon con = new DataCon();
             
                var ab = con.ItemTypes.Where(x=>x.deletst==0).ToList();

                return Json(ab, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddItem(ItemMasert i,HttpPostedFileBase img)
        {
            string k="";
            if (ModelState.IsValid)
            {
               try
                {
                    DataCon con = new DataCon();
                  //  if(Session["UID"]!=null && img.ContentLength > 0)
                    {
                        string FileName = Path.GetFileName(img.FileName);
                        string path = Path.Combine(Server.MapPath("~/UploadedItem"), FileName);
                        img.SaveAs(path);
                        string oo = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");


                            
                        i.date = DateTime.ParseExact(oo, "dd/MM/yyyy", CultureInfo.InvariantCulture);    
                        i.img = FileName.Trim().ToString();
                        if (Request.Cookies["Memid"] != null && Request.Cookies["Memname"] != null)
                        {
                            i.rid =Convert.ToInt32(Request.Cookies["Memid"].Value.Trim().ToString());
                            //i.stussubcrop = "SubmitCrop";
                            i.uid= Convert.ToInt32(Session["UID"].ToString());
                            i.stuspay = "Pending";
                            i.stusclu = 1;
                        }
                        else if(Session["UID"]!=null && Session["UTYPE"].ToString() =="NewGroup" )
                        {
                            i.rid =Convert.ToInt32( Session["UID"].ToString());
                            i.uid = Convert.ToInt32(Session["UID"].ToString());
                            i.stussubcrop = "Pending";
                            i.stusshow = 1;
                            i.stuspay = "Pending";
                        }else if (Session["UID"] != null)
                        {
                            i.rid = Convert.ToInt32(Session["UID"].ToString());
                            i.uid = Convert.ToInt32(Session["UID"].ToString());
                            i.stussubcrop = "Pending";
                            i.stuscomp = 1;
                            i.stusclu = 2;
                            i.stuspay = "Pending";
                        }


                       
                        con.ItemMaserts.Add(i);
                        if (Request.Cookies["Memid"] != null && Request.Cookies["Memname"] != null)
                        {
                            //  i.rid = Convert.ToInt32(Request.Cookies["Memid"].Value.Trim().ToString());
                            
                              var c = new HttpCookie("Memid");
                            c.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(c);
                            var b = new HttpCookie("Memname");
                            b.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(b);
                            k = "success";
                            con.SaveChanges();
                            return Json(k, JsonRequestBehavior.AllowGet);
                        }


                         con.SaveChanges();
                        k = "Your Item added successfully";
                    }


                    return Json(k, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    k = "Connection problem please try agin.";
                }
            }
            return Json(k, JsonRequestBehavior.AllowGet);
        }

        //profile update 
        public ActionResult ProfileUser()
        {
            return View();
        }

        public JsonResult GetProfile()
        {
            DataCon con = new DataCon();
            int a= Convert.ToInt32(Session["UID"].ToString()); 
            var k = con.Reg.Find(a);
            return Json(k, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProUpdate(Registration r)
        {
            string n = "";
            try
            {
                if (r != null)
                {
                    DataCon con = new DataCon();
                    var l = con.Reg.Find(r.rid);
                    l.name = r.name;
                    l.gname = r.gname;
                    l.addr = r.addr;
                    l.monum = r.monum;
                    l.email = r.email;
                    if(r.rtype== "Member")
                    {
                        Session["UNAME"] = r.name;
                    }
                    else
                    {
                        Session["UNAME"] = r.name;
                        Session["GNAME"] = r.gname;
                    }
                    
                   


                    con.SaveChanges();
                    n = "Your Profile Update successfully !";
                }else
                {
                    n = "Please try agin !";
                }
            }
            catch (Exception)
            {
                n= "Connection problem please try agin."; 
            }

            return Json(n, JsonRequestBehavior.AllowGet);
        }

        //forgot password
        public ActionResult Forgotpass()
        {
            return View();
        }

        public JsonResult SendEmail(string em)
        {
            string s = "";
            try
            {
                if (em != null)
                {
                    DataCon con = new DataCon();
                    int a = con.Reg.Where(x => x.email == em && x.stusdele == 0).Count();
                    if (a == 1)
                    {
                        var k = con.Reg.Where(x => x.email == em && x.stusdele == 0).SingleOrDefault();
                        WebMail.SmtpServer = "smtp.gmail.com";

                        WebMail.SmtpPort = 587;
                        WebMail.SmtpUseDefaultCredentials = true;

                        WebMail.EnableSsl = true;

                        WebMail.UserName = "rathvasandip147@student.aau.in";
                        WebMail.Password = "8141849471";


                        WebMail.From = "rathvasandip147@student.aau.in";

                        WebMail.Send(to: k.email, subject: "Your password ", body: "You password is: " + k.pass, isBodyHtml: true);
                        s = "Your password is send to email successfully !";

                    }
                    else
                    {
                        s = "Your E-mail is not found please try agin !";
                    }
                }
            }
            catch (Exception)
            {
                s = "Connection problem please try agin.";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //allredy exit or not item type
        public JsonResult ItemTypExit(string nm)
        {
            string s = "";
            try
            {
                DataCon con = new DataCon();
                int a = con.ItemTypes.Where(x => x.type == nm).Count();
                if (a == 1)
                {
                    s = "suc";
                }
            }
            catch (Exception)
            {
                s = "Connection problem please try agin.";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //allredy group name exit or not
        [HttpPost]
        public JsonResult GroupNameExit(string gnm)
        {
            string s = "";
            try
            {
                DataCon con = new DataCon();
                int a = con.Reg.Where(x => x.gname == gnm).Count();
                if (a == 1)
                {
                    s = "suc";
                }
            }
            catch (Exception)
            {
                s = "Connection problem please try agin.";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }
        //allredy Email id exit or not
        [HttpPost]
        public JsonResult EmailExit(string em)
        {
            string s = "";
            try
            {
                DataCon con = new DataCon();
                int a = con.Reg.Where(x => x.email == em).Count();
                if (a == 1)
                {
                    s = "suc";
                }
            }
            catch (Exception)
            {
                s = "Connection problem please try agin.";
            }
            return Json(s, JsonRequestBehavior.AllowGet);

        }
        //allredy Mobile Number exit or not
        [HttpPost]
        public JsonResult MonoExit(string mo)
        {
            string s = "";
            try
            {
                DataCon con = new DataCon();
                int a = con.Reg.Where(x => x.monum == mo).Count();
                if (a == 1)
                {
                    s = "suc";
                }
            }
            catch (Exception)
            {
                s = "Connection problem please try agin.";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }


        //add crop for admin

        public ActionResult Addcrop()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddCropFer(AddCrop a)
        {
            string s = "";
            try
            {
                DataCon con = new DataCon();
              //  a.ItypeId = Convert.ToInt32(id);
                con.crop.Add(a);
                con.SaveChanges();
                s = "Your Crop is Added Successfully";
            }
            catch (Exception)
            {
                s = "Connection problem please try agin.";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //get crop details

        public JsonResult GetCrop()
        {
            try
            {
                DataCon con = new DataCon();
                var K = con.crop.Where(x => x.deletst == 0).ToList();

                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult EditeCrop(AddCrop m)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(m.cpid);
                var ab = con.crop.Find(a);
                UpdateModel(ab);
                con.SaveChanges();
                k = "Your Data Successfully Update";
            }
           catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteCrop(string id)
        {
            string k;

            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(id);
                var ab = con.crop.Find(a);
                ab.deletst = 1;
               // UpdateModel(ab);
                var mn = con.ItemMaserts.Where(x => x.cpid == a).ToList();
                mn.ForEach(x => x.stusdele = 1);
                con.SaveChanges();
                k = "Your Crop/Item is Delete Successfully";
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);
        }

        //allredy exit or not crop name 
        public JsonResult CropNameExit(string nm,string id)
        {
            string s = "";
            try
            {
                DataCon con = new DataCon();
                int b = Convert.ToInt32(id);
                int a = con.crop.Where(x => x.cpname == nm && x.ItypeId==b && x.deletst==0).Count();
                if (a == 1)
                {
                    s = "suc";
                }
            }
            catch (Exception)
            {
                s = "Connection problem please try agin.";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        //get crop name 
        public JsonResult GetCropName()
        {

            try
            {
                DataCon con = new DataCon();
              //  var K = (from a in con.crop join d in con.ItemMaserts on a.cpid equals d.cpid where (a.deletst == 0  && d.stusclu == 1 && d.stusdele == 0) select new { a.cpname, a.ItypeId, a.cpid, d.img, d.qty, d.price, d.uid, d.rid }).Distinct().ToList();

                   var K = con.crop.Where(x => x.deletst==0 ).Select(x => new { x.cpid, x.cpname, x.ItypeId }).Distinct().OrderBy(x => x.cpname).ToList();

                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult GetCropNameClu()
        {

            try
            {
                DataCon con = new DataCon();
                int s = Convert.ToInt32(Session["UID"].ToString());
                //var K = con.crop.Where(x => x.deletst == 0).Select(x => new { x.cpid, x.cpname, x.ItypeId }).Distinct().OrderBy(x => x.cpname).ToList();
            //    var K = (from a in con.crop join d in con.ItemMaserts on a.cpid equals d.cpid where (a.deletst == 0 && d.uid == s && d.stusclu == 1 && d.stusdele == 0 ) select   new  {  a.cpname, a.ItypeId, a.cpid, d.img, d.qty, d.price, d.uid, d.rid }).Distinct().ToList();
                  var K = (from a in con.crop join d in con.ItemMaserts on a.cpid equals d.cpid where (a.deletst == 0 && d.uid == s && d.stusclu == 1 && d.stusdele == 0 && d.stussubcrop== "SubmitCrop") select   new  {  a.cpname, a.ItypeId, a.cpid, d.img, d.qty, d.price, d.uid, d.rid }).Distinct().ToList();

                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }
        }
        //crop calculation
        public JsonResult SubCropClu(string id)
        {
            try
            {
                DataCon con = new DataCon();
                int s = Convert.ToInt32(Session["UID"].ToString());
                int ss = Convert.ToInt32(id);
                var K = con.ItemMaserts.Where(x => x.stusclu==1 && x.uid==s && x.varkeyItem==ss).Sum(x => x.qty);
               // var K = (from a in con.crop join d in con.ItemMaserts on a.cpid equals d.cpid where (a.deletst == 0 && d.uid == s && d.stusclu == 1 && d.stusdele == 0) select new { a.cpname, a.ItypeId, a.cpid, d.img, d.qty, d.price, d.uid, d.rid }).Distinct().ToList();
                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult SubCropCluId(string id)
        {
            try
            {
                DataCon con = new DataCon();
                int s = Convert.ToInt32(Session["UID"].ToString());
                int ss = Convert.ToInt32(id);
                var K = con.ItemMaserts.Where(x => x.stusclu == 1 && x.uid == s && x.varkeyItem == ss).ToList();
                // var K = (from a in con.crop join d in con.ItemMaserts on a.cpid equals d.cpid where (a.deletst == 0 && d.uid == s && d.stusclu == 1 && d.stusdele == 0) select new { a.cpname, a.ItypeId, a.cpid, d.img, d.qty, d.price, d.uid, d.rid }).Distinct().ToList();
                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult RemoveCok()
        {
            try
            {
                string[] myCookies = Request.Cookies.AllKeys;
                foreach (string cookie in myCookies)
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                }
                //var c = new HttpCookie("Memid");
                //c.Expires = DateTime.Now.AddDays(-1);
                //Response.Cookies.Add(c);
                //var b = new HttpCookie("Memname");
                //b.Expires = DateTime.Now.AddDays(-1);
                //Response.Cookies.Add(b);
            }
            catch (Exception)
            {

            }
            return Json(JsonRequestBehavior.AllowGet);
        }
    }

}