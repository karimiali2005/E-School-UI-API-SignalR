function Empty(val) {
    return val === '' || val === undefined || val === null;
}
//var someVarName = localStorage.getItem("someVarKey");


var _isConnectedChat = false;
function SetConnectedChat() {
    _isConnectedChat = false;
    try {
        _isConnectedChat= GetConnected();
    }
    catch (err) {
        _isConnectedChat = false;
    }
    return _isConnectedChat;
}

setTimeout(CheckConnectedChat, 5000);

function CheckConnectedChat() {
    SetConnectedChat();
    if (_isConnectedChat === false) {
        var chantOffline = document.getElementById("ChatOffline");
        if (chantOffline.classList.contains("displayNone") === true)
            chantOffline.classList.remove('displayNone');
    }
    else {
        var chantOffline = document.getElementById("ChatOffline");
        if (chantOffline.classList.contains("displayNone") === false)
            chantOffline.classList.add('displayNone');
    }
    return _isConnectedChat;
}

function GetGroupId() {
    return _roomChatGroupID;
}
function GetPermissionStudentChatEdit() {
    return _permissionStudentChatEditValue;
}
function GetPermissionStudentChatDelete() {
    return _permissionStudentChatDeleteValue;
}
function GetRoomChatGroupType() {
    return _roomChatGroupType;
}

function MessageAlertNewTab(address) {
    var showPass = document.getElementById("MessageAlertNewTab");
    if (showPass.classList.contains("displayNone") === true)
       showPass.classList.remove('displayNone');
    var btnNewTab = document.getElementById('btnOpenNewTab');
    btnNewTab.onclick = function () {
        window.open(address);
        if (showPass.classList.contains("displayNone") === false)
          showPass.classList.add('displayNone');
    }
}
function OpenManageHomWork(course, roomId, courseid, roomChatGroupID)
{
    var address = window.location.origin + '/HomeWork/manageHomWork?nameDars=' + course + '&idroom=' + roomId + '&courseid=' + courseid + '&roomChatGroupID=' + roomChatGroupID+'&returnUrl='+getUrl();
    window.open(address, "_self");

}
function OpenHomeWorkStudent( roomId, courseid) {
    var address = window.location.origin + '/HomeWorkStuden/Index?idroom=' + roomId + '&idcours=' + courseid + '&returnUrl='  + getUrl();
    window.open(address, "_self");

}

function OpenLinkAdobe(adobeLiveAddress, adobeLiveUsername, adobeLivePass) {
   
    if (!Empty(adobeLivePass)) {
        var txtUserName = document.getElementById("divNewTabSiteUserName");
        var txtUserPass = document.getElementById("divNewTabSiteUserPass");

        if (txtUserName.classList.contains("displayNone") === true)
            txtUserName.classList.remove('displayNone');

        txtUserName.textContent = adobeLiveUsername;
        txtUserPass.textContent = adobeLivePass;

        MessageAlertNewTab(adobeLiveAddress);
    } else {
                window.open(adobeLiveAddress);
        }
}
function OpenLinkJitsi(jitsiLiveAddress, jitsiLivePassword) {

    if (!Empty(jitsiLiveAddress)) {
        var txtUserName = document.getElementById("divUserNameMain");
        var txtUserPass = document.getElementById("divNewTabSiteUserPass");

        if (txtUserName.classList.contains("displayNone") === false)
            txtUserName.classList.add('displayNone');
        
        txtUserPass.textContent = jitsiLivePassword;

        MessageAlertNewTab(jitsiLiveAddress);
    } else {
        window.open(jitsiLiveAddress);
    }
}
function OpenLinkExam(examAddress) {
    window.open(examAddress);
}
function OpenLinkZoom(zoomAddress) {
    window.open(zoomAddress);
}
function TagLearnFilter() {


    
    if (_tagLearnFilter === false) {
        _tagLearnFilter = true;
        RoomChatLeftShow(_roomChatGroupID, _roomChatGroupType, _roomId, _teacherId, _courseId, _picName, _roomChatGroupTitle, _tagLearnFilter);
    }
    else {
        _tagLearnFilter = false;
        RoomChatLeftShow(_roomChatGroupID, _roomChatGroupType, _roomId, _teacherId, _courseId, _picName, _roomChatGroupTitle, _tagLearnFilter);
    }





}

function RoomChatDivUnLock() {
    var divChatBoxWrite = document.getElementById("writeText");
    if (divChatBoxWrite != null) {
        divChatBoxWrite.remove();
    }
    

    var template = $('#divUNLockTemplate').text();
    //var pinTempClose = (userTypeId == 4) ? '<button onclick="RoomChatPinUndo(\'' + roomChatModel.roomChatId + '\')" class=" close-icon" type="button"><img src="/imagemember/close.svg" alt=""></button>' : "";


    //template = template.replaceAll('%%PinRoomChatID%%', roomChatModel.roomChatId);
    //template = template.replaceAll('%%PinTextChat%%', roomChatModel.textChat);
    //template = template.replaceAll('%%RoomChatPinUndo', pinTempClose);
    template = $(template);
    //  template.prop('id', 'uploaderFile' + id);
    $(template).insertAfter($("#divChatScreen"));
    SetTextChatMessageChange();
}
function RoomChatDivLock() {
    var divChatBoxWrite = document.getElementById("writeText");
    if (divChatBoxWrite != null) {
        divChatBoxWrite.remove();
    }
    var divChatBoxReply = document.getElementById("replyWriteText");
    if (divChatBoxReply != null) {
        divChatBoxReply.remove();
    }

    var template = $('#divLockTemplate').text();
    
    template = $(template);
    //  template.prop('id', 'uploaderFile' + id);
    $(template).insertAfter($("#divChatScreen"));
    
}
function RoomChatDivPinAdd(roomChatModel, userTypeId) {
    var divRoomChatPin = document.getElementById("divRoomChatPin");
    if (divRoomChatPin != null) {
        divRoomChatPin.remove();
    }

    var template = $('#divChatPinTemplate').text();
    var pinTempClose = (userTypeId == 4) ? '<button onclick="RoomChatPinUndo(\'' +roomChatModel.roomChatId+'\')" class=" close-icon" type="button"><img src="/imagemember/close.svg" alt=""></button>' : "";


    template = template.replaceAll('%%PinRoomChatID%%', roomChatModel.roomChatId);
    template = template.replaceAll('%%PinTextChat%%', roomChatModel.textChat);
    template = template.replaceAll('%%RoomChatPinUndo', pinTempClose);
    template = $(template);
    //  template.prop('id', 'uploaderFile' + id);
    $(template).insertAfter($("#headerLeft"));
}
function RoomChatDivPinRemove() {
    var divRoomChatPin = document.getElementById("divRoomChatPin");
    if (divRoomChatPin != null) {
        divRoomChatPin.remove();
    }
}
function RoomChatDivDelete(roomChatId)
{
    $("#msg" + roomChatId).remove();
}
function RoomChatDivDeleteAll() {
    $("#divChatScreenDetail").empty();
}
function RoomChatDivEdit(roomChatModel) {
    var elem = document.getElementById("msg" + roomChatModel.roomChatId);
    var txtChat = elem.getElementsByClassName("div-text-massage-chat");
    txtChat[0].innerText = roomChatModel.textChat;

    //document.getElementById("dropdownEdit" + roomChatModel.roomChatId).setAttribute("onclick", "RoomChatEdit('" + roomChatModel.roomChatId + "','" + roomChatModel.textChat + "','" + roomChatModel.senderName +  "')");    
    //document.getElementById("dropdownEdit" + roomChatModel.roomChatId).setAttribute("onclick", "RoomChatReply('" + roomChatModel.roomChatId + "','" + roomChatModel.textChat + "','" + roomChatModel.senderName + "')");    

    var divDetailsMassage = elem.getElementsByClassName('details-massage')
    var divcategory = document.createElement('div');
    divcategory.innerText = 'محتوای آموزشی';
    divcategory.className = 'category';
 

    var divBefore = elem.getElementsByClassName('category');

    if (roomChatModel.tagLearn) {
        if (divBefore.length === 0)
            divDetailsMassage[0].append(divcategory)
    }
    else {
        if (divBefore.length !== 0) {
            divBefore[0].remove();
      
        }
            
    }
   
}

