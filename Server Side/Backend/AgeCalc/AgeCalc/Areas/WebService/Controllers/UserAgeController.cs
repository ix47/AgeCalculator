using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgeCalc.Areas.WebService.Models;
using AgeCalc.Models;
using AgeCalc.Models.Date_Calculations;
using System.Collections;

namespace AgeCalc.Areas.WebService.Controllers
{
    [HandleError]
    public class UserAgeController : Controller
    {
        private const string PARAMETER_FIRST_NAME = "FirstName";
        private const string PARAMETER_LAST_NAME = "LastName";
        private const string PARAMETER_DATE_OF_BIRTH = "DateOfBirth";
        private const string PARAMETER_CLIENT_DATE_TIME = "ClientDateTime";

        private const string JSON_ERROR_RESULT = "Error";

        AgeCalcEntities _database;

        public UserAgeController()
        {
            _database = new AgeCalcEntities();
        }

        //
        // GET: /WebService/UserAge/

        public ActionResult Index()
        {
            var list = _database.UserAges.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetVisitors(FormCollection collection)
        {
            var stringClientDateTime = (string)Utilities.ExtractParameter(collection, PARAMETER_CLIENT_DATE_TIME, typeof(string));
            DateTime clientDateTime = DateTime.ParseExact(stringClientDateTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            HashDate clientDateTimeHash = new HashDate(clientDateTime);

            ArrayList list = new ArrayList();

            foreach (var item in _database.UserAges)
            {
                Visitor visitor = new Visitor();
                visitor.UserId = item.UserId;
                visitor.FirstName = item.FirstName;
                visitor.LastName = item.LastName;
                HashDate dateOfBirthHash = new HashDate(item.DateOfBirth);
                HashDate diff = clientDateTimeHash.Difference(dateOfBirthHash);
                visitor.Age = diff.getAsDictionary();

                list.Add(visitor);
            }

            return Json(list);
        }

        //
        // POST: /WebService/UserAge/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var firstName = (string)Utilities.ExtractParameter(collection, PARAMETER_FIRST_NAME, typeof(string));
                var lastName = (string)Utilities.ExtractParameter(collection, PARAMETER_LAST_NAME, typeof(string));
                var stringDateOfBirth = (string)Utilities.ExtractParameter(collection, PARAMETER_DATE_OF_BIRTH, typeof(string));
                var stringClientDateTime = (string)Utilities.ExtractParameter(collection, PARAMETER_CLIENT_DATE_TIME, typeof(string));

                DateTime dateOfBirth = DateTime.ParseExact(stringDateOfBirth, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime clientDateTime = DateTime.ParseExact(stringClientDateTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);


                HashDate dateOfBirthHash = new HashDate(dateOfBirth);
                HashDate clientDateTimeHash = new HashDate(clientDateTime);

                HashDate diff = clientDateTimeHash.Difference(dateOfBirthHash);
                var result = diff.getAsDictionary();

                //Write the user to the database before sending the reply.
                UserAge newUserAge = new UserAge();
                newUserAge.FirstName = firstName;
                newUserAge.LastName = lastName;
                newUserAge.DateOfBirth = dateOfBirth;

                try
                {
                    _database.UserAges.AddObject(newUserAge);
                    _database.SaveChanges();
                }
                catch (Exception ex)
                {
                    //We could not save the data. However, we still want the user to be able to see the results for the age calculation.
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                result.Add(JSON_ERROR_RESULT, ex.Message);
                return Json(result);
            }
        }
        
        //
        // GET: /WebService/UserAge/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /WebService/UserAge/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /WebService/UserAge/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /WebService/UserAge/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
