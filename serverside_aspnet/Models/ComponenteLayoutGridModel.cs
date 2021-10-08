using System;
using Microsoft.AspNetCore.Html;

namespace serverside_aspnet.Models
{
    public class ComponenteLayoutGridModel
    {
        public int Coluna { get; set; }

        public Func<object, IHtmlContent> HtmlTemplate { get; set; }
    }
}
