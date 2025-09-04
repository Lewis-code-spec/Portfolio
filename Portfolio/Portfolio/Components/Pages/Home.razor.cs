using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Portfolio.Models;

namespace Portfolio.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    private HttpClient? Http { get; set; }

    private ContactRequest contactRequest = new ContactRequest();

    private bool? isSuccess;

    private async Task HandleValidSubmit()
    {
        try
        {
            var response = await Http!.PostAsJsonAsync("http://localhost:5099/api/Contact", contactRequest);
            isSuccess = response.IsSuccessStatusCode;
            contactRequest = new();
        }
        catch
        {
            isSuccess = false;
        }
    }
}