function RoomChatEdit(roomChatId, parentSenderName='') {

    document.getElementById('imgReplyWriteText').src = '/imagemember/edit2.svg';

    var txtChatMessage = document.getElementById("txtChatMessage");
    var replyWriteText = document.getElementById("replyWriteText");

   
    var inputRoomChatId = document.getElementById("input_roomChatId");
    var inputRoomChatIsAction = document.getElementById("input_roomChatIsAction");

    

    if (replyWriteText.classList.contains("displayNone") === true) {
        replyWriteText.classList.remove('displayNone');
    }


    var elem = document.getElementById("msg" + roomChatId);
    var txtChat = elem.getElementsByClassName("div-text-massage-chat");
    //txtChat[0].innerText = roomChatModel.textChat;

    //txtChatMessage.textContent = parentTextChat;
    txtChatMessage.textContent = txtChat[0].innerText;


    

    inputRoomChatId.value = roomChatId;
    inputRoomChatIsAction.value = 2;
    TextChatMessageChange();


    var replySenderName = document.getElementById("replySenderName");
    var replyChatText = document.getElementById("replyChatText");

    if (replySenderName.classList.contains("displayNone") === true) {
        replySenderName.classList.remove('displayNone');
    }
    replySenderName.textContent = parentSenderName;

    if (replyChatText.classList.contains("displayNone") === true) {
        replyChatText.classList.remove('displayNone');
    }
    //replyChatText.textContent = parentTextChat;
    replyChatText.textContent = txtChat[0].innerText;


    $("#txtChatMessage").focusTextToEnd();

}
(function ($) {
    $.fn.focusTextToEnd = function () {
        this.focus();
        var $thisVal = this.val();
        this.val('').val($thisVal);
        return this;
    }
}(jQuery));
function RoomChatReply(roomChatId,myMessage, parentSenderName = '') {

    document.getElementById('imgReplyWriteText').src = '/imagemember/reply-message.svg';
    var replyWriteText = document.getElementById("replyWriteText");
    var replySenderName = document.getElementById("replySenderName");
    var replyChatText = document.getElementById("replyChatText");
    var inputRoomChatId = document.getElementById("input_roomChatId");
    var inputRoomChatIsAction = document.getElementById("input_roomChatIsAction");

    if (replyWriteText.classList.contains("displayNone") === true) {
        replyWriteText.classList.remove('displayNone');
    }

    var elem = document.getElementById("msg" + roomChatId);
    var txtChat = elem.getElementsByClassName("div-text-massage-chat");
   
    replyChatText.textContent = txtChat[0].innerText;
    

    //if (myMessage) {
    //    if (replySenderName.classList.contains("displayNone") === false) {
    //        replySenderName.classList.add('displayNone');
    //    }
    //} else {
    //    if (replySenderName.classList.contains("displayNone") === true) {
    //        replySenderName.classList.remove('displayNone');
    //    }
        replySenderName.textContent = parentSenderName;
    //}

    inputRoomChatId.value = roomChatId;
    inputRoomChatIsAction.value = 1;
  
    $("#txtChatMessage").focusTextToEnd();

}
function RoomChatReplyFinishOrUndo() {
    var replyWriteText = document.getElementById("replyWriteText");
    var inputRoomChatId = document.getElementById("input_roomChatId");
    var inputRoomChatIsAction = document.getElementById("input_roomChatIsAction");


    var txtChatMessage = document.getElementById("txtChatMessage");

    txtChatMessage.textContent = "";

    if (replyWriteText.classList.contains("displayNone") === false) {
        replyWriteText.classList.add('displayNone');
    }

    inputRoomChatId.value = 0;
    inputRoomChatIsAction.value = 0;
    TextChatMessageChange();

}

function functionConfirm(msg, myYes, myNo,myYesAll) {
    var confirmBox = $("#MessageAlertDelete");
    confirmBox.find(".message").text(msg);
    confirmBox.find(".btn-yes,.btn-no,.btn-yes-all").unbind().click(function () {
        confirmBox.hide();
    });
    confirmBox.find(".btn-yes").click(myYes);
    confirmBox.find(".btn-no").click(myNo);
    confirmBox.find(".btn-yes-all").click(myYesAll);
    /*if (confirmBox.classList.contains("displayNone") === false) {
        confirmBox.classList.remove('displayNone');
    }*/
    confirmBox.show();
}
function RoomChatDelete(roomChatId) {

   

    functionConfirm("آیا از حذف پیام انتخاب شده مطمئن هستید؟", function yes() {
            ShowLoader();

            $.ajax({
                url: "/Member/RoomChatViewDelete",
                type: 'Post',
                data: {
                    roomChatId: roomChatId
                },

                success: function (response) {
                    if (response !== null && response !== undefined) {

                        RoomChatDivDelete(roomChatId);

                    }
                    HideLoader();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    HideLoader();
                }
            });
           

        },
        function no() {
           
        },
        function yesall() {
            ShowLoader();

            $.ajax({
                url: "/Member/RoomChatDelete",
                type: 'Post',
                data: {
                    roomChatId: roomChatId
                },

                success: function (response) {
                    if (response !== null && response !== undefined) {
                        if (response.allowDelete === true) {
                            RoomChatDivDelete(roomChatId);
                            if (CheckConnectedChat())
                                sendMessageSignalR(response.roomChatModel);
                        }
                        else {
                            alert("امکان حذف برای شما وجود ندارد");
                        }
                    }
                    HideLoader();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    HideLoader();
                }
            });
        });

    
}
function functionConfirmAll(msg, myYes, myNo) {
    var confirmBox = $("#MessageAlertDeleteAll");
    confirmBox.find(".message").text(msg);
    confirmBox.find(".btn-yes,.btn-no,.btn-yes-all").unbind().click(function () {
        confirmBox.hide();
    });
    confirmBox.find(".btn-yes").click(myYes);
    confirmBox.find(".btn-no").click(myNo);
  
    /*if (confirmBox.classList.contains("displayNone") === false) {
        confirmBox.classList.remove('displayNone');
    }*/
    confirmBox.show();
}
function RoomChatDeletAll(roomChatGroupId) {

    

    functionConfirmAll("آیا از حذف همه پیام ها مطمئن هستید؟", function yes() {
        ShowLoader();

        $.ajax({
            url: "/Member/RoomChatDeleteAll",
            type: 'Post',
            data: {
                roomChatGroupId: roomChatGroupId
            },

            success: function (response) {
                if (response !== null && response !== undefined) {
                    if (response.returnValue)
                      RoomChatDivDeleteAll()

                }
                HideLoader();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                HideLoader();
            }
        });


    },
        function no() {

        });
       

}

