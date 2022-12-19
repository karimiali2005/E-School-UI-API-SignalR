function newsGallery() {
    var data = {};
   
    data['returnUrl'] = getUrl();
    passingData('/Panel/Gallery/New', data);
}

function newdedGallery() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveGallery();
    } else {
        showError(errmsg);
    }
}

function saveGallery() {
    var form = new FormData();

    var inputs = getInputsWithoutId(true);
    for (let input of inputs) {
        form.append(input.id, input.value);
    }

    form.append("GalleryId", document.getElementById('GalleryId').value);
    form.append("returnUrl", document.getElementById('returnUrl').value);

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/Gallery/Newed');

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


function editGallery(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Gallery/Edit', data);
}

function editedGallery() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveEditGallery();
    } else {
        showError(errmsg);
    }
}

function saveEditGallery() {
    var form = new FormData();

    var inputs = getInputsWithoutId(true);
    for (let input of inputs) {
        form.append(input.id, input.value);
    }

    form.append("GalleryId", document.getElementById('GalleryId').value);
    form.append("returnUrl", document.getElementById('returnUrl').value);

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/Gallery/Edited');

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

function gallerydelete(id) {
    var data = {};
    if (Empty(id)) {

        return;

    } else {

        data['id'] = id;
    }

    data['returnUrl'] = getUrl();
    passingData('/Panel/Gallery/Delete', data, 'GET');
}


function gallerydeleted(id) {
    var data = {};
    if (Empty(id)) {

        return;

    } else {

        data['id'] = id;
    }

    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Gallery/Deleted', data, 'POST');
}


function galleryDetailShow(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Gallery/GalleryDetailPicShow', data);
}
function newGalleryDetailPicShow(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Gallery/NewDetailPic', data);
}



function  newGalleryDetailPicShowInsert(id) {

    showLoader();
    var input = document.getElementById('filePic');
    var data = new FormData();

    if (input !== null) {
        var file = input.files[0];


        if (file == 'undefined' && file == null ) {
            hideLoader();
          
            input.value = '';
            return;
        }
        data.append('filePic', input.files[0]);
    }

    data.append('galleryIsDefault', document.getElementById("GalleryIsDefault").checked);
  
    data.append('id', id);
    
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
    xhttp.open('POST', '/Panel/Gallery/NewDetailPic');
    xhttp.send(data);

}


function gallerydetaildelete(id) {
    var data = {};
    if (Empty(id)) {

        return;

    } else {

        data['id'] = id;
    }

    data['returnUrl'] = getUrl();
    passingData('/Panel/Gallery/GalleryDetailDelete', data, 'GET');
}


function gallerydetaildeleted(id) {
    var data = {};
    if (Empty(id)) {

        return;

    } else {

        data['id'] = id;
    }

    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Gallery/GalleryDetailDeleted', data, 'POST');
}



function galleryDetailVideoShow(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Gallery/GalleryDetailVideoShow', data);
}

function newGalleryDetailVideoShow(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Gallery/NewDetailVideo', data);
}

function newGalleryDetailVideoShowInsert(id) {

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

    data.append('galleryIsDefault', document.getElementById("GalleryIsDefault").checked);

    data.append('id', id);

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
    xhttp.open('POST', '/Panel/Gallery/NewDetailVideo');
    xhttp.send(data);

}