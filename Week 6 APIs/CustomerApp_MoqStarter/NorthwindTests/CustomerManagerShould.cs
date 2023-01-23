using NUnit.Framework;
using Moq;
using NorthwindBusiness;
using NorthwindData;
using NorthwindData.Services;
using System.Data;
using System.Collections.Generic;
using System;

namespace NorthwindTests
{
    public class CustomerManagerShould
    {
        private CustomerManager _sut; // sut = subject under test

        [Test]
        public void BeAbleToBeConstructed()
        {
            //Arrange
            var dummyCustomerService = new Mock<ICustomerService>();
            
            //Act
            _sut = new CustomerManager(dummyCustomerService.Object); //this is a Dummy

            //Assert
            Assert.That(_sut, Is.InstanceOf<CustomerManager>());
        }

        [Test]
        public void ReturnTrue_WhenUpdateIsCalled_WithValidId()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer() { CustomerId = "JSMITH"};

            mockCustomerService.Setup(cs => cs.GetCustomerById("JSMITH")).Returns(originalCustomer);

            _sut = new CustomerManager(mockCustomerService.Object);

            //Act
            var result = _sut.Update("JSMITH", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void UpdateCustomer_WhenUpdateIsCalled_WithValidIdAndInputs()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer() { 
                CustomerId = "JSMITH" ,
                ContactName = "John Smith",
                Country = "UK",
                City = "Birmingham",
                PostalCode = "B99 AB3"
            };

            mockCustomerService.Setup(cs => cs.GetCustomerById("JSMITH")).Returns(originalCustomer);

            _sut = new CustomerManager(mockCustomerService.Object);

            //Act
            var result = _sut.Update("JSMITH", "Jonathan Smith", "UK", "London", originalCustomer.PostalCode);

            //Assert
            Assert.That(originalCustomer.ContactName, Is.EqualTo("Jonathan Smith"));
            Assert.That(originalCustomer.City, Is.EqualTo("London"));
        }

        [Test]
        public void ReturnFalse_WhenUpdateIsCalled_WithInvalidId()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(cs => cs.GetCustomerById("JMSITH")).Returns((Customer)null); //MockCustomerService needs to return a customer so we need to cast Customer to null
            _sut = new CustomerManager(mockCustomerService.Object);

            //Act
            var result = _sut.Update("JSMITH", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnTrue_WhenDeleteIsCalled_WithValidId()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer() { CustomerId = "JSMITH" };

            mockCustomerService.Setup(cs => cs.GetCustomerById("JSMITH")).Returns(originalCustomer);

            _sut = new CustomerManager(mockCustomerService.Object);

            //Act
            var result = _sut.Delete("JSMITH");

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ReturnFalse_WhenDeleteIsCalled_WithInvalidId()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer() { CustomerId = "JSMITH" };

            mockCustomerService.Setup(cs => cs.GetCustomerById("JSMITH")).Returns((Customer)null);

            _sut = new CustomerManager(mockCustomerService.Object);

            //Act
            var result = _sut.Delete("JSMITH");

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void DeleteCustomer_WhenDeleteIsCalled_WithValidId()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer() { CustomerId = "JSMITH" };

            mockCustomerService.Setup(cs => cs.GetCustomerById("JSMITH")).Returns(originalCustomer);

            _sut = new CustomerManager(mockCustomerService.Object);

            //Act
            _sut.Delete("JSMITH");

            //Assert
            Assert.That(_sut.SelectedCustomer, Is.Null);
        }

        [Test]
        public void ReturnFalse_WhenUpdateIsCalled_AndADatabaseExceptionOccurs()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(cs => cs.GetCustomerById("JSMITH")).Returns(new Customer() { CustomerId = "JSMITH"});
            mockCustomerService.Setup(cs => cs.SaveCustomerChanges()).Throws<DataException>();

            _sut = new CustomerManager(mockCustomerService.Object);

            //Act
            var result = _sut.Update("JSMITH", "", "", "", "");

            //Assert
            Assert.That(result, Is.False);

        }

        [Test]
        public void CallSaveCustomerChangesOnce_WhenUpdateIsCalled_WithValidId()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer() { CustomerId = "JSMITH" };

            mockCustomerService.Setup(cs => cs.GetCustomerById("JSMITH")).Returns(originalCustomer);

            _sut = new CustomerManager(mockCustomerService.Object);

            //Act
            _sut.Update("JSMITH", "", "", "", "");

            //Assert
            mockCustomerService.Verify(cs => cs.SaveCustomerChanges(), Times.Once);
        }

        [Test]
        public void LetsSeeWhatHappens_WhenUpdateIsCalled_IfAllInvocationsArentSetUp()
        {
            // Arrange
            var mockCustomerService = new Mock<ICustomerService>(MockBehavior.Loose);
            mockCustomerService.Setup(cs => cs.GetCustomerById(It.IsAny<string>())).Returns(new Customer());
            mockCustomerService.Setup(cs => cs.SaveCustomerChanges());
            _sut = new CustomerManager(mockCustomerService.Object);
            // Act
            var result = _sut.Update("ROCK", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            // Assert
            Assert.That(result);
        }

        //Retrieve all tests
        [Test]
        public void ReturnCustomers_WhenCustomersExist()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomers = new List<Customer>() { new Customer(), new Customer(), new Customer()};
            mockCustomerService.Setup(cs => cs.GetCustomerList()).Returns(originalCustomers);
            _sut = new CustomerManager(mockCustomerService.Object);

            //Act
            var result = _sut.RetrieveAll();

            //Assert
            Assert.That(result, Is.EqualTo(originalCustomers));
        }

        [Test]
        public void ReturnAnEmptyCustomersList_WhenCustomersDoNotExist()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomers = new List<Customer>();
            mockCustomerService.Setup(cs => cs.GetCustomerList()).Returns(originalCustomers);
            _sut = new CustomerManager(mockCustomerService.Object);

            //Act
            var result = _sut.RetrieveAll();
            var expected = new List<Customer>();

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        //Set selected customer tests
        [Test]
        public void SelectsCustomer_WhenCustomerExists()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var originalCustomer = new Customer();
            _sut = new CustomerManager(mockCustomerService.Object);

            //Act
            _sut.SetSelectedCustomer(originalCustomer);

            //Assert
            Assert.That(_sut.SelectedCustomer, Is.EqualTo(originalCustomer));
        }
    }
}

