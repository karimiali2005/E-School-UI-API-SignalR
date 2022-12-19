var xhttp = null;

function fileUpload(size, addressDownload) {
    showLoader();
    var btnLoadingClose = showButtonCloseLoader();
    if (!Empty(btnLoadingClose)) {
        btnLoadingClose.addEventListener('click', function () {
            btnLoadingClose.classList.add('displayNone');
            hideLoader();
            if (xhttp != null) {
                xhttp = null;
                input.value = '';
            }
        });
    }
    var input = document.getElementById('file');
    var data = new FormData();
    var file = input.files[0];


    if (file.size < size * 1024 * 1024) {
        data.append('file', input.files[0]);
        data.append('tag', slcChatTag().value);

        xhttp = new XMLHttpRequest();

        xhttp.upload.addEventListener('progress', function (e) {
            var percent_complete = (e.loaded / e.total) * 100;
            var txtLoading = document.getElementById('txtLoading');
            txtLoading.innerHTML = parseInt(percent_complete) + "%" + ' ارسال شده است';
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
                        /*if (ws.readyState === WebSocket.CLOSED || ws.readyState === WebSocket.CLOSING) {
                            connect();
                            setTimeout(function () {
                                sendMsg(0, txtChatMsg.value, utype.value + ' ' + nameProfile.innerHTML, slcChatTag().value, '', 'C', 'All', result.filename, addressDownload);
                            }, 1000);
                        } else {*/
                            sendMsg(0, txtChatMsg.value, utype.value + ' ' + nameProfile.innerHTML, slcChatTag().value, '', 'C', 'All', result.filename, addressDownload);
                        /*}*/
                    } else {
                        alert(xhttp.responseText);
                    }
                }
                hideLoader();
                input.value = '';
            }
        };
        xhttp.onerror = function (err) {
            hideLoader();
            alert(err.message);
            input.value = '';
        };

       

        xhttp.open('POST', '/Chat/StoreFile');
        xhttp.timeout = 3600000; 
        xhttp.send(data);


        /*var xhrRequest = $.ajax({
            url: "/Chat/StoreFile",
            data: data,
            processData: false,
            contentType: false,
            type: "POST",
            xhr: function () {
                var xhr = new window.XMLHttpRequest();
                xhr.upload.addEventListener("progress", function (evt) {
                    if (evt.lengthComputable) {
                        var progress = Math.round((evt.loaded / evt.total) * 100);
                        /*progressEle.width(progress + "%");
                        progressText.text(progress + "%");#1#
                        var txtLoading = document.getElementById('txtLoading');
                        txtLoading.innerHTML = parseInt(progress) + "%" + ' ارسال شده است'
                    }
                    /*if (!videoEle.length) {
                        console.log("aborted");
                        xhrRequest.abort();
                    }#1#
                }, false);
                return xhr;
            },
            success: function (data) {
                if (data != undefined )
                    sendMsg(0, txtChatMsg.value, utype.value + ' ' + nameProfile.innerHTML, slcChatTag().value, '', 'C', 'All', data.filename, addressDownload);
               
                else {
                    alert("در اجرای برنامه خطایی رخ داده است");
                }
                    
                hideLoader();
                input.value = '';
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(thrownError);
            },
            timeout: 3600000
        });*/
    

    }
    else {
        alert("حجم فایل ارسالی بیش از  حد مجاز است \r حداکثر حجم مجاز " + size.toString());
        hideLoader();
        input.value = '';
    }

}

