function extrainfo() {

    hideError();
    errmsg = '';

    var Nationality = document.getElementById('Nationality');
    var Insurance = document.getElementById('Insurance');
    var FamilyNumber = document.getElementById('FamilyNumber');
    var SeveralChildren = document.getElementById('SeveralChildren');
    var RightHanded = document.getElementById('RightHanded');
    var PersianLanguage = document.getElementById('PersianLanguage');
    var IsRelativesParents = document.getElementById('IsRelativesParents');
    var IsStudentInsurance = document.getElementById('IsStudentInsurance');
    var PreschoolEducation = document.getElementById('PreschoolEducation');
    var IsCityPlace = document.getElementById('IsCityPlace');
    var ReferralReasonNew = document.getElementById('ReferralReasonNew');    

    if (Empty(Insurance.value)) {
        errmsg += ' فیلد ' + Insurance.previousElementSibling.innerHTML + ' اجباری می باشد ' + '<br />';
        setError(Insurance);
    }

    if (!checkDigits(FamilyNumber.value)) {
        errmsg += ' فیلد ' + FamilyNumber.previousElementSibling.innerHTML + ' اجباری می باشد ' + '<br />';
        setError(FamilyNumber);
    }

    if (!checkDigits(SeveralChildren.value)) {
        errmsg += ' فیلد ' + SeveralChildren.previousElementSibling.innerHTML + ' اجباری می باشد ' + '<br />';
        setError(SeveralChildren);
    }

    if (Empty(errmsg)) {
        submitForm();
    } else {
        showError(errmsg);
    }

}

function profile() {

    hideError();
    var errmsg = getErrors('');

    if (Empty(errmsg)) {
        saveProfile();
    } else {
        showError(errmsg);
    }
}

function saveProfile() {
    var form = new FormData();

    var inputs = getInputsWithoutId(true);
    for (let input of inputs) {
        form.append(input.id, input.value);
    }

    form.append("file", file);
    form.append("UserId", document.getElementById('UserId').value);    

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Account/Profile');

    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            hideError();
            var result = this.responseText;
            if (result.startsWith('ok,')) {                
                window.location.href = result.replace("ok,", "");
            } else {
                alert(this.responseText);
                hideLoader();
            }
        }
    };
    request.onerror = function (err) {
        hideLoader();
        alert(err.message);
    };
    
    request.send(form);

}


function sessionshow(id, day, sh, sm, fh, fm) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    if (!Empty(day)) {
        data['Day'] = day;
    }
    if (!Empty(sh)) {
        data['StartHour'] = sh;
    }
    if (!Empty(sm)) {
        data['StartMinute'] = sm;
    }
    if (!Empty(fh)) {
        data['FinishHour'] = fh;
    }
    if (!Empty(fm)) {
        data['FinishMinute'] = fm;
    }
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Class/SessionShow', data, 'POST');
}

function lessonshow(id, lesson) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    if (!Empty(lesson)) {
        data['Lesson'] = lesson;
    }    
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Class/LessonShow', data, 'POST');
}

function usershow(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }    
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Class/UserShow', data, 'POST');
}

function classedit(id, roomid) {
    var data = {};
    data['id'] = id;
    data['roomid'] = roomid;    
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Class/Edit', data);
}

function classedited() {
    var data = {};    
    data['id'] = document.getElementById('id').value;;
    data['roomid'] = document.getElementById('roomid').value;;
    data['Comment'] = document.getElementById('comment').value;;
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Class/Edited', data);
}

function week(step, id = '') {
    data = {};
    var url = '/Class/Teacher';
    data['step'] = step;
    if (!Empty(id)) {
        data['id'] = id;
        url = '/Class/Week?id=' + id + '&step=';
    }    
    passingData(url, data);

}

function chatroom(id = 0, tid = 0, rdid = 0, cid = 0) {
    var data = {};
    data['id'] = id;   
    data['teacherid'] = tid;   
    data['rdid'] = rdid;   
    data['courseid'] = cid;   
    var returnUrl = document.getElementById('returnUrl');
    data['returnUrl'] = Empty(returnUrl) ? "" : returnUrl.value;
    passingData('/Chat/Room', data, 'POST');
}