function preRegistrationArchive(id) {
    var data = {};
    if (Empty(id)) {
  
            return;
      
    } else {
      
        data['id'] = id;
    }

    data['returnUrl'] = getUrl();
    passingData('/Panel/PreRegistration/Archive', data, 'GET');
}
function preRegistrationArchivePost(id) {
    var data = {};
    if (Empty(id)) {

        return;

    } else {

        data['id'] = id;
    }

    data['returnUrl'] = document.getElementById('returnUrl').value;
    passingData('/Panel/PreRegistration/ArchiveFinish', data, 'POST');
}
