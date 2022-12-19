function ReportCardDetailShow(roomChatGroupID, elem) {
   // ShowLoader();
    var courseId = elem.value;
   


    $.ajax({
        url: "/ReportCardTeacher/ReportCardDetail",
        type: 'Get',
        data: {
            roomChatGroupID: roomChatGroupID,
            courseId: courseId
        },

        success: function (response) {

            if (response !== null && response !== undefined) {


                $("#div-reportCardDetail").html("");
                /*var temp = $("#divChatScreen").height();*/
                $("#div-reportCardDetail").html($.parseHTML(response, document, true));




            }
           // HideLoader();
            //  loadNewPage();
        },
        error: function (xhr, ajaxOptions, thrownError) {
          //  HideLoader();
        }
    });



}
function ReportCardTeacherScoreSave(reportCardId, roomChatGroupId, courseId, isDescriptives) {
    $("#btn1").attr("disabled", true);
    $("#btn2").attr("disabled", true);
    var listreportCardDetails = new Array();
    var reportCardDetailTemp = {};
    var countor = 0;
    $(".table tbody tr ").each(function () {

       


       
        var row = $(this);
        var reportCardDetail = {};
        
        //var row2 = row[0].getElementsByClassName("d-md-table-row");
        //alert(row.id)



        if (window.innerWidth > 768 && row[0].classList.contains("rowBig") === true) {
            var ids = row[0].id.substring(2, row[0].id.length).split('_');

            reportCardDetail.reportCardDetailId = parseInt(ids[0]);
            reportCardDetail.userId = parseInt(ids[1]);

            if (isDescriptives === "True") {
                var combo = row[0].getElementsByClassName("selectDescriptives");
                if (combo[0].value !== "")
                    reportCardDetail.reportCardDescriptiveId = parseInt(combo[0].value);
            }
            else {
                var input = row[0].getElementsByClassName("inputReportCardScore");
                if (input[0].value !== "")
                    reportCardDetail.reportCardScore = parseFloat(input[0].value);
            }
            var input = row[0].getElementsByClassName("txtAreaReportCardScore");
            if (input[0].value !== "")
                reportCardDetail.reportCardTeacherComment = input[0].value;

            reportCardDetail.reportCardId = parseInt(reportCardId);
            reportCardDetail.roomChatGroupId = parseInt(roomChatGroupId);
            reportCardDetail.courseId = parseInt(courseId);

            listreportCardDetails.push(reportCardDetail);
        }
        if (window.innerWidth <= 768 && row[0].classList.contains("rowSmall") === true) {

            if (countor === 0) {
                reportCardDetail = {};

                var ids = row[0].id.substring(2, row[0].id.length).split('_');

                reportCardDetail.reportCardDetailId = parseInt(ids[0]);
                reportCardDetail.userId = parseInt(ids[1]);

                reportCardDetailTemp = reportCardDetail;

            }
            if (countor === 1) {

                reportCardDetail = reportCardDetailTemp;

                if (isDescriptives === "True") {
                    var combo = row[0].getElementsByClassName("selectDescriptives");
                    if (combo[0].value !== "")
                        reportCardDetail.reportCardDescriptiveId = parseInt(combo[0].value);
                }
                else {
                    var input = row[0].getElementsByClassName("inputReportCardScore");
                    if (input[0].value !== "")
                        reportCardDetail.reportCardScore = parseFloat(input[0].value);
                }
                reportCardDetailTemp = reportCardDetail;
            }
            if (countor === 2) {

                reportCardDetail = reportCardDetailTemp;

                var input = row[0].getElementsByClassName("txtAreaReportCardScore");
                if (input[0].value !== "")
                    reportCardDetail.reportCardTeacherComment = input[0].value;


                reportCardDetail.reportCardId = parseInt(reportCardId);
                reportCardDetail.roomChatGroupId = parseInt(roomChatGroupId);
                reportCardDetail.courseId = parseInt(courseId);

                listreportCardDetails.push(reportCardDetail);
                countor = 0;
                reportCardDetailTemp = reportCardDetail;
            }
            else
                countor++;
        }

    });
   

    $.ajax({
        url: "/ReportCardTeacher/ReportCardTeacherScoreSave",
        type: 'post',
        //data: JSON.stringify({
        //    listreportCardDetails: listreportCardDetail

        //}),
        data: JSON.stringify(listreportCardDetails),
        //{
        //    reportCardId: reportCardId,
        //    roomChatGroupID: roomChatGroupID,
        //    courseId: courseId,
         //  
        //},
       contentType: 'application/json; charset=utf-8',
        success: function (response) {

            if (response !== null && response !== undefined) {
                //alert("ثبت شد");
                //location.reload();
                $("#btn1").attr("disabled", false);
                $("#btn2").attr("disabled", false);
                window.history.back();

            }
            // HideLoader();
            //  loadNewPage();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //  HideLoader();
        }
    });
}

