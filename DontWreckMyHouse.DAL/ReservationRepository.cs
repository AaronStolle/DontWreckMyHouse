using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DontWreckMyHouse.DAL
{
    public class ReservationRepository : IReservation
    {
        private const string HEADER = "id,start_date,end_date,guest_id,total";
        private readonly string _dataDirPath;
        public ICustomeFormatter<Reservation> Formatter { get; set; }

        public ReservationRepository(string dataDirectoryPath, ICustomeFormatter<Reservation> formatter)
        {
            _dataDirPath = dataDirectoryPath;
            Formatter = formatter;
        }

        public Result<Reservation> Add(Reservation reservation)
        {
            List<Reservation> reservations = FindAllByHostId(reservation.host.Id).Data;
            if(reservations == null)
            {
                File.Create(Path.Combine(_dataDirPath, reservation.host.Id.ToString()));
            }
            Result<Reservation> result = new Result<Reservation>();
            int nextId = (reservations.Count == 0) ? 0 : reservations.Max(i => i.Id);
            reservation.Id = nextId + 1;
            reservations.Add(reservation);
            Write(reservations, reservation.host.Id);
            result.Data = reservation;
            return result;
        }

        private void Write(List<Reservation> reservations, Guid hostId)
        {
            try
            {
                using StreamWriter sw = new StreamWriter(GetFilePath(hostId));
                sw.WriteLine(HEADER);

                if(reservations == null)
                {
                    return;
                }
                foreach(Reservation reservation in reservations)
                {
                    sw.WriteLine(Formatter.Serialize(reservation));
                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public bool Delete(Reservation reservation)
        {
            List<Reservation> reservations = FindAllByHostId(reservation.host.Id).Data;
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations[i].Id == reservation.Id)
                {
                    reservations[i] = reservation;
                    reservations.Remove(reservations[i]);
                    Write(reservations, reservation.host.Id);
                    return true;
                }
            }
            return false;
        }

        public bool Edit(Reservation reservation)
        {
            List<Reservation> reservations = FindAllByHostId(reservation.host.Id).Data;
            for (int i = 0; i < reservations.Count; i++)
            {
                if(reservations[i].Id == reservation.Id)
                {
                    reservations[i] = reservation;
                    Write(reservations, reservation.host.Id);
                    return true;
                }
            }return false;
        }

        public Result<List<Reservation>> FindAllByHostId(Guid hostId2Find)
        {
            // This will be all of the reservations taken from the one file of the host (if they have reservations)
            Result<List<Reservation>> result = new();

            // Need to start a list in order to add the file contents (reservations)
            result.Data = new List<Reservation>();
            var path = GetFilePath(hostId2Find);
            if (!File.Exists(path))
            {
                result.Success = false;
                result.Message = "I couldn't find any reservations";
                return result;
            }
            
            try
            {
                // If the file is there, then path is the filename CSV with Guid.
                // TODO: Verify presence of ifle and send back isSuccess = fasle
                using StreamReader sr = new StreamReader(path);
                
                string? data = sr.ReadLine();
                
                data = sr.ReadLine();
                while (data != null)
                {
                    // We are now reading a reseravation line by line from the exact host file!
                    Reservation reservation = Formatter.Deserialize(data);
                    if (reservation != null)
                    {
                        reservation.host.Id = hostId2Find;
                        result.Data!.Add(reservation);
                        result.Success = true;
                    }

                    // Read the next line
                    data = sr.ReadLine();
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }           
        }

        private string GetFilePath(Guid hostId2Find)
        {
            return Path.Combine(_dataDirPath, $"{hostId2Find}.csv");
        }

        public Result<Reservation> FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
