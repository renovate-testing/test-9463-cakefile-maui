using System.Threading.Tasks;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.DeviceTests
{
	public partial class PickerHandlerTests
	{
		[Fact(DisplayName = "Title Color Initializes Correctly")]
		public async Task TitleColorInitializesCorrectly()
		{
			var picker = new PickerStub
			{
				Title = "Select an Item",
				TitleColor = Color.CadetBlue
			};

			await ValidateNativeTitleColor(picker, picker.TitleColor);
		}

		MauiPicker GetNativePicker(PickerHandler pickerHandler) =>
			(MauiPicker)pickerHandler.View;

		Task ValidateNativeTitleColor(IPicker picker, Color color)
		{
			return InvokeOnMainThreadAsync(() =>
			{
				return GetNativePicker(CreateHandler(picker)).AssertContainsColor(color);
			});
		}
	}
}