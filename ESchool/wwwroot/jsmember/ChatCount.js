//Initialize SignalR
var _userId = 0;
var _userTypeId = 0;
var _isConnected = false;
var _roomChatId = 0;

//var connection = new signalR.HubConnectionBuilder()
//    .withUrl('/chatGroupHub')
//    .build();
var connection = new signalR.HubConnectionBuilder().withUrl("https://chat.hesabischool.com/chatGroupHub").build()
//var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:9116/chatGroupHub").build()
connection.on('ReceiveMessage', renderMessage);

connection.onclose(function () {
    onDisconnected();
    console.log('ReConnecting in 5 Second ...');
    setTimeout(startConnection, 5000);
});

function startConnection() {
    connection.start().then(onConnected).catch(function (err) {
        console.log(err);
        //var chantOffline = document.getElementById("ChatOffline");
        //if (chantOffline.classList.contains("displayNone") === true)
        //    chantOffline.classList.remove('displayNone');
        setTimeout(startConnection, 5000);
    });
    
}

function onDisconnected() {
    //alert("Disconnected");
    //var chantOffline = document.getElementById("ChatOffline");
    //if (chantOffline.classList.contains("displayNone") === true)
    //    chantOffline.classList.remove('displayNone');
    _isConnected = false;
    connection.invoke('LeaveRoom', _userId, _userTypeId);
    setTimeout(startConnection, 5000);
}

function onConnected() {
   //alert(connection.connectionId);
   // alert("onConnected");

    //var chantOffline = document.getElementById("ChatOffline");
    //if (chantOffline.classList.contains("displayNone") === false)
    //    chantOffline.classList.add('displayNone');


    if (_userId === 0) {
        $.ajax({
            url: "/Member/GetUserLogin",
            type: 'Post',
          
            success: function (response) {
                if (response !== null && response !== undefined) {
                    _userId = response.userId;
                    _userTypeId = response.userTypeId;
                    _isConnected = true;
                    //connection.invoke('JoinRoom', _userId, _userTypeId);
                    connection.invoke('JoinRoom', _userId);
                }
               
            },
            error: function (xhr, ajaxOptions, thrownError) {
                
            }
        });
    }
    
}
function ready() {
    startConnection();
  

    //var chatForm = document.getElementById('chatForm');
    //chatForm.addEventListener('submit',
    //    function (e) {
    //        e.preventDefault();
    //        var text = e.target[0].value;
    //        e.target[0].value = '';
    //        var text2 = e.target[1].value;
    //        e.target[1].value = '';
    //        sendMessage('C', text2, _userId, 1, 'ali', 'salam', 'ali.pdf', 'pdf', true, true, null, null, null);
    //    });


}
//function sendMessage(chatType, groupId, roomChatId, senderName, textChat, filename, mimeType, tagLearn, attachMsg, roomChatParentId, parentTextChat, parentSenderName) {
function sendMessageSignalR(chatMessage) {
    if (_isConnected) {


       

        //var key = chatType + '||' + groupId.toString() + '||' + _userId.toString();
        //var filenameMimeType = '';
        //if (filename !== null && filename !== '')
        //    filenameMimeType = filename + '||' + mimeType;
        //var roomChatParent = ''
        //if (roomChatParent !== null && roomChatParent !== '')
        //    roomChatParent = roomChatParentId.toString() + '||' + parentTextChat + '||' + parentSenderName;
        //var status = tagLearn.toString() + '||' + attachMsg.toString();
        //connection.invoke('SendMessage', key, roomChatId.toString(), senderName, textChat, filenameMimeType, roomChatParent, status);
        //var chatMessage = {

        //    ChatType: chatType ,
        //    GroupId: parseInt(groupId),
        //    UserId: parseInt(_userId),
        //    Filename: filename,
        //    MimeType: mimeType,
        //    RoomChatParentId: parseInt(roomChatParentId),
        //    ParentTextChat: parentTextChat,
        //    TagLearn: tagLearn,
        //    AttachMsg: attachMsg
            
        //}
        //connection.invoke('SendMessage', { ChatType: chatType, GroupID: parseInt(groupId), UserID: parseInt(user)} );
        connection.invoke('SendMessage', chatMessage);
    }
}

function renderMessage(chatMessage, connectionId) {
    //var resultKey = key.split("||");
    //var chatType = resultKey[0];
    //var groupIdRecieve = parseInt(resultKey[1]);
    //var userIdRecieve = parseInt(resultKey[2]);


    //var filename = '';
    //var mimeType = '';

    //if (filenameMimeType !== null && filenameMimeType !== '') {
    //    var resultfilenameMimeType = filenameMimeType.split("||");
    //    filename = resultfilenameMimeType[0];
    //    mimeType = resultfilenameMimeType[1];
    //}
    
    //var roomChatParentId =0;
    //var parentTextChat = '';
    //var parentSenderName = '';

    //if (roomChatParent !== null && roomChatParent !== '') {
    //    var resultroomChatParent = roomChatParent.split("||");
    //    var roomChatParentId = parseInt(resultroomChatParent[0]);
    //    var parentTextChat = resultroomChatParent[1];
    //    var parentSenderName = resultroomChatParent[2];
    //}

    //var tagLearn = '';
    //var attachMsg = '';

    //if (status !== null && status !== '') {
    //    var resultstatus = status.split("||");
    //    tagLearn = resultstatus[0];
    //    attachMsg = resultstatus[1];
    //}
    var groupId = GetGroupId();
 
    if (parseInt(groupId) === parseInt(chatMessage.groupId) && connection.connectionId !== connectionId) {
        switch (chatMessage.chatType) {
            case 'C':
                ChatMessageOneUp(chatMessage, _userId, _userTypeId, GetPermissionStudentChatEdit(), GetPermissionStudentChatDelete());
                break;
            case 'D':
                RoomChatDivDelete(chatMessage.roomChatId);
                break;
            case 'E':
                RoomChatDivEdit(chatMessage);
                break;
            case 'P':
                RoomChatDivPinAdd(chatMessage);
                break;
            case 'UP':
                RoomChatDivPinRemove(chatMessage, _userTypeId);
                break;
            case 'L':
                RoomChatDivLock();
                break;
            case 'UL':
                RoomChatDivUnLock();
                break;
        }
    }
    

}

function GetConnected() {
    return _isConnected;
}

function GetUserOnline() {
    connection.on('ReceiveMessageAdmin', renderMessageAdmin);
    connection.invoke('GetUserOnlineHub');
    //alert(test);
   
}
var test = "";
function renderMessageAdmin(userList) {

   

  
        $.ajax({
            url: "/Member/GetUserOnlineCount",
            type: 'Post',
            data: {
                userListOnline: userList
            },

            success: function (response) {

                /*alert(response.roomChatModel);*/
                /*var modelNew = JSON.stringify(response.roomChatModel);*/
                if (response !== null && response !== undefined) {
                  



                    alert(response.count);
                  

                }

                HideLoader();

            },
            error: function (xhr, ajaxOptions, thrownError) {
                HideLoader();
            }
        });
}
   


 

document.addEventListener('DOMContentLoaded', ready);