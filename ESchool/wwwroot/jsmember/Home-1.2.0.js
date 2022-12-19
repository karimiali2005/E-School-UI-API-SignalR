function Empty(val) {
    return val == '' || val == undefined || val == null;
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

function PreRegistrationInsert(size) {

   
    var input = document.getElementById('file');
    var data = new FormData();

    if (input !== null) {
        var file = input.files[0];


        if (file != 'undefined' && file !=null && file.size > size * 1024 * 1024) {
            alert("حجم فایل ارسالی بیش از  حد مجاز است \r حداکثر حجم مجاز " + size.toString());
            HideLoader();
            input.value = '';
            return;
        }
        data.append('pic', input.files[0]);
    }

    var classRoom = document.getElementById('ClassRoom');
    if (Empty(classRoom.value)) {
        alert("لطفا کد ملی وارد کنید");
        return;
    }


    var irnational = document.getElementById('Irnational');
    if (Empty(irnational.value)) {
        alert("لطفا کد ملی وارد کنید");
        return;
    }


    var firstName = document.getElementById('FirstName');
    if (Empty(firstName.value)) {
        alert("لطفا نام وارد کنید");
        return;
    }

    var lastName = document.getElementById('LastName');
    if (Empty(lastName.value)) {
        alert("لطفا نام خانوادگی وارد کنید");
        return;
    }

    var fatherName = document.getElementById('FatherName');
    if (Empty(fatherName.value)) {
        alert("لطفا نام پدر وارد کنید");
        return;
    }

    var fatherNumber = document.getElementById('FatherNumber');
    if (Empty(fatherNumber.value)) {
        alert("لطفا همراه پدر وارد کنید");
        return;
    }

    var motherFullName = document.getElementById('MotherFullName');
    if (Empty(motherFullName.value)) {
        alert("لطفا نام و نام خانوادگی مادر وارد کنید");
        return;
    }

    var motherNumber = document.getElementById('MotherNumber');
    if (Empty(motherNumber.value)) {
        alert("لطفا همراه مادر وارد کنید");
        return;
    }

    data.append('classRoom', classRoom.value);
    data.append('irnational', irnational.value);
    data.append('firstName', firstName.value);
    data.append('lastName', lastName.value);
    data.append('fatherName', fatherName.value);
    data.append('fatherNumber', fatherNumber.value);
    data.append('motherFullName', motherFullName.value);
    data.append('motherNumber', motherNumber.value);
    


   

        xhttp = new XMLHttpRequest();

       
    xhttp.onreadystatechange = function () {
     

            if (this.readyState == 4 && this.status == 200) {
                if (!Empty(xhttp.responseText)) {
                    if (IsJsonString(xhttp.responseText)) {
                      
                        var result = JSON.parse(xhttp.responseText);
                        alert(result.resultMessage);

                    } else {
                        alert(xhttp.responseText);
                    }
                    /*alert("salam");*/

                }
               // HideLoader();
                //input.value = '';
                classRoom.value = '';
                irnational.value = '';
                firstName.value = '';
                lastName.value = '';
                fatherName.value = '';
                fatherNumber.value = '';
                motherFullName.value = '';
                motherNumber.value = '';
            }
        };
        xhttp.onerror = function (err) {
            //HideLoader();
            alert(err.message);
           // input.value = '';
            classRoom.value = '';
            irnational.value = '';
            firstName.value = '';
            lastName.value = '';
            fatherName.value = '';
            fatherNumber.value = '';
            motherFullName.value = '';
            motherNumber.value = '';
        };
    xhttp.open('POST', '/Home/PreRegistration');
    xhttp.send(data);

}
const bars = document.getElementById('bars');
const menu = document.getElementById('menu');
let bol = false;
bars.addEventListener('click', function () {
    if (bol) {
        menu.style.display = 'none';
        bol = false
    } else {
        menu.style.display = 'flex';
        bol = true
    }
})

//function contactInsert() {

//  //  ShowLoader();
//    $.ajax({
//        url: "/Member/RoomChatGroupInsert",
//        type: 'Post',
//        data: {
//            teacherId: teacherId,
//            teacherTitle: teacherTitle
//        },
//        success: function (response) {


//            if (response !== null && response !== undefined) {



              


//            }

//           // HideLoader();

//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//           // HideLoader();
//        }
//    });

//}

   

