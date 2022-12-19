


var ws = null;
var txtChatMsg = null;
var nameProfile = null;
var utype = null;

/*
window.setInterval(getUserOnline, 2000);

function getUserOnline() {
    var teacherid = document.getElementById('teacherid');
    var roomid = document.getElementById('roomid');
    if (!Empty(teacherid) && teacherid != 0 && !Empty(roomid) && roomid != 0) {
        var form = new FormData();
        form.append('teacherid', teacherid.value);
        var request = new XMLHttpRequest();
        request.open('post', '/Chat/getUserOnlines');
        request.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var ids = JSON.parse(request.responseText);
                var spans = document.querySelectorAll('.userpic span');
                for (let span of spans) {
                    for (let id of ids) {                        
                        if (id == span.title) {
                            span.classList.remove("off");
                            span.classList.add("on");
                            break;
                        } else {
                            span.classList.remove("on");
                            span.classList.add("off");
                        }
                    }
                }
            }
        };
        request.send(form);
    }    
}
*/

document.addEventListener("DOMContentLoaded", function () {
    txtChatMsg = document.getElementById('txtChatMsg');    
    nameProfile = document.getElementById('nameProfile');
    utype = document.getElementById('utype');
    setImageLocks();
    //chRightLeft();
});

function getCurrentMessageId() {
    return divmessages().id.replace("messages", "");
}

const divmessages = () => document.querySelector('.messages.current');
const slcChatTag = () => document.querySelector('#slcChatTag');
const toolbar = () => document.querySelector('#toolbar');


function DivMessagesScrollDown() {
    if (!Empty(divmessages())) {
        divmessages().scrollTo(0, divmessages().scrollHeight);
    }
}


function setImageLocks() {
    /* For Img Lock & UnLock */
    var imgLock = document.querySelector('#imgLock');
    var imgUnLock = document.querySelector('#imgUnLock');    
    if (!Empty(imgLock) && !Empty(imgUnLock)) {
        var lock = divmessages().querySelector('#lock' + getCurrentMessageId());
        var lockpermission = divmessages().querySelector('#lockpermission' + getCurrentMessageId());
        if (lockpermission.value == "True") {
            if (lock.value == "True") {
                imgLock.classList.add('displayNone');
                imgUnLock.classList.remove('displayNone');
            } else {
                imgLock.classList.remove('displayNone');
                imgUnLock.classList.add('displayNone');
            }
        }
    }       
}

function keydownTxtChatMsg(e) {
    const keyCode = e.which || e.keyCode;
    if (keyCode == 13 && e.ctrlKey) {        
        txtChatMsg.value += '\n';
        txtChatMsg.selectionStart = txtChatMsg.selectionEnd;
    } else if (keyCode == 13 && !e.ctrlKey) {
        e.preventDefault();
        btnSendMessage();        
    }
}

function btnSendMessage(addressDownload) {    
    if (Empty(txtChatMsg.value)) {
        alert('پیامی برای ارسال وجود ندارد');
        return;
    }

    var text = txtChatMsg.value.replace(/\n|\r\n|\r/g, '<br/>');
    console.log(text);

    sendMsg(0, text, utype.value + ' ' + nameProfile.innerHTML, slcChatTag().value);
}

