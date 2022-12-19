#pragma checksum "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\CancelExam.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6245a70776d2dd7d1df3295c3ae3535cf09d1e46"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Exam_CancelExam), @"mvc.1.0.view", @"/Views/Exam/CancelExam.cshtml")]
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
#nullable restore
#line 2 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\CancelExam.cshtml"
using Es.DataLayerCore.Model;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6245a70776d2dd7d1df3295c3ae3535cf09d1e46", @"/Views/Exam/CancelExam.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27fe79aadfc202295453e0150d93b2d924692028", @"/Views/_ViewImports.cshtml")]
    public class Views_Exam_CancelExam : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Exam>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CancelExam", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\CancelExam.cshtml"
  
    ViewBag.Title = "مدرسه حسابی";
    Layout = "~/Views/Shared/_LayoutMember.cshtml";
    var hide = "alert-danger d-none";
    if (!string.IsNullOrEmpty(ViewBag.Message))
    {
        hide = "alert-danger";

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("<link href=\"/cssexam/exam.css\" rel=\"stylesheet\" />\r\n\r\n\r\n<section class=\" Chat-page single-homeWork add-homeWork single-sendHomeWork\">\r\n    <div class=\" p-0 chat \">\r\n        ");
#nullable restore
#line 18 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\CancelExam.cshtml"
   Write(await Html.PartialAsync("Header"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        <div class="" div-chat-screen"">
            <!-- start change18 -->
            <div class="" chat-screen check-homework py-4 px-3"">
                <!-- end change18 -->
                <div class="" option p-3 mt-0 mx-5 bg-white font-weight-bold text-center"">
                    <div><span>");
#nullable restore
#line 24 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\CancelExam.cshtml"
                          Write(Model.ExamTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div>\r\n                </div>\r\n                <div class=\" card-all\">\r\n                    <div");
            BeginWriteAttribute("class", " class=\"", 908, "\"", 946, 3);
            WriteAttributeValue("", 916, "alert", 916, 5, true);
            WriteAttributeValue("  ", 921, "alert-dismissible", 923, 19, true);
#nullable restore
#line 27 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\CancelExam.cshtml"
WriteAttributeValue(" ", 940, hide, 941, 5, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"myalertContainer\">\r\n                        <a href=\"#\" class=\"close\" onclick=\"hideAlert()\">&times;</a>\r\n                        <p class=\"text-right\" id=\"myalert\">");
#nullable restore
#line 29 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\CancelExam.cshtml"
                                                      Write(ViewBag.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    </div>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6245a70776d2dd7d1df3295c3ae3535cf09d1e467531", async() => {
                WriteLiteral("\r\n                        ");
#nullable restore
#line 32 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\CancelExam.cshtml"
                   Write(Html.HiddenFor(m => m.ExamId));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                        <div class=\" option\">\r\n                            <div class=\" key\"><span><img src=\"/imagemember/title.svg\"");
                BeginWriteAttribute("alt", " alt=\"", 1422, "\"", 1428, 0);
                EndWriteAttribute();
                WriteLiteral(@"></span><span>دلیل لغو شدن آزمون: </span></div>
                            <div class="" value"">
                                <div class="" div-input score"">
                                    <input type=""text"" placeholder=""دلیل لغو آزمون را وارد نمایید"" required data-val-required=""لطفا دلیل لغو آزمون را وارد نمایید "" data-val=""true"" data-val-length=""حداکثر تعداد کارکتر 500 میباشد"" data-val-length-max=""500"" id=""ExamCancelReason"" maxlength=""500"" name=""ExamCancelReason""");
                BeginWriteAttribute("value", " value=\"", 1908, "\"", 1939, 1);
#nullable restore
#line 38 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\CancelExam.cshtml"
WriteAttributeValue("", 1916, Model.ExamCancelReason, 1916, 23, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" aria-required=\"true\">\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n\r\n                        <div class=\" accept-cancel \">\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6245a70776d2dd7d1df3295c3ae3535cf09d1e469460", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 44 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\CancelExam.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.All;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n                            <button type=\"submit\" class=\"w-50 btn btn-success d-inline\">ثبت</button>\r\n                            <a");
                BeginWriteAttribute("href", " href=\"", 2353, "\"", 2387, 1);
#nullable restore
#line 47 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\CancelExam.cshtml"
WriteAttributeValue("", 2360, Url.Action("Exams","Exam"), 2360, 27, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"w-50 btn btn-info d-inline float-left\">برگشت</a>\r\n\r\n                        </div>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n</section>\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"

    <script src=""https://ajax.aspnetcdn.com/ajax/jquery.validate/1.16.0/jquery.validate.min.js""></script>
    <script src=""https://ajax.aspnetcdn.com/ajax/jquery.validation.unobtrusive/3.2.6/jquery.validate.unobtrusive.min.js""></script>
    <script src=""/jsexam/exam.js""></script>

");
            }
            );
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Exam> Html { get; private set; }
    }
}
#pragma warning restore 1591
