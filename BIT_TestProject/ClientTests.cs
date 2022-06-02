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
    public class ClientTests
    {
        // Unit Testing: Testing a part of a code that can be represented as a unit and cannot be decomposed further
        // Integrated Testing: Where you integrate one more layer of your application and test the two layers together
        // End-to-End Testing: Overall system testing and user acceptance testing

        [TestMethod]
        public void TestClientNames()
        {
            Client newClient = new Client(createHelper: false);
            newClient.FirstName = "Jack";
            string expectedFName = "Jack";
            Assert.AreEqual(expectedFName, newClient.FirstName);
        }
        [TestMethod]
        public void TestClientCollection()
        {
            ClientViewModel clientVM = new ClientViewModel();
            int count = clientVM.Clients.Count;
            Assert.AreEqual(10, count);
        }
        [TestMethod]
        public void TestClientCollectionMock()
        {
            ObservableCollection<Client> mockClients = new ObservableCollection<Client>();
            mockClients.Add(new Client { ClientID = 1, FirstName = "John", LastName = "Doe" });
            mockClients.Add(new Client { ClientID = 2, FirstName = "Jane", LastName = "Smith" });
            Mock<ClientViewModel> mockClientVM = new Mock<ClientViewModel>();
            mockClientVM.Setup(mc => mc.GetClients()).Returns(mockClients);
            int count = mockClientVM.Object.Clients.Count;
            Assert.AreEqual(10, count);
        }
    }
}
