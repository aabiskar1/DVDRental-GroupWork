using DataContext.Data;
using DVDRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVDRentalSystem.Controllers
{
    [Authorize(Roles = "Manager,Assistant")]
    public class FilterByAlphabaticalMembersController : Controller
    {
          
        private DVDRentalSystemContext dbCon = new DVDRentalSystemContext();
        // GET: FilterByAlphabaticalMembers
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult filterByName() {

            var filteredMemberList = dbCon.Members
                .OrderBy(x => x.FirstName).ToList();


            var loanedData = dbCon.Loans.ToList();

            dynamic model = new MergeModel();
            model.Members = filteredMemberList;
            model.Loans = loanedData;


            return View(model);
        }
    }

}