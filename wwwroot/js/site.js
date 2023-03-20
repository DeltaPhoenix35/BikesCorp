// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var menuId;

$(document).ready(function(){
   $('.profile-menu-link').mouseover(function() {
      menuId = this.id;
      });

     
     $('.profile-menu-link').hover(
      function(){
         $("." + menuId + "-icon").addClass("profile-menu-icons-hovered");
         $("." + menuId + "-link").addClass("profile-menu-link-hovered");
	},
      function(){
         $("." + menuId + "-icon").removeClass("profile-menu-icons-hovered");
         $("." + menuId + "-link").removeClass("profile-menu-link-hovered");
   }
   );
});

$(document).ready(function(){
   $('#Edit').click(function(){   
       $('.profile-container-details').load('https://localhost:44364/Customer/Edit/'+ userId + ' .profile-menu-content');
   });
   $('#Profile').click(function(){   
       $('.profile-container-details').load('https://localhost:44364/Customer/Details/' + profileId + ' .profile-menu-content');
   });
   $('#Redeem-Coupons').click(function(){
       $('.profile-container-details').load('https://localhost:44364/Customer/RedeemCoupon/'+ userId +' .profile-menu-content');
   });
   $('#Password').click(function(){
       $('.profile-container-details').load('https://localhost:44364/Customer/ChangePassword/'+ userId + ' .profile-menu-content');
   });

});
