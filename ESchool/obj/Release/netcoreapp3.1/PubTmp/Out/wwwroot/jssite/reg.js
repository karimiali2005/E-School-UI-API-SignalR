var errmsg = "";

function changeProgress(step, m = 'p') {
    var steps = ["مرحله 1 از 5 - ", "مرحله 2 از 5 - ", "مرحله 3 از 5 - ", "مرحله 4 از 5 - ", "مرحله 5 از 5 - "];
    var titles = ["انتخاب مقطع و رشته تحصیلی", "اطلاعات دانش آموز", "اطلاعات پدر دانش آموز", "اطلاعات مادر دانش آموز", "اطلاعات تکمیلی"];    

    var contentTitle = document.getElementById('contentTitle');
    var contentProgress = document.getElementById('contentProgress');
    var formTitle = document.getElementById('formTitle');

    var afterStep = step;
    var currentStep = m == 'p' ? step - 1 : step + 1;

    contentTitle.innerHTML = steps[afterStep - 1] + titles[afterStep - 1];
    contentProgress.style.width = (afterStep * 20) + '%';
    contentProgress.innerHTML = contentProgress.style.width;
    formTitle.innerHTML = titles[afterStep - 1];

    var CurrentStep = document.getElementById("step" + (currentStep));
    var NextStep = document.getElementById("step" + (afterStep));    

    CurrentStep.className = CurrentStep.className.replace("opacityOne", "opacityZero");
    NextStep.className = NextStep.className.replace("opacityZero", "opacityOne");
}

function selectCh(elem) {

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
            if (t == value) {
                removeClass(option);
                index = index == -1 ? option.index : index;
            } else {
                addClass(option);
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

    } else if (id == "Grade") {        
        if (option.title.indexOf(',r') == -1) {
            addClass(StudyField1);
        } else {
            removeClass(StudyField1);
        }
    }

}

var hasFather = false;
var hasMother = false;

