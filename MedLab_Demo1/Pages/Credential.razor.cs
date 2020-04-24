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
    public class CredentialBase : ComponentBase
    {
        public List<ConnectionContract> connections = new List<ConnectionContract>();
        [CascadingParameter(Name = "AgencyServiceClient")] public IAgencyServiceClient AgencyServiceClient { get; set; }
        public VerificationContract verificationResponse;
        [Parameter] public string verificationResponseId { get; set; }
        public CredentialContract credentialContract;

        protected async override Task OnInitializedAsync()
        {
        
            try
            {
                verificationResponse = await AgencyServiceClient.GetVerificationAsync(verificationResponseId);
                if(verificationResponse.State == "Accepted" && verificationResponse.IsValid == true)
                {
                    credentialContract = await AgencyServiceClient.CreateCredentialAsync( new CredentialOfferParameters{
                        DefinitionId = "QfuwopKZiKRhJaRY3YadPu:3:CL:15551:default",
                        AutomaticIssuance = true,
                        CredentialValues = new Dictionary<string,string>{
                        {"Clean", "yes"}}
                    });
                }
            }
            catch(Exception e){
                var err = e;
            }
            StateHasChanged();
        }
    }
}