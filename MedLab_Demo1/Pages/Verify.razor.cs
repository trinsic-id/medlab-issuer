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
using Microsoft.JSInterop;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using MedLab_Demo1.Data;

namespace MedLab_Demo1.Pages
{
    public class VerifyCredentialBase : ComponentBase
    {
        public List<ConnectionContract> connections = new List<ConnectionContract>();
        [CascadingParameter(Name = "AgencyServiceClient")] public IAgencyServiceClient AgencyServiceClient { get; set; }
        [Inject] IJSRuntime JSRuntime {get; set;}
        public VerificationContract verificationRequest;
        public VerificationContract verificationResponse;
        public CredentialContract credentialContract;
        public CredentialModel credentialModel = new CredentialModel();
        public bool FormReady = false;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeAsync<bool>("GetScanLottie");
        }
        protected async override Task OnInitializedAsync()
        {     

            try{
                // Get a QR for the verification
                verificationRequest = await AgencyServiceClient.CreateVerificationFromPolicyAsync("ab6adaa7-4d2d-4b6d-5a09-08d7e7b71754");
                StateHasChanged();
                verificationResponse = await Polly.Policy.HandleResult<VerificationContract>(r => r.IsValid != true)
                .WaitAndRetryAsync(100, _ => TimeSpan.FromSeconds(4))
                .ExecuteAsync(async () => await AgencyServiceClient.GetVerificationAsync(verificationRequest.VerificationId));
                if(verificationResponse.IsValid == true)
                {
                    FormReady = true;   
                    verificationRequest = null; 
                    StateHasChanged();          
                }
            }
            catch(Exception e){
                var err = e;
            }
        }
        protected async Task FormSubmit()
        {
            try{
                credentialContract = await AgencyServiceClient.CreateCredentialAsync( new CredentialOfferParameters{
                    DefinitionId = "QfuwopKZiKRhJaRY3YadPu:3:CL:15913:default",
                    AutomaticIssuance = true,
                    CredentialValues = new Dictionary<string,string>{
                    {"virus", credentialModel.virus},
                    {"checktime", credentialModel.checktime},
                    {"checkedlocation", credentialModel.checkedlocation},
                    {"checkedby", credentialModel.checkedby},
                    {"checkedfacility", credentialModel.checkedfacility},
                    {"diagnosismethod", credentialModel.diagnosismethod},
                    {"issued", credentialModel.issued},
                    {"expiry", credentialModel.expiry}
                    }
                });  
                FormReady = false;
            }catch(System.Exception e)
            {
                var err = e;
            }
            if(FormReady)
            {
                
            }
            StateHasChanged();
        }
        
    }
}