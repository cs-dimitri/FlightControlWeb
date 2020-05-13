using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models.FlightInfo.FlightBuilder
{
    public class MyFlightBuilder : IFlightBuilder
    {
        private Flight flight = new Flight();

        private struct Location
        {
            public double longitude;
            public double latitude;
        }

        public Flight GetFlight()
        {
            return this.flight;
        }

        public void SetCompany_Name(string name)
        {
            flight.Company_Name = name;
        }


        public void SetFlight_Id(string id)
        {
            flight.Flight_Id = id;
        }

        public void SetIs_External(bool isExternal)
        {
            flight.Is_External = isExternal;
        }

        public void SetPassengers(int passengers)
        {
            flight.Passengers = passengers;
        }

        public void SetDate_Time()
        {
            flight.Date_Time = DateTime.UtcNow;
        }




        public void SetLatitude(FlightPlan flightPlanOutside)
        {
            //flight.Latitude = latitude; // calculation needed!!
            flight.Latitude = CalculateLinInterpolation(flightPlanOutside).latitude;
        }

        private Location CalculateLinInterpolation(FlightPlan flightPlan)
        {
            Location toReturn;
            //////////////////////////////maybe problem ibo they are pointing to the same place 
            DateTime cummultTime = flightPlan.Initial_Location.Date_Time;
            LinkedList<Segment> segments = flightPlan.Segments;
            int index = 0;
            double relation;
            while ((cummultTime = cummultTime.AddSeconds(segments.ElementAt(index).TimeSpan_Seconds)) < DateTime.UtcNow
                                                                                        && index < segments.Count)
            {
                index += 1;
            }
            Segment prev = new Segment();
            //end position
            Segment cur = segments.ElementAt(index);
            cur = segments.ElementAt(index);
            //diff of cummulative time and current time
            TimeSpan timeSpanDif = Math.Abs(cummultTime.Subtract(DateTime.UtcNow));
            TimeSpan ofLastSegment = TimeSpan.FromSeconds(cur.TimeSpan_Seconds);
            relation = timeSpanDif / ofLastSegment;
            //there are at least 2 segments
            if (index > 0)
            {
                //start position
                prev = segments.ElementAt(index - 1); ;
            }
            //there is only one index
            else
            {
                //prev = new Segment();
                prev.Latitude = flightPlan.Initial_Location.Latitude;
                prev.Longitude = flightPlan.Initial_Location.Longitude;
            }
            //interpolation
            toReturn.longitude = prev.Longitude +  (cur.Longitude - prev.Longitude) * relation;
            toReturn.latitude = prev.Latitude + (cur.Latitude - prev.Latitude) * relation;
            return toReturn;
        }


        public void SetLongitude(FlightPlan flightPlanOutside)
        {
            flight.Longitude = CalculateLinInterpolation(flightPlanOutside).longitude;
        }





    }
}
