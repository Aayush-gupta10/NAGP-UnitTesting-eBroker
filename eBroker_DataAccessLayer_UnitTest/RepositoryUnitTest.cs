using eBroker_App_DataAccessLayer;
using eBroker_App_DataAccessLayer.Models;
using eBroker_App_DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace eBroker_DataAccessLayer_UnitTest
{
    public class RepositoryUnitTest
    {
        public RepositoryImplementation<Equity> _repository;
        public TraderEquityDBContext _context;

        private RepositoryImplementation<Equity> Seed()
        {
            var options = new DbContextOptionsBuilder<TraderEquityDBContext>()
                .UseInMemoryDatabase(databaseName: "TraderEquityDBContext")
                .Options;


            
            var equities = new List<Equity>()
                {
                     new Equity
                      {
                         EquityID = 1,
                        EquityName = "HDFC",
                        EquityValue = 100
                      },
                      new Equity
                      {
                        EquityID = 2,
                        EquityName = "Axis",
                        EquityValue = 500
                      },
                      new Equity
                      {
                        EquityID = 3,
                        EquityName = "SBI",
                        EquityValue = 1000
                      },
                      new Equity
                      {
                        EquityID = 4,
                        EquityName = "BoB",
                        EquityValue = 2000
                      }
                };

            _context = new TraderEquityDBContext(options);
            _context.Equity.RemoveRange(_context.Equity);
            _context.Equity.AddRange(equities);
            _context.SaveChanges();
            _repository = new RepositoryImplementation<Equity>(_context);
            return _repository;
        }

        [Fact]
        public void GetAll_Then_Return_All_equities()
        {
            // Arrange
            Seed();

            // Act
            var result = _repository.GetAll().ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void Get_Then_Return_equities()
        {
            // Arrange
            Seed();

            // Act
            var result = _repository.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.EquityID);
        }

        [Fact]
        public void Add_Then_Return_Added_Trader()
        {
            // Arrange
            Seed();

            // Act
            var result = _repository.Add(new Equity()
            {
               EquityID = 5,
               EquityName = "PNB",
               EquityValue = 5000
            });

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Update()
        {
            // Arrange
            Seed();
            var equity = _context.Equity.First();
            equity.EquityValue = 10000;

            // Act
            var result = _repository.Update(equity, equity.EquityID);

            // Assert
            Assert.Equal(10000, result.EquityValue);
        }

        [Fact]
        public void Update_return_null_if_object_null()
        {
            // Arrange
            Seed();
            Equity trader = null;

            // Act
            var result = _repository.Update(trader);

            // Assert
            Assert.Null(result);
        }
    }
}