function sendMsg(id, text, sender, tag = false, pid = '', crud = 'C', reciever = 'All', file = '', addressDownload = '') {

    /*if (ws != null) {*/
    /*var roomid = 0;
    var messages = document.getElementsByClassName('messages');
    for (let message of messages) {
        roomid = message.className.indexOf('current') >= 0 ? message.id.replace("messages", "") : 0;             
        if (roomid > 0) {
            break;
        }
    }*/

    /*var roomid = getCurrentMessageId();

    if (parseInt(roomid) != 0) {
        var teacherid = document.getElementById('teacherid').value;
        var courseid = document.getElementById('courseid').value;
        var json = JSON.stringify({ Id: id, Reciever: reciever, RoomId: roomid, Teacher: teacherid, Course: courseid, Message: text, Tag: tag, ParentId: pid, CRUD: crud, Filename: file, AddressDownload: addressDownload });
        ws.send(json);
        txtChatMsg.value = '';
    } else {
        alert('کلاس مشخص نیست');
    }*/

    /*} else {
        alert('ارتباط با سرور برقرار نیست');
    }*/
    var roomid = getCurrentMessageId();
    if (parseInt(roomid) != 0) {



        var form = new FormData();



        form.append("Id", id);
        form.append("Reciever", reciever);
        form.append("RoomId", roomid);
        form.append("Teacher", document.getElementById('teacherid').value);
        form.append("Course", document.getElementById('courseid').value);
        form.append("Message", text);
        form.append("Tag", tag);
        form.append("ParentId", pid);
        form.append("CRUD", crud);
        form.append("Filename", file);
        form.append("AddressDownload", addressDownload);


        showLoader();

        var request = new XMLHttpRequest();
        request.open('post', '/Chat/ChatInsert');

        request.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                if (!Empty(request.responseText)) {
                    if (IsJsonString(request.responseText)) {
                        var result = JSON.parse(request.responseText);
                        var sendMessageResult = result.messageSend;
                        recieveMsg(sendMessageResult.id,
                            sendMessageResult.message,
                            sendMessageResult.sender,
                            sendMessageResult.senderName,
                            sendMessageResult.senderTime,
                            sendMessageResult.roomId,
                            sendMessageResult.tag,
                            sendMessageResult.parentId,
                            sendMessageResult.messageParent,
                            sendMessageResult.senderNameParent,
                            sendMessageResult.crud,
                            sendMessageResult.filename,
                            sendMessageResult.mimeType,
                            sendMessageResult.addressDownload);
                        hideLoader();
                        txtChatMsg.value = '';
                    } else {
                        alert(xhttp.responseText);
                    }
                    /*alert("salam");*/

                }


                /*hideError();
                var result = this.responseText.messageSend;
                recieveMsg(id,text)
               
                if (result.startsWith('ok,')) {
                    window.location.href = result.replace("ok,", "");
                } else {
                    alert(this.responseText);
                    hideLoader();
                }*/
            }
        };
        request.onerror = function (err) {
            hideLoader();
            alert(err.message);
        };

        request.send(form);
    } else {
        alert('کلاس مشخص نیست');
    }
}

function getparent(pid, pmsg, pSenderName) {
    var parent = '';
    if (!Empty(pid)) {
        if (pid != '0' && parseInt(pid) != 0) {
            var subrpmsg = getSubr(pmsg);
            if (pmsg.length > subrpmsg.length) {
                subrpmsg += ' ...';
            }
            /*if (roomChat !== null) {
                parent = "<div class=\"parent\"  onclick=\"gotomessage('msg" + roomChat.RoomChatParentId + "')\">";
            }*/
            parent = "<div class=\"parent\">";
            parent += pSenderName + " : " + "<br/>" + subrpmsg;
            parent += "</div>";
        }
    }
    return parent;
}

function addMsgToDiv(id, sender, senderName, text, time, rid, tag, pid, pmsg, pSenderName, filename, mimetype, addressDownload) {

    var parent = getparent(pid, pmsg, pSenderName);    
    var file = getfile(filename, mimetype, addressDownload,tag);

    var uid = document.getElementById('uid');
    divmessagesRid = document.getElementById("messages" + rid);

    var tagLearn = tag == false ? "" : "آموزشی";  

    if (tag === true)
        addressDownload += 'Learn/';
    else
        addressDownload += 'Normal/';
   
    if (uid.value != sender) {
        if(filename!=='')
            divmessagesRid.innerHTML += '<div onmousedown=\"onMousDown(this);\" ondblclick=\"slcBoxMsg(this)\" id=\"msg' + id + '\" class=\"divreciever\"><div class=\"reciever\">' + parent + file + senderName + ':' + '<br/>' + text + '<br/><span class=\"time\">' + time + '</span><span class=\"tag\">' + tagLearn + '</span><a href=\"' + addressDownload + filename + '\"> <img src=\"/imagepanel/icons/dwonLoad2.png\" /></a></div></div>';
        else
            divmessagesRid.innerHTML += '<div onmousedown=\"onMousDown(this);\" ondblclick=\"slcBoxMsg(this)\" id=\"msg' + id + '\" class=\"divreciever\"><div class=\"reciever\">' + parent + file + senderName + ':' + '<br/>' + text + '<br/><span class=\"time\">' + time + '</span><span class=\"tag\">' + tagLearn + '</span></div></div>';
    } else {
        if (filename !== '')
            divmessagesRid.innerHTML += '<div onmousedown=\"onMousDown(this);\" ondblclick=\"slcBoxMsg(this)\" id=\"msg' + id + '\" class=\"divsender\"><div class=\"sender\">' + parent + file + senderName + ':' + '<br/>' + text + '<br/><span class=\"time\">' + time + '</span><span class=\"tag\">' + tagLearn + '</span><a href=\"' + addressDownload + filename + '\"> <img src=\"/imagepanel/icons/dwonLoad2.png\" /></a></div></div>';
        else
          divmessagesRid.innerHTML += '<div onmousedown=\"onMousDown(this);\" ondblclick=\"slcBoxMsg(this)\" id=\"msg' + id + '\" class=\"divsender\"><div class=\"sender\">' + parent + file + senderName + ':' + '<br/>' + text + '<br/><span class=\"time\">' + time + '</span><span class=\"tag\">' + tagLearn + '</span></div></div>';
    }

}

