function shPwdPanel(id) {
    var x = document.getElementById(id);
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

function loginpanel() {
    hideError();
    var errmsg = getErrorsPanel('login');
    if (!Empty(errmsg)) {
        showError(errmsg);
        return;
    }

    var data = {};
    data['loginUsername'] = document.getElementById('loginUsername').value;
    data['loginPassword'] = document.getElementById('loginPassword').value;
    data['remeberme'] = document.getElementById('remeberme').checked;
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Admin/Login', data, 'POST');
}

function logout() {
    window.location.href = '/Panel/Admin/Logout';
}

function forgetPwd() {
    hideError();
    var errmsg = getErrors('forgetPwd');
    if (!Empty(errmsg)) {
        showError(errmsg);
        return;
    }

    submitForm();
}

function verify() {
    hideError();
    var errmsg = getErrors('verify');
    if (!Empty(errmsg)) {
        showError(errmsg);
        return;
    }

    submitForm();
}

function chpwd() {
    hideError();
    var errmsg = getErrors('chPwd');
    if (!Empty(errmsg)) {
        showError(errmsg);
        return;
    }

    submitForm();
}