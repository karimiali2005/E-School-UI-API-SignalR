function getReportCards(page) {
    
    var q = document.getElementById('q');
    var CountRow = document.getElementById('CountRow');

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

    passingData('/Panel/ReportCard', data);
}
function newsReportCard() {
    var data = {};

    data['returnUrl'] = getUrl();
    passingData('/Panel/ReportCard/New', data);
}

function newdedReportCard() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveReportCard();
    } else {
        showError(errmsg);
    }
}

function saveReportCard() {
    var form = new FormData();

    var inputs = getInputsWithoutId(true);
    for (let input of inputs) {
        form.append(input.id, input.value);
    }

   
    form.append("returnUrl", document.getElementById('returnUrl').value);

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/ReportCard/Newed');

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

function editReportCard(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/ReportCard/Edit', data);
}

function editedReportCard(id) {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveEditReportCard(id);
    } else {
        showError(errmsg);
    }
}

function saveEditReportCard(id) {
    var form = new FormData();

    var inputs = getInputsWithoutId(true);
    for (let input of inputs) {
        form.append(input.id, input.value);
    }

    form.append("ReportCardId", id);
    form.append("returnUrl", document.getElementById('returnUrl').value);

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/ReportCard/Edited');

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

function rTeacherCourse(id,gradeId) {
    var data = {};
    data['id'] = id;
    data['gradeId'] = gradeId;
    if (data['id'] != null) {
        data['returnUrl'] = getUrl();
        passingData('/Panel/ReportCard/ReportCardTeacherCourse', data);
    }
}

function rTeacherCoursed() {
    var roomChatGroup = document.getElementById('RoomChatGroupId');
    var course = document.getElementById('CourseId');
   

    var data = {};

    if (!Empty(roomChatGroup.value)) {
        data['roomChatGroupId'] = roomChatGroup.value;
    }
   
    if (!Empty(course.value)) {
        data['courseId'] = course.value;
    }

    data['id'] = document.getElementById('id').value;
    data['returnUrl'] = document.getElementById('returnUrl').value;

    passingData('/Panel/ReportCard/ReportCardTeacherCourse', data, 'POST');
}

function getReportCard(page) {
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

    passingData('/Panel/ReportCard', data);
}

function reportCardDetaildelete(id) {
    var data = {};
    var ids = getChChecked(true);
    if (ids != null) {
        data['ids'] = JSON.stringify(ids);
        data['reportCardId'] = id;
        data['returnUrl'] = getUrl();
        passingData('/Panel/ReportCard/ReportCardDetailDelete', data, 'POST');
    }
}
function reportCardDetaildeleted() {
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
    passingData('/Panel/ReportCard/ReportCardDetailDeleted', data, 'POST');
}