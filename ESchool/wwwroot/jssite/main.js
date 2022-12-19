function Empty(val) {
    return val == '' || val == undefined || val == null;
}

function Next(e, n) {
    if (e.keyCode == 13) {
        if (!Empty(document.getElementById(n))) {
            document.getElementById(n).focus();
        }
    }
}

function submitForm() {
    var form = document.getElementsByClassName('form')[0];
    if (!Empty(form)) {
        form.submit();
    }
}

function toggleClass(elem, c = 'displayNone') {
    if (elem.classList.contains(c)) {
        elem.classList.remove(c);
    } else {
        elem.classList.add(c);
    }
}

function addClass(elem, c = 'displayNone') {
    elem.classList.add(c);
}

function removeClass(elem, c = 'displayNone') {
    elem.classList.remove(c);
}

function setError(elem) {
    if (elem.className.indexOf('colorErrorBorder') == -1) {
        elem.classList.toggle("colorErrorBorder");
    }
}

function showLoader() {
    var backLdsRoller = document.getElementById('backLdsRoller');
    backLdsRoller.classList.add('show');
}

function hideLoader() {
    var backLdsRoller = document.getElementById('backLdsRoller');
    backLdsRoller.classList.remove('show');
}

function showButtonCloseLoader() {
    var btnLoadingClose = document.getElementById('btnLoadingClose');
    btnLoadingClose.classList.remove('displayNone');
    console.log(btnLoadingClose);
    return btnLoadingClose;
}

function showError(msg) {
    var diverrors = document.getElementById('diverrors');
    if (diverrors.className.indexOf('displayBlock') == -1) {
        diverrors.classList.toggle('displayBlock');
        var errormsg = document.getElementById('errormsg');
        errormsg.innerHTML = msg;
        window.location.hash = '';
        window.location.hash = '#diverrors';
    }
    
}

function hideError() {
    var diverrors = document.getElementById('diverrors');
    if (diverrors.className.indexOf('displayBlock') >= 0) {
        diverrors.classList.toggle('displayBlock');
    }
    var errormsg = document.getElementById('errormsg');
    errormsg.innerHTML = '';
    
}

function getSubr(t) {
    var text = t.replace(/^(.{20}[^\s]*).*/, "$1");
    return text;
}

function checkIrNationalCode(m) {
    m = p2e(m);
    m = a2e(m);
    var p = "^[0-9]{10}";
    m = convertNumbers2English(m);
    var pattern = new RegExp(p);
    var result = pattern.test(m);
    return result;
}

function checkPersianYear(m) {
    m = p2e(m);
    m = a2e(m);
    var p = "^[1]{1}[2-4]{1}[0-9]{2}";
    m = convertNumbers2English(m);
    var pattern = new RegExp(p);
    var result = pattern.test(m);
    return result;
}

function checkPhone(m) {
    m = p2e(m);
    m = a2e(m);
    var p = "^[1-9]{1}[0-9]{7}";
    m = convertNumbers2English(m);
    var pattern = new RegExp(p);
    var result = pattern.test(m);
    return result;
}

function checkMobile(m) {
    m = p2e(m);
    m = a2e(m);
    var p = "^09[0-9]{9}";
    m = convertNumbers2English(m);
    var pattern = new RegExp(p);
    var result = pattern.test(m);    
    return result;
}

function checkDigits(m) {
    m = p2e(m);
    m = a2e(m);
    var p = "^[0-9]{1,}";
    m = convertNumbers2English(m);
    var pattern = new RegExp(p);
    var result = pattern.test(m);
    return result;
}

const e2p = s => s.replace(/\d/g, d => '۰۱۲۳۴۵۶۷۸۹'[d]);
const e2a = s => s.replace(/\d/g, d => '٠١٢٣٤٥٦٧٨٩'[d]);

const p2e = s => s.replace(/[۰-۹]/g, d => '۰۱۲۳۴۵۶۷۸۹'.indexOf(d));
const a2e = s => s.replace(/[٠-٩]/g, d => '٠١٢٣٤٥٦٧٨٩'.indexOf(d));

const p2a = s => s.replace(/[۰-۹]/g, d => '٠١٢٣٤٥٦٧٨٩'['۰۱۲۳۴۵۶۷۸۹'.indexOf(d)]);
const a2p = s => s.replace(/[٠-٩]/g, d => '۰۱۲۳۴۵۶۷۸۹'['٠١٢٣٤٥٦٧٨٩'.indexOf(d)]);

function parseArabic(str) {
    return Number(str.replace(/[٠١٢٣٤٥٦٧٨٩]/g, function (d) {
        return d.charCodeAt(0) - 1632; // Convert Arabic numbers
    }).replace(/[۰۱۲۳۴۵۶۷۸۹]/g, function (d) {
        return d.charCodeAt(0) - 1776; // Convert Persian numbers
    }));
}