function getfile(filename, mimetype, addressDownload, tag) {
    if (Empty(filename))
        return '';
    if (tag === true)
        addressDownload += 'Learn/';
    else
        addressDownload += 'Normal/';

    var file = '<div class=\"file\" >';

    var mime_types_images = ['image/jpeg', 'image/png', 'image/gif'];
    var mime_types_audios = ['audio/wav', 'audio/mp3', 'audio/ogg', 'audio/mpeg'];
    var mime_types_videos = ['video/mp4', 'video/webm', 'video/ogg'];

    if (mimetype === "video/quicktime" && filename.split('.').pop().toLocaleLowerCase() === 'mov') {
        file += '<video controls><source src=\"' + addressDownload + filename + '\" type=\"video/mp4\"></video>';
    }
    else if (mime_types_images.indexOf(mimetype) >= 0) {
        file += '<a href=\"' + addressDownload + filename + '\">' + '<img src=\"' + addressDownload + filename + '\" />' + '</a>';
    }
    else if (mime_types_audios.indexOf(mimetype) >= 0) {
        file += '<audio controls><source src=\"' + addressDownload + filename + '\" type=\"' + mimetype + '\"></audio>';
    }
    else if (mime_types_videos.indexOf(mimetype) >= 0) {
        file += '<video controls><source src=\"' + addressDownload + filename + '\" type=\"' + mimetype + '\"></video>';
    } else {
        file += '<a href=\"' + addressDownload + filename + '\"><img class=\"icon\" src=\"/imagesuser/icons/folder.png\" /><span>' + filename + '</span></a>';
    }

    file += '</div>';
    return file;
}

/*

var mediaRecorder = null;

function startRecordA(elem) {

    navigator.mediaDevices.getUserMedia({ audio: true })
        .then(stream => {
            mediaRecorder = new MediaRecorder(stream);
            mediaRecorder.start();

            const audioChunks = [];
            mediaRecorder.addEventListener("dataavailable", event => {
                audioChunks.push(event.data);
            });

            mediaRecorder.addEventListener("stop", () => {
                const audioBlob = new Blob(audioChunks, { type: "audio/wav" });


                var audio = document.querySelector('#audioTag');
                audio.src = window.URL.createObjectURL(audioBlob);

                reader.readAsBinaryString(audioBlob)
            });

        });

    var imgStopRA = document.getElementById('imgStopRA');
    imgStopRA.classList.toggle('displayNone');
    elem.classList.toggle('displayNone');
}

function stopRecordA(elem) {
    mediaRecorder.stop();
    var imgStartRA = document.getElementById('imgStartRA');
    imgStartRA.classList.toggle('displayNone');
    elem.classList.toggle('displayNone');
}*/

/*var reader = new FileReader();
reader.onloadend = sendRecordAudio;

function sendRecordAudio(addressDownload) {

    var BinaryProp = window.btoa(reader.result);

    var form = new FormData();
    form.append('base64Str', BinaryProp);

    var request = new XMLHttpRequest();
    request.open('POST', '/Chat/SendRecordAudio');

    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (!Empty(request.responseText)) {
                if (IsJsonString(request.responseText)) {
                    var result = JSON.parse(request.responseText);
                    if (ws.readyState === WebSocket.CLOSED || ws.readyState === WebSocket.CLOSING) {
                        connect();
                        setTimeout(function () {
                            sendMsg(0, txtChatMsg.value, utype.value + ' ' + nameProfile.innerHTML, slcChatTag().value, '', 'C', 'All', result.filename, addressDownload);
                        }, 1000);
                    } else {
                        sendMsg(0, txtChatMsg.value, utype.value + ' ' + nameProfile.innerHTML, slcChatTag().value, '', 'C', 'All', result.filename, addressDownload);
                    }
                } else {
                    alert(xhttp.responseText);
                }
            }
            hideLoader();

        }
    };
    request.onerror = function (err) {
        alert(err.message);
    };
    showLoader();
    request.send(form);
}*/


//============================================

/*var audio = document.querySelector('audio');*/

function captureMicrophone(callback) {
    /*var btnReleaseMicrophone = document.querySelector('#btn-release-microphone');
    btnReleaseMicrophone.disabled = false;*/

    if (microphone) {
        callback(microphone);
        return;
    }

    if (typeof navigator.mediaDevices === 'undefined' || !navigator.mediaDevices.getUserMedia) {
        alert('This browser does not supports WebRTC getUserMedia API.');

        if (!!navigator.getUserMedia) {
            alert('This browser seems supporting deprecated getUserMedia API.');
        }
    }

    navigator.mediaDevices.getUserMedia({
        audio: isEdge
            ? true
            : {
                echoCancellation: false
            }
    }).then(function (mic) {
        callback(mic);
    }).catch(function (error) {
        alert('Unable to capture your microphone. Please check console logs.');
        console.error(error);
    });
}

