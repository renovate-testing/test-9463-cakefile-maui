using Android.Content.Res;
using Android.Graphics;
using Android.Text;
using Android.Util;
using Android.Widget;

namespace Microsoft.Maui
{
	public static class TextViewExtensions
	{
		public static void UpdateText(this TextView textView, ILabel label) =>
			UpdateText(textView, label.Text);

		public static void UpdateText(this TextView textView, string newText)
		{
			newText ??= string.Empty;
			var oldText = textView.Text ?? string.Empty;

			if (oldText != newText)
				textView.Text = newText;
		}

		public static void UpdateTextColor(this TextView textView, ITextStyle textStyle, Color defaultColor)
		{
			var textColor = textStyle.TextColor;
			if (textColor.IsDefault)
				textView.SetTextColor(defaultColor.ToNative());
			else
				textView.SetTextColor(textColor.ToNative());
		}

		public static void UpdateTextColor(this TextView textView, ITextStyle textStyle) =>
			textView.UpdateTextColor(textStyle, textView.TextColors);

		public static void UpdateTextColor(this TextView textView, ITextStyle textStyle, ColorStateList? defaultColor)
		{
			var textColor = textStyle.TextColor;
			if (textColor.IsDefault)
				textView.SetTextColor(defaultColor);
			else
				textView.SetTextColor(textColor.ToNative());
		}

		public static void UpdateFont(this TextView textView, ITextStyle textStyle, IFontManager fontManager)
		{
			var font = textStyle.Font;

			var tf = fontManager.GetTypeface(font);
			textView.Typeface = tf;

			var sp = fontManager.GetScaledPixel(font);
			textView.SetTextSize(ComplexUnitType.Sp, sp);
		}

		public static void UpdateCharacterSpacing(this TextView textView, ITextStyle textStyle) =>
			textView.LetterSpacing = textStyle.CharacterSpacing.ToEm();

		public static void UpdateHorizontalTextAlignment(this TextView textView, ITextAlignment text)
		{
			if (textView.Context!.HasRtlSupport())
			{
				// We want to use TextAlignment where possible because it doesn't conflict with the
				// overall gravity of the underlying control
				textView.TextAlignment = text.HorizontalTextAlignment.ToTextAlignment();
			}
			else
			{
				// But if RTL support is not available for some reason, we have to resort
				// to gravity, because Android will simply ignore text alignment
				textView.Gravity = text.HorizontalTextAlignment.ToHorizontalGravityFlags();
			}
		}

		public static void UpdateLineBreakMode(this TextView textView, ILabel label)
		{
			textView.SetLineBreakMode(label);
		}

		public static void UpdateMaxLines(this TextView textView, ILabel label)
		{
			textView.SetLineBreakMode(label);
		}

		public static void UpdatePadding(this TextView textView, ILabel label)
		{
			var context = textView.Context;

			if (context == null)
			{
				return;
			}

			textView.SetPadding(
				(int)context.ToPixels(label.Padding.Left),
				(int)context.ToPixels(label.Padding.Top),
				(int)context.ToPixels(label.Padding.Right),
				(int)context.ToPixels(label.Padding.Bottom));
		}

		public static void UpdateTextDecorations(this TextView textView, ILabel label)
		{
			var textDecorations = label.TextDecorations;

			if ((textDecorations & TextDecorations.Strikethrough) == 0)
				textView.PaintFlags &= ~PaintFlags.StrikeThruText;
			else
				textView.PaintFlags |= PaintFlags.StrikeThruText;

			if ((textDecorations & TextDecorations.Underline) == 0)
				textView.PaintFlags &= ~PaintFlags.UnderlineText;
			else
				textView.PaintFlags |= PaintFlags.UnderlineText;
		}

		public static void UpdateLineHeight(this TextView textView, ILabel label, float lineSpacingAddDefault, float lineSpacingMultDefault)
		{
			if (label.LineHeight == -1)
				textView.SetLineSpacing(lineSpacingAddDefault, lineSpacingMultDefault);
			else if (label.LineHeight >= 0)
				textView.SetLineSpacing(0, (float)label.LineHeight);
		}

		internal static void SetLineBreakMode(this TextView textView, ILabel label)
		{
			var lineBreakMode = label.LineBreakMode;

			int maxLines = label.MaxLines;
			if (maxLines <= 0)
				maxLines = int.MaxValue;

			bool singleLine = false;

			switch (lineBreakMode)
			{
				case LineBreakMode.NoWrap:
					maxLines = 1;
					textView.Ellipsize = null;
					break;
				case LineBreakMode.WordWrap:
					textView.Ellipsize = null;
					break;
				case LineBreakMode.CharacterWrap:
					textView.Ellipsize = null;
					break;
				case LineBreakMode.HeadTruncation:
					maxLines = 1;
					singleLine = true; // Workaround for bug in older Android API versions (https://bugzilla.xamarin.com/show_bug.cgi?id=49069)
					textView.Ellipsize = TextUtils.TruncateAt.Start;
					break;
				case LineBreakMode.TailTruncation:
					maxLines = 1;
					textView.Ellipsize = TextUtils.TruncateAt.End;
					break;
				case LineBreakMode.MiddleTruncation:
					maxLines = 1;
					singleLine = true; // Workaround for bug in older Android API versions (https://bugzilla.xamarin.com/show_bug.cgi?id=49069)
					textView.Ellipsize = TextUtils.TruncateAt.Middle;
					break;
			}

			textView.SetSingleLine(singleLine);
			textView.SetMaxLines(maxLines);
		}
	}
}