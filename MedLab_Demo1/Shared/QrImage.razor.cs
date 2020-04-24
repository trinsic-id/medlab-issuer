using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using QRCoder;

namespace MedLab_Demo1.Shared
{
    public class QrImageBase : ComponentBase
    {
        [Parameter] public string ImageData
        { 
            get => imageData; 
            set
            {
                imageData = value;
                EncodedPngData = EncodePng(value);
            }
        }

        [Parameter] public EventCallback<string> ImageDataChanged { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> AdditionalAttributes { get; set; }

        protected string EncodedPngData;
        private string imageData;

        protected string EncodePng(string data)
        {
            if (ImageData == null) return null;

            var png = PngByteQRCodeHelper.GetQRCode(data, QRCodeGenerator.ECCLevel.L, 10);
            return $"data:image/png;base64,{Convert.ToBase64String(png)}";
        }
    }
}