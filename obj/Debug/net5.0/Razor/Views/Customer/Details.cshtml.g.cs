#pragma checksum "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b24eedb81188ef288c348ec3351b24737227f749"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_Details), @"mvc.1.0.view", @"/Views/Customer/Details.cshtml")]
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
#line 1 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\_ViewImports.cshtml"
using BikesTest;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\_ViewImports.cshtml"
using BikesTest.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b24eedb81188ef288c348ec3351b24737227f749", @"/Views/Customer/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a9a915356f956d37f00bd5e67ac69bfe10a60d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BikesTest.Models.Customer>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("profile-menu-link"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ChangePassword", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RedeemCoupon", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Subscription", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CustomerIndex", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Coupon", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
   ViewData["Title"] = "My Profile"; 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""Profile-Page"">
	<div class=""profile-div"">
		<div class=""profile-container-menu"">
			<div class=""profile-menu"">
				<div class=""profile-menu-option"">
					<h6 class=""profile-menu-link Edit-link"" id=""Edit""><img class=""profile-menu-icons Edit-icon"" src=""/images/profile/edit.png""");
            BeginWriteAttribute("alt", " alt=\"", 361, "\"", 367, 0);
            EndWriteAttribute();
            WriteLiteral("> Edit</h6>\n\n\t\t\t\t</div>\n\n\t\t\t\t<div class=\"profile-menu-option\">\n\t\t\t\t\t<h6 class=\"profile-menu-link Password-link\" id=\"Password\"><img class=\"profile-menu-icons Password-icon\" src=\"/images/profile/padlock.png\"");
            BeginWriteAttribute("alt", " alt=\"", 573, "\"", 579, 0);
            EndWriteAttribute();
            WriteLiteral("> Password</h6>\n\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b24eedb81188ef288c348ec3351b24737227f7496646", async() => {
                WriteLiteral("Change Password");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 15 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                                                                               WriteLiteral(Model.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\t\t\t\t</div>\n\n");
#nullable restore
#line 18 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                 if (User.IsInRole("Customer"))
				{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t<div class=\"profile-menu-option\">\n\t\t\t\t\t\t<h6 class=\"profile-menu-link Profile-link\" id=\"Profile\"><img class=\"profile-menu-icons Profile-icon\" src=\"/images/profile/user.png\"");
            BeginWriteAttribute("alt", " alt=\"", 933, "\"", 939, 0);
            EndWriteAttribute();
            WriteLiteral("> Profile</h6>\n\t\t\t\t\t</div>\n");
            WriteLiteral("\t\t\t\t\t<div class=\"profile-menu-option\">\n\t\t\t\t\t\t<h6 class=\"profile-menu-link Redeem-Coupons-link\" id=\"Redeem-Coupons\"><img class=\"profile-menu-icons Redeem-Coupons-icon\" src=\"/images/profile/discount-coupon.png\"");
            BeginWriteAttribute("alt", " alt=\"", 1176, "\"", 1182, 0);
            EndWriteAttribute();
            WriteLiteral("> Redeem Coupons</h6>\n\t\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b24eedb81188ef288c348ec3351b24737227f7499986", async() => {
                WriteLiteral("Redeem Coupon");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 26 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                                                                                 WriteLiteral(Model.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n\t\t\t\t\t</div>\n");
            WriteLiteral("\t\t\t\t\t<div class=\"profile-menu-option\">\n\t\t\t\t\t\t<h6 class=\"profile-menu-link Subscriptions-link\" id=\"Subscriptions\"><img class=\"profile-menu-icons Subscriptions-icon\" src=\"/images/profile/subscription-model.png\"");
            BeginWriteAttribute("alt", " alt=\"", 1531, "\"", 1537, 0);
            EndWriteAttribute();
            WriteLiteral("> My Subscriptions</h6>\n\t\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b24eedb81188ef288c348ec3351b24737227f74912719", async() => {
                WriteLiteral("My subscriptions");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 32 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                                                                                                                WriteLiteral(Model.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n\t\t\t\t\t</div>\n");
            WriteLiteral("\t\t\t\t\t<div class=\"profile-menu-option\">\n\t\t\t\t\t\t<h6 class=\"profile-menu-link Coupons-link\" id=\"Coupons\"><img class=\"profile-menu-icons Coupons-icon\" src=\"/images/profile/promo-code.png\"");
            BeginWriteAttribute("alt", " alt=\"", 1896, "\"", 1902, 0);
            EndWriteAttribute();
            WriteLiteral("> My Coupons</h6>\n\t\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b24eedb81188ef288c348ec3351b24737227f74915661", async() => {
                WriteLiteral("My Coupons");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 38 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                                                                                                  WriteLiteral(Model.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\t\t\t\t\t</div>\n\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b24eedb81188ef288c348ec3351b24737227f74918187", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 40 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                                                                     WriteLiteral(Model.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" \n");
#nullable restore
#line 41 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
				}
				else
				{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b24eedb81188ef288c348ec3351b24737227f74920665", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b24eedb81188ef288c348ec3351b24737227f74921918", async() => {
                WriteLiteral("subscriptions");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 45 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                                                                                                    WriteLiteral(Model.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b24eedb81188ef288c348ec3351b24737227f74924431", async() => {
                WriteLiteral("Coupons");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 46 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                                                                                              WriteLiteral(Model.id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 47 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
				}

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t</div>\n\t\t</div>\n\n\t\t<div class=\"profile-container-details\">\n\t\t\t<div class=\"profile-menu-content\">\n\t\t\t\t<h4 class=\"h4-Profile\" style=\"text-align: center;\">Profile Details</h4>\n\t\t\t\t<table>\n\t\t\t\t\t<tr>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 56 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayNameFor(model => model.user.firstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 57 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayFor(model => model.user.firstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t</tr>\n\t\t\t\t\t<tr>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 60 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayNameFor(model => model.user.lastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 61 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayFor(model => model.user.lastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t</tr>\n\t\t\t\t\t<tr>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 64 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayNameFor(model => model.user.username));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 65 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayFor(model => model.user.username));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t</tr>\n\t\t\t\t\t<tr>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 68 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayNameFor(model => model.user.email));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 69 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayFor(model => model.user.email));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t</tr>\n\t\t\t\t\t<tr>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 72 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayNameFor(model => model.user.birthday));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 73 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayFor(model => model.user.birthday));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t</tr>\n\t\t\t\t\t<tr>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 76 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayNameFor(model => model.isCurrentlyBiking));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 77 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayFor(model => model.isCurrentlyBiking));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t</tr>\n\t\t\t\t\t<tr>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 80 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayNameFor(model => model.timeBiked));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 81 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayFor(model => model.timeBiked));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t</tr>\n\t\t\t\t\t<tr>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 84 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayNameFor(model => model.numberOfBikesRented));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 85 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayFor(model => model.numberOfBikesRented));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t</tr>\n\t\t\t\t\t<tr>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 88 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayNameFor(model => model.points));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t\t<td>");
#nullable restore
#line 89 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                       Write(Html.DisplayFor(model => model.points));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t\t</tr>\n\t\t\t\t</table>\n\t\t\t</div>\n\t\t</div>\n\t</div>\n\n\n\t<script type=\"text/javascript\">\n\t\tvar userId = ");
#nullable restore
#line 98 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                Write(Model.id);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\n\t\tvar profileId = ");
#nullable restore
#line 99 "D:\prog projects\ASP.NET MVC\BikesTest\BikesCo\Views\Customer\Details.cshtml"
                   Write(Model.user_id);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\n\t</script>\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BikesTest.Models.Customer> Html { get; private set; }
    }
}
#pragma warning restore 1591