function addAttachMsg(id, text) {
    var attach = document.querySelector('.attach');
    var attachmsg = '<div class=\"attachmsg\"><div class=\"text\" >' + text +'</div>';
    attachmsg += '<div class=\"close\" onclick=\"closeAttachMsg(this, ' + id + ')\"> X</div></div >';
    attach.innerHTML += attachmsg;
}

function closeAttachMsg(elem, id) {
    var parent = elem.parentElement.parentElement;
    var form = new FormData();
    form.append('id', id);
    var request = new XMLHttpRequest();
    request.open('post', '/Chat/RemoveAttach');
    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            if (request.responseText == "ok") {
                parent.removeChild(elem.parentElement);
            }
        }
    };
    request.send(form);
}

function recieveMsg(id, text, sender, senderName, time, rid, tag, pid, pmsg, pSenderName, crud, filename, mimetype, addressDownload) {    

    divmessagesRid = document.getElementById("messages" + rid);
    var uid = document.getElementById('uid');

    switch (crud) {
        case 'C':            
            addMsgToDiv(id, sender, senderName, text, time, rid, tag, pid, pmsg, pSenderName, filename, mimetype, addressDownload);            
            DivMessagesScrollDown();
            break;
        case 'A':
            /*if (uid.value != sender) {
                addMsgToDiv(id, sender, senderName, text, time, rid, false);
            }*/
            addAttachMsg(id, text);
            DivMessagesScrollDown();
            break;
        case 'L':
            addMsgToDiv(id, sender, senderName, text, time, rid, false);
            var lock = divmessagesRid.querySelector('#lock' + rid);
            lock.value = 'True';
            chLockMessage(rid);
            setImageLocks();
            DivMessagesScrollDown();            
            break;
        case 'UL':
            addMsgToDiv(id, sender, senderName, text, time, rid, false);
            var lock = divmessagesRid.querySelector('#lock' + rid);
            lock.value = 'False';
            chLockMessage(rid);
            setImageLocks();
            DivMessagesScrollDown();
            break;
        case 'D':
            var msg = document.getElementById('msg' + id);
            divmessagesRid.removeChild(msg);
            DivMessagesScrollDown();
            break;
        case 'E':
            formChat('متن جایگزین', id, text);
            break;
        case 'U':
            var msg = document.getElementById('msg' + id);            
            var s = msg.querySelector('.sender');
            var r = msg.querySelector('.reciever');
            var file = getfile(filename, mimetype, addressDownload,tag);
            var parent = getparent(pid, pmsg, pSenderName);
            if (!Empty(s)) {
                s.innerHTML = file + parent + senderName + ':' + '<br/>' + text;
            } else if (!Empty(r)) {
                r.innerHTML = file + parent + senderName + ':' + '<br/>' + text;
            }
            DivMessagesScrollDown();
            break;
    }
}

function chLockMessage(rid) {

    var utid = document.getElementById('utid').value;
    if (utid != 4) {
        var lock = document.querySelector('#lock' + rid);
        if (Empty(lock)) {
            lock = document.querySelector('#lock' + getCurrentMessageId());
        }
        console.log(lock);
        var divUnlock = document.getElementById('divUnlock');
        var divLock = document.getElementById('divLock');
        if (lock.value == "True") {
            divUnlock.classList.add('displayNone');
            divLock.classList.remove('displayNone');
        } else {
            divUnlock.classList.remove('displayNone');
            divLock.classList.add('displayNone');
        }
    }
}

