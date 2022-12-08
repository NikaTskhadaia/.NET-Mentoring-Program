using Trip.Insurance.Calculation.Infrastructure.CurrencyService;
using Trip.Insurance.Calculation.Infrastructure.TripRepository;

namespace Trip.Insurance.Calculation.Calculators;

public class InsurancePaymentCalculator : ICalculator
{
    private readonly ICurrencyService _currencyService;
    private readonly ITripRepository _tripRepository;

    public InsurancePaymentCalculator(ICurrencyService currencyService, ITripRepository tripRepository)
    {
        this._currencyService = currencyService;
        this._tripRepository = tripRepository;
    }

    public decimal CalculatePayment(string touristName)
    {
        var rate = _currencyService.LoadCurrencyRate();
        var tripDetails = _tripRepository.LoadTrip(touristName);
        
        return Constants.A * rate * tripDetails.FlyCost +
               Constants.B * rate * tripDetails.AccommodationCost +
               Constants.C * rate * tripDetails.ExcursionCost;
    } 
}