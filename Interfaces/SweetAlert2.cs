using Microsoft.JSInterop;

namespace ClubBlazor.Interfaces
{
    public class SweetAlert2
    {
        private readonly IJSRuntime _jsRuntime;

        public SweetAlert2(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        //instanciamos el método de SweetAlert2
        public async Task ShowAlert(string tipo, string titulo, string mensaje)
        {
            await _jsRuntime.InvokeVoidAsync("showSwal", tipo, titulo, mensaje);
        }

        //instanciamos el método de SweetAlert2 con confirmación
        public async Task<bool> Confirmation(string titlulo, string mensaje, TipoIconSweetAlert tipoIconSweetAlert)
        {
            return await _jsRuntime.InvokeAsync<bool>("SweetAlertHelper.showDeleteConfirmation", titlulo, mensaje, tipoIconSweetAlert.ToString());
        }

        // Enumeramos el tipo de alterta segun el icon
        public enum TipoIconSweetAlert
        {
            question, warning, success, info
        }
    }
}
