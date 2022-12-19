function getRoomDetails(page) {
    var StartDay = document.getElementById('StartDay');
    var StartMonth = document.getElementById('StartMonth');
    var StartYear = document.getElementById('StartYear');
    var FinishDay = document.getElementById('FinishDay');
    var FinishMonth = document.getElementById('FinishMonth');
    var FinishYear = document.getElementById('FinishYear');
    var Week = document.getElementById('Week');
    
    var q = document.getElementById('q');
    var CountRow = document.getElementById('CountRow');

    var data = {};
    data['StartDay'] = StartDay.value;
    data['StartMonth'] = StartMonth.value;
    data['StartYear'] = StartYear.value;
    data['FinishDay'] = FinishDay.value;
    data['FinishMonth'] = FinishMonth.value;
    data['FinishYear'] = FinishYear.value;
    data['Week'] = Week.value;

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

    data['id'] = document.getElementById('id').value;
    passingData('/Panel/RoomDetail', data);
}

function rdshow(id, day, sh, sm, fh, fm) {
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
    data['returnUrl'] = getUrl();
    passingData('/Panel/RoomDetail/RoomDetailShow', data);
}

function rddelete(id) {
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
    passingData('/Panel/RoomDetail/RoomDetailDelete', data, 'POST');
}

function rddelete2(id, day, sh, sm, fh, fm ) {
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
    data['returnUrl'] = getUrl();
    passingData('/Panel/RoomDetail/RoomDetailDelete2', data, 'POST');
}


function rddeleted() {
    var lbls = document.getElementsByClassName('lbl');
    var ids = [];
    for (let lbl of lbls) {
        ids.push(lbl.title);
    }

    var data = {};
    data['ids'] = JSON.stringify(ids);
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/RoomDetail/RoomDetailDeleted', data, 'POST');
}

function newsRoomDetail(week, id, day, sh, sm, fh, fm) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    if (!Empty(day)) {
        data['day'] = day;
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
    data['roomid'] = document.getElementById('id').value;
    data['week'] = week;
    data['returnUrl'] = getUrl();
    passingData('/Panel/RoomDetail/New', data);
}

function newsRoomDetailed() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveRoomDetail();
    } else {
        showError(errmsg);
    }
}

function saveRoomDetail() {
    var form = new FormData();

    var inputs = getInputsWithoutId(true);
    for (let input of inputs) {
        form.append(input.id, input.value);
    }

    form.append("returnUrl", document.getElementById('returnUrl').value);
    form.append("id", document.getElementById('id').value);
    form.append("roomid", document.getElementById('roomid').value);

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/RoomDetail/Newed');

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

function AddCToWeek() {
    var gridWeek = document.getElementById('gridWeek');
    var StartHour = document.getElementById('StartHour').value;
    var StartMinute = document.getElementById('StartMinute').value;
    var FinishHour = document.getElementById('FinishHour').value;
    var FinishMinute = document.getElementById('FinishMinute').value;

    var Start = StartHour + ':' + StartMinute;
    var Finish = FinishHour + ':' + FinishMinute;
    var th_text = Start + ' تا ' + Finish;

    if (Start >= Finish) {
        alert('ساعت شروع نباید بعد از ساعت پایان باشد');
        return;
    }

    var trs = gridWeek.querySelectorAll('tr');
    var ths = trs[0].querySelectorAll('th');
    for (var i = 0; i < ths.length; i++) {
        if (ths[i].innerHTML == th_text) {
            alert('این ساعت تکراری می باشد');
            return;
        }
    }

    var thNew = '<th>' + th_text + '</th>';
    trs[0].innerHTML += thNew;
    var len = trs.length;
    for (var i = 1; i < len; i++) {
        var tr = trs[i];
        var day = tr.querySelector('td:first-child').innerHTML;
        var tdNew = "<td><a class=\"operate\" href=\"#\"><img class=\"icon\" src=\"/imagepanel/icons/schedule1.png\" onclick=\"newsRoomDetail(1, 0, '[0]', '[1]', '[2]', '[3]', '[4]')\" /></a></td>";
        tdNew = tdNew.replace("[0]", day);
        tdNew = tdNew.replace("[1]", StartHour);
        tdNew = tdNew.replace("[2]", StartMinute);
        tdNew = tdNew.replace("[3]", FinishHour);
        tdNew = tdNew.replace("[4]", FinishMinute);
        tr.innerHTML += tdNew;
    }
}