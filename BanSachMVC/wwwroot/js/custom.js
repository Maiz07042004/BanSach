// on ready function
  $(document).ready(function() {
	"use strict";
  
  // Preloader 
	jQuery(window).on('load', function() {
		jQuery("#status").fadeOut();
		jQuery("#preloader").delay(350).fadeOut("slow");
	});
	
	
	// Menu button
	$('#menu_click').on('click', function(){
		this.classList.toggle("change");
		$('.ss_menu').slideToggle();
	});
	
	$('.dropdown').hover(function() {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(500);
      }, function() {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(500);
	});
	
	
		// ===== Scroll to Top ==== 
	$(window).scroll(function() {
		if ($(this).scrollTop() >= 100) {       
			$('#return-to-top').fadeIn(200);   
		} else {
			$('#return-to-top').fadeOut(200);  
		}
	});
	$('#return-to-top').on('click', function() {     
		$('body,html').animate({
			scrollTop : 0                
		}, 500);
	});
	
	// Featured Products Js
	$('.ss_featured_products .owl-carousel').owlCarousel({
		loop:true,
		margin:0,
		nav:true,
		navText:["<i class='fa fa-angle-left'></i>" , "<i class='fa fa-angle-right'></i>"],
		dots:false,
		responsive:{
			0:{
				items:1
			},
			600:{
				items:2
			},
			900:{
				items:2
			},
			1000:{
				items:3
			},
			1200:{
				items:4
			}
		}
	});
	
	// Latest Products Js
	$('.ss_latest_products .owl-carousel').owlCarousel({
		loop:true,
		margin:0,
		nav:true,
		navText:["<i class='fa fa-angle-left'></i>" , "<i class='fa fa-angle-right'></i>"],
		dots:false,
		responsive:{
			0:{
				items:1
			},
			600:{
				items:2
			},
			900:{
				items:2
			},
			1000:{
				items:3
			},
			1200:{
				items:4
			}
		}
	});
	
	// Best Seller Js
	$('.ss_best_seller .owl-carousel').owlCarousel({
		loop:true,
		margin:0,
		nav:true,
		navText:["<i class='fa fa-angle-left'></i>" , "<i class='fa fa-angle-right'></i>"],
		dots:false,
		responsive:{
			0:{
				items:1
			},
			600:{
				items:2
			},
			900:{
				items:2
			},
			1000:{
				items:2
			},
			1200:{
				items:3
			}
		}
	});
	
	// News Slider Js
	$('.ss_news_slider .owl-carousel').owlCarousel({
		loop:true,
		margin:15,
		nav:true,
		navText:["<i class='fa fa-angle-left'></i>" , "<i class='fa fa-angle-right'></i>"],
		dots:false,
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
	});
	
	// Brandlogo Slider Js
	$('.ss_brandlogo_slider .owl-carousel').owlCarousel({
		loop:true,
		autoplay:true,
		margin:15,
		nav:true,
		navText:["<i class='fa fa-angle-left'></i>" , "<i class='fa fa-angle-right'></i>"],
		dots:false,
		responsive:{
			0:{
				items:1
			},
			600:{
				items:4
			},
			1000:{
				items:6
			}
		}
	});
	
	// featured category Slider Js
	$('.ss_feat_cate_slider .owl-carousel').owlCarousel({
		loop:true,
		margin:10,
		nav:true,
		navText:["<i class='fa fa-angle-left'></i>" , "<i class='fa fa-angle-right'></i>"],
		dots:false,
		responsive:{
			200:{
				items:1
			},
			300:{
				items:2
			},
			600:{
				items:3
			},
			900:{
				items:4
			},
			1000:{
				items:5
			},
			1200:{
				items:5
			}
		}
	});
	
	// Top Rated Sidebar Slider Js
	$('.ss_toprated_slider .owl-carousel').owlCarousel({
		loop:true,
		margin:10,
		nav:true,
		navText:["<i class='fa fa-angle-left'></i>" , "<i class='fa fa-angle-right'></i>"],
		dots:false,
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
	});
	
	//* slider *//
	 $(document).ready(function() {
            $('.cc_ps_top_slider_section .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
                autoplay: false,
                responsiveClass: true,
                smartSpeed: 1200,
                navText: ['<i class="fa fa-angle-left" aria-hidden="true"></i>', '<i class="fa fa-angle-right" aria-hidden="true"></i>'],
                responsive: {
                    0: {
                        items: 1,
                        nav: true
                    },
                    600: {
                        items: 1,
                        nav: true
                    },
                    1000: {
                        items: 1,
                        nav: true,
                        loop: true,
                        margin: 20
                    }
                }
            })
        })

		
		$(document).ready(function() {
            $('.ss_news_slider_box_img .owl-carousel').owlCarousel({
                loop: true,
                margin: 10,
                autoplay: true,
                responsiveClass: true,
                smartSpeed: 1200,
                navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
                responsive: {
                    0: {
                        items: 1,
                        nav: true
                    },
					500: {
                        items: 1,
                        nav: true
                    },
                    700: {
                        items: 1,
                        nav: true
                    },
                    1000: {
                        items: 1,
                        nav: true,
                        loop: true,
                        margin: 20
                    }
                }
            })
        })
		
		
		
		
		
		// Vertical Menu dropdown min-height
        var $vertical_menu = $( '.vertical-menu' ),
            vertical_menu_height = $vertical_menu.height(),
            vm_header_height = 52.25,
            dd_menu_min_height = vertical_menu_height - vm_header_height;

        $vertical_menu.find( '.dropdown > .dropdown-menu' ).each( function() {
            $(this).css( 'min-height', dd_menu_min_height );
            $(this).find( '.menu-item-object-static_block' ).css( 'min-height', dd_menu_min_height );
        });

        var $list_group_dd = $( '.vertical-menu > .list-group-item > .dropdown-menu' ),
            list_group_dd_style = $list_group_dd.attr( 'style' ),
            list_group_dd_height = 0;

        $list_group_dd.css({
            visibility: 'hidden',
            display:    'none'
        });

        list_group_dd_height = $list_group_dd.height() + 15;

        $list_group_dd.attr( 'style', list_group_dd_style ? list_group_dd_style : '' );

        $list_group_dd.find( '.dropdown-menu' ).each( function() {
            $(this).css( 'min-height', list_group_dd_height );
            $(this).find( '.menu-item-object-static_block' ).css( 'min-height', list_group_dd_height );
        });

        // Departments menu Height
        var $departments_menu_dropdown = $( '.departments-menu-dropdown' ),
            departments_menu_dropdown_height = $departments_menu_dropdown.height();

        $departments_menu_dropdown.find( '.dropdown > .dropdown-menu' ).each( function() {
            $(this).find( '.menu-item-object-static_block' ).css( 'min-height', departments_menu_dropdown_height + 24 );
            $(this).css( 'min-height', departments_menu_dropdown_height + 28 );
        });

        $( '.vertical-menu, .departments-menu-dropdown' ).on( 'mouseleave', function() {
            var $this = $(this);
            $this.removeClass( 'animated-dropdown' );
        });

        $( '.vertical-menu .menu-item-has-children, .departments-menu-dropdown .menu-item-has-children' ).on({
            mouseenter: function() {
                var $this = $(this),
                    $dropdown_menu = $this.find( '.dropdown-menu' ),
                    $vertical_menu = $this.parents( '.vertical-menu' ),
                    $departments_menu = $this.parents( '.departments-menu-dropdown' ),
                    css_properties = {
                        width:      540,
                        opacity:    1
                    },
                    animation_duration = 300,
                    has_changed_width = true,
                    animated_class = '',
                    $container = '';

                if ( $vertical_menu.length > 0 ) {
                    $container = $vertical_menu;
                } else if ( $departments_menu.length > 0 ) {
                    $container = $departments_menu;
                }

                if ( $this.hasClass( 'yamm-tfw' ) ) {
                    css_properties.width = 540;

                    if ( $departments_menu.length > 0 ) {
                        css_properties.width = 600;
                    }
                } else if ( $this.hasClass( 'yamm-fw' ) ) {
                    css_properties.width = 900;
                } else if ( $this.hasClass( 'yamm-hw' ) ) {
                    css_properties.width = 450;
                } else {
                    css_properties.width = 277;
                }

                $dropdown_menu.css( {
                    visibility: 'visible',
                    display:    'block'
                } );

                if ( ! $container.hasClass( 'animated-dropdown' ) ) {
                    $dropdown_menu.animate( css_properties, animation_duration, function() {
                        $container.addClass( 'animated-dropdown' );
                    });
                } else {
                    $dropdown_menu.css( css_properties );
                }
            }, mouseleave: function() {
                $(this).find( '.dropdown-menu' ).css({
                    visibility: 'hidden',
                    opacity:    0,
                    width:      0,
                    display:    'none'
                });
            }
        });
		



	 var xv_ww=$(window).width(),xv_slideshow=true;
    $(window).on('resize load', function() {
        xv_ww = $(window).width();
        if(xv_ww <= 991) {
            $('.responsive-menu').removeClass('xv-menuwrapper').addClass('dl-menuwrapper');
            $('.lg-submenu').addClass("dl-submenu");
        } else {
            $('.responsive-menu').removeClass('dl-menuwrapper').addClass('xv-menuwrapper');
            $('.lg-submenu').removeClass("dl-submenu");
        }
    });
    $('#dl-menu').dlmenu({
        animationClasses: {
            classin: 'dl-animate-in-5',
            classout: 'dl-animate-out-5'
        }
    });		
    $('#dl-menu-three').dlmenu({
        animationClasses: {
            classin: 'dl-animate-in-5',
            classout: 'dl-animate-out-5'
        }
    });	
	
	
	 // Main Slider Animation

        (function($) {

            //Function to animate slider captions 
            function doAnimations(elems) {
                //Cache the animationend event in a variable
                var animEndEv = 'webkitAnimationEnd animationend';

                elems.each(function() {
                    var $this = $(this),
                        $animationType = $this.data('animation');
                    $this.addClass($animationType).one(animEndEv, function() {
                        $this.removeClass($animationType);
                    });
                });
            }

            //Variables on page load 
            var $myCarousel = $('#carousel-example-generic'),
                $firstAnimatingElems = $myCarousel.find('.item:first').find("[data-animation ^= 'animated']");

            //Initialize carousel 
            $myCarousel.carousel();

            //Animate captions in first slide on page load 
            doAnimations($firstAnimatingElems);

            //Pause carousel  
            $myCarousel.carousel('pause');


            //Other slides to be animated on carousel slide event 
            $myCarousel.on('click slide.bs.carousel', function(e) {
                var $animatingElems = $(e.relatedTarget).find("[data-animation ^= 'animated']");
                doAnimations($animatingElems);
            });


        })(jQuery);
		
				// Theme-banner slider 
function BannerSlider () {
  var banner = $("#theme-main-banner");
  if (banner.length) {
      banner.camera({ //here I declared some settings, the height and the presence of the thumbnails 
        height: '727px',
        navigation: true,
        pagination: true,
        thumbnails: false,
        playPause: false,
        autoplay:true,
        pauseOnClick: false,
        hover: false,
        overlayer: true,
        loader: 'none',
        time: 5000,
        minHeight: '600px',
      });
  };
}
// DOM ready function
jQuery(document).on('ready', function(){
	(function($){
		BannerSlider ();
	})(jQuery);
});



jQuery(document).ready(function (e) {
    function t(t) {
        e(t).bind("click", function (t) {
            t.preventDefault();
            e(this).parent().fadeOut()
        })
    }
    e(".dropdown-toggle").on("click",function () {
        var t = e(this).parents(".button-dropdown").children(".dropdown-menu").is(":hidden");
        e(".button-dropdown .dropdown-menu").hide();
        e(".button-dropdown .dropdown-toggle").removeClass("active");
        if (t) {
            e(this).parents(".button-dropdown").children(".dropdown-menu").toggle().parents(".button-dropdown").children(".dropdown-toggle").addClass("active")
        }
    });
    e(document).bind("click", function (t) {
        var n = e(t.target);
        if (!n.parents().hasClass("button-dropdown")) e(".button-dropdown .dropdown-menu").hide();
    });
    e(document).bind("click", function (t) {
        var n = e(t.target);
        if (!n.parents().hasClass("button-dropdown")) e(".button-dropdown .dropdown-toggle").removeClass("active");
    })
});
		
		
	
});