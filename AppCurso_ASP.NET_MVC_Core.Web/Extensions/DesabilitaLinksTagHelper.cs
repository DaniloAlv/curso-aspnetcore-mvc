using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace AppCurso_ASP.NET_MVC_Core.Web.Extensions
{
    [HtmlTargetElement("a", Attributes = "disable-by-claim-name")]
    [HtmlTargetElement("a", Attributes = "disable-by-claim-value")]

    public class DesabilitaLinksTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DesabilitaLinksTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("disable-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("disable-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var possuiAcesso = CustomAuthorization.ValidarClaimsUsuario(_httpContextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (possuiAcesso) return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add("title", "Você não possui permissão!");
            output.Attributes.Add("style", "cursor: not-allowed");
        }
    }
}
