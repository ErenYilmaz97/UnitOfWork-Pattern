using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.ApiServices;
using Core.Results;
using Entities.Dto;
using Entities.Entities;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json;

namespace MVC.ApiServices
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;


        //DI
        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IDataResult<List<Product>>> GetAll()
        {
            var response = await _httpClient.GetAsync("products");

            if (response.IsSuccessStatusCode)
            {
                return new SuccessDataResult<List<Product>>(JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync()));
            }

            return new ErrorDataResult<List<Product>>(await response.Content.ReadAsStringAsync());
        }




        public async Task<IDataResult<Product>> Get(int productID)
        {
            var response = await _httpClient.GetAsync($"products/{productID}");

            if (response.IsSuccessStatusCode)
            {
                return new SuccessDataResult<Product>(JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync()));
            }

            return new ErrorDataResult<Product>(await response.Content.ReadAsStringAsync());
        }




        public async Task<IResult> AddProduct(Product product)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(product),Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("products", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessResult(await response.Content.ReadAsStringAsync());
            }


            return new ErrorResult(await response.Content.ReadAsStringAsync());
        }




        public async Task<IResult> UpdateProduct(Product product)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("products", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessResult(await response.Content.ReadAsStringAsync());
            }

            return new ErrorResult(await response.Content.ReadAsStringAsync());
        }





        public async Task<IResult> DeleteProduct(int productID)
        {
            var response = await _httpClient.DeleteAsync($"products/{productID}");

            if (response.IsSuccessStatusCode)
            {
                return new SuccessResult(await response.Content.ReadAsStringAsync());
            }

            return new ErrorResult(await response.Content.ReadAsStringAsync());
        }




        public async Task<IDataResult<List<GetProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var response = await _httpClient.GetAsync("products/withcategory");

            if (response.IsSuccessStatusCode)
            {
                return new SuccessDataResult<List<GetProductWithCategoryDto>>
                    (JsonConvert.DeserializeObject<List<GetProductWithCategoryDto>>(await response.Content.ReadAsStringAsync()));
            }

            return new ErrorDataResult<List<GetProductWithCategoryDto>>(await response.Content.ReadAsStringAsync());
        }



        public async Task<IDataResult<GetProductWithCategoryDto>> GetproductWithCategory(int productID)
        {
            var response = await _httpClient.GetAsync($"products/{productID}/withcategory");


            if (response.IsSuccessStatusCode)
            {
                return new SuccessDataResult<GetProductWithCategoryDto>
                    (JsonConvert.DeserializeObject<GetProductWithCategoryDto>(await response.Content.ReadAsStringAsync()));
            }


            return new ErrorDataResult<GetProductWithCategoryDto>(await response.Content.ReadAsStringAsync());
        }




        public async Task<IDataResult<List<Product>>> GetByCategory(int categoryID)
        {
            var response = await _httpClient.GetAsync($"products/category/{categoryID}");

            if (response.IsSuccessStatusCode)
            {
                return new SuccessDataResult<List<Product>>(JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync()));
            }


            return new ErrorDataResult<List<Product>>(await response.Content.ReadAsStringAsync());
        }
}
}