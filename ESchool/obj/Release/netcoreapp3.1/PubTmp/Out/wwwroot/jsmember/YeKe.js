// <![CDATA[
function substituteCharInFireFox(charCode, e) {
    var keyEvt = document.createEvent("KeyboardEvent");
    keyEvt.initKeyEvent("keypress", true, true, null, false, false, false, false, 0, charCode);
    e.target.dispatchEvent(keyEvt);
    e.preventDefault();
}

function substituteCharInChrome(charCode, e) {
    //it does not work yet! /*$.browser.webkit*/
    //https://bugs.webkit.org/show_bug.cgi?id=16735
    var keyEvt = document.createEvent("KeyboardEvent");
    keyEvt.initKeyboardEvent("keypress", true, true, null, false, false, false, false, 0, charCode);
    e.target.dispatchEvent(keyEvt);
    e.preventDefault();
}

function insertAtCaret(myValue, e) {
    var obj = e.target;
    var startPos = obj.selectionStart;
    var endPos = obj.selectionEnd;
    var scrollTop = obj.scrollTop;
    obj.value = obj.value.substring(0, startPos) + myValue + obj.value.substring(endPos, obj.value.length);
    obj.focus();
    obj.selectionStart = startPos + myValue.length;
    obj.selectionEnd = startPos + myValue.length;
    obj.scrollTop = scrollTop;
   // var code = obj.value.charCodeAt(0);
   //alert(code);
    e.preventDefault();
}

