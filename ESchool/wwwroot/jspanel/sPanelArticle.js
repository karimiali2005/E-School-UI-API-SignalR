function newsArticle(){
    var data = {};

    data['returnUrl'] = getUrl();
    passingData('/Panel/Article/New', data);
}

function newdedArticle() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveArticle();
    } else {
        showError(errmsg);
    }
}

function saveArticle() {
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

    data.append('ArticleTitle', document.getElementById("ArticleTitle").value);

    data.append('ArticleBody', document.getElementById("ArticleBody").value);
    data.append('ArticleStudyTime', document.getElementById("ArticleStudyTime").value);

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
    xhttp.open('POST', '/Panel/Article/New');
    xhttp.send(data);
}

function editArticle(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Article/Edit', data);
}



function editedArticle(id) {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveEditArticle(id);
    } else {
        showError(errmsg);
    }
}

function saveEditArticle(id) {
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
    data.append('id',id);

    data.append('ArticleTitle', document.getElementById("ArticleTitle").value);

    data.append('ArticleBody', document.getElementById("ArticleBody").value);
    data.append('ArticleStudyTime', document.getElementById("ArticleStudyTime").value);

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
    xhttp.open('POST', '/Panel/Article/Edit');
    xhttp.send(data);
}

function articledelete(id) {
    var data = {};
    if (Empty(id)) {

        return;

    } else {

        data['id'] = id;
    }

    data['returnUrl'] = getUrl();
    passingData('/Panel/Article/Delete', data, 'GET');
}


function articledeleted(id) {
    var data = {};
    if (Empty(id)) {

        return;

    } else {

        data['id'] = id;
    }

    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Article/Deleted', data, 'POST');
}

