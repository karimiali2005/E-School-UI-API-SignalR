#pragma checksum "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\User\UserPic2.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dedafa11a1f7638b8d3c8ce4c95ab1d2cc92688c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Panel_Views_User_UserPic2), @"mvc.1.0.view", @"/Areas/Panel/Views/User/UserPic2.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dedafa11a1f7638b8d3c8ce4c95ab1d2cc92688c", @"/Areas/Panel/Views/User/UserPic2.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ec35194f4018e79d92c388c5c4744b42661dd8b", @"/Areas/Panel/Views/_ViewImports.cshtml")]
    public class Areas_Panel_Views_User_UserPic2 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<User>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form textAlignCenter"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\User\UserPic2.cshtml"
  
    Layout = "~/Areas/Panel/Views/Shared/_Layout.cshtml";
    //Layout = null;
    string Title = Convert.ToString(ViewData["Title"]);

    string Nickname = Model.FirstName + " " + Model.LastName;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<link href=\"/Sys/CSSCropper/2\" rel=\"stylesheet\" />\r\n<script type=\"text/javascript\" src=\"/Sys/JSCropper/2\"></script>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dedafa11a1f7638b8d3c8ce4c95ab1d2cc92688c4697", async() => {
                WriteLiteral("\r\n    <p id=\"formTitle\" class=\"titr textAlignRight\">");
#nullable restore
#line 15 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\User\UserPic2.cshtml"
                                             Write(Html.Raw(Title));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</p>

    <div id=""diverrors"" class=""errors textAlignRight"">
        <img src=""/imagesite/icons/error.png"" />
        <p id=""errormsg""></p>
    </div>

    <div class=""step opacityOne"" id=""step2"">

        <div class=""row textAlignCenter"">
            <input type=""button"" class=""buttonSingle colorBackThirty"" value=""بازگشت"" onclick=""cancelCh()"" />
            <input type=""button"" class=""buttonSingle colorBackSeconday"" value=""ذخیره شود"" onclick=""upiced()"" />
        </div>

        <input type=""hidden"" id=""returnUrl"" name=""returnUrl""");
                BeginWriteAttribute("value", " value=\"", 1000, "\"", 1030, 1);
#nullable restore
#line 29 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\User\UserPic2.cshtml"
WriteAttributeValue("", 1008, ViewData["returnUrl"], 1008, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n        <input type=\"hidden\" id=\"id\" name=\"id\"");
                BeginWriteAttribute("value", " value=\"", 1082, "\"", 1103, 1);
#nullable restore
#line 30 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\User\UserPic2.cshtml"
WriteAttributeValue("", 1090, Model.UserId, 1090, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n\r\n        <div class=\"row textAlignCenter\">\r\n            <label class=\"lbl\" id=\"result\">");
#nullable restore
#line 33 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\User\UserPic2.cshtml"
                                      Write(Html.Raw(Nickname));

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n        </div>        \r\n\r\n\r\n    </div>\r\n\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<div class=\"modal directionLtr\">\r\n    <img");
            BeginWriteAttribute("src", " src=\"", 1321, "\"", 1375, 3);
            WriteAttributeValue("", 1327, "/Panel/User/Picture?id=", 1327, 23, true);
#nullable restore
#line 42 "E:\E-School\EschoolHesabiNew\ESchool\Areas\Panel\Views\User\UserPic2.cshtml"
WriteAttributeValue("", 1350, Model.UserId, 1350, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1363, "&o=1&large=1", 1363, 12, true);
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 1376, "\"", 1382, 0);
            EndWriteAttribute();
            WriteLiteral(@" id=""imageUserPic"" class=""directionLtr"">
</div>

<style>
    .modal {
        padding: 18px;
        position: absolute;
        border: 1px solid lightgrey;
        left: 50%;
        transform: translateX(-50%);
        width: 300px;
        height: auto;
    }

        .modal img {
            max-width: 100%;
        }
</style>

<script>

    var croppr = new Croppr('#imageUserPic', {
        onInitialize: (instance) => {

        }/*,
        onCropMove: (data) => {
            var json = JSON.stringify(croppr.getValue());
            document.getElementById('result').innerHTML = json;
        }*/
    });

    window.addEventListener(""DOMContentLoaded"", function () {        

        /*croppr.moveTo(50, 50);
        croppr.resizeTo(50, 50);*/

    });

    function getCrop() {
        return croppr.getValue();
    }
    
</script>

");
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
