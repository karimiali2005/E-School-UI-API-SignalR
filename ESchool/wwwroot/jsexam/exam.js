var i = document.getElementsByClassName("question-type").length;
function addQuestionFirst(type) {


    //$("#div_defaultQuestion_hidden").css("display", "block");
    // $("#first-btn-add").css("display", "none");

    $.ajax({
        url: '/Exam/AddQuestion',
        type: 'GET',
        data: {
            type: type,
            i: i
        },
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            i++;
            var thisseparator = $("#div_defaultQuestion_hidden").closest("div");
            $($.parseHTML(result, document, true)).insertAfter(thisseparator);
            $("#first-btn-add").css("display", "none");
            showScore();
            setRandom();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("error");
        }
    });
}

function addQuestion(a, type) {
    i = document.getElementsByClassName("question-type").length;
    $.ajax({
        url: '/Exam/AddQuestion',
        type: 'GET',
        data: {
            type: type,
            i: i
        },
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            i++;
            var thisseparator = $(a).closest(".question-type");
            $($.parseHTML(result, document, true)).insertAfter(thisseparator);
            showScore();
            setRandom();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("error");
        }
    });


    //$('#remove_more').fadeIn(function () {
    //    $(this).show();
    //});
}

function deleteQuestion(a) {
    i--;
    var thisseparator = $(a).closest(".question-type");
    $(thisseparator).remove();
    i = $(".question-type").length;
    if (i == 0) {
        $("#first-btn-add").css("display", "block");
        //$("#div_defaultQuestion_hidden").css("display", "none");
    }
}

