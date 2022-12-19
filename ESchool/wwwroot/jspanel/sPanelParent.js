function getParents(page) {    
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

    passingData('/Panel/Parent', data);
}


function newsParent(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Parent/New', data);    
}

function newdedParent() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveParent();
    } else {
        showError(errmsg);
    }
}

function saveParent() {
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
    request.open('post', '/Panel/Parent/Newed');

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

