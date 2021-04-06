using Microsoft.Maui;

namespace Microsoft.Maui.Handlers
{
	public abstract partial class ViewHandler<TVirtualView, TNativeView>
	{
		public override void SetFrame(Rectangle rect)
		{

		}

		public override Size GetDesiredSize(double widthConstraint, double heightConstraint)
			=> Size.Zero;

		protected override void SetupContainer()
		{

		}

		protected override void RemoveContainer()
		{

		}
	}
}