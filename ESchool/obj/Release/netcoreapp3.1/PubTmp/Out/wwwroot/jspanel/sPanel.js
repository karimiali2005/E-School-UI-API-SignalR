
function getChChecked(all = true, allowNoSelect = false) {
    var chkGrids = document.querySelectorAll('td .chkGrid');    
    var ids = [];
    for (let chkGrid of chkGrids) {
        if (chkGrid.checked) {
            ids.push(parseInt(chkGrid.value));
        }
    }

    if (allowNoSelect == false) {
        if (ids.length == 0) {
            alert('هیچ سطری انتخاب نشده است')
            return null;
        }

        if (all == false) {
            if (ids.length > 1) {
                alert('فقط یک سطر را انتخاب نمایید');
                return null;
            } else {
                return ids[0];
            }
        }
    }
    
    return ids;

}

function getExcel(id) {
    var degree = document.getElementById('Degree');
    var grade = document.getElementById('Grade');
    var studyField = document.getElementById('StudyField');
    var q = document.getElementById('q');        

    var form = new FormData();

    if (!Empty(degree)) {
        if (!Empty(degree.value)) {
            form.append('degreeId', degree.value);
            console.log(degree.value);
        }
    }
    if (!Empty(grade)) {
        if (!(Empty(grade.value) || grade.parentElement.className.indexOf('displayNone') >= 0)) {            
            form.append('gradeId', grade.value);
        }
    }
    if (!Empty(studyField)) {
        if (!(Empty(studyField.value) || studyField.parentElement.className.indexOf('displayNone') >= 0)) {
            form.append('studyFieldId', studyField.value);
        }
    }
    if (!Empty(q.value)) {
        form.append('q', q.value);
    }    

    form.append('usertypeid', id)

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/User/DownloadExcelDocument');
    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            hideLoader();  
            a = document.createElement('a');
            a.href = window.URL.createObjectURL(request.response);
            // Give filename you wish to download
            var fileName = "students.xlsx";
            if (id == 2 || id == 3) {
                fileName = "parents.xlsx";
            }
            if (id == 4) {
                fileName = "teachers.xlsx";
            }
            a.download = fileName;
            a.style.display = 'none';
            document.body.appendChild(a);
            a.click();
        }
    };
    request.onerror = function (err) {
        hideLoader();
        alert(err.message);
    };
    showLoader();
    request.responseType = 'blob';    
    request.send(form);
    
}

function uactive(t, id) {
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

    data['t'] = t;
    data['returnUrl'] = getUrl();
    passingData('/Panel/User/UserActive', data, 'POST');
}


function uactived() {
    var lbls = document.getElementsByClassName('lbl');
    var ids = [];
    for (let lbl of lbls) {
        if (!Empty(lbl.title)) {
            ids.push(lbl.title); 
        }
    }

    var data = {};
    data['ids'] = JSON.stringify(ids);
    data['t'] = document.getElementById('t').value;
    var explain = document.getElementById('explain');
    if (!Empty(explain)) {
        data['explain'] = explain.value;
    } else {
        data['explain'] = '';
    }
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/User/UserActived', data, 'POST');
}

function utype(id) {
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
    passingData('/Panel/User/UserType', data, 'POST');
}


function utyped() {
    var selects = document.getElementsByTagName('select');
    var ids = [];
    var uts = [];
    for (let select of selects) {
        if (!Empty(select.title)) {
            ids.push(select.title);
            uts.push(select.value);
        }
    }

    var data = {};
    data['ids'] = JSON.stringify(ids);    
    data['uts'] = JSON.stringify(uts);    
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/User/UserTyped', data, 'POST');
}

function urelation(id) {
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
    passingData('/Panel/User/UserRelation', data, 'POST');
}

function udelete(id) {
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
    passingData('/Panel/User/UserDelete', data, 'POST');
}


function udeleted() {
    var lbls = document.getElementsByClassName('lbl');
    var ids = [];
    for (let lbl of lbls) {
        ids.push(lbl.title);
    }

    var data = {};
    data['ids'] = JSON.stringify(ids);    
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/User/UserDeleted', data, 'POST');
}


function upwd(id) {
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
    passingData('/Panel/User/UserPassword', data, 'POST');
}

function upwded() {
    var inputs = document.querySelectorAll('input[class^=\'chPreviousColor\']');
    var ids = [];
    var ps = [];
    for (let input of inputs) {
        ids.push(input.id);
        ps.push(input.value);
    }

    var data = {};
    data['ids'] = JSON.stringify(ids);
    data['ps'] = JSON.stringify(ps);
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/User/UserPassworded', data, 'POST');
}

function upic(id) {
    var data = {};
    data['id'] = id;
    data['returnUrl'] = getUrl();
    passingData('/Panel/User/UserPic', data, 'POST');
}

function upiced() {
    var crop = getCrop();

    var data = {};
    data['id'] = document.getElementById('id').value;
    data['top'] = crop['x'];
    data['left'] = crop['y'];
    data['width'] = crop['width'];
    data['height'] = crop['height'];
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/User/UserPicCrop', data, 'POST');
}





function uStudentFinancial(id) {
    var data = {};


    data['id'] = id;



    if (data['id'] != null) {
        data['returnUrl'] = getUrl();
        passingData('/Panel/Student/StudentFinancial', data);
    }
}

function uStudentFinancialed() {
    var financialSendType = document.getElementById('FinancialSendTypeId');


    var data = {};

    if (!Empty(financialSendType.value)) {
        data['financialSendTypeId'] = financialSendType.value;
    }

    data['financialText'] = document.getElementById('FinancialText').value;

    data['id'] = document.getElementById('id').value;
    data['returnUrl'] = document.getElementById('returnUrl').value;

    passingData('/Panel/Student/StudentFinancial', data, 'POST');
}

function studentFinancialdelete() {
    var data = {};
    var ids = getChChecked(true);
    if (ids != null) {
        data['ids'] = JSON.stringify(ids);

        data['returnUrl'] = getUrl();
        passingData('/Panel/Student/StudentFinancialDelete', data, 'POST');
    }
}

function studentFinancialdeleted() {


    var data = {};
    data['ids'] = document.getElementById('ids').value;
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Student/StudentFinancialDeleted', data, 'POST');
}


function uStudentDiscipline(id) {
    var data = {};


    data['id'] = id;



    if (data['id'] != null) {
        data['returnUrl'] = getUrl();
        passingData('/Panel/Student/StudentDiscipline', data);
    }
}


function uStudentDisciplined() {
    var disciplineType = document.getElementById('DisciplineTypeId');


    var data = {};

    if (!Empty(disciplineType.value)) {
        data['disciplineTypeId'] = disciplineType.value;
    }

    data['disciplineText'] = document.getElementById('DisciplineText').value;

    data['id'] = document.getElementById('id').value;
    data['returnUrl'] = document.getElementById('returnUrl').value;

    passingData('/Panel/Student/StudentDiscipline', data, 'POST');
}

function studentDisciplindelete() {
    var data = {};
    var ids = getChChecked(true);
    if (ids != null) {
        data['ids'] = JSON.stringify(ids);

        data['returnUrl'] = getUrl();
        passingData('/Panel/Student/StudentDisciplineDelete', data, 'POST');
    }
}

function studentDisciplindeleted() {


    var data = {};
    data['ids'] = document.getElementById('ids').value;
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Student/StudentDisciplineDeleted', data, 'POST');
}