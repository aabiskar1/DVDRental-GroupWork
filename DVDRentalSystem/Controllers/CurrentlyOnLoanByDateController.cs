using DataContext.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVDRentalSystem.Controllers
{
    public class CurrentlyOnLoanByDateController : Controller
    {
        private DVDRentalSystemContext db = new DVDRentalSystemContext();
        // GET: CurrentlyOnLoanByDate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult byDate() {
            List<DateTime> UniqueDateList = new List<DateTime>();

            var OnLoanList = db.Loans.Where(x => x.ActualReturnedDate == null)
                .OrderByDescending(x => x.IssueDate)
                .ThenBy(x => x.DVDDetails.Name);


            var uniqueDate = db.Loans.Where(x => x.ActualReturnedDate == null)
                .OrderByDescending(x => x.IssueDate)
                .ThenBy(x => x.DVDDetails.Name)
                .Select(x => x.IssueDate).Distinct();
            foreach (var v in uniqueDate) {

                UniqueDateList.Add(v.Date);
            }


            var memberIDOnLoans = db.Loans.Select(x => x.MemberId)

                .Distinct();




            ViewBag.UniqueDateList = UniqueDateList.Distinct();

            ViewBag.MemberIDOnLoans = memberIDOnLoans;


            return View(OnLoanList);        
        }
    }
}