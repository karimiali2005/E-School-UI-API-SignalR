function getHolidays(page) {
    

    var data = {};

    data['CountRow'] = 10;
    if (!Empty(CountRow)) {
        data['CountRow'] = checkDigits(CountRow.value) ? CountRow.value : 10
    }

    if (!Empty(page)) {
        if (page > 1) {
            data['pagenumber'] = page;
        }
    }

    passingData('/Panel/Holiday', data);
}

function holidayed() {

    var Day = document.getElementById('Day');
    var Month = document.getElementById('Month');
    var Year = document.getElementById('Year');
    var Comment = document.getElementById('Comment');

    var form = new FormData();

    form.append("Year", Year.value);
    form.append("Month", Month.value);
    form.append("Day", Day.value);
    form.append("Comment", Comment.value);

    showLoader();

    var request = new XMLHttpRequest();
    request.open('post', '/Panel/Holiday');

    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            hideError();
            var result = this.responseText;
            if (result.startsWith('ok,')) {
                window.location.href = result.replace("ok,", "");
            } else {
                alert(this.responseText);
                hideLoader();
            }
        }
    };
    request.onerror = function (err) {
        hideLoader();
        alert(err.message);
    };
    request.send(form);   

}

function holidaydelete(id) {
    var data = {};
    if (Empty(id)) {
        var ids = getChChecked();
        if (ids != null) {
            data['ids'] = JSON.stringify(ids);
        } else {
            return;
        }
    } else {
        var id2 = [id];
        data['ids'] = JSON.stringify(id2);
    }

    data['returnUrl'] = getUrl();
    passingData('/Panel/Holiday/Delete', data, 'POST');
}


function holidaydeleted() {
    var lbls = document.getElementsByClassName('lbl');
    var ids = [];
    for (let lbl of lbls) {
        ids.push(lbl.title);
    }

    var data = {};
    data['ids'] = JSON.stringify(ids);
    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/Holiday/Deleted', data, 'POST');
}

