$(function () {
    var faid = document.getElementById("faid").value;
    var addressDownload = document.getElementById("adressfile").value; 
    var size = document.getElementById("sizeuploadhomework").value;
  /*
   * For the sake keeping the code clean and the examples simple this file
   * contains only the plugin configuration & callbacks.
   * 
   * UI functions ui_* can be located in: demo-ui.js
   */
    
  var uploder= $('#drag-and-drop-zone').dmUploader({ //
      url: '/HomeWorkStuden/StoreFile',       
      maxFileSize: size * 1024 * 1024, // 3 Megs 
    onDragEnter: function(){
      // Happens when dragging something over the DnD area
      this.addClass('active');
    },
    onDragLeave: function(){
      // Happens when dragging something OUT of the DnD area
      this.removeClass('active');
    },
    onInit: function(){
      // Plugin is ready to use
      ui_add_log('Penguin initialized :)', 'info');
    },
    onComplete: function(){
      // All files in the queue are processed (success or error)
      ui_add_log('All pending tranfers finished');
    },
    onNewFile: function(id, file){
      // When a new file is added using the file selector or the DnD area
      ui_add_log('فایل جدید اضافه شد #' + id);
      ui_multi_add_file(id, file);
    },
    onBeforeUpload: function(id){
      // about tho start uploading a file
      ui_add_log('شروع اپلود #' + id);
        ui_multi_update_file_status(id, 'uploading', 'در حال بار گزاری...');
      ui_multi_update_file_progress(id, 0, '', true);
    },
    onUploadCanceled: function(id) {
      // Happens when a file is directly canceled by the user.
        ui_multi_update_file_status(id, 'warning', 'توسط کاربر کنسل گردید ');
      ui_multi_update_file_progress(id, 0, 'warning', false);
    },
    onUploadProgress: function(id, percent){
      // Updating file progress
      ui_multi_update_file_progress(id, percent);
    },
    onUploadSuccess: function(id, data){
        // A file was successfully uploaded    
        creatnamefile(data,id)
       
      
    },
    onUploadError: function(id, xhr, status, message){
        ui_multi_update_file_status(id, 'danger', message);
      ui_multi_update_file_progress(id, 0, 'danger', false);  
    },
    onFallbackMode: function(){
      // When the browser doesn't support this plugin :(
      ui_add_log('Plugin cant be used here, running Fallback callback', 'danger');
    },
    onFileSizeError: function(file){
        ui_add_log('فایا \'' + file.name + '\' امکان ارسال به دلیل حجم بالا وجود ندارد ', 'danger');
        alert('امکان ارسال به دلیل حجم بالا وجود ندارد');
    }
  });


    function creatnamefile(data, id) {
        ui_add_log('اطلاعات بازگشتی #' + id + ': ' + JSON.stringify(data.value.path));

        $.ajax({
            method: "POST",
            url: "/HomeWorkStuden/addresponsefileHomeworh",
            data: { homeworkanswerId: faid, filenam: data.value.path }
        })
            .done(function (msg) {
                if (data.value.mimtype != null) {
                    console.log(data.value.mimtype);
                    var file = getfile(data.value.path, data.value.mimtype, "/");
                    ui_add_log('فایل  #' + id + 'با موفقیت ارسال گردید', 'success');
                    ui_multi_update_file_status(id, 'success', 'آپلود شد', msg.value.id, file);
                    ui_multi_update_file_progress(id, 100, 'success', false);
                }
            });

    }
    function getfile(filename, mimetype) {
       
        console.log(filename);
        console.log(mimetype);
        if (Empty(filename))
            return '';
       
            addressDownload += 'Homework/';
       
        var style = "style=\" width: 20px; height: 20px;\"";
        var file = '<div class=\"file\" >';

        //var mime_types_images = ['image/jpeg', 'image/png', 'image/gif'];
        //var mime_types_audios = ['audio/wav', 'audio/mp3', 'audio/ogg', 'audio/mpeg'];
        //var mime_types_videos = ['video/mp4', 'video/webm', 'video/ogg'];

        //if (mimetype === "video/quicktime" && filename.split('.').pop().toLocaleLowerCase() === 'mov') {
        //    file += '<video controls><source src=\"' + addressDownload + filename + '\" type=\"video/mp4\"></video>';
        //}
        //else if (mime_types_images.indexOf(mimetype) >= 0) {
        //    file += '<a href=\"' + addressDownload + filename + '\">' + '<img src=\"' + addressDownload + filename + '\"   />' + '</a>';
        //}
        //else if (mime_types_audios.indexOf(mimetype) >= 0) {
        //    file += '<audio controls><source src=\"' + addressDownload + filename + '\" type=\"' + mimetype + '\"></audio>';
        //}
        //else if (mime_types_videos.indexOf(mimetype) >= 0) {
        //    file += '<video controls><source src=\"' + addressDownload + filename + '\" type=\"' + mimetype + '\"></video>';
        //} else {
        //    file += '<a href=\"' + addressDownload + filename + '\"><img class=\"icon\" src=\"/imagesuser/icons/folder.png\" /><span>' + filename + '</span></a>';
        //}
        file += "<a  target='_blank' href=\"" + addressDownload + filename + "\" class=\"dl-file-user btn btn-info d-flex align-items-start\" ><img class=\"icon\" src=\"/imagemember/down-arrow.svg\"  " + style + "/></a>";

         file += '</div>';
      
        console.log(file);
        return file;
    }

    
});

