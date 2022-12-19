#pragma checksum "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "637b7f086da9e33b444e1dbf5beed4473468c662"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Article_Detail), @"mvc.1.0.view", @"/Views/Home/Article_Detail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"637b7f086da9e33b444e1dbf5beed4473468c662", @"/Views/Home/Article_Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27fe79aadfc202295453e0150d93b2d924692028", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Article_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Es.DataLayerCore.DTOs.Article.ArticleViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
  
    ViewBag.Title = "اخبار";
    Layout = "LayoutMain";
    var addressDownload = "https://hesabidownload.ir/hesabischoolfiles/" + SettingContext.PathStoreFiles.Instance.Article;
    var time = "";
    switch (Model.Article.ArticleStudyTime)
    {
        case 1: time = "یک";
            break;
        case 2:
            time = "دو";
            break;
        case 3:
            time = "سه";
            break;
        default:
            time = "چهار";
            break;

    }

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
            BeginWriteAttribute("alt", " alt=\"", 850, "\"", 856, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                <!-- end cahnge11 -->\r\n            </div>\r\n            <div class=\"barsmenu-h\">\r\n                <div id=\"bars\" class=\"d-block d-lg-none text-right\">\r\n                    <img src=\"../imagemember/Component 1 – 10@2x.png\"");
            BeginWriteAttribute("alt", " alt=\"", 1096, "\"", 1102, 0);
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
            WriteLiteral("\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</header>\r\n<div class=\" Article\">\r\n    <div class=\"back-blue-img p-5\">\r\n        <div class=\" container\">\r\n            <div class=\" title\">\r\n                <h1>");