var pressTimer;
function onMousDown(elem) {
    // clearTimeout(pressTimer);

    // set timeout for this element
    var timeout = window.setTimeout(function () {

        elem.classList.toggle('selectdm');
        setToolbar(elem);
    }, 1000);
    $(elem).mouseup(function () {
        // clear timeout for this element
        window.clearTimeout(timeout);
        // reset mouse up event handler
        $(this).unbind("mouseup");
        return false;
    });
    return false;
  
}


function gotomessage(id) {
    var myEle = document.getElementById(id);
    console.log(id)
    if (myEle) {

       // myEle.scrollIntoView();
        myEle.classList.toggle('selectdm');
        myEle.scrollIntoView({ behavior: 'smooth', block: 'center' })
    }
}


function slcBoxMsg(elem) {
    elem.classList.toggle('selectdm');
   setToolbar(elem);
}

function setToolbar(elem) {
    var selectdms = document.querySelectorAll('.selectdm');
    if (selectdms.length > 0) {

        for (let selectdm of selectdms) {
            if (selectdm != elem) {
                selectdm.classList.remove('selectdm');
            }
        }        

        setImageLocks();

        /* For Img Trash */
        var imgTrash = toolbar().querySelector('#imgTrash');
        var delpermission = divmessages().querySelector('#delpermission' + getCurrentMessageId());
        if (delpermission.value == "True") {
            imgTrash.classList.remove('displayNone');
        } else {
            if (elem.className.indexOf('divsender') >= 0) {
                imgTrash.classList.remove('displayNone');
            } else {
                imgTrash.classList.add('displayNone');
            }
        }

        /* For Img Edit */
        var imgEdit = toolbar().querySelector('#imgEdit');
        var editpermission = divmessages().querySelector('#editpermission' + getCurrentMessageId());
        if (editpermission.value == "True") {
            imgEdit.classList.remove('displayNone');
        } else {
            if (elem.className.indexOf('divsender') >= 0) {
                imgEdit.classList.remove('displayNone');
            } else {
                imgEdit.classList.add('displayNone');
            }
        }


        /* For Img Replay */
        /*var imgReplay = toolbar().querySelector('#imgReplay');
        if (elem.className.indexOf('divreciever') >= 0) {
            imgReplay.classList.remove('displayNone');
        } else {
            imgReplay.classList.add('displayNone');
        }*/

        /* For Img AttachMsg */
        var imgAttachMsg = toolbar().querySelector('#imgAttachMsg');
        if (!Empty(imgAttachMsg)) {
            if (elem.className.indexOf('divsender') >= 0) {
                imgAttachMsg.classList.remove('displayNone');
            } else {
                imgAttachMsg.classList.add('displayNone');
            }
        }

        toolbar().classList.remove('displayNone');
        toolbar().focus();
    } else {
        toolbar().classList.add('displayNone');
    }
}//end of setToolbar

function removeToolbar() {
    toolbar().classList.add('displayNone');
    var selectdms = document.querySelectorAll('.selectdm');
    for (let selectdm of selectdms) {
        selectdm.classList.remove('selectdm');
    }
}

function chChatRoom(elem, id) {
    /* se Back For Room */
    var rows = document.querySelectorAll(".chatusers .row");
    for (let row of rows) {
        row.classList.remove('back');
    }
    elem.classList.add('back');

    /* Change Div Message */
    messages = document.getElementsByClassName('messages');
    for (let message of messages) {
        message.className = 'messages';
        if (message.id == 'messages' + id) {
            message.className += ' current';            
            DivMessagesScrollDown();                      

            /* For txtMsg */
            chLockMessage('');            
        }
    }

    removeToolbar();
    setImageLocks();
    chRightLeft();    

    /* For Select Tag */
    var selectTag = document.querySelector('.divtag select');
    selectTag.selectedIndex = 0;
    chTag(selectTag);
}

function chRightLeft(type = 'L') {
    var left = document.querySelector('.chatbox .left');
    var right = document.querySelector('.chatbox .right');

    if (getWidthPage() <= 700) {
        if (type == 'L') {
            left.style.right = '0px';
            right.style.right = '-700px';            
        } else {
            right.style.right = '0px';
            left.style.right = '-700px';
        }
    }
}

