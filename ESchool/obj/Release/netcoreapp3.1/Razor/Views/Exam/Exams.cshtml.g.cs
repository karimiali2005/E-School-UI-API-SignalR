#pragma checksum "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e4c03eda3e1ee4fe2fc5701124f2c596335c3fb4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Exam_Exams), @"mvc.1.0.view", @"/Views/Exam/Exams.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4c03eda3e1ee4fe2fc5701124f2c596335c3fb4", @"/Views/Exam/Exams.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27fe79aadfc202295453e0150d93b2d924692028", @"/Views/_ViewImports.cshtml")]
    public class Views_Exam_Exams : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Es.DataLayerCore.Model.Exam>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/imagemember/icons8-scorecard-24.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
  
    ViewBag.Title = "مدرسه حسابی";
    Layout = "~/Views/Shared/_LayoutMember.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<link href=\"/cssexam/exam.css\" rel=\"stylesheet\" />\r\n\r\n<section class=\" Chat-page table-plan table-plan-lesson planAlllessons managementHomeWork AzmonTable2 AllAzmonTable single-homeWork\">\r\n    <div class=\"  p-0 chat\">\r\n        ");
#nullable restore
#line 11 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
   Write(await Html.PartialAsync("Header"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        <div class="" table-div mx-auto my-3"">
            <div class=""alert  alert-dismissible d-none"" id=""myalertContainer"">
                <a href=""#"" class=""close"" onclick=""hideAlert()"">&times;</a>
                <p class=""text-right"" id=""myalert""></p>
            </div>
            <div id=""MessageAlertExamDelete"" class="" div-password"" style=""display: none;"">
                <div class="" bg-white shadow delete-massage-pupup"">
                    <div class="" message font-weight-bold""><span>آیا از حذف آزمون انتخاب شده مطمئن هستید؟</span></div>
                    <div class=""d-flex "">
                        <div class=""ml-2""><button id=""MessageAlertDeleteButton1"" type=""button"" class="" btn-yes btn bg-success text-white"">بله</button></div>
                        <div class=""ml-2""></div>
                        <div><button type=""button"" class="" btn-no btn bg-success text-white"">لغو</button></div>
                    </div>
                </div>
            </div>

            <div clas");
            WriteLiteral("s=\"row my-3\">\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 1458, "\"", 1493, 1);
#nullable restore
#line 29 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 1465, Url.Action("Create","Exam"), 1465, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class="" btn btn-success""><img src=""/imagemember/add.svg""><span class=""ml-2 mr-2"">افزودن</span></a>
            </div>
            <table class="" table table-bordered"">
                <thead>
                    <tr class="" d-md-none"">
                        <th  colspan=""3""  class=""bottom-res-p""><span>عنوان</span></th>                        
                        <th  colspan=""3""  class=""bottom-res-p""><span>کلاس</span></th>
                    </tr>
                    <tr class=""d-none d-md-table-row"">
                        <th class=""radif""><span>ردیف</span></th>
                        <th class=""width100""><span>عنوان</span></th>
                        <th><span>توضیحات</span></th>
                        <th><span>کلاس</span></th>
                        <th><span>تاریخ آزمون</span></th>
                        <th><span>ساعت آزمون</span></th>
                        <th><span>وضعیت</span></th>
                        <th class=""width150""><span>عملیات</span></th>
                ");
            WriteLiteral("    </tr>\r\n                </thead>\r\n                <tbody>\r\n");
#nullable restore
#line 49 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                      
                        var countor = 1;
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 52 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                     foreach (var exam in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr class=\" d-none d-md-table-row\"");
            BeginWriteAttribute("id", " id=\"", 2800, "\"", 2821, 2);
            WriteAttributeValue("", 2805, "row_", 2805, 4, true);
#nullable restore
#line 54 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 2809, exam.ExamId, 2809, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <td>\r\n                                <div>\r\n                                    ");
#nullable restore
#line 57 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                               Write(Html.Raw(@countor++.ToString()));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </div>\r\n                            </td>\r\n                            <td><span>");
#nullable restore
#line 60 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                 Write(exam.ExamTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n                            <td><span>");
#nullable restore
#line 61 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                 Write(exam.ExamDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n                            <td><span>");
#nullable restore
#line 62 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                 Write(exam.RoomChatGroup.RoomChatGroupTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n                            <td><span>");
#nullable restore
#line 63 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                 Write(exam.ExamStartDateTime.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n                            <td><span>");
#nullable restore
#line 64 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                 Write(exam.ExamStartDateTime.ToShortTimeString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n\r\n                            <td>\r\n");
#nullable restore
#line 67 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                 if (exam.ExamCancel == true)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"text-danger\"");
            BeginWriteAttribute("title", " title=\"", 3658, "\"", 3688, 1);
#nullable restore
#line 69 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 3666, exam.ExamCancelReason, 3666, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        آزمون لغو شده است\r\n                                    </span>\r\n");
#nullable restore
#line 72 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"

                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span>\r\n                                        آزمون برقرار می باشد\r\n                                    </span>\r\n");
#nullable restore
#line 79 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </td>\r\n\r\n                            <td>\r\n                                <span class=\"abzarak\">\r\n                                    <a href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\"", 4266, "\"", 4303, 3);
            WriteAttributeValue("", 4276, "ScoreAutoExam(", 4276, 14, true);
#nullable restore
#line 84 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 4290, exam.ExamId, 4290, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4302, ")", 4302, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"my-1 mx-1\" title=\"نمره دهی اتومات\">\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e4c03eda3e1ee4fe2fc5701124f2c596335c3fb412942", async() => {
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
            WriteLiteral("\r\n                                    </a>\r\n                                    <a");
            BeginWriteAttribute("href", " href=\"", 4529, "\"", 4593, 1);
#nullable restore
#line 87 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 4536, Url.Action("Responders","Exam",new { Id = exam.ExamId }), 4536, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"my-1 mx-1\" title=\"نمره دهی\">\r\n                                        <img src=\"/imagemember/icons8-stack-exchange-24.png\"");
            BeginWriteAttribute("alt", " alt=\"", 4724, "\"", 4730, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                    </a>\r\n\r\n                                    <a");
            BeginWriteAttribute("href", " href=\"", 4818, "\"", 4882, 1);
#nullable restore
#line 91 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 4825, Url.Action("CancelExam","Exam",new { Id = exam.ExamId }), 4825, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"my-1 mx-1\" title=\"لغو کردن\">\r\n                                        <img src=\"/imagemember/Iconly-Light-Paper@2x.png\"");
            BeginWriteAttribute("alt", " alt=\"", 5010, "\"", 5016, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                    </a>\r\n                                    \r\n                                        <a");
            BeginWriteAttribute("href", " href=\"", 5144, "\"", 5206, 1);
#nullable restore
#line 95 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 5151, Url.Action("Create", "Exam", new { Id = exam.ExamId }), 5151, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"y-1 mx-1\" title=\"ویرایش\">\r\n                                            <img src=\"/imagemember/Iconly-Light-Edit.png\"");
            BeginWriteAttribute("alt", " alt=\"", 5331, "\"", 5337, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                        </a>\r\n                                   \r\n\r\n");
#nullable restore
#line 100 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                     if (exam.ExamOn == true)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <label class=\"switch\">\r\n                                            <input type=\"checkbox\" checked");
            BeginWriteAttribute("onclick", " onclick=\"", 5668, "\"", 5705, 3);
            WriteAttributeValue("", 5678, "TurnOnOffExam(", 5678, 14, true);
#nullable restore
#line 103 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 5692, exam.ExamId, 5692, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 5704, ")", 5704, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <span class=\"slider round\"></span>\r\n                                        </label>\r\n");
#nullable restore
#line 106 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <label class=\"switch\">\r\n                                            <input type=\"checkbox\"");
            BeginWriteAttribute("onclick", " onclick=\"", 6089, "\"", 6126, 3);
            WriteAttributeValue("", 6099, "TurnOnOffExam(", 6099, 14, true);
#nullable restore
#line 110 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 6113, exam.ExamId, 6113, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 6125, ")", 6125, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <span class=\"slider round\"></span>\r\n                                        </label>\r\n");
#nullable restore
#line 113 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 114 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                     if (exam.Response.Count == 0)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <a href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\"", 6457, "\"", 6491, 3);
            WriteAttributeValue("", 6467, "DelectExam(", 6467, 11, true);
#nullable restore
#line 116 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 6478, exam.ExamId, 6478, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 6490, ")", 6490, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"my-1 mx-1\" title=\"حذف کردن\">\r\n                                            <img src=\"/imagemember/Iconly-Light-Delete@2x.png\"");
            BeginWriteAttribute("alt", " alt=\"", 6624, "\"", 6630, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                        </a>\r\n");
#nullable restore
#line 119 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <a href=\"#\" disabled class=\"my-1 mx-1 not-allowed \" title=\"حذف کردن\">\r\n                                            <img src=\"/imagemember/Iconly-Light-Delete@2x.png\"");
            BeginWriteAttribute("alt", " alt=\"", 7007, "\"", 7013, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                        </a>\r\n");
#nullable restore
#line 125 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </span>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 129 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"












                        ////


#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr class=\" d-md-none\">\r\n                            <td  colspan=\"3\" ><span>");
#nullable restore
#line 144 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                               Write(exam.ExamTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n                            <td  colspan=\"3\"><span>");
#nullable restore
#line 145 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                              Write(exam.RoomChatGroup.RoomChatGroupTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span></td>
                            
                        </tr>
                        <tr class="" d-md-none"">
                            <th class="" padding-res""><span>تاریخ آزمون</span></th>
                            <td class=""responsive-pad-score""><span>");
#nullable restore
#line 150 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                                              Write(exam.ExamStartDateTime.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n                            <th class=\" padding-res\"><span>ساعت آزمون</span></th>\r\n                            <td class=\"responsive-pad-score\"><span>");
#nullable restore
#line 152 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                                              Write(exam.ExamStartDateTime.ToShortTimeString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n                            <th class=\" padding-res\"><span>وضعیت</span></th>\r\n                            <td class=\"responsive-pad-score\">\r\n");
#nullable restore
#line 155 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                 if (exam.ExamCancel == true)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"text-danger\"");
            BeginWriteAttribute("title", " title=\"", 8325, "\"", 8355, 1);
#nullable restore
#line 157 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 8333, exam.ExamCancelReason, 8333, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        لغو شده\r\n                                    </span>\r\n");
#nullable restore
#line 160 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"

                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span>\r\n                                        برقرار است\r\n                                    </span>\r\n");
#nullable restore
#line 167 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            </td>
                        </tr>
                        <tr class="" d-md-none"">
                            <th><span>ابزارک ها</span></th>
                            <td colspan=""5"">
                                <span class=""abzarak"">
                                    <a href=""#""");
            BeginWriteAttribute("onclick", " onclick=\"", 9064, "\"", 9101, 3);
            WriteAttributeValue("", 9074, "ScoreAutoExam(", 9074, 14, true);
#nullable restore
#line 174 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 9088, exam.ExamId, 9088, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 9100, ")", 9100, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"my-1 mx-1\" title=\"نمره دهی اتومات\">\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e4c03eda3e1ee4fe2fc5701124f2c596335c3fb425397", async() => {
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
            WriteLiteral("\r\n                                    </a>\r\n                                    <a");
            BeginWriteAttribute("href", " href=\"", 9327, "\"", 9391, 1);
#nullable restore
#line 177 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 9334, Url.Action("Responders","Exam",new { Id = exam.ExamId }), 9334, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"my-1 mx-1\" title=\"نمره دهی\">\r\n                                        <img src=\"/imagemember/icons8-stack-exchange-24.png\"");
            BeginWriteAttribute("alt", " alt=\"", 9522, "\"", 9528, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                    </a>\r\n\r\n                                    <a");
            BeginWriteAttribute("href", " href=\"", 9616, "\"", 9680, 1);
#nullable restore
#line 181 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 9623, Url.Action("CancelExam","Exam",new { Id = exam.ExamId }), 9623, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"my-1 mx-1\" title=\"لغو کردن\">\r\n                                        <img src=\"/imagemember/Iconly-Light-Paper@2x.png\"");
            BeginWriteAttribute("alt", " alt=\"", 9808, "\"", 9814, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                    </a>\r\n                                    \r\n                                        <a");
            BeginWriteAttribute("href", " href=\"", 9942, "\"", 10004, 1);
#nullable restore
#line 185 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 9949, Url.Action("Create", "Exam", new { Id = exam.ExamId }), 9949, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"y-1 mx-1\" title=\"ویرایش\">\r\n                                            <img src=\"/imagemember/Iconly-Light-Edit.png\"");
            BeginWriteAttribute("alt", " alt=\"", 10129, "\"", 10135, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                        </a>\r\n                                    \r\n\r\n");
#nullable restore
#line 190 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                     if (exam.ExamOn == true)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <label class=\"switch\">\r\n                                            <input type=\"checkbox\" checked");
            BeginWriteAttribute("onclick", " onclick=\"", 10467, "\"", 10504, 3);
            WriteAttributeValue("", 10477, "TurnOnOffExam(", 10477, 14, true);
#nullable restore
#line 193 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 10491, exam.ExamId, 10491, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 10503, ")", 10503, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <span class=\"slider round\"></span>\r\n                                        </label>\r\n");
#nullable restore
#line 196 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <label class=\"switch\">\r\n                                            <input type=\"checkbox\"");
            BeginWriteAttribute("onclick", " onclick=\"", 10888, "\"", 10925, 3);
            WriteAttributeValue("", 10898, "TurnOnOffExam(", 10898, 14, true);
#nullable restore
#line 200 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 10912, exam.ExamId, 10912, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 10924, ")", 10924, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <span class=\"slider round\"></span>\r\n                                        </label>\r\n");
#nullable restore
#line 203 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 204 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                     if (exam.Response.Count == 0)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <a href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\"", 11256, "\"", 11290, 3);
            WriteAttributeValue("", 11266, "DelectExam(", 11266, 11, true);
#nullable restore
#line 206 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
WriteAttributeValue("", 11277, exam.ExamId, 11277, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 11289, ")", 11289, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"my-1 mx-1\" title=\"حذف کردن\">\r\n                                            <img src=\"/imagemember/Iconly-Light-Delete@2x.png\"");
            BeginWriteAttribute("alt", " alt=\"", 11423, "\"", 11429, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                        </a>\r\n");
#nullable restore
#line 209 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <a href=\"#\" disabled class=\"my-1 mx-1 not-allowed \" title=\"حذف کردن\">\r\n                                            <img src=\"/imagemember/Iconly-Light-Delete@2x.png\"");
            BeginWriteAttribute("alt", " alt=\"", 11806, "\"", 11812, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                        </a>\r\n");
#nullable restore
#line 215 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </span>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 219 "E:\E-School\EschoolHesabiNew\ESchool\Views\Exam\Exams.cshtml"


                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</section>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script src=\"/jsexam/exam.js\"></script>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Es.DataLayerCore.Model.Exam>> Html { get; private set; }
    }
}
#pragma warning restore 1591