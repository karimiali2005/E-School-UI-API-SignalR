document.addEventListener("DOMContentLoaded", function () {
    setHeightSlideMenu();
    readySlideMenu();
});
window.addEventListener("resize", setHeightSlideMenu);

function setHeightSlideMenu() {
    var slidemenu = document.getElementsByClassName("slidemenu")[0];
    if (!Empty(slidemenu)) {
        var smenu = document.getElementById('smenu');
        var content = document.getElementsByClassName("content")[0];

        var body = document.body, html = document.documentElement;
        var heightPage = Math.max(body.scrollHeight, body.offsetHeight, html.clientHeight, html.scrollHeight, html.offsetHeight);
        slidemenu.style["min-height"] = heightPage + "px";
        slidemenu.style["max-width"] =  "70px";
        content.style["min-height"] = heightPage + "px";
    }    
}

function readySlideMenu() {    

    var buttonmenu = document.getElementsByClassName("buttonmenuPanel")[0];
    if (!Empty(buttonmenu)) {
        buttonmenu.addEventListener("click", function () {
            buttonmenu.classList.toggle("change");
            var spans = document.querySelectorAll('.slidemenu ul li a span');
            for (let span of spans) {
                span.classList.toggle('show');
            }

        
            //var slidetextlinks = document.getElementsByClassName("slidetextlink");
            //for (let item of slidetextlinks) {
            //    item.classList.toggle("show");
            //}
            //var slidetextovers = document.getElementsByClassName("slidetextover");
            //for (let item of slidetextovers) {
            //    item.classList.toggle("hide");
            //}
            //var simplelinks = document.getElementsByClassName("simplelink");
            //for (let item of simplelinks) {
            //    item.classList.toggle("show");
            //}            
        });
    }
}