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
    public class DVDNotLoanedInAMonthController : Controller
    {
        private DVDRentalSystemContext db = new DVDRentalSystemContext();
        // GET: DVDNotLoanedInAMonth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DVDNotBorrowedInMonth() {

            var allDVDList = db.DVDDetails.Select(x => x.DVDDetailsId);

            var allLoanedList = db.Loans.Include("DVDDetails").Select(x => x.DVDDetailsId);
            List<int> neverLoanedDVDList = new List<int>();

            List<int> currentlyNotLoanedList = new List<int>();
            List<int> allLoanedDVDIDList = new List<int>();

            List<int> loanNotNulledList = new List<int>();


            List<int> falseDataList = new List<int>();
            List<int> notcontainslist = new List<int>();

            List<int> datelimitedList = new List<int>();


            //final sending list
            List<Models.DVDDetails> finalSendingList = new List<Models.DVDDetails>();

            foreach (var dvd in allDVDList) {
                if (!allLoanedList.Contains(dvd))
                {
                    neverLoanedDVDList.Add(dvd);
                }
                else { }
            }



            foreach (var x in neverLoanedDVDList) {
                Debug.WriteLine("never loaned dvd: " + x);
            }



            var loanedTableOldDVD = db.Loans.Include("DVDDetails");
           


            foreach (var x in allLoanedList) {
                allLoanedDVDIDList.Add(x);
            }
            var distinctAllLoanTableDVDID = allLoanedDVDIDList.Distinct();

            bool flag = true;
            foreach (var dis in distinctAllLoanTableDVDID  ) {

                foreach (var loanT in loanedTableOldDVD) {
                    if (loanT.DVDDetailsId == dis) {

                        if (loanT.ActualReturnedDate == null)
                        {
                            Debug.WriteLine("im in false flag");
                            falseDataList.Add(dis);
                            flag = false;



                        }
                        else
                        {

                        }
                    }




                }
                if (flag == true)
                {
                    loanNotNulledList.Add(dis);
                }
                flag = true;
            }


            foreach (var v in loanNotNulledList) { 
                if (!falseDataList.Contains(v))
                {
                    notcontainslist.Add(v);
                }
        }
            //var days = (Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(each.IssueDate)).TotalDays;

            //if (days >= 31) {
            //    currentlyNotLoanedList.Add(each.DVDDetailsId);
            //}
            //================================================================

            bool flagM = true;
            foreach (var dis in notcontainslist)
            {

                foreach (var loanT in loanedTableOldDVD)
                {
                    if (loanT.DVDDetailsId == dis)
                    {


                        var days = (Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(loanT.IssueDate)).TotalDays;
                        if (days < 30)
                        {


                            flagM = false;



                        }
                        else
                        {

                        }
                    }




                }
                if (flagM == true)
                {
                    datelimitedList.Add(dis);
                }
                flagM = true;
            }
           
            //=================================================================

            var allGoodList = neverLoanedDVDList.Concat(datelimitedList).Distinct().ToList();



            Models.DVDDetails dvdD = new Models.DVDDetails();


            IList<DVDDetails> sendData = new List<DVDDetails>();



            foreach (var z in allGoodList) {

                finalSendingList = db.DVDDetails.Where(x => x.DVDDetailsId == z).ToList();
                foreach (var data in finalSendingList) {

                    DVDDetails dvdDet = new DVDDetails();
                    dvdDet.DVDDetailsId = data.DVDDetailsId;
                    dvdDet.Name = data.Name;
                    dvdDet.Genre = data.Genre;
                    dvdDet.ReleaseDate = data.ReleaseDate;
                    dvdDet.Length = data.Length;
                    dvdDet.CoverImagePath = data.CoverImagePath;
                    dvdDet.NumberOfCopies = data.NumberOfCopies;
                    dvdDet.AgeRestricted = data.AgeRestricted;
                    dvdDet.StockedDate = data.StockedDate;
                    dvdDet.isDeleted = data.isDeleted;
                    sendData.Add(dvdDet);
                }
                

            }





            return View(sendData);
        }
    }
}