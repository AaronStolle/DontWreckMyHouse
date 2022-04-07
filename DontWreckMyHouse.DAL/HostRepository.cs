using System;
using System.Collections.Generic;
using System.Linq;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.DAL
{
    public class HostRepository : IHost
    {
        public string DataFilePath { get; set; }
        public ICustomeFormatter<Host> Formatter { get; set; }

        public HostRepository(string dataFilePath, ICustomeFormatter<Host> formatter)
        {
            DataFilePath = dataFilePath;
            Formatter = formatter;
        }
        public Result<Host> FindByEmail(string email)
        {
            Result<Host> result = new();

            try
            {
                using (StreamReader sr = new StreamReader(DataFilePath))
                {
                    string data = sr.ReadLine();
                    data = sr.ReadLine();
                    while(data != null)
                    {
                        Host host = Formatter.Deserialize(data);
                        if(host.Email == email)
                        {
                            result.Data = host;
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
            result.Message = "I couldn't find the email";
            return result;
        }
    }
}
