﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSonic.Impl;
using NSonic.Impl.Net;

namespace NSonic.Tests
{
    [TestClass]
    public class SonicSessionFactoryTests
    {
        private Mock<ITcpClient> client;

        private SonicSessionFactory factory;

        [TestInitialize]
        public void Initialize()
        {
            this.client = new Mock<ITcpClient>();

            this.factory = new SonicSessionFactory(this.client.Object);
        }

        [TestMethod]
        public void ShouldBeAbleToCreateSonicSession()
        {
            // Arrange

            var environment = new EnvironmentResponse(1, 42);

            // Act / Assert

            using (var result = this.factory.Create(environment))
            {
                Assert.AreSame(this.client.Object, ((SonicSession)result).Client);
                Assert.AreEqual(environment, ((SonicSession)result).Environment);
            }
        }

        [TestMethod]
        public void ShouldDisposeItsTcpClient()
        {
            // Act

            this.factory.Dispose();

            // Assert

            this.client.Verify(c => c.Dispose(), Times.Once);
        }
    }
}
