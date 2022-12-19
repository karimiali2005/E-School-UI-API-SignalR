function getRooms(page) {
    var StartDay = document.getElementById('StartDay');
    var StartMonth = document.getElementById('StartMonth');
    var StartYear = document.getElementById('StartYear');
    var FinishDay = document.getElementById('FinishDay');
    var FinishMonth = document.getElementById('FinishMonth');
    var FinishYear = document.getElementById('FinishYear');

    var degree = document.getElementById('Degree');
    var grade = document.getElementById('Grade');
    var studyField = document.getElementById('StudyField');
    var roomtype = document.getElementById('RoomType');
    var q = document.getElementById('q');
    var CountRow = document.getElementById('CountRow');

    var data = {};
    data['StartDay'] = StartDay.value;
    data['StartMonth'] = StartMonth.value;
    data['StartYear'] = StartYear.value;
    data['FinishDay'] = FinishDay.value;
    data['FinishMonth'] = FinishMonth.value;
    data['FinishYear'] = FinishYear.value;

    if (!Empty(degree.value)) {
        data['degree'] = degree.value;
    }
    if (!(Empty(grade.value) || grade.parentElement.className.indexOf('displayNone') >= 0)) {
        data['grade'] = grade.value;
    }
    if (!(Empty(studyField.value) || studyField.parentElement.className.indexOf('displayNone') >= 0)) {
        data['studyField'] = studyField.value;
    }
    if (!Empty(roomtype.value)) {
        data['roomtype'] = roomtype.value;
    }
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

    passingData('/Panel/Room', data);
}

function ruser(id) {
    var data = {};
    data['id'] = id;
    data['returnUrl'] = getUrl();
    passingData('/Panel/Room/RoomUser', data, 'POST');    
}

function rchatgroup(id) {
    var data = {};
    data['id'] = id;
    data['returnUrl'] = getUrl();
    passingData('/Panel/Room/RoomChatGroup', data, 'POST');
}
function rusercurrent(id) {
    var data = {};
    data['id'] = id;
    data['returnUrl'] = getUrl();
    passingData('/Panel/Room/RoomUserCurrent', data);
}



function rusered() {
    var data = {};
    var ids = getChChecked(true, true);
    if (ids != null) {
        data['ids'] = JSON.stringify(ids);
    } else {
        return;
    }

    data['id'] = document.getElementById('id').value;
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Room/RoomUsered', data, 'POST');
}
function rchatgroupset() {
    var data = {};
    var ids = getChChecked(true, true);
    if (ids != null) {
        data['ids'] = JSON.stringify(ids);
    } else {
        return;
    }

    data['id'] = document.getElementById('id').value;
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Room/RoomChatGroupSet', data, 'POST');
}

function rdelete(id) {
    var data = {};
    if (Empty(id)) {
        var ids = getChChecked();
        if (ids != null) {
            data['ids'] = JSON.stringify(ids);
        } else {
            return;
        }
    } else {
        var id2 = [id];
        data['ids'] = JSON.stringify(id2);
    }

    data['returnUrl'] = getUrl();
    passingData('/Panel/Room/RoomDelete', data, 'POST');
}


function rdeleted() {
    var lbls = document.getElementsByClassName('lbl');
    var ids = [];
    for (let lbl of lbls) {
        ids.push(lbl.title);
    }

    var data = {};
    data['ids'] = JSON.stringify(ids);
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Room/RoomDeleted', data, 'POST');
}

function newsRoom(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Room/New', data); 
}

function newsRoomed() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveRoom();
    } else {
        showError(errmsg);
    }
}

function updateRoomChatGroup() {
    var form = new FormData();
   
    form.append("returnUrl", getUrl());

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/Room/UpdateRoomChatGroup');

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

function saveRoom() {
    var form = new FormData();

    var inputs = getInputsWithoutId(true);
    for (let input of inputs) {
        form.append(input.id, input.value);        
    }

    

    form.append("returnUrl", document.getElementById('returnUrl').value);
    form.append("id", document.getElementById('id').value);

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/Room/Newed');

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

function rlive(id) {
    data = {};
    data['id'] = id;
    data['returnUrl'] = getUrl();
    passingData('/Panel/Room/Live', data);
    
}

function rlived() {  

    var data = {};    

    data['jitsiaddress'] = document.getElementById('jitsiaddress').value;
    data['jitsipassword'] = document.getElementById('jitsipassword').value;
    data['adobeaddress'] = document.getElementById('adobeaddress').value;
    data['adobeusername'] = document.getElementById('adobeusername').value;
    data['adobepassword'] = document.getElementById('adobepassword').value;
    data['examaddress'] = document.getElementById('examaddress').value;
    data['examaddress2'] = document.getElementById('examaddress2').value;
    data['zoomaddress'] = document.getElementById('zoomaddress').value;


    data['id'] = document.getElementById('id').value;
    data['returnUrl'] = document.getElementById('returnUrl').value;

    passingData('/Panel/Room/Live', data, 'POST');
}

function reportcard(roomId,userId) {
    data = {};
    data['roomId'] = roomId;
    data['userId'] = userId;
    data['returnUrl'] = getUrl();
    passingData('/Panel/Room/ReportCard', data);
}

function reportcardSave() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveReportcard();
    } else {
        showError(errmsg);
    }
}

function saveReportcard() {
    var form = new FormData();

    var inputs = getInputsWithoutId(true);
    for (let input of inputs) {
        form.append(input.id, input.value);
    }

    form.append("file", file);
    form.append("roomId", document.getElementById('RoomId').value);
    form.append("userId", document.getElementById('UserId').value);
    form.append("returnUrl", document.getElementById('returnUrl').value);

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/Room/ReportCard');

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



