// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function ($) {
    "use strict";

    // Sticky Navbar
    $(window).scroll(function () {
        if ($(this).scrollTop() > 40) {
            $('.navbar').addClass('sticky-top');
        } else {
            $('.navbar').removeClass('sticky-top');
        }
    });

    // Dropdown on mouse hover
    $(document).ready(function () {
        function toggleNavbarMethod() {
            if ($(window).width() > 992) {
                $('.navbar .dropdown').on('mouseover', function () {
                    $('.dropdown-toggle', this).trigger('click');
                }).on('mouseout', function () {
                    $('.dropdown-toggle', this).trigger('click').blur();
                });
            } else {
                $('.navbar .dropdown').off('mouseover').off('mouseout');
            }
        }
        toggleNavbarMethod();
        $(window).resize(toggleNavbarMethod);
    });

    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 1500, 'easeInOutExpo');
        return false;
    });

    // Product carousel
    $(".product-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1000,
        margin: 45,
        dots: false,
        loop: true,
        nav: true,
        navText: [
            '<i class="bi bi-arrow-left"></i>',
            '<i class="bi bi-arrow-right"></i>'
        ],
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            },
            1200: {
                items: 4
            }
        }
    });
})(jQuery);

var connection = new signalR.HubConnectionBuilder().withUrl("/SignalRHub").build();

connection.start().catch(err => alert(err));

connection.on("NewNotification", function () {
    ShowNewNotification();
})
function ShowNewNotification() {
    let popup = document.getElementById("pop_up_content");
    fetch("/Notifications/notification?handler=GetNewNotification")
        .then(res => res.json())
        .then(data => data.forEach(item => {
            console.log(item);
            let text = '<span>${item.Title}</span><br><span><${item.Content}/span>'
            popup.innerHTML = text;
        }))
}
// Disable Developer Mode
//document.onkeydown = function (e) {
//    if (e.keyCode == 123) {
//        return false; // F12
//    }
//    if (e.ctrlKey && e.shiftKey && (e.keyCode == 'I'.charCodeAt(0) || e.keyCode == 'C'.charCodeAt(0) || e.keyCode == 'J'.charCodeAt(0) || e.keyCode == 'U'.charCodeAt(0))) {
//        return false; // Ctrl+Shift+I, Ctrl+Shift+C, Ctrl+Shift+J, Ctrl+U
//    }
//};

// Check Developer Mode
//(function () {
//    var element = new Image();
//    var devtoolsOpen = false;
//    element.__defineGetter__('id', function () {
//        devtoolsOpen = true;
//        console.log("Developer tools are open");
//    });
//    setInterval(function () {
//        devtoolsOpen = false;
//        console.log(element);
//        if (devtoolsOpen) {
//            alert('Please do not open developer tools!');
//        }
//    }, 1000);
//})();