/*function replaceAudio(src) {
    var newAudio = document.createElement('audio');
    newAudio.controls = true;
    newAudio.autoplay = true;

    if (src) {
        newAudio.src = src;
    }

    var parentNode = audio.parentNode;
    parentNode.innerHTML = '';
    parentNode.appendChild(newAudio);

    /*audio = newAudio;#1#
}*/

function stopRecordingCallback() {
    /*var btnStartRecording = document.getElementById('btn-start-recording');
    var btnReleaseMicrophone = document.querySelector('#btn-release-microphone');*/
    /*replaceAudio(URL.createObjectURL(recorder.getBlob()));*/

    /*btnStartRecording.disabled = false;*/

    /*
    setTimeout(function () {
        if (!audio.paused) return;

        setTimeout(function () {
            if (!audio.paused) return;
            audio.play();
        },
            1000);

        audio.play();
    },
        300);

    audio.play();
*/

    /*btnDownloadRecording.disabled = false;*/

    if (isSafari) {
        ReleaseMicrophone();
        /*click(btnReleaseMicrophone);*/
    }
}

var isEdge = navigator.userAgent.indexOf('Edge') !== -1 && (!!navigator.msSaveOrOpenBlob || !!navigator.msSaveBlob);
var isSafari = /^((?!chrome|android).)*safari/i.test(navigator.userAgent);

var recorder; // globally accessible
var microphone;

//    var btnStartRecording = document.getElementById('btn-start-recording');
//    var btnStopRecording = document.getElementById('btn-stop-recording');
/*var btnReleaseMicrophone = document.querySelector('#btn-release-microphone');*/
/*var btnDownloadRecording = document.getElementById('btn-download-recording');*/

function StartRecording(elem) {
    /*var btnStartRecording = document.getElementById('btn-start-recording');
    var btnStopRecording = document.getElementById('btn-stop-recording');
    elem.disabled = true;
    elem.style.border = '';
    elem.style.fontSize = '';*/

    if (!microphone) {
        captureMicrophone(function (mic) {
            microphone = mic;

            if (isSafari) {
                /*replaceAudio();*/

                /*audio.muted = true;
                audio.srcObject = microphone;*/

                /*btnStartRecording.disabled = false;
                btnStartRecording.style.border = '1px solid red';
                btnStartRecording.style.fontSize = '150%';*/

                /*alert('لطفا مجددا دکمه شروع بزنید. تلاش اول برای پیدا کردن میکروفون ناموفق بود.');*/
                StartRecording();

                return;
            }
            StartRecording();
            /*click(btnStartRecording);*/
        });
        var imgStopRA = document.getElementById('imgStopRA');
        imgStopRA.classList.toggle('displayNone');
        elem.classList.toggle('displayNone');
        return;
    }

    /*replaceAudio();*/

    /*audio.muted = true;
    audio.srcObject = microphone;*/

    var options = {
        type: 'audio',
        numberOfAudioChannels: isEdge ? 1 : 2,
        checkForInactiveTracks: true,
        bufferSize: 16384
    };

   // if (isSafari || isEdge) {
        options.recorderType = StereoAudioRecorder;
  //  }

    if (navigator.platform && navigator.platform.toString().toLowerCase().indexOf('win') === -1) {
        options.sampleRate = 48000; // or 44100 or remove this line for default
    }

   // if (isSafari) {
        options.sampleRate = 44100;
        options.bufferSize = 4096;
        options.numberOfAudioChannels = 2;
   // }

    if (recorder) {
        recorder.destroy();
        recorder = null;
    }

    recorder = RecordRTC(microphone, options);

    recorder.startRecording();

    /*btnStopRecording.disabled = false;
    btnDownloadRecording.disabled = true;*/
};

function StopRecording(elem, addressDownload) {
    /*elem.disabled = true;*/

    recorder.stopRecording(stopRecordingCallback);

    var imgStartRA = document.getElementById('imgStartRA');
    imgStartRA.classList.toggle('displayNone');
    elem.classList.toggle('displayNone');
    //  DownloadRecording();
    /*console.log("isStop");*/

    setTimeout(function () {
        DownloadRecording();
    }, 1000);
};

