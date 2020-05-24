using DataContext.Data;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVDRentalSystem.Controllers
{
    [Authorize(Roles = "Manager,Assistant")]
    public class FilterByLoanCopyNumberController : Controller
    {
        private DVDRentalSystemContext dbCon = new DVDRentalSystemContext();
        // GET: FilterByLoanCopyNumber
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult filterByLoanCopyNumber(string dvdName, string copyNumber )
        {
            int numberOfCopies = 0;
            var numberOfCopiesList = dbCon.Loans.Include("DVDDetails")
                .Where(x => x.DVDDetails.Name == dvdName)
                .Select(x => x.DVDDetails.NumberOfCopies);
            foreach (var val in numberOfCopiesList)
            {
                numberOfCopies = val;

            }

            var totalNumberList = Enumerable.Range(1, numberOfCopies).ToList();
            var LoanedDVDsList = dbCon.Loans.Include("DVDDetails")
                .Where(x => x.DVDDetails.Name == dvdName);
            var movieList = dbCon.DVDDetails.Select(x => x.Name);


            //var totalNumberList1 = Enumerable.Range(5, 10).ToList();


            //var finalList = totalNumberList.Except(totalNumberList1);
           

            ViewBag.CopyId = totalNumberList;
            ViewBag.MovieLists = movieList.ToList();

            return View(LoanedDVDsList);
        }


   


    }
}