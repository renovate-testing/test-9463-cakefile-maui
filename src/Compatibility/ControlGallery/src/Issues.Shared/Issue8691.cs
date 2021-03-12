﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Maui.Controls.CustomAttributes;
using Microsoft.Maui.Controls.Internals;


#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
using Microsoft.Maui.Controls.Compatibility.UITests;
#endif

namespace Microsoft.Maui.Controls.Compatibility.ControlGallery.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 8691, "[Bug] TabIndex is ignored for first element on page for VoiceOver",
		PlatformAffected.iOS)]
#if UITEST
	[NUnit.Framework.Category(Compatibility.UITests.UITestCategories.Github10000)]
	[NUnit.Framework.Category(UITestCategories.ManualReview)]
	[NUnit.Framework.Category(UITestCategories.Accessibility)]
#endif
	public class Issue8691 : TestContentPage
	{
		protected override void Init()
		{

			Content = new StackLayout()
			{
				Children =
				{
					new Label()
					{
						Text = "2nd TabIndex",
						TabIndex = 20
					},
					new Label()
					{
						Text = "I should be the first element focused when voice over is on",
						TabIndex = 10
					},
					new Label()
					{
						Text = "3rd TabIndex",
						TabIndex = 30
					},
				}
			};
		}

	}
}
