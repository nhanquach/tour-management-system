using QuanLyTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.DAL
{
    public class TourInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TourContext>
    {
        protected override void Seed(TourContext context)
        {
            var LondonEyeDescription = "The London Eye, known for sponsorship reasons as the Coca-Cola London Eye, is a giant Ferris wheel on the South Bank of the River Thames in London. The structure is 443 feet(135 m) tall and the wheel has a diameter of 394 feet(120 m).When it opened to the public in 2000 it was the world's tallest Ferris wheel. Its height was surpassed by the 525-foot (160 m) Star of Nanchang in 2006, the 541-foot (165 m) Singapore Flyer in 2008, and the 550-foot (167.6 m) High Roller (Las Vegas) in 2014. Supported by an A-frame on one side only, unlike the taller Nanchang and Singapore wheels, the Eye is described by its operators as the world's tallest cantilevered observation wheel.";
            var BigBenTowerDescription = "Big Ben is the nickname for the Great Bell of the clock at the north end of the Palace of Westminster in London and is usually extended to refer to both the clock and the clock tower.[ The official name of the tower in which Big Ben is located was originally the Clock Tower, but it was renamed Elizabeth Tower in 2012 to mark the Diamond Jubilee of Elizabeth II.";
            var locations = new List<Location>
            {
                new Location {LocationID=1, Country="England", LocationDescription = LondonEyeDescription, LocationName="London Eye" },
                new Location {LocationID=2, Country="England", LocationDescription = BigBenTowerDescription, LocationName="Big Ben"},
                new Location {LocationID=3, Country="Japan", LocationDescription = "skajdh", LocationName="Tokyo Tower"},
                new Location {LocationID=4, Country="Shanghai", LocationDescription = "skajdh", LocationName="Bng"},
                new Location {LocationID=5, Country="HongKong", LocationDescription = "skajdh", LocationName="HKT"}
            };
            locations.ForEach(location => context.Locations.Add(location));

            var tours = new List<Tour>
            {
                new Tour{TourID = 1, TourName = "London", TourDescription = "Tour to London", TourPrice = "12 000 000"},
                new Tour{TourID = 2,TourName = "Tokyo", TourDescription = "Tour to Tokyo", TourPrice = "13 000 000"},
                new Tour{TourID = 3,TourName = "Shanghai", TourDescription = "Tour to Shahai", TourPrice = "14 000 000"},
                new Tour{TourID = 4,TourName = "HongKong", TourDescription = "Tour to HongKong", TourPrice = "1 000 000"}
            };
            tours.ForEach(tour => context.Tours.Add(tour));

            var tourDetail = new List<TourDetail>
            {
                new TourDetail {TourID = 1, LocationID = 1},
                new TourDetail {TourID = 1, LocationID = 2},
                new TourDetail {TourID = 2, LocationID = 3},
                new TourDetail {TourID = 3, LocationID = 1},
                new TourDetail {TourID = 3, LocationID = 2},
                new TourDetail {TourID = 3, LocationID = 3},
                new TourDetail {TourID = 4, LocationID = 4},
            };
            tourDetail.ForEach(d => context.TourDetails.Add(d));
            context.SaveChanges();
            
            var tourGroups = new List<TourGroup>
            {
                new TourGroup {Name="Group 1", Description = "ABC XYZ",
                    LeaveDate = DateTime.Parse("2018-03-03"),
                    ReturnDate = DateTime.Parse("2018-3-10"),
                    Status = TStatus.Scheduled, NumberOfPeople = 30,
                    TourID = 1},
                new TourGroup {Name="Group 2", Description = "ABC XYZ", LeaveDate = DateTime.Parse("2018-03-13"), ReturnDate = DateTime.Parse("2018-3-20"), Status= TStatus.Scheduled, NumberOfPeople = 40, TourID = 1},
                new TourGroup {Name="Group 3", Description = "ABC XYZ", LeaveDate = DateTime.Parse("2018-03-23"), ReturnDate = DateTime.Parse("2018-4-5"), Status= TStatus.Scheduled, NumberOfPeople = 35, TourID = 1},
                new TourGroup {Name="Group 1", Description = "ABC XYZ", LeaveDate = DateTime.Parse("2018-03-03"), ReturnDate = DateTime.Parse("2018-3-10"), Status= TStatus.Scheduled, NumberOfPeople = 35, TourID = 2},
                new TourGroup {Name="Group 1", Description = "ABC XYZ", LeaveDate = DateTime.Parse("2018-03-03"), ReturnDate = DateTime.Parse("2018-3-10"), Status= TStatus.Scheduled, NumberOfPeople = 35, TourID = 3}
            };
            tourGroups.ForEach(tourgroup => context.TourGroups.Add(tourgroup));
            context.SaveChanges();

            var groupDetail = new List<TourGroupDetail>
            {
                new TourGroupDetail { TourID = 1, TourGroup = 1},
                new TourGroupDetail { TourID = 1, TourGroup = 2},
                new TourGroupDetail { TourID = 2, TourGroup = 3}
            };
            groupDetail.ForEach(tourD => context.TourGroupDetails.Add(tourD));

            context.SaveChanges();
        }
    }
}