function validateForm() {
    var countError = 0;
    var multiflag = false;
    $(".text-danger").text("");
    $(".QuestionTitle").removeClass("input-validation-error");
    $(".option-input").removeClass("input-validation-error");
    $("#myalert").addClass('d-none');
    $("#myalert").text("");
    var RoomChatGroupId = $('option:selected', "#RoomChatGroupId").val();

    if (RoomChatGroupId === "" || RoomChatGroupId === undefined) {
        $("#myalert").append("<br />").append("کلاس مورد نظر را انتخاب نمایید ");
        countError++;
    }


    var allquestion = $(".question-type");
    if (allquestion.length > 0) {

        $.each(allquestion, function (i) {
            var errorString = "";
            var thisquestion = $(this);
            var questiontype = thisquestion.attr("data-typeid");
            var title = thisquestion.find('.QuestionTitle').val();
            if (title.length === 0) {
                thisquestion.find(".inp-write-question  .text-danger").text("عنوان سوال را وارد نمایید");
                countError++;
                multiflag = true;
            }
            if (title !== null && title !== "") {
                if (thisquestion.find(".QuestionTitle").attr("minlength")) {
                    var minLenght = thisquestion.find(".QuestionTitle").attr("minlength");
                    if (title.length < parseInt(minLenght)) {
                        thisquestion.find(".inp-write-question  .text-danger").removeClass('d-none').focus();
                        errorString = "حداقل تعداد کارکترهای وارد شده باید برابر" + minLenght;
                        thisquestion.find(".inp-write-question  .text-danger").append("<br />");
                        thisquestion.find(".inp-write-question  .text-danger").append(errorString);
                        countError++;
                        multiflag = true;
                    }
                }
                if (thisquestion.find(".QuestionTitle").attr("maxlength")) {
                    var maxLenght = thisquestion.find(".QuestionTitle").attr("maxlength");
                    if (title.length > parseInt(maxLenght)) {
                        thisquestion.find(".inp-write-question  .text-danger").removeClass('d-none').focus();
                        errorString = "حداکثر تعداد کارکترهای وارد شده باید برابر" + maxLenght;
                        thisquestion.find(".inp-write-question  .text-danger").append("<br />");
                        thisquestion.find(".inp-write-question  .text-danger").append(errorString);
                        countError++;
                        multiflag = true;
                    }
                }
            }
            //QuestionTitle
            if (questiontype === "2") {

                var radiosItems = thisquestion.find(".text-option-input");
                if (radiosItems.length === 0) {
                    multiflag = true;
                    thisquestion.find(".inp-write-question  .text-danger").append("<br />");
                    thisquestion.find(".inp-write-question  .text-danger").append("گزینه ها را وارد نمایید");

                } else {

                    $.each(radiosItems, function (indexinner) {
                        var flagChoice = false;
                        var radioText = $(this).find('.option-input').val();
                        if (radioText.length === 0) {
                            $(this).find(".text-danger").append("<br />");
                            $(this).find(".text-danger").append("عنوان گزینه را وارد نمایید");
                            countError++;
                            flagChoice = true;
                        }
                        if (radioText !== null && radioText !== "") {
                            if ($(this).find(".text-danger").attr("minlength")) {
                                var minLenght = $(this).find(".option-input").attr("minlength");
                                if (radioText.length < parseInt(minLenght)) {
                                    $(this).find(".text-danger").removeClass('d-none').focus();
                                    errorString = "حداقل تعداد کارکترهای وارد شده باید برابر" + minLenght;
                                    $(this).find(".text-danger").append("<br />");
                                    $(this).find(".text-danger").append(errorString);
                                    countError++;
                                    flagChoice = true;

                                }
                            }
                            if ($(this).find(".option-input").attr("maxlength")) {
                                var maxLenght = $(this).find(".option-input").attr("maxlength");
                                if (radioText.length > parseInt(maxLenght)) {
                                    $(this).find(".text-danger").removeClass('d-none').focus();
                                    errorString = "حداکثر تعداد کارکترهای وارد شده باید برابر" + maxLenght;
                                    $(this).find(".text-danger").append("<br />");
                                    $(this).find(".text-danger").append(errorString);
                                    countError++;
                                    flagChoice = true;
                                }
                            }
                        }

                        if (flagChoice) {
                            $(this).find('.option-input').addClass("input-validation-error");
                            flagChoice = false;
                        }
                    });
                }


            }
            if (multiflag) {

                thisquestion.find('.QuestionTitle').addClass("input-validation-error");
                multiflag = false;
            }

        });
    }

    //check number score
    var isNumberScore = $('option:selected', "#ScoreTypeId").attr('data-typescore');
    var maxscore = $('option:selected', "#ScoreTypeId").attr('data-maxscore');
    var sep = $("#seperator").val();
     maxscore = maxscore.replace("/", sep);
    if (isNumberScore === "True") {
        var allscore = $("input.Question-Score");
        sumscore = 0.0;
        if (allscore.length > 0) {
            $.each(allscore, function () {
                var score = $(this).val().replace("/", ".");
                sumscore += parseFloat(score);
            })
            if (parseFloat(maxscore) !== sumscore) {
                $("#myalert").append("<br />").append("لطفا نمره ها را بدقت وارد نمایید جمع نمره برابر باشد با:  " + maxscore);
                countError++;
            }
        }
    }
    //check random

    if ($("#RandomNumber").val() !== "0" && $("#RandomNumber").val() != "") {
        var sumrandom = 0;
        var randomNumber = $("#RandomNumber").val();
         sumrandom = $(".israndomnumber[type=checkbox]:checked").length;
        var sumquestions = $(".question-type").length;
        if (sumrandom < parseInt(randomNumber)) {
            $("#myalert").append("<br />").append("لطفا سوالات تصادفی نباید کمتر مقدار وارد شده باشد");
            countError++;
        }
        if (sumquestions < parseInt(randomNumber)) {
            $("#myalert").append("<br />").append("تعداد سوالات تصادفی از کل سوالات بیشتر است");
            countError++;
        }
    }
    if ($("#selectall").prop("checked") && ($("#RandomNumber").val() == "0" || $("#RandomNumber").val() == "")) {

        $("#myalert").append("<br />").append("لطفا تعداد سوالات تصادفی را وارد نمایید یا گزینه همه را انتخاب ننمایید");
        countError++;
    }
    if (countError > 0) {
        $("#myalert").removeClass('d-none').focus();
        $("#myalert").append("<br />").append("موارد بالا را تکمیل نمایید");
        return false;
    }
    if (countError === 0) {
        return true;
    }

}

