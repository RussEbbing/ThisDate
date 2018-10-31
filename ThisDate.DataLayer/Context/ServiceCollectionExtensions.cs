using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ThisDate.DateTimeDataLayer.Context
{
	/// <summary>	Service collection extensions for entity framework. </summary>
	public static class ServiceCollectionExtensions
	{
		///-------------------------------------------------------------------------------------------------
		/// <summary>	Adds the entity framework. </summary>
		///
		/// <exception cref="ArgumentNullException">
		/// 	<paramref name="connectionString"/> is <see langword="null"/>
		/// </exception>
		///
		/// <param name="services">		   	The services. </param>
		/// <param name="connectionString">	The connection string. </param>
		///-------------------------------------------------------------------------------------------------
		public static void AddEntityFramework([NotNull] this IServiceCollection services, [CanBeNull] string connectionString)
		{
			if (connectionString == null)
			{
				throw new ArgumentNullException(nameof(connectionString));
			}

			services.AddDbContext<DateTimeDimensionContext>(options => options.UseSqlServer(connectionString));
		}
	}
}