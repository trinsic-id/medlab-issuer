using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Streetcred.ServiceClients;
using Streetcred.ServiceClients.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace MedLab_Demo1.Pages
{
    public class RegistrationBase : ComponentBase
    {
        public List<ConnectionContract> connections = new List<ConnectionContract>();
        public IAgencyServiceClient AgencyServiceClient { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        public VerificationContract verificationResponse;
        [Parameter] public string verificationResponseId { get; set; }
        public CredentialContract credentialContract;
        public bool Ready = false;

        protected async override Task OnInitializedAsync()
        {
            var options = new ServiceClientOptions();
            options.AccessToken = "Us8PhNscIfESqf-GsNTUk0fqirRivRRBDZ4aavvv8Ao";
            options.AgencyBaseUri = "https://api.streetcred.id/agency/v1";
            options.SubscriptionKey = "ebacdad1a9a140bb9af6321cdedddb5e";
            AgencyServiceClient = new AgencyServiceClient(
                baseUri: new Uri("https://api.streetcred.id/agency/v1"),
                credentials: new StreetcredClientCredentials(Options.Create(options)));     
            Ready = true;       
        }
    }
}