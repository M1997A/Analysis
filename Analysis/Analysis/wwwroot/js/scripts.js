/*global $ , window*/
$(function () {
    'use strict';
    var myheader = $('.header'),
        myslider = $('.bxslider');
    
    myheader.height($(window).height());
    $(window).resize(function () {
        myheader.height($(window).height());
        
        myslider.each(function () {
       
        
            $(this).css('paddingTop', ($(window).height() - $('.bxslider li').height()) / 2);
        });
        
        
    });
    //links c
    $('.links li a').click(function () {
        
        $(this).parent().addClass('active').siblings().removeClass('active');
    });
    //center
    myslider.each(function () {
       
        $(this).css('paddingTop', ($(window).height() - $('.bxslider li').height()) / 2);
        
    });
        
    myslider.bxSlider({
        
        pager : false
    });
  //smooth
    $('.links li a').click(function () {
       
        $('html, body').animate({scrollTop: $('#' + $(this).data('value')).offset().top}, 1000);
    });
    $('.ge1').click(function () {
       
        $('html, body').animate({scrollTop: $('#gemail').offset().top}, 1000);
    });
    
    //auto slider
    
    (function autoslider2() {
        
        $('.slider .active').each(function () {
            
            if (!$(this).is(':last-child')) {
                        
                $(this).delay(2000).fadeOut(1000, function () {
                    $(this).removeClass('active').next().addClass('active').fadeIn();
                    autoslider2();
                });
            } else {
                
                
                $(this).delay(2000).fadeOut(1000, function () {
                    
                    $(this).removeClass('active');
                    
                    $('.slider div').eq(0).addClass('active').fadeIn();
                    autoslider2();
                });
            }
        });
        
    }());
    
    //triggers nice scroll 
    $('html').niceScroll({cursorcolor: '#1abc9c',
                          cursorwidth: '5px',
                          cursorborder: 'none'
                         });
});