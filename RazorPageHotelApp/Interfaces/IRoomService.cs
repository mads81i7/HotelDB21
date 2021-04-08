using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Interfaces
{
    public interface IRoomService
    {
        /// <summary>
        /// henter alle værelser fra databasen
        /// </summary>
        /// <returns>Liste af værelser</returns>
        Task<List<Room>> GetAllRoomsAsync();

        /// <summary>
        /// henter alle værelser tilknyttet et hotel specifikt hotel fra databasen
        /// </summary>
        /// <param name="hotelNr">Udpeger det specifikke hotel</param>
        /// <returns>Liste af værelser</returns>
        Task<List<Room>> GetAllRoomsInHotelAsync(int hotelNr);

        /// <summary>
        /// Henter et specifikt værelse fra databasen
        /// </summary>
        /// <param name="roomNr">udpeger det specifikke værelse</param>
        /// <param name="hotelNr">udpeger hvilket hotel det eftersøgte værelse er tilknyttet</param>
        /// <returns>Det specifikke værelse ved success eller NULL hvis et værelse ikke kan findes</returns>
        Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr);

        /// <summary>
        /// Indsætter et nyt værelse i databasen
        /// </summary>
        /// <param name="hotelNr">udpeger hvilket hotel det nye værelse skal tilknyttes</param>
        /// <param name="room">Indeholder de parameter som det nye værelse skal have</param>
        /// <returns>True hvis værelset kunne kreeres og False hvis det ikke kunne kreeres</returns>
        Task<bool> CreateRoomAsync(int hotelNr, Room room);

        /// <summary>
        /// Opdaterer informationerne i et værelse
        /// </summary>
        /// <param name="room">De nye værdier der skal indsættes</param>
        /// <param name="roomNr">bruges til at finde hvilket værelse skal opdateres</param>
        /// <param name="hotelNr">bruges til at finde hvilket hotel som indeholder det værelse der skal opdateres</param>
        /// <returns>True hvis værelset opdateres og false hvis værelset ikke kan opdateres</returns>
        Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr);

        /// <summary>
        /// Sletter et værelse fra databasen
        /// </summary>
        /// <param name="roomNr">roomNr">bruges til at finde hvilket værelse skal slettes</param>
        /// <param name="hotelNr">bruges til at finde hvilket hotel som indeholder det værelse der skal slettes</param>
        /// <returns>Returnere det slettede værelse og NULL hvis værelset ikke eksisterer</returns>
        Task<Room> DeleteRoomAsync(int roomNr, int hotelNr);

        /// <summary>
        /// Henter alle værelser fra et specifikt hotel og med en specifik type
        /// </summary>
        /// <param name="hotelNr">Udpeger hvilket hotel hvis værelser vi ønsker</param>
        /// <param name="roomType">Udpeger hvilken type værelserne vi ønsker skal have</param>
        /// <returns>En liste af værelser</returns>
        Task<List<Room>> GetAllRoomsInHotelWithTypeAsync(int hotelNr, char roomType);

        Task<List<Room>> GetAllRoomsInHotelSortedWithTypeAsync(int hotelNr, char roomType, string sortCriteria, string direction);

        Task<List<Room>> GetAllRoomsInHotelSortedAsync(int hotelNr, string sortCriteria, string direction);
    }
}
