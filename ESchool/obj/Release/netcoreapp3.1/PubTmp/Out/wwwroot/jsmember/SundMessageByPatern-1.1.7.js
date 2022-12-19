var mime_types_images = ['image/jpeg', 'image/png', 'image/gif'];
var mime_types_audios = ['audio/wav', 'audio/mp3', 'audio/ogg', 'audio/mpeg', "audio/mp4", "audio/aac", 'video/ogg'];
var mime_types_videos = ['video/mp4', 'video/webm', "video/quicktime"];
//function ChatMessageUp(model, userId, mainAddress) {
    
//    /*var objModel = JSON.parse(model);   */
//    var objModel = model;
//    $.each(objModel, function (i, roomChat) {
//        if (!roomChat.filename) {
//            if (userId != roomChat.senderId) {
//                if (!roomChat.roomChatParentId) {
//                    addmessageToViewModel(roomChat,1);
//                }
//                else {
//                    addmessageToViewModel(roomChat,2)
//                }
//            }
//            else {
//                if (!roomChat.roomChatParentId) {
//                    addmessageToViewModel(roomChat,3)
//                }
//                else {
//                    addmessageToViewModel(roomChat,4)
//                }
//            }
//        }
//        else {

//            if (userId != roomChat.senderId) {
//                if (!roomChat.roomChatParentId) {
//                    if (mime_types_images.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 1);
//                    }
//                    else if (mime_types_videos.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 5);
//                    }
//                    else if (mime_types_audios.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 9);
//                    }
//                    else {
//                        addmessageToViewModelFile(roomChat, 13);
//                    }
//                }
//                else {
//                    if (mime_types_images.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 3);
//                    }
//                    else if (mime_types_videos.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 7);
//                    }
//                    else if (mime_types_audios.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 11);
//                    }
//                    else {
//                        addmessageToViewModelFile(roomChat, 15);
//                    }
//                }

//            }
//            else {
//                if (!roomChat.roomChatParentId) {

//                    if (mime_types_images.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 2);
//                    }
//                    else if (mime_types_videos.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 6);
//                    }
//                    else if (mime_types_audios.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 10);
//                    }
//                    else {
//                        addmessageToViewModelFile(roomChat, 14);
//                    }
//                }
//                else {
//                    if (mime_types_images.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 4);
//                    }
//                    else if (mime_types_videos.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 8);
//                    }
//                    else if (mime_types_audios.indexOf(roomChat.mimeType) >= 0) {
//                        addmessageToViewModelFile(roomChat, 12);
//                    }
//                    else {
//                        addmessageToViewModelFile(roomChat, 16);
//                    }
//                }

