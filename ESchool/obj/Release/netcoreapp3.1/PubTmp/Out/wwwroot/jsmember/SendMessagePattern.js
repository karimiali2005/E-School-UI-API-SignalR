var adress = "";

function sendtextmessage(elm) {
    
    var template = $('#msg_normalText').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%',tag);
    template = $(template);
  //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendSelftextmessage(elm) {   
    var template = $('#msg_selfNormalText').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    template = $(template);
    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendtextmessageByreplay(elm) {
   
    var template = $('#msg_replyText').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //.................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);

    template = $(template);
  //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendSelftextmessageByreplay(elm) {
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);

    template = $(template);
  //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
//----------------------------------------------
function sendfileimg(elm) {
  
    var template = $('#msg_file_img').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //.................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    template = template.replace('%%MimeType%%', elm.MimeType);    
    //...........................................
    template = template.replace('%%Filename%%', adress + elm.Filename);  

    template = $(template);
    
    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendSelfFileimg(elm) {
    var text = "فایل";
    var template = $('#msg_selfFile_img').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //.................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    //...........................................
    template = template.replace('%%MimeType%%', elm.MimeType);
    template = template.replace('%%Filename%%', adress + elm.Filename);  

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendParentFileimg(elm) {
    var text = "فایل";
    var template = $('#msg_file_parentimg').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //.................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    //...........................................
    template = template.replace('%%MimeType%%', elm.MimeType);
    template = template.replace('%%Filename%%', adress + elm.Filename);  

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendParentSelfFileimg(elm) {
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    //.................
    template = template.replace('%%MimeType%%', elm.MimeType);
    template = template.replace('%%Filename%%', adress + elm.Filename); 
    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
//---------------------------------------------------
function sendFilevideo(elm) {
    
    var template = $('#msg_file_video').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    //.................
    template = template.replace('%%MimeType%%', elm.MimeType);
    template = template.replace('%%Filename%%', adress + elm.Filename); 

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendSelfFilevideo(elm) {
   
    var template = $('#msg_selfFile_video').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    //.................
    template = template.replace('%%MimeType%%', elm.MimeType);
    template = template.replace('%%Filename%%', adress + elm.Filename); 

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendParentFilevideo(elm) {
   
    var template = $('#msg_file_parentvideo').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    //.................
    template = template.replace('%%MimeType%%', elm.MimeType);
    template = template.replace('%%Filename%%', adress + elm.Filename); 

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendSelfParentFilevideo(elm) {
  
    var template = $('#msg_file_selfparentvideo').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    //.................
    template = template.replace('%%MimeType%%', elm.MimeType);
    template = template.replace('%%Filename%%', adress + elm.Filename); 

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
//---------------------------------------------------------
function sendFileaudio(elm) {
    
    var template = $('#msg_file_audio').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    //.................
    template = template.replace('%%MimeType%%', elm.MimeType);
    template = template.replace('%%Filename%%', adress + elm.Filename); 

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendSelfFileaudio(elm) {
   
    var template = $('#msg_selfFile_audio').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    //.................
    template = template.replace('%%MimeType%%', elm.MimeType);
    template = template.replace('%%Filename%%', adress + elm.Filename); 

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendParentFileaudio(elm) {
    
    var template = $('#msg_file_parentaudio').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    //.................
    template = template.replace('%%MimeType%%', elm.MimeType);
    template = template.replace('%%Filename%%', adress + elm.Filename); 

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
function sendSelfParentFileaudio(elm) {
  
    var template = $('#msg_file_selfparentaudio').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.RoomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.RoomChatID);
    template = template.replace('%%nameSender%%', elm.SenderName);
    template = template.replace('%%message%%', elm.TextChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.RoomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.ParentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.ParentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.RoomChatParentID);
    //.................
    template = template.replace('%%MimeType%%', elm.MimeType);
    template = template.replace('%%Filename%%', adress + elm.Filename); 

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#showchat').prepend(template);

}
//##########################################################################
function ChatMessage(model, userId, mainAddress) {
    adress = mainAddress;

    /*var objModel = JSON.parse(model);   */
    var objModel = model;   
    $.each(objModel, function (i, roomChat) {
        if (!roomChat.Filename) {
            if (userId != roomChat.SenderID) {
                if (!roomChat.RoomChatParentID) {
                    sendtextmessage(roomChat);
                }
                else {
                    sendtextmessageByreplay(roomChat)
                }
            }
            else {
                if (!roomChat.RoomChatParentID) {
                    sendSelftextmessage(roomChat)
                }
                else {
                    sendSelftextmessageByreplay(roomChat)
                }
            }
        }
        else {
            if (userId != roomChat.SenderID) {
                if (!roomChat.RoomChatParentID) {

                    if (roomChat.MimeType == "image/jpeg") {
                        sendfileimg(roomChat)
                    }
                    else if (roomChat.MimeType == "video/mp4" || roomChat.MimeType == "video/quicktime") {
                        sendFilevideo(roomChat)
                    }
                    else {
                        sendFileaudio(roomChat)
                    }
                }
                else {
                    if (roomChat.MimeType == "image/jpeg") {
                        sendParentFileimg(roomChat)
                    }
                    else if (roomChat.MimeType == "video/mp4" || roomChat.MimeType == "video/quicktime") {
                        sendParentFilevideo(roomChat)
                    }
                    else {
                        sendParentFileaudio(roomChat)
                    }
                }

            }
            else {
                if (!roomChat.RoomChatParentID) {
                    if (roomChat.MimeType == "image/jpeg") {
                        sendSelfFileimg(roomChat)
                    }
                    else if (roomChat.MimeType == "video/mp4" || roomChat.MimeType == "video/quicktime") {
                        sendSelfFilevideo(roomChat)
                    }
                    else {
                        sendSelfFileaudio(roomChat)
                    }
                }
                else {
                    if (roomChat.MimeType == "image/jpeg") {
                        sendParentSelfFileimg(roomChat)
                    }
                    else if (roomChat.MimeType == "video/mp4" || roomChat.MimeType == "video/quicktime") {
                        sendSelfParentFilevideo(roomChat)
                    }
                    else {
                        sendSelfParentFileaudio(roomChat)
                    }
                }



            }




        }
    });
}
function ChatMessageOne(roomChat, userId, mainAddress) {
    adress = mainAddress;
    /*var objModel = JSON.parse(model);   */
    if (!roomChat.Filename) {
        if (userId != roomChat.SenderID) {
            if (!roomChat.RoomChatParentID) {
                sendtextmessage(roomChat);
            }
            else {
                sendtextmessageByreplay(roomChat)
            }
        }
        else {
            if (!roomChat.RoomChatParentID) {
                sendSelftextmessage(roomChat)
            }
            else {
                sendSelftextmessageByreplay(roomChat)
            }
        }
    }
    else {
        if (userId != roomChat.SenderID) {
            if (!roomChat.RoomChatParentID) {

                if (roomChat.MimeType == "image/jpeg") {
                    sendfileimg(roomChat)
                }
                else if (roomChat.MimeType == "video/mp4" || roomChat.MimeType == "video/quicktime") {
                    sendFilevideo(roomChat)
                }
                else {
                    sendFileaudio(roomChat)
                }
            }
            else {
                if (roomChat.MimeType == "image/jpeg") {
                    sendParentFileimg(roomChat)
                }
                else if (roomChat.MimeType == "video/mp4" || roomChat.MimeType == "video/quicktime") {
                    sendParentFilevideo(roomChat)
                }
                else {
                    sendParentFileaudio(roomChat)
                }
            }

        }
        else {
            if (!roomChat.RoomChatParentID) {
                if (roomChat.MimeType == "image/jpeg") {
                    sendSelfFileimg(roomChat)
                }
                else if (roomChat.MimeType == "video/mp4" || roomChat.MimeType == "video/quicktime") {
                    sendSelfFilevideo(roomChat)
                }
                else {
                    sendSelfFileaudio(roomChat)
                }
            }
            else {
                if (roomChat.MimeType == "image/jpeg") {
                    sendParentSelfFileimg(roomChat)
                }
                else if (roomChat.MimeType == "video/mp4" || roomChat.MimeType == "video/quicktime") {
                    sendSelfParentFilevideo(roomChat)
                }
                else {
                    sendSelfParentFileaudio(roomChat)
                }
            }



        }




    }
}