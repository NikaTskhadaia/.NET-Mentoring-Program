namespace Trip.Insurance.Calculation.Infrastructure.TripRepository;

public class TripRepository : ITripRepository
{
    public TripDetails LoadTrip(string touristName)
    {
        return new TripDetails();
    }
}