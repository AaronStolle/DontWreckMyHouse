using System;


namespace DontWreckMyHouse
{
    
    public enum MainMenuOption
    {
        Exit,
        ViewReservations,
        AddReservation,
        EditReservation,
        RemoveReservation
    }
    public static class MainMenuOptionExtensions
    {
        public static string ToLabel(this MainMenuOption option) => option switch
        {
            MainMenuOption.Exit => "Exit",
            MainMenuOption.ViewReservations => "View Reservations for Host",
            MainMenuOption.AddReservation => "Make a Reservation",
            MainMenuOption.EditReservation => "Edit a Reservation",
            MainMenuOption.RemoveReservation => "Cancel a Reservation",
            _ => throw new NotImplementedException()
        };
    }
}
