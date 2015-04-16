using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiliconShores.Models;

namespace SiliconShores.Controllers
{
    public class HomeController : Controller
    {
        theme_park_dbEntities db = new theme_park_dbEntities();

        public ActionResult Index()
        {
            Random r = new Random();
            var hotelRooms = new List<hotel_rooms>();
            var stopDate = new DateTime(2015, 4, 14);
            
            foreach(var h in db.hotels)
            {
                int numFloors, roomsPerFloor;
                do
                {
                    numFloors = r.Next(2, 5);
                    roomsPerFloor = r.Next(8, 13) * 5;
                } while (numFloors*roomsPerFloor < 40);
                
                var roomTypeSelector = 0;
                for (var floor = 1; floor <= numFloors; floor++)
                {
                    for (var room = 1; room <= roomsPerFloor; room++)
                    {
                        int roomTypeID;

                        if (roomTypeSelector % 20 < 9)
                        {
                            roomTypeID = 1;
                        }
                        else if(roomTypeSelector % 20 < 14)
                        {
                            roomTypeID = 2;
                        }
                        else if (roomTypeSelector%20 < 17)
                        {
                            roomTypeID = 3;
                        }
                        else if (roomTypeSelector%20 < 19)
                        {
                            roomTypeID = 4;
                        }
                        else
                        {
                            roomTypeID = 5;
                        }
                        roomTypeSelector ++;

                        var rate = (55 * Math.Pow(1.5, (double) roomTypeID) - 2.5);
                        rate += rate* ((2 * r.NextDouble() - 1) * 0.1);
                        var rateDec = Decimal.Round((decimal) rate, 2);

                        hotelRooms.Add(new hotel_rooms()
                        {
                            hotel = h,
                            occupied = false,
                            room_number = floor * 100 + room,
                            room_type_id = roomTypeID,
                            room_rate = rateDec
                        });
                    }
                }
            }
            db.hotel_rooms.AddRange(hotelRooms);
            db.SaveChanges();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About us";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us";

            return View();
        }
    }
}