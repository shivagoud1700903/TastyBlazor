using Microsoft.Identity.Client;
using Microsoft.JSInterop;

namespace TastyBlazor.Services.Extensions
{
    public static class IJSRuntimeExtensions
    {

        public static async Task ToasterSuccess(this IJSRuntime js, string message) 

            {
                await js.InvokeVoidAsync("ShowToastr", "success", message);
            }
       public static async Task ToasterError(this IJSRuntime js, string message)
            {
                await js.InvokeVoidAsync("ShowToastr", "error", message);
            }
    }
}
