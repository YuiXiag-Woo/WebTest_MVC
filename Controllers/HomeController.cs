using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;



//新增
using WebTest_MVC.Models;

namespace WebTest_MVC.Controllers
{
    public class HomeController : Controller
    {
        WebDBEntities db=new WebDBEntities();
        public ActionResult Index()
        {
           var Customer=  db.EmpData.OrderBy(x => x.empId);
            HtmlStreamData data= new HtmlStreamData();
            data.empDatas = Customer;
            return View(data);
        }
        [HttpPost]
        public ActionResult CreateMember(HtmlStreamData HSdata)
        {
            //HSdata.isEdit = true;
            try
            {
                var Editmem = db.EmpData.FirstOrDefault(x => x.empName.Equals(HSdata.empMem.empName));
                if (Editmem != null)
                {
                    Editmem.empName = HSdata.empMem.empName;
                    Editmem.empBirth = HSdata.empMem.empBirth;
                    Editmem.empAge = HSdata.empMem.empAge;
                    db.SaveChanges();//更新資料庫
                }
                else 
                {
                    db.EmpData.Add(HSdata.empMem);
                    db.SaveChanges();//更新資料庫           
                }
                //新增
                       
                                                                                                                                 
                return RedirectToAction("Index");//執行 ActionResult AccountIndex()
            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }
        public ActionResult Delete(string name)
        {
            var Deletemem = db.EmpData.FirstOrDefault(x => x.empName.Equals(name) );
            db.EmpData.Remove(Deletemem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(HtmlStreamData HSdata)
        {         
            var Customer = db.EmpData.OrderBy(x => x.empId);

            HSdata.empDatas = Customer;
            //var editmem = db.EmpData.FirstOrDefault(x => x.empName.Equals(name));           
            return RedirectToAction("Index");
        }
    }
}