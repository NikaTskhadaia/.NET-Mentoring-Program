namespace Trip.Insurance.Calculation.Infrastructure.TripRepository;

public interface ITripRepository
{
    TripDetails LoadTrip(string touristName); 
}