//            }
//        }
//    });
//}
function ChatMessageOneUp(roomChat, userId, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd=true) {
    if (!roomChat.filename) {
        if (userId != roomChat.senderId) {
            if (!roomChat.roomChatParentId) {
                addmessageToViewModel(roomChat, 1, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
            }
            else {
                addmessageToViewModel(roomChat, 2, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd)
            }
        }
        else {
            if (!roomChat.roomChatParentId) {
                addmessageToViewModel(roomChat, 3, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd)
            }
            else {
                addmessageToViewModel(roomChat, 4, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType,chatScreenScrollEnd)
            }
        }
    }
    else {

        if (userId != roomChat.senderId) {
            if (!roomChat.roomChatParentId) {
                if (mime_types_images.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 1, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else if (mime_types_videos.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 5, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else if (mime_types_audios.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 9, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else {
                    addmessageToViewModelFile(roomChat, 13, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
            }
            else {
                if (mime_types_images.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 3, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else if (mime_types_videos.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 7, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else if (mime_types_audios.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 11, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else {
                    addmessageToViewModelFile(roomChat, 15, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
            }

        }
        else {
            if (!roomChat.roomChatParentId) {

                if (mime_types_images.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 2, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else if (mime_types_videos.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 6, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else if (mime_types_audios.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 10, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else {
                    addmessageToViewModelFile(roomChat, 14, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
            }
            else {
                if (mime_types_images.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 4, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else if (mime_types_videos.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 8, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else if (mime_types_audios.indexOf(roomChat.mimeType) >= 0) {
                    addmessageToViewModelFile(roomChat, 12, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
                else {
                    addmessageToViewModelFile(roomChat, 16, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd);
                }
            }

        }
    }
}

//########################################################
//id Help
//1==NormalText
//2==parentText
//3==SelfText
//4==parentSelfText
function addmessageToViewModel(elm, id, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd) {

    var template = "";
    switch (id) {
        case 1:
            template = $('#msg_normalText').text();
            break;
        case 2:
            template = $('#msg_parentNormalText').text();
            break;
       case 3:
            template = $('#msg_SelfNormalText').text();
            break;
       case 4:
            template = $('#msg_SelfNormalParentText').text();
            break;      

    }
    var tag = (elm.tagLearn) ? "<div class='category'>محتوای آموزشی</div>" : "";
    var pin = (roomChatGroupType==1 && userTypeId == 4) ? '<div class=" dropdown-item p-0"><button type = "button" onclick = "RoomChatPin(\'' + elm.roomChatId + '\')" >سنجاق</button ></div >' : "";
    var PermissionStudentChatDelete = (roomChatGroupType == 1 && permissionStudentChatDelete) ? '<div class=" dropdown-item p-0"><button type = "button" onclick = "RoomChatDelete(\'' + elm.roomChatId+'\')"> حذف</button></div >':"";
    var PermissionStudentChatEdit = (roomChatGroupType == 1 && permissionStudentChatEdit) ? '<div class=" dropdown-item p-0"><button onclick = "RoomChatEdit(\'' + elm.roomChatId+'\',\''+elm.senderName+'\')" type = "button"> ویرایش</button> </div >':"";
    template = template.replaceAll('%%RoomChatID', elm.roomChatId);
    template = template.replaceAll('%%SenderName', elm.senderName);
    template = template.replaceAll('%%TextChat', elm.textChat);
    template = template.replaceAll('%%userTypeId',pin);

    template = template.replaceAll('%%RoomChatViewNumber', elm.roomChatViewNumber);
    template = template.replaceAll('%%RoomChatDate', elm.roomChatDateString);
    template = template.replaceAll('%%TagLearn', tag);
    template = template.replaceAll('%%PermissionStudentChatDelete', PermissionStudentChatDelete);
    template = template.replaceAll('%%PermissionStudentChatEdit', PermissionStudentChatEdit);
    //......................
    template = template.replaceAll('%%ParentSenderName', elm.parentSenderName);
    template = template.replaceAll('%%ParentTextChat', elm.parentTextChat);
    template = template.replaceAll('%%RoomChatParentID', elm.roomChatParentId);
    template = template.replaceAll('%%RoomChatDate', elm.roomChatDateString);

    template = $(template);
    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);
    if (chatScreenScrollEnd)
      DivChatScreenScrollEnd();


}

//Id Helper
// 1==msg_file_img_normal;
//2==msg_file_img_self
//3==msg_file_img_parentnormal
//4==msg_file_img_selfparent
//5==msg_file_video_normal;
//6==msg_file_video_parentnormal
//7==msg_file_video_self
//8==msg_file_video_selfparent
function addmessageToViewModelFile(elm, id, userTypeId, permissionStudentChatEdit, permissionStudentChatDelete, roomChatGroupType, chatScreenScrollEnd) {

    var template = "";
    var mimtype = elm.mimeType;
    switch (id) {
        case 1:
            template = $('#msg_file_img_normal').text();
            break;
        case 2:
            template = $('#msg_file_img_self').text();
            break;
        case 3:
            template = $('#msg_file_img_parentnormal').text();
            break;
        case 4:
            template = $('#msg_file_img_selfparent').text();
            break;

         case 5:
            template = $('#msg_file_video_normal').text();
            mimtype = (elm.mimeType == "video/quicktime") ? "video/mp4" : elm.mimeType;
            break;
        case 6:
            template = $('#msg_file_video_self').text();
            mimtype = (elm.mimeType == "video/quicktime") ? "video/mp4" : elm.mimeType;
           
            break;
        case 7:
            template = $('#msg_file_video_parentnormal').text();
            mimtype = (elm.mimeType == "video/quicktime") ? "video/mp4" : elm.mimeType;
            break;
        case 8:
            template = $('#msg_file_video_selfparent').text();
            mimtype = (elm.mimeType == "video/quicktime") ? "video/mp4" : elm.mimeType;
            break;
          case 9:
            template = $('#msg_file_audio_normal').text();
          
            break;
        case 10:
            template = $('#msg_file_audio_self').text();       
          
            break;
        case 11:
            template = $('#msg_file_audio_parentnormal').text();
         
            break;
        case 12:
            template = $('#msg_file_audio_selfparent').text();
          
            break;

        case 13:
            template = $('#msg_file_normal').text();

            break;
        case 14:
            template = $('#msg_file_self').text();
          

            break;
        case 15:
            template = $('#msg_file_parentnormal').text();

            break;
        case 16:
            template = $('#msg_file_selfparent').text();

            break;


    }
    var tag = (elm.tagLearn) ? "<div class='category'>محتوای آموزشی</div>" : "";
    var pin = (roomChatGroupType == 1 && userTypeId == 4) ? '<div class=" dropdown-item p-0"><button type = "button" onclick = "RoomChatPin(\'' + elm.roomChatId + '\')" >سنجاق</button ></div >' : "";
    var PermissionStudentChatDelete = (roomChatGroupType == 1 && permissionStudentChatDelete) ? '<div class=" dropdown-item p-0"><button type = "button" onclick = "RoomChatDelete(\'' + elm.roomChatId + '\')" > حذف</button></div >' : "";
    var PermissionStudentChatEdit = (roomChatGroupType == 1 && permissionStudentChatEdit) ? '<div class=" dropdown-item p-0"><button onclick = "RoomChatEdit(\'' + elm.roomChatId +'\',\'' + elm.senderName + '\')" type = "button" > ویرایش</button> </div >' : "";
    template = template.replaceAll('%%RoomChatID', elm.roomChatId);
    template = template.replaceAll('%%SenderName', elm.senderName);
    template = template.replaceAll('%%TextChat', elm.textChat);
    template = template.replaceAll('%%userTypeId', pin);

    template = template.replaceAll('%%RoomChatViewNumber', elm.roomChatViewNumber);
    template = template.replaceAll('%%RoomChatDate', elm.roomChatDateString);
    template = template.replaceAll('%%TagLearn', tag);
    template = template.replaceAll('%%PermissionStudentChatDelete', PermissionStudentChatDelete);
    template = template.replaceAll('%%PermissionStudentChatEdit', PermissionStudentChatEdit);
    //......................
    template = template.replaceAll('%%ParentSenderName', elm.parentSenderName);
    template = template.replaceAll('%%ParentTextChat', elm.parentTextChat);
    template = template.replaceAll('%%RoomChatParentID', elm.roomChatParentId);
    template = template.replaceAll('%%RoomChatDate', elm.roomChatDateString);
    template = template.replaceAll('%%MimeType', mimtype);
    template = template.replaceAll('%%Filename', elm.mainAddress);
    template = template.replaceAll('%%FileTitle', elm.filename);

    template = $(template);
    //  template.prop('id', 'uploaderFile' + id);
    $('#divChatScreenDetail').append(template);
    if (chatScreenScrollEnd)
        DivChatScreenScrollEnd();


}