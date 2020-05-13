using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models.FlightInfo.FlightBuilder
{
    public class FlightCreator
    {
        private IFlightBuilder flightBuilder;

        public FlightCreator(IFlightBuilder builder)
        {
            this.flightBuilder = builder;
        }

        public void CreateFlight(FlightPlan flightPlan, string id)
        {
            flightBuilder.SetCompany_Name(flightPlan.Company_Name);
          
            flightBuilder.SetFlight_Id(id);
            //flightBuilder.SetIs_External(); // how to find out?
            flightBuilder.SetPassengers(flightPlan.Passengers);
            flightBuilder.SetDate_Time(); // where should the calculation take place?


            flightBuilder.SetLongitude(flightPlan); // requires calculation(may also need segments)
            flightBuilder.SetLatitude(flightPlan); // requires calculation(may also need segments)
         
        }

        public Flight GetFlight()
        {
            return flightBuilder.GetFlight();
        }
    }
}
