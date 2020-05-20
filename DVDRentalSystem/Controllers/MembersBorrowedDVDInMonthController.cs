using DataContext.Data;
using DVDRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Loan> loanUserListLatestIssue = new List<Loan>();
       
            foreach (var item in loanedMemberList)
            { allMemberInLoanList.Add(item.MemberId); }

            distinctMemberId = allMemberInLoanList.Distinct().ToList();


            foreach (var uniqueId in distinctMemberId)
            {
                loanUserListLatestIssue = db.Loans.Include("Members").Where(x => x.MemberId == uniqueId).OrderByDescending(x=>x.IssueDate).Take(1).ToList();
                               
            }

            return View(loanUserListLatestIssue);
        }
    }
}