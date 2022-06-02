using BIT_DesktopApp.Models;
using BIT_DesktopApp.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.ObjectModel;

namespace BIT_TestProject
{
    // With BIT Services
    // In all (includes UnitTests as well as Integration Test Cases)
    // We need to produce atleast 9 test cases
    // 3 test cases - Contractor
    // 3 test cases - Client
    // 3 test cases - Job

    [TestClass]
    public class ContractorTests
    {
        // Unit Testing: Testing a part of a code that can be represented as a unit and cannot be decomposed further
        // Integrated Testing: Where you integrate one more layer of your application and test the two layers together
        // End-to-End Testing: Overall system testing and user acceptance testing

        [TestMethod]
        public void TestContractorNames()
        {
            Contractor newContractor = new Contractor(createHelper: false);
            newContractor.FirstName = "Richard";
            string expectedFName = "Richard";
            Assert.AreEqual(expectedFName, newContractor.FirstName);
        }
        [TestMethod]
        public void TestContractorCollection()
        {
            ContractorViewModel contractorVM = new ContractorViewModel();
            int count = contractorVM.Contractors.Count;
            Assert.AreEqual(10, count);
        }
        [TestMethod]
        public void TestContractorCollectionMock()
        {
            ObservableCollection<Contractor> mockContractors = new ObservableCollection<Contractor>();
            mockContractors.Add(new Contractor { ContractorID = 1, FirstName = "John", LastName = "Doe" });
            mockContractors.Add(new Contractor { ContractorID = 2, FirstName = "Jane", LastName = "Smith" });
            Mock<ContractorViewModel> mockContractorVM = new Mock<ContractorViewModel>();
            mockContractorVM.Setup(mc => mc.GetContractors()).Returns(mockContractors);
            int count = mockContractorVM.Object.Contractors.Count;
            Assert.AreEqual(10, count);
        }
    }
}
