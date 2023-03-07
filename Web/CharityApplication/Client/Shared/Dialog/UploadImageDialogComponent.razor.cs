using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace CharityApplication.Client.Shared.Dialog
{
    public partial class UploadImageDialogComponent
    {
        [Parameter]
        public string ButtonText { get; set; }

        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public string ImageURL { get; set; } = string.Empty;

        [CascadingParameter]
        public MudDialogInstance? MudDialog { get; set; }

        private IBrowserFile SelectedFile = null!;
        private string SelectedFileName => SelectedFile?.Name ?? "No file has been chosen";
        private bool SetWidth { get; set; } = false;
        private int ImageWidth { get; set; } = 300;
        private long MAX_FILE_SIZE { get; set; } = 512000;
        private ObjectFit ImageFit { get; set; } = ObjectFit.Cover;

        private void Submit()
        {
            if (ImageURL is not null)
            {
                MudDialog.Close(DialogResult.Ok(ImageURL));
            }
            else
            {
                SnackBar.Add("Image is required", Severity.Warning);
            }
        }

        private void Cancel() => MudDialog.Cancel();

        private async Task OnChanged(InputFileChangeEventArgs e)
        {
            SelectedFile = e.File;
            if (!SelectedFile.ContentType.StartsWith("image/"))
            {
                SnackBar.Add("The file is not a picture.", Severity.Error);
            }
            else if (SelectedFile == null || SelectedFile.Size > MAX_FILE_SIZE)
            {
                SnackBar.Add("The file can't exceed the maxium: 512 KB.", Severity.Error);
            }
            else
            {
                var buffer = new byte[SelectedFile.Size];
                await SelectedFile.OpenReadStream().ReadAsync(buffer);
                ImageURL = $"data:image/png;base64,{Convert.ToBase64String(buffer)}";
            }
        }
    }
}