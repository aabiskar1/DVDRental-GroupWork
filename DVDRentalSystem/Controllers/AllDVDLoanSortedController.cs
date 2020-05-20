using DataContext.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVDRentalSystem.Controllers
{
    public class AllDVDLoanSortedController : Controller
    {

        private DVDRentalSystemContext dbCon = new DVDRentalSystemContext();
        // GET: AllDVDLoanSorted
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult sortDVDLoans() {

            var sortedDVDLoanList = dbCon.Loans.Include("DVDDetails")
                .Include("Members")
                .Where(x => x.ActualReturnedDate == null)
                .OrderBy(x => x.IssueDate).ThenBy(x => x.DVDDetails.Name);
            return null;
        }
    }
}