$(document).ready(function () {
    $(document).keypress(function (e) {
        var keyCode = e.keyCode ? e.keyCode : e.which;


        var arabicYeCharCode = 1610;
        var persianYeCharCode = 1740;
        var arabicKeCharCode = 1603;
        var persianKeCharCode = 1705;


        var persianZeroCharCode1 = 1776;
        var persianZeroCharCode2 = 1632;
        var englishZeroCharCode = 48;
        var persianOneCharCode1 = 1777;
        var persianOneCharCode2 = 1633;
        var englishOneCharCode = 49;
        var persianTwoCharCode1 = 1778;
        var persianTwoCharCode2 = 1634;
        var englishTwoCharCode = 50;
        var persianThreeCharCode1 = 1779;
        var persianThreeCharCode2 = 1635;
        var englishThreeCharCode = 51;
        var persianFourCharCode1 = 1780;
        var persianFourCharCode2 = 1636;
        var englishFourCharCode = 52;
        var persianFiveCharCode1 = 1781;
        var persianFiveCharCode2 = 1637;
        var englishFiveCharCode = 53;
        var persianSexCharCode1 = 1782;
        var persianSexCharCode2 = 1638;
        var englishSexCharCode = 54;
        var persianSevenCharCode1 = 1783;
        var persianSevenCharCode2 = 1639;
        var englishSevenCharCode = 55;
        var persianEightCharCode1 = 1784;
        var persianEightCharCode2 = 1640;
        var englishEightCharCode = 56;
        var persianNineCharCode1 = 1785;
        var persianNineCharCode2 = 1641;
        var englishNineCharCode = 57;


        if ($.browser.msie) {
           // alert("msi");
            switch (keyCode) {
                case arabicYeCharCode:
                    event.keyCode = persianYeCharCode;
                    break;
                case arabicKeCharCode:
                    event.keyCode = persianKeCharCode;
                    break;
                case persianZeroCharCode1:
                case persianZeroCharCode2:
                    event.keyCode = englishZeroCharCode;
                    break;
                case persianOneCharCode1:
                case persianOneCharCode2:
                    event.keyCode = englishOneCharCode;
                    break;
                case persianTwoCharCode1:
                case persianTwoCharCode2:
                    event.keyCode = englishTwoCharCode;
                    break;
                case persianThreeCharCode1:
                case persianThreeCharCode2:
                    event.keyCode = englishThreeCharCode;
                    break;
                case persianFourCharCode1:
                case persianFourCharCode2:
                    event.keyCode = englishFourCharCode;
                    break;
                case persianFiveCharCode1:
                case persianFiveCharCode2:
                    event.keyCode = englishFiveCharCode;
                    break;
                case persianSexCharCode1:
                case persianSexCharCode2:
                    event.keyCode = englishSexCharCode;
                    break;
                case persianSevenCharCode1:
                case persianSevenCharCode2:
                    event.keyCode = englishSevenCharCode;
                    break;
                case persianEightCharCode1:
                case persianEightCharCode2:
                    event.keyCode = englishEightCharCode;
                    break;
                case persianNineCharCode1:
                case persianNineCharCode2:
                    event.keyCode = englishNineCharCode;
                    break;

            }
           
        }
        else if ($.browser.firefox) {
           // alert("fire");
            switch (keyCode) {
                case arabicYeCharCode:
                    substituteCharInFireFox(persianYeCharCode, e);
                    break;
                case arabicKeCharCode:
                    substituteCharInFireFox(persianKeCharCode, e);
                    break;
                case persianZeroCharCode1:
                case persianZeroCharCode2:
                    substituteCharInFireFox(englishZeroCharCode, e);
                    break;
                case persianOneCharCode1:
                case persianOneCharCode2:
                    substituteCharInFireFox(englishOneCharCode, e);
                    break;
                case persianTwoCharCode1:
                case persianTwoCharCode2:
                    substituteCharInFireFox(englishTwoCharCode, e);
                    break;
                case persianThreeCharCode1:
                case persianThreeCharCode2:
                    substituteCharInFireFox(englishThreeCharCode, e);
                    break;
                case persianFourCharCode1:
                case persianFourCharCode2:
                    substituteCharInFireFox(englishFourCharCode, e);
                    break;
                case persianFiveCharCode1:
                case persianFiveCharCode2:
                    substituteCharInFireFox(englishFiveCharCode, e);
                    break;
                case persianSexCharCode1:
                case persianSexCharCode2:
                    substituteCharInFireFox(englishSexCharCode, e);
                    break;
                case persianSevenCharCode1:
                case persianSevenCharCode2:
                    substituteCharInFireFox(englishSevenCharCode, e);
                    break;
                case persianEightCharCode1:
                case persianEightCharCode2:
                    substituteCharInFireFox(englishEightCharCode, e);
                    break;
                case persianNineCharCode1:
                case persianNineCharCode2:
                    substituteCharInFireFox(englishNineCharCode, e);
                    break;
            }
        }
        else {
          //  alert(event.keyCode);
            switch (keyCode) {
                case arabicYeCharCode:
                    insertAtCaret(String.fromCharCode(persianYeCharCode), e);
                    break;
                case arabicKeCharCode:
                    insertAtCaret(String.fromCharCode(persianKeCharCode), e);
                    break;
                case persianZeroCharCode1:
                case persianZeroCharCode2:
                    insertAtCaret(String.fromCharCode(englishZeroCharCode), e);
                    break;
                case persianOneCharCode1:
                case persianOneCharCode2:
                    insertAtCaret(String.fromCharCode(englishOneCharCode), e);
                    break;
                case persianTwoCharCode1:
                case persianTwoCharCode2:
                    insertAtCaret(String.fromCharCode(englishTwoCharCode), e);
                    break;
                case persianThreeCharCode1:
                case persianThreeCharCode2:
                    insertAtCaret(String.fromCharCode(englishThreeCharCode), e);
                    break;
                case persianFourCharCode1:
                case persianFourCharCode2:
                    insertAtCaret(String.fromCharCode(englishFourCharCode), e);
                    break;
                case persianFiveCharCode1:
                case persianFiveCharCode2:
                    insertAtCaret(String.fromCharCode(englishFiveCharCode), e);
                    break;
                case persianSexCharCode1:
                case persianSexCharCode2:
                    insertAtCaret(String.fromCharCode(englishSexCharCode), e);
                    break;
                case persianSevenCharCode1:
                case persianSevenCharCode2:
                    insertAtCaret(String.fromCharCode(englishSevenCharCode), e);
                    break;
                case persianEightCharCode1:
                case persianEightCharCode2:
                    insertAtCaret(String.fromCharCode(englishEightCharCode), e);
                    break;
                case persianNineCharCode1:
                case persianNineCharCode2:
                    insertAtCaret(String.fromCharCode(englishNineCharCode), e);
                    break;
            }
          
        }
       // alert(event.keyCode);
    });
});
// ]]>