// using AutoMapper;
// using FluentAssertions;
// using TestTask.Infrastructure;
// using TestTask.Models;
// using TestTask.Models.Dto;
//
// namespace TestTask.UnitTests;
//
// [TestFixture]
// public class MappingTests
// {
//   private IMapper _mapper;
//   private MapperConfiguration _config;
//
//   [SetUp]
//   public void Setup()
//   {
//     _mapper = MappingConfig.RegisterMaps().CreateMapper();
//   }
//
//   [Test]
//   public void CalculatesTotalPriceWithVatForProducts()
//   {
//     // Arrange
//     var source = new Product
//     {
//       ProductId = 1,
//       Title = "test",
//       Price = 100,
//       Quantity = 15
//     };
//     
//     // Act
//     var destination = _mapper.Map<ProductDto>(source);
//
//     // Assert
//     destination.TotalPriceWithVat
//       .Should()
//       .Be(source.Price * source.Quantity * (1 + VatValue.Value) / 100);
//     }
// }