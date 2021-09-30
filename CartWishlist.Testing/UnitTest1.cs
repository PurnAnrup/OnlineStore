using Microsoft.AspNetCore.Mvc;
using Moq;
using ProceedToBuyModule;
using ProceedToBuyModule.Controllers;
using ProceedToBuyModule.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CartWishlist.Testing
{
    public class UnitTest1
    {

    //    {
    //"id": 2,
    //"productId": 2,
    //"zipCode": 530026,
    //"deliveryDate": "2021-10-05T13:20:26.8295003",
    //"vendor": {
    //  "id": 1,
    //  "vendorName": "pradeep",
    //  "deliveryCharges": 20,
    //  "rating": 4.5
    //}

        public Mock<ICartWishhlist> mock = new Mock<ICartWishhlist>();
        [Fact]
        public  void Test_ReturnCart_GetCartById()
        {
            var result = new List<Cart>();


            Cart c = new Cart()
            {
                Id = 2,
                ProductId = 2,
                DeliveryDate = DateTime.Parse("2021-10-05T13:20:26.8295003"),
                Vendor = new Vendor()
                {
                    Id = 1,
                    VendorName = "pradeep",
                    DeliveryCharges = 20,
                    Rating = 4.5
                }
            };
            result.Add(c);


            mock.Setup(p => p.GetCartById(2)).Returns(result);
            
            var controller = new CartMicroserviceController(mock.Object);
            var result1 = controller.GetCartById(2);
            Assert.True(result.Equals(result1.Value));

        }

        [Fact]
        public void Test_ReturnVendor_GetVendor()
        {
            Vendor result = new Vendor()
            {
                Id = 1,
                VendorName = "pradeep",
                DeliveryCharges = 20,
                Rating = 4.5
            };
            mock.Setup(p => p.GetVendor(1)).Returns(result);

            var controller = new CartMicroserviceController(mock.Object);
            var result1 = controller.GetVendor(1);
            Assert.True(result.Equals(result1));
        }

        [Fact]
        public void Test_ReturnNullVendor_GetVendor()
        {
            Vendor vendor = null;
            mock.Setup(p => p.GetVendor(10)).Returns(vendor);
            var controller = new CartMicroserviceController(mock.Object);
            var result1 = controller.GetVendor(10);
            Assert.Null(result1);
        }



//        {
//  "id": 1,
//  "productId": 1,
//  "zipCode": 530026,
//  "deliveryDate": "2021-09-30T12:47:44.435Z",
//  "vendor": {
//    "id": 0,
//    "vendorName": "string",
//    "deliveryCharges": 0,
//    "rating": 0
//  }
//}




//        {
//  "id": 1,
//  "productId": 1,
//  "zipCode": 530026,
//  "deliveryDate": "2021-09-30T12:47:44.435Z",
//  "vendor": {
//    "id": 1,
//    "vendorName": "pradeep",
//    "deliveryCharges": 20,
//    "rating": 4.5
//  }
//}


        [Fact]
        public void Test_StockAvailable_AddProductToCart()
        {
            Cart input = new Cart()
            {
                Vendor = null,
                ProductId = 1,
                DeliveryDate = DateTime.Parse("2021-09-30T12:47:44.435Z"),
                Id=1,
                ZipCode=530026
            };
            Cart result = new Cart()
            {
                ProductId = 1,
                DeliveryDate = DateTime.Parse("2021-09-30T12:47:44.435Z"),
                Id = 1,
                ZipCode = 530026,
                Vendor = new Vendor()
                {
                    Id = 1,
                    VendorName = "pradeep",
                    DeliveryCharges = 20,
                    Rating = 4.5
                }
            };
            mock.Setup(p => p.AddProductToCart(input)).Returns(result);
            var controller = new CartMicroserviceController(mock.Object);
            var result1 = controller.AddProductToCart(input);
            Assert.True(result.Equals(result1.Value));
        }

        [Fact]
        public void Test_StockNotAvailable_AddProductToCart()
        {
            Cart input = new Cart()
            {
                Vendor = null,
                ProductId = 10,
                DeliveryDate = DateTime.Parse("2021-09-30T12:47:44.435Z"),
                Id = 1,
                ZipCode = 530026
            };
            Cart result = new Cart()
            {
                ProductId = 10,
                DeliveryDate = DateTime.Parse("2021-09-30T12:47:44.435Z"),
                Id = 1,
                ZipCode = 530026,
                Vendor = new Vendor()
                {
                    Id = 1,
                    VendorName = "pradeep",
                    DeliveryCharges = 20,
                    Rating = 4.5
                }
            };
            mock.Setup(p => p.AddProductToCart(input)).Returns(input);
            var controller = new CartMicroserviceController(mock.Object);
            var result1 = controller.AddProductToCart(input);
            Assert.False(result.Equals(result1.Value));
        }
        [Fact]
        public void Test_ValidData_AddProductToWishlist()
        {

            mock.Setup(p => p.AddProductToWishlist(1,1)).ReturnsAsync(true);
            //A.CallTo(() => DataStore.SearchProductById(1)).Returns(result);
            var controller = new CartMicroserviceController(mock.Object);
            OkObjectResult result1 = (OkObjectResult) controller.AddProductToWishlist(1,1);
           
            Assert.Equal(200, result1.StatusCode);

        }
    }
}
