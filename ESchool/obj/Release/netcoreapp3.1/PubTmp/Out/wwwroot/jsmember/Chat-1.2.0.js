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
        var chantOffline = document.getElementById("ChatOffline");
        if (chantOffline.classList.contains("displayNone") === true)
            chantOffline.classList.remove('displayNone');
        setTimeout(startConnection, 5000);
    });
    
}

function onDisconnected() {
    //alert("Disconnected");
    var chantOffline = document.getElementById("ChatOffline");
    if (chantOffline.classList.contains("displayNone") === true)
        chantOffline.classList.remove('displayNone');
    _isConnected = false;
    connection.invoke('LeaveRoom', _userId, _userTypeId);
    setTimeout(startConnection, 5000);
}

function onConnected() {
   //alert(connection.connectionId);
   // alert("onConnected");

    var chantOffline = document.getElementById("ChatOffline");
    if (chantOffline.classList.contains("displayNone") === false)
        chantOffline.classList.add('displayNone');


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

}

function sendMessageSignalR(chatMessage) {
    if (_isConnected) {
        connection.invoke('SendMessage', chatMessage);
    }
}

function renderMessage(chatMessage, connectionId) {
    var groupId = GetGroupId();

    if (connection.connectionId !== connectionId) {

  
        if (parseInt(groupId) === parseInt(chatMessage.groupId)) {
            switch (chatMessage.chatType) {
                case 'C':
                    var result=ChatScreenScrollEndUpdate();
                    ChatMessageOneUp(chatMessage, _userId, _userTypeId, GetPermissionStudentChatEdit(), GetPermissionStudentChatDelete(), GetRoomChatGroupType(), false);
                    RoomChatViewInsert(chatMessage.roomChatId, chatMessage.groupId);

                    RoomChatRightUpdate(parseInt(chatMessage.groupId), chatMessage.textChat, chatMessage.roomChatDateString, !result)
                    playSound();
                    if (!result)
                        DivChatScreenScrollEnd();
                    
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
        else {
            switch (chatMessage.chatType) {
                case 'C':
                    RoomChatRightUpdate(parseInt(chatMessage.groupId), chatMessage.textChat, chatMessage.roomChatDateString);
                    playSound();
                    break;
               
            }
        }
    }
    

}

function GetConnected() {
    return _isConnected;
}

function GetUserOnline() {
    connection.on('ReceiveMessageAdmin', renderMessageAdmin);
    connection.invoke('GetUserOnlineHub');
   
   
}

function renderMessageAdmin(userList) {

    var groupId = GetGroupId();
    RoomChatGroupOnlineShow(groupId, userList);

}

document.addEventListener('DOMContentLoaded', ready);