'use strict';
// $("#modal-get-info").fancybox().trigger('click');
/* HELPER: Checks Whether an Element Exists
----------------------------------------------------------------------------------------------------*/
(function( $ ) {

  $.fn.extend({
    exists: function() {
      if ( this.length > 0 ) {
        return true;
      } else {
        return false;
      }
    }
  });

})( jQuery );


function menu_table(){
  $(".menu-toggle").click(function(){
    var data_src=$(this).attr("data-src");
    if($(this).children(".span-menu").hasClass("active-span-menu")){
      $(data_src).removeClass("active-submenu");
      $(this).children(".span-menu").removeClass("active-span-menu");
    }else{
      $(data_src).addClass("active-submenu");
      $(this).children(".span-menu").addClass("active-span-menu");
    }
  })
}
menu_table();

function scrool_to_div(){
  $(".page-ebook-detail .element-arrow-down").click(function(){
    if($(window).width>768){
      $('html,body').animate({
          scrollTop: $(".related-news").offset().top},
          'slow');
    }else{
      $('html,body').animate({
          scrollTop: $(".related-news").offset().top - 60},
          'slow');
    }
    
  })
  $(".page-truong-ngoai-khoa .element-arrow-down").click(function(){
    if($(window).width>768){
      $('html,body').animate({
          scrollTop: $(".slider-ngoai-khoa").offset().top},
          'slow');
    }else{
      $('html,body').animate({
          scrollTop: $(".slider-ngoai-khoa").offset().top - 60},
          'slow');
    }
    
  })
}
scrool_to_div();

function menu_click(){
  $(".toggle-icon").click(function(){
    $(this).toggleClass("active");
    $(".main-menu").toggleClass("active-menu-mobile");
    $(".sub-menu-2").removeClass('active-sub-menu-2');
  })
  if($(window).width()<1024.1){
    $(".sub-menu-1").slideUp();
    $(".active-sub-1").children(".sub-menu-1").slideDown();
    $(".sub-menu-1").siblings("a").click(function(){
      // $(this).slideToggle();
      
      if($(this).parent(".has-sub-menu-1").hasClass("active-sub-1")){
        $(this).parent(".has-sub-menu-1").removeClass("active-sub-1");
        $(this).siblings(".sub-menu-1").slideUp();
      }else{
        $(this).parent(".has-sub-menu-1").toggleClass('active-sub-1');
        $(this).siblings(".sub-menu-1").slideToggle();
      }
      
    })
    $(".sub-menu-1 ul li a").click(function(){
      $(this).siblings(".sub-menu-2").addClass("active-sub-menu-2");
      var get_text_breck_crum_1=$(this).parent("li").parent("ul").parent(".sub-menu-1").siblings("a").text();
      var get_text_breck_crum_2=$(this).text();
      $(this).parent("li").find(".breck-crum-1 span").html(get_text_breck_crum_1);
      $(this).parent("li").find(".breck-crum-2 span").html(get_text_breck_crum_2);
    })


    $(".breck-crum-menu").click(function(){
      $(".sub-menu-2").removeClass('active-sub-menu-2');
    })
  }
  $(".ul-main").children("li").children("a").hover(function(){
    $(this).parent("li").addClass("li-hover");
    }, function(){
    $(this).parent("li").removeClass("li-hover");
  });
}
menu_click();

function back_to_top(){
  var btn = $('.back-to-top');

  $(window).scroll(function() {
    if ($(window).scrollTop() > 300) {
      btn.addClass('show');
    } else {
      btn.removeClass('show');
    }
  });

  btn.on('click', function(e) {
    e.preventDefault();
    $('html, body').animate({scrollTop:0}, '300');
  });
}
back_to_top();

// slider 
var bannerOwl = $('#bannerCarousel').owlCarousel({
    loop: true,
    margin: 10,
    nav: false,
    dots: true,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 1
        },
        1000: {
            items: 1
        }
    },
    autoplay: true,
    autoplayTimeout: 5000 // Thời gian chuyển đổi ảnh (5 giây)
});

// Khởi tạo carousel cho Slider News
var newsOwl = $('#newsCarousel').owlCarousel({
    loop: true,
    margin: 10,
    nav: false,
    dots: true,
    nav: true,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 2
        },
        1000: {
            items: 3
        }
    }
});


$('.home-gellary-slider .owl-carousel').owlCarousel({
    loop:true,
    margin:0,
    nav:false,
    dots:true,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },  
        1000:{
            items:1
        }
    }
})

$('.slider-news .owl-carousel').owlCarousel({
    loop:true,
    margin:10,
    nav:false,
    dots: true,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:2
        },
        1000:{
            items:3
        }
    }
})
function owlInitialize() {
  if ($(window).width() <768) {
      $('.blk-three-col').addClass("owl-carousel owl-theme");
      $('.blk-three-col').owlCarousel({
            loop: true,
            margin: 0,
            nav: false,
            dots:false,
            stagePadding: 70,
            responsive:{
                0:{
                    items: 1,
                    dots:true,
                    stagePadding: 40,
                    margin:10
                },
                400:{
                    items: 1,
                    dots:true,
                    stagePadding: 50,
                    margin:10
                },    
                576:{
                    items: 1,
                    dots:true,
                    stagePadding: 60,
                    margin:15
                },
                768:{
                    items: 1,
                    dots:true,
                    stagePadding: 70,
                    margin:15
                }
            }
      })
  }else{
      $('.blk-three-col').owlCarousel('destroy');
      $('.blk-three-col').removeClass("owl-carousel owl-theme");
  }
}
owlInitialize();



