function newsCeremony() {
    var data = {};

    data['returnUrl'] = getUrl();
    passingData('/Panel/Ceremony/New', data);
}

function newdedCeremony() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveCeremony();
    } else {
        showError(errmsg);
    }
}

function saveCeremony() {
    var form = new FormData();

    var inputs = getInputsWithoutId(true);
    for (let input of inputs) {
        form.append(input.id, input.value);
    }

   
    form.append("returnUrl", document.getElementById('returnUrl').value);

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/Ceremony/Newed');

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

function ceremonydelete(id) {
    var data = {};
    if (Empty(id)) {

        return;

    } else {

        data['id'] = id;
    }

    data['returnUrl'] = getUrl();
    passingData('/Panel/Ceremony/Delete', data, 'GET');
}
function ceremonydeleted(id) {
    var data = {};
    if (Empty(id)) {

        return;

    } else {

        data['id'] = id;
    }

    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Ceremony/Deleted', data, 'POST');
}


