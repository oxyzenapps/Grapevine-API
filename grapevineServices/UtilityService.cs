using Dapper;
using grapevineData;
using grapevineData.Interfaces;
using System.Data;

using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Reporting.NETCore;

namespace grapevineServices.Services
{
	public class UtilityService
	{
		private readonly IDapperExecutor _db;

		public UtilityService(IDapperExecutor db)
		{
			_db = db;
		}
 
		public class UI_ACTION_RESULT
		{
			public Dictionary<string, object> result { get; set; } = new();
			public List<string> errors { get; set; } = new();
		}

		// ===============================
		// snake_case → camelCase
		// ===============================
		private string ToCamelCase(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				return input;

			var parts = input.Split('_', StringSplitOptions.RemoveEmptyEntries)
							 .Select(p => char.ToUpper(p[0]) + p.Substring(1).ToLower());

			var pascal = string.Join("", parts);
			return char.ToLower(pascal[0]) + pascal.Substring(1);
		}

		 
		private List<Dictionary<string, object>> ConvertRows(IEnumerable<dynamic> rows)
		{
			var list = new List<Dictionary<string, object>>();

			foreach (IDictionary<string, object> row in rows)
			{
				var dict = new Dictionary<string, object>();

				foreach (var col in row)
				{
					dict[ToCamelCase(col.Key)] =
						col.Value is DateTime dt ? dt.ToString("o") : col.Value;
				}

				list.Add(dict);
			}

			return list;
		}

		public async Task<IEnumerable<IEnumerable<dynamic>>> DataInDataSet(
			string storedProcedure,
			DynamicParameters parameters = null)
		{
			if (string.IsNullOrWhiteSpace(storedProcedure))
				return Enumerable.Empty<IEnumerable<dynamic>>(); // return empty if SP name invalid

			try
			{
				var request = new StoredProcedureRequest
				{
					ProcedureName = storedProcedure,
					Parameters = parameters
				};

				// Directly return the datasets from the DB call
				var datasets = await _db.QueryMultipleDynamicAsync(request);
				return datasets;
			}
			catch
			{
				return Enumerable.Empty<IEnumerable<dynamic>>(); // return empty on exception
			}
		}

		public async Task<UI_ACTION_RESULT> GetDataResultAsync(
			string storedProcedure,
			DynamicParameters parameters = null)
		{
			var result = new UI_ACTION_RESULT();

			if (string.IsNullOrWhiteSpace(storedProcedure))
			{
				result.errors.Add("Invalid stored procedure name.");
				return result;
			}

			try
			{
				var request = new StoredProcedureRequest
				{
					ProcedureName = storedProcedure,
					Parameters = parameters
				};

				var datasets = await _db.QueryMultipleSqlAsync(request);

				int index = 1;
				foreach (var set in datasets)
				{
					result.result[$"dataset{index}"] = ConvertRows(set);
					index++;
				}
			}
			catch (Exception ex)
			{
				result.errors.Add(ex.Message);
			}

			return result;
		}

		public string FormatDate(string date, bool forceFirstDay = false, string format = "MM-dd-yyyy")
		{
			if (!DateTime.TryParse(date, out var d))
				return string.Empty;

			if (forceFirstDay)
				d = new DateTime(d.Year, d.Month, 1);

			return d.ToString(format);
		}

 


	}



}
