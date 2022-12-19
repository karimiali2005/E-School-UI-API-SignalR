function newsIntroduction() {
    var data = {};

    data['returnUrl'] = getUrl();
    passingData('/Panel/Introduction/New', data);
}

function newdedIntroduction() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveIntroduction();
    } else {
        showError(errmsg);
    }
}

function saveIntroduction() {
    showLoader();
    var input = document.getElementById('filePic');
    var data = new FormData();

    if (input !== null) {
        var file = input.files[0];


        if (file == 'undefined' && file == null) {
            hideLoader();

            input.value = '';
            return;
        }
        data.append('filePic', input.files[0]);
    }

    data.append('IntroductionTitle', document.getElementById("IntroductionTitle").value);

    data.append('IntroductionPosition', document.getElementById("IntroductionPosition").value);
    data.append('IntroductionTypeId', document.getElementById("IntroductionTypeId").value);

    data.append('returnUrl', document.getElementById('returnUrl').value);









    xhttp = new XMLHttpRequest();

    xhttp.upload.addEventListener('progress', function (e) {
        var percent_complete = (e.loaded / e.total) * 100;
        var txtLoading = document.getElementById('txtLoading');
        txtLoading.innerHTML = parseInt(percent_complete) + "%" + ' ارسال شده است';
    });

    xhttp.onreadystatechange = function () {


        if (this.readyState == 4 && this.status == 200) {
            if (!Empty(xhttp.responseText)) {
                hideLoader();
                cancelCh();
                /*alert("salam");*/

            }
            // HideLoader();
            //input.value = '';

        }
    };
    xhttp.onerror = function (err) {
        hideLoader();
        alert(err.message);
        // input.value = '';

    };
    xhttp.open('POST', '/Panel/Introduction/New');
    xhttp.send(data);
}
function editIntroduction(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Introduction/Edit', data);
}
function editedIntroduction(id) {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveEditIntroduction(id);
    } else {
        showError(errmsg);
    }
}

function saveEditIntroduction(id) {
    showLoader();
    var input = document.getElementById('filePic');
    var data = new FormData();

    if (input !== null) {
        var file = input.files[0];


        if (file == 'undefined' && file == null) {
            hideLoader();

            input.value = '';
            return;
        }
        data.append('filePic', input.files[0]);
    }
    data.append('id', id);

    data.append('IntroductionTitle', document.getElementById("IntroductionTitle").value);

    data.append('IntroductionPosition', document.getElementById("IntroductionPosition").value);
    data.append('IntroductionTypeId', document.getElementById("IntroductionTypeId").value);

    data.append('returnUrl', document.getElementById('returnUrl').value);









    xhttp = new XMLHttpRequest();

    xhttp.upload.addEventListener('progress', function (e) {
        var percent_complete = (e.loaded / e.total) * 100;
        var txtLoading = document.getElementById('txtLoading');
        txtLoading.innerHTML = parseInt(percent_complete) + "%" + ' ارسال شده است';
    });

    xhttp.onreadystatechange = function () {


        if (this.readyState == 4 && this.status == 200) {
            if (!Empty(xhttp.responseText)) {
                hideLoader();
                cancelCh();
                /*alert("salam");*/

            }
            // HideLoader();
            //input.value = '';

        }
    };
    xhttp.onerror = function (err) {
        hideLoader();
        alert(err.message);
        // input.value = '';

    };
    xhttp.open('POST', '/Panel/Introduction/Edit');
    xhttp.send(data);
}
function introductiondelete(id) {
    var data = {};
    if (Empty(id)) {

        return;

    } else {

        data['id'] = id;
    }

    data['returnUrl'] = getUrl();
    passingData('/Panel/Introduction/Delete', data, 'GET');
}


function introductiondeleted(id) {
    var data = {};
    if (Empty(id)) {

        return;

    } else {

        data['id'] = id;
    }

    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Introduction/Deleted', data, 'POST');
}


