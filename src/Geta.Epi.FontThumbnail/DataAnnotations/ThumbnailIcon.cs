using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using EPiServer.DataAnnotations;
using Geta.Epi.FontThumbnail.Extensions;

namespace Geta.Epi.FontThumbnail.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ThumbnailIcon : ImageUrlAttribute
	{
		public ThumbnailIcon(FontAwesome icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1) : base(icon.GetUrl(backgroundColor,foregroundColor, fontSize))
		{
		}
	}
}