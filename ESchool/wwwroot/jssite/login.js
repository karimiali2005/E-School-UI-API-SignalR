function shPwd(id) {
    var x = document.getElementById(id);
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

function login() {
    hideError();
    var errmsg = getErrors('login');
    if (!Empty(errmsg)) {
        showError(errmsg);
        return;
    }

    var data = {};
    data['loginUsername'] = p2e(a2e(document.getElementById('loginUsername').value));
    data['loginPassword'] = p2e(a2e(document.getElementById('loginPassword').value));
    data['remeberme'] = document.getElementById('remeberme').checked;
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Account/Login', data, 'POST');
}

function logoutuser() {
    window.location.href = '/Account/Logout';
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