function ReportCardParentDetailShow(elem) {
    // ShowLoader();
    var userId = elem.value;



    $.ajax({
        url: "/ReportCardParent/ReportCardParentDetailShow",
        type: 'Get',
        data: {
            id: userId
        },

        success: function (response) {

            if (response !== null && response !== undefined) {


                $("#div-reportCardParentDetail").html("");
                /*var temp = $("#divChatScreen").height();*/
                $("#div-reportCardParentDetail").html($.parseHTML(response, document, true));




            }
            // HideLoader();
            //  loadNewPage();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //  HideLoader();
        }
    });



}

////File
function ShowLoader(showPercent = false) {
    var divLoadingUpload = document.getElementById('div-loading-upload');
    var divPercentLoading = document.getElementById('divPercentLoading');
    var divTextLoading = document.getElementById('divTextLoading');
    var btnLoadingUploadClose = document.getElementById('btnLoadingUploadClose');

    if (divLoadingUpload.classList.contains("displayNone") === true)
        divLoadingUpload.classList.remove('displayNone');
    if (showPercent === false) {
        if (divPercentLoading.classList.contains("displayNone") === false)
            divPercentLoading.classList.add('displayNone');
        if (btnLoadingUploadClose.classList.contains("displayNone") === false)
            btnLoadingUploadClose.classList.add('displayNone');
        divTextLoading.innerText = "";
    } else {
        if (divPercentLoading.classList.contains("displayNone") === true)
            divPercentLoading.classList.remove('displayNone');
        if (btnLoadingUploadClose.classList.contains("displayNone") === true)
            btnLoadingUploadClose.classList.remove('displayNone');
        divTextLoading.innerText = "ارسال شده است";
    }

}
function HideLoader() {
    var divLoadingUpload = document.getElementById('div-loading-upload');
    divLoadingUpload.classList.add('displayNone');
}

function ShowLoadingUploadClose() {
    var btnLoadingUploadClose = document.getElementById('btnLoadingUploadClose');
    btnLoadingUploadClose.classList.remove('displayNone');
    return btnLoadingUploadClose;
}

function Empty(val) {
    return val === '' || val === undefined || val === null;
}

function IsJsonString(str) {
    try {
        JSON.parse(str);
    }
    catch (e) {
        return false;
    }
    return true;
}


function ReportCardSendFile(size, id, userId, reportCardId) {
    ShowLoader(true);
    var btnLoadingUploadClose = ShowLoadingUploadClose();
    if (!Empty(btnLoadingUploadClose)) {
        btnLoadingUploadClose.addEventListener('click', function () {
            btnLoadingUploadClose.classList.add('displayNone');
            HideLoader();
            if (xhttp != null) {
                xhttp = null;
                input.value = '';
            }
        });
    }

    var nameFile ='file'

    if (window.innerWidth > 768) {
        nameFile = nameFile + userId.toString();
    }
    else {
        nameFile = nameFile+'Small' + userId.toString()
    }

    var input = document.getElementById(nameFile );
    var data = new FormData();
    var file = input.files[0];


    if (file.size < size * 1024 * 1024) {
        data.append('file', input.files[0]);
        data.append('id', id);
        data.append('userId', userId);
        data.append('reportCardId', reportCardId);
      

        xhttp = new XMLHttpRequest();

        xhttp.upload.addEventListener('progress', function (e) {
            var percent_complete = (e.loaded / e.total) * 100;
            var spnLoadingUpload = document.getElementById('spnLoadingUpload');
            spnLoadingUpload.innerHTML = parseInt(percent_complete);
        });
        xhttp.addEventListener('load', function (e) {
            //console.log(xhttp.status);
            //console.log(xhttp.response);
        });
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                if (!Empty(xhttp.responseText)) {
                    if (IsJsonString(xhttp.responseText)) {
                        var result = JSON.parse(xhttp.responseText);
                        location.reload();

                    } else {
                        alert(xhttp.responseText);
                    }
                    /*alert("salam");*/

                }
                HideLoader();
                input.value = '';
            }
        };
        xhttp.onerror = function (err) {
            HideLoader();
            alert(err.message);
            input.value = '';
        };
        xhttp.open('POST', '/ReportCardTeacher/ReportCardSendFile');
        xhttp.send(data);

    }
    else {
        alert("حجم فایل ارسالی بیش از  حد مجاز است \r حداکثر حجم مجاز " + size.toString());
        HideLoader();
        input.value = '';
    }

}