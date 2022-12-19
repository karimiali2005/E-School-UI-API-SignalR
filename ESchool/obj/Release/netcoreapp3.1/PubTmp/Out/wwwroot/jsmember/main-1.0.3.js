var div = document.getElementById("cart-side");
var a = document.getElementById("a");
var divC = document.getElementById("cart-sideC");
var aC = document.getElementById("aC");
// start change16
var divL = document.getElementById("cart-sideL");
var aL = document.getElementById("aL");
// end change16

function openCart() {
    div.style.right = 0;
    div.style.backgroundColor = "red";
    a.style.opacity = 0.8;
    a.style.visibility = "visible";
}

function closeCart() {
    div.style.right = "-300px";
    a.style.opacity = 0;
    a.style.visibility = "hidden";
}

function closeContact() {
    divC.style.right = "-300px";
    aC.style.opacity = 0;
    aC.style.visibility = "hidden";
}
$(document).ready(function () {
    $('.button-panel-menu-side').click(function () {
        $('.panel-menu-slider').toggleClass('show');
        $('.content-all div.content').toggleClass('show');
    });
    if ($(window).width() <= 576) {
        $('.panel-menu-slider').toggleClass('show');
        $('.content-all div.content').toggleClass('show');
    }
    $(".dropdown-toggle").dropdown();
});

// start change10

// start change16
function openMenu_LOG() {
    divL.style.right = 0;
    divL.style.backgroundColor = "red";
    aL.style.opacity = 0.8;
    aL.style.visibility = "visible";
}
function closeMenu_LOG() {
    divL.style.right = "-300px";
    aL.style.opacity = 0;
    aL.style.visibility = "hidden";
}
// end change16
$('.btn-stu-desc-show').mouseover(function () {
    console.log("dddd")
    $(this).find(".desc-stu").first().css({
        "display": "block"
    })
})
$('.btn-stu-desc-show').mouseout(function () {
    console.log("oooo")
    $(this).find(".desc-stu").first().css({
        "display": "none"
    })
})
$('.btn-teach-desc-show').mouseover(function () {
    console.log("dddd")
    $(this).find(".desc-teach").first().css({
        "display": "block"
    })
})
$('.btn-teach-desc-show').mouseout(function () {
    console.log("oooo")
    $(this).find(".desc-teach").first().css({
        "display": "none"
    })
})


function getUrl() {
    var url = window.location.pathname + window.location.search;
    url = url.replaceAll('&','||');
    return url;
}
function goBack(returnUrl) {
    returnUrl = returnUrl.replaceAll('||', '&');
    window.location.href = returnUrl;
     

}