using CFMS.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CFMS.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        int total = 0;
        //string[] quty;
        //string[] cartid;
        int p = 0;


        public ActionResult RegItem(string id, string nm)
        {
            if (id != null && nm != null)
            {
                HttpCookie Memid = new HttpCookie("MemId", id);
                Response.Cookies.Add(Memid);
                HttpCookie Memname = new HttpCookie("Memname", nm);
                Response.Cookies.Add(Memname);

            }
            return View();
        }

        public ActionResult AddregItem()
        {
          
            return View();
        }

        [HttpPost]
        public JsonResult AdddItem(ItemMasert i, HttpPostedFileBase img,string id)
        {
            string k = "";
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
                        //if (Request.Cookies["Memid"] != null && Request.Cookies["Memname"] != null)
                        //{
                        //    i.rid = Convert.ToInt32(Request.Cookies["Memid"].Value.Trim().ToString());
                        //    //i.stussubcrop = "SubmitCrop";
                        //    i.uid = Convert.ToInt32(Session["UID"].ToString());
                        //    i.stuspay = "Pending";
                        //}
                         if (Session["UID"] != null && Session["UTYPE"].ToString() == "NewGroup")
                        {
                            i.rid = Convert.ToInt32(Session["UID"].ToString());
                            i.uid = Convert.ToInt32(Session["UID"].ToString());
                            i.stussubcrop = "Pending";
                            i.stusshow = 1;
                            
                            i.stuspay = "Pending";

                            int s = Convert.ToInt32(Session["UID"].ToString());
                          
                            var K = con.ItemMaserts.Where(x => x.stusclu == 1 && x.uid == s && x.cpid == i.cpid).ToList();

                            foreach(var abb in K)
                            {
                                abb.stusclu = 2;
                                con.SaveChanges();
                            }


                        }
                        else if (Session["UID"] != null)
                        {
                            i.rid = Convert.ToInt32(Session["UID"].ToString());
                            i.uid = Convert.ToInt32(Session["UID"].ToString());
                            i.stussubcrop = "Pending";
                            i.stuscomp = 1;
                            i.stuspay = "Pending";
                        }



                        con.ItemMaserts.Add(i);
                       //if (Request.Cookies["Memid"] != null && Request.Cookies["Memname"] != null)
                       // {
                       //     //  i.rid = Convert.ToInt32(Request.Cookies["Memid"].Value.Trim().ToString());

                       //     //var c = new HttpCookie("Memid");
                       //     //c.Expires = DateTime.Now.AddDays(-1);
                       //     //Response.Cookies.Add(c);
                       //     //var b = new HttpCookie("Memname");
                       //     //b.Expires = DateTime.Now.AddDays(-1);
                       //     //Response.Cookies.Add(b);
                       //     k = "success";
                       //     con.SaveChanges();
                       //     return Json(k, JsonRequestBehavior.AllowGet);
                       // }


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


        [HttpPost]
        public JsonResult AddCart(string id,string ab)
        {
            string k;

            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(id);
                int b = Convert.ToInt32(ab);
                var j = con.carts.Where(x=>x.Imid==a && x.stucart==1).Count();
                if (j != 1)
                {
                    Cart c = new Cart();
                    c.Imid = a;
                    c.rid = Convert.ToInt32(Session["UID"].ToString());
                    c.stucart = 1;
                    c.varidCart = b;
                    con.carts.Add(c);
                    con.SaveChanges();
                    int l = Convert.ToInt32(Session["UID"].ToString());
                    int noti = con.carts.Where(x => x.stucart == 1 && x.rid==l).Count();
                    Session["noti"] = noti;
                }
                

                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);

        }


        public ActionResult CartShow()
        {
            Session["noti"] = null;
            return View();
        }

        public JsonResult GetCartDetails()
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(Session["UID"].ToString());
                var innerJoin =( from e in con.carts
                                join d in con.ItemMaserts on e.Imid equals d.Imid join m in con.crop on d.cpid equals m.cpid join s in con.Varity on e.varidCart equals s.varid
                                where (e.stucart == 1 && e.rid == a) select new { e.stucart,
                                    e.rid,
                                    m.cpname,
                                    d.price,
                                    d.qty,
                                    e.Imid,
                                    e.qt,
                                    e.amtot,
                                    d.img,
                                    e.cid,
                                    s.verity,
                                     e.varidCart
                                }).ToList();
                var inner = (from e in con.carts
                                join d in con.ItemMaserts on e.Imid equals d.Imid
                                join m in con.crop on d.cpid equals m.cpid                               
                                where (e.stucart == 1 && e.rid == a && e.varidCart==0)
                                select new
                                {
                                    e.stucart,
                                    e.rid,
                                    m.cpname,
                                    d.price,
                                    d.qty,
                                    e.Imid,
                                    e.qt,
                                    e.amtot,
                                    d.img,
                                    e.cid,   
                                    e.varidCart
                                }).ToList();
                List<object> aa = new List<object>();

                //    

                foreach (var s in innerJoin)
                {
                    aa.Add(s);

                }

                foreach (var ss in inner)
                {
                    aa.Add(ss);

                }



                return Json(aa, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);

        }
        //add item incremnt in textbox
        [HttpPost]
        public JsonResult CartItemIncre(string id)
        {
            DataCon con = new DataCon();
            int a = Convert.ToInt32(id);
            var k = con.carts.Find(a);
            var l = con.ItemMaserts.Find(k.Imid);
            if(k.qt==0)
            {
                k.qt = 1;
                k.amtot = k.qt * l.price;
                k.pric = l.price;
            }
            else
            {
                p = k.qt;
                p++;
                k.qt = p;
                k.amtot = k.qt * l.price;
            }
            con.SaveChanges();


            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CartItemDecre(string id)
        {
            DataCon con = new DataCon();
            int a = Convert.ToInt32(id);
            var k = con.carts.Find(a);
            var l = con.ItemMaserts.Find(k.Imid);
            if (k.qt == 0)
            {
                k.qt = 1;
                k.amtot = k.qt * l.price;
                k.pric = l.price;
            }
            else
            {
                p = k.qt;              
                k.qt = p - 1;
                k.amtot = k.qt * l.price;
            }
            con.SaveChanges();


            return Json(JsonRequestBehavior.AllowGet);
        }


        public JsonResult CartRemove(string id)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(Session["UID"].ToString());
                int b = Convert.ToInt32(id);
                var m = con.carts.Find(b);
                con.carts.Remove(m);
                con.SaveChanges();


                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);

        }

        //to confrim order
        [HttpPost]
        public JsonResult CorfrimOrder(string qt, string id)
        {
            string res="";
          try
            {
               DataCon con = new DataCon();
                int a = Convert.ToInt32(Session["UID"].ToString());
                var o = con.carts.Where(x => x.stucart == 1 && x.rid == a).ToList();
             
               
                for (int i = 0; i < o.Count; i++)
                {
                    var ss=   o.ElementAt(i);
                  //cart ststus update
                    ss.stucart = 2;

                    //item master
                    var itm = con.ItemMaserts.Find(ss.Imid);

                    var tot = itm.price * ss.qt;

                    var qtyntiy = itm.qty - ss.qt;

                    itm.qty = qtyntiy;
                    int pay = 0;
                    if (itm.stuspay.ToString() == "Pending")
                    {
                        itm.stuspay = "0";
                    }
                    else
                    {
                        pay = Convert.ToInt32(itm.stuspay);
                    }

                    pay = pay + Convert.ToInt32(tot);
                    itm.stuspay = pay.ToString();
                    //sell master
                    var x = new Sell_Master();
                    x.cid = ss.cid;
           
                    x.date = DateTime.UtcNow.Date;
                    x.Imid = ss.Imid;
                    x.rid = ss.rid;
                    x.Quty = ss.qt;
                    x.varidsell = ss.varidCart;
                    x.price = itm.price;
                    x.amut = tot;

                    UpdateModel(itm);         
                  

                    con.sell.Add(x);

                    con.SaveChanges();
                    Session["bill"] += x.sid.ToString() + ",";
                    res = "Your Order is confrim. ! Thank you";


                }

                

            }
           catch (Exception )
            {
                res= "Connection problem please try agin.";
            }
           
            return Json(res, JsonRequestBehavior.AllowGet);
                       
        }

        public ActionResult PrintBill()
        {
            if (Session["bill"] != null)
            {
                string d = Session["bill"].ToString();



                string[] aa = d.Split(',');

                int[] nums = new int[aa.Length];

                for (int i = 0; i < aa.Length; i++)
                {
                    if (aa[i] != "")
                    {
                        nums[i] = Convert.ToInt32(aa[i]);
                    }
                }

                var idList = nums;




                MemoryStream ms = new MemoryStream();

                Document doc = new Document(iTextSharp.text.PageSize.A4, 40, 40, 40, 40);
                PdfWriter pw = PdfWriter.GetInstance(doc, ms);

                PdfPTable table = new PdfPTable(6);




                //table in some print from database
                Paragraph vv = new Paragraph("Co-Operative Farming  Management System");

                vv.Alignment = Font.BOLD;
                vv.SpacingAfter = 5f;

                string path = ControllerContext.HttpContext.Server.MapPath("~/img/Capture.JPG");
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
                image.ScaleToFit(80f, 40f);
                image.Alignment = Element.ALIGN_CENTER;


                Paragraph vv1 = new Paragraph("Order Date :" + DateTime.UtcNow.Date.ToString("dd/MM/yyyy"));
             
                vv1.SpacingAfter = 5;
                vv1.Alignment = Font.BOLD;


                vv1.Alignment = Element.ALIGN_RIGHT;
                doc.Open();


                DataCon con = new DataCon();
                int a = Convert.ToInt32(Session["UID"].ToString());
                string dd = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");

                var k = con.sell.Where(x => x.rid == a &&  idList.Contains(x.sid)).ToList();

                PdfPCell cell = new PdfPCell(new Phrase(" Order Bill "));

                var w = con.Reg.Find(a);
                cell.Colspan = 6;
                cell.HorizontalAlignment = Font.BOLD;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;


                

                PdfPCell vv11 = new PdfPCell(new Phrase("Name :" + w.name + "\n" + "Group Name :" + w.gname));
                vv11.Colspan = 6;
                vv11.HorizontalAlignment = Element.ALIGN_LEFT;





                table.AddCell(cell);
                table.AddCell(vv11);

                table.AddCell("Id");
                table.AddCell("Product Name");
                table.AddCell("Varity Name");
                table.AddCell("Quantity");
                table.AddCell("Price");
                table.AddCell("Amount");

                int s = 1;

                foreach (var it in k)
                {
                    var item = con.ItemMaserts.Find(it.Imid);
                    table.AddCell(s.ToString());

                    var aaa = con.crop.Find(item.cpid);
                    table.AddCell(aaa.cpname); 

                    if (item.varkeyItem == 0)
                    {
                        table.AddCell("Fertilizer");
                    }
                    else
                    {
                        var ss = con.Varity.Find(item.varkeyItem);
                        table.AddCell(ss.verity.ToString());
                    }

             

                    table.AddCell(it.Quty.ToString());
                    table.AddCell(it.price.ToString());
                    table.AddCell((it.Quty * it.price).ToString());
                    int rr = it.Quty * it.price;
                    total += rr;
                    s++;
                }

                Session["bill"] = null;

                PdfPCell cell1 = new PdfPCell(new Phrase(" Total : " + total.ToString()));

                //var w = con.Reg.Find(a);
                cell1.Colspan = 6;
                cell1.HorizontalAlignment = Font.BOLD;
                cell1.HorizontalAlignment = Element.ALIGN_RIGHT;

                table.AddCell(cell1);
                doc.Add(image);
                doc.Add(vv);

                doc.Add(vv1);
                doc.Add(vv11);
                doc.Add(table);
                doc.Close();
                byte[] byt = ms.ToArray();
                ms = new MemoryStream();
                ms.Write(byt, 0, byt.Length);
                ms.Position = 0;
                return new FileStreamResult(ms, "application/pdf");
               
                //   return Json(new FileStreamResult(ms, "application/pdf"), JsonRequestBehavior.AllowGet);
            }else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Please order first !');</script>");
            }
            
        }
        

        //sell item and requrment and submit crop details Edite for secretary and company
        public ActionResult SellitemEidt(int id ,string a)
        { 
            HttpCookie Sellid = new HttpCookie("Sellid", id.ToString());
            Response.Cookies.Add(Sellid);
            DataCon con = new DataCon();
            string l = Request.Cookies["Sellid"].Value.ToString();
            int M = Convert.ToInt32(l);
            //  var res = (from a in con.crop join d in con.ItemMaserts on a.cpid equals d.cpid where (a.deletst == 0 && d.Imid==aa && d.stusdele == 0) select new { a.cpname, a.ItypeId, a.cpid, d.img, d.qty, d.price, d.uid, d.rid }).Distinct().ToList();


            var res = con.ItemMaserts.Find(M);
            var k = con.crop.Find(res.cpid);
            Session["cpnmm"] = k.cpname;

            return View();
        }

        public JsonResult SellItemEditeUser()
        {
            DataCon con = new DataCon();
            string l = Request.Cookies["Sellid"].Value.ToString();
            int a = Convert.ToInt32(l);
          //  var res = (from a in con.crop join d in con.ItemMaserts on a.cpid equals d.cpid where (a.deletst == 0 && d.Imid==aa && d.stusdele == 0) select new { a.cpname, a.ItypeId, a.cpid, d.img, d.qty, d.price, d.uid, d.rid }).Distinct().ToList();


           var res = con.ItemMaserts.Find(a);
            var k = con.crop.Find(res.cpid);
            //Session["cpnmm"] = k.cpname;
            return Json(res, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EditeSellItem(ItemMasert a, HttpPostedFileBase img)
        {
            string k = "";
            try
            {
                if (a != null)
                {
                    DataCon con = new DataCon();
                    var m = con.ItemMaserts.Find(a.Imid);
                    if (img != null)
                    {
                        string FileName = Path.GetFileName(img.FileName);
                        string path = Path.Combine(Server.MapPath("~/UploadedItem"), FileName);
                        img.SaveAs(path);
                        m.img = FileName.ToString();
                    }
                   // m.name = a.name;
                    m.price = a.price;
                    m.qty = a.qty;

                    k = "Your Item Edit successfully";
                    // UpdateModel(m);
                    con.SaveChanges();
                }
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletItem(string id)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                int b = Convert.ToInt32(id);
                var m = con.ItemMaserts.Find(b);
                m.stusdele = 1;
                UpdateModel(m);
                con.SaveChanges();
                k = "Your Item Delete Successfully .!";
                return Json(k,JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);
        }

        //cart button show or not
        public JsonResult GetCartButton()
        {
            int noti = 0;
            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(Session["UID"].ToString());
                 noti = con.carts.Where(x => x.stucart == 1 && x.rid == a).Count();
                return Json(noti, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                noti=0;
            }
            return Json(noti, JsonRequestBehavior.AllowGet);
        }
        //order cancel 
        public JsonResult OredrCencel()
        {
            string noti = "";
            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(Session["UID"].ToString());
                var k = con.carts.Where(x => x.rid == a && x.stucart == 1).ToList();
                k.ForEach(x => x.stucart=0);
                con.SaveChanges();
                noti = "Your Order Is Cancel successfully .!";
                return Json(noti, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                noti = "Connection problem please try agin.";
            }
            return Json(noti, JsonRequestBehavior.AllowGet);
        }

        //add crop varity

        public ActionResult CropVerity()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddCropVerity(VarityCrop v)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                con.Varity.Add(v);
               
                con.SaveChanges();
                k = "Your Varity Added Successfully .!";
                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);
        }
        //get crop verity added by admin
        public JsonResult GetVerity()
        {

            try
            {
                DataCon con = new DataCon();
                //   var K = con.Varity.Where(x => x.deletst == 0).ToList();
                //   var K = (from a in con.crop join d in con.ItemMaserts on a.cpid equals d.cpid where (a.deletst == 0 && d.uid == s && d.stusclu == 1 && d.stusdele == 0 && d.stussubcrop == "SubmitCrop") select new { a.cpname, a.ItypeId, a.cpid, d.img, d.qty, d.price, d.uid, d.rid }).Distinct().ToList();
                var k = (from a in con.Varity join d in con.ItemTypes on a.ItypeId equals d.ItypeId join c in con.crop on a.cpid equals c.cpid where (a.deletst == 0 ) select new { a.verity, c.cpname, a.varid,a.ItypeId,a.cpid,a.deletst}).ToList();


                return Json(k, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public JsonResult VarityEditePost(VarityCrop m)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(m.varid);
                var ab = con.Varity.Find(a);
                UpdateModel(ab);
                con.SaveChanges();
                k = "Your Data Successfully Updated";
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteCropVar(string id)
        {
            string k;

            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(id);
                var ab = con.Varity.Find(a);
                ab.deletst = 1;
                //// UpdateModel(ab);
                //var mn = con.Varity.Where(x => x.varid==a).ToList();

                con.SaveChanges();
                k = "Your Crop Varity is Delete Successfully";
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        //allredy exit or not crop name 
        public JsonResult CropVarityExit(string nam, string id)
        {
            string s = "";
            try
            {
                DataCon con = new DataCon();
                int b = Convert.ToInt32(id);
                int a = con.Varity.Where(x =>x.cpid==b && x.verity==nam && x.deletst==0).Count();
                if (a == 1)
                {
                    s = "exit";
                }
            }
            catch (Exception)
            {
                s = "Connection problem please try agin.";
            }
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //get ajax throg varity name on crop name 
        public JsonResult CropVarGet(string id)
        {
            string s = "";
            try
            {
                DataCon con = new DataCon();
                int b = Convert.ToInt32(id);
                var k = con.Varity.Where(x => x.cpid == b && x.deletst == 0).Distinct().ToList();
                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                s = "Connection problem please try agin.";
                return Json(s, JsonRequestBehavior.AllowGet);
            }
           
        }

        [HttpPost]
        //get ajax throg varity name on crop name 
        public JsonResult CropVarGetNew(string id)
        {
            string s = "";
            try
            {
                DataCon con = new DataCon();
                int b = Convert.ToInt32(id);
                //   var k = con.Varity.Where(x => x.cpid == b && x.deletst == 0 ).Distinct().ToList();

                var k = (from a in con.Varity join d in con.ItemMaserts on a.varid equals d.varkeyItem  where (a.deletst == 0 && d.stusdele==0 && d.stusclu==1 && a.cpid==b) select new { a.verity, a.varid, a.ItypeId, a.cpid, a.deletst }).Distinct().ToList();

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                s = "Connection problem please try agin.";
                return Json(s, JsonRequestBehavior.AllowGet);
            }

        }

    }
      
}