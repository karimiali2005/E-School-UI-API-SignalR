function openlive_panel(type, jitsiAddress, jitsiPassword, adobeAddress, adobeUsername, adobePassword, examAddress, zoomAddress) {
    if (type == 'jitsi') {
        /*var JitsiAddress = document.getElementById('JitsiAddress').value;
        var JitsiPassword = document.getElementById('JitsiPassword').value;*/
        if (!Empty(jitsiPassword)) {
            alertmessage('کلمه عبور: ' + jitsiPassword, 'شروع', function () {
                window.open(jitsiAddress);
            });
        } else {
            window.open(jitsiAddress);
        }
    } else if (type == 'adobe') {
        /*var AdobeAddress = document.getElementById('AdobeAddress').value;
        var AdobeUsername = document.getElementById('AdobeUsername').value;
        var AdobePassword = document.getElementById('AdobePassword').value;*/
        if (!Empty(adobePassword)) {
            var text = 'نام کاربری: ' + adobeUsername + '<br/>';
            text += 'کلمه عبور: ' + adobePassword;
            alertmessage(text, 'شروع', function () {
                window.open(adobeAddress);
            });
        } else {
            window.open(adobeAddress);
        }
    } else if (type == 'exam') {
        /*var ExamAddress = document.getElementById('ExamAddress').value;*/
        window.open(examAddress);
    }
    else if (type == 'zoom') {
        /*var ExamAddress = document.getElementById('ExamAddress').value;*/
        window.open(zoomAddress);
    }
}