function checkIrNational(t = 'student') {

    code = document.getElementById(t + "IRNational").value;

    if (!checkIrNationalCode(code)) {
        alert('کد ملی را به صورت صحیح وارد نمایید');
        return;
    }

    var form = new FormData();
    form.append('irnational', code);    

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Account/GetUserIrNational');

    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            hideError();            

            if (this.responseText.indexOf("ok,") >= 0) {
                alert(this.responseText.replace("ok,", ""));
                if (t == 'student') {
                    alert("برای این دانش آموز نیازی به ثبت نام مجدد نیست");
                    window.location.href = '/Account/Register';
                } else if (t == 'father') {
                    alert("هم اکنون به مرحله بعد منتقل می شوید");
                    hasFather = true;
                    changeProgress(4);
                } else if (t == 'mother') {
                    alert("هم اکنون به مرحله بعد منتقل می شوید");
                    hasMother = true;
                    changeProgress(5);
                }
                hideLoader();
                
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



function step1(m = 'p') {

    hideError();
    changeProgress(1, m);
    Degree.focus();
}

function step2(m = 'p') {

    hideError();
    changeProgress(2, m);    
    document.getElementById("studentName").focus();     
}

function step2Error() {
    errmsg = '';
    if (Empty(file)) {
        errmsg += ' تصویر دانش آموز ارسال نشده است ' + '<br/>';
    }
    errmsg += getErrors('student');
    if (Empty(errmsg)) {
        return true;
    } else {
        showError(errmsg);
        return false;
    }
}

function chLivePerson(elem) {    
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

function step3(m = 'p') {
    hideError();
    if (step2Error() == true) {
        changeProgress(3, m);
        document.getElementById("fatherName").focus();
    }
}

function step3Error() {
    errmsg = '';
    if (!hasFather) {
        errmsg += getErrors('father');
        if (Empty(errmsg)) {
            return true;
        } else {
            showError(errmsg);
            return false;
        }
    }
    else {
        return true;
    }
}


function step4(m = 'p') {
    hideError();
    if (step3Error() == true) {
        changeProgress(4, m);
        document.getElementById("motherName").focus();       
    }
}

function step4Error() {
    errmsg = '';
    if (!hasMother) {
        errmsg += getErrors('mother');
        if (Empty(errmsg)) {
            return true;
        } else {
            showError(errmsg);
            return false;
        }
    } else {
        return true;
    }
}

function step5(m = 'p') {
    hideError();
    if (step4Error() == true) {
        changeProgress(5, m);
        Nationality.focus();
    }
}




function final() {
    hideError();   
    errmsg = '';

    if (Empty(Nationality.value)) {
        errmsg += ' فیلد ' + Nationality.previousElementSibling.innerHTML + ' اجباری می باشد ' + '<br />';
        setError(Nationality);
    }

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
        sendReg();
    } else {
        showError(errmsg);
    }

}

function sendReg() {    

    var form = new FormData();

    form.append(Degree.id, Degree.value);
    var GradeId = Grade.id;
    var gradeValue = Degree.options[Degree.selectedIndex].title.indexOf(',r') >= 0 ? Grade.value : "";
    form.append(GradeId, gradeValue);
    var StudyFieldId = StudyField.id;
    var studyfield = 0;
    if (!Empty(Grade.options[Grade.selectedIndex])) {
        studyfield = Grade.options[Grade.selectedIndex].title.indexOf(',r') >= 0 ? StudyField.value : "";
    }    
    form.append(StudyFieldId, studyfield);    
    
    var inputs = getInputs('student', true);
    for (let input of inputs) {
        form.append(input.id, input.value);
    }
    var inputs2 = getInputs('father', true);
    for (let input of inputs2) {
        form.append(input.id, input.value);
    }
    var inputs3 = getInputs('mother', true);
    for (let input of inputs3) {
        form.append(input.id, input.value);
    }
    form.append(Nationality.id, Nationality.value);
    form.append(Insurance.id, Insurance.value);
    form.append(FamilyNumber.id, FamilyNumber.value);
    form.append(SeveralChildren.id, SeveralChildren.value);
    form.append(RightHanded.id, RightHanded.value);
    form.append(PersianLanguage.id, PersianLanguage.value);
    form.append(IsRelativesParents.id, IsRelativesParents.value);
    form.append(IsStudentInsurance.id, IsStudentInsurance.value);
    form.append(PreschoolEducation.id, PreschoolEducation.value);
    form.append(IsCityPlace.id, IsCityPlace.value);
    form.append(ReferralReasonNew.id, ReferralReasonNew.value);   

    form.append("file", file);    

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Account/Reg');

    /*request.upload.addEventListener('progress', function (e) {
        var percent_complete = (e.loaded / e.total) * 100;
        console.log("Uploaded: " + percent_complete + "%");
    });
    request.addEventListener('load', function (e) {
        console.log(request.status);
        console.log(request.response);
    });*/
    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            hideError();
            if (this.responseText == "ok") {
                alert("ضمن تشکر از شما ثبت نام با موفقیت انجام شد");
                window.location.href = "/Account/Message/RegisterSuccess";
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

function getDataReg(id) {
    var data = {};
    
    return data;
}


function getErrors(id) {
    var ids = ['father', 'mother'];
    var noNeeds = ['number', 'address'];

    var msg_errors = '';
    var inputs = Empty(id) ? getInputs() : getInputs(id);
    for (let input of inputs) {

        var allow = true;
        if (ids.indexOf(id) >= 0) {
            var InputId = input.id.toLowerCase();            
            for (var no of noNeeds) {
                if (InputId.indexOf(no) >= 0) {
                    allow = false;
                }
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

    var CodeMeli = 'IRNational';
    var CertificateSerial = 'CertificateSerial'
    var CertificateNum = 'CertificateNum';
    var BirthYear = 'BirthYear';
    var Number = 'Number';
    var MobileNumber = 'MobileNumber';
    if (Empty(id)) {
        CodeMeli = 'Irnational';
        CertificateSerial = 'IdcardSerial'
        CertificateNum = 'IdcardSeriesNumber';
        BirthYear = 'BirthYear';
        Number = 'Telephone';
        MobileNumber = 'MobileNumber';
    }

    var irnational = document.getElementById(id + CodeMeli);
    if (!Empty(irnational)) {
        if (!checkIrNationalCode(irnational.value)) {
            msg_errors += irnational.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />';
            setError(irnational);
        }
    }

    var certificateserial = document.getElementById(id + CertificateSerial);
    if (!Empty(certificateserial)) {
        if (!checkDigits(certificateserial.value)) {
            msg_errors += certificateserial.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />';
            setError(certificateserial);
        }
    }

    var certificatenum = document.getElementById(id + CertificateNum);
    if (!Empty(certificatenum)) {
        if (!checkDigits(certificatenum.value)) {
            msg_errors += certificatenum.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />'
            setError(certificatenum);
        }
    }

    var birthyear = document.getElementById(id + BirthYear);
    if (!Empty(birthyear)) {
        if (!checkPersianYear(birthyear.value)) {
            msg_errors += birthyear.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />';
            setError(birthyear);
        }
    }

    var phone = document.getElementById(id + Number);
    if (!Empty(phone)) {
        if (!Empty(phone.value)) {
            if (!checkPhone(phone.value)) {
                msg_errors += phone.previousElementSibling.innerHTML + ' صحیح نمی باشد ' + '<br />';
                setError(phone);
            }
        }    
    }

    var mobile = document.getElementById(id + MobileNumber);    
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
function uProfilepic(uploader) {

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


/* Elements */

document.addEventListener("DOMContentLoaded", function () {
    Grade = document.getElementById('Grade');
    Degree = document.getElementById('Degree');
    StudyField = document.getElementById('StudyField');

    Nationality = document.getElementById('Nationality');
    Insurance = document.getElementById('Insurance');
    FamilyNumber = document.getElementById('FamilyNumber');
    SeveralChildren = document.getElementById('SeveralChildren');
    RightHanded = document.getElementById('RightHanded');
    PersianLanguage = document.getElementById('PersianLanguage');
    IsRelativesParents = document.getElementById('IsRelativesParents');
    IsStudentInsurance = document.getElementById('IsStudentInsurance');
    PreschoolEducation = document.getElementById('PreschoolEducation');
    IsCityPlace = document.getElementById('IsCityPlace');
    ReferralReasonNew = document.getElementById('ReferralReasonNew');
});

var Degree = null;
var Grade = null;
var StudyField = null;

var Nationality = null;
var Insurance = null;
var FamilyNumber = null;
var SeveralChildren = null;
var RightHanded = null;
var PersianLanguage = null;
var IsRelativesParents = null;
var IsStudentInsurance = null;
var PreschoolEducation = null;
var IsCityPlace = null;
var ReferralReasonNew = null;








