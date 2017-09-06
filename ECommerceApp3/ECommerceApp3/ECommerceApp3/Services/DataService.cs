using ECommerceApp3.Data;
using ECommerceApp3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp3.Services
{
    public class DataService
    {
        public Response InsertUser(User user){
            try
            {
                using (var da= new DataAccess())
                {
                    var oldUser = da.First<User>(false);
                    if (oldUser!=null)
                    {
                        da.Delete(oldUser);
                    }
                    da.Insert(user);
                }

                return new Response
                {
                    IsSuccess=true,
                    Message="Usuario Insertado Ok",
                    Result=user
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message=ex.Message,
                    Result=
                };
            }
        }
    }
}
