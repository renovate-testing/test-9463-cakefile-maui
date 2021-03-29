using System.Threading.Tasks;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Handlers;
using UIKit;
using Xunit;

namespace Microsoft.Maui.DeviceTests
{
	public partial class PickerHandlerTests
	{
		[Fact(DisplayName = "Title Color Initializes Correctly")]
		public async Task TitleColorInitializesCorrectly()
		{
			var xplatTitleColor = Color.CadetBlue;

			var picker = new PickerStub
			{
				Title = "Select an Item",
				TitleColor = xplatTitleColor
			};

			var expectedValue = xplatTitleColor.ToNative();

			var values = await GetValueAsync(picker, (handler) =>
			{
				return new
				{
					ViewValue = picker.TitleColor,
					NativeViewValue = GetNativeTitleColor(handler)
				};
			});

			Assert.Equal(xplatTitleColor, values.ViewValue);
			Assert.Equal(expectedValue, values.NativeViewValue);
		}

		MauiPicker GetNativePicker(PickerHandler pickerHandler) =>
			(MauiPicker)pickerHandler.View;

		UIColor GetNativeTitleColor(PickerHandler pickerHandler)
		{
			var mauiPicker = GetNativePicker(pickerHandler);
			return mauiPicker.AttributedPlaceholder.GetForegroundColor();
		}
	}
}