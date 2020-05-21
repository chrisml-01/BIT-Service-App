using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIT_Service_Ver2.ViewModel;
using BIT_Service_Ver2.Model;
using BIT_Service_Ver2.Commands;

namespace BITServiceUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DOB_WhenDOBisonaFutureDate()
        {
            InputValidation dob = new InputValidation();
            //DateTime sampleDOB = DateTime.Parse("24/01/2005");
            DateTime sampleDOB = DateTime.Parse("01/24/2005"); //the date doesn't seem compatible with some computers
            var dobmethod = dob.DOB(sampleDOB);
            //dob.dob_isFuture(sampleDOB);
            Assert.IsTrue(dobmethod, "Passed");
        }

        [TestMethod]
        public void FirstName_isNull()
        {
            InputValidation fn = new InputValidation();
            String sampleFN = "Chris";
            String sampleLN = "Luntok";
            var fnmethod = fn.Name(sampleFN, sampleLN);
            Assert.IsTrue(fnmethod, "Passed");
        }

        [TestMethod]
        public void Email()
        {
            InputValidation em = new InputValidation();
            String email = "email@email.com";
            var emmethod = em.Email(email);
            Assert.IsTrue(emmethod, "Passed");
        }
    }
}
