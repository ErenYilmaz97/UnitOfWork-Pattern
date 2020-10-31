using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Results;
using Entities.Entities;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json;

namespace MVC.ApiServices
{
    public class CategoryApiService
    {

        private readonly HttpClient _httpClient;


        //DI
        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IDataResult<List<Category>>> GetAll()
        {
            var response = await _httpClient.GetAsync("categories");

            if (response.IsSuccessStatusCode)
            {
                return new SuccessDataResult<List<Category>>(JsonConvert.DeserializeObject<List<Category>>(await response.Content.ReadAsStringAsync()));
            }

            return new ErrorDataResult<List<Category>>(await response.Content.ReadAsStringAsync()); 
        }




        public async Task<IDataResult<Category>> GetCategory(int categoryID)
        {
            var response = await _httpClient.GetAsync($"categories/{categoryID}");

            if (response.IsSuccessStatusCode)
            {
                return new SuccessDataResult<Category>(JsonConvert.DeserializeObject<Category>(await response.Content.ReadAsStringAsync()));
            }

            return new ErrorDataResult<Category>(await response.Content.ReadAsStringAsync());
        }





        public async Task<IDataResult<List<Category>>> GetAllWithProducts()
        {
            var response = await _httpClient.GetAsync("categories/withproducts");


            if (response.IsSuccessStatusCode)
            {
                return new SuccessDataResult<List<Category>>(JsonConvert.DeserializeObject<List<Category>>(await response.Content.ReadAsStringAsync()));
            }

            return new ErrorDataResult<List<Category>>(await response.Content.ReadAsStringAsync());
        }





        public async Task<IResult> AddCategory(Category category)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(category),Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("categories", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessResult(await response.Content.ReadAsStringAsync());
            }


            return new ErrorResult(await response.Content.ReadAsStringAsync());
        }




        public async Task<IResult> DeleteCategory(int categoryID)
        {
            var response = await _httpClient.DeleteAsync($"categories/{categoryID}");

            if (response.IsSuccessStatusCode)
            {
                return new SuccessResult(await response.Content.ReadAsStringAsync());
            }

            return new ErrorResult(await response.Content.ReadAsStringAsync());
        }




        public async Task<IResult> UpdateCategory(Category category)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(category),Encoding.UTF8,"application/json");
            var response = await _httpClient.PutAsync("categories", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessResult(await response.Content.ReadAsStringAsync());
            }

            return new ErrorResult(await response.Content.ReadAsStringAsync());
        }

    }


    }
