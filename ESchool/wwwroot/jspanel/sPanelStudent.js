function getStudents(page) {
    var degree = document.getElementById('Degree');
    var grade = document.getElementById('Grade');
    var studyField = document.getElementById('StudyField');
    var q = document.getElementById('q');
    var CountRow = document.getElementById('CountRow');

    var data = {};

    if (!Empty(degree.value)) {
        data['degree'] = degree.value;
    }
    if (!(Empty(grade.value) || grade.parentElement.className.indexOf('displayNone') >= 0)) {
        data['grade'] = grade.value;
    }
    if (!(Empty(studyField.value) || studyField.parentElement.className.indexOf('displayNone') >= 0)) {
        data['studyField'] = studyField.value;
    }
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

    passingData('/Panel/Student', data);
}

function redirectChDegree() {
    var data = {};
    data['id'] = getChChecked(false);
    if (data['id'] != null) {
        data['returnUrl'] = getUrl();
        passingData('/Panel/Student/Degree', data);
    }
}

function setChDegree() {
    var degree = document.getElementById('Degree');
    var grade = document.getElementById('Grade');
    var studyField = document.getElementById('StudyField');
    var id = document.getElementById('id');
    var returnUrl = document.getElementById('returnUrl');

    var data = {};

    if (!Empty(degree.value)) {
        data['degree'] = degree.value;
    }
    if (!(Empty(grade.value) || grade.parentElement.className.indexOf('displayNone') >= 0)) {
        data['grade'] = grade.value;
    }
    if (!(Empty(studyField.value) || studyField.parentElement.className.indexOf('displayNone') >= 0)) {
        data['studyField'] = studyField.value;
    }
    if (!Empty(id.value)) {
        data['id'] = id.value;
    }
    if (!Empty(returnUrl)) {
        data['returnUrl'] = returnUrl.value;
    }

    passingData('/Panel/Student/Degree', data, 'POST');
}

function extrainfo() {
    var data = {};
    data['id'] = getChChecked(false);
    if (data['id'] != null) {
        data['returnUrl'] = getUrl();
        passingData('/Panel/Student/ExtraInfo', data);
    }
}

function extrainfoed() {
    hideError();
    errmsg = '';

    var Nationality = document.getElementById('Nationality');
    var Insurance = document.getElementById('Insurance');
    var FamilyNumber = document.getElementById('FamilyNumber');
    var SeveralChildren = document.getElementById('SeveralChildren');
    var RightHanded = document.getElementById('RightHanded');
    var PersianLanguage = document.getElementById('PersianLanguage');
    var IsRelativesParents = document.getElementById('IsRelativesParents');
    var IsStudentInsurance = document.getElementById('IsStudentInsurance');
    var PreschoolEducation = document.getElementById('PreschoolEducation');
    var IsCityPlace = document.getElementById('IsCityPlace');
    var ReferralReasonNew = document.getElementById('ReferralReasonNew');

    
    if (Empty(Insurance.value)) {
        errmsg += ' فیلد ' + Insurance.previousElementSibling.innerHTML + ' اجباری می باشد ' + '<br />';
        setError(Insurance);
    }

    if (!checkDigits(FamilyNumber.value)) {
        errmsg += ' فیلد ' + FamilyNumber.previousElementSibling.innerHTML + ' اجباری می باشد ' + '<br />';
        setError(FamilyNumber);
    }

    if (!checkDigits(SeveralChildren.value)) {
        errmsg += ' فیلد ' + SeveralChildren.previousElementSibling.innerHTML + ' اجباری می باشد ' + '<br />';
        setError(SeveralChildren);
    }

    if (Empty(errmsg)) {
        submitForm();
    } else {
        showError(errmsg);
    }
}

function parent() {
    var data = {};
    data['id'] = getChChecked(false);
    if (data['id'] != null) {
        data['returnUrl'] = getUrl();
        passingData('/Panel/Student/Parent', data);
    }
}

function parented() {
    hideError();
    errmsg = '';

    var IrnationalFather = document.getElementById('IrnationalFather');
    var IrnationalMother = document.getElementById('IrnationalMother');

    if (Empty(IrnationalFather.value)) {
        errmsg += ' فیلد ' + IrnationalFather.previousElementSibling.innerHTML + ' اجباری می باشد ' + '<br />';
        setError(IrnationalFather);
    }    

    if (!Empty(IrnationalFather)) {
        if (!checkIrNationalCode(IrnationalFather.value)) {
            errmsg += IrnationalFather.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />';
            setError(IrnationalFather);
        }
    }

    if (Empty(IrnationalMother.value)) {
        errmsg += ' فیلد ' + IrnationalMother.previousElementSibling.innerHTML + ' اجباری می باشد ' + '<br />';
        setError(IrnationalMother);
    }

    if (!Empty(IrnationalMother)) {
        if (!checkIrNationalCode(IrnationalMother.value)) {
            errmsg += IrnationalMother.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />';
            setError(IrnationalMother);
        }
    }

    if (Empty(errmsg)) {
        submitForm();
    } else {
        showError(errmsg);
    }
}


function chLivePersonPanel(elem) {
    var next = elem.parentElement.nextElementSibling;
    if (elem.selectedIndex == 3) {
        next.classList.remove('displayNone');
        next.classList.add('rowInline');
        var input = next.getElementsByTagName("input")[0];
        input.value = next.className.indexOf('displayNone') >= 0 ? "ندارد" : "";
    } else {
        next.classList.add('displayNone');
        next.classList.remove('rowInline');
        var input = next.getElementsByTagName("input")[0];
        input.value = next.className.indexOf('displayNone') >= 0 ? "ندارد" : "";
    }

}

function newsStudent(id) {
    var data = {};
    if (!Empty(id)) {
        data['id'] = id;
    }
    data['returnUrl'] = getUrl();
    passingData('/Panel/Student/New', data);    
}

function newdedStudent() {

    hideError();
    var errmsg = getErrorsPanel('');

    if (Empty(errmsg)) {
        saveStudent();
    } else {
        showError(errmsg);
    }
}

function saveStudent() {
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
    request.open('post', '/Panel/Student/Newed');

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

