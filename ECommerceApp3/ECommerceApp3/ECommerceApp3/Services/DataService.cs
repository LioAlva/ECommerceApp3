using ECommerceApp3.Data;
using ECommerceApp3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceApp3.Services
{
    public class DataService
    {

        public Response UpdateUser(User user)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.Update(user);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario Actualizado Ok",
                    Result = user,
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

            public User GetUser()
            {
                using (var da = new DataAccess())
                {
                    return da.First<User>(true);
                }
            }

            public Response InsertUser(User user) {
                try
                {
                    using (var da = new DataAccess())
                    {
                        var oldUser = da.First<User>(false);
                        if (oldUser != null)
                        {
                            da.Delete(oldUser);
                        }
                        da.Insert(user);
                    }

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Usuario Insertado Ok",
                        Result = user
                    };
                }
                catch (Exception ex)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = ex.Message,
                        // Result=
                    };
                }
            }

        //public void SaveProducts(List<Product> products)
        //{
        //    using (var da=new DataAccess())
        //    {
        //        var oldProducts = da.GetList<Product>(false);
        //        foreach (var  product  in oldProducts)
        //        {
        //            da.Delete(product);
        //        }

        //        foreach (var product in products)
        //        {
        //            da.Insert(product);
        //        }
        //    }
        //}

        //public List<Product> GetProducts()
        //{
        //    using (var da=new DataAccess())
        //    {
        //        return da.GetList<Product>(true).OrderBy(p=>p.Description).ToList();
        //    }
        //}

         public Response Login(string email, string password)
        {
            try
            {
                using (var da=new DataAccess())
                {
                    var user = da.First<User>(true);
                    if (user==null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message="No hay conección a internet y no hay un usuario previo"
                        };
                    }
                    if (user.UserName.ToUpper() == email.ToUpper() && user.Password==password)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Log Ok",
                            Result = user
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario y contraseñaincorrectas"
                    };
                }
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

        public void Save<T>(List<T> list) where T:class
        {
            using (var da = new DataAccess())
            {
                var oldRecords = da.GetList<T>(false);
                foreach (var record in oldRecords)
                {
                    da.Delete(record);
                }

                foreach (var record in list)
                {
                    da.Insert(record);
                }
            }
        }

        public List<Customer> GetCustomers(string filter)
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Customer>(true).
                    OrderBy(c => c.FirstName).
                    ThenBy(c=>c.LastName).
                    Where(c => c.FirstName.ToUpper().Contains(filter.ToUpper())
                    ||         c.LastName.ToUpper().Contains(filter.ToUpper())
                    )
                    .ToList();
            }
        }

        //public List<Customer> GetCustomers()
        //{
        //    using (var da = new DataAccess())
        //    {
        //        return da.GetList<Customer>(true).
        //            OrderBy(c => c.FirstName).
        //            ThenBy(c=>c.LastName).
        //            ToList();
        //    }
        //}

        public List<T> Get<T>(bool widthChildren)  where T:class{
            using (var da=new DataAccess())
            {
                return da.GetList<T>(widthChildren).ToList();
            }
        }



        public List<Product> GetProducts(string filter)
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Product>(true).
                    OrderBy(p => p.Description).
                    Where(p => p.Description.ToUpper().
                    Contains(filter.ToUpper()))
                    .ToList();
            }
        }
    }
}

