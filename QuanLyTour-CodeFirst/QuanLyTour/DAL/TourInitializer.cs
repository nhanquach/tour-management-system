using QuanLyTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.DAL
{
    public class TourInitializer : System.Data.Entity.DropCreateDatabaseAlways<TourContext>
    {
        protected override void Seed(TourContext context)
        {
            var locations = new List<Location>
            {
                new Location {LocationID=1, Country="London", LocationDescription = "skajdh", LocationName="London Eye" },
                new Location {LocationID=2, Country="London", LocationDescription = "skajdh", LocationName="London Tower"},
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
            tourGroups.ForEach(tourgroup => context.TourGroup.Add(tourgroup));
            context.SaveChanges();

            var groupDetail = new List<TourGroupDetail>
            {
                new TourGroupDetail { TourID = 1, TourGroup = 1},
                new TourGroupDetail { TourID = 1, TourGroup = 2},
                new TourGroupDetail { TourID = 2, TourGroup = 3}
            };
            groupDetail.ForEach(tourD => context.TourGroupDetail.Add(tourD));

            context.SaveChanges();
        }
    }
}