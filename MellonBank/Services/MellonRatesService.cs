using AutoMapper;
using MellonBank.Data;
using MellonBank.Interfaces;
using MellonBank.Migrations;
using MellonBank.Models;
using MellonBank.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static MellonBank.Services.CustomerService;

namespace MellonBank.Services
{
	public class MellonRatesService : IMellonRatesService
	{
		private readonly AppDbContext _context;

		public MellonRatesService(AppDbContext context)
		{
			_context = context;
		}
		public IEnumerable<MellonRates> GetRates()
		{
			return _context.Rates.ToList();
		}

		public async Task PutRates()
		{
			using (HttpClient client = new HttpClient())
			{
				try
				{
					HttpResponseMessage response = await client.GetAsync("https://api.freecurrencyapi.com/v1/latest?apikey=fca_live_xTdpNAgsWp7cfLlyIShnzHC3n392XRq2TIJrXP2O&currencies=GBP%2CUSD%2CCHF%2CAUD&base_currency=EUR");
					if (response.IsSuccessStatusCode)
					{
						string apiResponse = await response.Content.ReadAsStringAsync();
						ExchangeRates exchangeRatesEur = JsonConvert.DeserializeObject<ExchangeRates>(apiResponse);

						//Mellon exchange profit
						var mellonProfit = 0.05;

						MellonRates existingRates = _context.Rates.Find(1);

						if (existingRates != null)
						{
							existingRates.AUD = (decimal)(exchangeRatesEur.Data.AUD * (1 + mellonProfit));
							existingRates.CHF = (decimal)(exchangeRatesEur.Data.CHF * (1 + mellonProfit));
							existingRates.GBP = (decimal)(exchangeRatesEur.Data.GBP * (1 + mellonProfit));
							existingRates.USD = (decimal)(exchangeRatesEur.Data.USD * (1 + mellonProfit));
							_context.SaveChanges();
						};
					}
					else
					{
						string errorResponse = await response.Content.ReadAsStringAsync();
						Console.WriteLine($"Error: {response.StatusCode}, {errorResponse}");
					}
				}
				catch (Exception ex) { }
			}
		}
		public class ExchangeRates
		{
			public ExchangeRateData Data { get; set; }
		}

		public class ExchangeRateData
		{
			public double AUD { get; set; }
			public double CHF { get; set; }
			public double GBP { get; set; }
			public double USD { get; set; }

		}
	}
}
