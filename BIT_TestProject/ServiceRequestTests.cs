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
    public class ServiceRequestTests
    {
        // Unit Testing: Testing a part of a code that can be represented as a unit and cannot be decomposed further
        // Integrated Testing: Where you integrate one more layer of your application and test the two layers together
        // End-to-End Testing: Overall system testing and user acceptance testing

        [TestMethod]
        public void TestServiceRequest()
        {
            ServiceRequest newServiceRequest = new ServiceRequest(createHelper: false);
            newServiceRequest.BusinessName = "TAFE NSW";
            string expectedName = "TAFE NSW";
            Assert.AreEqual(expectedName, newServiceRequest.BusinessName);
        }
        [TestMethod]
        public void TestServiceRequestCollection()
        {
            ServiceRequestViewModel serviceRequestVM = new ServiceRequestViewModel();
            int count = serviceRequestVM.AllServiceRequests.Count;
            Assert.AreEqual(19, count);
        }
        [TestMethod]
        public void TestServiceRequestCollectionMock()
        {
            ObservableCollection<ServiceRequest> mockServiceRequests = new ObservableCollection<ServiceRequest>();
            mockServiceRequests.Add(new ServiceRequest { ClientID = 1, BusinessName = "Datacom" });
            mockServiceRequests.Add(new ServiceRequest { ClientID = 2, BusinessName = "Peakbound" });
            Mock<ServiceRequestViewModel> mockServiceRequestVM = new Mock<ServiceRequestViewModel>();
            mockServiceRequestVM.Setup(mc => mc.GetServiceRequests()).Returns(mockServiceRequests);
            int count = mockServiceRequestVM.Object.AllServiceRequests.Count;
            Assert.AreEqual(19, count);
        }
    }
}
