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
                new Location {ID=1, Country="London", Discrict = "0", LocationDescription = "skajdh", LocationName="London Eye"},
                new Location {ID=2, Country="London", Discrict = "0", LocationDescription = "skajdh", LocationName="London Tower"},
                new Location {ID=3, Country="Japan", Discrict = "Hiroshima", LocationDescription = "skajdh", LocationName="Tokyo Tower"}
            };
            locations.ForEach(location => context.Location.Add(location));
            context.SaveChanges();

            var tours = new List<Tour>
            {
                new Tour{TourID = 1, TourName = "London", TourDescription = "Tour di London", TourPrice = "10000000"},
                new Tour{TourID = 2, TourName = "Tokyo", TourDescription = "Tour di Tokyo", TourPrice = "10000000"},
                new Tour{TourID = 3, TourName = "Shanghai", TourDescription = "Tour di Shahai", TourPrice = "10000000"}
            };
            tours.ForEach(tour => context.Tour.Add(tour));
            context.SaveChanges();

            var tourGroups = new List<TourGroup>
            {
                new TourGroup {Name="Group 1", Description = "ABC XYZ", LeaveDate = DateTime.Parse("2018-03-03"), ReturnDate = DateTime.Parse("2018-3-10"), Status= TStatus.Scheduled, NumberOfPeople = 30, TourID = 1},
                new TourGroup {Name="Group 1", Description = "ABC XYZ", LeaveDate = DateTime.Parse("2018-03-13"), ReturnDate = DateTime.Parse("2018-3-20"), Status= TStatus.Scheduled, NumberOfPeople = 40, TourID = 1},
                new TourGroup {Name="Group 1", Description = "ABC XYZ", LeaveDate = DateTime.Parse("2018-03-23"), ReturnDate = DateTime.Parse("2018-4-5"), Status= TStatus.Scheduled, NumberOfPeople = 35, TourID = 1},
                new TourGroup {Name="Group 1", Description = "ABC XYZ", LeaveDate = DateTime.Parse("2018-03-03"), ReturnDate = DateTime.Parse("2018-3-10"), Status= TStatus.Scheduled, NumberOfPeople = 35, TourID = 2},
                new TourGroup {Name="Group 1", Description = "ABC XYZ", LeaveDate = DateTime.Parse("2018-03-03"), ReturnDate = DateTime.Parse("2018-3-10"), Status= TStatus.Scheduled, NumberOfPeople = 35, TourID = 3}
            };
            tourGroups.ForEach(tourgroup => context.TourGroup.Add(tourgroup));
            context.SaveChanges();

            var tourDetail = new List<TourDetail>
            {
                new TourDetail {TourID = 1, LocationID = 1 },
                new TourDetail {TourID = 1, LocationID = 2 },
                new TourDetail {TourID = 3, LocationID = 3}
            };
            tourDetail.ForEach(tourD => context.TourDetail.Add(tourD));

            context.SaveChanges();

            base.Seed(context);
        }
    }
}