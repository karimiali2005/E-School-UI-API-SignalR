function getTeachers(page) {    
    var q = document.getElementById('q');

    var data = {};

    if (!Empty(q.value)) {
        data['q'] = q.value;
    }
    data['CountRow'] = 10;
    if (!Empty(CountRow)) {
        data['CountRow'] = checkDigits(CountRow.value) ? CountRow.value : 10
    }
    if (!Empty(page)) {
        if (page > 1) {
            data['pagenumber'] = page;
        }
    }

    passingData('/Panel/Teacher', data);
}


function newsTeacher(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Teacher/New', data);    
}

function newdedTeacher() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveTeacher();
    } else {
        showError(errmsg);
    }
}

function saveTeacher() {
    var form = new FormData();

    var inputs = getInputsWithoutId(true);
    for (let input of inputs) {
        form.append(input.id, input.value);
    }

    form.append("file", file);
    form.append("UserId", document.getElementById('UserId').value);
    form.append("returnUrl", document.getElementById('returnUrl').value);

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/Teacher/Newed');

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
    console.log(form);
    request.send(form);

}

function tcourse() {
    var data = {};
    data['id'] = getChChecked(false);
    if (data['id'] != null) {
        data['returnUrl'] = getUrl();
        passingData('/Panel/Teacher/TeacherCourse', data);
    }
}

function tcoursed() {
    var degree = document.getElementById('Degree');
    var grade = document.getElementById('Grade');
    var studyField = document.getElementById('StudyField');    
    var course = document.getElementById('Course');    

    var data = {};

    if (!Empty(degree.value)) {
        data['degree'] = degree.value;
    }
    if (!(Empty(grade.value) || grade.parentElement.className.indexOf('displayNone') >= 0)) {
        data['grade'] = grade.value;
    }
    if (!(Empty(studyField.value) || studyField.parentElement.className.indexOf('displayNone') >= 0)) {
        data['studyField'] = studyField.value;
    }
    if (!Empty(course.value)) {
        data['course'] = course.value;
    }  

    data['id'] = document.getElementById('id').value;
    data['returnUrl'] = document.getElementById('returnUrl').value;

    passingData('/Panel/Teacher/TeacherCourse', data, 'POST');
}

function tcoursedelete(id) {
    var data = {};
    var ids = getChChecked(true);
    if (ids != null) {
        data['ids'] = JSON.stringify(ids);
        data['returnUrl'] = getUrl();        
        passingData('/Panel/Teacher/TeacherCourseDelete', data, 'POST');
    }
}

function tcoursedeleted() {
    var lbls = document.getElementsByClassName('lbl');
    var ids = [];
    for (let lbl of lbls) {
        if (!Empty(lbl.title)) {
            ids.push(lbl.title);
        }
    }

    var data = {};
    data['ids'] = JSON.stringify(ids);
    data['returnUrl'] = document.getElementById('returnUrl').value;    
    passingData('/Panel/Teacher/TeacherCourseDeleted', data, 'POST');    
}


function rteacher() {
    var data = {};
    data['id'] = getChChecked(false);
    if (data['id'] != null) {
        data['returnUrl'] = getUrl();
        passingData('/Panel/Teacher/RoomTeacher', data);
    }
}

function rteachered() {    
    var course = document.getElementById('Course');
    var room = document.getElementById('Room');

    var data = {};
    
    if (!Empty(course.value)) {
        data['course'] = course.value;
    }
    if (!Empty(room.value)) {
        data['room'] = room.value;
    }

    data['jitsiaddress'] = document.getElementById('jitsiaddress').value;
    data['jitsipassword'] = document.getElementById('jitsipassword').value;
    data['adobeaddress'] = document.getElementById('adobeaddress').value;
    data['adobeusername'] = document.getElementById('adobeusername').value;
    data['adobepassword'] = document.getElementById('adobepassword').value;

    data['id'] = document.getElementById('id').value;
    data['returnUrl'] = document.getElementById('returnUrl').value;

    passingData('/Panel/Teacher/RoomTeacher', data, 'POST');
}

function rteacherdelete() {
    var data = {};
    var ids = getChChecked(true);
    if (ids != null) {
        data['ids'] = JSON.stringify(ids);
        data['returnUrl'] = getUrl();
        passingData('/Panel/Teacher/RoomTeacherDelete', data, 'POST');
    }
}

function rteacheredeleted() {
    var lbls = document.getElementsByClassName('lbl');
    var ids = [];
    for (let lbl of lbls) {
        if (!Empty(lbl.title)) {
            ids.push(lbl.title);
        }
    }

    var data = {};
    data['ids'] = JSON.stringify(ids);
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Teacher/RoomTeacherDeleted', data, 'POST');
}

function tpersonnel() {
    var data = {};
    data['id'] = getChChecked(false);
    if (data['id'] != null) {
        data['returnUrl'] = getUrl();
        passingData('/Panel/Teacher/TeacherPersonnel', data);
    }
}
function tpersonneled(id, ispersonnel) {
    var data = {};
    data['id'] = id;
    data['ispersonnel'] = ispersonnel;
   
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Teacher/TeacherPersonneled', data, 'POST');
}


