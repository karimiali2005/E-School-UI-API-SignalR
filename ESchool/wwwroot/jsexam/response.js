//webkitURL is deprecated but nevertheless
URL = window.URL || window.webkitURL;

var gumStream; 						//stream from getUserMedia()
var rec; 							//Recorder.js object
var input; 							//MediaStreamAudioSourceNode we'll be recording

// shim for AudioContext when it's not avb. 
var AudioContext = window.AudioContext || window.webkitAudioContext;
var audioContext //audio context to help us record

var recordButton = document.getElementById("imgVoiceStart");
var stopButton = document.getElementById("imgVoiceStop");
//var pauseButton = document.getElementById("pauseButton");

//add events to those 2 buttons
recordButton.addEventListener("click", startRecording);
stopButton.addEventListener("click", stopRecording);
//pauseButton.addEventListener("click", pauseRecording);

function startRecording() {
    console.log("recordButton clicked");

	/*
		Simple constraints object, for more advanced audio features see
		https://addpipe.com/blog/audio-constraints-getusermedia/
	*/

    var constraints = { audio: true, video: false }

    /*
      Disable the record button until we get a success or fail from getUserMedia() 
  */
    recordButton.classList.toggle('displayNone');
    stopButton.classList.toggle('displayNone');

    //recordButton.disabled = true;
    //stopButton.disabled = false;
    //pauseButton.disabled = false

	/*
    	We're using the standard promise based getUserMedia() 
    	https://developer.mozilla.org/en-US/docs/Web/API/MediaDevices/getUserMedia
	*/

    navigator.mediaDevices.getUserMedia(constraints).then(function (stream) {
        console.log("getUserMedia() success, stream created, initializing Recorder.js ...");

		/*
			create an audio context after getUserMedia is called
			sampleRate might change after getUserMedia is called, like it does on macOS when recording through AirPods
			the sampleRate defaults to the one set in your OS for your playback device

		*/
        audioContext = new AudioContext();

        //update the format 
     //   document.getElementById("formats").innerHTML = "Format: 1 channel pcm @ " + audioContext.sampleRate / 1000 + "kHz"

        /*  assign to gumStream for later use  */
        gumStream = stream;

        /* use the stream */
        input = audioContext.createMediaStreamSource(stream);

		/* 
			Create the Recorder object and configure to record mono sound (1 channel)
			Recording 2 channels  will double the file size
		*/
        rec = new Recorder(input, { numChannels: 1 });

        //start the recording process
        rec.record();

        console.log("Recording started");

    }).catch(function (err) {
        //enable the record button if getUserMedia() fails
        recordButton.disabled = false;
        stopButton.disabled = true;
        //pauseButton.disabled = true
    });
}

//function pauseRecording() {
//    console.log("pauseButton clicked rec.recording=", rec.recording);
//    if (rec.recording) {
//        //pause
//        rec.stop();
//        pauseButton.innerHTML = "Resume";
//    } else {
//        //resume
//        rec.record()
//        pauseButton.innerHTML = "Pause";

//    }
//}

function stopRecording() {
    console.log("stopButton clicked");

    //disable the stop button, enable the record too allow for new recordings
    //stopButton.disabled = true;
    //recordButton.disabled = false;
    //pauseButton.disabled = true;
    recordButton.classList.toggle('displayNone');
    stopButton.classList.toggle('displayNone');

    //reset button just in case the recording is stopped while paused
    //pauseButton.innerHTML = "Pause";

    //tell the recorder to stop the recording
    rec.stop();

    //stop microphone access
    gumStream.getAudioTracks()[0].stop();

    //create the wav blob and pass it on to createDownloadLink
    rec.exportWAV(createDownloadLink);
}

function createDownloadLink(blob) {

    var url = URL.createObjectURL(blob);
    var au = document.createElement('audio');
 
    //au.id = "ResponseValue_Voice";
    //au.setAttribute("name", "ResponseValue_Voice");
    var li = document.createElement('li');
    var link = document.createElement('a');

    //name of .wav file to use during upload and download (without extendion)
    var filename = new Date().toISOString();

    //add controls to the <audio> element
    au.controls = true;
    au.src = url;

    //save to disk link
    link.href = url;
    link.download = filename + ".wav"; //download forces the browser to donwload the file using the  filename
    link.innerHTML = "دانلود کنید";

    //add the new audio element to li
    li.appendChild(au);

    //add the filename to the li
    li.appendChild(document.createTextNode(filename + ".wav "));

    //add the save to disk link to li
    li.appendChild(link); 
   
    recordingsList.appendChild(li);

    //upload file
    ShowLoader(true);
    var file = new File([blob],
        getFileName('wav'),
        {
            type: 'audio/wav'
        });

    var form = new FormData();
    form.append('ResponseValue_file', file);
    form.append('ResponseID', $("#ResponseID").val());
    form.append('ExamId', $("#ExamId").val());
    form.append('QuestionId', $("#QuestionId").val());
    form.append('QuestionTypeId', $("#QuestionTypeId").val());    
    form.append('NextQuestionId', $("#NextQuestionId").val());    
    form.append('ResponseQuestionID', $("#ResponseQuestionID").val());    

    
    var request = new XMLHttpRequest();

    request.upload.addEventListener('progress', function (e) {
        var percent_complete = (e.loaded / e.total) * 100;
        var spnLoadingUpload = document.getElementById('spnLoadingUpload');
        spnLoadingUpload.innerHTML = parseInt(percent_complete);
    });



    request.open('POST', '/Exam/SaveVoiceResponse');

    request.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            if (IsJsonString(request.responseText)) {
                
                var result = JSON.parse(request.responseText);
                 if (result.url === "" && result.ErrorMsg !== "") {
                     $("#myalertContainer").removeClass('d-none');
                     $("#myalert").text(result.ErrorMsg);
                 }
                 //if (result.url !== "") {
                 //    window.location.href = result.url;
                 //}
            }
            else {
                    alert(request.responseText);
                }
                /*alert("salam");*/

            }
            HideLoader();
            input.value = '';

        }
    
    request.onerror = function (err) {
        alert(err.message);
        HideLoader();
    };

    request.addEventListener('load', function (e) {
    });

    request.send(form);
    
}

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

function getFileName(fileExtension) {
    var d = new Date();
    var year = d.getFullYear();
    var month = d.getMonth();
    var date = d.getDate();
    return 'RecordRTC-' + year + month + date + '-' + getRandomString() + '.' + fileExtension;
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

function IsJsonString(str) {
    try {
        JSON.parse(str);
    }
    catch (e) {
        return false;
    }
    return true;
}