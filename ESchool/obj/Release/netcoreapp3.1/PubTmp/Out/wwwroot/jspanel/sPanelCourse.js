function getCourses(page) {    
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

    passingData('/Panel/Course', data);
}


function newsCourse(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Course/New', data);    
}

function newdedCourse() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveCourse();
    } else {
        showError(errmsg);
    }
}

function saveCourse() {
    var form = new FormData();

    var inputs = getInputsWithoutId(true);
    for (let input of inputs) {
        form.append(input.id, input.value);
    }
    
    form.append("CourseId", document.getElementById('CourseId').value);
    form.append("returnUrl", document.getElementById('returnUrl').value);

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/Course/Newed');

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

function coursedelete(id) {
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
    passingData('/Panel/Course/Delete', data, 'POST');
}


function coursedeleted() {
    var lbls = document.getElementsByClassName('lbl');
    var ids = [];
    for (let lbl of lbls) {
        ids.push(lbl.title);
    }

    var data = {};
    data['ids'] = JSON.stringify(ids);
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Course/Deleted', data, 'POST');
}