function RoomChatPin(roomChatId) {
    ShowLoader();

    $.ajax({
        url: "/Member/RoomChatPin",
        type: 'Post',
        data: {
            roomChatGroupId: _roomChatGroupID,
            roomChatId: roomChatId,
            isPin:true
        },

        success: function (response) {
            if (response !== null && response !== undefined) {

                RoomChatDivPinAdd(response.roomChatModel, response.userTypeId)
                if (CheckConnectedChat())
                 sendMessageSignalR(response.roomChatModel);
            }
            HideLoader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });
}
function RoomChatPinUndo(roomChatId) {
    ShowLoader();

    $.ajax({
        url: "/Member/RoomChatPin",
        type: 'Post',
        data: {
            roomChatGroupId: _roomChatGroupID,
            roomChatId: roomChatId,
            isPin: false
        },

        success: function (response) {
            if (response !== null && response !== undefined) {
                RoomChatDivPinRemove();
                if (CheckConnectedChat())
                  sendMessageSignalR(response.roomChatModel);
            }
            HideLoader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });
}
function RoomChatLock(roomChatGroupId) {
    ShowLoader();

    $.ajax({
        url: "/Member/RoomChatLock",
        type: 'Post',
        data: {
            roomChatGroupId: roomChatGroupId,
            isLock: true
        },

        success: function (response) {
            if (response !== null && response !== undefined) {
                var chatLock = document.getElementById("ChatLock");
                var chatUnLock = document.getElementById("ChatUnLock");

                if (chatLock.classList.contains("displayNone") === true)
                    chatLock.classList.remove('displayNone');
                if (chatUnLock.classList.contains("displayNone") === false)
                    chatUnLock.classList.add('displayNone');

                if (CheckConnectedChat())
                 sendMessageSignalR(response.roomChatModel);
            }
            HideLoader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });
}
function RoomChatUnLock(roomId) {
    ShowLoader();

    $.ajax({
        url: "/Member/RoomChatLock",
        type: 'Post',
        data: {
            roomChatGroupId: _roomChatGroupID,
            roomId: roomId,
            isLock: false
        },

        success: function (response) {
            if (response !== null && response !== undefined) {
                var chatLock = document.getElementById("ChatLock");
                var chatUnLock = document.getElementById("ChatUnLock");

                if (chatLock.classList.contains("displayNone") === false)
                    chatLock.classList.add('displayNone');
                if (chatUnLock.classList.contains("displayNone") === true)
                    chatUnLock.classList.remove('displayNone');
                if (CheckConnectedChat())
                 sendMessageSignalR(response.roomChatModel);
            }
            HideLoader();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });
}
function RoomChatDownload(downloadAddress) {
    window.open(downloadAddress);
}

var _roomChatGroupType = 0;
var _roomChatGroupID = 0;
var _roomId = 0;
var _teacherId = 0;
var _courseId = 0;
var _picName = '';
var _roomChatGroupTitle = '';
var _tagLearnFilter = false;
var _permissionStudentChatEditValue = false;
var _permissionStudentChatDeleteValue = false;
var _searchText = '';



function RoomChatLeftShowSearch(isUndo = false) {
    var txtSearchMessage = document.getElementById('txtSearchMessage');
    if (isUndo===false && Empty(txtSearchMessage.value)) {
        
        return;
    }

    RoomChatLeftShow(_roomChatGroupID, _roomChatGroupType, _roomId, _teacherId, _courseId, _picName, _roomChatGroupTitle, _tagLearnFilter, txtSearchMessage.value)
}

function RoomChatLeftShow(roomChatGroupID, roomChatGroupType, roomId, teacherId, courseId, picName, roomChatGroupTitle, tagLearnFilter = false, searchText='') {
    ShowLoader();
    _roomChatGroupID = roomChatGroupID;
    _roomId = roomId;
    _teacherId = teacherId;
    _courseId = courseId;
    _picName = picName;
    _roomChatGroupTitle = roomChatGroupTitle;
    _tagLearnFilter = tagLearnFilter;
    _roomChatGroupType = roomChatGroupType;
    _searchText = searchText; 

    

    if (window.innerWidth < 576) {
        var roomChatRight = document.getElementById('div-roomChatRight');
        var roomChatLeft = document.getElementById('div-roomChatLeft');
        if (roomChatRight.classList.contains("d-none") === false) {
            roomChatRight.classList.add('d-none');
            roomChatRight.classList.add('d-sm-block');
            roomChatLeft.classList.remove('d-none');
            roomChatLeft.classList.remove('d-sm-block');
        }
    }

    var count=0
    var elem = document.getElementById("liRoomChatRight" + roomChatGroupID);
    var newChat = elem.getElementsByClassName("count");
    if (newChat[0]) {
       
        var countString = newChat[0].innerText.trim();
        if (countString !== '99+')
            count = parseInt(countString);
        else
            count = -2;
    }
    
  
    //var hasNewChat = false;
    //if (newChat.length >0)
    //    hasNewChat = true;

    $.ajax({
        url: "/Member/RoomChatLeft",
        type: 'Get',
        data: {
            roomChatGroupID: roomChatGroupID,
            roomChatGroupType: roomChatGroupType,
            tagLearn: _tagLearnFilter,
            newChatCount: count,
            roomId: roomId,
            teacherId: teacherId,
            courseId: courseId,
            pageNumber: 1,
            pageSize: 30,
            picName: picName,
            roomChatGroupTitle: roomChatGroupTitle,
            searchText: searchText
           

        },

        success: function (response) {

            if (response !== null && response !== undefined) {


                $("#div-roomChatLeft").html("");
                /*var temp = $("#divChatScreen").height();*/
                $("#div-roomChatLeft").html($.parseHTML(response, document, true));


                isallow = true;
                isLoading = false;

                page = 1;
                SetScrool();


                SetTextChatMessageChange();

            

                var writeText = document.getElementById('writeText');
                if (writeText.classList.contains("displayNone") === true)
                    writeText.classList.remove('displayNone');



                
                var input_permissionStudentChatEdit = document.getElementById("input_permissionStudentChatEdit");
                if (input_permissionStudentChatEdit.value === 'True')
                    _permissionStudentChatEditValue = true;

                
                var input_permissionStudentChatDelete = document.getElementById("input_permissionStudentChatDelete");
                if (input_permissionStudentChatDelete.value === 'True')
                    _permissionStudentChatDeleteValue = true;

                if (roomChatGroupType === "1") {
                    var tagLearnUnFilterbtn = document.getElementById("TagLearnUnFilter");
                    var tagLearnFilterbtn = document.getElementById("TagLearnFilter");
                    if (_tagLearnFilter === false) {
                        if (tagLearnFilterbtn.classList.contains("displayNone") === false)
                            tagLearnFilterbtn.classList.add('displayNone');
                        if (tagLearnUnFilterbtn.classList.contains("displayNone") === true)
                            tagLearnUnFilterbtn.classList.remove('displayNone');


                    }
                    else {
                        if (tagLearnFilterbtn.classList.contains("displayNone") === true)
                            tagLearnFilterbtn.classList.remove('displayNone');
                        if (tagLearnUnFilterbtn.classList.contains("displayNone") === false)
                            tagLearnUnFilterbtn.classList.add('displayNone');
                    }
                }




                if (searchText == '') {

                    var roomchatLeftHeaderButton = document.getElementById('roomchatLeftHeaderButton');
                    var roomchatLeftHeaderSearch = document.getElementById('roomchatLeftHeaderSearch');
                    if (roomchatLeftHeaderButton.classList.contains("d-none") === true) {
                        roomchatLeftHeaderSearch.classList.add('d-none');
                        roomchatLeftHeaderButton.classList.remove('d-none');
                        roomchatLeftHeaderButton.classList.add('d-flex');
                    }
                }

                else {
                    var roomchatLeftHeaderButton = document.getElementById('roomchatLeftHeaderButton');
                    var roomchatLeftHeaderSearch = document.getElementById('roomchatLeftHeaderSearch');
                    if (roomchatLeftHeaderSearch.classList.contains("d-none") == true) {
                        roomchatLeftHeaderSearch.classList.remove('d-none');
                        roomchatLeftHeaderButton.classList.add('d-none');
                        roomchatLeftHeaderButton.classList.remove('d-flex');
                    }
                    var txtSearchMessage = document.getElementById('txtSearchMessage');
                    txtSearchMessage.value = searchText;
                }

                
                $(".dropdown-toggle").dropdown();
                SetActiveRoomChatRightLi();


                if (count>0) {
                    var elem = document.getElementById("liRoomChatRight" + roomChatGroupID);
                    var newChat = elem.getElementsByClassName("count");
                    newChat[0].remove();
                }
                
                var input_roomChatViewLast = document.getElementById("input_roomChatViewLast");
                if (count!=0 && parseInt(input_roomChatViewLast.value) !== 0) {
                    DivChatScreenScrollEnd();
                    GoToMessageWithoutAnimateEnd("msg" + input_roomChatViewLast.value);

                    setTimeout(function Add() {
                        var elem = document.getElementById("BottomMassageEnd");

                        var divCount = document.createElement('span');
                        if (count != -2) {
                            divCount.innerText = count.toString();
                        }
                        else {
                            divCount.innerText = '99+';
                        }
                        
                        divCount.className = 'circle-count-massage';


                        $(elem).append(divCount);},500)
                   
                   
                } 
                else
                    DivChatScreenScrollEnd();
  
            }
            HideLoader();
            //  loadNewPage();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });

   

}

function ReturnToListUser() {
    if (window.innerWidth < 576) {
        var roomChatRight = document.getElementById('div-roomChatRight');
        var roomChatLeft = document.getElementById('div-roomChatLeft');
        if (roomChatLeft.classList.contains("d-none") === false) {
            roomChatLeft.classList.add('d-none');
            roomChatLeft.classList.add('d-sm-block');
            roomChatRight.classList.remove('d-none');
            roomChatRight.classList.remove('d-sm-block');
        }
    }
}
function SetActiveRoomChatRightLi() {
    $('ul li').click(function () {
        if ($('li').hasClass("active"))
            $('li').removeClass("active");
        $(this).addClass("active");
    });
}

$(document).ready(function () {

   
    SetActiveRoomChatRightLi();

    SetTextChatMessageChange();
  //  SetScroolRight();
   

});

function TextChatMessageChange() {
    var imgVoiceStart = document.getElementById('imgVoiceStart');
    var imgVoiceStop = document.getElementById('imgVoiceStop');
    var imgSendMessage = document.getElementById('imgSendMessage');
    if ($("#txtChatMessage").val().trim() !== '') {

        if (imgVoiceStart.classList.contains("displayNone") === false) {
            imgVoiceStart.classList.add('displayNone');
        }

        if (imgVoiceStop.classList.contains("displayNone") === false)
            imgVoiceStop.classList.add('displayNone');

        if (imgSendMessage.classList.contains("displayNone") === true)
            imgSendMessage.classList.remove('displayNone');
    } else {

        if (imgVoiceStart.classList.contains("displayNone") === true) {
            imgVoiceStart.classList.remove('displayNone');
        }

        if (imgVoiceStop.classList.contains("displayNone") === false)
            imgVoiceStop.classList.add('displayNone');

        if (imgSendMessage.classList.contains("displayNone") === false)
            imgSendMessage.classList.add('displayNone');
    }  
}

function SetTextChatMessageChange() {
    $("#txtChatMessage").on("input",
        function () {
            TextChatMessageChange();

        });
   
}

var isEducation = 'false';

function MessageEducation(type) {
    var imgSimple = document.getElementById('imgSimple');
    var imgEducation = document.getElementById('imgEducation');
    if (type === 1) {
        if (imgSimple.classList.contains("displayNone") === true)
            imgSimple.classList.remove('displayNone');
        if (imgEducation.classList.contains("displayNone") === false)
            imgEducation.classList.add('displayNone');
        isEducation = 'false';
    } else {
        if (imgSimple.classList.contains("displayNone") === false)
            imgSimple.classList.add('displayNone');
        if (imgEducation.classList.contains("displayNone") === true)
            imgEducation.classList.remove('displayNone');
        isEducation = 'true';
    }
    
}

//function KeyDownTextMessageChat(e) {
//    const keyCode = e.which || e.keyCode;
//    if (keyCode === 13 && e.ctrlKey) {
//        txtChatMsg.value += '\n';
//        txtChatMsg.selectionStart = txtChatMsg.selectionEnd;
//    } else if (keyCode === 13 && !e.ctrlKey) {
//        e.preventDefault();
//        $("#imgSendMessage").click();
//    }
//}


function SendMessageText(roomChatGroupId, roomId, teacherId, courseId, roomChatId, roomChatIsAction, parentTextChat, parentSenderName, roomChatGroupTitle, roomChatGroupType) {
    var txtChatMessage = document.getElementById('txtChatMessage');
    if (Empty(txtChatMessage.value)) {
        alert('پیامی برای ارسال وجود ندارد');
        return;
    }

    var text = txtChatMessage.value.replace(/\n|\r\n|\r/g, '<br/>');
    if (roomChatIsAction === 0)
        RoomChatInsert(roomChatGroupId, text, '', null, roomId, teacherId, courseId, roomChatIsAction, parentTextChat, parentSenderName, roomChatGroupTitle, roomChatGroupType);
    else if (roomChatIsAction === 1)
        RoomChatInsert(roomChatGroupId, text, '', roomChatId, roomId, teacherId, courseId, roomChatIsAction, parentTextChat, parentSenderName, roomChatGroupTitle, roomChatGroupType);
    else {
        RoomChatEditFinish(roomChatId, text, parentTextChat, parentSenderName);
    }
        
    
    
  /*$(this).val('');*/
    $("#txtChatMessage").val('');
    $("#txtChatMessage").textContent = "";
   
}

function ChatScreenScrollEndRemove() {
    var elem = document.getElementById("BottomMassageEnd");
    var chatScreenScrollEnd = elem.getElementsByClassName("circle-count-massage");
    if (chatScreenScrollEnd[0]) {
        chatScreenScrollEnd[0].remove();

    }

 
    var elem = document.getElementById("liRoomChatRight" + _roomChatGroupID);
    var newChat = elem.getElementsByClassName("count");
     if (newChat[0])
        newChat[0].remove();
    
}

function DivChatScreenScrollEnd(isDeleteCount=false) {
    $('#divChatScreen').scrollTop($('#divChatScreen')[0].scrollHeight - $('#divChatScreen')[0].clientHeight);
    if (isDeleteCount) {
        ChatScreenScrollEndRemove();

    }
   
}

function RoomChatEditFinish(roomChatId, textChat, parentTextChat, parentSenderName) {

         ShowLoader();

        $.ajax({
            url: "/Member/RoomChatEdit",
            type: 'Post',
            data: {
                roomChatId: roomChatId,
                textChat: textChat,
                tagLearn: isEducation,
                parentTextChat: parentTextChat,
                parentSenderName: parentSenderName
            },

            success: function (response) {
              
                if (response !== null && response !== undefined) {
                    RoomChatReplyFinishOrUndo();
                    RoomChatDivEdit(response.roomChatModel);

                    if (CheckConnectedChat())
                       sendMessageSignalR(response.roomChatModel);
                }
                HideLoader();


            },
            error: function (xhr, ajaxOptions, thrownError) {
                HideLoader();
            }
        });
  

}


function RoomChatInsert(roomChatGroupId, textChat, fileName, roomChatParentId, roomId, teacherId, courseId, roomChatIsAction, parentTextChat, parentSenderName, roomChatGroupTitle, roomChatGroupType) {
    if (roomChatIsAction === 0 || roomChatIsAction === 1) {
        ShowLoader();
        $.ajax({
            url: "/Member/RoomChatInsert",
            type: 'Post',
            data: {
                roomChatGroupId: roomChatGroupId,
                textChat: textChat,
                fileName: fileName,
                tagLearn: isEducation,
                roomChatParentId: roomChatParentId,
                roomId: roomId,
                teacherId: teacherId,
                courseId: courseId,
                parentTextChat: parentTextChat,
                parentSenderName: parentSenderName,
                roomChatGroupTitle: roomChatGroupTitle,
                roomChatGroupType: roomChatGroupType
            },

            success: function(response) {
                /*alert(response.roomChatModel);*/
            /*var modelNew = JSON.stringify(response.roomChatModel);*/
                if (response !==null && response !== undefined) {
                    if (roomChatIsAction === 1)
                        RoomChatReplyFinishOrUndo();

                    ChatMessageOneUp(response.roomChatModel, response.roomChatModel.senderId, response.userTypeId, _permissionStudentChatEditValue, _permissionStudentChatDeleteValue, _roomChatGroupType);
                    ////sendMessage('C', _roomChatGroupID, response.roomChatModel.roomChatID, response.roomChatModel.senderName, response.roomChatModel.textChat, response.roomChatModel.filename, response.roomChatModel.mimeType, response.roomChatModel.tagLearn, response.roomChatModel.attachMsg, response.roomChatModel.roomChatParentID, response.roomChatModel.parentTextChat, response.roomChatModel.parentSenderName);
                    if (CheckConnectedChat())
                       sendMessageSignalR(response.roomChatModel);
                    DivChatScreenScrollEnd();
                    TextChatMessageChange();
                    RoomChatRightUpdate(response.roomChatModel.groupId, response.roomChatModel.textChat, response.roomChatModel.roomChatDateString,true)

                }

                HideLoader();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                HideLoader();
            }
        });
    }

}

function GoToMessageWithoutAnimate(id) {
    var myEle = document.getElementById(id);

    if (myEle) {

        // myEle.scrollIntoView();
        /*myEle.classList.toggle('selectdm');*/
        myEle.scrollIntoView({ block: 'start' });

    }
}
function GoToMessageWithoutAnimateEnd(id) {
    var myEle = document.getElementById(id);

    if (myEle) {

        // myEle.scrollIntoView();
        /*myEle.classList.toggle('selectdm');*/
        myEle.scrollIntoView({ block: 'end' });

    }
}
function GoToMessage(id) {
    var myEle = document.getElementById(id);
   
    if (myEle) {

        // myEle.scrollIntoView();
        /*myEle.classList.toggle('selectdm');*/
        myEle.scrollIntoView({ behavior: 'smooth', block: 'center' });

        
        var divContent = myEle.getElementsByClassName('content')
        if (divContent[0].classList.contains("fadeIn-reply") === false)
            divContent[0].classList.add('fadeIn-reply');

        setTimeout(GoToMessageUndo, 2000, id);
        
    }
}
function GoToMessageUndo(id) {

    var myEle = document.getElementById(id);

    var divContent = myEle.getElementsByClassName('content')
    if (divContent[0].classList.contains("fadeIn-reply") === true)
        divContent[0].classList.remove('fadeIn-reply');
}



var page = 0;

var isLoading = false;

var isallow = true;

function SetScroolRight() {
    $("#divRoomChatGroupInsert").scroll(function () {

        //alert("asd");
        //if ($("#divChatScreen").scrollTop() < 60 && !isLoading && isallow) {


        //    isLoading = true;
        //    setTimeout(loadNewPage, 1200);
        //    /*loadNewPage();*/
        //}

        if (($('#divRoomChatGroupInsert')[0].offsetHeight + $('#divRoomChatGroupInsert')[0].scrollTop + 150 >= $('#divRoomChatGroupInsert')[0].scrollHeight)) {
          //  ChatScreenScrollEndRemove();

            alert("SDF");
        }


    });
}

function SetScrool() {

    
    $("#divChatScreen").scroll(function () {

        
        if ($("#divChatScreen").scrollTop() < 60 && !isLoading && isallow) {

           
            isLoading = true;
            setTimeout(loadNewPage, 1200);
            /*loadNewPage();*/
        }

        if (($('#divChatScreen')[0].offsetHeight + $('#divChatScreen')[0].scrollTop + 150 >= $('#divChatScreen')[0].scrollHeight)) {
            ChatScreenScrollEndRemove();

            
        }


    });



    


    /*
    $('.button-panel-menu-side').click(function () {
        $('.panel-menu-slider').toggleClass('show');
        $('.content-all div.content').toggleClass('show');
    });
    if ($(window).width() <= 576) {
        $('.panel-menu-slider').toggleClass('show');
        $('.content-all div.content').toggleClass('show');
    }
    $(".dropdown-toggle").dropdown();
*/



/*if ($('#divChatScreen').hasScrollBar() == false) {
    //برای وقتی که اسکرول کامل نشده است
    isLoading = true;
    setTimeout(loadNewPage, 1200);
  
}*/
}

/*$(document).ready(function () {
    console.log("main");
    console.log($('#divChatScreen').hasScrollBar().toString());
    *//*loadNewPage();*//*
    $("#divChatScreen").scroll(function () {

        console.log($("#divChatScreen").scrollTop());
        if ($("#divChatScreen").scrollTop() < 60 && !isLoading && isallow) {

            console.log("scroolTrue");
            isLoading = true;
            setTimeout(loadNewPage, 1200);

        }

    });

    *//*if ($('#divChatScreen').hasScrollBar() == false) {
        //برای وقتی که اسکرول کامل نشده است
        isLoading = true;
        setTimeout(loadNewPage, 1200);
        alert('content 1: ' + $('#divChatScreen').hasScrollBar());
    }*//*



});
*/


function loadNewPage() {
    
    /*var temp = $("#divChatScreen").height();*/
    page++;

    $(".container").prepend('<div class="big-box"><h1>Page ' + page + '</h1></div>');
    RoomChatLeftShow2();
    /*$("#divChatScreen").scrollTop($("#divChatScreen").height() - temp);*/
    /*isLoading = false;*/
}

function RoomChatRightShow2() {
    ShowLoader();
    var input_roomChatFirst = document.getElementById("input_roomChatFirst");
    $.ajax({
        url: "/Member/RoomChatRight2",
        type: 'Get',
        data: {
            pageNumber: pageRight,
            pageSize: 15
        },

        success: function (response) {
            if (response !== null && response !== undefined) {
                var message = "";
                message = response;
                if (message.trim().length > 1) {

                    var temp = $('#divChatScreen').height();
                    $(".chat-screen").prepend($.parseHTML(response, document, true));

                    // $('#divChatScreen').scrollTop($('#divChatScreen').height() + temp);
                    GoToMessageWithoutAnimate("msg" + input_roomChatFirst.value);


                    isLoading = false;

                } else {
                    isallow = false;
                    isLoading = false;

                }

            }
            HideLoader();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });
}

function RoomChatLeftShow2() {

    
    ShowLoader();
    var input_roomChatFirst = document.getElementById("input_roomChatFirst");
    $.ajax({
        url: "/Member/RoomChatLeft2",
        type: 'Get',
        data: {
            roomChatGroupID: _roomChatGroupID,
            tagLearn: _tagLearnFilter,
            pageNumber: page,
            pageSize: 30,
            permissionStudentChatEdit: _permissionStudentChatEditValue,
            permissionStudentChatDelete: _permissionStudentChatDeleteValue,
            searchText: _searchText
        },

        success: function (response) {
            if (response !== null && response !== undefined) {
                var message = "";
                message = response;
                if (message.trim().length > 1) {
                   
                    var temp = $('#divChatScreen').height();
                    $(".chat-screen").prepend($.parseHTML(response, document, true));
              
                   // $('#divChatScreen').scrollTop($('#divChatScreen').height() + temp);
                    GoToMessageWithoutAnimate("msg" + input_roomChatFirst.value);
                    
                  
                    isLoading = false;

                } else {
                    isallow = false;
                    isLoading = false;

                }

            }
            HideLoader();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });

}

(function ($) {
    $.fn.hasScrollBar = function () {
        return this.get(0).scrollHeight > this.height();
    }
})(jQuery);




////Voice

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
        var imgVoiceStop = document.getElementById('imgVoiceStop');
        imgVoiceStop.classList.toggle('displayNone');
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

function StopRecording(elem, roomChatGroupID, textChat, roomChatParentId, roomChatIsAction, roomId, teacherId, courseId, parentTextChat, parentSenderName, roomChatGroupTitle, roomChatGroupType) {
    /*elem.disabled = true;*/

    recorder.stopRecording(stopRecordingCallback);

    var imgVoiceStart = document.getElementById('imgVoiceStart');
    imgVoiceStart.classList.toggle('displayNone');
    elem.classList.toggle('displayNone');
    //  DownloadRecording();
    /*console.log("isStop");*/

    setTimeout(function () {
        DownloadRecording(roomChatGroupID, textChat, roomChatParentId, roomChatIsAction, roomId, teacherId, courseId, parentTextChat, parentSenderName, roomChatGroupTitle, roomChatGroupType);
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

function DownloadRecording(roomChatGroupID, textChat, roomChatParentId, roomChatIsAction, roomId, teacherId, courseId, parentTextChat, parentSenderName, roomChatGroupTitle, roomChatGroupType) {
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
    form.append('tagLearn', isEducation);
    form.append('roomChatGroupId', roomChatGroupID);
    form.append('textChat', textChat);
    form.append('roomChatParentId', roomChatParentId);
    form.append('roomId', roomId);
    form.append('teacherId', teacherId);
    form.append('courseId', courseId);
    form.append('parentTextChat', parentTextChat);
    form.append('parentSenderName', parentSenderName);
    form.append('roomChatGroupTitle', roomChatGroupTitle);
    form.append('roomChatGroupType', roomChatGroupType);

    var request = new XMLHttpRequest();
    request.open('POST', '/Member/SendRecordAudio');

    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (!Empty(request.responseText)) {
                if (IsJsonString(request.responseText)) {
                    if (roomChatIsAction === 1) {
                        RoomChatReplyFinishOrUndo();
                    }
                    var result = JSON.parse(request.responseText);
                    ChatMessageOneUp(result.roomChatModel, result.roomChatModel.senderId, result.userTypeId, _permissionStudentChatEditValue, _permissionStudentChatDeleteValue, _roomChatGroupType);
                    if (CheckConnectedChat())
                        sendMessageSignalR(result.roomChatModel);
                    DivChatScreenScrollEnd();
                    TextChatMessageChange();
                } else {
                    alert(request.responseText);
                }
                /*alert("salam");*/

            }
            HideLoader();
            input.value = '';

        }
    };
    request.onerror = function (err) {
        alert(err.message);
    };
    ShowLoader();
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


////File
function ShowLoader(showPercent=false) {
    var divLoadingUpload = document.getElementById('div-loading-upload');
    var divPercentLoading = document.getElementById('divPercentLoading');
    var divTextLoading = document.getElementById('divTextLoading');
    var btnLoadingUploadClose = document.getElementById('btnLoadingUploadClose');

    if (divLoadingUpload.classList.contains("displayNone") === true)
        divLoadingUpload.classList.remove('displayNone');
    if (showPercent===false) {
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
function IsJsonString(str) {
    try {
        JSON.parse(str);
    }
    catch (e) {
        return false;
    }
    return true;
}

function fileUpload(size, roomChatGroupID, roomChatParentId, roomChatIsAction, roomId, teacherId, courseId, parentTextChat, parentSenderName, roomChatGroupTitle,roomChatGroupType) {
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

    var textChat = '';
    var txtChatMessage = document.getElementById('txtChatMessage');
    if (Empty(txtChatMessage.value)===false) {
        textChat = txtChatMessage.value.replace(/\n|\r\n|\r/g, '<br/>');
    }

    

    var input = document.getElementById('file');
    var data = new FormData();
    var file = input.files[0];


    if (file.size < size * 1024 * 1024) {
        data.append('file', input.files[0]);
        data.append('tagLearn', isEducation);
        data.append('roomChatGroupId', roomChatGroupID);
        data.append('textChat', textChat);
        data.append('roomChatParentId', roomChatParentId);
        data.append('roomId',roomId);
        data.append('teacherId', teacherId);
        data.append('courseId', courseId);
        data.append('parentTextChat', parentTextChat);
        data.append('parentSenderName', parentSenderName);
        data.append('roomChatGroupTitle', roomChatGroupTitle);
        data.append('roomChatGroupType', roomChatGroupType);

        xhttp = new XMLHttpRequest();

        xhttp.upload.addEventListener('progress', function (e) {
            var percent_complete = (e.loaded / e.total) * 100;
            var spnLoadingUpload = document.getElementById('spnLoadingUpload');
            spnLoadingUpload.innerHTML = parseInt(percent_complete) ;
        });
        xhttp.addEventListener('load', function (e) {
            //console.log(xhttp.status);
            //console.log(xhttp.response);
        });
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                if (!Empty(xhttp.responseText)) {
                    if (IsJsonString(xhttp.responseText)) {
                        if (roomChatIsAction === 1) {
                            RoomChatReplyFinishOrUndo();
                        }
                        var result = JSON.parse(xhttp.responseText);
                        ChatMessageOneUp(result.roomChatModel, result.roomChatModel.senderId, result.userTypeId, _permissionStudentChatEditValue, _permissionStudentChatDeleteValue, _roomChatGroupType);
                        if (CheckConnectedChat())
                            sendMessageSignalR(result.roomChatModel);
                        DivChatScreenScrollEnd();
                        TextChatMessageChange();
                      
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
        xhttp.open('POST', '/Member/StoreFile');
        xhttp.send(data);

    }
    else {
        alert("حجم فایل ارسالی بیش از  حد مجاز است \r حداکثر حجم مجاز " + size.toString());
        HideLoader();
        input.value = '';
    }

}


//Online Member
function ShowUserOnline() {
    if (CheckConnectedChat())
        GetUserOnline();
    else
        alert('شما افلاین هستید');
}

function RoomChatGroupOnlineShow(roomChatGroupId, userList) {
    ShowLoader();
    $.ajax({
        url: "/Member/RoomChatGroupOnlineShow",
        type: 'Post',
        data: {
            roomChatGroupId: roomChatGroupId,
            userListOnline: userList
        },

        success: function (response) {
            
            /*alert(response.roomChatModel);*/
            /*var modelNew = JSON.stringify(response.roomChatModel);*/
            if (response !== null && response !== undefined) {
                $("#divRoomChatGroupOnline ul").empty()
                $.each(response.roomChatGroupOnline, function (index, roomGroupOnlie) {
                    


                    var template = $('#liRoomChatGroup').text();
                    

                    
                    var online = (roomGroupOnlie.isOnline === true) ? '<span class=" online-off bg-success"></span>' : '<span class=" online-off bg-danger"></span>';
                    template = template.replaceAll('%%IsOnline%%', online);
                    template = template.replaceAll('%%PicName%%', roomGroupOnlie.picName);
                    template = template.replaceAll('%%FullName%%', roomGroupOnlie.fullName);
                    template = $(template);
                    //  template.prop('id', 'uploaderFile' + id);
                    $("#divRoomChatGroupOnline ul").append(template);

                   

                });  
             
                $('#staticBackdrop').modal('show'); 
               
            }

            HideLoader();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });
}


//Contact

function openContact() {
    ShowLoader();
    $.ajax({
        url: "/Member/RoomChatContactShow",
        type: 'Post',
        
        success: function (response) {

         
            if (response !== null && response !== undefined) {
                $("#divRoomChatConatct ul").empty()
                $.each(response.roomChatContact, function (index, roomContact) {



                    var template = $('#liRoomChatContact').text();


                    template = template.replaceAll('%%TeacherID%%', roomContact.userID);
                    template = template.replaceAll('%%TeacherTitle%%', roomContact.fullName);
                    template = template.replaceAll('%%PicName%%', roomContact.picName);
                    template = template.replaceAll('%%PicNameShort%%', roomContact.picNameShort);
                    template = template.replaceAll('%%FullName%%', roomContact.fullName);
                    template = $(template);
                    $("#divRoomChatConatct ul").append(template);



                });

                var divC = document.getElementById("cart-sideC");
                var aC = document.getElementById("aC");
                divC.style.right = 0;
                divC.style.backgroundColor = "red";
                aC.style.opacity = 0.8;
                aC.style.visibility = "visible";

            }

            HideLoader();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });
    
}

function RoomChatGroupInsert(teacherId, teacherTitle, picName, picNameShort) {
    ShowLoader();
    $.ajax({
        url: "/Member/RoomChatGroupInsert",
        type: 'Post',
        data: {
            teacherId: teacherId,
            teacherTitle: teacherTitle
        },
        success: function (response) {


            if (response !== null && response !== undefined) {
            
              

                var template = $('#liRoomChatGroupInsert').text();

                template = template.replaceAll('%%RoomChatGroupID%%', response.roomChatGroupModel.roomChatGroupId);
                template = template.replaceAll('%%RoomChatGroupType%%', 2);
                template = template.replaceAll('%%TeacherID%%', response.roomChatGroupModel.teacherId);
                template = template.replaceAll('%%PicName%%', picName);
                template = template.replaceAll('%%PicNameShort%%', picNameShort );
                template = template.replaceAll('%%RoomChatTitle%%', response.roomChatGroupModel.roomChatGroupTitle);
                template = $(template);
                $("#divRoomChatGroupInsert ul").append(template);



                closeContact();
                $("#liRoomChatRight" + response.roomChatGroupModel.roomChatGroupId).click();
              

            }

            HideLoader();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });

}


//Search

$("#box").on('keyup', function () {
    var matcher = new RegExp($(this).val(), 'gi');
    $('.li-contact-list').show().not(function () {
        return matcher.test($(this).find('.name').text())
    }).hide();
});

function SearchRoomChatGroupShow() {

    var searchRoomChatGroup = document.getElementById("SearchRoomChatGroup");
    if (searchRoomChatGroup.classList.contains("displayNone") === true)
        searchRoomChatGroup.classList.remove('displayNone');
}
function SearchRoomChatGroupHide() {

    var searchRoomChatGroup = document.getElementById("SearchRoomChatGroup");
    if (searchRoomChatGroup.classList.contains("displayNone") === false)
        searchRoomChatGroup.classList.add('displayNone');
}

$("#boxRoomChatGroup").on('keyup', function () {
    var matcher = new RegExp($(this).val(), 'gi');
    $('.li-roomChatGroup-list').show().not(function () {
        return matcher.test($(this).find('.name').text())
    }).hide();
});


//Play Sound

function playSound() {
    
    var address = window.location.origin + '/imagemember/alert3.mp3';
    const audio = new Audio(address);
    
    audio.play();
}

//Refresh Group

function RoomChatRightUpdate(roomChatGroupId, textChat, roomChatDateString,endPosition= false) {
    try {


        var elem = document.getElementById("liRoomChatRight" + roomChatGroupId);
        if (elem) {
            var elmNew = elem;
            elem.remove();
        }

        var roomChatCount = elem.getElementsByClassName("count");
        if (roomChatCount[0]) {
            if (endPosition == false) {


                var count = parseInt(roomChatCount[0].innerText.trim()) + 1;
                if (count > 99)
                    roomChatCount[0].innerText = '99+';
                else
                    roomChatCount[0].innerText = count.toString();
            }

            var time = elem.getElementsByClassName('date');
            time[0].innerText = roomChatDateString;
        }
        else {



            if (endPosition == false) {
                var divCount = document.createElement('div');

                divCount.innerText = '1';
                divCount.className = 'count ml-2';
            }

            var divTime = document.createElement('div');
            divTime.innerText = roomChatDateString;
            divTime.className = 'date';


            var divDateCount = elmNew.getElementsByClassName('date-count');

            
            if (divDateCount[0]) {
                var time = divDateCount[0].getElementsByClassName('date')
                time[0].remove();
                if (endPosition == false) {
                    divDateCount[0].append(divCount)
                }
                divDateCount[0].append(divTime)
            }
   
        }

        var textChatDiv = elem.getElementsByClassName("pTextChat")
        textChatDiv[0].innerText = textChat;
       

        $("#divRoomChatGroupInsert ul").prepend(elmNew);
    }
    catch (err) {
        alert( err.message);
    }

}

//View 
function RoomChatViewInsert(roomChatId, roomChatGroupId) {
    ShowLoader();
    $.ajax({
        url: "/Member/RoomChatViewInsert",
        type: 'Post',
        data: {
            roomChatId: roomChatId,
            roomChatGroupId: roomChatGroupId
        },

        success: function (response) {
            HideLoader();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });
}


//Update ChatScreenScrollEnd
function ChatScreenScrollEndUpdate() {
 

    if (!($('#divChatScreen')[0].offsetHeight + $('#divChatScreen')[0].scrollTop + 150 >= $('#divChatScreen')[0].scrollHeight)) {


        var elem = document.getElementById("BottomMassageEnd");


        var chatScreenScrollEnd = elem.getElementsByClassName("circle-count-massage");
        if (chatScreenScrollEnd[0]) {
            var count = parseInt(chatScreenScrollEnd[0].innerText.trim()) + 1;
            if (count > 99)
                chatScreenScrollEnd[0].innerText = '99+';
            else
                chatScreenScrollEnd[0].innerText = count.toString();

        }
        else {




            var divCount = document.createElement('span');
            divCount.innerText = '1';
            divCount.className = 'circle-count-massage';


            $(elem).append(divCount);

        }

        return true;
    }
    else {
       
        return false;
    }


}

//Refresh Page Hold
function RefreshPage() {
   
    location.reload();
}

//function RoomChatReady() {
//    try {
//        if (someVarName) {
//            $("#liRoomChatRight" + someVarName).click();
//        }
//    }
//    catch  {
        
//    }
    
//}
//document.addEventListener('DOMContentLoaded', RoomChatReady);


//Start Voice

//webkitURL is deprecated but nevertheless
URL = window.URL || window.webkitURL;

var gumStream; 						//stream from getUserMedia()
var rec; 							//Recorder.js object
var input; 							//MediaStreamAudioSourceNode we'll be recording

// shim for AudioContext when it's not avb. 
var AudioContext = window.AudioContext || window.webkitAudioContext;
var audioContext //audio context to help us record

//var recordButton = document.getElementById("recordButton");
//var stopButton = document.getElementById("stopButton");
//var pauseButton = document.getElementById("pauseButton");

////add events to those 2 buttons
//recordButton.addEventListener("click", startRecording);
//stopButton.addEventListener("click", stopRecording);
//pauseButton.addEventListener("click", pauseRecording);

function startRecording1(elem) {
    console.log("recordButton clicked");

	/*
		Simple constraints object, for more advanced audio features see
		https://addpipe.com/blog/audio-constraints-getusermedia/
	*/

    var constraints = { audio: true, video: false }

    /*
      Disable the record button until we get a success or fail from getUserMedia() 
  */

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
        //document.getElementById("formats").innerHTML = "Format: 1 channel pcm @ " + audioContext.sampleRate / 1000 + "kHz"

        /*  assign to gumStream for later use  */
        gumStream = stream;

        /* use the stream */
        input = audioContext.createMediaStreamSource(stream);

		/* 
			Create the Recorder object and configure to record mono sound (1 channel)
			Recording 2 channels  will double the file size
		*/
        rec = new Recorder(input, { numChannels: 1 })

        //start the recording process
        rec.record()

        console.log("Recording started");

    }).catch(function (err) {
        //enable the record button if getUserMedia() fails
        //recordButton.disabled = false;
        //stopButton.disabled = true;
        //pauseButton.disabled = true
    });

    var imgVoiceStop = document.getElementById('imgVoiceStop');
    imgVoiceStop.classList.toggle('displayNone');
    elem.classList.toggle('displayNone');
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


var roomChatGroupIDUpload;
var textChatUpload;
var roomChatParentIdUpload;
var roomChatIsActionUpload;
var roomIdUpload;
var teacherIdUpload;
var courseIdUpload;
var parentTextChatUpload;
var parentSenderNameUpload;

function StopRecording2(elem, roomChatGroupID, textChat, roomChatParentId, roomChatIsAction, roomId, teacherId, courseId, parentTextChat, parentSenderName) {

    roomChatGroupIDUpload = roomChatGroupID;
    textChatUpload = textChat;
    roomChatParentIdUpload = roomChatParentId;
    roomChatIsActionUpload = roomChatIsAction;
    roomIdUpload = roomId;
    teacherIdUpload = teacherId;
    courseIdUpload = courseId;
    parentTextChatUpload = parentTextChat;
    parentSenderNameUpload = parentSenderName;


    console.log("stopButton clicked");

    var imgVoiceStart = document.getElementById('imgVoiceStart');
    imgVoiceStart.classList.toggle('displayNone');
    elem.classList.toggle('displayNone');

    //disable the stop button, enable the record too allow for new recordings
    //stopButton.disabled = true;
    //recordButton.disabled = false;
    //pauseButton.disabled = true;

    //reset button just in case the recording is stopped while paused
    //pauseButton.innerHTML = "Pause";

    //tell the recorder to stop the recording
    rec.stop();

    //stop microphone access
    gumStream.getAudioTracks()[0].stop();

    //create the wav blob and pass it on to createDownloadLink


    //setTimeout(function () {
    //    rec.exportWAV(createDownloadLink);
    //}, 1000);






    setTimeout(function () {
        rec.exportWAV(DownloadRecording2);
        // DownloadRecording2(roomChatGroupID, textChat, roomChatParentId, roomChatIsAction, roomId, teacherId, courseId, parentTextChat, parentSenderName);
    }, 1000);
};
function DownloadRecording2(blob) {
    /*console.log("isDownload")*/
    /*this.disabled = true;*/
    //if (!recorder || !recorder.getBlob()) return;

    /* if (isSafari) {
         recorder.getDataURL(function (dataURL) {
             SaveToDisk(dataURL, getFileName('mp3'));
         });
         return;
     }*/

    // var blob = rec.getBlob();

    ShowLoader(true);

    var btnLoadingUploadClose = ShowLoadingUploadClose();
    if (!Empty(btnLoadingUploadClose)) {
        btnLoadingUploadClose.addEventListener('click', function () {
            btnLoadingUploadClose.classList.add('displayNone');
            HideLoader();
            if (request != null) {
                request = null;
                input.value = '';
            }
        });
    }


    var file = new File([blob],
        getFileName('wav'),
        {
            type: 'audio/wav'
        });

    //var file = new Date().toISOString();



    var form = new FormData();
    form.append('file', file);
    form.append('tagLearn', isEducation);
    form.append('roomChatGroupId', roomChatGroupIDUpload);
    form.append('textChat', textChatUpload);
    form.append('roomChatParentId', roomChatParentIdUpload);
    form.append('roomId', roomIdUpload);
    form.append('teacherId', teacherIdUpload);
    form.append('courseId', courseIdUpload);
    form.append('parentTextChat', parentTextChatUpload);
    form.append('parentSenderName', parentSenderNameUpload);

    var request = new XMLHttpRequest();

    request.upload.addEventListener('progress', function (e) {
        var percent_complete = (e.loaded / e.total) * 100;
        var spnLoadingUpload = document.getElementById('spnLoadingUpload');
        spnLoadingUpload.innerHTML = parseInt(percent_complete);
    });
    


    request.open('POST', '/Member/SendRecordAudio');

    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (!Empty(request.responseText)) {
                if (IsJsonString(request.responseText)) {
                    if (roomChatIsActionUpload === 1) {
                        RoomChatReplyFinishOrUndo();
                    }
                    var result = JSON.parse(request.responseText);
                    ChatMessageOneUp(result.roomChatModel, result.roomChatModel.senderId, result.userTypeId, _permissionStudentChatEditValue, _permissionStudentChatDeleteValue, _roomChatGroupType);
                    if (CheckConnectedChat())
                        sendMessageSignalR(result.roomChatModel);
                    DivChatScreenScrollEnd();
                    TextChatMessageChange();
                } else {
                    alert(request.responseText);
                }
                /*alert("salam");*/

            }
            HideLoader();
            input.value = '';

        }
    };
    request.onerror = function (err) {
        alert(err.message);
        HideLoader();
    };

    request.addEventListener('load', function (e) {
        //console.log(xhttp.status);
        //console.log(xhttp.response);
    });

    request.send(form);
    //   ReleaseMicrophone();


};



//Stop Voice


//Forward
function RoomChatForward(roomChatId) {
    ShowLoader();
    $.ajax({
        url: "/Member/RoomChatForwardUserShow",
        type: 'Post',
        data: {
            roomChatId: roomChatId,
            roomChatGroupId:_roomChatGroupID
        },

        success: function (response) {

            /*alert(response.roomChatModel);*/
            /*var modelNew = JSON.stringify(response.roomChatModel);*/
            if (response !== null && response !== undefined) {
                $("#divRoomChatForwardUser ul").empty()
                $.each(response.roomChatForwardUser, function (index, roomChatForwardUser) {



                    var template = $('#liRoomChatForwardUser').text();



                    //var online = (roomGroupOnlie.isOnline === true) ? '<span class=" online-off bg-success"></span>' : '<span class=" online-off bg-danger"></span>';
                    // template = template.replaceAll('%%IsOnline%%', online);
                    template = template.replaceAll('%%GroupID%%', roomChatForwardUser.groupID);
                    template = template.replaceAll('%%PicName%%', roomChatForwardUser.picName);
                    template = template.replaceAll('%%FullName%%', roomChatForwardUser.groupTitle);
                    template = $(template);
                    //  template.prop('id', 'uploaderFile' + id);
                    $("#divRoomChatForwardUser ul").append(template);



                });

                var inputRoomChatIdFroward = document.getElementById("input_roomChatIdFroward");
                inputRoomChatIdFroward.value = roomChatId;
                $('#staticBackdropFroward').modal('show');

            }

            HideLoader();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });
}

function RoomChatForwardSend() {
    var listchecked = '';
    $('#divRoomChatForwardUser li').each(function () {
       // alert(this.id);
        var liId = this.id;
        var id = liId.substring(21, liId.length);
        if ($('#chkStatus' + id).is(':checked')) {
            listchecked += id+',';
        }
        //console.log(this.id);  //id
        //console.log($('.tagit-label', this).text());  //text
    });
    var inputRoomChatIdFroward = document.getElementById("input_roomChatIdFroward");
    var roomChatId=inputRoomChatIdFroward.value;
     
    ShowLoader();
    $.ajax({
        url: "/Member/RoomChatForwardSend",
        type: 'Post',
        data: {
            listId: listchecked,
            roomChatId: roomChatId
        },

        success: function (response) {
            /*alert(response.roomChatModel);*/
            /*var modelNew = JSON.stringify(response.roomChatModel);*/
            if (response !== null && response !== undefined) {
               

                $.each(response.listRoomChatReturn, function (index, listRoomChatReturn) {

                    if (CheckConnectedChat())
                        sendMessageSignalR(listRoomChatReturn);

                    RoomChatRightUpdate(listRoomChatReturn.groupId, listRoomChatReturn.textChat, listRoomChatReturn.roomChatDateString, true)



                });
                $("#staticBackdropFroward").modal("hide");

            }

            HideLoader();

        },
        error: function (xhr, ajaxOptions, thrownError) {
            HideLoader();
        }
    });
}
//Search

function RoomChatSearch() {
    var roomchatLeftHeaderButton = document.getElementById('roomchatLeftHeaderButton');
    var roomchatLeftHeaderSearch = document.getElementById('roomchatLeftHeaderSearch');
    if (roomchatLeftHeaderSearch.classList.contains("d-none") == true) {
        roomchatLeftHeaderSearch.classList.remove('d-none');
        roomchatLeftHeaderButton.classList.add('d-none');
        roomchatLeftHeaderButton.classList.remove('d-flex');
    }
}
function RoomChatSearchUndo() {
    var roomchatLeftHeaderButton = document.getElementById('roomchatLeftHeaderButton');
    var roomchatLeftHeaderSearch = document.getElementById('roomchatLeftHeaderSearch');
    if (roomchatLeftHeaderButton.classList.contains("d-none") === true) {
        roomchatLeftHeaderSearch.classList.add('d-none');
        roomchatLeftHeaderButton.classList.remove('d-none');
        roomchatLeftHeaderButton.classList.add('d-flex');
    }
    var txtSearchMessage = document.getElementById('txtSearchMessage');
    txtSearchMessage.value = '';
    RoomChatLeftShowSearch(true);
}


//Exam
function OpenExam() {
    var address = window.location.origin + '/Exam/Index?returnUrl=' + getUrl();
    window.open(address, "_self");

}


function RoomChatGroupDivDelete(roomChatGroupId) {
    $("#liRoomChatRight" + roomChatGroupId).remove();
}

function RoomChatGroupRemove(roomChatGroupId) {



    functionConfirmAll("آیا از حذف گفتگو مطمئن هستید؟", function yes() {
        ShowLoader();

        $.ajax({
            url: "/Member/RoomChatGroupRemove",
            type: 'Post',
            data: {
                roomChatGroupId: roomChatGroupId
            },

            success: function (response) {
                if (response !== null && response !== undefined) {
                    if (response.returnValue) {
                        RoomChatGroupDivDelete(roomChatGroupId);
                        RefreshPage();
                    }

                }
                HideLoader();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                HideLoader();
            }
        });


    },
        function no() {

        });


}


function RoomChatPlayVoiceUpdate(roomChatId) {
    var elem = document.getElementById("playVoiceBullet" + roomChatId);
    if (elem) {
        ShowLoader();
        $.ajax({
            url: "/Member/RoomChatPlayVoiceUpdate",
            type: 'Post',
            data: {
                roomChatId: roomChatId
            },

            success: function (response) {
                $("#playVoiceBullet" + roomChatId).remove();
                HideLoader();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                HideLoader();
            }
        });
    }
}

//ReportCard
function MangeReportCardTeacher(roomChatGroupID) {
    var address = window.location.origin + '/ReportCardTeacher/MangeReportCardTeacher?id=' + roomChatGroupID + '&returnUrl=' + getUrl();
    window.open(address, "_self");

}