$('.slider-news .owl-carousel').trigger("to.owl.carousel", [2, 1])
$('.home-gellary-slider .owl-carousel').trigger("to.owl.carousel", [2, 1])
$('.blk-banner-slider  .owl-carousel').trigger("to.owl.carousel", [3, 1])


var App = {
    get_max_height_home: function () {
      var max_height=1;
      $( ".tai-sao-chon .group" ).children(".md-col").each(function( i ) {
          $(".tai-sao-chon .group .md-col .inner-md-col").css("min-height","auto");
          var height_md_cold=$(this).children(".inner-md-col").outerHeight()
          if(height_md_cold> max_height){
            max_height=height_md_cold
          }
          $(".tai-sao-chon .group .md-col .inner-md-col").css("min-height",max_height);
      });
    },
    owl_show_on_mobile: function () {
      //  số ký tự tối đa cho textarea
      function startCarousel(){
        $('.md-slider-center .owl-carousel').owlCarousel({
            stagePadding: 70,
            loop: true,
            margin: 0,
            nav: false,
            dots:false,
            responsive:{
                0:{
                    items: 1,
                    stagePadding: 50
                },
                400:{
                    items: 1,
                    stagePadding: 50
                },
                576:{
                    items: 1,
                    stagePadding: 70
                },
                1000:{
                    items: 1
                }
            }
        });
      }
      // startCarousel();
      function stopCarousel() {
        var owl = $('.md-slider-center .owl-carousel');
        owl.trigger('destroy.owl.carousel');
        owl.addClass('off');
      }
      $(document).ready(function() {
        if ( $(window).width() < 768.1 ) {
          startCarousel();
        } else {
          $('.md-slider-center-2 .owl-carousel').addClass('off');
           stopCarousel();
        }
      });

      $(window).resize(function() {
          if ( $(window).width() < 768.1 ) {
            startCarousel();
          } else {
            stopCarousel();
          }
      });
    },
    set_min_height_item_blk_three_col: function(){
      $(".blk-three-col .owl-stage-outer .item").css("min-height","auto");
      var get_height=$(".blk-three-col .owl-stage").height();
      $(".blk-three-col .owl-stage-outer .item").css("min-height",get_height);
    },
    padding_bt_home_register: function () {
      var padding_bt_home_register=$(".home-register .img-right img").outerHeight()-$(".home-register .content").outerHeight();
      $(".home-register").css("padding-bottom",padding_bt_home_register/2);
      
    },
    call_scroll_popup_skill: function(){
      //$(".md-skill .info").mCustomScrollbar();
    }
};
jQuery(document).ready(function () {
    App.get_max_height_home();
    // App.cta_popup_share();
    // App.show_error_textarea();
    App.owl_show_on_mobile();
    App.padding_bt_home_register();
     App.set_min_height_item_blk_three_col();
     App.call_scroll_popup_skill();
    // App.set_scrool();
    jQuery(window).resize(function () {
      App.get_max_height_home();
      App.padding_bt_home_register();
       App.set_min_height_item_blk_three_col();
       owlInitialize();
    });
    window.addEventListener("resize", function(event) {
        App.get_max_height_home();
        App.owl_show_on_mobile();
        App.padding_bt_home_register();
         App.set_min_height_item_blk_three_col();
         owlInitialize();
    })
});


$(window).scroll(function() {
  if ($(window).scrollTop() > 10) {
    $(".page-header").addClass("header-fixed");
  } else {
    $(".page-header").removeClass("header-fixed");
  }
});
$('#slide-big').owlCarousel({
   items:1,
   loop:false,
   dots:false,
   nav:false,
   center:false,
   margin:0,
   URLhashListener:true
});
$('#slide-thumb').owlCarousel({
  nav:false,
  margin:10,
     responsive:{  
    0:{
           center:false,
           items:3,
           loop:false,
           loop:true,
           dots:false,
           margin:10,
           URLhashListener:true
       },
       768:{
           items:5,
           loop:false,
           loop:true,
           dots:false,
           margin:10,
           URLhashListener:true
       }
    }
});



$('.ui.dropdown')
  .dropdown()
;

///function call modal đăng ký thành công


// $("#modal-sucess").fancybox().trigger('click');
// $("#md-tk").fancybox().trigger('click');
// $("#md-tk-2").fancybox().trigger('click');


/*$(document).ready(function(){
    $("select").change(function(){
        $(this).find("option:selected").each(function(){
            var optionValue = $(this).attr("value");
            console.log(optionValue);
            if(optionValue){
                $(".box").not("." + optionValue).hide();
                $("." + optionValue).show();

                // select active marker
                activeMarker(optionValue);
            } else{
                $(".box").hide();
            }
        });
    })
});
*/


new WOW().init();