function lock() {
    sendMsg(0, 'گفتگوی ' + divmessages().title + ' توسط معلم بسته شد', utype.value + ' ' + nameProfile.innerHTML, false, '', 'L');
    removeToolbar();
}

function unlock() {
    sendMsg(0, 'گفتگوی ' + divmessages().title + ' توسط معلم باز شد', utype.value + ' ' + nameProfile.innerHTML, false, '', 'UL');
    removeToolbar();
}

function trash() {
    var selectdm = document.querySelector('.selectdm');
    var id = selectdm.id.replace('msg', '');
    sendMsg(id, 'd', utype.value + ' ' + nameProfile.innerHTML, false, '', 'D');
    removeToolbar();
}

function edit() {
    var selectdm = document.querySelector('.selectdm');
    var id = selectdm.id.replace('msg', '');    
    sendMsg(id, '', utype.value + ' ' + nameProfile.innerHTML, false, '', 'E');    
    removeToolbar();
}

function replay() {
    var selectdm = document.querySelector('.selectdm');    
    var id = selectdm.id.replace('msg', '');
    formChat('متن پاسخ', id, '', 'r');
    removeToolbar();
}

function attachmsg() {
    var selectdm = document.querySelector('.selectdm');
    var id = selectdm.id.replace('msg', '');
    sendMsg(id, 'a', utype.value + ' ' + nameProfile.innerHTML, false, '', 'A');
    removeToolbar();
}

function formChat(title, id, text = '', type = 'u') {
    var backPage = document.getElementById("backPage");
    var formChat = document.getElementById("formChat");
    var formTitle = document.getElementById("formTitle");
    var txtFormChat = document.getElementById('txtFormChat');
    document.getElementById("btnFormChatSend").onclick = function () {
        closeFormChat();
        if (type == 'u') {
            sendMsg(id, txtFormChat.value, utype.value + ' ' + nameProfile.innerHTML, false, '', 'U');
        } else if (type == 'r') {
            sendMsg(0, txtFormChat.value, utype.value + ' ' + nameProfile.innerHTML, false, id, 'C');
        }
    };
    formTitle.innerHTML = title;
    txtFormChat.value = text;
    backPage.className = "show";
    formChat.className = "show";
}

function closeFormChat() {
    var backPage = document.getElementById("backPage");
    backPage.className = backPage.className.replace("show", "");
    var formChat = document.getElementById("formChat");
    formChat.className = formChat.className.replace("show", "");
}

function chTag(elem) {
    var msgs = divmessages().querySelectorAll("div[id^='msg']");
    getErrors()
    for (let msg of msgs) {
        var tag = msg.querySelector('.tag');
        if (Empty(elem.value)) {
            msg.classList.remove('displayNone');
        } else if (tag.innerHTML == elem.value) {
            msg.classList.remove('displayNone');
        } else {
            msg.classList.add('displayNone');
        }
    }
    DivMessagesScrollDown();
}

function openlive(type, jitsiAddress, jitsiPassword, adobeAddress, adobeUsername, adobePassword, examAddress) {
    if (type == 'jitsi') {
        /*var JitsiAddress = document.getElementById('JitsiAddress').value;
        var JitsiPassword = document.getElementById('JitsiPassword').value;*/
        if (!Empty(jitsiPassword)) {
            alertmessage('کلمه عبور: ' + jitsiPassword, 'شروع', function () {
                window.open(jitsiAddress);
            });
        } else {
            window.open(jitsiAddress);
        }
    } else if (type == 'adobe') {
        /*var AdobeAddress = document.getElementById('AdobeAddress').value;
        var AdobeUsername = document.getElementById('AdobeUsername').value;
        var AdobePassword = document.getElementById('AdobePassword').value;*/
        if (!Empty(adobePassword)) {
            var text = 'نام کاربری: ' + adobeUsername + '<br/>';
            text += 'کلمه عبور: ' + adobePassword;
            alertmessage(text, 'شروع', function () {
                window.open(adobeAddress);
            });
        } else {
            window.open(adobeAddress);
        }
    } else if (type == 'exam') {
        /*var ExamAddress = document.getElementById('ExamAddress').value;*/
        window.open(examAddress);
    }
}


