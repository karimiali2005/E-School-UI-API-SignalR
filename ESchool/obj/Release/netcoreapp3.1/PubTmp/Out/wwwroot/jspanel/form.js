function selectCh(elem, hasZero = false) {

    var id = elem.id;
    var value = elem.value;
    var option = elem.options[elem.selectedIndex];
    var Grade1 = document.getElementById('Grade').parentElement;
    var StudyField1 = document.getElementById('StudyField').parentElement;

    if (id == "Degree") {

        var indexDegree = Degree.selectedIndex;
        var optionDegree = Degree.options[indexDegree];
        if (optionDegree.title.indexOf(',r') == -1) {
            addClass(Grade1);
            addClass(StudyField1);
        } else {
            removeClass(Grade1);
            removeClass(StudyField1);
        }

        var index = -1;
        for (let option of Grade.options) {
            var t = option.title.indexOf(',r') == -1 ? option.title : option.title.replace(',r', '');
            if (option.value != '0') {
                if (t == value) {
                    removeClass(option);
                    index = index == -1 ? option.index : index;
                } else {
                    addClass(option);
                }
            }
        }
        Grade.selectedIndex = index;        
        if (!Empty(Grade.options[index])) {
            var titleGrade = Grade.options[index].title;
            if (titleGrade.indexOf(',r') == -1) {
                addClass(StudyField1);
            } else {
                removeClass(StudyField1);
            }
        }
        if (hasZero) {
            /*Grade.selectedIndex = 0;*/
        }

    } else if (id == "Grade") {
        if (option.title.indexOf(',r') == -1) {
            addClass(StudyField1);
        } else {
            removeClass(StudyField1);
        }
    }

}

function showList(id) {
    var listcontent = document.getElementById('lc' + id);
    if (listcontent.className.indexOf('show') >= 0) {
        listcontent.classList.remove('show');
    } else {
        listcontent.classList.add('show');
    }

    var img = listcontent.previousElementSibling.querySelector('.listarrow');
    if (img.className.indexOf('rotate') >= 0) {
        img.classList.remove('rotate');
    } else {
        img.classList.add('rotate');
    }
}

function chkList(elem) {
    var listcontent = elem.parentElement.parentElement.nextElementSibling;
    var chks = listcontent.querySelectorAll('input[type=checkbox]');
    for (let chk of chks) {
        chk.checked = elem.checked;
    }
}

function getInputsWithoutId(withSelect = false) {
    var inputs = withSelect == true ?
        document.querySelectorAll('.row input[class^=\'chPreviousColor\'][type=text], .row input[class^=\'chPreviousColor\'][type=password], .row select[class^=\'chPreviousColor\'], .row textarea[class^=\'chPreviousColor\']') :
        document.querySelectorAll('.row input[class^=\'chPreviousColor\'][type=text], .row input[class^=\'chPreviousColor\'][type=password], .row textarea[class^=\'chPreviousColor\']');
    console.log(inputs);
    return inputs;
}

function getInputs(id, withSelect = false) {
    var inputs = withSelect == true ?
        document.querySelectorAll('.row input[id^="' + id + '"][type=text], .row input[id^="' + id + '"][type=password], .row select[id^="' + id + '"], .row textarea[id^="' + id + '"]') :
        document.querySelectorAll('.row input[id^="' + id + '"][type=text], .row input[id^="' + id + '"][type=password], .row textarea[id^="' + id + '"]');
    return inputs;
}


function getErrorsPanel(id) {    
    var noNeeds = ['telephone', 'address'];

    var msg_errors = '';
    var inputs = Empty(id) ? getInputs() : getInputs(id);
    for (let input of inputs) {

        var allow = true;
        var InputId = input.id.toLowerCase();
        for (var no of noNeeds) {
            if (InputId.indexOf(no) >= 0) {
                allow = false;
            }
        }

        if (allow == true) {
            if (Empty(input.value)) {
                var lbl = input.previousElementSibling;
                msg_errors += ' فیلد ' + lbl.innerHTML + ' اجباری می باشد ' + '<br />';
                setError(input);
            }
        }
    }

    var irnational = document.getElementById(id + "Irnational");
    if (!Empty(irnational)) {
        if (!checkIrNationalCode(irnational.value)) {
            msg_errors += irnational.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />';
            setError(irnational);
        }
    }

    var certificateserial = document.getElementById(id + "IdcardSerial");
    if (!Empty(certificateserial)) {
        if (!checkDigits(certificateserial.value)) {
            msg_errors += certificateserial.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />';
            setError(certificateserial);
        }
    }

    var certificatenum = document.getElementById(id + "IdcardSeriesNumber");
    if (!Empty(certificatenum)) {
        if (!checkDigits(certificatenum.value)) {
            msg_errors += certificatenum.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />'
            setError(certificatenum);
        }
    }

    var birthyear = document.getElementById(id + "BirthYear");
    if (!Empty(birthyear)) {
        if (!checkPersianYear(birthyear.value)) {
            msg_errors += birthyear.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />';
            setError(birthyear);
        }
    }

    var phone = document.getElementById(id + "Telephone");
    if (!Empty(phone)) {
        if (!Empty(phone.value)) {
            if (!checkPhone(phone.value)) {
                msg_errors += phone.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />';
                setError(phone);
            }
        }
    }

    var mobile = document.getElementById(id + "MobileNumber");
    if (!Empty(mobile)) {
        if (!checkMobile(mobile.value)) {
            msg_errors += mobile.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />';
            setError(mobile);
        }
    }

    return msg_errors;
}


/* Upload Profile Pic */
var file = null;
function uProfilepicPanel(uploader) {

    errmsg = '';
    hideError();
    var imgId = uploader.id.replace('Pic', 'Image');
    var img = document.getElementById(imgId);
    var reader = new FileReader();
    var mime_types = ['image/jpeg', 'image/png'];

    file = uploader.files[0];

    if (Empty(file)) {
        errmsg = 'فایل به درستی ارسال نشده است';
    } else {
        if (file.size > 5242880) {
            errmsg += "اندازه فایل " + file.name + " بیشتر از 5 مگابایت می باشد " + "<br/>";
        }
        else if (mime_types.indexOf(file.type) == -1) {
            errmsg += " فایل " + file.name + " از نوع تصویر نمی باشد " + "<br/>";
        } else {

            reader.onload = function (e) {
                img.src = e.target.result;
            }

            reader.readAsDataURL(file);
        }
    }

    if (!Empty(errmsg)) {
        showError(errmsg);
        file = null;        
    }
}

function remURL() { (URL || webkitURL).revokeObjectURL(this.src) }

function openBigBox(elem) {
    elem.classList.toggle('rotate');
    elem.parentElement.classList.toggle('hideOverflow');
}