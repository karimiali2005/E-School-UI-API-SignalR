#pragma checksum "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ed5dd8a59f9f7afcd4520e056c08fa6f29fc7f8e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Panel_Views_Teacher_Room), @"mvc.1.0.view", @"/Areas/Panel/Views/Teacher/Room.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ed5dd8a59f9f7afcd4520e056c08fa6f29fc7f8e", @"/Areas/Panel/Views/Teacher/Room.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ec35194f4018e79d92c388c5c4744b42661dd8b", @"/Areas/Panel/Views/_ViewImports.cshtml")]
    public class Areas_Panel_Views_Teacher_Room : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RoomProperties>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("Course"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("chPreviousColor"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("Room"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form textAlignCenter"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
  
    Layout = "~/Areas/Panel/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
  
    

    int PageNumber = 1;   
    int CourseId = 0;
    int RoomId = 0;    

    try { PageNumber = Convert.ToInt32(System.Web.MyHttpContext.Current.Request.Query["pagenumber"]); } catch { PageNumber = 1; }
    PageNumber = PageNumber <= 0 ? 1 : PageNumber;    
    try { CourseId = Convert.ToInt32(System.Web.MyHttpContext.Current.Request.Query["course"]); } catch { CourseId = 0; }
    try { RoomId = Convert.ToInt32(System.Web.MyHttpContext.Current.Request.Query["room"]); } catch { RoomId = 0; }  
    
    

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 22 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
Write(await Html.PartialAsync("Message", new MessageModel()));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"bigbox colorBackWhite colorBorderGray positionRelative\">\r\n\r\n    <img class=\"arrow rotate\" src=\"/imagepanel/icons/arrow_down.png\" onclick=\"openBigBox(this)\" />\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ed5dd8a59f9f7afcd4520e056c08fa6f29fc7f8e6769", async() => {
                WriteLiteral(@"

        <div id=""diverrors"" class=""errors textAlignRight"">
            <img src=""/imagesite/icons/error.png"" />
            <p id=""errormsg""></p>
            <input type=""text"" id=""focusable"" />
        </div>

        <div class=""step opacityOne"" id=""step1"">

            <div class=""row rowInline"">
                <label class=""lbl"">نام درس</label>
                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ed5dd8a59f9f7afcd4520e056c08fa6f29fc7f8e7426", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#nullable restore
#line 40 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = ViewBag.Courses;

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
                WriteLiteral("\r\n                \r\n            </div>\r\n\r\n            <div class=\"row rowInline\">\r\n                <label class=\"lbl\">نام کلاس</label>\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ed5dd8a59f9f7afcd4520e056c08fa6f29fc7f8e9195", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#nullable restore
#line 46 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = ViewBag.Rooms;

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
                WriteLiteral("\r\n            </div>\r\n\r\n            <div class=\"row rowInline\">\r\n                <label class=\"lbl\">آدرس جیتسی</label>\r\n                <input id=\"jitsiaddress\"");
                BeginWriteAttribute("value", " value=\"", 1753, "\"", 1824, 1);
#nullable restore
#line 51 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
WriteAttributeValue("", 1761, System.Web.MyHttpContext.Current.Request.Query["jitsiaddress"], 1761, 63, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"chPreviousColor textAlignLeft directionLtr\" type=\"text\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 1888, "\"", 1902, 0);
                EndWriteAttribute();
                WriteLiteral(" />\r\n            </div>\r\n\r\n            <div class=\"row rowInline\">\r\n                <label class=\"lbl\">کلمه عبور جیتسی</label>\r\n                <input id=\"jitsipassword\"");
                BeginWriteAttribute("value", " value=\"", 2072, "\"", 2144, 1);
#nullable restore
#line 56 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
WriteAttributeValue("", 2080, System.Web.MyHttpContext.Current.Request.Query["jitsipassword"], 2080, 64, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"chPreviousColor textAlignLeft directionLtr\" type=\"text\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 2208, "\"", 2222, 0);
                EndWriteAttribute();
                WriteLiteral(" />\r\n            </div>\r\n\r\n            <div class=\"row rowInline\">\r\n                <label class=\"lbl\">آدرس ادوب</label>\r\n                <input id=\"adobeaddress\"");
                BeginWriteAttribute("value", " value=\"", 2385, "\"", 2456, 1);
#nullable restore
#line 61 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
WriteAttributeValue("", 2393, System.Web.MyHttpContext.Current.Request.Query["adobeaddress"], 2393, 63, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"chPreviousColor textAlignLeft directionLtr\" type=\"text\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 2520, "\"", 2534, 0);
                EndWriteAttribute();
                WriteLiteral(" />\r\n            </div>\r\n\r\n            <div class=\"row rowInline\">\r\n                <label class=\"lbl\">نام کاربری ادوب</label>\r\n                <input id=\"adobeusername\"");
                BeginWriteAttribute("value", " value=\"", 2704, "\"", 2776, 1);
#nullable restore
#line 66 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
WriteAttributeValue("", 2712, System.Web.MyHttpContext.Current.Request.Query["adobeusername"], 2712, 64, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"chPreviousColor textAlignLeft directionLtr\" type=\"text\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 2840, "\"", 2854, 0);
                EndWriteAttribute();
                WriteLiteral(" />\r\n            </div>\r\n\r\n            <div class=\"row rowInline\">\r\n                <label class=\"lbl\">کلمه عبور ادوب</label>\r\n                <input id=\"adobepassword\"");
                BeginWriteAttribute("value", " value=\"", 3023, "\"", 3095, 1);
#nullable restore
#line 71 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
WriteAttributeValue("", 3031, System.Web.MyHttpContext.Current.Request.Query["adobepassword"], 3031, 64, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"chPreviousColor textAlignLeft directionLtr\" type=\"text\"");
                BeginWriteAttribute("placeholder", " placeholder=\"", 3159, "\"", 3173, 0);
                EndWriteAttribute();
                WriteLiteral(" />\r\n            </div>\r\n\r\n            <input type=\"hidden\" id=\"returnUrl\" name=\"returnUrl\"");
                BeginWriteAttribute("value", " value=\"", 3265, "\"", 3295, 1);
#nullable restore
#line 74 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
WriteAttributeValue("", 3273, ViewData["returnUrl"], 3273, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n            <input type=\"hidden\" id=\"id\" name=\"id\"");
                BeginWriteAttribute("value", " value=\"", 3351, "\"", 3378, 1);
#nullable restore
#line 75 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
WriteAttributeValue("", 3359, ViewData["UserId"], 3359, 19, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />

            <div class=""row textAlignCenter"">
                <input type=""button"" class=""buttonSingle colorBackThirty"" value=""بازگشت"" onclick=""cancelCh()"" />
                <input type=""button"" class=""buttonSingle colorBackSeconday"" value=""ثبت شود"" onclick=""rteachered()"" />
            </div>

        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
</div>

<div class=""bigbox displayBlock textAlignRight"">
    <input class=""buttonSingle buttonSingle4 displayInlineBlock colorBackRed"" style=""width :250px"" value=""حذف کلاس های انتخاب شده"" onclick=""rteacherdelete()"" />
</div>

<div class=""bigbox displayBlock textAlignCenter colorBackWhite"" style=""overflow-x: auto; padding: 10px;"">
    <table cellspacing=""0"" class=""grid"">
        <tr>
            <th><input type=""checkbox"" id=""chkAll"" onclick=""chkAllGrid();"" /></th>
            <th>ردیف</th>
            <th>نام درس</th>
            <th>نام کلاس</th>
            <th>آدرس جیتسی</th>
            <th>کلمه عبور جیتسی</th>
            <th>آدرس ادوب</th>
            <th>نام کاربری</th>
            <th>کلمه عبور ادوب</th>
        </tr>
");
#nullable restore
#line 103 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
          
            int i = 0;
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 106 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
         foreach (var item in Model.RoomTeachers)
        {
            string RoomTitle = item.Room != null ? item.Room.RoomTitle : "";
            string CourseTitle = item.Course != null ? item.Course.CourseTitle : "";


#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td><input type=\"checkbox\" class=\"chkGrid\"");
            BeginWriteAttribute("value", " value=\"", 4823, "\"", 4850, 1);
#nullable restore
#line 112 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
WriteAttributeValue("", 4831, item.RoomTeacherId, 4831, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /></td>\r\n                <td>");
#nullable restore
#line 113 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
                Write(i+1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>                \r\n                <td>");
#nullable restore
#line 114 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
               Write(Html.Raw(CourseTitle));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 115 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
               Write(Html.Raw(RoomTitle));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 116 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
               Write(Html.Raw(item.JitsiLiveAddress));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 117 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
               Write(Html.Raw(item.JitsiLivePassword));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 118 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
               Write(Html.Raw(item.AdobeLiveAddress));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 119 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
               Write(Html.Raw(item.AdobeLiveUsername));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 120 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"
               Write(Html.Raw(item.AdobeLivePass));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 122 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\Teacher\Room.cshtml"

            i++;
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </table>\r\n\r\n\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RoomProperties> Html { get; private set; }
    }
}
#pragma warning restore 1591