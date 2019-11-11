using CFMS.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CFMS.Controllers
{
    public class SecretaryController : Controller
    {
        // GET: FarmerMem for accept and reject
       
        public ActionResult ShowMemaber()
        {
            return View();
        }
        //get memebar list for specifice group
        public JsonResult GetMemaberSp()
        {
            try
            {
                DataCon con = new DataCon();
                string a = Session["UID"].ToString();
                var K = con.Reg.Where(x => x.gamemsatus==0 && x.gname==a && x.stusdele==0).ToList();

                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }

        }

        //memeber can delete
        [HttpPost]
        public JsonResult DeletMembar(string id)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                int b = Convert.ToInt32(id);
                var m = con.Reg.Find(b);
                m.stusdele = 1;
                UpdateModel(m);
                con.SaveChanges();
                k = "Your Membar is Delete Successfully .!";
                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);

        }

        
        //memeber can delete
        [HttpPost]
        public JsonResult RejectMembar(string id)
        {
            string k;
            try
            {
                DataCon con = new DataCon();
                int b = Convert.ToInt32(id);
                var m = con.Reg.Find(b);
                m.stusdele = 1;
                UpdateModel(m);
                con.SaveChanges();
                k = "Your Membar  Rejected Successfully .!";
                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                k = "Connection problem please try agin.";
            }
            return Json(k, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult AcceptMembar(string id)
        {
            string s;
            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(id);
                var k = con.Reg.Find(a);
                k.gamemsatus = 1;
                UpdateModel(k);
                con.SaveChanges();
                s = "Requset is accepted succesfully";
                return Json(s, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "Connection problems try agin";
                return Json(l, JsonRequestBehavior.AllowGet);

            }

        }
    //get id and name from group membar
        [HttpPost]
        public JsonResult GetMemabrDetails(string id)
        {
            try
            {
                DataCon con = new DataCon();
                string a = Session["UID"].ToString();
                int b = Convert.ToInt32(id);
                var K = con.Reg.Where(x => x.rid==b && x.gname == a && x.stusdele==0).ToList();
                if(K==null)
                {
                   var  n = "Data is not faund";
                    return Json(n, JsonRequestBehavior.AllowGet);
                }

                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }

        }

        //show all membar in specifice secreatry
        public ActionResult ShowMemabersp()
        {
            return View();
        }

        public JsonResult GetMemaberGroup()
        {
            try
            {
                DataCon con = new DataCon();
                string a = Session["UID"].ToString();
                var K = con.Reg.Where(x => x.gamemsatus == 1 && x.gname == a && x.stusdele==0).ToList();

                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }
        }
       //show add product by secretary of selling seed or other
       public ActionResult ShowSellingItem()
        {
            return View();
        }
    
        public JsonResult GetShowItem()
        {
            DataCon con = new DataCon();
            if (Session["UTYPE"].ToString() == "Company")
            {
                int a = Convert.ToInt32(Session["UID"].ToString());
                //int y = 0;
                //  var K = con.ItemMaserts.Where(x => x.rid == a && x.stusdele==0).ToList();

              // var K = (from n in con.crop join d in  con.Varity on n.cpid equals d.cpid join c in con.ItemMaserts on d.varid equals c.varkeyItem    where (n.deletst == 0 && c.rid == a && c.stusdele == 0 && d.deletst==0  )   select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, c.price, c.qty, c.rid, c.stuspay, c.uid, c.img, c.Imid, d.verity, c.varkeyItem}).ToList();
                var Kg = (from n in con.crop join d in  con.Varity on n.cpid equals d.cpid join c in con.ItemMaserts on d.varid equals c.varkeyItem  where (n.deletst == 0 && c.rid == a && c.stusdele == 0 && d.deletst==0  )   select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, c.price, c.qty, c.rid, c.stuspay, c.uid, c.img, c.Imid, d.verity, c.varkeyItem}).ToList();

               
                var mm = (from n in con.ItemMaserts join v in con.crop on n.cpid equals v.cpid  where (n.stusdele == 0 && n.varkeyItem == 0 && v.deletst == 0 ) select new   { n.rid, n.price, n.qty,  n.Imid, v.cpname, v.cpid,n.img,n.ItypeId }).Distinct().ToList();
             
                List<object> aa = new List<object>();
           
            //    

                foreach(var s in Kg)
                {
                    aa.Add(s);

                }

                foreach (var ss in mm)
                {
                    aa.Add(ss);

                }
              


                return Json(aa, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int a = Convert.ToInt32(Session["UID"].ToString());

                //   var K = con.ItemMaserts.Where(x => x.rid == a && x.stusshow == 1 && x.stusdele==0).ToList();
                var K = from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid  join c in con.Varity on d.varkeyItem equals c.varid    where (n.deletst == 0 && d.rid == a &&  d.stusshow == 1 && d.stusdele == 0 && c.deletst == 0 ) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid, c.verity };

                return Json(K, JsonRequestBehavior.AllowGet);
            }
            
        }

        //buy item redio button 
        public JsonResult GetShowItemBuy()
        {
            DataCon con = new DataCon();
            if (Session["UTYPE"].ToString() == "Company")
            {
                //int a = Convert.ToInt32(Session["UID"].ToString());

                 //var K = con.ItemMaserts.Where(x => x.stusshow ==1 && x.stusdele==0).ToList();

               //// var K = from e in con.carts
               //                 join d in con.ItemMaserts on e.Imid equals d.Imid
               //                 join m in con.crop on d.cpid equals m.cpid
               //                 where (e.stucart == 1 && e.rid == a)
               //                 select new
               //                 {
               //                     e.stucart,
               //                     e.rid,
               //                     m.cpname,
               //                     d.price,
               //                     d.qty,
               //                     e.Imid,
               //                     e.qt,
               //                     e.amtot,
               //                     d.img,
               //                     e.cid
               //                 };
                var K = from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid join c in con.Varity on d.cpid equals c.cpid  where (n.deletst == 0 && d.stusshow == 1 && d.stusdele == 0 && c.deletst == 0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid , c.verity,c.varid };


                return Json(K, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // int a = Convert.ToInt32(Session["UID"].ToString());
                var K = (from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid where (n.deletst == 0 && d.stuscomp == 1 && d.stusdele == 0 && d.varkeyItem==0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid, }).ToList();

               // var K = con.ItemMaserts.Where(x => x.stuscomp==1 && x.stusdele==0).ToList();
               // return Json(K, JsonRequestBehavior.AllowGet);

                var Kg = (from n in con.crop join d in con.Varity on n.cpid equals d.cpid join c in con.ItemMaserts on d.varid equals c.varkeyItem where (n.deletst == 0 && c.stuscomp==1 && c.stusdele == 0 && d.deletst == 0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, c.price, c.qty, c.rid, c.stuspay, c.uid, c.img, c.Imid, d.verity, c.varkeyItem,d.varid }).ToList();


                var mm = (from n in con.ItemMaserts join v in con.crop on n.cpid equals v.cpid where (n.stusdele == 0 && n.varkeyItem == 0 && n.stuscomp == 1 && v.deletst == 0) select new { n.rid, n.price, n.qty, n.Imid, v.cpname, v.cpid, n.img, n.ItypeId }).Distinct().ToList();

                List<object> aa = new List<object>();

                //    

                foreach (var s in Kg)
                {
                    aa.Add(s);

                }

                foreach (var ss in mm)
                {
                    aa.Add(ss);

                }



                return Json(aa, JsonRequestBehavior.AllowGet);
            }
        }

        //Show item for specific user
        public ActionResult Show()
        {
            return View();
        }

        public JsonResult GetItemData()
        {
            DataCon con = new DataCon();
            if (Session["UTYPE"].ToString() == "Company")
            {
                int a = Convert.ToInt32(Session["UID"].ToString());

               // var K = con.ItemMaserts.Where(x => x.rid == a && x.stusdele==0).ToList();


                var K = (from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid join c in con.Varity on d.varkeyItem equals c.varid where (n.deletst == 0 && d.stusdele == 0 && d.rid==a && c.deletst==0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid,c.verity }).ToList();



                return Json(K, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int a = Convert.ToInt32(Session["UID"].ToString());
                // var K = con.ItemMaserts.Where(x => x.rid == a && x.stusshow == 1 && x.stusdele==0).ToList();
                var K = from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid join c in con.Varity on d.varkeyItem equals c.varid where (n.deletst == 0 && d.stusshow == 1 && d.stusdele == 0 && d.rid == a && c.deletst == 0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid, c.verity };

                return Json(K, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetItemDataCm()
        {
            DataCon con = new DataCon();
            if (Session["UTYPE"].ToString() == "Company")
            {
                int a = Convert.ToInt32(Session["UID"].ToString());

                // var K = con.ItemMaserts.Where(x => x.rid == a && x.stusdele==0).ToList();


                var K = (from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid  where (n.deletst == 0 && d.stusdele == 0 && d.rid == a && d.varkeyItem == 0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid }).ToList();



                return Json(K, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int a = Convert.ToInt32(Session["UID"].ToString());
                // var K = con.ItemMaserts.Where(x => x.rid == a && x.stusshow == 1 && x.stusdele==0).ToList();
                var K = from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid where (n.deletst == 0 && d.stusshow == 1 && d.stusdele == 0 && d.rid == a && d.varkeyItem==0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid, };

                return Json(K, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetShowSubmitCrop()
        {
            DataCon con = new DataCon();
            int a = Convert.ToInt32(Session["UID"].ToString());
            //var K = con.ItemMaserts.Where(x => x.uid == a && x.stussubcrop == "SubmitCrop" && x.stusdele==0).ToList();
            var K = from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid join r in con.Varity on d.varkeyItem equals r.varid where (n.deletst == 0 && d.stussubcrop == "SubmitCrop"  && d.stusdele == 0 && d.uid == a && r.deletst==0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid,d.stussubcrop,r.verity };


            return Json(K, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetShowRequirement()
        {
            DataCon con = new DataCon();
            int a = Convert.ToInt32(Session["UID"].ToString());
            // var K = con.ItemMaserts.Where(x => x.uid == a && x.stussubcrop == "Requirement" && x.stusdele==0).ToList();
            var K =( from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid join r in con.Varity on d.varkeyItem equals r.varid where (n.deletst == 0 && d.stussubcrop == "Requirement"  && d.stusdele == 0 && d.uid == a && r.deletst == 0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid, d.stussubcrop,r.verity,d.varkeyItem }).ToList();
            //  var mm = (from n in con.ItemMaserts join r in con.Varity on n.varkeyItem equals r.varid where (n.stusdele == 0 && v.deletst == 0 && n.stussubcrop== "Requirement" && n.uid == a) select new { n.rid, n.price, n.qty, n.Imid, v.cpname, v.cpid, n.img, n.ItypeId,n.stuspay,n.stussubcrop }).Distinct().ToList();

            var mm = (from n in con.ItemMaserts join v in con.crop on n.cpid equals v.cpid where (n.stusdele == 0 && n.varkeyItem == 0 && n.stussubcrop== "Requirement" && n.uid==a && v.deletst == 0 ) select new {n.varkeyItem, n.rid, n.price, n.qty, n.Imid, v.cpname, v.cpid, n.img, n.ItypeId,n.stuspay, }).Distinct().ToList();

            List<object> aa = new List<object>();

            //    

            foreach (var s in K)
            {
                aa.Add(s);

            }

            foreach (var ss in mm)
            {
                aa.Add(ss);

            }
            return Json(aa, JsonRequestBehavior.AllowGet);
        }

        //group member can seen the our details specifice memabar

        public ActionResult MemDetail()
        {
            return View();
        }

        public JsonResult GetMembar()
        {
            try
            {
                DataCon con = new DataCon();
                int  a = Convert.ToInt32( Session["UID"].ToString());
             //   var K = con.ItemMaserts.Where(x => x.rid == a && x.stusshow == 0 && x.stussubcrop == "SubmitCrop" && x.stusdele==0).ToList();
              //  var K = from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid where (n.deletst == 0 && d.stussubcrop == "SubmitCrop" && d.stusdele == 0 && d.rid == a && d.stusshow == 0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid, d.stussubcrop };

                var K = (from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid join r in con.Varity on d.varkeyItem equals r.varid where (n.deletst == 0 && d.stussubcrop == "SubmitCrop" && d.stusdele == 0 && d.rid == a && r.deletst == 0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid, d.stussubcrop, r.verity, d.varkeyItem }).ToList();
         

                return Json(K, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult GetMembarRqurmet()
        {
            try
            {
                DataCon con = new DataCon();
                int a = Convert.ToInt32(Session["UID"].ToString());
             //   var K = con.ItemMaserts.Where(x => x.rid == a && x.stusshow == 0 && x.stussubcrop == "Requirement" && x.stusdele == 0).ToList();
                var K = (from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid join r in con.Varity on d.varkeyItem equals r.varid where (n.deletst == 0 && d.stussubcrop == "Requirement" && d.stusdele == 0 && d.rid == a && d.stusshow == 0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid, d.stussubcrop, r.verity, d.varkeyItem }).ToList();


              //  var K = (from n in con.crop join d in con.ItemMaserts on n.cpid equals d.cpid join r in con.Varity on d.varkeyItem equals r.varid where (n.deletst == 0 && d.stussubcrop == "Requirement" && d.stusdele == 0 && d.uid == a && r.deletst == 0) select new { n.cpid, n.cpname, n.deletst, n.ItypeId, itemtp = d.ItypeId, d.price, d.qty, d.rid, d.stuspay, d.uid, d.img, d.Imid, d.stussubcrop, r.verity, d.varkeyItem }).ToList();
                //  var mm = (from n in con.ItemMaserts join r in con.Varity on n.varkeyItem equals r.varid where (n.stusdele == 0 && v.deletst == 0 && n.stussubcrop== "Requirement" && n.uid == a) select new { n.rid, n.price, n.qty, n.Imid, v.cpname, v.cpid, n.img, n.ItypeId,n.stuspay,n.stussubcrop }).Distinct().ToList();

                var mm = (from n in con.ItemMaserts join v in con.crop on n.cpid equals v.cpid where (n.stusdele == 0 && n.varkeyItem == 0 && n.stussubcrop == "Requirement" && n.rid == a && v.deletst == 0 && n.varkeyItem==0) select new { n.varkeyItem, n.rid, n.price, n.qty, n.Imid, v.cpname, v.cpid, n.img, n.ItypeId, n.stuspay }).Distinct().ToList();

                List<object> aa = new List<object>();

                //    

                foreach (var s in K)
                {
                    aa.Add(s);

                }

                foreach (var ss in mm)
                {
                    aa.Add(ss);

                }


                return Json(aa, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                var l = "fail";
                return Json(l, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult GetMembarReq()
        {
            return View();
        }

        //payment of details is membar by secretary
        [HttpPost]
        public JsonResult PaymentMembar(ItemMasert i)
        {
            string l = "";
            try
            {
                DataCon con = new DataCon();
                var x = con.ItemMaserts.Find(i.Imid);
                x.stuspay = Convert.ToString(i.price * i.qty);
               // UpdateModel(x);
                con.SaveChanges();
                l = "Your Payment is Successfully ";
            }
            catch (Exception)
            {
               l= "Connection problems try agin";
            }
            return Json( l,JsonRequestBehavior.AllowGet);
        }


        //repot of selling details of pdf
        public ActionResult ReportDetalis()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ReportSell(string sd, string ld, string tp)
        {
            string k = "";
           try
            {



                DataCon con = new DataCon();
                int a = Convert.ToInt32(Session["UID"].ToString());



                DateTime uu = Convert.ToDateTime(Convert.ToDateTime(ld.ToString()).ToShortDateString());
                string  ldaate = uu.Date.ToString("dd/MM/yyyy");
                

                DateTime uuu = Convert.ToDateTime(Convert.ToDateTime(sd.ToString()).ToShortDateString());
                string sdaate = uuu.Date.ToString("dd/MM/yyyy");

                //tpye = tp.ToString();

                //  string sdd = Request.Cookies["sdate"].Value.ToString();
                // string ldd = Request.Cookies["ldate"].Value.ToString();
                List<Sell_Master> kk = new List<Sell_Master>();
                if (Session["UTYPE"].ToString() == "Admin")
                {
                    kk = con.sell.Where(x => (x.date >= uuu && x.date <= uu)).ToList();

                }
                else
                {
                    kk = con.sell.Where(x => (x.rid == a) && (x.date >= uuu && x.date <= uu)).ToList();
                }
                if(kk==null)
                {

                        k = "Data is Not Found";
                }
                else
                {

                    HttpCookie ldate = new HttpCookie("ldate", uu.ToString());
                    Response.Cookies.Add(ldate);
                    HttpCookie sdate = new HttpCookie("sdate", uuu.ToString());
                    Response.Cookies.Add(sdate);
                    HttpCookie tpye = new HttpCookie("tpye", tp.ToString());
                    Response.Cookies.Add(tpye);
                    k = "success";
                }

                return Json(k,JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                k= "Connection problems try agin";
                return Json(k,JsonRequestBehavior.AllowGet);
            }
        


    
        }
        //report of requrment
        [HttpPost]
        public JsonResult ReportRequrment(string sd, string ld, string tp)
        {
            string k = "";
            try
            {
                            


                DataCon con = new DataCon();
                int a = Convert.ToInt32(Session["UID"].ToString());

                DateTime uu = Convert.ToDateTime(Convert.ToDateTime(ld.ToString()).ToShortDateString());
                string ldaate = uu.Date.ToString("dd/MM/yyyy");


                DateTime uuu = Convert.ToDateTime(Convert.ToDateTime(sd.ToString()).ToShortDateString());
                string sdaate = uuu.Date.ToString("dd/MM/yyyy");
                List<ItemMasert> KK = new List<ItemMasert>();
                if (Session["UTYPE"].ToString() == "Admin")
                {
                    KK = con.ItemMaserts.Where(x => (x.date >= uuu && x.date <= uu) && x.stusdele == 0 && x.stussubcrop == tp.ToString()).ToList();

                }
                else
                {
                    KK = con.ItemMaserts.Where(x => (x.uid == a) && (x.date >= uuu && x.date <= uu) && x.stusdele == 0 && x.stussubcrop == tp.ToString()).ToList();
                }
                if (KK == null)
                {

                    k = "Data is Not Found";
                }
                else
                {

                    HttpCookie ldate = new HttpCookie("ldate", uu.ToString());
                    Response.Cookies.Add(ldate);
                    HttpCookie sdate = new HttpCookie("sdate", uuu.ToString());
                    Response.Cookies.Add(sdate);
                    HttpCookie tpye = new HttpCookie("tpye", tp.ToString());
                    Response.Cookies.Add(tpye);
                    k = "success";
                }

                return Json(k, JsonRequestBehavior.AllowGet);


            }
            catch (Exception)
            {
                k = "Connection problems try agin";
                return Json(k, JsonRequestBehavior.AllowGet);
            }




        }
        //print sell item
        public ActionResult Print()
        {
            MemoryStream ms = new MemoryStream();

            Document doc = new Document(iTextSharp.text.PageSize.A4, 40, 40, 40, 40);
            PdfWriter pw = PdfWriter.GetInstance(doc, ms);

            PdfPTable table = new PdfPTable(6);

            //data fetch from database
            DataCon con = new DataCon();
            int a = Convert.ToInt32(Session["UID"].ToString());
            string sd = Request.Cookies["sdate"].Value.ToString();
            string ld = Request.Cookies["ldate"].Value.ToString();
            
            DateTime uuu = Convert.ToDateTime(Convert.ToDateTime(sd.ToString()).ToShortDateString());
            DateTime uu = Convert.ToDateTime(Convert.ToDateTime(ld.ToString()).ToShortDateString());

            List<Sell_Master> kk = new List<Sell_Master>();
            if (Session["UTYPE"].ToString() == "Admin")
            {
                kk = con.sell.Where(x => (x.date >= uuu && x.date <= uu)).ToList();

            }
            else
            {
                kk = con.sell.Where(x => (x.rid == a) && (x.date >= uuu && x.date <= uu)).Distinct().ToList();
            }

           // var kk = con.sell.Where(x => x.rid == a && (x.date >= uuu && x.date <= uu)).ToList();

                Paragraph vv = new Paragraph("Co-Operative Farming  Management System");

                vv.Alignment = Font.BOLD;
                vv.SpacingAfter = 5f;

                string path = ControllerContext.HttpContext.Server.MapPath("~/img/Capture.JPG");
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
                image.ScaleToFit(80f, 40f);
                image.Alignment = Element.ALIGN_CENTER;


                Paragraph vv1 = new Paragraph("Report Date :" + DateTime.UtcNow.Date.ToString("dd/MM/yyyy"));

                vv1.SpacingAfter = 6;
                vv1.Alignment = Font.BOLD;


                vv1.Alignment = Element.ALIGN_RIGHT;
                doc.Open();
                string PP = "";

                if (Request.Cookies["tpye"].Value.ToString() == "1")
                {
                    PP = "Buy Item";

                }
                else
                {
                    PP = Request.Cookies["tpye"].Value.ToString();
                }

                PdfPCell cell = new PdfPCell(new Phrase(" Report OF " + PP));

                cell.Colspan = 6;
                cell.HorizontalAlignment = Font.BOLD;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);


                table.AddCell("No");
                table.AddCell("Product Name");
                table.AddCell("Varity Name");
                table.AddCell("Quantity");
                table.AddCell("Price");
                table.AddCell("Amount");

                int s = 1;

                foreach (var it in kk)
                {
                    var item = con.ItemMaserts.Find(it.Imid);
                    table.AddCell(s.ToString());
                //   table.AddCell(item.cpid.ToString());
                var aaa = con.crop.Find(item.cpid);
                table.AddCell(aaa.cpname);
                if (it.varidsell == 0)
                {
                    table.AddCell("Fertilizer");
                }
                else
                {
                    var ss = con.Varity.Find(it.varidsell);
                    table.AddCell(ss.verity.ToString());
                }

                table.AddCell(it.Quty.ToString());
                    table.AddCell(it.price.ToString());
                    table.AddCell((it.Quty * it.price).ToString());
                    s++;
                }


                doc.Add(image);
                doc.Add(vv);

                doc.Add(vv1);
                //  doc.Add(vv11);
                doc.Add(table);
                doc.Close();
                //remove cookie
                var c = new HttpCookie("ldate");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
                var b = new HttpCookie("sdate");
                b.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(b);
                var bb = new HttpCookie("tpye");
                b.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(bb);


                byte[] byt = ms.ToArray();
                ms = new MemoryStream();
                ms.Write(byt, 0, byt.Length);
                ms.Position = 0;
                return new FileStreamResult(ms, "application/pdf");
            
        }


//print of requrment and submit of farmer
        public ActionResult PrintRequ()
        {
            MemoryStream ms = new MemoryStream();

            Document doc = new Document(iTextSharp.text.PageSize.A4, 40, 40, 40, 40);
            PdfWriter pw = PdfWriter.GetInstance(doc, ms);

            PdfPTable table = new PdfPTable(6);

            //data fetch from database
            DataCon con = new DataCon();
            int a = Convert.ToInt32(Session["UID"].ToString());
            string sd = Request.Cookies["sdate"].Value.ToString();
            string ld = Request.Cookies["ldate"].Value.ToString();

            DateTime uuu = Convert.ToDateTime(Convert.ToDateTime(sd.ToString()).ToShortDateString());
            DateTime uu = Convert.ToDateTime(Convert.ToDateTime(ld.ToString()).ToShortDateString());
            if (Request.Cookies["tpye"].Value.ToString() == "Requirement")
            {
                List<ItemMasert> kk = new List<ItemMasert>();
                if (Session["UTYPE"].ToString() == "Admin")
                {
                   kk = con.ItemMaserts.Where(x => (x.date >= uuu && x.date <= uu) && x.stusdele == 0 && x.stussubcrop == "Requirement").ToList();

                }
                else
                {
                    kk = con.ItemMaserts.Where(x => (x.uid == a) && (x.date >= uuu && x.date <= uu) && x.stusdele == 0 && x.stussubcrop == "Requirement").ToList();
                }


                // var kk = con.ItemMaserts.Where(x => x.uid == a && (x.date >= uuu && x.date <= uu) && x.stusdele == 0 && x.stussubcrop == "Requirement").ToList();

                Paragraph vv = new Paragraph("Co-Operative Farming  Management System");

                vv.Alignment = Font.BOLD;
                vv.SpacingAfter = 6f;

                string path = ControllerContext.HttpContext.Server.MapPath("~/img/Capture.JPG");
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
                image.ScaleToFit(80f, 40f);
                image.Alignment = Element.ALIGN_CENTER;


                Paragraph vv1 = new Paragraph("Report Date :" + DateTime.UtcNow.Date.ToString("dd/MM/yyyy"));

                vv1.SpacingAfter = 6;
                vv1.Alignment = Font.BOLD;


                vv1.Alignment = Element.ALIGN_RIGHT;
                doc.Open();
                string PP = "";

                if (Request.Cookies["tpye"].Value.ToString() == "1")
                {
                    PP = "Sell Item";

                }
                else
                {
                    PP = Request.Cookies["tpye"].Value.ToString();
                }

                PdfPCell cell = new PdfPCell(new Phrase(" Report OF " + PP));

                cell.Colspan = 6;
                cell.HorizontalAlignment = Font.BOLD;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);


                table.AddCell("No");
                table.AddCell("Farmer Name");
                table.AddCell("Product Name");
                table.AddCell("Varity Name");
                table.AddCell("Quantity");
                table.AddCell("Price");
              

                int s = 1;

                foreach (var it in kk)
                {
                    var re = con.Reg.Find(it.rid);
                    table.AddCell(s.ToString());
                    table.AddCell(re.name);
                    ///   table.AddCell(it.cpid.ToString());
                      var aaa = con.crop.Find(it.cpid);
                    table.AddCell(aaa.cpname);
                  
                    if (it.varkeyItem == 0)
                    {
                        table.AddCell("Fertilizer");
                    }
                    else
                    {
                        var ss = con.Varity.Find(it.varkeyItem);
                        table.AddCell(ss.verity.ToString());
                    }

                    table.AddCell(it.qty.ToString());
                    table.AddCell(it.price.ToString());
                
                    s++;
                }


                doc.Add(image);
                doc.Add(vv);

                doc.Add(vv1);

                doc.Add(table);
                doc.Close();


                byte[] byt = ms.ToArray();
                ms = new MemoryStream();
                ms.Write(byt, 0, byt.Length);
                ms.Position = 0;

            }
            else
            {
                List<ItemMasert> kk = new List<ItemMasert>();
               
                if (Session["UTYPE"].ToString() == "Admin")
                {
                    kk = con.ItemMaserts.Where(x => (x.date >= uuu && x.date <= uu) && x.stusdele == 0 && x.stussubcrop == "SubmitCrop" && x.varkeyItem!=0).ToList();

                }
                else
                {
                    kk = con.ItemMaserts.Where(x => (x.uid == a) && (x.date >= uuu && x.date <= uu) && x.stusdele == 0 && x.stussubcrop == "SubmitCrop" && x.varkeyItem!=0 ).ToList();

                     //h = (from o in con.sell join d in con.ItemMaserts on o.Imid equals d.Imid join cc in con.Varity on o.varidsell equals cc.varid where (cc.deletst == 0 && d.stusdele == 0 && o.stussmdele == 0 && d.uid == a) select new { o.price, o.Quty, o.Imid, o.varidsell, cc.verity, cc.varid, d.uid }).ToList();
                }


                //  var kk = con.ItemMaserts.Where(x => x.uid == a && (x.date >= uuu && x.date <= uu) && x.stusdele == 0 && x.stussubcrop == "SubmitCrop").ToList();

                Paragraph vv = new Paragraph("Co-Operative Farming  Management System");

                vv.Alignment = Font.BOLD;
                vv.SpacingAfter = 6f;

                string path = ControllerContext.HttpContext.Server.MapPath("~/img/Capture.JPG");
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
                image.ScaleToFit(80f, 40f);
                image.Alignment = Element.ALIGN_CENTER;


                Paragraph vv1 = new Paragraph("Report Date :" + DateTime.UtcNow.Date.ToString("dd/MM/yyyy"));

                vv1.SpacingAfter = 6;
                vv1.Alignment = Font.BOLD;


                vv1.Alignment = Element.ALIGN_RIGHT;
                doc.Open();
                string PP = "";

                if (Request.Cookies["tpye"].Value.ToString() == "1")
                {
                    PP = "Sell Item";

                }
                else
                {
                    PP = Request.Cookies["tpye"].Value.ToString();
                }

                PdfPCell cell = new PdfPCell(new Phrase(" Report OF " + PP));

                cell.Colspan = 6;
                cell.HorizontalAlignment = Font.BOLD;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);


                table.AddCell("No");
                table.AddCell("Farmer Name");
                table.AddCell("Product Name");
                table.AddCell("Varity Name");
                table.AddCell("Quantity");
                table.AddCell("Price");            

                int s = 1;

                foreach (var it in kk)
                {
                    var re = con.Reg.Find(it.rid);
                    table.AddCell(s.ToString());
                    table.AddCell(re.name);
                    //  table.AddCell(it.cpid.ToString());
                    var aaa = con.crop.Find(it.cpid);
                    table.AddCell(aaa.cpname);
                   
                    if (it.varkeyItem == 0)
                    {
                        
                    }
                    else
                    {
                        var ss = con.Varity.Find(it.varkeyItem);
                        table.AddCell(ss.verity.ToString());
                    }


                   
                    table.AddCell(it.qty.ToString());
                    table.AddCell(it.price.ToString());


                    s++;
                }


                doc.Add(image);
                doc.Add(vv);

                doc.Add(vv1);

                doc.Add(table);
                doc.Close();


                byte[] byt = ms.ToArray();
                ms = new MemoryStream();
                ms.Write(byt, 0, byt.Length);
                ms.Position = 0;
            }



            //remove cookie
            var c = new HttpCookie("ldate");
            c.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(c);
            var b = new HttpCookie("sdate");
            b.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(b);
            var bb = new HttpCookie("tpye");
            b.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(bb);           
            return new FileStreamResult(ms, "application/pdf");


        }
        [HttpPost]
        public JsonResult ReportSellItem(string sd, string ld, string tp)
        {
            string k = "";
            try
            {



                DataCon con = new DataCon();
                int a = Convert.ToInt32(Session["UID"].ToString());

                DateTime uu = Convert.ToDateTime(Convert.ToDateTime(ld.ToString()).ToShortDateString());
                string ldaate = uu.Date.ToString("dd/MM/yyyy");


                DateTime uuu = Convert.ToDateTime(Convert.ToDateTime(sd.ToString()).ToShortDateString());
                string sdaate = uuu.Date.ToString("dd/MM/yyyy");

                //List kk = new List
                var kk =  from s in con.sell join i in con.ItemMaserts on s.rid equals i.rid join r in con.Reg on s.rid equals r.rid where(i.uid==a && r.stusdele==0 && i.stusdele==0 && s.stussmdele==0 && (s.date>=uuu && s.date<=uu)) select new {
                    s.date,
                    s.Quty,
                    s.price,
                    i.cpid,
                    r.gname,
                         

                };


               
                if (kk == null)
                {

                    k = "Data is Not Found";
                }
                else
                {

                    HttpCookie ldate = new HttpCookie("ldate", uu.ToString());
                    Response.Cookies.Add(ldate);
                    HttpCookie sdate = new HttpCookie("sdate", uuu.ToString());
                    Response.Cookies.Add(sdate);
                    HttpCookie tpye = new HttpCookie("tpye", tp.ToString());
                    Response.Cookies.Add(tpye);
                    k = "success";
                }

                return Json(k, JsonRequestBehavior.AllowGet);


            }
            catch (Exception)
            {
                k = "Connection problems try agin";
                return Json(k, JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult PrintItemSell()
        {
            MemoryStream ms = new MemoryStream();

            Document doc = new Document(iTextSharp.text.PageSize.A4, 40, 40, 40, 40);
            PdfWriter pw = PdfWriter.GetInstance(doc, ms);

            PdfPTable table = new PdfPTable(6);

            //data fetch from database
            DataCon con = new DataCon();
            int a = Convert.ToInt32(Session["UID"].ToString());
            string sd = Request.Cookies["sdate"].Value.ToString();
            string ld = Request.Cookies["ldate"].Value.ToString();

            DateTime uuu = Convert.ToDateTime(Convert.ToDateTime(sd.ToString()).ToShortDateString());
            DateTime uu = Convert.ToDateTime(Convert.ToDateTime(ld.ToString()).ToShortDateString());

            //var kk =(from ss in con.sell
            //         join i in con.ItemMaserts on ss.rid equals i.rid
            //         join rr in con.Reg on ss.rid equals rr.rid join zz in con.crop on i.cpid equals zz.cpid
            //         where (i.uid == a && rr.stusdele == 0 && i.stusdele == 0 && ss.stussmdele == 0 && zz.deletst==0 && ss.varidsell==0 && (ss.date >= uuu && ss.date <= uu))
            //         select new
            //         {
            //             ss.date,
            //             ss.rid,
            //             ss.Quty,
            //             ss.price,
            //             i.cpid,
            //             rr.gname,
            //             zz.cpname,
            //           farmername=rr.name,
            //           i.varkeyItem

            //         }).ToList();

            if (Session["UTYPE"].ToString() != "Admin")
            {
                var kkk = (from ss in con.sell
                           join i in con.ItemMaserts on ss.Imid equals i.Imid
                           join rr in con.Reg on ss.rid equals rr.rid
                           join zz in con.crop on i.cpid equals zz.cpid
                           join cc in con.Varity on ss.varidsell equals cc.varid
                           where (i.uid == a && rr.stusdele == 0 && i.stusdele == 0 && ss.stussmdele == 0 && zz.deletst == 0 &&  ss.rid!=a && (ss.date >= uuu && ss.date <= uu))
                           select new
                           {
                               ss.date,
                               ss.rid,
                               ss.Quty,
                               ss.price,
                               i.cpid,
                               rr.gname,
                               zz.cpname,
                               farmername = rr.name,
                               ss.varidsell,
                               cc.varid,
                               cc.verity,
                               ss.sid,

                           }).GroupBy(x => x.sid).Select(x => x.FirstOrDefault()).Distinct().ToList();



                Paragraph vv = new Paragraph("Co-Operative Farming  Management System");

                vv.Alignment = Font.BOLD;
                vv.SpacingAfter = 6f;

                string path = ControllerContext.HttpContext.Server.MapPath("~/img/Capture.JPG");
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
                image.ScaleToFit(80f, 40f);
                image.Alignment = Element.ALIGN_CENTER;


                Paragraph vv1 = new Paragraph("Report Date :" + DateTime.UtcNow.Date.ToString("dd/MM/yyyy"));

                vv1.SpacingAfter = 6;
                vv1.Alignment = Font.BOLD;


                vv1.Alignment = Element.ALIGN_RIGHT;
                doc.Open();
                string PP = "";

                if (Request.Cookies["tpye"].Value.ToString() == "2")
                {
                    PP = "Sell Item";

                }
                else if (Request.Cookies["tpye"].Value.ToString() == "1")
                {
                    PP = "Buy Item";
                }
                else
                {
                    PP = Request.Cookies["tpye"].Value.ToString();
                }

                PdfPCell cell = new PdfPCell(new Phrase(" Report OF " + PP));

                cell.Colspan = 6;
                cell.HorizontalAlignment = Font.BOLD;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);


                table.AddCell("No");
                table.AddCell("Date");
                table.AddCell("Product Name");
                table.AddCell("Varity Name");
                table.AddCell("Quantity");
                table.AddCell("Price");


                int s = 1;

                foreach (var it in kkk)
                {

                    table.AddCell(s.ToString());
                    table.AddCell(it.date.ToString("dd/MM/yyyy"));

                    table.AddCell(it.cpname);
                    if (it.varidsell == 0)
                    {
                        table.AddCell("Fertilizer");
                    }
                    else
                    {
                        table.AddCell(it.verity);
                    }

                    table.AddCell(it.Quty.ToString());
                    table.AddCell(it.price.ToString());

                    s++;
                }
                doc.Add(image);
                doc.Add(vv);

                doc.Add(vv1);
            }
            else
            {
                var kkk = (from ss in con.sell
                           join i in con.ItemMaserts on ss.rid equals i.rid
                           join rr in con.Reg on ss.rid equals rr.rid
                           join zz in con.crop on i.cpid equals zz.cpid
                           join cc in con.Varity on ss.varidsell equals cc.varid
                           where (rr.stusdele == 0 && i.stusdele == 0 && ss.stussmdele == 0 && zz.deletst == 0 && (ss.date >= uuu && ss.date <= uu))
                           select new
                           {
                               ss.date,
                               ss.rid,
                               ss.Quty,
                               ss.price,
                               i.cpid,
                               rr.gname,
                               zz.cpname,
                               farmername = rr.name,
                               ss.varidsell,
                               cc.varid,
                               cc.verity,
                               ss.sid,

                           }).GroupBy(x => x.sid).Select(x => x.FirstOrDefault()).Distinct().ToList();



                Paragraph vv = new Paragraph("Co-Operative Farming  Management System");

                vv.Alignment = Font.BOLD;
                vv.SpacingAfter = 6f;

                string path = ControllerContext.HttpContext.Server.MapPath("~/img/Capture.JPG");
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
                image.ScaleToFit(80f, 40f);
                image.Alignment = Element.ALIGN_CENTER;


                Paragraph vv1 = new Paragraph("Report Date :" + DateTime.UtcNow.Date.ToString("dd/MM/yyyy"));

                vv1.SpacingAfter = 6;
                vv1.Alignment = Font.BOLD;


                vv1.Alignment = Element.ALIGN_RIGHT;
                doc.Open();
                string PP = "";

                if (Request.Cookies["tpye"].Value.ToString() == "2")
                {
                    PP = "Sell Item";

                }
                else if (Request.Cookies["tpye"].Value.ToString() == "1")
                {
                    PP = "Buy Item";
                }
                else
                {
                    PP = Request.Cookies["tpye"].Value.ToString();
                }

                PdfPCell cell = new PdfPCell(new Phrase(" Report OF " + PP));

                cell.Colspan = 6;
                cell.HorizontalAlignment = Font.BOLD;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);


                table.AddCell("No");
                table.AddCell("Date");
                table.AddCell("Product Name");
                table.AddCell("Varity Name");
                table.AddCell("Quantity");
                table.AddCell("Price");


                int s = 1;

                foreach (var it in kkk)
                {

                    table.AddCell(s.ToString());
                    table.AddCell(it.date.ToString("dd/MM/yyyy"));

                    table.AddCell(it.cpname);
                    if (it.varidsell == 0)
                    {
                        table.AddCell("Fertilizer");
                    }
                    else
                    {
                        table.AddCell(it.verity);
                    }

                    table.AddCell(it.Quty.ToString());
                    table.AddCell(it.price.ToString());

                    s++;
                }
                doc.Add(image);
                doc.Add(vv);

                doc.Add(vv1);
            }

           

           

                doc.Add(table);
                doc.Close();


                byte[] byt = ms.ToArray();
                ms = new MemoryStream();
                ms.Write(byt, 0, byt.Length);
                ms.Position = 0;
            return new FileStreamResult(ms, "application/pdf");
        }
        }
}