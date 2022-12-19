var adress = "";

function sendtextmessage(elm) {

    var template = $('#msg_normalText').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    template = $(template);
    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendSelftextmessage(elm) {
    var template = $('#msg_selfNormalText').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    template = $(template);
    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendtextmessageByreplay(elm) {

    var template = $('#msg_replyText').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //.................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);

    template = $(template);
    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendSelftextmessageByreplay(elm) {
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);

    template = $(template);
    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
//----------------------------------------------
function sendfileimg(elm) {

    var template = $('#msg_file_img').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //.................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    template = template.replace('%%MimeType%%', elm.mimeType);
    //...........................................
    template = template.replace('%%Filename%%', adress + elm.filename);

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendSelfFileimg(elm) {
    var text = "فایل";
    var template = $('#msg_selfFile_img').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //.................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    //...........................................
    template = template.replace('%%MimeType%%', elm.mimeType);
    template = template.replace('%%Filename%%', adress + elm.filename);

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendParentFileimg(elm) {
    var text = "فایل";
    var template = $('#msg_file_parentimg').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //.................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    //...........................................
    template = template.replace('%%MimeType%%', elm.mimeType);
    template = template.replace('%%Filename%%', adress + elm.filename);

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendParentSelfFileimg(elm) {
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    //.................
    template = template.replace('%%MimeType%%', elm.mimeType);
    template = template.replace('%%Filename%%', adress + elm.filename);
    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
//---------------------------------------------------
function sendFilevideo(elm) {

    var template = $('#msg_file_video').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    //.................
    template = template.replace('%%MimeType%%', elm.mimeType);
    template = template.replace('%%Filename%%', adress + elm.filename);

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendSelfFilevideo(elm) {

    var template = $('#msg_selfFile_video').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    //.................
    template = template.replace('%%MimeType%%', elm.mimeType);
    template = template.replace('%%Filename%%', adress + elm.filename);

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendParentFilevideo(elm) {

    var template = $('#msg_file_parentvideo').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    //.................
    template = template.replace('%%MimeType%%', elm.mimeType);
    template = template.replace('%%Filename%%', adress + elm.filename);

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendSelfParentFilevideo(elm) {

    var template = $('#msg_file_selfparentvideo').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    //.................
    template = template.replace('%%MimeType%%', elm.mimeType);
    template = template.replace('%%Filename%%', adress + elm.filename);

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
//---------------------------------------------------------
function sendFileaudio(elm) {

    var template = $('#msg_file_audio').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    //.................
    template = template.replace('%%MimeType%%', elm.mimeType);
    template = template.replace('%%Filename%%', adress + elm.filename);

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendSelfFileaudio(elm) {

    var template = $('#msg_selfFile_audio').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    //.................
    template = template.replace('%%MimeType%%', elm.mimeType);
    template = template.replace('%%Filename%%', adress + elm.filename);

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendParentFileaudio(elm) {

    var template = $('#msg_file_parentaudio').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    //.................
    template = template.replace('%%MimeType%%', elm.mimeType);
    template = template.replace('%%Filename%%', adress + elm.filename);

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
function sendSelfParentFileaudio(elm) {

    var template = $('#msg_file_selfparentaudio').text();
    var shortDateFormat = 'HH:mm';
    var date = elm.roomChatDateString;
    var tag = (elm.TagLearn) ? "محتوانی آموزشی" : "";
    template = template.replace('%%divid%%', elm.roomChatId);
    template = template.replace('%%nameSender%%', elm.senderName);
    template = template.replace('%%message%%', elm.textChat);
    template = template.replace('%%RoomChatViewNumber%%', elm.roomChatViewNumber);
    template = template.replace('%%RoomChatDate%%', date);
    template = template.replace('%%tag%%', tag);
    //......................
    template = template.replace('%%ParentSenderName%%', elm.parentSenderName);
    template = template.replace('%%ParentTextChat%%', elm.parentTextChat);
    template = template.replace('%%RoomChatParentID%%', elm.roomChatParentId);
    //.................
    template = template.replace('%%MimeType%%', elm.mimeType);
    template = template.replace('%%Filename%%', adress + elm.filename);

    template = $(template);

    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);

}
//##########################################################################
function ChatMessageUp(model, userId, mainAddress) {
    adress = mainAddress;
    /*var objModel = JSON.parse(model);   */
    var objModel = model;
    $.each(objModel, function (i, roomChat) {
        if (!roomChat.filename) {
            if (userId != roomChat.senderId) {
                if (!roomChat.roomChatParentId) {
                    sendtextmessage(roomChat);
                }
                else {
                    sendtextmessageByreplay(roomChat)
                }
            }
            else {
                if (!roomChat.roomChatParentId) {
                    sendSelftextmessage(roomChat)
                }
                else {
                    sendSelftextmessageByreplay(roomChat)
                }
            }
        }
        else {
            if (userId != roomChat.senderId) {
                if (!roomChat.roomChatParentId) {

                    if (roomChat.mimeType == "image/jpeg") {
                        sendfileimg(roomChat)
                    }
                    else if (roomChat.mimeType == "video/mp4" || roomChat.mimeType == "video/quicktime") {
                        sendFilevideo(roomChat)
                    }
                    else {
                        sendFileaudio(roomChat)
                    }
                }
                else {
                    if (roomChat.mimeType == "image/jpeg") {
                        sendParentFileimg(roomChat)
                    }
                    else if (roomChat.mimeType == "video/mp4" || roomChat.mimeType == "video/quicktime") {
                        sendParentFilevideo(roomChat)
                    }
                    else {
                        sendParentFileaudio(roomChat)
                    }
                }

            }
            else {
                if (!roomChat.roomChatParentId) {
                    if (roomChat.mimeType == "image/jpeg") {
                        sendSelfFileimg(roomChat)
                    }
                    else if (roomChat.mimeType == "video/mp4" || roomChat.mimeType == "video/quicktime") {
                        sendSelfFilevideo(roomChat)
                    }
                    else {
                        sendSelfFileaudio(roomChat)
                    }
                }
                else {
                    if (roomChat.mimeType == "image/jpeg") {
                        sendParentSelfFileimg(roomChat)
                    }
                    else if (roomChat.mimeType == "video/mp4" || roomChat.mimeType == "video/quicktime") {
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
function ChatMessageOneUp(roomChat, userId) {
    adress = roomChat.mainAddress;
    if (!roomChat.filename) {
        if (userId != roomChat.senderId) {
            if (!roomChat.roomChatParentId) {
                sendtextmessage(roomChat);
            }
            else {
                sendtextmessageByreplay(roomChat)
            }
        }
        else {
            if (!roomChat.roomChatParentId) {
                sendSelftextmessage(roomChat)
            }
            else {
                sendSelftextmessageByreplay(roomChat)
            }
        }
    }
    else {
        if (userId != roomChat.senderId) {
            if (!roomChat.roomChatParentId) {

                if (roomChat.mimeType == "image/jpeg") {
                    sendfileimg(roomChat)
                }
                else if (roomChat.mimeType == "video/mp4" || roomChat.mimeType == "video/quicktime") {
                    sendFilevideo(roomChat)
                }
                else {
                    sendFileaudio(roomChat)
                }
            }
            else {
                if (roomChat.mimeType == "image/jpeg") {
                    sendParentFileimg(roomChat)
                }
                else if (roomChat.mimeType == "video/mp4" || roomChat.mimeType == "video/quicktime") {
                    sendParentFilevideo(roomChat)
                }
                else {
                    sendParentFileaudio(roomChat)
                }
            }

        }
        else {
            if (!roomChat.roomChatParentId) {
                if (roomChat.mimeType == "image/jpeg") {
                    sendSelfFileimg(roomChat)
                }
                else if (roomChat.mimeType == "video/mp4" || roomChat.mimeType == "video/quicktime") {
                    sendSelfFilevideo(roomChat)
                }
                else {
                    sendSelfFileaudio(roomChat)
                }
            }
            else {
                if (roomChat.mimeType == "image/jpeg") {
                    sendParentSelfFileimg(roomChat)
                }
                else if (roomChat.mimeType == "video/mp4" || roomChat.mimeType == "video/quicktime") {
                    sendSelfParentFilevideo(roomChat)
                }
                else {
                    sendSelfParentFileaudio(roomChat)
                }
            }



        }




    }
}