function convertNumbers2English(numbers) {
    return numbers.replace(/[\u0660-\u0669]/g, function (c) {
        return c.charCodeAt(0) - 0x0660;
    }).replace(/[\u06f0-\u06f9]/g, function (c) {
        return c.charCodeAt(0) - 0x06f0;
    });
}

function IsJsonString(str) {
    try {
        JSON.parse(str);
    }
    catch (e) {
        return false;
    }
    return true;
}


function getUrl() {
    var url = window.location.pathname + window.location.search;
    /*alert(url);*/
    var i = url.indexOf('?msg=');
    var i2 = url.indexOf('&msg=');
    if (i != -1) {
        url = url.substring(0, i);
    }
    if (i2 != -1) {
        url = url.substring(0, i2);
    }
    /*alert(url);*/
    return url;
}

function cancelCh() {
    var returnUrl = document.getElementById('returnUrl').value
    window.location.href = returnUrl;
}

function passingData(url, data, method = 'GET', fr = '', target = '_top') {
    var f = document.createElement('form');
    f.action = url;
    f.method = method;
    f.target = target;
    if (fr == 'js') {
        f.enctype = 'application/json';
    }

    for (var key in data) {
        var i = document.createElement('input');
        i.type = 'hidden';
        i.name = key;
        i.value = data[key];
        f.appendChild(i);
    }

    console.log(f);

    document.body.appendChild(f);
    f.submit();
}

document.addEventListener("DOMContentLoaded", function () {
    setDefaults();
});

function setDefaults() {

    var tags = document.querySelectorAll(".chPreviousColor");
    for (let tag of tags) {
        tag.addEventListener("focus", function () {
            tag.previousElementSibling.classList.toggle("colorPrimary");
            tag.classList.toggle("colorBorderPrimary");
            if (tag.className.indexOf('colorErrorBorder') >= 0) {
                tag.classList.toggle("colorErrorBorder");
            }
        });
        tag.addEventListener("focusout", function () {
            tag.previousElementSibling.classList.toggle("colorPrimary");
            tag.classList.toggle("colorBorderPrimary");
        });
    }

    var inputs = document.querySelectorAll('.row input[type=text]');
    var len = inputs.length;
    for (var i = 0; i < len; i++) {
        var input = inputs[i];
        var inputNext = inputs[i + 1];
        if (!Empty(inputNext)) {
            input.setAttribute("nextid", inputNext.id);
            input.addEventListener("keydown", function (event) {
                Next(event, event.target.getAttribute('nextid'));
            });
        }
    }

}

function replaceForSearch(text) {
    text = text.toString();
    text = text.replace(' ', '');
    //text = text.replace('ک', '');
    text = text.replace('ی', 'ي');
    return text;
}

function getTime() {
    var date = new Date();
    var hourOffset = 3;
    date.setUTCHours(date.getUTCHours(), date.getUTCMinutes());
    var time = date.getTime();
    date.setUTCFullYear(date.getUTCFullYear(), 2, 22);
    var dstStart = date.getTime();
    date.setUTCFullYear(date.getUTCFullYear(), 8, 22);
    var dstEnd = date.getTime();
    if (time > dstStart && time < dstEnd) hourOffset = 4;
    date.setUTCHours(date.getUTCHours() + hourOffset, date.getUTCMinutes() + 30);
    var output = date.getUTCHours() + ":" + date.getUTCMinutes() + ":" + date.getUTCSeconds();
    return output;
}


window.addEventListener("resize", function () {
    chMenu();
});

function refreshPage() {
    window.location.reload();
}

function getWidthPage() {
    var width = (window.innerWidth > 0) ? window.innerWidth : screen.width;
    return width;
}

function chMenu() {
    var buttonMenu = document.getElementById("buttonMenu");
    if (!Empty(buttonMenu)) {
        var className = 'changestatusbuttonmenu';
        if (buttonMenu.className.indexOf(className) >= 0) {
            buttonMenu.classList.toggle(className);
            var ul = buttonMenu.nextElementSibling;
            ul.className = ul.className.replace('showMenu', '');
        }
    }
    
}

function openMenu() {
    var buttonMenu = document.getElementById("buttonMenu");
    var className = 'changestatusbuttonmenu';
    buttonMenu.classList.toggle(className);
    var ul = buttonMenu.nextElementSibling;
    ul.classList.toggle('showMenu');
}

function alertmessage(text, button_text, func) {
    var alertmessage = document.getElementById("alertmessage");
    var boxmessage = document.getElementById("boxmessage");
    var msg = document.getElementById("msg");
    var btnClose = document.getElementById("btnAlertMsgClose");
    btnClose.value = button_text;
    btnClose.onclick = function () { alertmessage.className = alertmessage.className.replace("show", ""); func(); };
    msg.innerHTML = text;
    alertmessage.className = "show";
    boxmessage.className = "show";
}