function ReleaseMicrophone() {
    /*var btnStartRecording = document.getElementById('btn-start-recording');*/
    /*elem.disabled = true;*/
    /*btnStartRecording.disabled = false;*/

    if (microphone) {
        microphone.stop();
        microphone = null;
    }

    if (recorder) {
        // click(btnStopRecording);
    }
};
/*
function DownloadRecording() {
*//*this.disabled = true;*//*
    console.log("isDownload")
   
    if (!recorder || !recorder.getBlob()) {
        console.log("is" +recorder)
        return
    };

    if (isSafari) {
        recorder.getDataURL(function (dataURL) {
            SaveToDisk(dataURL, getFileName('mp3'));
        });
        ReleaseMicrophone()
        return;
    }

    var blob = recorder.getBlob();
    var file = new File([blob],
        getFileName('mp3'),
        {
            type: 'audio/mp3'
        });
    invokeSaveAsDialog(file);
    
    console.log("isDownload2")
    console.log(file)
    ReleaseMicrophone()
};*/

function DownloadRecording(addressDownload) {
    /*console.log("isDownload")*/
    /*this.disabled = true;*/
    if (!recorder || !recorder.getBlob()) return;

    /* if (isSafari) {
         recorder.getDataURL(function (dataURL) {
             SaveToDisk(dataURL, getFileName('mp3'));
         });
         return;
     }*/

    var blob = recorder.getBlob();
    var file = new File([blob],
        getFileName('mp3'),
        {
            type: 'audio/mp3'
        });


    var form = new FormData();
    form.append('file', file);
    form.append('tag', slcChatTag().value);

    var request = new XMLHttpRequest();
    request.open('POST', '/Chat/SendRecordAudio');

    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (!Empty(request.responseText)) {
                if (IsJsonString(request.responseText)) {
                    var result = JSON.parse(request.responseText);
                 
                    /*if (ws.readyState === WebSocket.CLOSED || ws.readyState === WebSocket.CLOSING) {
                        connect();
                        setTimeout(function () {
                            sendMsg(0, txtChatMsg.value, utype.value + ' ' + nameProfile.innerHTML, slcChatTag().value, '', 'C', 'All', result.filename, addressDownload);
                        }, 1000);
                    } else {*/
                        sendMsg(0, txtChatMsg.value, utype.value + ' ' + nameProfile.innerHTML, slcChatTag().value, '', 'C', 'All', result.filename, addressDownload);
                    /*}*/
                } else {
                    alert(xhttp.responseText);
                }
            }
            hideLoader();

        }
    };
    request.onerror = function (err) {
        alert(err.message);
    };
    showLoader();
    request.send(form);
    ReleaseMicrophone();


};

/*function click(el) {
    el.disabled = false; // make sure that element is not disabled
    var evt = document.createEvent('Event');
    evt.initEvent('click', true, true);
    el.dispatchEvent(evt);
}*/

function getRandomString() {
    if (window.crypto && window.crypto.getRandomValues && navigator.userAgent.indexOf('Safari') === -1) {
        var a = window.crypto.getRandomValues(new Uint32Array(3)),
            token = '';
        for (var i = 0, l = a.length; i < l; i++) {
            token += a[i].toString(36);
        }
        return token;
    } else {
        return (Math.random() * new Date().getTime()).toString(36).replace(/\./g, '');
    }
}

function getFileName(fileExtension) {
    var d = new Date();
    var year = d.getFullYear();
    var month = d.getMonth();
    var date = d.getDate();
    return 'RecordRTC-' + year + month + date + '-' + getRandomString() + '.' + fileExtension;
}

function SaveToDisk(fileURL, fileName) {
    // for non-IE
    if (!window.ActiveXObject) {
        var save = document.createElement('a');
        save.href = fileURL;
        save.download = fileName || 'unknown';
        save.style = 'display:none;opacity:0;color:transparent;';
        (document.body || document.documentElement).appendChild(save);

        if (typeof save.click === 'function') {
            save.click();
        } else {
            save.target = '_blank';
            var event = document.createEvent('Event');
            event.initEvent('click', true, true);
            save.dispatchEvent(event);
        }

        (window.URL || window.webkitURL).revokeObjectURL(save.href);
    }
    // for IE
    else if (!!window.ActiveXObject && document.execCommand) {
        var _window = window.open(fileURL, '_blank');
        _window.document.close();
        _window.document.execCommand('SaveAs', true, fileName || fileURL)
        _window.close();
    }
}



