using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace AppCurso_ASP.NET_MVC_Core.Web.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-claim-value")]

    public class OcultaElementoPorClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OcultaElementoPorClaimTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("supress-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("supress-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var possuiAcesso = CustomAuthorization.ValidarClaimsUsuario(_httpContextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);

            if (possuiAcesso) return;

            output.SuppressOutput();
        }
    }
}