function uploaderChange(a) {
    $("#myalert").addClass('d-none');
    $("#myalert").text("");
    var file = null;
    var errmsg = "";
    var reader = new FileReader();
    reader.onload = function (e) {
        file = a.files[0];
        if (Empty(file)) {
            errmsg = 'فایل به درستی ارسال نشده است';
        } else {
            if (file.size > 5242880) {
                errmsg = "اندازه فایل  بیشتر از 5 مگابایت می باشد ";
                alert(errmsg);
                return;
            }
            else {
                var box = $(a).parent();
                $(box).children(".boxPreview ").removeClass("d-none");
                $(box).children(".QuestionFilelabel").addClass("d-none");
                $(box).children('.boxPreview').children(".download").attr("href", e.target.result);
            }
        };
    };


    reader.readAsDataURL(a.files[0]);

}
function deleteImg(a) {
    var box = $(a).parent().parent();
    $(box).children('.boxPreview').children(".download").attr("href", "");
    $(box).children(".QuestionFilelabel").removeClass("d-none");
    $(box).children(".boxPreview").addClass("d-none");

}

function Empty(val) {
    return val === '' || val === undefined || val === null;
}

$('#ScoreTypeId').change(function () {
        showScore();
    });

function showScore() {
    var isNumberScore = $('option:selected', "#ScoreTypeId").attr('data-typescore');
    if (isNumberScore === "True") {
        $(".Question-Score").removeClass('d-none');
        $(".lblscore").removeClass('d-none');
    } else {
        $(".Question-Score").addClass('d-none');
        $(".lblscore").addClass('d-none');
        $("input.Question-Score").val("0");
    }
}

$("#selectall").click(function () {
    setRandom();
});
function setRandom() {
    if ($("#selectall").prop("checked")) {
        $(".israndomnumber").prop("checked", true);

    } else {
        $(".israndomnumber").prop("checked", false);
    }
}



function addchoice(e, indexq, qId) {
    var j = $(e).closest(".question-type").find(".optoin.my-2").length;
    var loc = $(e).closest(".question-type").find(".answer-options .optoin:last");
    var id = "QuestionChoiceFile" + j;
    $("<div class='optoin my-2'>"
        + "<input type='hidden' name='Questions[" + indexq + "].QuestionChoice[" + j + "].QuestionId' value='" + qId + "' />"
        + "<input type='hidden' name='Questions[" + indexq + "].QuestionChoice[" + j + "].QuestionChoiceId' value='0' />"
        + "<div class='my-22 d-flex ltr justify-content-end align-items-center custom-control custom-radio'>"
        + "<input class='custom-control-input' type='radio' name='Questions[" + indexq + "].QuestionChoice[" + j + "].IsAnswer' id='Questions[" + indexq + "].QuestionChoice[" + j + "].IsAnswer'  value='True'  >"
        + "<label class='mr-2 custom-control-label mr-3' for='Questions[" + indexq + "].QuestionChoice[" + j + "].IsAnswer'>"
        + " </label></div>"
        + "<div class='text-option-input'>"
        + "<input class='option-input' asp-for='Questions[" + indexq + "].QuestionChoice[" + j + "].QuestionChoiceTitle'  name='Questions[" + indexq + "].QuestionChoice[" + j + "].QuestionChoiceTitle'  type='text' >"
        + "<span class='text-danger'></span>"
        + "</div>"
        + "<div class=' delete-img'>"
        + " <div class='div-img'>"
        + " <input type='file' id='Questions[" + indexq + "].QuestionChoice[" + j + "].QuestionChoiceFile_file' name='Questions[" + indexq + "].QuestionChoice[" + j + "].QuestionChoiceFile_file' class='QuestionFile d-none'   onchange='uploaderChange(this)'>"
        + " <label for='Questions[" + indexq + "].QuestionChoice[" + j + "].QuestionChoiceFile_file' class='btn img-25 p-0 shadow-none m-0 bg-img QuestionFilelabel'>"
        + "    <img src='/imagemember/Iconly-Light-Image 2@2x.png' alt=''>"
        + " </label>"
        + " <div class='div-img boxPreview d-none'>"
        + "  <a onclick='deleteImg(this)' class='btn  p-0 shadow-none'>"
        + "       <img class='img-20' src='/imagemember/Iconly-Light-Close Square@2x.png' alt=''>"
        + "   </a>"
        + "  <a download class='btn  p-0 shadow-none download' href=''>"
        + "      <img class='img-25' src='/imagemember/download.svg' alt=''>"
        + "   </a>"
        + " </div>"
        + " </div>"
        + "<a onclick='deletechoice(this)' class='btn  p-0 shadow-none'>"
        + "<img class='img-25' src='/imagemember/Iconly-Light-Delete@2x.png' alt=''></a>"
        + "</div></div>")
        .insertAfter(loc);
    j++;
}

