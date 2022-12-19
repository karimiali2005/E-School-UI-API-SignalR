#pragma checksum "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b3760a6e97536d0d21c6ca9066e76cc1345edde5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Class_UserShow), @"mvc.1.0.view", @"/Views/Class/UserShow.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b3760a6e97536d0d21c6ca9066e76cc1345edde5", @"/Views/Class/UserShow.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27fe79aadfc202295453e0150d93b2d924692028", @"/Views/_ViewImports.cshtml")]
    public class Views_Class_UserShow : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<RoomUser>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
  
    Layout = "~/Views/Shared/_LayoutUser.cshtml";
    int count = 0;
    RoomUserOp roomUserOp = new RoomUserOp();
    UserOp userOp = new UserOp();

    var UserId = Convert.ToInt32(this.User.FindFirst("UserId").Value);
    var UserTypeId = Convert.ToInt32(this.User.FindFirst("UserTypeId").Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"form textAlignCenter\">\r\n\r\n    <p id=\"contentTitle\" class=\"titr textAlignRight\">");
#nullable restore
#line 15 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
                                                Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>

    <div class=""row textAlignLeft"">
        <input type=""button"" class=""buttonSingle colorBackThirty displayInlineBlock colorBackBlue2"" value=""بازگشت"" onclick=""cancelCh()"" />
    </div>

</div>

<div class=""bigbox displayBlock textAlignCenter colorBackWhite"" style=""overflow-x: auto; padding: 10px;"">
    <table cellspacing=""0"" class=""grid"">
        <tr>
            <th>ردیف</th>
            <th style=""width: 80px;"">عکس کاربر</th>
            <th>کد ملی</th>
            <th>نام</th>
            <th>نام خانوادگی</th>
            <th>تاریخ تولد</th>
            <th>شماره همراه</th>
        </tr>
");
#nullable restore
#line 34 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
          
            int i = 0;
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
         foreach (RoomUser roomUser in Model)
        {
            User user = userOp.GetUser(roomUser.UserId);
            if (user != null)
            {
                int UId = user.UserId;
                string IrNational = user.Irnational;
                string FirstName = user.FirstName;
                string LastName = user.LastName;
                string PersianBirthDate = Codes.getPersianDate(user.BirthDate);
                string MobileNumber = user.MobileNumber;


#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 50 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
                    Write(i + 1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td><a target=\"_blank\"");
            BeginWriteAttribute("href", " href=\"", 1730, "\"", 1765, 2);
            WriteAttributeValue("", 1737, "/Account/PictureUser?id=", 1737, 24, true);
#nullable restore
#line 51 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
WriteAttributeValue("", 1761, UId, 1761, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><img");
            BeginWriteAttribute("src", " src=\"", 1771, "\"", 1810, 3);
            WriteAttributeValue("", 1777, "/Account/PictureUser?id=", 1777, 24, true);
#nullable restore
#line 51 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
WriteAttributeValue("", 1801, UId, 1801, 4, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1805, "&s=30", 1805, 5, true);
            EndWriteAttribute();
            WriteLiteral(" /></a></td>\r\n                    <td>");
#nullable restore
#line 52 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
                   Write(Html.Raw(IrNational));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 53 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
                   Write(Html.Raw(FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 54 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
                   Write(Html.Raw(LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 55 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
                   Write(Html.Raw(PersianBirthDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 56 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
                   Write(Html.Raw(MobileNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 58 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"

                i++;
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </table>\r\n\r\n    <input type=\"hidden\" id=\"returnUrl\"");
            BeginWriteAttribute("value", " value=\"", 2220, "\"", 2250, 1);
#nullable restore
#line 65 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
WriteAttributeValue("", 2228, ViewData["returnUrl"], 2228, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n    <input type=\"hidden\" id=\"id\"");
            BeginWriteAttribute("value", " value=\"", 2288, "\"", 2315, 1);
#nullable restore
#line 66 "E:\E-School\EschoolHesabiNew\ESchool\Views\Class\UserShow.cshtml"
WriteAttributeValue("", 2296, ViewData["RoomId"], 2296, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n\r\n</div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<RoomUser>> Html { get; private set; }
    }
}
#pragma warning restore 1591