using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL
{
    public class ReservationService
    {
        private readonly IReservation reservationRepository;
        private readonly IHost hostRepository;
        private readonly IGuest guestRepository;
        
        public ReservationService(IReservation reservationRepository, IGuest guestRepository, IHost hostRepository)
        {
            this.reservationRepository = reservationRepository;
            this.guestRepository = guestRepository;
            this.hostRepository = hostRepository;
        }

        public Result<List<Reservation>> FindAllByHostId(Guid hostId2Find)
        {
            return reservationRepository.FindAllByHostId(hostId2Find);
        }

        public Result<Reservation> Add(Reservation reservation)
        {
            Result<Reservation> result = new();
            result = reservationRepository.Add(reservation);
            return result;
        }

        private Result<Reservation> Validate(Reservation reservation)
        {
            Result<Reservation> result = ValidateNulls(reservation);
            if (!result.Success)
            {
                return result;
            }
            ValidateFields(reservation, result);
            if (!result.Success)
            {
                return result;
            }
            ValidateChildrenExist(reservation, result);
            if (!result.Success)
            {
                return result;
            }
            List<Reservation> records = FindAllByHostId(reservation.host.Id).Data;
            
            //foreach(var record in records)
            //{
            //    for (var day = record.StartDate.Date; day <= record.EndDate.Date; day = day.AddDays(1))
            //    {
            //        for (var reservationDay = reservation.StartDate.Date; reservationDay <= reservation.EndDate.Date; reservationDay = reservationDay.AddDays(1))
            //        {
            //            if(day == reservationDay)
            //            {
            //                result.Message = "reservation dates cannot overlap with reservations already booked";
            //            }
            //        }
            //    }

            //}
            var reservationAttempt = records.Where(q => q.Id != reservation.Id);

            var currentReservation = reservationAttempt.FirstOrDefault(
                q => (q.StartDate <= reservation.StartDate && q.EndDate >= reservation.StartDate)
                || (q.StartDate <= reservation.EndDate && q.EndDate >= reservation.EndDate)
                || (q.StartDate <= reservation.StartDate && q.EndDate >= reservation.EndDate)
                || (q.StartDate >= reservation.StartDate && q.EndDate <= reservation.EndDate));

            if (currentReservation != null)
            {
                result.Message = "reservation dates cannot overlap with reservations already booked";
                result.Success = false;
                return result;
            }
            result.Success = true;
            return result;
            

        }

        public bool Delete(Reservation reservation)
        {
            return reservationRepository.Delete(reservation);
        }

        private void ValidateChildrenExist(Reservation reservation, Result<Reservation> result)
        {
            if (reservation.host.Id == null || hostRepository.FindByEmail(reservation.host.Email) == null)
            {
                result.Message = "Host does not exist";
                result.Success = false;
            }
            if (reservation.host.Id == null || guestRepository.FindByEmail(reservation.guest.Email) == null)
            {
                result.Message = "Guest does not exist";
                result.Success=false;
            }result.Success = true;
        }

        public Result<Reservation> Reservation2Edit(Reservation oldReservation, Reservation newReservation)
        {
            Result<Reservation> result = new Result<Reservation>();
            if(newReservation.StartDate == DateTime.MinValue)
            {
                newReservation.StartDate = oldReservation.StartDate;
            }
            else
            {
                newReservation.StartDate = newReservation.StartDate;
            }
            if (newReservation.EndDate == DateTime.MinValue)
            {
                newReservation.EndDate = oldReservation.EndDate;
            }
            else
            {
                newReservation.EndDate = newReservation.EndDate;
            }
            result.Success = true;
            result = Validate(newReservation);

            if (!result.Success)
            {
                return result;
            }
            newReservation.Total = newReservation.Value();
            newReservation.Id = oldReservation.Id;
            result.Data = newReservation;
            return result;
        }

        public bool Edit(Reservation reservation)
        {
            return reservationRepository.Edit(reservation);
        }

        private void ValidateFields(Reservation reservation, Result<Reservation> result)
        {
            if (reservation.StartDate < DateTime.Today)
            {
                result.Message = "Start Date cannot be in the past";
                result.Success = false;
                
            }
            if (reservation.StartDate > reservation.EndDate)
            {
                result.Message = "Start Date cannot come before End Date.";
                result.Success = false;
                
            }result.Success = true;
        }

        public Result<Reservation> FindById(List<Reservation> reservations, int reservationId)
        {
            Result<Reservation> result = new Result<Reservation>();
            Reservation reservation = reservations.FirstOrDefault(r => r.Id == reservationId);
            if(reservation == null)
            {
                result.Message = $"Reservation with Id {reservationId} does not exist";
                result.Success = false;
            }
            result.Success = true;
            result.Data = reservation;
            return result;
        }

        private Result<Reservation> ValidateNulls(Reservation reservation)
        {
            Result<Reservation> result = new Result<Reservation>();
            if(reservation == null)
            {
                result.Message = "Reservation is null.";
                result.Success = false;
                return result;
            }
            if(reservation.host == null)
            {
                result.Message = "Host is null.";
                result.Success=false;
                return result;
            }
            if (reservation.guest == null)
            {
                result.Message = "Guest is null.";
                result.Success = false;
                return result;
            }
            result.Success = true;
            return result;
        }

        public Result<Reservation> Make(Reservation reservation)
        {
            Result<Reservation> result = Validate(reservation);
            if (!result.Success)
            {
                return result;
            }
            reservation.Total = reservation.Value();
            result.Data = reservation;
            return result;
        }
    }
}
//host:
//smcgreary20@upenn.edu
//nmclurg1s@umn.edu
//krhodes1@posterous.com
//guest: slomas0@mediafire.com