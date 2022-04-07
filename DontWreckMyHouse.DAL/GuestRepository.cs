﻿using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DontWreckMyHouse.DAL
{
    public class GuestRepository : IGuest
    {
        public string DataFilePath { get; set; }
        public ICustomeFormatter<Guest> Formatter { get; set; }

        public GuestRepository(string dataFilePath, ICustomeFormatter<Guest> formatter)
        {
            DataFilePath = dataFilePath;
            Formatter = formatter;
        }
        public Result<Guest> FindByEmail(string email)
        {
            Result<Guest> result = new();

            try
            {
                using (StreamReader sr = new StreamReader(DataFilePath))
                {
                    string data = sr.ReadToEnd();
                    data = sr.ReadLine();
                    while(data != null)
                    {
                        Guest guest = Formatter.Deserialize(data);
                        if(guest.Email == email)
                        {
                            result.Data = guest;
                            result.Success = true;
                            return result;
                        }
                        data = sr.ReadLine();
                    }
                }
            }catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }

            result.Success = false;
            result.Message = "I couldn't find the email.";
            return result;
        }
    }
}