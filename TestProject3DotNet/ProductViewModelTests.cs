using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace TestProject3DotNet
{
    public class ProductViewModelTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void DisplayErrorMissingName_EmptyValue(string name = "")
        {
            // Arrange
            ProductViewModel productM = new ProductViewModel
            {
                Id = 1,
                Stock = "1",
                Price = "12,34",
                Details = "un détail",
                Description = "description",
                Name = name
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productM, new ValidationContext(productM), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "MissingName");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void DisplayErrorMissingStock_EmptyValue(string stock = "")
        {
            // Arrange
            ProductViewModel productM = new ProductViewModel
            {
                Id = 1,
                Stock = stock,
                Price = "12,34",
                Details = "un détail",
                Description = "description",
                Name = "un nom"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productM, new ValidationContext(productM), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "MissingStock");
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("0")]
        public void DisplayErrorStockNotGreaterThanZero(string stock)
        {
            // Arrange
            ProductViewModel productM = new ProductViewModel
            {
                Id = 1,
                Stock = stock,
                Price = "12,34",
                Details = "un détail",
                Description = "description",
                Name = "un nom"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productM, new ValidationContext(productM), validationResults, true);

            // Assert
            Assert.True(Convert.ToInt16(productM.Stock) < 1);
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "StockNotGreaterThanZero");
        }

        [Theory]
        [InlineData("-1,5", false)]
        [InlineData("1.5", false)]
        public void DisplayErrorStockStockNotAnInteger(string stock, bool expected)
        {
            // Arrange
            ProductViewModel productM = new ProductViewModel
            {
                Id = 1,
                Stock = stock,
                Price = "12,34",
                Details = "un détail",
                Description = "description",
                Name = "un nom"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productM, new ValidationContext(productM), validationResults, true);

            // Assert
            Assert.Equal(false, expected);
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "StockNotAnInteger");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void DisplayErrorMissingPrice_EmptyValue(string price = "")
        {
            // Arrange
            ProductViewModel productM = new ProductViewModel
            {
                Id = 1,
                Stock = "1",
                Price = price,
                Details = "un détail",
                Description = "description",
                Name = "un nom"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productM, new ValidationContext(productM), validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "MissingPrice");
        }

        [Theory]
        [InlineData("-1.5", false)]
        [InlineData("1.5e", false)]
        [InlineData("1,5", false)]
        public void DisplayErrorPriceNotANumber(string price, bool expected)
        {
            // Arrange
            ProductViewModel productM = new ProductViewModel
            {
                Id = 1,
                Stock = "1",
                Price = price,
                Details = "un détail",
                Description = "description",
                Name = "un nom"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productM, new ValidationContext(productM), validationResults, true);

            // Assert
            Assert.Equal(false, expected);
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "PriceNotANumber");
        }

        [Theory]
        [InlineData("-1,5", false)]
        [InlineData("0", false)]
        public void DisplayErrorPriceNotGreaterThanZero(string price, bool expected)
        {
            // Arrange
            ProductViewModel productM = new ProductViewModel
            {
                Id = 1,
                Stock = "1",
                Price = price,
                Details = "un détail",
                Description = "description",
                Name = "un nom"
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(productM, new ValidationContext(productM), validationResults, true);

            // Assert
            Assert.Equal(false, expected);
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "PriceNotGreaterThanZero");
        }
    }
}