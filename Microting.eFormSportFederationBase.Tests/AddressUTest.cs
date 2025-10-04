/*
The MIT License (MIT)

Copyright (c) 2007 - 2025 Microting A/S

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace Microting.eFormSportFederationBase.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    [TestFixture]
    public class AddressUTest : DbTestFixture
    {
        [Test]
        public async Task Address_Create_DoesCreate()
        {
            // Arrange
            var address = new Address
            {
                Street = "123 Main St",
                City = "Springfield",
                State = "IL",
                PostalCode = "62701",
                Country = "USA"
            };

            // Act
            await address.Create(DbContext);

            // Assert
            var addresses = await DbContext.Addresses.AsNoTracking().ToListAsync();
            var addressVersions = await DbContext.AddressVersions.AsNoTracking().ToListAsync();

            Assert.That(addresses, Is.Not.Null);
            Assert.That(addresses.Count, Is.EqualTo(1));
            Assert.That(addresses[0].Street, Is.EqualTo("123 Main St"));
            Assert.That(addresses[0].City, Is.EqualTo("Springfield"));
            Assert.That(addresses[0].State, Is.EqualTo("IL"));
            Assert.That(addresses[0].PostalCode, Is.EqualTo("62701"));
            Assert.That(addresses[0].Country, Is.EqualTo("USA"));
            Assert.That(addresses[0].Version, Is.EqualTo(1));

            Assert.That(addressVersions, Is.Not.Null);
            Assert.That(addressVersions.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Address_Update_DoesUpdate()
        {
            // Arrange
            var address = new Address
            {
                Street = "123 Main St",
                City = "Springfield",
                State = "IL",
                PostalCode = "62701",
                Country = "USA"
            };

            await address.Create(DbContext);

            // Act
            address.Street = "456 Oak Ave";
            await address.Update(DbContext);

            // Assert
            var addresses = await DbContext.Addresses.AsNoTracking().ToListAsync();
            var addressVersions = await DbContext.AddressVersions.AsNoTracking().ToListAsync();

            Assert.That(addresses, Is.Not.Null);
            Assert.That(addresses.Count, Is.EqualTo(1));
            Assert.That(addresses[0].Street, Is.EqualTo("456 Oak Ave"));
            Assert.That(addresses[0].Version, Is.EqualTo(2));

            Assert.That(addressVersions, Is.Not.Null);
            Assert.That(addressVersions.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task Address_Delete_DoesDelete()
        {
            // Arrange
            var address = new Address
            {
                Street = "123 Main St",
                City = "Springfield",
                State = "IL",
                PostalCode = "62701",
                Country = "USA"
            };

            await address.Create(DbContext);

            // Act
            await address.Delete(DbContext);

            // Assert
            var addresses = await DbContext.Addresses.AsNoTracking().ToListAsync();
            var addressVersions = await DbContext.AddressVersions.AsNoTracking().ToListAsync();

            Assert.That(addresses, Is.Not.Null);
            Assert.That(addresses.Count, Is.EqualTo(1));
            Assert.That(addresses[0].WorkflowState, Is.EqualTo(Infrastructure.Constants.Constants.WorkflowStates.Removed));
            Assert.That(addresses[0].Version, Is.EqualTo(2));

            Assert.That(addressVersions, Is.Not.Null);
            Assert.That(addressVersions.Count, Is.EqualTo(2));
        }
    }
}
