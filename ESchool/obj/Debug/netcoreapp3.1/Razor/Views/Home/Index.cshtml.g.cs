#pragma checksum "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e68c36730e67c3add27be091e5036876487904d8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e68c36730e67c3add27be091e5036876487904d8", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27fe79aadfc202295453e0150d93b2d924692028", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Es.DataLayerCore.DTOs.Home.HomeViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/imagemember/01logohesabi1.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("logo"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Index.cshtml"
  
    ViewBag.Title = "صفحه اصلی";
    Layout = "LayoutMain";
    var addressDownload = "https://hesabidownload.ir/hesabischoolfiles/" + SettingContext.PathStoreFiles.Instance.Article;

#line default
#line hidden
#nullable disable
            WriteLiteral("<header>\r\n    <header>\r\n        <div class=\"bigfoto\">\r\n            <div class=\"container\">\r\n                <div class=\"menu\">\r\n                    <div class=\"row\">\r\n                        <div class=\"logo col-2 \">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "e68c36730e67c3add27be091e5036876487904d85018", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                        </div>
                        <div class=""right col-lg-7 col-3"">
                            <div id=""bars"" class=""d-block d-lg-none text-right"">
                                <i class=""fa fa-bars"" aria-hidden=""true""></i>
                            </div>
                            <ul id=""menu"">
                                <li> <a href=""/Home/Index"">صفحه اصلی</a></li>
                                <li> <a href=""/Account/Register"">ثبت دانش آموزان</a></li>
                                <li> <a href=""#"">معرفی اساتید</a></li>
                                <li> <a href=""#"">اخبار</a></li>
                                <li> <a href=""/Home/Gallery"">گالری تصاویر</a></li>
                                <li> <a href=""/Home/About"">درباره ما</a></li>
                                <li> <a href=""#mainfooter"">تماس با ما</a></li>
                            </ul>
                        </div>
                        <div class=""v-seite col-lg-3 col-5"">
          ");
            WriteLiteral(@"                  <div>
                                <a href=""/Accounts/Login"">
                                    <span>
                                        <i class=""fa fa-sign-in"" aria-hidden=""true""></i>
                                    </span>ورود به سایت
                                </a>
                            </div>
                        </div>
                        <div>
                            <div class=""buttom col-lg-12"">
                                <h1>شروع پیش ثبت نام مجموعه آموزشی دکتر حسابی</h1>
                                <h3>برای ثبت نام روی دکمه پیش ثبت نام کلیک کنید</h3>
                                <div class=""btn"">
                                    <a href=""/Home/PreRegistration"">پیش ثبت نام</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div>

                </div>
            </div>

        </div>");
            WriteLiteral(@"

    </header>

</header>

<section class=""section1"">
    <div class=""container"">
        <div class=""row"">
            <div class="" br col-md-3 col-6"">
                <h1>پیش ثبت نام</h1>
                <h3>دوره متوسطه اول</h3>
            </div>
            <div class=""br col-md-3 col-6"">
                <h1>پیش ثبت نام</h1>
                <h3>دوره متوسطه اول</h3>
            </div>
            <div class=""br col-md-3 col-6"">
                <h1>پیش ثبت نام</h1>
                <h3>دوره متوسطه اول</h3>
            </div>
            <div class=""col-md-3 col-6"">
                <h1>پیش ثبت نام</h1>
                <h3>دوره متوسطه اول</h3>
            </div>
        </div>
    </div>

</section>

<section class=""section2 mosalas"">
    <div class="" container mtp"">
        <div class=""moarfi"">
            <h1>معرفی اساتید</h1>
        </div>
        <div class=""row"">
            <div class=""col-md-3"">
                <div class=""steps"">
                    <a href=""/Hom");
            WriteLiteral("e/IntroductionGroup?id=4\">\r\n                        <div class=\"img-step\">\r\n                            <div class=\"div-svg\">\r\n                                <img src=\"../imagemember/111.png\"");
            BeginWriteAttribute("alt", " alt=\"", 3806, "\"", 3812, 0);
            EndWriteAttribute();
            WriteLiteral(@" height=""87"" width=""87"">
                            </div>
                        </div>
                        <div class=""div-txt"">
                            <h3>پرسنل مدرسه دکتر حسابی</h3>
                        </div>
                    </a>
                </div>
            </div>
            <div class=""col-md-3"">
                <div class=""steps"">
                    <a href=""/Home/IntroductionGroup?id=1"">
                        <div class=""img-step"">
                            <div class=""div-svg"">
                                <img src=""../imagemember/12.png""");
            BeginWriteAttribute("alt", " alt=\"", 4414, "\"", 4420, 0);
            EndWriteAttribute();
            WriteLiteral(@" height=""87"" width=""87"">
                            </div>
                        </div>
                        <div class=""div-txt"">
                            <h3>اساتید دوره دبستان</h3>
                        </div>
                    </a>
                </div>
            </div>
            <div class=""col-md-3"">
                <div class=""steps"">
                    <a href=""/Home/IntroductionGroup?id=2"">
                        <div class=""img-step"">
                            <div class=""div-svg"">
                                <img src=""../imagemember/13.png""");
            BeginWriteAttribute("alt", " alt=\"", 5018, "\"", 5024, 0);
            EndWriteAttribute();
            WriteLiteral(@" height=""87"" width=""87"">
                            </div>
                        </div>
                        <div class=""div-txt"">
                            <h3>اساتید دوره متوسطه اول</h3>
                        </div>
                    </a>
                </div>
            </div>
            <div class=""col-md-3"">
                <div class=""steps"">
                    <a href=""/Home/IntroductionGroup?id=3"">
                        <div class=""img-step"">
                            <div class=""div-svg"">
                                <img src=""../imagemember/13.png""");
            BeginWriteAttribute("alt", " alt=\"", 5626, "\"", 5632, 0);
            EndWriteAttribute();
            WriteLiteral(@" height=""87"" width=""87"">
                            </div>
                        </div>
                        <div class=""div-txt"">
                            <h3>اساتید دوره متوسطه دوم</h3>
                        </div>
                    </a>
                </div>
            </div>

        </div>
    </div>

</section>
<section class=""section3"">


    <div class="" div-bnf"">
        <a href=""/Home/Gallery"">گالری تصاویر و ویدئو ها</a>

    </div>


</section>
<section class=""section5"">
    <div class=""container"">
        <div>
            <h2>اخبار</h2>

        </div>
        <div class=""row"">
");
#nullable restore
#line 169 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Index.cshtml"
             foreach (var item in Model.Article)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <dic class=\"col-md-4\">\r\n                    <div class=\"card\">\r\n                        <div class=\"div-img\"><img class=\"img-fluid\"");
            BeginWriteAttribute("src", " src=\"", 6489, "\"", 6529, 2);
#nullable restore
#line 173 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Index.cshtml"
WriteAttributeValue("", 6495, addressDownload, 6495, 18, false);

#line default
#line hidden
#nullable disable
#nullable restore
#line 173 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Index.cshtml"
WriteAttributeValue("", 6513, item.ArticlePic, 6513, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 6530, "\"", 6536, 0);
            EndWriteAttribute();
            WriteLiteral("></div>\r\n                        <div class=\"div-txt\">\r\n                            <h3>");
#nullable restore
#line 175 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Index.cshtml"
                           Write(item.ArticleTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                            <p>");
#nullable restore
#line 176 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Index.cshtml"
                          Write(item.ArticleBody);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                        <div class=\"div-a\"><a");
            BeginWriteAttribute("href", " href=\"", 6781, "\"", 6827, 2);
            WriteAttributeValue("", 6788, "/Home/Article_Detail?id=", 6788, 24, true);
#nullable restore
#line 178 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Index.cshtml"
WriteAttributeValue("", 6812, item.ArticleID, 6812, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">ادامه مطلب</a></div>\r\n                    </div>\r\n                </dic>\r\n");
#nullable restore
#line 181 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Index.cshtml"

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
            WriteLiteral("        </div>\r\n    </div>\r\n</section>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Es.DataLayerCore.DTOs.Home.HomeViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
