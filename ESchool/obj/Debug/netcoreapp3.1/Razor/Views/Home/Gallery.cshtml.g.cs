#pragma checksum "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Gallery.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "08e09822d64bec63e43598215e481b3e57a7aaca"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Gallery), @"mvc.1.0.view", @"/Views/Home/Gallery.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\E-School\EschoolHesabiNew\ESchool\Views\_ViewImports.cshtml"
using ESchool;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\E-School\EschoolHesabiNew\ESchool\Views\_ViewImports.cshtml"
using ESchool.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\E-School\EschoolHesabiNew\ESchool\Views\_ViewImports.cshtml"
using ESchool.Need;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\E-School\EschoolHesabiNew\ESchool\Views\_ViewImports.cshtml"
using DatabaseLayer.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\E-School\EschoolHesabiNew\ESchool\Views\_ViewImports.cshtml"
using DatabaseLayer.Access;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\E-School\EschoolHesabiNew\ESchool\Views\_ViewImports.cshtml"
using System;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"08e09822d64bec63e43598215e481b3e57a7aaca", @"/Views/Home/Gallery.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27fe79aadfc202295453e0150d93b2d924692028", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Gallery : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Es.DataLayerCore.DTOs.Gallery.GalleryShowResult>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "ID", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Gallery.cshtml"
  
    ViewBag.Title = "گالری";
    Layout = "LayoutMain";
    var addressDownload = "https://hesabidownload.ir/hesabischoolfiles/" + SettingContext.PathStoreFiles.Instance.Gallery;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<header class=""log-reg-header"">
    <div class="" container-fluid d-flex justify-content-between align-items-center"">
        <div class=""menu-logo"">
            <div class="" logo"">
                <!-- start cahnge11 -->
                <img src=""../imagemember/logo1.png""");
            BeginWriteAttribute("alt", " alt=\"", 537, "\"", 543, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                <!-- end cahnge11 -->\r\n            </div>\r\n            <div class=\"barsmenu-h\">\r\n                <div id=\"bars\" class=\"d-block d-lg-none text-right\">\r\n                    <img src=\"../imagemember/Component 1 – 10@2x.png\"");
            BeginWriteAttribute("alt", " alt=\"", 783, "\"", 789, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                </div>
                <div class="" menu"" id=""menu"">
                    <ul>
                        <li class=""active"">
                            <a href=""/Home/Index"">صفحه اصلی</a>
                        </li>
                        <li>
                            <a href=""/Account/Register"">ثبت دانش آموزان</a>
                        </li>
                        <li>
                            <a href=""#"">معرفی اساتید</a>
                        </li>
                        <li>
                            <a href=""#"">اخبار</a>
                        </li>


                        <li>
                            <a href=""/Home/Gallery"">گالری تصاویر</a>
                        </li>
                        <li>
                            <a href=""/Home/About"">درباره ما</a>
                        </li>
                        <li>
                            <a href=""#mainfooter"">تماس با ما</a>
                        </li>
                    </ul>");
            WriteLiteral(@"
                </div>
            </div>
        </div>
    </div>
</header>
<section class=""gallery"">
    <div class=""container"">
        <div class=""row"">
            <div class="" col-lg-12 up-gallery"">
                <div class=""div-pic"">
                    <div class=""pic-fa"">
                        <i class=""fa fa-picture-o"" aria-hidden=""true""></i>
                    </div>
                    <div>
                        <h1>گالری تصاویر</h1>
                    </div>
                </div>
                <div class=""details-box"">
                    <span>مرتب سازی</span>
                    <select");
            BeginWriteAttribute("name", " name=\"", 2455, "\"", 2462, 0);
            EndWriteAttribute();
            WriteLiteral(" id=\"sorts\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "08e09822d64bec63e43598215e481b3e57a7aaca7068", async() => {
                WriteLiteral("جدیدترین");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "08e09822d64bec63e43598215e481b3e57a7aaca8253", async() => {
                WriteLiteral("قدیمی ترین");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                    </select>\r\n                </div>\r\n            </div>\r\n            <div class=\"col-lg-12 down-gallery my-5\">\r\n                <div class=\"container\">\r\n                    <div class=\"row\">\r\n");
#nullable restore
#line 75 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Gallery.cshtml"
                         foreach(var item in Model)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"div-box col-lg-3 col-md-6 col-12 py-2\">\r\n                                <article class=\"h-100\">\r\n                                    <a");
            BeginWriteAttribute("href", " href=\"", 3071, "\"", 3117, 2);
            WriteAttributeValue("", 3078, "/Home/Gallery_Detail?id=", 3078, 24, true);
#nullable restore
#line 79 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Gallery.cshtml"
WriteAttributeValue("", 3102, item.GalleryID, 3102, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        <div class=\"overflow-hidden py-1\">\r\n                                            <img class=\"imgGallery\"");
            BeginWriteAttribute("src", " src=\"", 3264, "\"", 3311, 2);
#nullable restore
#line 81 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Gallery.cshtml"
WriteAttributeValue("", 3270, addressDownload, 3270, 18, false);

#line default
#line hidden
#nullable disable
#nullable restore
#line 81 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Gallery.cshtml"
WriteAttributeValue("", 3288, item.GalleryDetailName, 3288, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 3312, "\"", 3318, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                            <div class=""dg-haver"">
                                            </div>
                                        </div>
                                        <div class=""p-1"">
                                            <h4> ");
#nullable restore
#line 86 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Gallery.cshtml"
                                            Write(item.GalleryTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h4>\r\n                                        </div>\r\n                                        <div class=\"p-1\">\r\n                                            <span>");
#nullable restore
#line 89 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Gallery.cshtml"
                                             Write(Codes.getPersianDate(@item.GalleryDateCreate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                        </div>\r\n                                    </a>\r\n                                </article>\r\n                            </div>\r\n");
#nullable restore
#line 94 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Gallery.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        \r\n");
            WriteLiteral("\r\n                    </div>\r\n");
            WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Es.DataLayerCore.DTOs.Gallery.GalleryShowResult>> Html { get; private set; }
    }
}
#pragma warning restore 1591