function deletechoice(a) {
    $($(a).closest(".optoin")).remove();
}

$('body').on('change', 'input[type=radio].custom-control-input', function () {

    var container = $(this).parent().parent().parent();
    $(container).find('input[type=radio]:checked').not(this).prop('checked', false);
});


function hideAlert() {
    $("#myalertContainer").addClass('d-none');
    $("#myalert").text("");
}

function DelectExam(id) {

    functionConfirm("آیا از حذف آزمون انتخاب شده مطمئن هستید؟",
        function yes() {

            hideAlert();
            $.ajax({
                url: '/Exam/DeleteExam?Id=' + id,
                type: 'POST',
                success: function (result) {
                    if (result.status == "Success") {
                        $("#myalertContainer").addClass('alert-success').focus();
                        var row = document.getElementById('row_' + id);
                        row.parentElement.removeChild(row);
                    } else {
                        $("#myalertContainer").addClass('alert-danger').focus();
                    }
                    $("#myalertContainer").removeClass('d-none').focus();
                    $("#myalert").append(result.message);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert("error");
                }
            });
        },
        function no() {

        });
}

function TurnOnOffExam(id) {
    hideAlert();
    $.ajax({
        url: '/Exam/TurnOnOffExam?Id=' + id,
        type: 'POST',
        success: function (result) {
            if (result.status == "Success") {
                $("#myalertContainer").addClass('alert-success').focus();

            } else {
                $("#myalertContainer").addClass('alert-danger').focus();
            }
            $("#myalertContainer").removeClass('d-none').focus();
            $("#myalert").append(result.message);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("error");
        }
    });

}
function functionConfirm(msg, yes, no) {
    var confirmBox = $("#MessageAlertExamDelete");
    confirmBox.find(".message").text(msg);
    confirmBox.find(".btn-yes,.btn-no").unbind().click(function () {
        confirmBox.hide();
    });
    confirmBox.find(".btn-yes").click(yes);
    confirmBox.find(".btn-no").click(no);

    confirmBox.show();
}

function ScoreAutoExam(id) {
    hideAlert();
    $.ajax({
        url: '/Exam/ScoreAutoExam?Id=' + id,
        type: 'POST',
        success: function (result) {
            if (result.status == "Success") {
                $("#myalertContainer").addClass('alert-success').focus();

            } else {
                $("#myalertContainer").addClass('alert-danger').focus();
            }
            $("#myalertContainer").removeClass('d-none').focus();
            $("#myalert").append(result.message);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("error");
        }
    });

}

function goBack() {
    window.history.back();
}