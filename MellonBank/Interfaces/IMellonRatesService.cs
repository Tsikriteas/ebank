using MellonBank.Models;

namespace MellonBank.Interfaces
{
	public interface IMellonRatesService
	{
		IEnumerable<MellonRates> GetRates();
		Task PutRates();
	}
}