function Empty(val) {
    return val == '' || val == undefined || val == null;
}
function removeFileUpload(elm) {
    event.preventDefault();
    let idaf = $(elm).data("faid");
    var faid = $("#faid").val()  
    $.ajax({
        method: "POST",
        url: "/HomeWorkStuden/deletefileHomeworh",
        data: { idanswerfile: idaf, idhomeworkAnswer: faid }
    })
        .done(function (msg) {
            console.log("DeleteSucsess")
        });
    var p = $(elm).closest('li');
    $(p).remove();

}

//==============================================ForVoice==========================

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



function StartRecording(elem) {
 
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

   
};

function StopRecording(elem, addressDownload) {
   
    recorder.stopRecording(stopRecordingCallback);

    var imgStartRA = document.getElementById('imgStartRA');
    imgStartRA.classList.toggle('displayNone');
    elem.classList.toggle('displayNone');


    setTimeout(function () {
        DownloadRecording();
    }, 1000);
};

function ReleaseMicrophone() {


    if (microphone) {
        microphone.stop();
        microphone = null;
    }

    if (recorder) {
        // click(btnStopRecording);
    }
};


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
//



    var form = new FormData();
   form.append('file', file);
  
    let id = guidGenerator();
    console.log(id)
    ui_multi_add_file(id, file);
    ui_multi_update_file_progress(id, 25, '', true);
    var request = new XMLHttpRequest();
    request.open('POST', '/HomeWorkStuden/StoreFile');

    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (!Empty(request.responseText)) {
               
                var result = JSON.parse(request.responseText);      
                ui_multi_update_file_progress(id, 75, '', true);
                creatnamefileVoice(result, id)
                    
               
            }
           // hideLoader();

        }
    };
    request.onerror = function (err) {
        alert(err.message);
    };
   
    request.send(form);
    ui_multi_update_file_progress(id, 75, '', true);
    ReleaseMicrophone();


};
function creatnamefileVoice(data, id) {
    ui_add_log('اطلاعات بازگشتی #' + id + ': ' + JSON.stringify(data.value.path));
    var faid = document.getElementById("faid").value;
    $.ajax({
        method: "POST",
        url: "/HomeWorkStuden/addresponsefileHomeworh",
        data: { homeworkanswerId: faid, filenam: data.value.path }
    })
        .done(function (msg) {
            if (data.value.mimtype != null) {
                console.log(data.value.mimtype);
                var file = getfileVoice(data.value.path, data.value.mimtype, "/");
                ui_add_log('فایل  #' + id + 'آپلود شد', 'success');
                ui_multi_update_file_status(id, 'success', 'با موفقیت اپلود گردید', msg.value.id, file);
                ui_multi_update_file_progress(id, 100, 'success', false);
            }
        });

}
function guidGenerator() {
    var S4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    };
    return (S4() + S4() +  S4()+ S4()+ S4());
}

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
var addressDownload = document.getElementById("adressfile").value; 
function getfileVoice(filename, mimetype) {
    
    console.log(filename);
    console.log(mimetype);
    if (Empty(filename))
        return '';

    addressDownload += 'Homework/';

    var style = "style=\" width: 100px; height: 100px;\"";
    var file = ' <div class="mb-2"><div class=\"file\" >';
   

    var mime_types_images = ['image/jpeg', 'image/png', 'image/gif'];
    var mime_types_audios = ['audio/wav', 'audio/mp3', 'audio/ogg', 'audio/mpeg', "audio/mp4", "audio/aac", 'video/ogg'];
    var mime_types_videos = ['video/mp4', 'video/webm', "video/quicktime"];

    if (mimetype === "video/quicktime" && filename.split('.').pop().toLocaleLowerCase() === 'mov') {
        file += '<video controls><source src=\"' + addressDownload + filename + '\" type=\"video/mp4\"></video>';
    }
    else if (mime_types_images.indexOf(mimetype) >= 0) {
        file += '<a  target="_blank" href=\"' + addressDownload + filename + '\">' + '<img src=\"' + addressDownload + filename + '\"  ' + style + ' />' + '</a>';
    }
    else if (mime_types_audios.indexOf(mimetype) >= 0) {
        file += '<audio controls><source src=\"' + addressDownload + filename + '\" type=\"' + mimetype + '\"></audio>';
    }
    else if (mime_types_videos.indexOf(mimetype) >= 0) {
        file += '<video controls><source src=\"' + addressDownload + filename + '\" type=\"' + mimetype + '\"></video>';
    } else {
        file += '<a  target="_blank" href=\"' + addressDownload + filename + '\"><img class=\"icon\" src=\"/imagemember/404File.png\" /></a>';
    }

    file += '</div></div>';

    console.log(file);
    return file;
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



