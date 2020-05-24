using DataContext.Data;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVDRentalSystem.Controllers
{
    [Authorize(Roles = "Manager,Assistant")]
    public class FilterByLoanCopyNumberPostController : Controller
    {
        string dvdName ;
        private DVDRentalSystemContext dbCon = new DVDRentalSystemContext();
        // GET: FilterByLoanCopyNumberPost
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult filterByLoanCopyNumberPost()
        {


            List<int> numberOfCopies = new List<int>();
            dvdName = HttpContext.Request["DVDDetails"];

            var numberOfCopiesList = dbCon.Loans.Include("DVDDetails")
                .Where(x => x.DVDDetails.Name == dvdName)
                .Select(x => x.CopyId);
            //var finalData = dbCon.Loans.Include("DVDDetails").Where(x => x.DVDDetails.Name == dvdName).Where(x => x.CopyId == int.Parse(copyid)).ToList();
            foreach (var val in numberOfCopiesList)
            {
                numberOfCopies.Add(val);

            }



            var totalNumberList = numberOfCopies;


            //var finalList = totalNumberList.Except(totalNumberList1);
            TempData["dvdNameStored"] = dvdName;

            ViewBag.CopyId = totalNumberList;

            return View();
        }

        [HttpGet]
        public ActionResult filterByLoanCopyNumberFinal(string CopyId)
        {

            string dvdId = TempData["dvdNameStored"] as string;
            int numberOfCopies = 0;
      

            int intCopyId = int.Parse(CopyId);

            var numberOfCopiesList = dbCon.Loans.Include("DVDDetails").Where(x => x.DVDDetails.Name == dvdName).Select(x => x.DVDDetails.NumberOfCopies);
            var finalData = dbCon.Loans.Include("DVDDetails").Where(x => x.DVDDetails.Name == dvdId).Where(x => x.CopyId == intCopyId).OrderByDescending(x => x.IssueDate).ToList();
           
            foreach (var val in numberOfCopiesList)
            {
                numberOfCopies = val;

            }



            var totalNumberList = Enumerable.Range(1, numberOfCopies).ToList();


            //var finalList = totalNumberList.Except(totalNumberList1);

            ViewBag.CopyId = totalNumberList;

            return View(finalData);
        }





    }
}