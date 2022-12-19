#pragma checksum "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c6bb4bec895c8db6a8baeed1b8efbf61bebff892"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Panel_Views_Student_ExtraInfo), @"mvc.1.0.view", @"/Areas/Panel/Views/Student/ExtraInfo.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\_ViewImports.cshtml"
using ESchool;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\_ViewImports.cshtml"
using ESchool.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\_ViewImports.cshtml"
using ESchool.Need;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\_ViewImports.cshtml"
using DatabaseLayer.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\_ViewImports.cshtml"
using DatabaseLayer.Access;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c6bb4bec895c8db6a8baeed1b8efbf61bebff892", @"/Areas/Panel/Views/Student/ExtraInfo.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ec35194f4018e79d92c388c5c4744b42661dd8b", @"/Areas/Panel/Views/_ViewImports.cshtml")]
    public class Areas_Panel_Views_Student_ExtraInfo : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<User>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("chPreviousColor"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form textAlignCenter"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("/Panel/Student/ExtraInfo"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
   
    List<SelectListItem> RightHands = new List<SelectListItem>();
    RightHands.Add(new SelectListItem { Text = "دست راست", Value = "true" });
    RightHands.Add(new SelectListItem { Text = "دست چپ", Value = "false" });

    List<SelectListItem> PersianLanguages = new List<SelectListItem>();
    PersianLanguages.Add(new SelectListItem { Text = "فارسی", Value = "true" });
    PersianLanguages.Add(new SelectListItem { Text = "سایر زبان ها", Value = "false" });

    List<SelectListItem> IsRelativesParents = new List<SelectListItem>();
    IsRelativesParents.Add(new SelectListItem { Text = "دارد", Value = "true" });
    IsRelativesParents.Add(new SelectListItem { Text = "ندارد", Value = "false" });

    List<SelectListItem> IsStudentInsurances = new List<SelectListItem>();
    IsStudentInsurances.Add(new SelectListItem { Text = "دارد", Value = "true" });
    IsStudentInsurances.Add(new SelectListItem { Text = "ندارد", Value = "false" });

    List<SelectListItem> PreschoolEducations = new List<SelectListItem>();
    PreschoolEducations.Add(new SelectListItem { Text = "دارد", Value = "true" });
    PreschoolEducations.Add(new SelectListItem { Text = "ندارد", Value = "false" });

    List<SelectListItem> IsCityPlaces = new List<SelectListItem>();
    IsCityPlaces.Add(new SelectListItem { Text = "شهر", Value = "true" });
    IsCityPlaces.Add(new SelectListItem { Text = "روستا", Value = "false" });

    List<SelectListItem> ReferralReasonNews = new List<SelectListItem>();
    ReferralReasonNews.Add(new SelectListItem { Text = "نوآموز جدید", Value = "true" });
    ReferralReasonNews.Add(new SelectListItem { Text = "باقی مانده از کنکور سال های قبل", Value = "false" });


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"bigbox colorBackWhite colorBorderGray\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6bb4bec895c8db6a8baeed1b8efbf61bebff8927312", async() => {
                WriteLiteral(@"

        <div id=""diverrors"" class=""errors textAlignRight"">
            <img src=""/imagesite/icons/error.png"" />
            <p id=""errormsg""></p>
            <input type=""text"" id=""focusable"" />
        </div>

        <p id=""formTitle"" class=""titr textAlignRight"">ویرایش اطلاعات تکمیلی ");
#nullable restore
#line 43 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
                                                                       Write(Html.Raw(Model.FirstName));

#line default
#line hidden
#nullable disable
                WriteLiteral(" ");
#nullable restore
#line 43 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
                                                                                                  Write(Html.Raw(Model.LastName));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</p>

        <div class=""step opacityOne"" id=""step1"">

            <div class=""row displayNone"">
                <label class=""lbl"">ملیت</label>
                <input id=""Nationality"" name=""Nationality"" class=""chPreviousColor"" type=""text"" placeholder=""مثال: ایران""");
                BeginWriteAttribute("value", " value=\"", 2504, "\"", 2530, 1);
#nullable restore
#line 49 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
WriteAttributeValue("", 2512, Model.Nationality, 2512, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n            </div>\r\n\r\n            <div class=\"row rowInline\">\r\n                <label class=\"lbl\">بیمه پایه</label>\r\n                <input id=\"Insurance\" name=\"Insurance\" class=\"chPreviousColor\" type=\"text\" placeholder=\"مثال: تامین اجتماعی\"");
                BeginWriteAttribute("value", " value=\"", 2777, "\"", 2801, 1);
#nullable restore
#line 54 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
WriteAttributeValue("", 2785, Model.Insurance, 2785, 16, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
            </div>

            <div class=""row rowInline"">
                <label class=""lbl"">تعداد اعضای خانواده</label>
                <input id=""FamilyNumber"" name=""FamilyNumber"" class=""chPreviousColor textAlignLeft"" type=""text"" placeholder=""مثال: 4"" pattern=""[0-9]*"" inputmode=""numeric""");
                BeginWriteAttribute("value", " value=\"", 3103, "\"", 3130, 1);
#nullable restore
#line 59 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
WriteAttributeValue("", 3111, Model.FamilyNumber, 3111, 19, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
            </div>

            <div class=""row rowInline"">
                <label class=""lbl"">چندمین فرزند</label>
                <input id=""SeveralChildren"" name=""SeveralChildren"" class=""chPreviousColor textAlignLeft"" type=""text"" placeholder=""مثال: 2"" pattern=""[0-9]*"" inputmode=""numeric""");
                BeginWriteAttribute("value", " value=\"", 3431, "\"", 3461, 1);
#nullable restore
#line 64 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
WriteAttributeValue("", 3439, Model.SeveralChildren, 3439, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n            </div>\r\n\r\n            <div class=\"row rowInline\">\r\n                <label class=\"lbl\">دست غالب</label>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6bb4bec895c8db6a8baeed1b8efbf61bebff89211397", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 69 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.RightHanded);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 69 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = RightHands;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            </div>\r\n\r\n            <div class=\"row displayNone\">\r\n                <label class=\"lbl\">زبان</label>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6bb4bec895c8db6a8baeed1b8efbf61bebff89213591", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#nullable restore
#line 74 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.PersianLanguage);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 74 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = PersianLanguages;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            </div>\r\n\r\n            <div class=\"row rowInline\">\r\n                <label class=\"lbl\">نسبت خویشاوندی بین والدین</label>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6bb4bec895c8db6a8baeed1b8efbf61bebff89215814", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#nullable restore
#line 79 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.IsRelativesParents);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 79 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = IsRelativesParents;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            </div>\r\n\r\n            <div class=\"row rowInline\">\r\n                <label class=\"lbl\">بیمه تکمیلی</label>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6bb4bec895c8db6a8baeed1b8efbf61bebff89218028", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#nullable restore
#line 84 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.IsStudentInsurance);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 84 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = IsStudentInsurances;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            </div>\r\n\r\n            <div class=\"row displayNone\">\r\n                <label class=\"lbl\">تحصیلات قبل از دبستان</label>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6bb4bec895c8db6a8baeed1b8efbf61bebff89220255", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#nullable restore
#line 89 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.PreschoolEducation);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 89 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = PreschoolEducations;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            </div>\r\n\r\n            <div class=\"row displayNone\">\r\n                <label class=\"lbl\">محل سکونت</label>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6bb4bec895c8db6a8baeed1b8efbf61bebff89222470", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#nullable restore
#line 94 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.IsCityPlace );

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 94 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = IsCityPlaces;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            </div>\r\n\r\n            <div class=\"row rowInline displayNone\">\r\n                <label class=\"lbl\">علت مراجعه</label>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6bb4bec895c8db6a8baeed1b8efbf61bebff89224683", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#nullable restore
#line 99 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ReferralReasonNew);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 99 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = ReferralReasonNews;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            </div>\r\n\r\n            <input type=\"hidden\" id=\"id\" name=\"id\"");
                BeginWriteAttribute("value", " value=\"", 5203, "\"", 5224, 1);
#nullable restore
#line 102 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
WriteAttributeValue("", 5211, Model.UserId, 5211, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n            <input type=\"hidden\" id=\"returnUrl\" name=\"returnUrl\"");
                BeginWriteAttribute("value", " value=\"", 5294, "\"", 5324, 1);
#nullable restore
#line 103 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Student\ExtraInfo.cshtml"
WriteAttributeValue("", 5302, ViewData["returnUrl"], 5302, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />

            <div class=""row rowInline textAlignLeft"">
                <input type=""button"" class=""buttonSingle buttonSingle3 colorBackSeconday displayInlineBlock"" value=""تغییر داده شود"" onclick=""extrainfoed();"" />
                <input type=""button"" class=""buttonSingle buttonSingle3 colorBackThirty displayInlineBlock"" value=""برگشت"" onclick=""cancelCh()"" />
            </div>

        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<User> Html { get; private set; }
    }
}
#pragma warning restore 1591