#nullable restore
#line 73 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
               Write(Model.Article.ArticleTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n");
            WriteLiteral(@"            </div>
        </div>
    </div>
    <div class="" container pos-top"">
        <div class="" row"">
            <div class="" Specifications mb-5 p-0 d-lg-none col-12"">
                <div class=""row"">
                    <div class="" col-12 row mx-0 "">
                        <div class="" col-5 offset-2 time-read"">
                            <div class="" img"">
                                <img class="" img-fluid"" src=""../imagemember/035-essay-os4p3wrks7n8koc7k7hv8utlosf1jzzcsn2sb94v5a.png""");
            BeginWriteAttribute("alt", " alt=\"", 2985, "\"", 2991, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                            </div>\r\n                            <div class=\" pb-xl-3 pb-2 text-center\">\r\n                                <div class=\" mb-xl-3 mb-2 mt-1 key\">مدت زمان مطالعه</div>\r\n                                <div class=\" value\">");
#nullable restore
#line 89 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
                                               Write(time);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" دقیقه</div>
                            </div>
                        </div>
                        <div class="" col-5 author"">
                            <div class="" img"">
                                <img class="" img-fluid"" src=""../imagemember/005-science-os4p3wrks7n8koc7k7hv8utlosf1jzzcsn2sb94v5a.png""");
            BeginWriteAttribute("alt", " alt=\"", 3568, "\"", 3574, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                            </div>\r\n                            <div class=\" pb-xl-3 pb-2 text-center\">\r\n                                <div class=\" mb-xl-3 mb-2 mt-1 key\">نویسنده</div>\r\n                                <div class=\" value\">");
#nullable restore
#line 98 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
                                               Write(Model.Article.CreateUser);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                            </div>
                        </div>
                    </div>
                    <div class="" col-12 row mx-0 mt-5"">
                        <div class="" col-5 offset-2 type-learning"">
                            <div class="" img"">
                                <img class="" img-fluid"" src=""../imagemember/045-microphone-os4p3wrks7n8koc7k7hv8utlosf1jzzcsn2sb94v5a.png""");
            BeginWriteAttribute("alt", " alt=\"", 4261, "\"", 4267, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                            </div>
                            <div class="" pb-xl-3 pb-2 text-center"">
                                <div class="" mb-xl-3 mb-2 mt-1 key"">نوع آموزش</div>
                                <div class="" value"">متن</div>
                            </div>
                        </div>
                        <div class="" col-5 source"">
                            <div class="" img"">
                                <img class="" img-fluid"" src=""../imagemember/013-globe-os4p3wrks7n8koc7k7hv8utlosf1jzzcsn2sb94v5a.png""");
            BeginWriteAttribute("alt", " alt=\"", 4828, "\"", 4834, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                            </div>
                            <div class="" pb-xl-3 pb-2 text-center"">
                                <div class="" mb-xl-3 mb-2 mt-1 key"">منبع</div>
                                <div class="" value"">مدرسه حسابی</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <section class="" col-lg-8"">
                <div class="" main-img-article mb-5"">
                    <img class=""img-fluid""");
            BeginWriteAttribute("src", " src=\"", 5373, "\"", 5422, 2);
#nullable restore
#line 126 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
WriteAttributeValue("", 5379, addressDownload, 5379, 18, false);

#line default
#line hidden
#nullable disable
#nullable restore
#line 126 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
WriteAttributeValue("", 5397, Model.Article.ArticlePic, 5397, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 5423, "\"", 5429, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                </div>\r\n                <div class=\" details my-5\">\r\n                    <div class=\" writer\">\r\n                        <div class=\" ml-2\"><i class=\"fa fa-user\" aria-hidden=\"true\"></i></div>\r\n                        <div>");
#nullable restore
#line 131 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
                        Write(Model.Article.CreateUser);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    </div>\r\n                    <div class=\" date\">\r\n                        <div class=\" ml-2\"><i class=\"fa fa-clock-o\" aria-hidden=\"true\"></i></div>\r\n                        <div>");
#nullable restore
#line 135 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
                        Write(Codes.getPersianDate(Model.Article.ArticleCreateDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    </div>\r\n                    <div class=\" view\">\r\n                        <div class=\" ml-2\"><i class=\"fa fa-eye\" aria-hidden=\"true\"></i></div>\r\n                        <div><span class=\" ml-2\">بازدید</span><span>");
#nullable restore
#line 139 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
                                                               Write(Model.Article.ArticleCountor);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div>\r\n                    </div>\r\n                </div>\r\n                <div class=\" desc \">\r\n                    <div class=\" text-article\">\r\n                        <p>\r\n                            ");
#nullable restore
#line 145 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
                       Write(Model.Article.ArticleBody);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        </p>
                      
                    </div>
                   

                </div>
            </section>
            <aside class="" col-lg-4"">
                <div class="" Specifications mb-5 p-0 d-lg-block d-none"">
                    <div class=""row"">
                        <div class="" col-12 row mx-0 "">
                            <div class="" col-5 offset-2 time-read"">
                                <div class="" img"">
                                    <img class="" img-fluid"" src=""../imagemember/035-essay-os4p3wrks7n8koc7k7hv8utlosf1jzzcsn2sb94v5a.png""");
            BeginWriteAttribute("alt", " alt=\"", 7084, "\"", 7090, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                                </div>
                                <div class="" pb-xl-3 pb-2 text-center"">
                                    <div class="" mb-xl-3 mb-2 mt-1 key"">مدت زمان مطالعه</div>
                                    <div class="" value"">سه دقیقه</div>
                                </div>
                            </div>
                            <div class="" col-5 author"">
                                <div class="" img""><img class="" img-fluid"" src=""../imagemember/005-science-os4p3wrks7n8koc7k7hv8utlosf1jzzcsn2sb94v5a.png""");
            BeginWriteAttribute("alt", " alt=\"", 7662, "\"", 7668, 0);
            EndWriteAttribute();
            WriteLiteral(@" /></div>
                                <div class="" pb-xl-3 pb-2 text-center"">
                                    <div class="" mb-xl-3 mb-2 mt-1 key"">نویسنده</div>
                                    <div class="" value"">علی کریمی</div>
                                </div>
                            </div>
                        </div>
                        <div class="" col-12 row mx-0 mt-5"">
                            <div class="" col-5 offset-2 type-learning"">
                                <div class="" img""><img class="" img-fluid"" src=""../imagemember/045-microphone-os4p3wrks7n8koc7k7hv8utlosf1jzzcsn2sb94v5a.png""");
            BeginWriteAttribute("alt", " alt=\"", 8311, "\"", 8317, 0);
            EndWriteAttribute();
            WriteLiteral(@" /></div>
                                <div class="" pb-xl-3 pb-2 text-center"">
                                    <div class="" mb-xl-3 mb-2 mt-1 key"">نوع آموزش</div>
                                    <div class="" value"">متن</div>
                                </div>
                            </div>
                            <div class="" col-5 source"">
                                <div class="" img""><img class="" img-fluid"" src=""../imagemember/013-globe-os4p3wrks7n8koc7k7hv8utlosf1jzzcsn2sb94v5a.png""");
            BeginWriteAttribute("alt", " alt=\"", 8842, "\"", 8848, 0);
            EndWriteAttribute();
            WriteLiteral(@" /></div>
                                <div class="" pb-xl-3 pb-2 text-center"">
                                    <div class="" mb-xl-3 mb-2 mt-1 key"">منبع</div>
                                    <div class="" value"">مدرسه دکتر حسابی</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
");
            WriteLiteral("                <div class=\" lastNews-lastWorks\">\r\n                    <div class=\" last-news\">\r\n                        <div class=\" title\">\r\n                            آخرین اخبار\r\n                        </div>\r\n");
#nullable restore
#line 207 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
                         foreach (var item in Model.ArticleShowTops)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <a");
            BeginWriteAttribute("href", " href=\"", 10062, "\"", 10108, 2);
            WriteAttributeValue("", 10069, "/Home/Article_Detail?id=", 10069, 24, true);
#nullable restore
#line 209 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
WriteAttributeValue("", 10093, item.ArticleID, 10093, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\" item-news my-3\">\r\n                                <div class=\" ml-3\"><img class=\" img-fluid\"");
            BeginWriteAttribute("src", " src=\"", 10210, "\"", 10250, 2);
#nullable restore
#line 210 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
WriteAttributeValue("", 10216, addressDownload, 10216, 18, false);

#line default
#line hidden
#nullable disable
#nullable restore
#line 210 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
WriteAttributeValue("", 10234, item.ArticlePic, 10234, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 10251, "\"", 10257, 0);
            EndWriteAttribute();
            WriteLiteral("></div>\r\n                                <div>");
#nullable restore
#line 211 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
                                Write(item.ArticleTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                            </a>\r\n");
#nullable restore
#line 213 "E:\E-School\EschoolHesabiNew\ESchool\Views\Home\Article_Detail.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("                    </div>\r\n                </div>\r\n            </aside>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Es.DataLayerCore.DTOs.Article.ArticleViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
