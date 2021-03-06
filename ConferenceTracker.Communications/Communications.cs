using System.Collections.Generic;
using System.Fabric;
using ConferenceTracker.Communications.Services;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ConferenceTracker.Communications
{
	/// <summary>
	/// An instance of this class is created for each service instance by the Service Fabric runtime.
	/// </summary>
	internal sealed class Communications : StatelessService
	{
		public Communications(StatelessServiceContext context)
			 : base(context)
		{ }

		/// <summary>
		/// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
		/// </summary>
		/// <returns>A collection of listeners.</returns>
		protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
		{
			return new[]
			{
				new ServiceInstanceListener(c =>
				{
					var settings = new FabricTransportRemotingListenerSettings { UseWrappedMessage = true };
						  return new FabricTransportServiceRemotingListener(c, new TwilioSmsService(), settings);
				})
			};

		}

	}
}
