using DataContext.Data;
using DVDRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace DVDRentalSystem.Controllers
{
    public class MembersBorrowedDVDInMonthController : Controller
    {
        private DVDRentalSystemContext db = new DVDRentalSystemContext();
        // GET: MembersBorrowedDVDInMonth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BorrowedDVDInLastMonth()
        {
            var loanedMemberList = db.Loans.Include("Members");


            List<int> allMemberInLoanList = new List<int>();
            List<int> distinctMemberId = new List<int>();
           
            IList<Loan> listof = new List<Loan>();

            foreach (var item in loanedMemberList)
            { allMemberInLoanList.Add(item.MemberId); }

            distinctMemberId = allMemberInLoanList.Distinct().ToList();


            foreach (var uniqueId in distinctMemberId)
            {
                var data = db.Loans.Include("Members").Where(x => x.MemberId == uniqueId).OrderByDescending(x=>x.IssueDate).Take(1);
                foreach (var d in data) {

                    Loan loandata = new Loan();
                    loandata.LoanId = d.LoanId;
                    loandata.DVDDetailsId = d.DVDDetailsId;
                   
                    loandata.MemberId = d.MemberId;
                    loandata.IssueDate = d.IssueDate;
                    loandata.ReturnDate = d.ReturnDate;
                    loandata.FineAmount = d.FineAmount;
                    loandata.ActualReturnedDate = d.ActualReturnedDate;
                    loandata.CopyId = d.CopyId;
                    loandata.LoanTypeId = d.LoanTypeId;


                    listof.Add(loandata);

                }





            }

            var dvdListData = db.DVDDetails;


            var allMemberList = db.Members;

            ViewBag.DVDList = listof.ToList();
            ViewBag.MemberList = allMemberList.ToList();
            ViewBag.DVDDetailsList = dvdListData.ToList();
            return View(listof);
        }
    }
}