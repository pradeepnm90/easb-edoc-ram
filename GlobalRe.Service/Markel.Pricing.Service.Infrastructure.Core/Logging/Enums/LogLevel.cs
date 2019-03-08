namespace Markel.Pricing.Service.Infrastructure.Logging
{
	/// <summary>
	/// Enum to specify logging level
	/// </summary>
	public enum LogLevel
	{
		/// <summary>
		/// The logging level is undefined. This is regarded
		/// an invalid value.
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// Logs debugging output.
		/// </summary>
		Debug = 1,
		/// <summary>
		/// Logs basic information.
		/// </summary>
		Info = 2,
		/// <summary>
		/// Logs a warning.
		/// </summary>
		Warn = 3,
		/// <summary>
		/// Logs an error.
		/// </summary>
		Error = 4,
		/// <summary>
		/// Logs a fatal incident.
		/// </summary>
		Fatal = 5
	}
}
