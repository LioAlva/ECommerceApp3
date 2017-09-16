using ECommerceApp3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp3.Services
{
    public class ApiService
    {
        public async Task<Response> Login(string email,string password)
        {
            try
            {
                var loginRequest = new LoginRequest
                {
                    Email=email,
                    Password=password,
                };
                var request = JsonConvert.SerializeObject(loginRequest);
                var content =new  StringContent(request,Encoding.UTF8,"application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                //client.BaseAddress = new Uri("http://luisperseo-001-site1.itempurl.com");
                //var url = "/api/Users/Login";
                var url = "/ECommerce/api/Users/Login";
                var response = await client.PostAsync(url,content);

                if(!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess=false,
                        Message="Usuario o contraseña incorrecta"
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = user
                };

            }
            catch (Exception ex)
            {
                return new Response {
                    IsSuccess = false,
                    Message=ex.Message
                };
            }
        }

        //public async Task<List<Product>> GetProducts()
        //{
        //    try
        //    {
        //        var client = new HttpClient()
        //        {
        //            BaseAddress = new Uri("http://zulu-software.com")
        //        };
        //        //client.BaseAddress = new Uri("http://luisperseo-001-site1.itempurl.com");
        //        var url = "/ECommerce/api/Products";
        //        var response = await client.GetAsync(url);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return null;
        //        }

        //        var result = await response.Content.ReadAsStringAsync();
        //        var products = JsonConvert.DeserializeObject<List<Product>>(result);
        //        return products.OrderBy(p=>p.Description).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public async Task<List<Customer>> GetCustomers()
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri("http://zulu-software.com");
        //        //client.BaseAddress = new Uri("http://luisperseo-001-site1.itempurl.com");
        //        var url = "/ECommerce/api/Customers";
        //        var response = await client.GetAsync(url);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return null;
        //        }

        //        var result = await response.Content.ReadAsStringAsync();
        //        var customers = JsonConvert.DeserializeObject<List<Customer>>(result);
        //        return customers.OrderBy(c => c.FirstName)
        //            .ThenBy(c=>c.LastName)//primero ordena por firsname luego por lasname
        //            .ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}


        public async Task<List<T>> Get<T>(string controller) where T:class 
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                //client.BaseAddress = new Uri("http://luisperseo-001-site1.itempurl.com");
                var url =string.Format("/ECommerce/api/{0}",controller);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<T>>(result);
                return lista;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Response> NewCustomer(Customer customer)
        {
            try
            {
                var request = JsonConvert.SerializeObject(customer);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://zulu-software.com");
                //client.BaseAddress = new Uri("http://luisperseo-001-site1.itempurl.com");
                //var url = "/api/Users/Login";
                var url = "/ECommerce/api/Customer";
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                var newCustomer = JsonConvert.DeserializeObject<Customer>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Cliente creado Ok",
                    Result = newCustomer
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }
    }
}
