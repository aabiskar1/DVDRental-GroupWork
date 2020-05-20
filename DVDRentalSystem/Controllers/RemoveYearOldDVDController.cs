using DataContext.Data;
using DVDRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVDRentalSystem.Controllers
{
    public class RemoveYearOldDVDController : Controller
    {
        // GET: RemoveYearOldDVD


        private DVDRentalSystemContext db = new DVDRentalSystemContext();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListOldDVD() {
            var DVDList = db.DVDDetails.Where(x => x.isDeleted==false).ToList();
            var LoanList = db.Loans.ToList();
            dynamic model = new DVDLoanMergeModel();
            model.DVDDetails = DVDList;
            model.Loans = LoanList;

           
            return View(model);
        }

        [HttpPost]
        public ActionResult removeAllDVD(string valueINeed)
        {

            var removeLists = valueINeed;
            string[] array = removeLists.TrimEnd(',').Split(',');
            List<string> list = new List<string>(array);

         
            foreach (var ids in list) {
                int intDVDId = int.Parse(ids);
                var updateDelete = db.DVDDetails.Where(x => x.DVDDetailsId == intDVDId);
                foreach (DVDDetails ord in updateDelete)
                {
                    ord.isDeleted = true ;
                   
                }
                db.SaveChanges();

            }

            Debug.WriteLine(removeLists.ToString());
            return  RedirectToAction("index", "DVDDetails");
        }
    }
}