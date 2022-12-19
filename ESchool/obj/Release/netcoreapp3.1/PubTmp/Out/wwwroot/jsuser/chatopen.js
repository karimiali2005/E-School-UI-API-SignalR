function connect(welcome = false) {
    ws = new WebSocket(uri);

    ws.onopen = function () {
        if (welcome) {
            DivMessagesScrollDown();
        }

    };
}

connect(true);

ws.onmessage = function (indata) {
    var data = JSON.parse(indata.data);
    recieveMsg(data.Id, data.Message, data.Sender, data.SenderName, data.SenderTime, data.RoomId, data.Tag, data.ParentId, data.MessageParent, data.SenderNameParent, data.CRUD, data.Filename, data.MimeType, data.AddressDownload);
};

ws.onclose = function (e) {
    console.log("ارتباط با سرور قطع شده است");
    setTimeout(function () {
        connect();
        console.log("ارتباط با سرور مجددا وصل شد");
    }, 1000);
};

ws.onerror = function (err) {
    console.error('Socket encountered error: ', err.message, 'Closing socket');
    ws.close();
};