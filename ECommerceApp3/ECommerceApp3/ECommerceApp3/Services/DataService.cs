﻿using ECommerceApp3.Data;
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
        }
    }

