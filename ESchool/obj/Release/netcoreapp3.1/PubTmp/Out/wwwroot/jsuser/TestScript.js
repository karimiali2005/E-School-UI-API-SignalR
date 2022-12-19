function fileUpload() {

    var input = document.getElementById('file');
    var data = new FormData();
    


    data.append('file', input.files[0]);


    var xhrRequest = $.ajax({
        url: "/TestSendFile/StoreFile",
        data: data,
        processData: false,
        contentType: false,
        type: "POST",
        xhr: function() {
            var xhr = new window.XMLHttpRequest();
            xhr.upload.addEventListener("progress",
                function(evt) {
                    if (evt.lengthComputable) {
                        var progress = Math.round((evt.loaded / evt.total) * 100);
                        /*progressEle.width(progress + "%");
                        progressText.text(progress + "%");*/
                        var txtLoading = document.getElementById('txtLoading');
                        txtLoading.innerHTML = parseInt(progress) + "%" + ' ارسال شده است'
                    }
                    /*if (!videoEle.length) {
                        console.log("aborted");
                        xhrRequest.abort();
                    }*/
                },
                false);
            return xhr;
        },
        success: function(data) {
            if (data != undefined)
                alert("finish");
            else {
                alert("در اجرای برنامه خطایی رخ داده است");
            }

            hideLoader();
            input.value = '';
        },
        error: function(xhr, ajaxOptions, thrownError) {
            alert(thrownError);
        },
        timeout: 3600000
    });


}
 

